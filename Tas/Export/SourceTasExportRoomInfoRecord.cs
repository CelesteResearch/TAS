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
    internal sealed record SourceTasExportRoomInfoRecord
    {
        private readonly GameApplication Process;
        internal SourceTasExportRoomInfoRecord(GameApplication process) => Process = process;
        public OriginalAreaKey? AreaKey;
        public long? EnterRoomChapterTime;
        public long? EnterRoomFileTime;
        public long? LeaveRoomChapterTime;
        public long? LeaveRoomFileTime;
        public string? RoomName;

        private OriginalGameTextProcess Text => ((IOriginalGameTextEnvironment)Process).Text;

        private string ChapterName
        {
            get
            {
                if (AreaKey == null)
                {
                    return string.Empty;
                }

                IOriginalAreaData areaData = Process.AreaKeys.GetArea(AreaKey.Value);
                string chapterName = Text.Dialog.Get(areaData.Name, Text.Dialog.Languages["english"]);

                if (areaData.Interlude || chapterName == "Farewell")
                {
                    return chapterName;
                }

                OriginalAreaMode areaMode = (OriginalAreaMode)AreaKey.Value.Mode;
                string side = areaMode switch
                {
                    OriginalAreaMode.Normal => "A Side",
                    OriginalAreaMode.BSide => "B Side",
                    OriginalAreaMode.CSide => "C Side",
                    _ => $"{(char)(areaMode + 'A')} Side"
                };

                return $"{chapterName} {side}";
            }
        }

        public override string ToString()
        {
            if (AreaKey == null)
            {
                return string.Empty;
            }

            List<string?> values =
            [
                ChapterName,
                RoomName,
            ];

            if (EnterRoomChapterTime != null && LeaveRoomChapterTime != null)
            {
                values.Add(FormatTime(LeaveRoomChapterTime - EnterRoomChapterTime));
                values.Add(ConvertToFrames(LeaveRoomChapterTime - EnterRoomChapterTime));
            }
            else
            {
                values.Add(string.Empty);
                values.Add(string.Empty);
            }

            if (EnterRoomFileTime != null && LeaveRoomFileTime != null)
            {
                values.Add(FormatTime(LeaveRoomFileTime - EnterRoomFileTime));
                values.Add(ConvertToFrames(LeaveRoomFileTime - EnterRoomFileTime));
            }
            else
            {
                values.Add(string.Empty);
                values.Add(string.Empty);
            }

            values.Add(FormatTimeWithFrames(LeaveRoomChapterTime));
            values.Add(FormatTimeWithFrames(LeaveRoomFileTime));

            return string.Join("\t", values);
        }

        public static string GetTableHead()
        {
            return string.Join("\t",
                "Chapter",
                "Room",
                "Elapsed Chapter Time",
                "Elapsed Chapter Time (Frame)",
                "Elapsed File Time",
                "Elapsed File Time (Frame)",
                "Chapter Time",
                "File Time"
            );
        }

        private static string FormatTime(long? time)
        {
            if (time == null)
            {
                return string.Empty;
            }

            TimeSpan timeSpan = TimeSpan.FromTicks(time.Value);
            if (timeSpan.TotalHours >= 1)
            {
                return (int)timeSpan.TotalHours + ":" + timeSpan.ToString("mm\\:ss\\.fff");
            }

            if (timeSpan.TotalMinutes >= 1)
            {
                return (int)timeSpan.TotalMinutes + ":" + timeSpan.ToString("ss\\.fff");
            }

            return timeSpan.ToString("s\\.fff");
        }

        private static long SecondsToTicks(float seconds)
        {
            // Original TAS helper reproduces .NET Framework millisecond rounding.
            double millis = seconds * 1000 + (seconds >= 0 ? +0.5 : -0.5);
            return (long)millis * TimeSpan.TicksPerMillisecond;
        }

        private string ConvertToFrames(long? time)
        {
            return time == null ? string.Empty : (time.Value / SecondsToTicks(Process.RawDeltaTime)).ToString();
        }

        private string FormatTimeWithFrames(long? time)
        {
            return time == null ? string.Empty : $"{FormatTime(time)} ({ConvertToFrames(time)})";
        }
    }
}
