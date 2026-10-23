using Celeste.Game.Execution.Levels;

namespace Celeste.Mods.Tas
{
    public sealed partial class SourceTasHotkeys
    {
        private void AfterUpdate()
        {
            if (process.Frame.Scenes.CurrentScene is OriginalLevelSceneRoot level &&
                (!level.Paused || level.PauseMainMenuOpen || driver.Running))
            {
                if (Hitboxes.Pressed)
                {
                    driver.Settings.ShowHitboxes = !driver.Settings.ShowHitboxes;
                    driver.SaveModuleSettings();
                }

                if (TriggerHitboxes.Pressed)
                {
                    driver.Settings.ShowTriggerHitboxes = !driver.Settings.ShowTriggerHitboxes;
                    driver.SaveModuleSettings();
                }

                if (SimplifiedGraphic.Pressed)
                {
                    driver.Settings.SimplifiedGraphics = !driver.Settings.SimplifiedGraphics;
                    driver.SaveModuleSettings();
                }

                if (CenterCamera.Pressed)
                {
                    driver.Settings.CenterCamera = !driver.Settings.CenterCamera;
                    driver.SaveModuleSettings();
                }
            }

            driver.ToggleSourceInfoHud();
        }
    }
}
