using Celeste.Game.Execution.Levels;
using Celeste.Core.Scheduling.Hooks;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasGameInfo
    {
        [SourceTasLoad]
        private static void Load()
        {
            var driver = CelesteTasModule.Instance.Driver;
            TasRuntimeEnvironment.Get(driver.Process).InstallSourceTasGameInfoEngineUpdate(driver);
            ((ISourceUpdateMethodHooksEnvironment)driver.Process).SourceSceneUpdateHooks.AfterUpdate +=
                driver.GameInfo.AfterSourceSceneAfterUpdate;
            driver.Process.LevelUpdateEvents.OnTransitionTo += driver.GameInfo.OnSourceLevelTransition;
            driver.Process.LevelSourceHooks.AfterUpdate += driver.GameInfo.AfterLevelUpdate;
        }

        [SourceTasUnload]
        private static void Unload()
        {
            var driver = CelesteTasModule.Instance.Driver;
            TasRuntimeEnvironment.Get(driver.Process).UninstallSourceTasGameInfoEngineUpdate();
            ((ISourceUpdateMethodHooksEnvironment)driver.Process).SourceSceneUpdateHooks.AfterUpdate -=
                driver.GameInfo.AfterSourceSceneAfterUpdate;
            driver.Process.LevelUpdateEvents.OnTransitionTo -= driver.GameInfo.OnSourceLevelTransition;
            driver.Process.LevelSourceHooks.AfterUpdate -= driver.GameInfo.AfterLevelUpdate;
        }

        private void AfterLevelUpdate(OriginalLevelSceneRoot level) => AfterSourceLevelUpdate();
    }
}
