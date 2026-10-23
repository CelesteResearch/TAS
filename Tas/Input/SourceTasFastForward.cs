using StudioCommunication;
using System.Globalization;

namespace Celeste.Mods.Tas.Input
{
    /// A breakpoint to which the TAS will fast-forward at a high speed
    internal record SourceTasFastForward(
        int Frame,
        int StudioLine,
        string FilePath,
        int FileLine,
        bool ForceStop = false,
        bool SaveState = false,
        float Speed = SourceTasFastForward.DefaultSpeed)
    {
        internal const float DefaultSpeed = 400.0f;

        public SourceTasFastForward(int frame, int studioLine, string filePath, int fileLine,
            FastForwardLine fastForwardLine)
            : this(frame, studioLine, filePath, fileLine, fastForwardLine.ForceStop, fastForwardLine.SaveState,
                fastForwardLine.PlaybackSpeed ?? DefaultSpeed)
        {
        }

        public string Format()
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            return
                $"***{(ForceStop ? "!" : "")}{(SaveState ? "S" : "")}{(Speed == DefaultSpeed ? "" : Speed.ToString(CultureInfo.InvariantCulture))}";
        }

        public override string ToString()
        {
            return $"***{(ForceStop ? "!" : "")}{(SaveState ? "S" : "")}{Speed}";
        }
    }
}
