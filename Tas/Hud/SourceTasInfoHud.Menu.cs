using System.Collections.Generic;
using StudioCommunication;
using Celeste.Everest.Runtime.Input;
using Celeste.Everest.UI.Settings;
using Celeste.Localization.Lookup;
using Celeste.Simulation.UI.Menus.Text;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Hud
{
    public sealed partial class SourceTasInfoHud
    {
        private SourceTasEaseInSubMenu subMenuItem;

        private string DialogText(string input) =>
            global::Celeste.Mods.Tas.SourceTasDialogText.ToDialogText(process, input);

        public SourceTasEaseInSubMenu CreateSubMenu(TextMenuResources resources)
        {
            var subMenu = new SourceTasEaseInSubMenu(resources, driver, DialogText("Info HUD"), false);
            {
                subMenu.Add(
                    new TextMenuPresentation.OnOff(DialogText("Enabled"), driver.Settings.InfoHud, process)
                        .Change(value =>
                            driver.Settings.InfoHud = value));
                subMenu.Add(
                    new TextMenuPresentation.OnOff(DialogText("Info Game"), driver.Settings.InfoGame, process)
                        .Change(value => driver.Settings.InfoGame = value));
                subMenu.Add(
                    new TextMenuPresentation.OnOff(DialogText("Info TAS Input"), driver.Settings.InfoTasInput, process)
                        .Change(value =>
                            driver.Settings.InfoTasInput = value));
                subMenu.Add(new TextMenuPresentation.OnOff(DialogText("Info Subpixel Indicator"),
                    driver.Settings.InfoSubpixelIndicator, process).Change(value =>
                    driver.Settings.InfoSubpixelIndicator = value));
                subMenu.Add(new EverestMenuEnumerableSlider<HudOptions>(DialogText("Info Custom"), CreateHudOptions(),
                        driver.Settings.InfoCustom)
                    .Change(value => driver.Settings.InfoCustom = value));
                subMenu.Add(new TextMenuPresentation.Button(DialogText("Info Copy Custom Template")).Pressed(() =>
                    ((IEverestTextInputEnvironment)process).TextInput.SetClipboardText(
                        string.IsNullOrEmpty(driver.Settings.InfoCustomTemplate)
                            ? "\0"
                            : driver.Settings.InfoCustomTemplate)));
                subMenu.Add(new TextMenuPresentation.Button(DialogText("Info Set Custom Template")).Pressed(() =>
                {
                    driver.Settings.InfoCustomTemplate =
                        ((IEverestTextInputEnvironment)process).TextInput.GetClipboardText() ?? string.Empty;
                    driver.SourceModule.SaveSettings();
                }));

                subMenu.Add(new EverestMenuEnumerableSlider<WatchEntityType>(DialogText("Info Watch Entity HUD Type"), [
                        new KeyValuePair<WatchEntityType, string>(WatchEntityType.None,
                            DialogText("Info Watch Entity None")),
                        new KeyValuePair<WatchEntityType, string>(WatchEntityType.Position,
                            DialogText("Info Watch Entity Position")),
                        new KeyValuePair<WatchEntityType, string>(WatchEntityType.DeclaredOnly,
                            DialogText("Info Watch Entity Declared Only")),
                        new KeyValuePair<WatchEntityType, string>(WatchEntityType.All,
                            DialogText("Info Watch Entity All"))
                    ], driver.Settings.InfoWatchEntityHudType)
                    .Change(value => driver.Settings.InfoWatchEntityHudType = value));
                subMenu.Add(new EverestMenuEnumerableSlider<WatchEntityType>(
                        DialogText("Info Watch Entity Studio Type"), [
                            new KeyValuePair<WatchEntityType, string>(WatchEntityType.None,
                                DialogText("Info Watch Entity None")),
                            new KeyValuePair<WatchEntityType, string>(WatchEntityType.Position,
                                DialogText("Info Watch Entity Position")),
                            new KeyValuePair<WatchEntityType, string>(WatchEntityType.DeclaredOnly,
                                DialogText("Info Watch Entity Declared Only")),
                            new KeyValuePair<WatchEntityType, string>(WatchEntityType.All,
                                DialogText("Info Watch Entity All"))
                        ], driver.Settings.InfoWatchEntityStudioType)
                    .Change(value => driver.Settings.InfoWatchEntityStudioType = value));
                subMenu.Add(new TextMenuPresentation.OnOff(DialogText("Info Watch Entity Log To Console"),
                        driver.Settings.InfoWatchEntityLogToConsole, process)
                    .Change(value => driver.Settings.InfoWatchEntityLogToConsole = value));
                subMenu.Add(new EverestMenuIntSlider(DialogText("Info Text Size"), 5, 20, driver.Settings.InfoTextSize)
                    .Change(value =>
                        driver.Settings.InfoTextSize = value));
                subMenu.Add(new EverestMenuIntSlider(DialogText("Info Subpixel Indicator Size"), 5, 20,
                        driver.Settings.InfoSubpixelIndicatorSize)
                    .Change(value =>
                        driver.Settings.InfoSubpixelIndicatorSize = value));
                subMenu.Add(
                    new EverestMenuIntSlider(DialogText("Info Opacity"), 1, 10, driver.Settings.InfoOpacity)
                        .Change(value =>
                            driver.Settings.InfoOpacity = value));
                subMenu.Add(
                    new EverestMenuIntSlider(DialogText("Info Masked Opacity"), 0, 10,
                            driver.Settings.InfoMaskedOpacity)
                        .Change(value =>
                            driver.Settings.InfoMaskedOpacity = value));
            }
            subMenuItem = subMenu;
            return subMenuItem;
        }

        private KeyValuePair<HudOptions, string>[] CreateHudOptions()
        {
            return
            [
                new KeyValuePair<HudOptions, string>(HudOptions.Off, DialogText("Info HUD Options Off")),
                new KeyValuePair<HudOptions, string>(HudOptions.HudOnly, DialogText("Info HUD Options Hud Only")),
                new KeyValuePair<HudOptions, string>(HudOptions.StudioOnly, DialogText("Info HUD Options Studio Only")),
                new KeyValuePair<HudOptions, string>(HudOptions.Both, DialogText("Info HUD Options Both")),
            ];
        }

        public void AddSubMenuDescription(TextMenuPresentation menu)
        {
            subMenuItem.AddDescription(menu, DialogText("Info HUD Description"));
            subMenuItem = null;
        }
    }
}
