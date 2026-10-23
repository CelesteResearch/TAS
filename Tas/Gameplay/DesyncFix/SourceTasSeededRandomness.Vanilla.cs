using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Linq;
using Celeste.Core.Randomness;
using Celeste.Core.Mathematics.Framework;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;
using Celeste.Everest.Reflection;
using Celeste.Everest.Runtime.Events;
using Celeste.Scene.Tracking;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Profiles;
using MonoMod.Utils;
using MonoMod.Cil;

namespace Celeste.Mods.Tas.Gameplay.DesyncFix
{
    internal sealed partial class SourceTasSeededRandomness
    {
        private const string GameAssembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        private DebrisHandler debris;
        private AuraHelperLanternHandler auraLantern;
        private AuraHelperGeneratorHandler auraGenerator;
        private PandorasBoxTileGlitcherHandler pandoras;
        private VortexHelperColorSwitchHandler vortex;
        internal AuraHelperLanternHandler AuraLantern => auraLantern ??= new AuraHelperLanternHandler(this);
        internal AuraHelperGeneratorHandler AuraGenerator => auraGenerator ??= new AuraHelperGeneratorHandler(this);
        internal PandorasBoxTileGlitcherHandler Pandoras => pandoras ??= new PandorasBoxTileGlitcherHandler(this);
        internal VortexHelperColorSwitchHandler Vortex => vortex ??= new VortexHelperColorSwitchHandler(this);
        private bool Installed => process.SourceProfile == GameProfile.InstalledEverest;
        private ISourceRandom DeepClone(ISourceRandom from) => driver.SourceSpeedrunTool.DeepClone(from);

        private int PositionHash(Vector2 position) => Installed
            ? SourceSingleHashMath.GetVector2HashCodeInstalled(position)
            : SourceSingleHashMath.GetVector2HashCodeOriginal(position);

        private OriginalLevelSceneRoot CurrentLevel()
            => process.Frame.Scenes.CurrentScene switch
            {
                OriginalLevelSceneRoot level => level,
                global::Celeste.Game.Execution.LevelLoader.OriginalLevelLoaderScene loader => loader.Level,
                _ => null
            };

        private static int PositionArgument(MethodBase method)
        {
            var parameters = method.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
                if (parameters[i].ParameterType == typeof(Vector2))
                    return i + (method.IsStatic ? 0 : 1);
            throw new InvalidOperationException("Seeded source position has no qualified actual Vector2 parameter: " +
                                                method);
        }

        private ConstructorInfo AuraConstructor(Type actualType)
        {
            var reflection = ((ISourceEverestReflectionEnvironment)process).Reflection;
            var type = reflection.SourceMembers.GetOriginalTypeDefinition(actualType);
            var definition = reflection.SourceMethods.EnumerateDeclaredDefinitions(type.AssemblyIdentity, type.Identity)
                .SingleOrDefault(d =>
                    d.Name == ".ctor" && ((MethodAttributes)d.Attributes & MethodAttributes.MemberAccessMask) ==
                                      MethodAttributes.Public
                                      && d.ParameterTypeIdentities.SequenceEqual(new[]
                                          { "Microsoft.Xna.Framework.Vector2", "System.String", "System.Int32" }));
            return definition == null
                ? null
                : (ConstructorInfo)reflection.SourceMethods.RequireActualMethodBase(type.AssemblyIdentity,
                    definition.MethodDefinitionToken);
        }

        private MethodInfo Vanilla(int originalToken, int installedToken)
            => ((ISourceEverestReflectionEnvironment)process).Reflection.SourceMethods
                .RequireActualMethod(GameAssembly, Installed ? installedToken : originalToken);

        private void ConstructRemainingHandlers()
        {
            debris = new DebrisHandler(this);
            handlers.Add(debris);
            handlers.Add(new PrologueBridgeHandler(this));
            handlers.Add(new SummitLaunchHandler(this));
        }

        internal void Initialize()
        {
            var reflection = driver.SourceModReflection;
            if (reflection.GetModule("AuraHelper") != null)
            {
                handlers.Add(AuraLantern);
                handlers.Add(AuraGenerator);
            }

            if (reflection.GetModule("AurorasHelper") != null) handlers.Add(new AurorasHelperHandler(this));
            if (reflection.GetModule("BossesHelper") != null) handlers.Add(new BossesHelperHandler(this));
            if (reflection.GetModule("CrystallineHelper") != null) handlers.Add(new CrystallineHelperHandler(this));
            if (reflection.GetModule("PandorasBox") != null) handlers.Add(Pandoras);
            if (reflection.GetModule("Spekio's Toolbox") != null) handlers.Add(new SpekioToolboxShooterHandler(this));
            if (reflection.GetModule("VortexHelper") != null) handlers.Add(Vortex);
            ((IEverestGameEventsEnvironment)process).GameEvents.OnLoadLevel +=
                (_, _, fromLoader) => OnLoadLevel(fromLoader);
            foreach (var handler in handlers) handler.Init();
        }

        private IEnumerable<(MethodInfo Method, int Argument)> DebrisInitMethods()
        {
            var reflection = ((ISourceEverestReflectionEnvironment)process).Reflection;
            var catalog = ((ISourceSceneCatalogEnvironment)process).SceneCatalog;
            // Import identities retain the original type-name selection and aggregate TypeDef order.
            foreach (var assembly in reflection.GetOriginalAggregateAssemblyOrder().Distinct())
            foreach (var type in catalog.GetRuntimeAssemblyTypeMetadata(assembly))
            {
                int separator = Math.Max(type.Identity.LastIndexOf('.'), type.Identity.LastIndexOf('+'));
                if (!type.Identity.Substring(separator + 1).EndsWith("Debris")) continue;
                var current = catalog.RequireActualTypeForLogicalIdentity(type.Identity);
                while (current != null && current.IsSubclassOf(typeof(object)))
                {
                    var methods = new List<(MethodInfo Method, SourceMethodDefinition Definition)>();
                    foreach (var method in current.GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
                                                              BindingFlags.Instance | BindingFlags.Static |
                                                              BindingFlags.DeclaredOnly))
                    {
                        // A new portable helper does not acquire an original Init identity from its owner.
                        if (!reflection.SourceMethods.TryGetOriginalMethodDefinition(method, out var definition) ||
                            definition.Name != "Init" || method.IsStatic) continue;
                        methods.Add((method, definition));
                    }

                    foreach (var entry in methods.OrderBy(entry => entry.Definition.MethodDefinitionToken))
                    {
                        var parameters = entry.Method.GetParameters();
                        if (!parameters.Any(parameter => parameter.ParameterType == typeof(Vector2))) continue;
                        yield return (entry.Method, PositionArgument(entry.Method));
                    }

                    // Traverse the actual CLR hierarchy, including inherited private methods, as TAS does.
                    current = current.BaseType;
                }
            }
        }

        internal sealed class DebrisHandler : Handler
        {
            public override string Name => "Debris";
            private ISourceRandom debrisRandom;
            private int debrisAmount;

            internal DebrisHandler(SourceTasSeededRandomness owner) : base(owner)
            {
            }

            public override void Init()
            {
                var methods = new Dictionary<MethodInfo, int>();
                if (owner.Installed) methods.Add(owner.Vanilla(100668969, 100673257), 1);
                methods.Add(owner.Vanilla(100668969, 100673250), 1);
                methods.Add(owner.Vanilla(100668970, 100673251), 1);
                foreach (var entry in owner.DebrisInitMethods()) methods[entry.Method] = entry.Argument;
                foreach (var entry in methods)
                    owner.driver.SourceHooks.SeedPositionMethod(entry.Key, PositionArgument(entry.Key), PushRandom,
                        PopRandom);
            }

            private void PushRandom(Vector2 position)
            {
                if (!owner.driver.Running) return;
                if (debrisRandom != null)
                {
                    owner.RandomProcess.PushRandom(debrisRandom);
                    return;
                }

                debrisAmount++;
                int seed = unchecked(debrisAmount + owner.PositionHash(position));
                if (owner.process.Frame.Scenes.CurrentScene is OriginalLevelSceneRoot level)
                    seed = unchecked(seed +
                                     ((global::Celeste.Game.Content.Maps.Levels.OriginalLevelRoomData)level.Session
                                         .LevelData).LoadSeed);
                owner.RandomProcess.PushRandom(seed);
            }

            private void PopRandom()
            {
                if (owner.driver.Running) owner.RandomProcess.PopRandom();
            }

            public override void Reset() => debrisRandom = null;

            public override void PreUpdate()
            {
                if (NextSeed(out int seed))
                {
                    debrisRandom = owner.NewRandom(seed);
                    AssertNoSeedsRemaining();
                }

                debrisAmount = 0;
            }

            public override void SaveState(Dictionary<string, object> data) => data["DebrisHandler_debrisRandom"] =
                debrisRandom == null ? null : owner.DeepClone(debrisRandom);

            public override void LoadState(Dictionary<string, object> data)
            {
                var random = (ISourceRandom)data["DebrisHandler_debrisRandom"];
                debrisRandom = random == null ? null : owner.DeepClone(random);
            }
        }

        internal sealed class PrologueBridgeHandler : Handler
        {
            public override string Name => "Celeste_PrologueBridge";
            private ISourceRandom bridgeRandom;

            internal PrologueBridgeHandler(SourceTasSeededRandomness owner) : base(owner)
            {
            }

            public override void Init() => owner.driver.SourceHooks.SeedMethod(owner.Vanilla(100670113, 100675559),
                () => bridgeRandom, owner.RandomProcess);

            public override void Reset() => bridgeRandom = null;

            public override void PreUpdate()
            {
                if (NextSeed(out int seed))
                {
                    bridgeRandom = owner.NewRandom(seed);
                    AssertNoSeedsRemaining();
                }
            }

            public override void SaveState(Dictionary<string, object> data) =>
                data["PrologueBridgeHandler_bridgeRandom"] =
                    bridgeRandom == null ? null : owner.DeepClone(bridgeRandom);

            public override void LoadState(Dictionary<string, object> data)
            {
                var random = (ISourceRandom)data["PrologueBridgeHandler_bridgeRandom"];
                bridgeRandom = random == null ? null : owner.DeepClone(random);
            }
        }

        internal sealed class SummitLaunchHandler : Handler
        {
            public override string Name => "Celeste_SummitLaunch";
            private ISourceRandom shakeRandom;

            internal SummitLaunchHandler(SourceTasSeededRandomness owner) : base(owner)
            {
            }

            public override void Init()
            {
                owner.driver.SourceHooks.SeedMethod(owner.Vanilla(100667298, 100669364).GetStateMachineTarget(),
                    () => shakeRandom, owner.RandomProcess);
                owner.driver.SourceHooks.SeedMethod(
                    owner.driver.SourceModReflection
                        .GetMethod("StrawberryJam2021", "Celeste.Mod.StrawberryJam2021.Entities.CustomAscendManager",
                            "Routine")?.ActualMethod?.GetStateMachineTarget(), () => shakeRandom, owner.RandomProcess);
            }

            public override void Reset() => shakeRandom = null;

            public override void PreUpdate()
            {
                if (NextSeed(out int seed))
                {
                    shakeRandom = owner.NewRandom(seed);
                    AssertNoSeedsRemaining();
                }
            }

            public override void SaveState(Dictionary<string, object> data) => data["SummitLaunchHandler_shakeRandom"] =
                shakeRandom == null ? null : owner.DeepClone(shakeRandom);

            public override void LoadState(Dictionary<string, object> data)
            {
                var random = (ISourceRandom)data["SummitLaunchHandler_shakeRandom"];
                shakeRandom = random == null ? null : owner.DeepClone(random);
            }
        }
    }
}
