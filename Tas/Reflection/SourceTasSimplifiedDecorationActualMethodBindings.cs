using System;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Reflection.Methods;
using Celeste.Core.Rendering;
using Celeste.Everest.Gameplay.Environment.Ridge;
using Celeste.Game.Entities.Decoration.Cobwebs;
using Celeste.Game.Entities.Decoration.Resort;
using Celeste.Game.Entities.Decoration.Wires;
using Celeste.Game.Entities.Platforms.Wood;
using Celeste.Game.Entities.ReflectionTentacles;
using Celeste.Game.Gameplay.Decoration.MoonCreatures;
using Celeste.Game.Gameplay.Decoration.SummitClouds;
using Celeste.Game.Gameplay.Environment.Debris;
using Celeste.Game.Gameplay.Environment.Ridge;
using Celeste.Game.Gameplay.Hazards.TempleEyes;
using Celeste.Game.Profiles;
using Celeste.Simulation.Gameplay.Walls.Concealment;

namespace Celeste.Mods.Tas.Reflection
{
    public static class SourceTasSimplifiedDecorationActualMethodBindings
    {
        private const BindingFlags InstanceDeclared = BindingFlags.Public | BindingFlags.NonPublic |
                                                      BindingFlags.Instance | BindingFlags.DeclaredOnly;

        public static void Register(GameApplication process)
        {
            BindRender(process, "Celeste.CliffsideWindFlag", typeof(OriginalCliffsideWindFlagState));
            BindRender(process, "Celeste.Flagline", process.SourceProfile == GameProfile.Original
                ? typeof(OriginalFlaglineState)
                : typeof(EverestFlaglineState));
            BindRender(process, "Celeste.FakeWall", typeof(FakeWallState));
            BindRender(process, "Celeste.ReflectionTentacles", typeof(OriginalReflectionTentaclesState));
            BindRender(process, "Celeste.SummitCloud", typeof(OriginalSummitCloud));
            BindRender(process, "Celeste.TempleEye", typeof(OriginalTempleEyeState));
            BindRender(process, "Celeste.Wire", typeof(OriginalWireState));
            BindRender(process, "Celeste.Cobweb", typeof(OriginalCobwebState));
            BindRender(process, "Celeste.HangingLamp", typeof(OriginalHangingLampState));
            BindRender(process, "Celeste.LightBeam", typeof(OriginalLightBeamState));
            BindRender(process, "Celeste.SinkingPlatform", typeof(OriginalSinkingPlatformState));
            BindConstructor(process, "Celeste.FloatingDebris", typeof(OriginalFloatingDebrisState),
                typeof(IOriginalFloatingDebrisProcess));
            BindConstructor(process, "Celeste.MoonCreature", typeof(OriginalMoonCreature),
                typeof(IOriginalMoonCreatureProcess));
            BindConstructor(process, "Celeste.ResortLantern", typeof(OriginalResortLanternState),
                typeof(IOriginalResortFixtureEnvironment));
        }

        private static SourceMethodDefinition FindDefinition(GameApplication process,
            string sourceType, string name, string[] sourceParameters)
        {
            var definition =
                process.SourceMembers.GetOriginalTypeDefinition(process.OriginalGameAssemblyIdentity, sourceType);
            return process.CanonicalSourceMethods.EnumerateDeclaredDefinitions(definition.AssemblyIdentity, sourceType)
                .Single(method =>
                    method.Name == name && method.ParameterTypeIdentities.SequenceEqual(sourceParameters));
        }

        private static void BindRender(GameApplication process, string sourceType, Type actualType)
        {
            var definition = FindDefinition(process, sourceType, "Render", Array.Empty<string>());
            var method = actualType.GetMethod("Render", InstanceDeclared, null,
                             new[] { typeof(ISourceRenderContext) }, null) ??
                         throw new MissingMethodException(actualType.FullName, "Render(ISourceRenderContext)");
            process.CanonicalSourceMethods.BindActualMethod(definition.AssemblyIdentity,
                definition.MethodDefinitionToken, method, null);
        }

        private static void BindConstructor(GameApplication process, string sourceType,
            Type actualType, Type processRole)
        {
            if (!processRole.IsInstanceOfType(process))
                throw new InvalidOperationException("The actual constructor environment is not implemented: " +
                                                    processRole.FullName);
            var definition = FindDefinition(process, sourceType, ".ctor", new[] { "Microsoft.Xna.Framework.Vector2" });
            var constructor = actualType.GetConstructor(InstanceDeclared, null,
                                  new[] { typeof(Vector2), processRole }, null) ??
                              throw new MissingMethodException(actualType.FullName,
                                  ".ctor(Vector2, " + processRole.Name + ")");
            process.CanonicalSourceMethods.BindActualConstructor(definition.AssemblyIdentity,
                definition.MethodDefinitionToken, constructor);
        }
    }
}
