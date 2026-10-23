using System;
using Celeste.Simulation.Presentation.Hud.Speedrun;

namespace Celeste.Mods.Tas.Information
{
    internal static class SourceTasTimeSpanExtensions
    {
        public static string ShortGameplayFormat(this TimeSpan time)
            => SpeedrunTimerState.ShortGameplayFormat(time.Ticks);
    }
}
