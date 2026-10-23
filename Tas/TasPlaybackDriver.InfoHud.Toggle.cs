namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        internal void ToggleSourceInfoHud()
        {
            if (hotkeys.InfoHud.DoublePressed)
            {
                Settings.InfoHud = !Settings.InfoHud;
                if (Settings.InfoHud && Settings.EnableInfoHudFirstTime)
                {
                    Settings.EnableInfoHudFirstTime = false;
                    ShowSourcePopupToast(
                        $"Info HUD is provided by TAS Mod\nDouble press {hotkeys.InfoHud} to toggle it",
                        10.0f);
                }

                module.SaveSettings();
            }
        }

        internal void SaveModuleSettings() => module.SaveSettings();
    }
}
