using System;
using System.Collections.Generic;
using System.Reflection;
using Celeste.Core.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;
using Celeste.Game.Reflection.Metadata;
using Celeste.Scene.Tracking;
using Celeste.Scene.Tracking.Initialization;

namespace Celeste.Mods.Tas.Reflection
{
    /// <summary>Publishes tracker views of the canonical original TAS member and method metadata.</summary>
    public static class SourceTasLogicalTypeFacts
    {
        public static void Register(SceneEntityCatalog catalog, SourceMemberCatalog members,
            SourceAssemblyMethodCatalog methods)
        {
            const string identity = global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata
                .TasAssemblyIdentity;
            if (catalog.GetLogicalAssemblyTypeMetadata(identity).Length != 0)
                throw new InvalidOperationException(
                    "The canonical TAS tracker type views have already been published.");

            var definitions = new List<SourceTypeDefinition>();
            foreach (var definition in members.OriginalTypeDefinitions)
                if (definition.AssemblyIdentity == identity && definition.Identity != "<Module>")
                    definitions.Add(definition);
            if (definitions.Count == 0)
                throw new InvalidOperationException("The canonical original TAS type inventory is not published.");
            definitions.Sort((left, right) => left.MetadataToken.CompareTo(right.MetadataToken));

            // All views share the same assembly owner. Each SceneEntityType is published once;
            // successive GetTypes calls return these same objects, as original Type identity requires.
            var assembly = new SourceCatalogAssemblyMetadata(catalog, identity);
            var keys = new HashSet<string>(StringComparer.Ordinal);
            foreach (var definition in definitions)
            {
                string registryKey = GetRegistryKey(definition);
                if (!keys.Add(registryKey))
                    throw new InvalidOperationException(
                        "Distinct original TAS types have the same tracker registry key.");
                var bases = new List<string>();
                var visited = new HashSet<SourceTypeDefinition>();
                for (var parent = members.GetOriginalBaseTypeDefinition(definition);
                     parent != null;
                     parent = members.GetOriginalBaseTypeDefinition(parent))
                {
                    if (!visited.Add(parent))
                        throw new InvalidOperationException(
                            "The canonical original TAS base type chain contains a cycle.");
                    bases.Add(parent.Identity);
                }

                var tracked = new List<bool>();
                int pooled = 0;
                foreach (var attribute in definition.DeclaredAttributes)
                {
                    if (attribute.TypeIdentity == "Monocle.Tracked")
                        tracked.Add(ReadTracked(attribute));
                    else if (attribute.TypeIdentity == "Monocle.Pooled")
                    {
                        RequireParameterless(attribute, "System.Void Monocle.Pooled::.ctor()");
                        pooled++;
                    }
                    else if (attribute.TypeIdentity == "Celeste.Mod.TrackedAsAttribute")
                        // The genuine published TAS PE declares none. Do not fabricate aliases
                        // or silently interpret a future unsupported constructor/blob shape.
                        throw new InvalidOperationException(
                            "An unsupported original TAS TrackedAs attribute was published.");
                }

                bool publicParameterless = false;
                foreach (var method in methods.EnumerateDeclaredDefinitions(identity, definition.Identity))
                {
                    if (method.Name != ".ctor" || ((MethodAttributes)method.Attributes & MethodAttributes.Static) != 0
                                               || ((MethodAttributes)method.Attributes &
                                                   MethodAttributes.MemberAccessMask) != MethodAttributes.Public)
                        continue;
                    if (method.ParameterTypeIdentities == null)
                        throw new InvalidOperationException(
                            "Original TAS constructor parameter facts are not published.");
                    if (method.ParameterTypeIdentities.Count == 0) publicParameterless = true;
                }

                string sourceName = definition.Identity.Substring(definition.Identity.LastIndexOf('+') + 1);
                sourceName = sourceName.Substring(sourceName.LastIndexOf('.') + 1);
                catalog.RegisterLogicalTypeFacts(definition.Identity, registryKey, sourceName,
                    definition.AssemblyIdentity, "CelesteTAS-EverestInterop", definition.MetadataToken,
                    ((TypeAttributes)definition.Attributes & TypeAttributes.Abstract) != 0,
                    bases.ToArray(), tracked.ToArray(), assembly);
                catalog.PublishDeclaredPoolFacts(registryKey, pooled, publicParameterless);
                catalog.PublishDeclaredTrackedAliases(registryKey, Array.Empty<SourceTrackedAliasMetadata>());
            }
        }

        private static string GetRegistryKey(SourceTypeDefinition definition)
        {
            // The PE identity is an import record. Runtime actors use their actual CLR name.
            Type actualType = definition.Identity switch
            {
                "TAS.EverestInterop.RemoveSelfComponent" =>
                    typeof(global::Celeste.Mods.Tas.EverestInterop.SourceTasRemoveSelfComponent),
                "TAS.EverestInterop.InfoHUD.SelectedAreaEntity" =>
                    typeof(global::Celeste.Mods.Tas.Information.SourceTasSelectedAreaEntity),
                "TAS.Input.Commands.RequireDependencyCommand+OuiInstallDependenciesConfirmation" =>
                    typeof(global::Celeste.Mods.Tas.Commands.SourceTasDependencyConfirmationState),
                "TAS.Input.Commands.RequireDependencyCommand+LevelExitExt" =>
                    typeof(global::Celeste.Mods.Tas.Commands.SourceTasDependencyLevelExitSequence),
                "TAS.Input.Commands.RequireDependencyCommand+OverworldLoaderExt" => typeof(
                    global::Celeste.Mods.Tas.Commands.SourceTasDependencyOverworldLoaderSequence),
                _ => null
            };
            if (actualType != null) return actualType.FullName;

            // Metadata-only imported types are not fabricated CLR runtime types.
            const string importedPrefix = "TAS.";
            string importedName = definition.Identity.StartsWith(importedPrefix, StringComparison.Ordinal)
                ? definition.Identity.Substring(importedPrefix.Length)
                : definition.Identity;
            return "Celeste.Mods.Tas.ImportedTypes." + importedName.Replace('+', '.');
        }

        private static bool ReadTracked(SourceDeclaredAttribute attribute)
        {
            var blob = attribute.ValueBlob;
            if (attribute.ConstructorIdentity != "System.Void Monocle.Tracked::.ctor(System.Boolean)"
                || blob.Count != 5 || blob[0] != 1 || blob[1] != 0 || blob[2] > 1
                || blob[3] != 0 || blob[4] != 0)
                throw new InvalidOperationException(
                    "The original TAS Tracked attribute constructor/blob is unsupported.");
            return blob[2] != 0;
        }

        private static void RequireParameterless(SourceDeclaredAttribute attribute, string constructor)
        {
            var blob = attribute.ValueBlob;
            if (attribute.ConstructorIdentity != constructor || blob.Count != 4
                                                             || blob[0] != 1 || blob[1] != 0 || blob[2] != 0 ||
                                                             blob[3] != 0)
                throw new InvalidOperationException(
                    "The original TAS parameterless attribute constructor/blob is unsupported.");
        }
    }
}
