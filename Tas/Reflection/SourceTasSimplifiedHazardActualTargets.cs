using System;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;
using Celeste.Core.Rendering;
using Celeste.Game.Entities.Hazards.Spinners.Crystal;
using Celeste.Game.Entities.Platforms.Dream;
using Celeste.Game.Gameplay.Enemies.BadelineBoss.Projectiles;
using Celeste.Game.Gameplay.Hazards.Lightning;
using Celeste.Simulation.Gameplay.Hazards.Lightning;
using Celeste.Graphics.Effects.Liquids;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasSimplifiedHazardActualTargets
    {
        internal static void Register(GameApplication process)
        {
            Bind(process, "Celeste.FinalBossBeam", typeof(FinalBossBeamState), "Render", "System.Void", [],
                [typeof(ISourceRenderContext)]);
            Bind(process, "Celeste.CrystalStaticSpinner", typeof(OriginalCrystalStaticSpinner), "CreateSprites",
                "System.Void", [], []);
            Bind(process, "Celeste.CrystalStaticSpinner", typeof(OriginalCrystalStaticSpinner), "GetHue",
                "Microsoft.Xna.Framework.Color", ["Microsoft.Xna.Framework.Vector2"], [typeof(Vector2)]);
            Bind(process, "Celeste.LightningRenderer", typeof(LightningRendererState), "Render", "System.Void", [],
                [typeof(ISourceRenderContext)]);
            Bind(process, "Celeste.LightningRenderer+Bolt", typeof(LightningRendererState.Bolt), "Render",
                "System.Void",
                [], [typeof(ISourceRenderContext)]);
            Bind(process, "Celeste.DreamBlock", typeof(OriginalDreamBlockState), "Lerp", "System.Single",
                ["System.Single", "System.Single", "System.Single"], [typeof(float), typeof(float), typeof(float)]);
            Bind(process, "Celeste.LavaRect", typeof(LavaRectangleComponentState), "Wave", "System.Single",
                ["System.Int32", "System.Single"], [typeof(int), typeof(float)]);
            Bind(process, "Celeste.LightningStrike", typeof(LightningStrikeState), "Render", "System.Void", [],
                [typeof(ISourceRenderContext)]);
            var edgeType = typeof(global::Celeste.Simulation.Gameplay.Enemies.Seeker.SeekerBarrierRendererState.Edge);
            var edgeDefinition = process.SourceMembers.GetOriginalTypeDefinition(process.OriginalGameAssemblyIdentity,
                "Celeste.SeekerBarrierRenderer+Edge");
            process.SourceMembers.RegisterSourceType(edgeType, edgeDefinition.AssemblyIdentity,
                edgeDefinition.Identity);
            Bind(process, "Celeste.SeekerBarrierRenderer+Edge", edgeType, "GetWaveAt", "System.Single",
                ["System.Single", "System.Single", "System.Single"], [typeof(float), typeof(float), typeof(float)]);
            BindField(process, "Celeste.CrystalStaticSpinner", typeof(OriginalCrystalStaticSpinner), "color");
            BindField(process, "Celeste.CrystalStaticSpinner", typeof(OriginalCrystalStaticSpinner), "expanded");
            BindField(process, "Celeste.CrystalStaticSpinner", typeof(OriginalCrystalStaticSpinner), "border");
            BindField(process, "Celeste.CrystalStaticSpinner", typeof(OriginalCrystalStaticSpinner), "filler");
            BindField(process, "Celeste.LightningRenderer", typeof(LightningRendererState), "DrawEdges");
        }

        private static void Bind(GameApplication process, string sourceType, Type actualType,
            string name, string returnType, string[] originalParameters, Type[] physicalParameters)
        {
            var declaring = process.SourceMembers.GetOriginalTypeDefinition(actualType);
            if (declaring.Identity != sourceType)
                throw new InvalidOperationException(
                    "The simplified graphics target has a different original declaring type: " + sourceType);
            var methods = process.Reflection.SourceMethods;
            var definitions = methods.EnumerateDeclaredDefinitions(declaring.AssemblyIdentity, sourceType)
                .Where(definition => definition.Name == name && definition.ReturnTypeIdentity == returnType
                                                             && definition.ParameterTypeIdentities != null
                                                             && definition.ParameterTypeIdentities.SequenceEqual(
                                                                 originalParameters)).ToArray();
            if (definitions.Length != 1)
                throw new MissingMethodException(sourceType, name);
            var definition = definitions[0];
            if ((definition.Attributes & (int)MethodAttributes.Static) != 0)
                throw new InvalidOperationException("An instance source target is required: " + sourceType + "." +
                                                    name);
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                       BindingFlags.DeclaredOnly;
            var method = actualType.GetMethod(name, flags, null, physicalParameters, null)
                         ?? throw new MissingMethodException(actualType.FullName, name);
            methods.BindActualMethod(declaring.AssemblyIdentity, definition.MethodDefinitionToken, method, null);
        }

        private static void BindField(GameApplication process, string sourceType, Type actualType, string name)
        {
            var members = process.SourceMembers;
            var declaring = members.GetOriginalTypeDefinition(actualType);
            if (declaring.Identity != sourceType)
                throw new InvalidOperationException(
                    "The simplified graphics member has a different original declaring type: " + sourceType);
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                       BindingFlags.DeclaredOnly;
            var field = actualType.GetField(name, flags);
            if (field == null && actualType == typeof(LightningRendererState) && name == "DrawEdges")
                field = typeof(TiledLightningRendererState<LightningState>).GetField(name, flags);
            field = field ?? throw new MissingFieldException(actualType.FullName, name);
            var definition =
                members.RequireDefinition(declaring.AssemblyIdentity, sourceType, name, SourceMemberKind.Field);
            members.RegisterBinding(new SourcePhysicalMemberBinding(definition, actualType, field));
        }
    }
}
