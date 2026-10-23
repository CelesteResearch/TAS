using System.Collections.Generic;
using Celeste.Everest.Modules.Dependencies;
using Celeste.Everest.Modules.Updating;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasGameData
    {
        [SourceTasLoad]
        private static void Load()
        {
            var driver = CelesteTasModule.Instance.Driver;
            ((IEverestModUpdaterEnvironment)driver.Process).ModUpdater.OnDownloadModUpdateListCompleted +=
                driver.GameData.AfterDownloadModUpdateList;
        }

        [SourceTasUnload]
        private static void Unload()
        {
            var driver = CelesteTasModule.Instance.Driver;
            ((IEverestModUpdaterEnvironment)driver.Process).ModUpdater.OnDownloadModUpdateListCompleted -=
                driver.GameData.AfterDownloadModUpdateList;
        }

        private void AfterDownloadModUpdateList(Dictionary<string, EverestModUpdateInfo> actualResult)
        {
            TasRuntimeEnvironment.Get(process).SourceTasModUpdateInfos = actualResult;
        }
    }
}
