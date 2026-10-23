using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Components;
using Celeste.Core.Rendering;
using Celeste.Audio.Fmod.Events;
using Celeste.Game.Audio.Runtime;
using Celeste.Game.Entities.Decoration.Decals;
using Celeste.Game.Presentation.Effects.Dust;
using Celeste.Game.Presentation.PostProcessing.ColorGrading;
using Celeste.Game.Execution.LevelLoader.Terrain;
using Celeste.Game.Execution.Levels;
using Celeste.Scene.Entities;
using Celeste.Simulation.Runtime.Session;
using Celeste.Simulation.Gameplay.Hazards.Spikes;
using Celeste.Simulation.Gameplay.Hazards.Spinners.Dust;
using Celeste.Graphics.Content.Atlas;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;

namespace Celeste.Mods.Tas.EverestInterop
{
    public sealed partial class SourceTasSimplifiedGraphicsFeature
    {
        private readonly Dictionary<string, IDisposable> ownHooks = new();

        private void OwnIl(string key, MethodBase method, ILContext.Manipulator callback) =>
            ownHooks.Add(key, new ILHook(method, callback));

        private void OwnOn(string key, MethodBase method, Delegate callback) =>
            ownHooks.Add(key, new Hook(method, callback));

        private void RemoveOwnHook(string key)
        {
            if (ownHooks.Remove(key, out var hook)) hook.Dispose();
        }

        private ConstructorInfo SourceConstructor(Type type, string[] originalParameters)
        {
            var sourceType = Members.GetOriginalTypeDefinition(type);
            var definition = Methods.EnumerateDeclaredDefinitions(sourceType.AssemblyIdentity, sourceType.Identity)
                .Single(method =>
                    method.Name == ".ctor" && method.ParameterTypeIdentities.SequenceEqual(originalParameters));
            return (ConstructorInfo)Methods.RequireActualMethodBase(sourceType.AssemblyIdentity,
                definition.MethodDefinitionToken);
        }

        private void ClutterConstructorReturned(ILContext il)
        {
            var cursor = new ILCursor(il);
            while (cursor.TryGotoNext(MoveType.Before, instruction => instruction.MatchRet()))
            {
                cursor.EmitLdarg0();
                cursor.EmitDelegate((Action<SceneEntityState>)AddClutterRemoval);
                cursor.Index++;
            }
        }

        public void Initialize()
        {
            OwnOn("Level.Update", Method(typeof(OriginalLevelSceneRoot), "Update"),
                (Action<Action<OriginalLevelSceneRoot>, OriginalLevelSceneRoot>)((original, self) =>
                {
                    original(self);
                    LevelUpdated(self);
                }));
            if (ModMethod("PandorasBox", "Celeste.Mod.PandorasBox.TileGlitcher", "tileGlitcher") is { } tileGlitcher)
                driver.SourceHooks.IlHook(tileGlitcher.GetStateMachineTarget(), ModTileGlitcher);
            driver.SourceHooks.SkipMethod(Method(GameType("Celeste.MirrorSurfaces"), "Render"), IsSimplifiedGraphics);
            OwnIl("Lighting.Render", Method(GameType("Celeste.LightingRenderer"), "Render"), LightingRendererRender);
            OwnOn("ColorGrade.Set", Methods.RequireActualMethod(driver.Process.OriginalGameAssemblyIdentity, 100674463),
                (Action<Action<OriginalColorGradeProcess, AtlasEntry, AtlasEntry, float>, OriginalColorGradeProcess,
                    AtlasEntry, AtlasEntry, float>)ColorGradeSet);
            OwnIl("Bloom.Apply", Method(GameType("Celeste.BloomRenderer"), "Apply"), BloomRendererApply);
            OwnOn("Decal.Render", Method(typeof(OriginalDecalEntity), "Render"),
                (Action<
                    Action<global::Celeste.Simulation.Presentation.Decals.Entities.SourceDecalEntityState,
                        ISourceRenderContext>,
                    global::Celeste.Simulation.Presentation.Decals.Entities.SourceDecalEntityState,
                    ISourceRenderContext>)
                DecalRender);
            driver.SourceHooks.SkipMethods(IsSimplifiedDecal,
                Method(GameType("Celeste.CliffsideWindFlag"), "Render"), Method(GameType("Celeste.Flagline"), "Render"),
                Method(GameType("Celeste.FakeWall"), "Render"));
            driver.SourceHooks.SkipMethods(IsSimplifiedParticle,
                Method(GameType("Monocle.ParticleSystem"), "Render", []),
                Method(GameType("Monocle.ParticleSystem"), "Render", ["System.Single"]));
            driver.SourceHooks.SkipMethod(Method(GameType("Celeste.Glitch"), "Apply", originalStatic: true),
                IsSimplifiedDistort);
            driver.SourceHooks.SkipMethod(Method(GameType("Celeste.MiniTextbox"), "Render"), IsSimplifiedMiniTextbox);
            OwnIl("Distort.Render", Method(GameType("Celeste.Distort"), "Render", originalStatic: true), DistortRender);
            OwnOn("SolidTiles.ctor",
                SourceConstructor(typeof(OriginalSolidTiles),
                    ["Microsoft.Xna.Framework.Vector2", "Monocle.VirtualMap`1<System.Char>"]),
                (Action<Action<OriginalSolidTiles, Vector2, Celeste.Core.Collections.VirtualMap<char>,
                        Celeste.Simulation.Gameplay.Platforms.Foundation.ISourceSolidWorld>,
                    OriginalSolidTiles, Vector2, Celeste.Core.Collections.VirtualMap<char>,
                    Celeste.Simulation.Gameplay.Platforms.Foundation.ISourceSolidWorld>)SolidTilesCtor);
            OwnIl("Autotiler.GetTile", Method(GameType("Celeste.Autotiler"), "GetTile"), AutotilerGetTile);
            OwnOn("Entity.Render", Method(GameType("Monocle.Entity"), "Render"),
                (Action<Action<SceneEntityState, ISourceRenderContext>, SceneEntityState, ISourceRenderContext>)
                BackgroundTilesRender);
            OwnIl("Backdrop.Render", Method(GameType("Celeste.BackdropRenderer"), "Render"), BackdropRendererRender);
            OwnOn("DustStyles.Get", Methods.RequireActualMethod(driver.Process.OriginalGameAssemblyIdentity, 100670507),
                (Func<Func<OriginalDustStyleProvider, int, DustStylePresentation>, OriginalDustStyleProvider, int,
                    DustStylePresentation>)DustStylesGet);
            OwnOn("Lightning.RenderBlock",
                typeof(global::Celeste.Simulation.Gameplay.Hazards.Lightning.LightningRendererState)
                    .GetMethod("ShouldRenderBlock",
                        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                ?? throw new MissingMethodException("The vanilla Lightning renderer has no block policy."),
                (Func<Func<global::Celeste.Simulation.Gameplay.Hazards.Lightning.LightningRendererState,
                        global::Celeste.Simulation.Gameplay.Hazards.Lightning.LightningState, bool>,
                    global::Celeste.Simulation.Gameplay.Hazards.Lightning.LightningRendererState,
                    global::Celeste.Simulation.Gameplay.Hazards.Lightning.LightningState, bool>)LightningRenderBlock);
            OwnOn("Lightning.RenderEdges",
                typeof(global::Celeste.Simulation.Gameplay.Hazards.Lightning.LightningRendererState)
                    .GetMethod("ShouldRenderEdges",
                        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                ?? throw new MissingMethodException("The vanilla Lightning renderer has no edge policy."),
                (Func<Func<global::Celeste.Simulation.Gameplay.Hazards.Lightning.LightningRendererState, bool>,
                    global::Celeste.Simulation.Gameplay.Hazards.Lightning.LightningRendererState, bool>)
                LightningRenderEdges);
            driver.SourceHooks.OverrideReturns(SimplifiedWavedBlock, 0f, Method(GameType("Celeste.DreamBlock"), "Lerp"),
                Method(GameType("Celeste.LavaRect"), "Wave"));
            driver.SourceHooks.OverrideReturns(SimplifiedWavedBlock, 0f, driver.SourceModReflection.GetTypes()
                .Where(type =>
                    global::Celeste.Core.Reflection.Types.TypeReflectionIdentity.Resolve(Members, type).TypeIdentity
                        ?.EndsWith("Renderer+Edge") == true)
                .Select(type =>
                    type == typeof(global::Celeste.Simulation.Gameplay.Enemies.Seeker.SeekerBarrierRendererState.Edge)
                        ? Method(type, "GetWaveAt", ["System.Single", "System.Single", "System.Single"])
                        : driver.SourceModReflection.GetMethodInfo(type, "GetWaveAt", logFailure: false)?.ActualMethod)
                .ToArray());
            OwnIl("Bolt.Render", Method(GameType("Celeste.LightningRenderer+Bolt"), "Render"), BoltRender);
            OwnIl("Level.Render", Method(typeof(OriginalLevelSceneRoot), "Render"), LevelRender);
            OwnOn("Audio.Play", Methods.RequireActualMethod(driver.Process.OriginalGameAssemblyIdentity, 100674602),
                (Func<Func<OriginalGameAudioSession, string, FmodEventReference>, OriginalGameAudioSession, string,
                    FmodEventReference>)AudioPlay);
            driver.SourceHooks.SkipMethods(IsSimplifiedLightningStrike,
                Method(GameType("Celeste.LightningStrike"), "Render"),
                ModMethod("ContortHelper", "ContortHelper.BetterLightningStrike", "Render"));
            driver.SourceHooks.SkipMethods(IsSimplifiedClutteredEntity,
                Method(GameType("Celeste.ReflectionTentacles"), "Render"),
                Method(GameType("Celeste.SummitCloud"), "Render"),
                Method(GameType("Celeste.TempleEye"), "Render"), Method(GameType("Celeste.Wire"), "Render"),
                Method(GameType("Celeste.Cobweb"), "Render"),
                Method(GameType("Celeste.HangingLamp"), "Render"),
                Method(GameType("Celeste.DustGraphic+Eyeballs"), "Render"),
                Method(GameType("Celeste.LightBeam"), "Render"),
                ModMethod("BrokemiaHelper", "BrokemiaHelper.PixelRendered.PixelComponent", "Render"),
                ModMethod("JungleHelper", "Celeste.Mod.JungleHelper.Entities.Firefly", "Render"),
                ModMethod("SorbetHelper", "Celeste.Mod.SorbetHelper.Entities.CustomLightBeam", "Render"));
            driver.SourceHooks.IlHook(Method(GameType("Celeste.SinkingPlatform"), "Render"), (cursor, _) =>
            {
                var shaker =
                    typeof(global::Celeste.Scene.Components.Shaking.ShakerComponentState).GetField(
                        nameof(global::Celeste.Scene.Components.Shaking.ShakerComponentState.Value)) ??
                    throw new MissingFieldException(
                        typeof(global::Celeste.Scene.Components.Shaking.ShakerComponentState).FullName, "Value");
                if (!cursor.TryGotoNext(MoveType.After, instruction => MatchActualField(instruction, shaker)))
                    throw new InvalidOperationException(
                        "The actual SinkingPlatform Render no longer reads its shaker Value.");
                {
                    cursor.EmitDelegate((Func<bool>)IsSimplifiedClutteredEntity);
                    cursor.EmitDelegate(
                        (Func<Vector2, bool, Vector2>)((amount, ignore) => ignore ? Vector2.Zero : amount));
                }
            });
            OwnIl("FloatingDebris.ctor",
                SourceConstructor(GameType("Celeste.FloatingDebris"), ["Microsoft.Xna.Framework.Vector2"]),
                ClutterConstructorReturned);
            OwnIl("MoonCreature.ctor",
                SourceConstructor(GameType("Celeste.MoonCreature"), ["Microsoft.Xna.Framework.Vector2"]),
                ClutterConstructorReturned);
            OwnIl("ResortLantern.ctor",
                SourceConstructor(GameType("Celeste.ResortLantern"), ["Microsoft.Xna.Framework.Vector2"]),
                ClutterConstructorReturned);
            if (driver.SourceModReflection.TryGetType("FemtoHelper", "CustomMoonCreature") is
                    { } customMoonCreatureType &&
                driver.SourceModReflection.GetMethodInfo(customMoonCreatureType, "Added")?.ActualMethod is
                    { } customMoonCreatureAdded)
            {
                driver.SourceHooks.HookAfter<SceneEntityState>(customMoonCreatureAdded, AddClutterRemoval);
                clutteredTypes.Add(customMoonCreatureType);
            }

            driver.SourceHooks.SkipMethods(IsSimplifiedHud,
                Method(GameType("Celeste.HeightDisplay"), "Render"),
                Method(GameType("Celeste.Mod.Entities.CustomHeightDisplay"), "Render"),
                ModMethod("Monika's D-Sides", "Celeste.Mod.RubysEntities.AltHeightDisplay", "Render"),
                Method(GameType("Celeste.TalkComponent+TalkComponentUI"), "Render"),
                Method(GameType("Celeste.BirdTutorialGui"), "Render"),
                Method(GameType("Celeste.CoreMessage"), "Render"),
                Method(GameType("Celeste.Mod.Entities.CustomCoreMessage"), "Render"),
                ModMethod("ArphimigonsToyBox", "Celeste.Mod.ArphimigonHelper.CustomCoreMessage", "Render"),
                ModMethod("ChroniaHelper", "ChroniaHelper.Entities.ColoredCustomCoreMessage", "Render"),
                ModMethod("VivHelper", "VivHelper.Entities.ColoredCustomCoreMessage", "Render"),
                Method(GameType("Celeste.MemorialText"), "Render"));
            OwnOn("Spikes.Added", Method(typeof(SpikesState), "Added"),
                (Action<Action<SpikesState, object>, SpikesState, object>)SpikesAdded);
        }

        public void Unload()
        {
            RemoveOwnHook("Level.Update");
            RemoveOwnHook("Lighting.Render");
            RemoveOwnHook("Bolt.Render");
            RemoveOwnHook("Level.Render");
            RemoveOwnHook("ColorGrade.Set");
            RemoveOwnHook("Bloom.Apply");
            RemoveOwnHook("Decal.Render");
            RemoveOwnHook("Distort.Render");
            RemoveOwnHook("SolidTiles.ctor");
            RemoveOwnHook("Autotiler.GetTile");
            RemoveOwnHook("Entity.Render");
            RemoveOwnHook("Backdrop.Render");
            RemoveOwnHook("DustStyles.Get");
            RemoveOwnHook("Lightning.RenderBlock");
            RemoveOwnHook("Lightning.RenderEdges");
            RemoveOwnHook("Audio.Play");
            RemoveOwnHook("FloatingDebris.ctor");
            RemoveOwnHook("MoonCreature.ctor");
            RemoveOwnHook("ResortLantern.ctor");
            RemoveOwnHook("Spikes.Added");
        }
    }
}
