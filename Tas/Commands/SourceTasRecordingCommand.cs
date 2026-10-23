using System.Collections.Generic;
using System.IO;
using System.Linq;
using StudioCommunication;
using Celeste.Mods.Tas.Module;
using Celeste.Core.Diagnostics.Tas;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed class SourceTasRecordingCommand
    {
        private readonly TasPlaybackDriver driver;
        internal SourceTasRecordingCommand(TasPlaybackDriver driver) => this.driver = driver;

        internal record RecordingTime
        {
            public int StartFrame = int.MaxValue;
            public int StopFrame = int.MaxValue;

            public int Duration => StopFrame - StartFrame;
        }

        internal readonly Dictionary<int, RecordingTime> RecordingTimes = new();

        /// "StartRecording, (File Path)"
        [SourceTasCommand("StartRecording",
            ExecuteTiming = SourceTasExecuteTiming.Parse | SourceTasExecuteTiming.Runtime)]
        internal void StartRecording(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            if (driver.CommandRegistry.Parsing)
            {
                if (driver.SourceStudioConnected && driver.Running)
                {
                    if (!driver.Recorder.Installed)
                    {
                        driver.SendSourceRecordingFailed(RecordingFailedReason.TASRecorderNotInstalled);
                    }
                    else if (!driver.Recorder.IsFFmpegInstalled)
                    {
                        driver.SendSourceRecordingFailed(RecordingFailedReason.FFmpegNotInstalled);
                    }
                }

                if (!driver.Recorder.Installed)
                {
                    driver.AbortSourceTas("TAS Recorder isn't installed");
                    return;
                }

                if (!driver.Recorder.IsFFmpegInstalled)
                {
                    driver.AbortSourceTas("FFmpeg isn't properly installed");
                    return;
                }

                if (RecordingTimes.Count > 0)
                {
                    RecordingTime last = RecordingTimes.Last().Value;
                    if (last.StopFrame == int.MaxValue)
                    {
                        string errorText = $"{Path.GetFileName(filePath)} line {fileLine}\n";
                        driver.AbortSourceTas($"{errorText}StopRecording is required before another StartRecording");
                        return;
                    }
                }

                var time = new RecordingTime { StartFrame = driver.Controller.Inputs.Count };
                RecordingTimes[time.StartFrame] = time;
            }
            else
            {
                if (driver.Recorder.IsRecording)
                {
                    driver.AbortSourceTas("Tried to start recording, while already recording");
                    return;
                }

                string? fileName = commandLine.Arguments.Length > 1 ? commandLine.Arguments[0] : null;
                driver.Recorder.StartRecording(fileName);
                if (RecordingTimes.TryGetValue(driver.Controller.CurrentFrameInTas, out var time) &&
                    time.StartFrame != int.MaxValue && time.StopFrame != int.MaxValue)
                {
                    driver.Recorder.SetDurationEstimate(time.Duration);
                }

                driver.CurrState = driver.NextState = SourceTasManagerState.Running;
                driver.PlaybackSpeed = 1.0f;
            }
        }

        /// "StopRecording"
        [SourceTasCommand("StopRecording",
            ExecuteTiming = SourceTasExecuteTiming.Parse | SourceTasExecuteTiming.Runtime)]
        internal void StopRecording(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            if (driver.CommandRegistry.Parsing)
            {
                string errorText = $"{Path.GetFileName(filePath)} line {fileLine}\n";
                if (RecordingTimes.Count == 0)
                {
                    driver.AbortSourceTas($"{errorText}StartRecording is required before StopRecording");
                    return;
                }

                var last = RecordingTimes.Last().Value;
                if (last.StopFrame != int.MaxValue)
                {
                    if (last.StopFrame == int.MaxValue)
                    {
                        driver.AbortSourceTas($"{errorText}StartRecording is required before another StopRecording");
                        return;
                    }
                }

                last.StopFrame = driver.Controller.Inputs.Count;
            }
            else
            {
                driver.Recorder.StopRecording();
            }
        }

        [SourceTasParseFileEnd]
        internal void ParseFileEnd()
        {
            if (RecordingTimes.Count == 0)
            {
                return;
            }

            var last = RecordingTimes.Last().Value;
            if (last.StopFrame == int.MaxValue)
            {
                last.StopFrame = driver.Controller.Inputs.Count;
            }
        }

        [SourceTasClearInputs]
        internal void Clear()
        {
            RecordingTimes.Clear();
        }

        [SourceTasDisableRun]
        internal void DisableRun()
        {
            if (driver.Recorder.IsRecording)
            {
                driver.Recorder.StopRecording();
            }
        }
    }
}
