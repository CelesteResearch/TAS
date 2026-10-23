using Celeste.Mods.Tas.Interop;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasRecorderInterop recorder;
        internal SourceTasRecorderInterop Recorder => recorder ??= new SourceTasRecorderInterop(process, this);
        private bool IsSourceRecording() => Recorder.IsRecording;
        private Commands.SourceTasRecordingCommand recordingCommand;

        internal Commands.SourceTasRecordingCommand RecordingCommand =>
            recordingCommand ??= new Commands.SourceTasRecordingCommand(this);

        internal System.Collections.Generic.Dictionary<int, Commands.SourceTasRecordingCommand.RecordingTime>
            RecordingTimes => RecordingCommand.RecordingTimes;
    }
}
