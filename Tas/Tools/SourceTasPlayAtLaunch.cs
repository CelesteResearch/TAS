using Celeste.Core.Scheduling;
using Celeste.Game.Menus.Overworld;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Tools
{
    /// Runs the TAS selected by the original launch flag after the Overworld initializes.
    internal sealed class SourceTasPlayAtLaunch(TasPlaybackDriver driver)
    {
        public string? FilePath;

        [SourceTasLoad]
        private static void Load()
            => CelesteTasModule.Instance.Process.SourceSceneTransitioned +=
                CelesteTasModule.Instance.Driver.PlayTasAtLaunch.OnSceneTransition;

        [SourceTasUnload]
        private static void Unload()
            => CelesteTasModule.Instance.Process.SourceSceneTransitioned -=
                CelesteTasModule.Instance.Driver.PlayTasAtLaunch.OnSceneTransition;

        private void OnSceneTransition(ISourceSceneExecution last, ISourceSceneExecution next)
        {
            if (FilePath != null && next is OverworldSequence overworld)
            {
                driver.Process.CurrentCoreSettings.DebugMode =
                    global::Celeste.Everest.Settings.EverestVanillaTristate.Everest;
                overworld.OnEndOfFrame += () =>
                {
                    driver.SetSourceFilePath(FilePath);
                    driver.EnableRun();
                    FilePath = null;
                };
            }
        }
    }
}
