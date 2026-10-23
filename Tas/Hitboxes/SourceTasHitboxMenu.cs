using System;
using StudioCommunication;
using Celeste.Everest.Modules.Updating;
using Celeste.Everest.UI.Settings;
using Celeste.Mods.Tas.Module;
using Celeste.Simulation.UI.Menus.Text;

namespace Celeste.Mods.Tas.Hitboxes
{
    public sealed class SourceTasHitboxMenu
    {
        private readonly TasPlaybackDriver driver;
        private SourceTasEaseInSubMenu subMenuItem;

        public SourceTasHitboxMenu(TasPlaybackDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        private string DialogText(string input)
        {
            return SourceTasDialogText.ToDialogText(driver.Process, input);
        }

        public SourceTasEaseInSubMenu CreateSubMenu(TextMenuResources resources, TextMenuPresentation menu, bool inGame)
        {
            var subMenu = new SourceTasEaseInSubMenu(resources, driver, DialogText("Show Hitboxes"), false);
            subMenu.Add(new TextMenuPresentation.OnOff(DialogText("Enabled"), driver.Settings.ShowHitboxes,
                    driver.Process)
                .Change(value => driver.Settings.ShowHitboxes = value));
            subMenu.Add(new TextMenuPresentation.OnOff(DialogText("Show Trigger Hitboxes"),
                    driver.Settings.ShowTriggerHitboxes, driver.Process)
                .Change(value => driver.Settings.ShowTriggerHitboxes = value));
            subMenu.Add(new TextMenuPresentation.OnOff(DialogText("Show Unloaded Rooms Hitboxes"),
                    driver.Settings.ShowUnloadedRoomsHitboxes, driver.Process)
                .Change(value => driver.Settings.ShowUnloadedRoomsHitboxes = value));
            subMenu.Add(new TextMenuPresentation.OnOff(DialogText("Show Camera Hitboxes"),
                    driver.Settings.ShowCameraHitboxes, driver.Process)
                .Change(value => driver.Settings.ShowCameraHitboxes = value));
            var actualCollide =
                new TextMenuPresentation.Option<ActualCollideHitboxType>(DialogText("Actual Collide Hitboxes"));
            foreach (ActualCollideHitboxType value in Enum.GetValues(typeof(ActualCollideHitboxType)))
            {
                actualCollide.Add(DialogText(EverestStringSource.SpacedPascalCase(value.ToString())), value,
                    value.Equals(driver.Settings.ShowActualCollideHitboxes));
            }

            actualCollide.Change(value => driver.Settings.ShowActualCollideHitboxes = value);
            subMenu.Add(actualCollide);
            subMenu.Add(new TextMenuPresentation.OnOff(DialogText("Show Cycle Hitbox Colors"),
                    driver.Settings.ShowCycleHitboxColors, driver.Process)
                .Change(value => driver.Settings.ShowCycleHitboxColors = value));
            subMenu.Add(new TextMenuPresentation.OnOff(DialogText("Simplified Hitboxes"),
                    driver.Settings.SimplifiedHitboxes, driver.Process)
                .Change(value => driver.Settings.SimplifiedHitboxes = value));
            subMenu.Add(new EverestMenuIntSlider(DialogText("Un-collidable Hitboxes Opacity"), 0, 10,
                    driver.Settings.UnCollidableHitboxesOpacity)
                .Change(value => driver.Settings.UnCollidableHitboxesOpacity = value));
            subMenu.Add(driver.SourceHitboxColor.CreateEntityHitboxColorButton(menu, inGame));
            subMenu.Add(driver.SourceHitboxColor.CreateTriggerHitboxColorButton(menu, inGame));
            subMenu.Add(driver.SourceHitboxColor.CreatePlatformHitboxColorButton(menu, inGame));
            subMenu.Add(driver.SourceCycleHitboxColor.CreateCycleHitboxColorButton(0, menu, inGame));
            subMenu.Add(driver.SourceCycleHitboxColor.CreateCycleHitboxColorButton(1, menu, inGame));
            subMenu.Add(driver.SourceCycleHitboxColor.CreateCycleHitboxColorButton(2, menu, inGame));
            subMenu.Add(driver.SourceCycleHitboxColor.CreateCycleHitboxColorButton(3, menu, inGame));
            subMenuItem = subMenu;
            return subMenuItem;
        }

        public void AddSubMenuDescription(TextMenuPresentation menu, bool inGame)
        {
            if (inGame)
            {
                subMenuItem.AddDescription(menu, DialogText("Hitbox Color Description"));
            }

            subMenuItem = null;
        }
    }
}
