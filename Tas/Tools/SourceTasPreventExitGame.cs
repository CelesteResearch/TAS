using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Tools
{
    internal static class SourceTasPreventExitGame
    {
        [SourceTasLoad]
        private static void Load() => TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process)
            .InstallSourceTasPreventExitHook();

        [SourceTasUnload]
        private static void Unload() => TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process)
            .UninstallSourceTasPreventExitHook();
    }
}
