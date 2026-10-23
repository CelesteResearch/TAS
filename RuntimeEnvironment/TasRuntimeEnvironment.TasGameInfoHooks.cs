using System;
using Celeste.Core.Scheduling.Hooks;
using Celeste.Mods.Tas;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : ISourceEngineUpdateInterceptionEnvironment
    {
        private Action<Action> sourceTasGameInfoEngineUpdate;

        internal void InstallSourceTasGameInfoEngineUpdate(TasPlaybackDriver driver)
            => sourceTasGameInfoEngineUpdate = driver.GameInfo.AroundSourceEngineUpdate;

        internal void UninstallSourceTasGameInfoEngineUpdate() => sourceTasGameInfoEngineUpdate = null;

        void ISourceEngineUpdateInterceptionEnvironment.InvokeSourceEngineUpdate(Action original)
        {
            var hook = sourceTasGameInfoEngineUpdate;
            if (hook != null) hook(original);
            else original();
        }
    }
}
