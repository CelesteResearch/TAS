using System;
using System.Reflection;
using Celeste.Core.Reflection.Methods;
using Celeste.Mods.Tas.Tools;
using Celeste.Mods.Tas.Information;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasStudioInstallationMethodBindings
    {
        internal static void BindStaticLifecycle(SourceAssemblyMethodCatalog methods)
        {
            Bind(methods, 100664933, typeof(SourceTasStudioHelper), "Load");
            Bind(methods, 100664934, typeof(SourceTasStudioHelper), "Initialize");
            Bind(methods, 100664939, typeof(SourceTasStudioUpdateBanner), "Load");
        }

        private static void Bind(SourceAssemblyMethodCatalog methods, int token, Type type, string name)
        {
            const BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            methods.BindActualMethod(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity, token,
                type.GetMethod(name, flags, null, Type.EmptyTypes, null)
                ?? throw new MissingMethodException(type.FullName, name), null);
        }
    }
}
