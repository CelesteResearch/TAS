using System;
using System.Reflection;
using StudioCommunication;
using Celeste.Core.Reflection.Methods;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Information;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasStudioMethodBindings
    {
        private static readonly Type[] CommandParameters =
            { typeof(CommandLine), typeof(int), typeof(string), typeof(int) };

        internal static void BindStaticLifecycle(SourceAssemblyMethodCatalog methods)
        {
            Bind(methods, 100663446, typeof(SourceTasGameInfo), "Load", null, Type.EmptyTypes);
            Bind(methods, 100663447, typeof(SourceTasGameInfo), "Unload", null, Type.EmptyTypes);
            Bind(methods, 100663745, typeof(SourceTasSyncChecker), "InitializeSourceSyncChecker", null,
                Type.EmptyTypes);
            Bind(methods, 100663746, typeof(SourceTasSyncChecker), "UnloadSourceSyncChecker", null, Type.EmptyTypes);
            Bind(methods, 100663749, typeof(SourceTasSyncChecker), "ParseSourceFileEnd", null, Type.EmptyTypes);
            Bind(methods, 100663781, typeof(SourceTasCommandRegistry), "InitializeSourceCommandRegistry", null,
                Type.EmptyTypes);
            Bind(methods, 100663921, typeof(TasPlaybackDriver), "LoadSourceConsoleCallbacks", null, Type.EmptyTypes);
            Bind(methods, 100663922, typeof(TasPlaybackDriver), "UnloadSourceConsoleCallbacks", null, Type.EmptyTypes);
            Bind(methods, 100663992, typeof(SourceTasStudioMethodBindings), "ParseRecordingFileEnd", null,
                Type.EmptyTypes);
            Bind(methods, 100663993, typeof(SourceTasStudioMethodBindings), "ClearRecording", null, Type.EmptyTypes);
            Bind(methods, 100663994, typeof(SourceTasStudioMethodBindings), "DisableRecording", null, Type.EmptyTypes);
            Bind(methods, 100664711, typeof(SourceTasAreaCompleteInfo), "Initialize", null, Type.EmptyTypes);
            Bind(methods, 100664712, typeof(SourceTasAreaCompleteInfo), "Load", null, Type.EmptyTypes);
            Bind(methods, 100664713, typeof(SourceTasAreaCompleteInfo), "Unload", null, Type.EmptyTypes);
            Bind(methods, 100664723, typeof(SourceTasStudioMethodBindings), "EnableAreaCompleteInfo", null,
                Type.EmptyTypes);
            Bind(methods, 100664860, typeof(TasPlaybackDriver), "LoadSourcePico8Fixer", null, Type.EmptyTypes);
            Bind(methods, 100664861, typeof(TasPlaybackDriver), "UnloadSourcePico8Fixer", null, Type.EmptyTypes);
            Bind(methods, 100665169, typeof(TasPlaybackDriver), "LoadSourceStudioCommunication", null, Type.EmptyTypes);
            Bind(methods, 100665170, typeof(TasPlaybackDriver), "UnloadSourceStudioCommunication", null,
                Type.EmptyTypes);
            Bind(methods, 100665181, typeof(SourceTasGameData), "Load", null, Type.EmptyTypes);
            Bind(methods, 100665182, typeof(SourceTasGameData), "Unload", null, Type.EmptyTypes);
        }

        internal static void BindCommands(SourceAssemblyMethodCatalog methods, TasPlaybackDriver driver)
        {
            Bind(methods, 100663929, typeof(SourceTasConsoleProcess), "Execute", driver.ConsoleProcess,
                CommandParameters);
            Bind(methods, 100663990, typeof(SourceTasRecordingCommand), "StartRecording", driver.RecordingCommand,
                CommandParameters);
            Bind(methods, 100663991, typeof(SourceTasRecordingCommand), "StopRecording", driver.RecordingCommand,
                CommandParameters);
            Bind(methods, 100664724, typeof(SourceTasAreaCompleteInfo), "CompleteInfoCommand", driver.AreaCompleteInfo,
                CommandParameters);
        }

        private static void ParseRecordingFileEnd() => CelesteTasModule.Instance.Driver.RecordingCommand.ParseFileEnd();
        private static void ClearRecording() => CelesteTasModule.Instance.Driver.RecordingCommand.Clear();
        private static void DisableRecording() => CelesteTasModule.Instance.Driver.RecordingCommand.DisableRun();
        private static void EnableAreaCompleteInfo() => CelesteTasModule.Instance.Driver.AreaCompleteInfo.EnableRun();

        private static void Bind(SourceAssemblyMethodCatalog methods, int originalToken, Type actualType,
            string actualName, object actualOwner, Type[] parameters)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                                       BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            MethodInfo actualMethod = actualType.GetMethod(actualName, flags, null, parameters, null)
                                      ?? throw new InvalidOperationException("Actual source method is absent: " +
                                                                             actualType.FullName + "." + actualName);
            methods.BindActualMethod(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity,
                originalToken, actualMethod, actualOwner);
        }
    }
}
