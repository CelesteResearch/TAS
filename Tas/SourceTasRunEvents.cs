using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    internal sealed class SourceTasRunEvents
    {
        private readonly TasPlaybackDriver driver;

        public SourceTasRunEvents(TasPlaybackDriver driver, GameApplication process)
        {
            this.driver = driver;
        }

        internal void Initialize()
        {
            driver.ModuleAttributes.CollectAllMethods<SourceTasEnableRunAttribute>();
            driver.ModuleAttributes.CollectAllMethods<SourceTasDisableRunAttribute>();
            driver.ModuleAttributes.CollectAllMethods<SourceTasUpdateMetaAttribute>();
        }

        internal void InvokeEnableRun() => driver.ModuleAttributes.Invoke<SourceTasEnableRunAttribute>();
        internal void InvokeDisableRun() => driver.ModuleAttributes.Invoke<SourceTasDisableRunAttribute>();
        internal void InvokeUpdateMeta() => driver.ModuleAttributes.Invoke<SourceTasUpdateMetaAttribute>();
    }
}
