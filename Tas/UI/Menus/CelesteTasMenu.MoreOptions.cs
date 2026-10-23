using System.Collections.Generic;
using StudioCommunication;
using Celeste.Core.Display;
using Celeste.Everest.UI.Settings;
using Celeste.Simulation.UI.Menus.Text;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.UI.Menus
{
    internal sealed partial class CelesteTasMenu
    {
        private SourceTasEaseInSubMenu CreateMoreOptionsSubMenu(TextMenuPresentation menu)
        {
            var subMenu = new SourceTasEaseInSubMenu(menu.Resources, driver, DialogText("More Options"), false);
            {
                subMenu.Add(new TextMenuPresentation.OnOff(DialogText("Center Camera"), Settings.CenterCamera, process)
                    .Change(value =>
                        Settings.CenterCamera = value));
                subMenu.Add(new TextMenuPresentation.OnOff(DialogText("Center Camera Horizontally Only"),
                    Settings.CenterCameraHorizontallyOnly, process).Change(value =>
                    Settings.CenterCameraHorizontallyOnly = value));
                TextMenuPresentation.Item autoEnableExCameraDynamicsItem;
                subMenu.Add(autoEnableExCameraDynamicsItem =
                    new TextMenuPresentation.OnOff(DialogText("Auto Enable ExCameraDynamics"),
                        Settings.EnableExCameraDynamicsForCenterCamera, process).Change(value =>
                        Settings.EnableExCameraDynamicsForCenterCamera = value));
                if (driver.SourceExCameraDynamicsInstalled)
                {
                    AddDescription(subMenu, menu, autoEnableExCameraDynamicsItem,
                        DialogText("ExCameraDynamics Description B"), PackedColor.Red);
                }
                else
                {
                    AddDescription(subMenu, menu, autoEnableExCameraDynamicsItem,
                        DialogText("ExCameraDynamics Description A"), PackedColor.Red);
                }

                subMenu.Add(
                    new TextMenuPresentation.OnOff(DialogText("Restore Settings"), Settings.RestoreSettings, process)
                        .Change(value =>
                            Settings.RestoreSettings = value));
                TextMenuPresentation.Item autoPauseDraftItem;
                subMenu.Add(autoPauseDraftItem =
                    new TextMenuPresentation.OnOff(DialogText("AUTO_PAUSE_DRAFT"), Settings.AutoPauseDraft, process)
                        .Change(value =>
                            Settings.AutoPauseDraft = value));
                AddDescription(subMenu, menu, autoPauseDraftItem, DialogText("AUTO_PAUSE_DRAFT_DESC"));
                subMenu.Add(
                    new TextMenuPresentation.OnOff(DialogText("Launch Studio At Boot"), Settings.LaunchStudioAtBoot,
                        process).Change(value =>
                    {
                        Settings.LaunchStudioAtBoot = value;
                        if (value)
                        {
                            // Also launch directly
                            driver.StudioHelper.LaunchStudio();
                        }
                    }));
                subMenu.Add(new TextMenuPresentation.OnOff(DialogText("Show Studio Update Banner"),
                    Settings.ShowStudioUpdateBanner, process).Change(value =>
                    Settings.ShowStudioUpdateBanner = value));
                subMenu.Add(new TextMenuPresentation.OnOff(DialogText("Attempt To Connect To Studio"),
                    Settings.AttemptConnectStudio, process).Change(value =>
                {
                    Settings.AttemptConnectStudio = value;
                    driver.ChangeSourceStudioStatus();
                }));
                TextMenuPresentation.Item betterInvincible;
                subMenu.Add(betterInvincible =
                    new TextMenuPresentation.OnOff(DialogText("Better Invincibility"), Settings.BetterInvincible,
                        process).Change(value =>
                    {
                        Settings.BetterInvincible = value;
                        driver.SourceBetterInvincible.Invincible =
                            false; // in case that value doesn't get reset for some unknown reason... yeah i have such bug report
                    }));
                AddDescription(subMenu, menu, betterInvincible, DialogText("Better Invincible Description"));

                void UpdateForceAllowAccessibilityDesc(EverestMenuEaseInSubHeaderExt desc, StudioEnableCondition value)
                {
                    switch (value)
                    {
                        case StudioEnableCondition.Never:
                            desc.FadeVisible = false;
                            break;
                        case StudioEnableCondition.Always:
                            desc.Title = DialogText("FORCE_ALLOW_ACCESS_TOOLS_DESC_D");
                            desc.TextColor = PackedColor.Red;
                            desc.FadeVisible = true;
                            break;
                        case StudioEnableCondition.WhileStudioConnected:
                            desc.Title = DialogText("FORCE_ALLOW_ACCESS_TOOLS_DESC_A");
                            desc.TextColor = PackedColor.Gray;
                            desc.FadeVisible = true;
                            break;
                        case StudioEnableCondition.ForCurrentSession:
                            desc.Title = DialogText("FORCE_ALLOW_ACCESS_TOOLS_DESC_C");
                            desc.TextColor = PackedColor.Yellow;
                            desc.FadeVisible = true;
                            break;
                        case StudioEnableCondition.AfterCasualPlaythrough:
                            desc.Title = DialogText("FORCE_ALLOW_ACCESS_TOOLS_DESC_B");
                            desc.TextColor = PackedColor.Gray;
                            desc.FadeVisible = true;
                            break;
                    }
                }

                TextMenuPresentation.Item forceAllowAccessibilityTools;
                EverestMenuEaseInSubHeaderExt forceAllowAccessibilityToolsExtraDesc = null!;
                subMenu.Add(forceAllowAccessibilityTools = new EverestMenuEnumerableSlider<StudioEnableCondition>(
                        DialogText("FORCE_ALLOW_ACCESS_TOOLS"), [
                            new KeyValuePair<StudioEnableCondition, string>(StudioEnableCondition.Never,
                                DialogText("NEVER")),
                            new KeyValuePair<StudioEnableCondition, string>(StudioEnableCondition.WhileStudioConnected,
                                DialogText("StudioEnableCondition_StudioConnected")),
                            new KeyValuePair<StudioEnableCondition, string>(
                                StudioEnableCondition.AfterCasualPlaythrough,
                                DialogText("StudioEnableCondition_AfterCasual")),
                            new KeyValuePair<StudioEnableCondition, string>(StudioEnableCondition.ForCurrentSession,
                                DialogText("StudioEnableCondition_CurrentSession")),
                            new KeyValuePair<StudioEnableCondition, string>(StudioEnableCondition.Always,
                                DialogText("ALWAYS"))
                        ], Settings.ForceAllowAccessibilityTools)
                    .Change(value =>
                    {
                        Settings.ForceAllowAccessibilityTools = value;
                        UpdateForceAllowAccessibilityDesc(forceAllowAccessibilityToolsExtraDesc, value);
                    }));
                AddDescription(subMenu, menu, forceAllowAccessibilityTools,
                    DialogText("FORCE_ALLOW_ACCESS_TOOLS_DESC_1"));
                AddDescription(subMenu, menu, forceAllowAccessibilityTools,
                    DialogText("FORCE_ALLOW_ACCESS_TOOLS_DESC_2"), PackedColor.Orange);
                forceAllowAccessibilityToolsExtraDesc =
                    AddDescription(subMenu, menu, forceAllowAccessibilityTools, string.Empty);
                UpdateForceAllowAccessibilityDesc(forceAllowAccessibilityToolsExtraDesc,
                    Settings.ForceAllowAccessibilityTools);
                forceAllowAccessibilityToolsExtraDesc.FadeVisible = false;

                TextMenuPresentation.Item preventSkinModGameplayChangesRTA;
                subMenu.Add(preventSkinModGameplayChangesRTA = new EverestMenuEnumerableSlider<GameplayEnableCondition>(
                        DialogText("PREVENT_SKINMOD_CHANGES"), [
                            new KeyValuePair<GameplayEnableCondition, string>(GameplayEnableCondition.Never,
                                DialogText("NEVER")),
                            new KeyValuePair<GameplayEnableCondition, string>(GameplayEnableCondition.DuringTAS,
                                DialogText("GameplayEnableCondition_DuringTAS")),
                            new KeyValuePair<GameplayEnableCondition, string>(GameplayEnableCondition.Always,
                                DialogText("ALWAYS"))
                        ], Settings.PreventSkinModGameplayChanges)
                    .Change(value => Settings.PreventSkinModGameplayChanges = value));
                AddDescription(subMenu, menu, preventSkinModGameplayChangesRTA,
                    DialogText("PREVENT_SKINMOD_CHANGES_DESC"));

                TextMenuPresentation.Item hideFreezeFramesItem;
                subMenu.Add(hideFreezeFramesItem =
                    new TextMenuPresentation.OnOff(DialogText("Hide Freeze Frames"), Settings.HideFreezeFrames, process)
                        .Change(value =>
                            Settings.HideFreezeFrames = value));
                AddDescription(subMenu, menu, hideFreezeFramesItem, DialogText("Hide Freeze Frames Description"));
                TextMenuPresentation.Item ignoreGcItem;
                subMenu.Add(ignoreGcItem =
                    new TextMenuPresentation.OnOff(DialogText("Ignore GC Collect"), Settings.IgnoreGcCollect, process)
                        .Change(value =>
                            Settings.IgnoreGcCollect = value));
                AddDescription(subMenu, menu, ignoreGcItem, DialogText("Ignore GC Collect Description"));
            }
            return subMenu;
        }
    }
}
