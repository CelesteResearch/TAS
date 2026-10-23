#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Game.Save.Data;
using Celeste.Game.Execution.Processes;
using Celeste.Game.Execution.Areas;
using Celeste.Mods.Tas.Input;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Information;
using Celeste.Mods.Tas.Interop;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;

namespace Celeste.Mods.Tas.Commands
{
    /// Commands which don't influence gameplay at all and just provide information to the user
    internal sealed class SourceTasMetadataCommands
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;

        internal SourceTasMetadataCommands(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        private OriginalSaveDataState? CurrentSave => ((IOriginalGameplayProcess)process).SaveContext.CurrentSave;
        private static SourceTasMetadataCommands Current => CelesteTasModule.Instance.Driver.SourceMetadataCommands;

        [SourceTasLoad]
        private static void Load() => Current.LoadSourceBody();

        [SourceTasUnload]
        private static void Unload() => Current.UnloadSourceBody();

        [SourceTasEnableRun(EnableRunPriority)]
        private static void ResetTimes() => Current.ResetTimesSourceBody();

        [SourceTasDisableRun]
        private static void UpdateTimes() => Current.UpdateTimesSourceBody();

        [SourceTasSaveState]
        private static void SaveState(Dictionary<string, object?> data) => Current.SaveStateSourceBody(data);

        [SourceTasLoadState]
        private static void LoadState(Dictionary<string, object?> data) => Current.LoadStateSourceBody(data);

        [SourceTasPreEngineUpdate]
        private static void AdvanceRealTime() => Current.AdvanceRealTimeSourceBody();

        // Track starting conditions for TAS to properly calculate (Midway)FileTime
        public (long FileTimeTicks, int FileSlot)? TasStartInfo;

        /// Total real-time frames in the TAS, without loading times
        internal (int FrameCount, int FileSlot)? RealTimeInfo = null;

        /// Only allow updating 'ActivatedLobbyWarps:' when loading with a 'console load'
        internal string? LoadedLobbySID = null;

        internal void LoadSourceBody()
        {
            process.GameEvents.OnComplete += UpdateChapterTime;
            process.LevelSourceHooks.AfterBegin += StartFileTime;
            process.LevelSourceHooks.AfterUpdateTime += StartFileTime;
        }

        internal void UnloadSourceBody()
        {
            process.GameEvents.OnComplete -= UpdateChapterTime;
            // Original HookAfter lifetime disposes at module unload; both use this one real hook list.
            process.LevelSourceHooks.AfterBegin -= StartFileTime;
            process.LevelSourceHooks.AfterUpdateTime -= StartFileTime;
        }

        private void StartFileTime(Level actualLevel)
        {
            if (!driver.Running || CurrentSave is not { } saveData) return;
            if (TasStartInfo == null || TasStartInfo.Value.FileSlot != saveData.FileSlot)
                TasStartInfo = (saveData.Time, saveData.FileSlot);
        }

        internal const int EnableRunPriority = 0;

        internal void ResetTimesSourceBody()
        {
            // Reset values, but make sure savestates overwrite with the correct value
            TasStartInfo = null;
            RealTimeInfo = null;
            LoadedLobbySID = null;
        }

        internal void UpdateTimesSourceBody()
        {
            if (TasStartInfo != null && CurrentSave != null && !driver.Controller.CanPlayback)
            {
                UpdateAllMetadata("FileTime",
                    _ => driver.GameInfo.FormatTime(CurrentSave.Time - TasStartInfo.Value.FileTimeTicks));
            }

            if (RealTimeInfo != null && !driver.Controller.CanPlayback)
            {
                UpdateAllMetadata("RealTime",
                    _ =>
                        $"{TimeSpan.FromSeconds(RealTimeInfo.Value.FrameCount / 60.0f).ShortGameplayFormat()}({RealTimeInfo.Value.FrameCount})");
            }

            if (driver.Running && process.Frame.Scenes.CurrentScene is Level level &&
                ((IOriginalAreaKeyEnvironment)process).AreaKeys.GetSID(
                    ((IOriginalAreaKeyEnvironment)process).AreaKeys.GetSessionArea(level.Session)) == LoadedLobbySID)
            {
                string res = driver.CollabUtils2.Lobby.TryGetActiveWarps(level, out string[]? warps)
                    ? '[' + string.Join(", ", warps) + ']'
                    : "[]";
                UpdateAllMetadata("ActivatedLobbyWarps", _ => res);
            }

            TasStartInfo = null;
            RealTimeInfo = null;
            LoadedLobbySID = null;
        }

        internal void SaveStateSourceBody(Dictionary<string, object?> data)
        {
            data[nameof(TasStartInfo)] = TasStartInfo;
            data[nameof(RealTimeInfo)] = RealTimeInfo;
            data[nameof(LoadedLobbySID)] = LoadedLobbySID;
        }

        internal void LoadStateSourceBody(Dictionary<string, object?> data)
        {
            TasStartInfo = ((long FileTimeTicks, int FileSlot)?)data[nameof(TasStartInfo)];
            RealTimeInfo = ((int FrameCount, int FileSlot)?)data[nameof(RealTimeInfo)];
            LoadedLobbySID = (string?)data[nameof(LoadedLobbySID)];
        }

        internal void AdvanceRealTimeSourceBody()
        {
            if (!driver.Running || CurrentSave is not { } saveData)
            {
                return;
            }

            if (RealTimeInfo != null && !driver.IsActuallyLoading())
            {
                RealTimeInfo = RealTimeInfo!.Value with { FrameCount = RealTimeInfo!.Value.FrameCount + 1 };
            }

            if (RealTimeInfo == null || RealTimeInfo.Value.FileSlot != saveData.FileSlot)
            {
                RealTimeInfo = (0, saveData.FileSlot);
            }
        }

        private void UpdateChapterTime(Level level)
        {
            if (!driver.Running || !level.Session.StartedFromBeginning)
            {
                return;
            }

            UpdateAllMetadata("ChapterTime", _ => driver.GameInfo.GetChapterTime(level));
        }

        public void UpdateRecordCount(SourceTasInputController inputController)
        {
            UpdateAllMetadata(
                "RecordCount",
                command => (int.Parse(command.Args.FirstOrDefault() ?? "0") + 1).ToString(),
                command => int.TryParse(command.Args.FirstOrDefault() ?? "0", out int _));
        }

        [SourceTasCommand("RecordCount", Aliases = ["RecordCount:", "RecordCount："], CalcChecksum = false,
            MetaDataProvider = typeof(SourceTasRecordCountMeta))]
        private static void RecordCountCommand(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            // dummy
        }

        [SourceTasCommand("FileTime", Aliases = ["FileTime:", "FileTime："], CalcChecksum = false)]
        private static void FileTimeCommand(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            // dummy
        }

        [SourceTasCommand("ChapterTime", Aliases = ["ChapterTime:", "ChapterTime："], CalcChecksum = false)]
        private static void ChapterTimeCommand(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            // dummy
        }

        [SourceTasCommand("RealTime", Aliases = ["RealTime:", "RealTime："], CalcChecksum = false)]
        private static void RealTimeCommand(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            // dummy
        }

        [SourceTasCommand("MidwayFileTime", Aliases = ["MidwayFileTime:", "MidwayFileTime："], CalcChecksum = false)]
        private static void MidwayFileTimeCommand(CommandLine commandLine, int studioLine, string filePath,
            int fileLine)
        {
            if (Current.TasStartInfo == null || Current.CurrentSave == null)
            {
                return;
            }

            Current.UpdateAllMetadata("MidwayFileTime",
                _ => Current.driver.GameInfo.FormatTime(Current.CurrentSave.Time -
                                                        Current.TasStartInfo.Value.FileTimeTicks),
                command => Current.driver.Controller.CurrentCommands.Contains(command));
        }

        [SourceTasCommand("MidwayChapterTime", Aliases = ["MidwayChapterTime:", "MidwayChapterTime："],
            CalcChecksum = false)]
        private static void MidwayChapterTimeCommand(CommandLine commandLine, int studioLine, string filePath,
            int fileLine)
        {
            if (!Current.driver.Running || Current.process.Frame.Scenes.CurrentScene is not Level level)
            {
                return;
            }

            Current.UpdateAllMetadata("MidwayChapterTime",
                _ => Current.driver.GameInfo.GetChapterTime(level),
                command => Current.driver.Controller.CurrentCommands.Contains(command));
        }

        [SourceTasCommand("MidwayRealTime", Aliases = ["MidwayRealTime:", "MidwayRealTime："], CalcChecksum = false)]
        private static void MidwayRealTimeCommand(CommandLine commandLine, int studioLine, string filePath,
            int fileLine)
        {
            if (Current.RealTimeInfo == null)
            {
                return;
            }

            Current.UpdateAllMetadata("MidwayRealTime",
                _ =>
                    $"{TimeSpan.FromSeconds(Current.RealTimeInfo.Value.FrameCount / 60.0f).ShortGameplayFormat()}({Current.RealTimeInfo.Value.FrameCount})",
                command => Current.driver.Controller.CurrentCommands.Contains(command));
        }

        [SourceTasCommand("ActivatedLobbyWarps", Aliases = ["ActivatedLobbyWarps:", "ActivatedLobbyWarps："],
            CalcChecksum = false)]
        private static void ActiveWarpsCommand(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            // dummy
        }

        private void UpdateAllMetadata(string commandName, Func<SourceTasCommand, string> getMetadata,
            Func<SourceTasCommand, bool>? predicate = null)
        {
            string tasFilePath = driver.Controller.FilePath;
            var metadataCommands = driver.Controller.Commands.SelectMany(pair => pair.Value)
                .Where(command => command.Is(commandName) && command.FilePath == driver.Controller.FilePath)
                .Where(predicate ?? (_ => true))
                .ToList();

            var updateLines = metadataCommands
                .Where(command =>
                {
                    string metadata = getMetadata(command);
                    if (metadata.IsNullOrEmpty())
                    {
                        return false;
                    }

                    if (command.Args.Length > 0 && command.Args[0] == metadata)
                    {
                        return false;
                    }

                    // Sync-check reporting
                    if (command.Is("FileTime") || command.Is("ChapterTime") || command.Is("MidwayFileTime") ||
                        command.Is("MidwayChapterTime"))
                    {
                        driver.SourceSyncChecker.ReportWrongTime(command.FilePath, command.FileLine,
                            command.Args.Length > 0 ? command.Args[0] : string.Empty, metadata);
                    }

                    return true;
                })
                .ToDictionary(command => command.StudioLine,
                    command => $"{command.Attribute.Name}: {getMetadata(command)}");

            if (updateLines.IsEmpty())
            {
                return;
            }

            string[] allLines = File.ReadAllLines(tasFilePath);
            int allLinesLength = allLines.Length;
            foreach ((int lineNumber, string replacement) in updateLines)
            {
                if (lineNumber >= 0 && lineNumber < allLinesLength)
                {
                    allLines[lineNumber] = replacement;
                }
            }

            // Prevent a reload from being triggered by the file-system change
            bool needsReload = driver.Controller.NeedsReload;
            try
            {
                File.WriteAllText(tasFilePath, allLines.FormatTasLinesToText());
            }
            catch (IOException)
            {
                // Something is blocking the TAS file. Just ignore it, the change should be reflected in Studio either way.
            }

            driver.Controller.NeedsReload = needsReload;

            driver.SendSourceUpdateLines(updateLines);
        }
    }
}
