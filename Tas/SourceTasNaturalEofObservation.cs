using System;
using Celeste.Core.Diagnostics.Tas;

namespace Celeste.Mods.Tas
{
    public readonly struct SourceTasNaturalEofObservation
    {
        public readonly int CurrentFrame;
        public readonly int ParsedFrameCount;
        public readonly string FilePath;
        public readonly bool ParseAborted;

        internal SourceTasNaturalEofObservation(int currentFrame, int parsedFrameCount,
            string filePath, bool parseAborted)
        {
            CurrentFrame = currentFrame;
            ParsedFrameCount = parsedFrameCount;
            FilePath = filePath;
            ParseAborted = parseAborted;
        }
    }
}
