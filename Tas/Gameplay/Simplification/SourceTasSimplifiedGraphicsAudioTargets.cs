using System;
using System.Linq;
using System.Reflection;
using Celeste.Core.Reflection.Methods;
using Celeste.Core.Reflection.Members;
using Celeste.Game.Audio.Runtime;
using Celeste.Game.Presentation.Effects.Dust;
using Celeste.Game.Presentation.PostProcessing.ColorGrading;
using Celeste.Simulation.Gameplay.Hazards.Spikes;
using Celeste.Graphics.Content.Atlas;

namespace Celeste.Mods.Tas.Gameplay.Simplification
{
    public static class SourceTasSimplifiedGraphicsAudioTargets
    {
        public static void Register(GameApplication process)
        {
            SourceAssemblyMethodCatalog methods = process.Reflection.SourceMethods;
            string assembly = process.OriginalGameAssemblyIdentity;
            SourceMemberCatalog members = process.SourceMembers;
            var spikesOverride =
                members.RequireDefinition(assembly, "Celeste.Spikes", "overrideType", SourceMemberKind.Field);
            if (spikesOverride.MetadataToken != 67119412 || spikesOverride.Attributes != 1 || spikesOverride.IsStatic ||
                spikesOverride.ValueTypeReference?.TypeIdentity != "System.String")
                throw new InvalidOperationException(
                    "The source Spikes.overrideType field differs from its actual original definition.");
            FieldInfo actualOverride = typeof(SpikesState).GetField("overrideType",
                                           BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
                                       ?? throw new MissingFieldException(typeof(SpikesState).FullName, "overrideType");
            members.RegisterBinding(
                new SourcePhysicalMemberBinding(spikesOverride, typeof(SpikesState), actualOverride));
            Bind(methods, assembly, 100674602, "Celeste.Audio", "Play", new[] { "System.String" },
                typeof(OriginalGameAudioSession), "Play", new[] { typeof(string) }, process.AudioSession);
            Bind(methods, assembly, 100674463, "Celeste.ColorGrade", "Set",
                new[] { "Monocle.MTexture", "Monocle.MTexture", "System.Single" },
                typeof(OriginalColorGradeProcess), "Set",
                new[] { typeof(AtlasEntry), typeof(AtlasEntry), typeof(float) },
                ((IOriginalColorGradeEnvironment)process).ColorGradeProcess);
            Bind(methods, assembly, 100670507, "Celeste.DustStyles", "Get", new[] { "Celeste.Session" },
                typeof(OriginalDustStyleProvider), "GetStyle", new[] { typeof(int) },
                (OriginalDustStyleProvider)process.DustStyles);
            Bind(methods, assembly, 100675569, "Celeste.Spikes", "Added", new[] { "Monocle.Scene" },
                typeof(SpikesState), "Added", new[] { typeof(object) }, null);
        }

        private static void Bind(SourceAssemblyMethodCatalog methods, string assembly, int token,
            string sourceType, string sourceName, string[] sourceParameters,
            Type actualType, string actualName, Type[] actualParameters, object owner)
        {
            var definition = methods.EnumerateDeclaredDefinitions(assembly, sourceType)
                .Single(method => method.MethodDefinitionToken == token && method.Name == sourceName &&
                                  method.ParameterTypeIdentities.SequenceEqual(sourceParameters));
            MethodInfo actual = actualType.GetMethod(actualName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly,
                null, actualParameters, null) ?? throw new MissingMethodException(actualType.FullName, actualName);
            methods.BindActualMethod(assembly, definition.MethodDefinitionToken, actual, owner);
        }
    }
}
