using Celeste.Everest.UI.Settings;
using Celeste.Everest.UI.ModOptions;
using Celeste.Game.Menus.Overworld;
using Celeste.Game.Execution.Processes;
using Celeste.Simulation.UI.Menus.Text;

namespace Celeste.Mods.Tas.Hitboxes
{
    public sealed partial class SourceTasHitboxColor
    {
        public TextMenuPresentation.Item CreateEntityHitboxColorButton(TextMenuPresentation textMenu, bool inGame)
        {
            TextMenuPresentation.Item item =
                new TextMenuPresentation.Button(
                    SourceTasDialogText.ToDialogText(driver.Process, "Entity Hitbox Color") +
                    $": {ColorToHex(driver.Settings.EntityHitboxColor)}").Pressed(() =>
                {
                    ((IOriginalGameplayProcess)driver.Process).AudioSession.Play(
                        "event:/ui/main/savefile_rename_start");
                    ((OverworldSequence)textMenu.Scene).Goto<EverestModOptionStringState>()
                        .Init<EverestModOptionsState>(ColorToHex(driver.Settings.EntityHitboxColor),
                            value => driver.Settings.EntityHitboxColor =
                                HexToColor(value, driver.Settings.EntityHitboxColor), 9);
                });
            item.Disabled = inGame;
            return item;
        }

        public TextMenuPresentation.Item CreateTriggerHitboxColorButton(TextMenuPresentation textMenu, bool inGame)
        {
            TextMenuPresentation.Item item =
                new TextMenuPresentation.Button(
                    SourceTasDialogText.ToDialogText(driver.Process, "Trigger Hitbox Color") +
                    $": {ColorToHex(driver.Settings.TriggerHitboxColor)}").Pressed(() =>
                {
                    ((IOriginalGameplayProcess)driver.Process).AudioSession.Play(
                        "event:/ui/main/savefile_rename_start");
                    ((OverworldSequence)textMenu.Scene).Goto<EverestModOptionStringState>()
                        .Init<EverestModOptionsState>(ColorToHex(driver.Settings.TriggerHitboxColor),
                            value => driver.Settings.TriggerHitboxColor =
                                HexToColor(value, driver.Settings.TriggerHitboxColor), 9);
                });
            item.Disabled = inGame;
            return item;
        }

        public TextMenuPresentation.Item CreatePlatformHitboxColorButton(TextMenuPresentation textMenu, bool inGame)
        {
            TextMenuPresentation.Item item =
                new TextMenuPresentation.Button(
                        SourceTasDialogText.ToDialogText(driver.Process, "Platform Hitbox Color") +
                        $": {ColorToHex(driver.Settings.PlatformHitboxColor)}")
                    .Pressed(() =>
                    {
                        ((IOriginalGameplayProcess)driver.Process).AudioSession.Play(
                            "event:/ui/main/savefile_rename_start");
                        ((OverworldSequence)textMenu.Scene).Goto<EverestModOptionStringState>()
                            .Init<EverestModOptionsState>(ColorToHex(driver.Settings.PlatformHitboxColor),
                                value => driver.Settings.PlatformHitboxColor =
                                    HexToColor(value, driver.Settings.PlatformHitboxColor), 9);
                    });
            item.Disabled = inGame;
            return item;
        }
    }
}
