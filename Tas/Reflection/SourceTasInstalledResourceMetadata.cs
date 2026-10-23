using System.Collections.Generic;
using Celeste.Core.Reflection.Resources;

namespace Celeste.Mods.Tas.Reflection
{
    public static class SourceTasInstalledResourceMetadata
    {
        // Complete PE ManifestResource table of the selected Installed TAS assembly has zero rows.
        public static void Register(SourceAssemblyResourceCatalog catalog)
            => catalog.RegisterCompleteEmbeddedResourceTable(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity,
                new Dictionary<string, byte[]>(), System.Array.Empty<string>());
    }
}
