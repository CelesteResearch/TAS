using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Celeste.Core.Display;
using Celeste.Everest.Modules.Runtime;
using Celeste.Everest.UI.Controls;
using Celeste.Everest.UI.Settings;
using Celeste.Game.Menus.Options;
using Celeste.Mods.Tas.Hitboxes;
using Celeste.Mods.Tas.Module;
using Celeste.Scene.Tracking;
using Celeste.Simulation.UI.Menus.Text;
using StudioCommunication;

namespace Celeste.Mods.Tas.UI.Menus
{
    internal sealed partial class CelesteTasMenu
    {
        private static readonly MethodInfo CreateKeyboardConfigUi = typeof(SourceEverestModule).GetMethod(
            "CreateKeyboardConfigUI", BindingFlags.Instance | BindingFlags.NonPublic);

        private static readonly MethodInfo CreateButtonConfigUi = typeof(SourceEverestModule).GetMethod(
            "CreateButtonConfigUI", BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly TasPlaybackDriver driver;
        private readonly SourceTasHitboxMenu hitboxMenu;
        private List<SourceTasEaseInSubMenu> options;
        private TextMenuPresentation.Item hotkeysSubMenu;
        private GameApplication process => driver.Process;
        private CelesteTasSettings Settings => driver.Settings;

        internal CelesteTasMenu(TasPlaybackDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            hitboxMenu = new SourceTasHitboxMenu(driver);
        }

        private string DialogText(string input)
        {
            return SourceTasDialogText.ToDialogText(process, input);
        }

        private void CreateOptions(SourceEverestModule module, TextMenuPresentation menu, bool inGame)
        {
            options = new List<SourceTasEaseInSubMenu>
            {
                hitboxMenu.CreateSubMenu(menu.Resources, menu, inGame),
                driver.SourceSimplifiedGraphicsFeature.CreateSubMenu(menu.Resources, menu),
                driver.Hud.CreateSubMenu(menu.Resources),
                CreateRoundValuesSubMenu(menu.Resources),
                CreateForwardSpeedSubMenu(menu.Resources),
                CreateHotkeysSubMenu(module, menu),
                CreateMoreOptionsSubMenu(menu)
            };
        }

        private EverestMenuEaseInSubHeaderExt AddDescription(EverestSubMenu subMenu, TextMenuPresentation menu,
            TextMenuPresentation.Item item, string description, PackedColor? color = null)
        {
            var text = new EverestMenuEaseInSubHeaderExt(description, false, menu)
            {
                TextColor = color ?? PackedColor.Gray,
                HeightExtra = 0f
            };
            subMenu.Add(text);
            item.OnEnter += () => text.FadeVisible = true;
            item.OnLeave += () => text.FadeVisible = false;
            return text;
        }

        private SourceTasEaseInSubMenu CreateForwardSpeedSubMenu(TextMenuResources resources)
        {
            var subMenu = new SourceTasEaseInSubMenu(resources, driver, DialogText("Forward Speed"), false);
            subMenu.Add(new EverestMenuIntSlider(DialogText("Fast Forward Speed"), 2, 30, Settings.FastForwardSpeed)
                .Change(value => Settings.FastForwardSpeed = value));
            subMenu.Add(new EverestMenuEnumerableSlider<float>(DialogText("Slow Forward Speed"),
                    Settings.SlowForwardSpeeds, Settings.SlowForwardSpeed)
                .Change(value => Settings.SlowForwardSpeed = value));
            return subMenu;
        }

        private SourceTasEaseInSubMenu CreateHotkeysSubMenu(SourceEverestModule module, TextMenuPresentation menu)
        {
            var subMenu = new SourceTasEaseInSubMenu(menu.Resources, driver, DialogText("Hotkeys"), false);
            subMenu.Add(new TextMenuPresentation.Button(process.Text.Dialog.Clean("options_keyconfig")).Pressed(() =>
            {
                subMenu.Focused = false;
                EverestKeyboardConfigPresentation keyboardConfig = CreateKeyboardConfigUi != null
                    ? (EverestKeyboardConfigPresentation)CreateKeyboardConfigUi.Invoke(module, new object[] { menu })
                    : new ModuleSettingsKeyboardConfigUI(module, process,
                        ((IOriginalMenuOptionsEnvironment)process).MenuOptions);
                keyboardConfig.OnClose = () => subMenu.Focused = true;
                var scene = ((ISourceScene)process.Frame.Scenes.CurrentScene).SourceScene;
                scene.QueueSourceEntity(keyboardConfig);
                scene.OnEndOfFrame += () => scene.CommitQueuedEntityLists();
            }));
            subMenu.Add(new TextMenuPresentation.Button(process.Text.Dialog.Clean("options_btnconfig")).Pressed(() =>
            {
                subMenu.Focused = false;
                EverestButtonConfigPresentation buttonConfig = CreateButtonConfigUi != null
                    ? (EverestButtonConfigPresentation)CreateButtonConfigUi.Invoke(module, new object[] { menu })
                    : new ModuleSettingsButtonConfigUI(module, process,
                        ((IOriginalMenuOptionsEnvironment)process).MenuOptions);
                buttonConfig.OnClose = () => subMenu.Focused = true;
                var scene = ((ISourceScene)process.Frame.Scenes.CurrentScene).SourceScene;
                scene.QueueSourceEntity(buttonConfig);
                scene.OnEndOfFrame += () => scene.CommitQueuedEntityLists();
            }));
            hotkeysSubMenu = subMenu;
            return subMenu;
        }

        private SourceTasEaseInSubMenu CreateRoundValuesSubMenu(TextMenuResources resources)
        {
            var subMenu = new SourceTasEaseInSubMenu(resources, driver, DialogText("Round Values"), false);
            subMenu.Add(new EverestMenuIntSlider(DialogText("Position Decimals"), GameSettings.MinDecimals,
                    GameSettings.MaxDecimals, Settings.PositionDecimals)
                .Change(value => Settings.PositionDecimals = value));
            subMenu.Add(new EverestMenuIntSlider(DialogText("Speed Decimals"), GameSettings.MinDecimals,
                GameSettings.MaxDecimals, Settings.SpeedDecimals).Change(value => Settings.SpeedDecimals = value));
            subMenu.Add(new EverestMenuIntSlider(DialogText("Velocity Decimals"), GameSettings.MinDecimals,
                    GameSettings.MaxDecimals, Settings.VelocityDecimals)
                .Change(value => Settings.VelocityDecimals = value));
            subMenu.Add(new EverestMenuIntSlider(DialogText("Angle Decimals"), GameSettings.MinDecimals,
                GameSettings.MaxDecimals, Settings.AngleDecimals).Change(value => Settings.AngleDecimals = value));
            subMenu.Add(new EverestMenuIntSlider(DialogText("Custom Info Decimals"), GameSettings.MinDecimals,
                    GameSettings.MaxDecimals, Settings.CustomInfoDecimals)
                .Change(value => Settings.CustomInfoDecimals = value));
            subMenu.Add(new EverestMenuIntSlider(DialogText("Subpixel Indicator Decimals"), 1,
                    GameSettings.MaxDecimals, Settings.SubpixelIndicatorDecimals)
                .Change(value => Settings.SubpixelIndicatorDecimals = value));
            var units = new[]
            {
                new KeyValuePair<SpeedUnit, string>(SpeedUnit.PixelPerSecond, DialogText("Pixel per Second")),
                new KeyValuePair<SpeedUnit, string>(SpeedUnit.PixelPerFrame, DialogText("Pixel per Frame"))
            };
            subMenu.Add(new EverestMenuEnumerableSlider<SpeedUnit>(DialogText("Speed Unit"), units, Settings.SpeedUnit)
                .Change(value => Settings.SpeedUnit = value));
            subMenu.Add(
                new EverestMenuEnumerableSlider<SpeedUnit>(DialogText("Velocity Unit"), units, Settings.VelocityUnit)
                    .Change(value => Settings.VelocityUnit = value));
            return subMenu;
        }

        internal void CreateMenu(SourceEverestModule module, TextMenuPresentation menu, bool inGame)
        {
            menu.OnClose += () => options = null;
            var descriptions = new List<EverestMenuEaseInSubHeaderExt>();

            EverestMenuEaseInSubHeaderExt AddEnabledDescription(TextMenuPresentation.Item item, string description)
            {
                var text = new EverestMenuEaseInSubHeaderExt(description, false, menu)
                {
                    TextColor = PackedColor.Gray,
                    HeightExtra = 0f
                };
                if (menu.Items.Contains(item))
                {
                    menu.Insert(menu.Items.IndexOf(item) + 1, text);
                }

                item.OnEnter += () => text.FadeVisible = Settings.Enabled;
                item.OnLeave += () => text.FadeVisible = false;
                return text;
            }

            var enabledItem =
                new TextMenuPresentation.OnOff(DialogText("Enabled"), Settings.Enabled, process).Change(value =>
                {
                    Settings.Enabled = value;
                    foreach (var subMenu in options)
                    {
                        subMenu.FadeVisible = value;
                    }

                    foreach (var text in descriptions)
                    {
                        text.FadeVisible = value;
                    }

                    driver.ChangeSourceStudioStatus();
                });
            menu.Add(enabledItem);
            CreateOptions(module, menu, inGame);
            foreach (var subMenu in options)
            {
                menu.Add(subMenu);
            }

            string path = driver.Controller.FilePath;
            if (!string.IsNullOrEmpty(path))
            {
                foreach (string text in Split(path, 60).Reverse())
                {
                    descriptions.Add(AddEnabledDescription(enabledItem, text));
                }
            }

            descriptions.Add(AddEnabledDescription(enabledItem, DialogText("Enabled Description")));
            hitboxMenu.AddSubMenuDescription(menu, inGame);
            driver.Hud.AddSubMenuDescription(menu);
            hotkeysSubMenu.AddDescription(menu, DialogText("Hotkeys Description"));
            hotkeysSubMenu = null;
        }

        private static IEnumerable<string> Split(string text, int count)
        {
            if (string.IsNullOrEmpty(text) || count < 1)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < text.Length; i += count)
            {
                yield return text.Substring(i, Math.Min(count, text.Length - i));
            }
        }
    }
}
