using System;

namespace Celeste.Mods.Tas.Information
{
    internal static class SourceTasNumberExtensions
    {
        public static long SecondsToTicks(this float seconds)
        {
            double millis = seconds * 1000 + (seconds >= 0 ? +0.5 : -0.5);
            return (long)millis * TimeSpan.TicksPerMillisecond;
        }
    }
}
