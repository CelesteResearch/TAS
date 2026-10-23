using Celeste.Input.Devices.Keyboard;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using Celeste.Mods.Tas.Commands;
using StudioCommunication.Util;
using Keys = Celeste.Input.Devices.Keyboard.SourceKeys;
using StudioCommunication;


namespace Celeste.Mods.Tas.Input
{
    /// Represents a fully parsed input-line in a TAS file
    public partial record SourceTasInputFrame : Celeste.Mods.Tas.Input.Feeding.ISourceTasFeedInputFrame
    {
        // Controller state
        public readonly Actions Actions;
        public readonly Vector2 StickPosition;
        public readonly Vector2 DashOnlyStickPosition;
        public readonly Vector2 MoveOnlyStickPosition;
        public readonly HashSet<Keys> PressedKeys = [];

        long Celeste.Mods.Tas.Input.Feeding.ISourceTasFeedInputFrame.SourceActions => (long)Actions;
        Vector2 Celeste.Mods.Tas.Input.Feeding.ISourceTasFeedInputFrame.FeatherStick => StickPosition;
        Vector2 Celeste.Mods.Tas.Input.Feeding.ISourceTasFeedInputFrame.DashOnlyStick => DashOnlyStickPosition;
        IEnumerable<Keys> Celeste.Mods.Tas.Input.Feeding.ISourceTasFeedInputFrame.PressedKeys => PressedKeys;

        // libTAS state
        public readonly SourceTasVector2Short StickPositionShort;
        public readonly SourceTasVector2Short DashOnlyStickPositionShort;
        public readonly SourceTasVector2Short MoveOnlyStickPositionShort;

        // Metadata
        public readonly string FilePath;
        public readonly int FileLine;

        public int Frames;
        public int StudioLine;
        public int FrameOffset;

        public int RepeatCount;
        public int RepeatIndex;
        public string RepeatString => RepeatCount > 1 ? $" {RepeatIndex}/{RepeatCount}" : "";

        /// Command which generated this input
        internal SourceTasCommand? ParentCommand;

        public SourceTasInputFrame? Previous;
        public SourceTasInputFrame? Next;

        private readonly string actionLineString;
        private readonly int checksum;

        private SourceTasInputFrame(SourceTasAnalogProcess analog, ActionLine actionLine, string filePath, int fileLine,
            int studioLine, int repeatIndex, int repeatCount, int frameOffset, SourceTasCommand? parentCommand)
            : this(analog, actionLine, filePath, fileLine, studioLine, repeatIndex, repeatCount, frameOffset,
                parentCommand, null)
        {
        }

        private SourceTasInputFrame(SourceTasAnalogProcess analog, ActionLine actionLine, string filePath, int fileLine,
            int studioLine, int repeatIndex, int repeatCount, int frameOffset, SourceTasCommand? parentCommand,
            Vector2? gameplayStick)
        {
            Actions = actionLine.Actions;
            Frames = actionLine.FrameCount;
            PressedKeys = actionLine.CustomBindings.Select(c => (Keys)c).ToHashSet();

            FilePath = filePath;
            FileLine = fileLine;

            StudioLine = studioLine;
            FrameOffset = frameOffset;

            RepeatIndex = repeatIndex;
            RepeatCount = repeatCount;

            ParentCommand = parentCommand;

            actionLineString = actionLine.ToString();
            checksum = actionLineString.GetHashCode();

            if (Actions.Has(Actions.Feather))
            {
                if (gameplayStick is { } stick)
                {
                    StickPosition = stick;
                    StickPositionShort = new SourceTasVector2Short((short)(stick.X * 32767), (short)(stick.Y * 32767));
                }
                else if (float.TryParse(actionLine.FeatherAngle, out float angle))
                {
                    float magnitude = float.TryParse(actionLine.FeatherMagnitude, out float m) ? m : 1.0f;
                    (StickPosition, StickPositionShort) = analog.ComputeAngleVector(angle, magnitude);
                }
                else
                {
                    // Fallback to 0°
                    (StickPosition, StickPositionShort) = analog.ComputeAngleVector(0.0f, 1.0f);
                }
            }

            if (Actions.Has(Actions.LeftDashOnly))
            {
                DashOnlyStickPosition.X = -1.0f;
            }
            else if (Actions.Has(Actions.RightDashOnly))
            {
                DashOnlyStickPosition.X = 1.0f;
            }

            if (Actions.Has(Actions.DownDashOnly))
            {
                DashOnlyStickPosition.Y = -1.0f;
            }
            else if (Actions.Has(Actions.UpDashOnly))
            {
                DashOnlyStickPosition.Y = 1.0f;
            }

            DashOnlyStickPositionShort = new SourceTasVector2Short((short)(DashOnlyStickPosition.X * 32767),
                (short)(DashOnlyStickPosition.Y * 32767));

            if (Actions.Has(Actions.LeftMoveOnly))
            {
                MoveOnlyStickPosition.X = -1.0f;
            }
            else if (Actions.Has(Actions.RightMoveOnly))
            {
                MoveOnlyStickPosition.X = 1.0f;
            }

            if (Actions.Has(Actions.DownMoveOnly))
            {
                MoveOnlyStickPosition.Y = -1.0f;
            }
            else if (Actions.Has(Actions.UpMoveOnly))
            {
                MoveOnlyStickPosition.Y = 1.0f;
            }

            MoveOnlyStickPositionShort = new SourceTasVector2Short((short)(MoveOnlyStickPosition.X * 32767),
                (short)(MoveOnlyStickPosition.Y * 32767));
        }

        public static bool TryParse(SourceTasAnalogProcess analog, string lineText, string filePath, int fileLine,
            int studioLine, SourceTasInputFrame? prevSourceTasInputFrame,
            [NotNullWhen(true)] out SourceTasInputFrame? inputFrame, int repeatIndex = 0, int repeatCount = 0,
            int frameOffset = 0, SourceTasCommand? parentCommand = null)
        {
            inputFrame = null;
            if (!ActionLine.TryParse(lineText, out var actionLine))
            {
                return false;
            }

            inputFrame = new SourceTasInputFrame(analog, actionLine, filePath, fileLine, studioLine, repeatIndex,
                repeatCount, frameOffset, parentCommand);

            inputFrame.Previous = prevSourceTasInputFrame;
            if (prevSourceTasInputFrame != null)
            {
                prevSourceTasInputFrame.Next = inputFrame;
            }

            return true;
        }

        public override string ToString() => actionLineString;
        public override int GetHashCode() => checksum;
    }
}
