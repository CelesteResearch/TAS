using Celeste.Everest.Runtime;

namespace Celeste.Mods.Tas.Gameplay.Optimization
{
    internal sealed class SourceTasHeadlessOptimizations
    {
        private readonly TasPlaybackDriver driver;

        internal SourceTasHeadlessOptimizations(TasPlaybackDriver driver)
        {
            this.driver = driver;
        }

        internal void LoadContent()
        {
            if (!((IEverestHeadlessEnvironment)driver.Process).IsHeadless && !driver.SourceSyncChecker.Active) return;
            // Original source explicitly contains no optimization operations here.
        }
    }
}
