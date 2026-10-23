using System;
using System.Reflection;
using StudioCommunication;
using Celeste.Core.Reflection.Methods;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Tools;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasGameProtectionMethodBindings
    {
        private static void Bind(SourceAssemblyMethodCatalog methods, int token, Type type, string name, object owner,
            Type[] args)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                                       BindingFlags.Instance | BindingFlags.DeclaredOnly;
            methods.BindActualMethod(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity, token,
                type.GetMethod(name, flags, null, args, null)
                ?? throw new MissingMethodException(type.FullName, name), owner);
        }

        internal static void BindStaticLifecycle(SourceAssemblyMethodCatalog methods)
        {
            Bind(methods, 100664730, typeof(SourceTasAutoMute), "Load", null, Type.EmptyTypes);
            Bind(methods, 100664731, typeof(SourceTasAutoMute), "Unload", null, Type.EmptyTypes);
            Bind(methods, 100664769, typeof(SourceTasDisableAchievements), "Load", null, Type.EmptyTypes);
            Bind(methods, 100664770, typeof(SourceTasDisableAchievements), "Unload", null, Type.EmptyTypes);
            Bind(methods, 100664867, typeof(SourceTasPreventExitGame), "Load", null, Type.EmptyTypes);
            Bind(methods, 100664868, typeof(SourceTasPreventExitGame), "Unload", null, Type.EmptyTypes);
            Bind(methods, 100664019, typeof(SourceTasSaveAndQuitReenterProcess), "Load", null, Type.EmptyTypes);
            Bind(methods, 100664020, typeof(SourceTasSaveAndQuitReenterProcess), "DisableRun", null, Type.EmptyTypes);
        }

        internal static void BindCommands(SourceAssemblyMethodCatalog methods, TasPlaybackDriver driver)
        {
            var args = new[] { typeof(CommandLine), typeof(int), typeof(string), typeof(int) };
            Bind(methods, 100663949, typeof(SourceTasExitGameCommand), "ExitGame", null, args);
            Bind(methods, 100664021, typeof(SourceTasSaveAndQuitReenterProcess), "Execute",
                driver.SourceSaveAndQuitReenter,
                args);
        }
    }
}
