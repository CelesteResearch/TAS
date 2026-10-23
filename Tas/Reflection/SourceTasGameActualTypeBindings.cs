using System;
using Celeste.Core.Reflection.Members;
using Celeste.Scene.Tracking;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasGameActualTypeBindings
    {
        private const string GameAssembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        internal static void Register(SceneEntityCatalog catalog, SourceMemberCatalog members)
        {
            Qualify(catalog, members, SourceTasSeedAndDesyncActualMethodBindings.AscendRoutineActualStateMachineType,
                "Celeste.AscendManager+<Routine>d__15");
        }

        private static void Qualify(SceneEntityCatalog catalog, SourceMemberCatalog members, Type actualType,
            string identity)
        {
            var source = members.GetOriginalTypeDefinition(GameAssembly, identity);
            var logical = catalog.FindLogicalMetadata(identity)
                          ?? throw new InvalidOperationException(
                              "The original physical declaration TypeDef is not published: " + identity);
            if (logical.LogicalAssemblyIdentity != source.AssemblyIdentity ||
                logical.LogicalMetadataToken != source.MetadataToken)
                throw new InvalidOperationException(
                    "The physical declaration has different canonical original TypeDef facts.");
            catalog.RegisterRuntimeType(actualType, logical.RegistryKey);
        }
    }
}
