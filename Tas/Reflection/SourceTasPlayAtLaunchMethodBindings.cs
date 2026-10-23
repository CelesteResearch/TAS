using System;
using System.Reflection;
using Celeste.Core.Reflection.Methods;
using Celeste.Mods.Tas.Tools;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasPlayAtLaunchMethodBindings
    {
        internal static void BindStaticLifecycle(SourceAssemblyMethodCatalog methods)
        {
            const BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            methods.BindActualMethod(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity, 100663731,
                typeof(SourceTasPlayAtLaunch).GetMethod("Load", flags, null, Type.EmptyTypes, null)
                ?? throw new MissingMethodException(typeof(SourceTasPlayAtLaunch).FullName, "Load"), null);
            methods.BindActualMethod(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity, 100663732,
                typeof(SourceTasPlayAtLaunch).GetMethod("Unload", flags, null, Type.EmptyTypes, null)
                ?? throw new MissingMethodException(typeof(SourceTasPlayAtLaunch).FullName, "Unload"), null);
        }
    }
}
