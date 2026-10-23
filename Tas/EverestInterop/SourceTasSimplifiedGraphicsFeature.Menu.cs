using System;
using System.Collections.Generic;
using System.Globalization;
using Celeste.Everest.UI.Settings;
using Celeste.Localization.Lookup;
using Celeste.Simulation.UI.Menus.Text;
using Celeste.Mods.Tas.Gameplay;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.EverestInterop
{
    public sealed partial class SourceTasSimplifiedGraphicsFeature
    {
        private string DialogText(string input) =>
            global::Celeste.Mods.Tas.SourceTasDialogText.ToDialogText(driver.Process, input);

        public SourceTasEaseInSubMenu CreateSubMenu(TextMenuResources resources, TextMenuPresentation menu)
        {
            var subMenu = new SourceTasEaseInSubMenu(resources, driver, DialogText("Simplified Graphics"), false);
            {
                subMenu.Add(new TextMenuPresentation.OnOff(DialogText("Enabled"), driver.Settings.SimplifiedGraphics,
                        driver.Process)
                    .Change(value => driver.Settings.SimplifiedGraphics = value));
                subMenu.Add(
                    new EverestMenuEnumerableSlider<bool>(DialogText("Gameplay"), CreateDefaultHideOptions(),
                            !driver.Settings.ShowGameplay)
                        .Change(value => driver.Settings.ShowGameplay = !value));
                subMenu.Add(
                    new EverestMenuEnumerableSlider<int?>(DialogText("Lighting"),
                            CreateSliderOptions(10, 0, i => $"{i * 10}%"),
                            driver.Settings.SimplifiedLighting)
                        .Change(value => driver.Settings.SimplifiedLighting = value));
                subMenu.Add(
                    new EverestMenuEnumerableSlider<int?>(DialogText("Bloom Base"),
                            CreateSliderOptions(0, 10, i => (i / 10f).ToString(CultureInfo.InvariantCulture)),
                            driver.Settings.SimplifiedBloomBase)
                        .Change(value => driver.Settings.SimplifiedBloomBase = value));
                subMenu.Add(
                    new EverestMenuEnumerableSlider<int?>(DialogText("Bloom Strength"), CreateSliderOptions(1, 10),
                            driver.Settings.SimplifiedBloomStrength)
                        .Change(value => driver.Settings.SimplifiedBloomStrength = value));
                subMenu.Add(
                    new EverestMenuEnumerableSlider<SpinnerColor>(DialogText("Spinner Color"), [
                                new KeyValuePair<SpinnerColor, string>(SpinnerColor.Default, DialogText("Default")),
                                new KeyValuePair<SpinnerColor, string>(SpinnerColor.White, DialogText("White")),
                                new KeyValuePair<SpinnerColor, string>(SpinnerColor.Blue, DialogText("Blue")),
                                new KeyValuePair<SpinnerColor, string>(SpinnerColor.Red, DialogText("Red")),
                                new KeyValuePair<SpinnerColor, string>(SpinnerColor.Purple, DialogText("Purple")),
                            ],
                            driver.Settings.SimplifiedSpinnerColor)
                        .Change(value => driver.Settings.SimplifiedSpinnerColor = value));
                subMenu.Add(
                    new EverestMenuEnumerableSlider<bool>(DialogText("Dust Sprite Edge"), CreateDefaultHideOptions(),
                            driver.Settings.SimplifiedDustSpriteEdge)
                        .Change(value => driver.Settings.SimplifiedDustSpriteEdge = value));
                subMenu.Add(
                    new EverestMenuEnumerableSlider<bool>(DialogText("Screen Wipe"), CreateDefaultHideOptions(),
                            driver.Settings.SimplifiedScreenWipe)
                        .Change(value => driver.Settings.SimplifiedScreenWipe = value));
                subMenu.Add(
                    new EverestMenuEnumerableSlider<bool>(DialogText("Color Grade"), CreateDefaultHideOptions(),
                            driver.Settings.SimplifiedColorGrade)
                        .Change(value => driver.Settings.SimplifiedColorGrade = value));
                subMenu.Add(
                    new EverestMenuEnumerableSlider<SolidTilesStyle>(DialogText("Solid Tiles Style"),
                            SolidTilesStyle.All,
                            driver.Settings.SimplifiedSolidTilesStyle)
                        .Change(value => driver.Settings.SimplifiedSolidTilesStyle = value));
                subMenu.Add(
                    new EverestMenuEnumerableSlider<bool>(DialogText("Background Tiles"), CreateDefaultHideOptions(),
                            driver.Settings.SimplifiedBackgroundTiles)
                        .Change(value => driver.Settings.SimplifiedBackgroundTiles = value));
                subMenu.Add(
                    new EverestMenuEnumerableSlider<bool>(DialogText("Backdrop"), CreateDefaultHideOptions(),
                            driver.Settings.SimplifiedBackdrop)
                        .Change(value => driver.Settings.SimplifiedBackdrop = value));
                subMenu.Add(
                    new EverestMenuEnumerableSlider<bool>(DialogText("Decal"), CreateDefaultHideOptions(),
                            driver.Settings.SimplifiedDecal)
                        .Change(value => driver.Settings.SimplifiedDecal = value));
                subMenu.Add(
                    new EverestMenuEnumerableSlider<bool>(DialogText("Particle"), CreateDefaultHideOptions(),
                            driver.Settings.SimplifiedParticle)
                        .Change(value => driver.Settings.SimplifiedParticle = value));
                subMenu.Add(new EverestMenuEnumerableSlider<bool>(DialogText("Distort"), CreateDefaultHideOptions(),
                    driver.Settings.SimplifiedDistort).Change(value => driver.Settings.SimplifiedDistort = value));
                subMenu.Add(new EverestMenuEnumerableSlider<bool>(DialogText("Mini Text Box"),
                        CreateDefaultHideOptions(),
                        driver.Settings.SimplifiedMiniTextbox)
                    .Change(value => driver.Settings.SimplifiedMiniTextbox = value));
                subMenu.Add(
                    new EverestMenuEnumerableSlider<bool>(DialogText("Lightning Strike"), CreateDefaultHideOptions(),
                            driver.Settings.SimplifiedLightningStrike)
                        .Change(value => driver.Settings.SimplifiedLightningStrike = value));

                TextMenuPresentation.Item clutteredItem;
                subMenu.Add(
                    clutteredItem = new EverestMenuEnumerableSlider<bool>(DialogText("Cluttered Entity"),
                            CreateDefaultHideOptions(),
                            driver.Settings.SimplifiedClutteredEntity)
                        .Change(value => driver.Settings.SimplifiedClutteredEntity = value));
                clutteredItem.AddDescription(subMenu, menu, DialogText("Cluttered Entity Description"));

                TextMenuPresentation.Item hudItem;
                subMenu.Add(
                    hudItem = new EverestMenuEnumerableSlider<bool>(DialogText("HUD"), CreateDefaultHideOptions(),
                            driver.Settings.SimplifiedHud)
                        .Change(value => driver.Settings.SimplifiedHud = value));
                hudItem.AddDescription(subMenu, menu, DialogText("HUD Description"));

                subMenu.Add(
                    new EverestMenuEnumerableSlider<bool>(DialogText("Waved Edge"), CreateSimplifyOptions(),
                            driver.Settings.SimplifiedWavedEdge)
                        .Change(value => driver.Settings.SimplifiedWavedEdge = value));

                subMenu.Add(
                    new EverestMenuEnumerableSlider<bool>(DialogText("Spikes"), CreateSimplifyOptions(),
                            driver.Settings.SimplifiedSpikes)
                        .Change(value => driver.Settings.SimplifiedSpikes = value));
            }
            return subMenu;
        }

        private IEnumerable<KeyValuePair<int?, string>> CreateSliderOptions(int start, int end,
            Func<int, string> formatter = null)
        {
            formatter ??= i => i.ToString();
            List<KeyValuePair<int?, string>> result = new();
            if (start <= end)
            {
                for (int current = start; current <= end; current++)
                    result.Add(new KeyValuePair<int?, string>(current, formatter(current)));
                result.Insert(0, new KeyValuePair<int?, string>(null, DialogText("Default")));
            }
            else
            {
                for (int current = start; current >= end; current--)
                    result.Add(new KeyValuePair<int?, string>(current, formatter(current)));
                result.Insert(0, new KeyValuePair<int?, string>(null, DialogText("Default")));
            }

            return result;
        }

        private IEnumerable<KeyValuePair<bool, string>> CreateDefaultHideOptions() =>
            new List<KeyValuePair<bool, string>>
            {
                new(false, DialogText("Default")), new(true, DialogText("Hide"))
            };

        private IEnumerable<KeyValuePair<bool, string>> CreateSimplifyOptions() => new List<KeyValuePair<bool, string>>
        {
            new(false, DialogText("Default")), new(true, DialogText("Simplify"))
        };

        public record struct SolidTilesStyle(string Name, char Value)
        {
            public static readonly SolidTilesStyle[] All =
            [
                default,
                new SolidTilesStyle("Dirt", '1'),
                new SolidTilesStyle("Snow", '3'),
                new SolidTilesStyle("Girder", '4'),
                new SolidTilesStyle("Tower", '5'),
                new SolidTilesStyle("Stone", '6'),
                new SolidTilesStyle("Cement", '7'),
                new SolidTilesStyle("Rock", '8'),
                new SolidTilesStyle("Wood", '9'),
                new SolidTilesStyle("Wood Stone", 'a'),
                new SolidTilesStyle("Cliffside", 'b'),
                new SolidTilesStyle("Pool Edges", 'c'),
                new SolidTilesStyle("Temple A", 'd'),
                new SolidTilesStyle("Temple B", 'e'),
                new SolidTilesStyle("Cliffside Alt", 'f'),
                new SolidTilesStyle("Reflection", 'g'),
                new SolidTilesStyle("Reflection Alt", 'G'),
                new SolidTilesStyle("Grass", 'h'),
                new SolidTilesStyle("Summit", 'i'),
                new SolidTilesStyle("Summit No Snow", 'j'),
                new SolidTilesStyle("Core", 'k'),
                new SolidTilesStyle("Deadgrass", 'l'),
                new SolidTilesStyle("Lost Levels", 'm'),
                new SolidTilesStyle("Scifi", 'n'),
                new SolidTilesStyle("Template", 'z'),
            ];

            public string Name = Name;
            public char Value = Value;

            public override string ToString()
            {
                return this == default
                    ? CelesteTasModule.Instance.Driver.SourceSimplifiedGraphicsFeature.DialogText("Default")
                    : Name;
            }
        }
    }
}
