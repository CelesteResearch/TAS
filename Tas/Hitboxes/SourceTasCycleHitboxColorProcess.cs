using Celeste.Core.Display;
using System;
using System.Collections.Generic;
using Celeste.Core.Rendering;
using Celeste.Core.Rendering.Diagnostics;
using Celeste.Scene.Entities;
using Celeste.Scene.Scenes;
using Celeste.Scene.Tracking;
using Celeste.Graphics.Display;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Execution.Processes;
using Celeste.Game.Content.Dialogs;
using Celeste.Game.Menus.Overworld;
using Celeste.Everest.UI.Settings;
using Celeste.Everest.UI.ModOptions;
using Celeste.Simulation.UI.Menus.Text;
using Celeste.Framework.Diagnostics.Commands;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Hitboxes
{
    public sealed partial class SourceTasCycleHitboxColorProcess
    {
        private readonly Celeste.Mods.Tas.TasPlaybackDriver driver;

        public SourceTasCycleHitboxColorProcess(Celeste.Mods.Tas.TasPlaybackDriver driver) =>
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));

        public static readonly PackedColor DefaultColor1 = PackedColor.Red;
        public static readonly PackedColor DefaultColor2 = PackedColor.Yellow;
        public static readonly PackedColor DefaultColor3 = new(0.1f, 0.2f, 1f);
        public static readonly PackedColor DefaultOthersColor = new(0.25f, 1f, 0.5f);

        private readonly Dictionary<Type, Func<object, float?>> OffsetGetters = new();

        // This counter defines which group is assigned to which third frame
        public int GroupCounter;

        private void EntityOnDebugRender(Action<SceneEntityState, ISourceRenderContext, object> orig,
            SceneEntityState self,
            ISourceRenderContext context, object camera)
        {
            if (driver.Settings.ShowHitboxes && driver.Settings.ShowCycleHitboxColors && self.Collider is { } collider)
            {
                float? offset = self is ISourceOptionalCycleHitboxOffset optionalOffset
                    ? optionalOffset.CycleHitboxOffset
                    : self is ISourceCycleHitboxOffset sourceOffset
                        ? sourceOffset.CycleHitboxOffset
                        : null;

                if (offset == null && self is not ISourceOptionalCycleHitboxOffset &&
                    OffsetGetters.TryGetValue(self.GetType(), out var getter))
                {
                    offset = getter(self);
                }

                if (offset is { } offsetValue)
                {
                    // Calculate how many frames away is the hazard's loading check (time distance)
                    float time = ((ISourceScene)self.Scene).SourceScene.Clock.TimeActive;
                    int timeDist = 0;

                    while (Math.Floor(((double)time - offsetValue - driver.Process.DeltaTime) / 0.05f) >=
                           Math.Floor(((double)time - offsetValue) / 0.05f) &&
                           timeDist < 3)
                    {
                        time += driver.Process.DeltaTime;
                        timeDist++;
                    }

                    // Calculate what the value of the counter is after the time distance, which defines the hazard's group
                    int group = 3;
                    if (timeDist < 3)
                    {
                        group = (timeDist + GroupCounter) % 3;
                    }

                    collider.Render(camera,
                        GetColor(group) * (self.Collidable ? 1f : driver.SourceHitboxColor.UnCollidableAlpha), context);
                    return;
                }
            }

            orig(self, context, camera);
        }

        private void SceneOnBeforeUpdate(Action<SourceSceneState> orig, SourceSceneState self)
        {
            float timeActive = self.TimeActive;
            orig(self);
            if (self is OriginalLevelSceneRoot && Math.Abs(timeActive - self.TimeActive) > 0.000001f)
            {
                // If the scene isn't paused (TimeActive is increased), advance the spinner group counter
                GroupCounter = (GroupCounter + 1) % 3;
            }
        }

        private void LevelOnBegin(Action<OriginalLevelSceneRoot> orig, OriginalLevelSceneRoot self)
        {
            orig(self);
            GroupCounter = 0;
        }

        private PackedColor GetColor(int index)
        {
            return index switch
            {
                0 => driver.Settings.CycleHitboxColor1,
                1 => driver.Settings.CycleHitboxColor2,
                2 => driver.Settings.CycleHitboxColor3,
                3 => driver.Settings.OtherCyclesHitboxColor,
                _ => throw new InvalidOperationException()
            };
        }

        private void SetColor(int index, string colorHex)
        {
            PackedColor color = SourceTasHitboxColor.HexToColor(colorHex, GetColor(index));

            if (index == 0)
            {
                driver.Settings.CycleHitboxColor1 = color;
            }
            else if (index == 1)
            {
                driver.Settings.CycleHitboxColor2 = color;
            }
            else if (index == 2)
            {
                driver.Settings.CycleHitboxColor3 = color;
            }
            else if (index == 3)
            {
                driver.Settings.OtherCyclesHitboxColor = color;
            }
        }

        public TextMenuPresentation.Item CreateCycleHitboxColorButton(int index, TextMenuPresentation textMenu,
            bool inGame)
        {
            TextMenuPresentation.Item item =
                new TextMenuPresentation.Button(
                        SourceTasDialogText.ToDialogText(driver.Process, $"Cycle{index + 1} Hitbox Color") +
                        $": {SourceTasHitboxColor.ColorToHex(GetColor(index))}")
                    .Pressed(() =>
                    {
                        ((IOriginalGameplayProcess)driver.Process).AudioSession.Play(
                            "event:/ui/main/savefile_rename_start");
                        ((OverworldSequence)textMenu.Scene).Goto<EverestModOptionStringState>()
                            .Init<EverestModOptionsState>(SourceTasHitboxColor.ColorToHex(GetColor(index)),
                                value => SetColor(index, value), 9);
                    });
            item.Disabled = inGame;
            return item;
        }

        [SourceCommandAttribute("cycle1_hitbox_color",
            "change the cycle 1 hitbox color (ARGB). eg Red = F00 or FF00 or FFFF0000 (CelesteTAS)")]
        internal void CmdChangeCycleHitboxColor1(string color)
        {
            driver.Settings.CycleHitboxColor1 = SourceTasHitboxColor.HexToColor(color, DefaultColor1);
            CelesteTasModule.Instance.SaveSettings();
        }

        [SourceCommandAttribute("cycle2_hitbox_color",
            "change the cycle 2 hitbox color (ARGB). eg Red = F00 or FF00 or FFFF0000 (CelesteTAS)")]
        internal void CmdChangeCycleHitboxColor2(string color)
        {
            driver.Settings.CycleHitboxColor2 = SourceTasHitboxColor.HexToColor(color, DefaultColor2);
            CelesteTasModule.Instance.SaveSettings();
        }

        [SourceCommandAttribute("cycle3_hitbox_color",
            "change the cycle 3 hitbox color (ARGB). eg Red = F00 or FF00 or FFFF0000 (CelesteTAS)")]
        internal void CmdChangeCycleHitboxColor3(string color)
        {
            driver.Settings.CycleHitboxColor3 = SourceTasHitboxColor.HexToColor(color, DefaultColor3);
            CelesteTasModule.Instance.SaveSettings();
        }

        [SourceCommandAttribute("other_cycles_hitbox_color",
            "change other cycles hitbox color (ARGB). eg Red = F00 or FF00 or FFFF0000 (CelesteTAS)")]
        internal void CmdChangeOtherCyclesHitboxColor(string color)
        {
            driver.Settings.OtherCyclesHitboxColor = SourceTasHitboxColor.HexToColor(color, DefaultOthersColor);
            CelesteTasModule.Instance.SaveSettings();
        }
    }
}
