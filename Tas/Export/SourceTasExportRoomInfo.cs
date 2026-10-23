using Celeste.Mods.Tas.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Core.Scheduling;
using Celeste.Game.Execution.Processes;
using Celeste.Game.Save.Data;
using Celeste.Game.Execution.Areas;
using Celeste.Game.Content.Dialogs;
using Celeste.Simulation.Runtime.Session;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Information;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;

namespace Celeste.Mods.Tas.Export
{
// ReSharper disable once UnusedType.Global
    internal sealed class SourceTasExportRoomInfo
    {
        private readonly TasPlaybackDriver driver;
        private GameApplication Process => driver.Process;
        private OriginalSaveDataState CurrentSave => ((IOriginalGameplayProcess)Process).SaveContext.CurrentSave;
        internal SourceTasExportRoomInfo(TasPlaybackDriver driver) => this.driver = driver;

        private class Meta : ISourceTasCommandMeta
        {
            public string Insert => $"ExportRoomInfo{CommandInfo.Separator}[0;dump_room_info.txt]";
            public bool HasArguments => true;
        }

        private StreamWriter? streamWriter;
        private bool exporting;
        private string? lastRoomName;
        private readonly List<SourceTasExportRoomInfoRecord> roomInfos = new();

        [SourceTasLoad]
        private static void Load()
        {
            var driver = CelesteTasModule.Instance.Driver;
            driver.Process.SourceSceneUpdateHooks.AfterUpdate += driver.SourceExportRoomInfo.AfterSceneAfterUpdate;
        }

        [SourceTasUnload]
        private static void Unload()
        {
            var driver = CelesteTasModule.Instance.Driver;
            driver.Process.SourceSceneUpdateHooks.AfterUpdate -= driver.SourceExportRoomInfo.AfterSceneAfterUpdate;
        }

        private void AfterSceneAfterUpdate(ISourceSceneExecution self)
        {
            if (!exporting)
            {
                return;
            }

            ChapterCompletionSession?
                session = SourceTasSceneSessionQuery.GetSession(Process.Frame.Scenes.CurrentScene);
            string? roomName = session?.Level;

            if (Process.Frame.Scenes.CurrentScene is Level { Completed: true } level)
            {
                if (roomInfos.LastOrDefault() is { RoomName: { } } lastSourceTasExportRoomInfoRecord)
                {
                    lastSourceTasExportRoomInfoRecord.LeaveRoomChapterTime ??= level.Session.Time;
                    if (level.TimerStopped)
                    {
                        lastSourceTasExportRoomInfoRecord.LeaveRoomFileTime ??= CurrentSave?.Time;
                    }
                }
            }

            if (lastRoomName != roomName)
            {
                if (roomName.IsNotNullOrEmpty() && session != null)
                {
                    if (roomInfos.LastOrDefault() is { } lastSourceTasExportRoomInfoRecord &&
                        lastSourceTasExportRoomInfoRecord.AreaKey == Process.AreaKeys.GetSessionArea(session))
                    {
                        lastSourceTasExportRoomInfoRecord.LeaveRoomChapterTime ??= session.Time;
                        lastSourceTasExportRoomInfoRecord.LeaveRoomFileTime ??= CurrentSave?.Time;
                    }

                    roomInfos.Add(new SourceTasExportRoomInfoRecord(Process)
                    {
                        AreaKey = Process.AreaKeys.GetSessionArea(session),
                        RoomName = roomName,
                        EnterRoomChapterTime = session.Time,
                        EnterRoomFileTime = CurrentSave?.Time
                    });
                }
                else
                {
                    if (roomInfos.LastOrDefault() is { } lastSourceTasExportRoomInfoRecord)
                    {
                        lastSourceTasExportRoomInfoRecord.LeaveRoomChapterTime ??=
                            CurrentSave?.CurrentSessionSafe?.Time;
                        lastSourceTasExportRoomInfoRecord.LeaveRoomFileTime ??= CurrentSave?.Time;
                    }

                    roomInfos.Add(new SourceTasExportRoomInfoRecord(Process));
                }
            }

            lastRoomName = roomName;
        }

        // "ExportRoomInfo [Path = dump_room_info.txt]"
        [SourceTasCommand("ExportRoomInfo", Aliases = ["StartExportRoomInfo"], CalcChecksum = false,
            MetaDataProvider = typeof(Meta))]
        private void StartExportCommand(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            string[] args = commandLine.Arguments;
            string path = args.Length > 0 ? args[0] : "dump_room_info.txt";
            BeginExport(path);
        }

        [SourceTasCommand("EndExportRoomInfo", Aliases = ["FinishExportRoomInfo"], CalcChecksum = false)]
        private void FinishExportCommand(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            EndExport();
        }

        private void BeginExport(string path)
        {
            exporting = true;
            roomInfos.Clear();
            streamWriter?.Dispose();
            if (Path.GetDirectoryName(path) is { } dir && dir.IsNotEmpty())
            {
                Directory.CreateDirectory(dir);
            }

            streamWriter = new StreamWriter(path);
            streamWriter.WriteLine($"TAS File: {driver.Controller.FilePath}");
            streamWriter.WriteLine(SourceTasExportRoomInfoRecord.GetTableHead());
        }

        [SourceTasDisableRun]
        private static void SourceEndExport() => CelesteTasModule.Instance.Driver.SourceExportRoomInfo.EndExport();

        private void EndExport()
        {
            ExportInfo();
            exporting = false;
            roomInfos.Clear();
            streamWriter?.Dispose();
        }

        private void ExportInfo()
        {
            if (!exporting)
            {
                return;
            }

            if (roomInfos.FirstOrDefault() is { AreaKey: null })
            {
                roomInfos.RemoveAt(0);
            }

            if (roomInfos.LastOrDefault() is { AreaKey: null })
            {
                roomInfos.RemoveAt(roomInfos.Count - 1);
            }

            if (roomInfos.LastOrDefault() is { } lastSourceTasExportRoomInfoRecord)
            {
                lastSourceTasExportRoomInfoRecord.LeaveRoomChapterTime ??= CurrentSave?.CurrentSessionSafe?.Time;
                lastSourceTasExportRoomInfoRecord.LeaveRoomFileTime ??= CurrentSave?.Time;
            }

            foreach (SourceTasExportRoomInfoRecord roomInfo in roomInfos)
            {
                streamWriter!.WriteLine(roomInfo);
            }
        }
    }
}
