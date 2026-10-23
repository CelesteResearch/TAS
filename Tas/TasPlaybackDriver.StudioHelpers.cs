using Celeste.Everest;
using System;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Information;
using Celeste.Mods.Tas.Query;
using Celeste.Everest.Diagnostics.Logging;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasCommandRegistry commandRegistry;

        internal SourceTasCommandRegistry CommandRegistry =>
            commandRegistry ??= new SourceTasCommandRegistry(process, this);

        internal ISourceTasCommandMeta GetSourceCommandMeta(string name) => CommandRegistry.GetMeta(name);
        internal StudioCommunication.CommandInfo[] GetSourceCommandList() => CommandRegistry.GetCommandList();
        private SourceTasModReflection sourceModReflection;

        internal SourceTasModReflection SourceModReflection =>
            sourceModReflection ??= new SourceTasModReflection(process);

        private SourceTasGameInfo sourceGameInfo;
        internal SourceTasGameInfo GameInfo => sourceGameInfo ??= new SourceTasGameInfo(process, this);
        private SourceTasConsoleProcess sourceConsoleProcess;

        internal SourceTasConsoleProcess ConsoleProcess =>
            sourceConsoleProcess ??= new SourceTasConsoleProcess(process, this);

        private SourceTasAssistProcess sourceAssistQuery;
        internal SourceTasAssistProcess SourceAssistQuery => sourceAssistQuery ??= new SourceTasAssistProcess(process);
        private SourceTasTargetQueryProcess sourceTargetQuery;

        internal SourceTasTargetQueryProcess TargetQuery => sourceTargetQuery ??= new SourceTasTargetQueryProcess(
            process,
            process, this,
            ((global::Celeste.Everest.Reflection.ISourceEverestReflectionEnvironment)process).Reflection.SourceMembers);

        private SourceTasSetProcess sourceSetProcess;

        internal SourceTasSetProcess SetProcess =>
            sourceSetProcess ??= new SourceTasSetProcess(process, this, TargetQuery);

        private SourceTasGameData sourceGameData;
        internal SourceTasGameData GameData => sourceGameData ??= new SourceTasGameData(process, this);

        internal global::Celeste.Framework.Diagnostics.Commands.SourceCommands SourceCommands =>
            ((global::Celeste.Framework.Diagnostics.Commands.ISourceCommandsProcessEnvironment)process).Commands;

        internal GameApplication Process => process;
        internal CelesteTasModule SourceModule => module;
        internal SourceTasHotkeys SourceHotkeys => hotkeys;
        internal SourceTasSyncChecker SourceSyncChecker => SyncChecker;

        internal void LogSourceCommunicationException(Exception error, string message)
        {
            var logger = ((IEverestLoggerProcessEnvironment)process).Logger;
            logger.Log(EverestLogLevel.Error, "CelesteTAS", message);
            logger.LogDetailed(error, "CelesteTAS");
        }
    }
}
