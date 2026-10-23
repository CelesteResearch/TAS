using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        [SourceTasLoad]
        private static void LoadSourceConsoleCallbacks()
        {
            var module = CelesteTasModule.Instance;
            TasRuntimeEnvironment.Get(module.Process).InstallSourceTasConsoleCallbacks(module.Driver);
            TasRuntimeEnvironment.Get(module.Process).InstallSourceTasTheoPlateauHook(module.Driver);
            module.Process.LevelSourceHooks.AfterEnd += module.Driver.AfterSourceConsoleLevelEnd;
        }

        [SourceTasUnload]
        private static void UnloadSourceConsoleCallbacks()
        {
            var module = CelesteTasModule.Instance;
            TasRuntimeEnvironment.Get(module.Process).UninstallSourceTasConsoleCallbacks(module.Driver);
            TasRuntimeEnvironment.Get(module.Process).UninstallSourceTasTheoPlateauHook();
            module.Process.LevelSourceHooks.AfterEnd -= module.Driver.AfterSourceConsoleLevelEnd;
        }

        private void AfterSourceConsoleLevelEnd(global::Celeste.Game.Execution.Levels.OriginalLevelSceneRoot level) =>
            ConsoleProcess.AfterLevelEnd();
    }
}
