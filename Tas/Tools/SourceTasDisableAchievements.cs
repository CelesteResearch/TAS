using Celeste.Core.Diagnostics.Tas;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Tools
{
    internal static class SourceTasDisableAchievements
    {
        [SourceTasLoad]
        private static void Load()
        {
            var process = CelesteTasModule.Instance.Process;
            var methods = (ISourceTasAchievementSourceEnvironment)TasRuntimeEnvironment.Get(process);
            // Original non-Steam Increment can be a single ret. Preserve this platform gate.
            if (methods.SourceStatsIncrementInstructionCount > 1)
                TasRuntimeEnvironment.Get(process).InstallSourceTasStatsIncrementHook();
            if (methods.SourceAchievementsRegisterInstructionCount > 4)
                TasRuntimeEnvironment.Get(process).InstallSourceTasAchievementRegisterHook();
        }

        [SourceTasUnload]
        private static void Unload()
        {
            var process = CelesteTasModule.Instance.Process;
            TasRuntimeEnvironment.Get(process).UninstallSourceTasAchievementRegisterHook();
            TasRuntimeEnvironment.Get(process).UninstallSourceTasStatsIncrementHook();
        }
    }
}
