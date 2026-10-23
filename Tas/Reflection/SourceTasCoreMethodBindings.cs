using System;
using System.Reflection;
using StudioCommunication;
using Celeste.Core.Reflection.Methods;
using Celeste.Mods.Tas.Export;
using Celeste.Mods.Tas.Interop;
using Celeste.Mods.Tas.Playback;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasCoreMethodBindings
    {
        private static readonly Type[] CommandParameters =
            { typeof(CommandLine), typeof(int), typeof(string), typeof(int) };

        internal static void BindStaticLifecycle(SourceAssemblyMethodCatalog methods)
        {
            Bind(methods, 100664385, typeof(SourceTasHelperInterop), "SourceInitialize", null, Type.EmptyTypes);
            Bind(methods, 100664870, typeof(SourceTasRestoreSettingsProcess), "SourceTryBackup", null, Type.EmptyTypes);
            Bind(methods, 100664871, typeof(SourceTasRestoreSettingsProcess), "SourceTryRestore", null,
                Type.EmptyTypes);
            Bind(methods, 100664872, typeof(SourceTasRestoreSettingsProcess), "SourceLoad", null, Type.EmptyTypes);
            Bind(methods, 100664873, typeof(SourceTasRestoreSettingsProcess), "SourceUnload", null, Type.EmptyTypes);
            Bind(methods, 100663509, typeof(SourceTasLibTasHelper), "SourceRestartExport", null, Type.EmptyTypes);
            Bind(methods, 100663510, typeof(SourceTasLibTasHelper), "SourceFinishExport", null, Type.EmptyTypes);
            Bind(methods, 100663423, typeof(SourceTasExportGameInfo), "SourceFinishExport", null, Type.EmptyTypes);
            Bind(methods, 100663424, typeof(SourceTasExportGameInfo), "Load", null, Type.EmptyTypes);
            Bind(methods, 100663425, typeof(SourceTasExportGameInfo), "Unload", null, Type.EmptyTypes);
            Bind(methods, 100663431, typeof(SourceTasExportRoomInfo), "Load", null, Type.EmptyTypes);
            Bind(methods, 100663432, typeof(SourceTasExportRoomInfo), "Unload", null, Type.EmptyTypes);
            Bind(methods, 100663437, typeof(SourceTasExportRoomInfo), "SourceEndExport", null, Type.EmptyTypes);
            Bind(methods, 100664371, typeof(SourceTasSpeedrunToolInterop), "SourceInitialize", null, Type.EmptyTypes);
            Bind(methods, 100664372, typeof(SourceTasSpeedrunToolInterop), "SourceUnload", null, Type.EmptyTypes);
            Bind(methods, 100664373, typeof(SourceTasSpeedrunToolInterop), "SourceOnTasDisableRun", null,
                Type.EmptyTypes);
            Bind(methods, 100664071, typeof(SourceTasSavestateManager), "SourceEnableRun", null, Type.EmptyTypes);
            Bind(methods, 100664077, typeof(SourceCelesteTasExports), "SourceLoad", null, Type.EmptyTypes);
        }

        internal static void BindCommands(SourceAssemblyMethodCatalog methods, TasPlaybackDriver driver)
        {
            Bind(methods, 100663517, typeof(SourceTasLibTasHelper), "StartExportLibTasCommand",
                driver.SourceLibTasHelper,
                CommandParameters);
            Bind(methods, 100663518, typeof(SourceTasLibTasHelper), "FinishExportLibTasCommand",
                driver.SourceLibTasHelper,
                CommandParameters);
            Bind(methods, 100663519, typeof(SourceTasLibTasHelper), "AddCommand", driver.SourceLibTasHelper,
                CommandParameters);
            Bind(methods, 100663520, typeof(SourceTasLibTasHelper), "SkipCommand", driver.SourceLibTasHelper,
                CommandParameters);
            Bind(methods, 100663521, typeof(SourceTasLibTasHelper), "MarkerCommand", driver.SourceLibTasHelper,
                CommandParameters);
            Bind(methods, 100663421, typeof(SourceTasExportGameInfo), "StartExportCommand", driver.SourceExportGameInfo,
                CommandParameters);
            Bind(methods, 100663422, typeof(SourceTasExportGameInfo), "FinishExportCommand",
                driver.SourceExportGameInfo,
                CommandParameters);
            Bind(methods, 100663434, typeof(SourceTasExportRoomInfo), "StartExportCommand", driver.SourceExportRoomInfo,
                CommandParameters);
            Bind(methods, 100663435, typeof(SourceTasExportRoomInfo), "FinishExportCommand",
                driver.SourceExportRoomInfo,
                CommandParameters);
        }

        private static void Bind(SourceAssemblyMethodCatalog methods, int token, Type actualType,
            string actualName, object actualOwner, Type[] parameters)
        {
            MethodInfo actualMethod = actualType.GetMethod(actualName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance |
                BindingFlags.DeclaredOnly,
                null, parameters, null) ?? throw new MissingMethodException(actualType.FullName, actualName);
            methods.BindActualMethod(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity, token,
                actualMethod, actualOwner);
        }
    }
}
