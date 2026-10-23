using Celeste.Game.Execution.Levels;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasAreaCompleteInfo
    {
        [SourceTasLoad]
        private static void Load()
        {
            var driver = CelesteTasModule.Instance.Driver;
            driver.Process.LevelSourceHooks.AfterUpdate += driver.AreaCompleteInfo.AfterLevelUpdate;
            TasRuntimeEnvironment.Get(driver.Process).InstallSourceTasAreaCompleteInfoHooks(driver.AreaCompleteInfo);
        }

        [SourceTasUnload]
        private static void Unload()
        {
            var driver = CelesteTasModule.Instance.Driver;
            driver.Process.LevelSourceHooks.AfterUpdate -= driver.AreaCompleteInfo.AfterLevelUpdate;
            TasRuntimeEnvironment.Get(driver.Process).RemoveSourceTasAreaCompleteInfoHooks();
        }

        private void AfterLevelUpdate(OriginalLevelSceneRoot level)
        {
            if (driver.Running)
                level.Session.SetFlag(TasWasRun);
        }
    }
}
