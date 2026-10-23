using System;
using System.Reflection;
using Celeste.Core.Reflection.Methods;
using Celeste.Mods.Tas.Commands;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasConsoleMethodBindings
    {
        internal static void BindStaticConsoleCommands(SourceAssemblyMethodCatalog methods)
        {
            const BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            methods.BindActualMethod(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity, 100663937,
                typeof(SourceTasConsoleProcess).GetMethod("CmdGiveBerry", flags, null, Type.EmptyTypes, null)
                ?? throw new MissingMethodException(typeof(SourceTasConsoleProcess).FullName, "CmdGiveBerry"), null);
        }
    }
}
