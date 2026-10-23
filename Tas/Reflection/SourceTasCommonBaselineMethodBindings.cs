using System;
using System.Reflection;
using Celeste.Core.Reflection.Methods;
using Celeste.Mods.Tas.Information;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasCommonBaselineMethodBindings
    {
        internal static void BindStaticLifecycle(SourceAssemblyMethodCatalog methods)
        {
            const BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            methods.BindActualMethod(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity, 100664740,
                typeof(SourceTasCommonBaselineFont).GetMethod("Load", flags, null, Type.EmptyTypes, null)
                ?? throw new MissingMethodException(typeof(SourceTasCommonBaselineFont).FullName, "Load"), null);
            methods.BindActualMethod(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity, 100664741,
                typeof(SourceTasCommonBaselineFont).GetMethod("Unload", flags, null, Type.EmptyTypes, null)
                ?? throw new MissingMethodException(typeof(SourceTasCommonBaselineFont).FullName, "Unload"), null);
        }
    }
}
