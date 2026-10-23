using System;
using Celeste.Core.Reflection.Members;
using Celeste.Game.Reflection.Metadata;
using Celeste.Scene.Tracking;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.EverestInterop;
using Celeste.Mods.Tas.Information;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasSceneActorActualTypeBindings
    {
        internal static void Register(SceneEntityCatalog catalog, SourceMemberCatalog members)
        {
            Bind(catalog, members, typeof(SourceTasRemoveSelfComponent),
                "TAS.EverestInterop.RemoveSelfComponent", 33554606, "Monocle.Component");
            Bind(catalog, members, typeof(SourceTasSelectedAreaEntity),
                "TAS.EverestInterop.InfoHUD.SelectedAreaEntity", 33554613, "Monocle.Entity");
            Bind(catalog, members, typeof(SourceTasDependencyConfirmationState),
                "TAS.Input.Commands.RequireDependencyCommand+OuiInstallDependenciesConfirmation", 33554737,
                "Celeste.Oui");
            Bind(catalog, members, typeof(SourceTasDependencyLevelExitSequence),
                "TAS.Input.Commands.RequireDependencyCommand+LevelExitExt", 33554738, "Celeste.LevelExit");
            Bind(catalog, members, typeof(SourceTasDependencyOverworldLoaderSequence),
                "TAS.Input.Commands.RequireDependencyCommand+OverworldLoaderExt", 33554739, "Celeste.OverworldLoader");
        }

        private static void Bind(SceneEntityCatalog catalog, SourceMemberCatalog members, Type actualType,
            string identity, int typeToken, string baseIdentity)
        {
            var definition = members.GetOriginalTypeDefinition(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity, identity);
            var metadata = catalog.FindLogicalMetadata(identity)
                           ?? throw new InvalidOperationException(
                               "The genuine TAS scene type facts are not published: " +
                               identity);
            if (definition.MetadataToken != typeToken || definition.BaseTypeIdentity != baseIdentity
                                                      || definition.BaseAssemblyIdentity !=
                                                      "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                                                      || metadata.LogicalMetadataToken != definition.MetadataToken
                                                      || metadata.LogicalAssemblyIdentity !=
                                                      definition.AssemblyIdentity ||
                                                      metadata.Identity != definition.Identity
                                                      || !metadata.HasBaseType(baseIdentity) ||
                                                      metadata.LogicalIsAbstract != actualType.IsAbstract)
                throw new InvalidOperationException("The genuine TAS scene TypeDef or complete actual body differs: " +
                                                    identity);
            // The complete original metadata universe has one publisher. This only
            // qualifies existing whole CPU types; it creates no actor or component.
            members.RegisterSourceType(actualType, definition.AssemblyIdentity, definition.Identity);
            catalog.RegisterRuntimeType(actualType, metadata.RegistryKey);
            catalog.SelectLogicalActualType(actualType, identity);
        }
    }
}
