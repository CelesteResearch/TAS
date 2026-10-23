namespace Celeste.Mods.Tas.Hud
{
    public sealed partial class SourceTasInfoHud
    {
        public void Toggle()
        {
            if (driver.SourceHotkeys.InfoHud.DoublePressed)
            {
                driver.Settings.InfoHud = !driver.Settings.InfoHud;
                if (driver.Settings.InfoHud && driver.Settings.EnableInfoHudFirstTime)
                {
                    driver.Settings.EnableInfoHudFirstTime = false;
                    driver.ShowSourcePopupToast(
                        $"Info HUD is provided by TAS Mod\nDouble press {driver.SourceHotkeys.InfoHud} to toggle it",
                        10.0f);
                }

                driver.SourceModule.SaveSettings();
            }
        }
    }
}
