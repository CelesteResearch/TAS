using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Core.Scheduling;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;
using Actor = Celeste.Simulation.Gameplay.Actors.Foundation.SourceActorEntityState;
using Player = Celeste.Simulation.RoomPlayerSession;
using PlayerSeeker = Celeste.Game.Gameplay.Player.Transformations.Seeker.OriginalPlayerSeekerState;
using LevelData = Celeste.Game.Content.Maps.Levels.OriginalLevelRoomData;
using Overworld = Celeste.Game.Menus.Overworld.OverworldSequence;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasGameInfo
    {
        public string GetStatuses(Level level, Player player)
        {
            List<string> statuses = BorrowStatusParts();
            try
            {
                string noControlFrames = TransitionFrames > 0 ? $"({FormatFrames(TransitionFrames)})" : string.Empty;
                float unpauseTimer = level.UnpauseTimer;
                if (unpauseTimer > 0f)
                {
                    noControlFrames = $"({(int)Math.Ceiling(unpauseTimer / process.RawDeltaTime)})";
                }

                if (process.Effects.FreezeTimer > 0f)
                {
                    statuses.Add($"Frozen({(int)Math.Ceiling(process.Effects.FreezeTimer / process.RawDeltaTime)})");
                }

                if (global::Celeste.Simulation.Gameplay.Player.Runtime.PlayerFrameMotion
                        .InControl(player.Machine.State) &&
                    !level.Transitioning && unpauseTimer <= 0f)
                {
                    if (player.Data.CooldownTimer <= 0 && player.Data.Dashes > 0)
                    {
                        statuses.Add("CanDash");
                    }

                    if (ToFloorFrames(player.Data.Motion.JumpGraceTimer) is var coyote and > 0)
                    {
                        statuses.Add($"Coyote({FormatFrames(coyote)})");
                    }

                    if (ToFloorFrames(player.Data.Motion.Normal.VariableJumpTimer) is var jumpTimer and > 0)
                    {
                        statuses.Add($"Jump({FormatFrames(jumpTimer)})");
                    }

                    if (player.Machine.State == 0 &&
                        (player.Data.Motion.Normal.Speed.Y > 0f || player.Holding.Holding is { SlowFall: true }))
                    {
                        statuses.Add(
                            $"MaxFall({ConvertSpeedUnit(player.Data.Motion.Normal.MaxFall, driver.Settings.SpeedUnit):0.##})");
                    }

                    if (ToCeilingFrames(player.Data.Motion.ForceMoveXTimer) is var forceMoveXTimer and > 0)
                    {
                        string direction = player.Data.Motion.ForceMoveX switch
                        {
                            > 0 => "R",
                            < 0 => "L",
                            0 => "N",
                        };
                        statuses.Add($"ForceMove{direction}({FormatFrames(forceMoveXTimer)})");
                    }
                }
                else
                {
                    statuses.Add($"NoControl{noControlFrames}");
                }

                if (level.Wipe != null && !level.InCutscene &&
                    global::Celeste.Simulation.Gameplay.Player.Runtime.PlayerFrameMotion
                        .InControl(player.Machine.State))
                {
                    statuses.Add("CantPause");
                }

                if (player.Dead)
                {
                    statuses.Add("Dead");
                }

                if (level.InCutscene)
                {
                    statuses.Add("Cutscene");
                }

                if (player.Holding.Holding == null)
                {
                    foreach (var holdable in level.SourceScene.Components
                                 .GetCastComponents<global::Celeste.Simulation.Gameplay.Holdables.HoldableState>(
                                     "Celeste.Holdable"))
                    {
                        if (!holdable.Check(player)) continue;
                        statuses.Add("Grab");
                        break;
                    }
                }

                return string.Join(" ", statuses);
            }
            finally
            {
                ReturnStatusParts(statuses);
            }
        }

        private string GetStatusWithoutTime(string pos, string speed, string velocity, Player player,
            PlayerSeeker? playerSeeker,
            string polarVel, string analog, string retainedSpeed, string liftBoost, string miscStats, string statuses,
            string timers)
        {
            StringBuilder builder = BorrowStatusTextBuilder();
            try
            {
                builder.AppendLine(pos);
                builder.AppendLine(speed);
                builder.AppendLine(velocity);

                if (player.Machine.State == 19
                    || PlayerStates.GetCurrentStateName(player) == "Custom Feather"
                    || playerSeeker != null
                    || process.SaveContext.CurrentSave.Assists.ThreeSixtyDashing
                    || process.SaveContext.CurrentSave.Assists.SuperDashing
                    || driver.SourceExtendedVariants.SuperDashing)
                {
                    builder.AppendLine(polarVel);
                }

                if (!string.IsNullOrEmpty(analog))
                {
                    builder.AppendLine(analog);
                }

                if (!string.IsNullOrEmpty(retainedSpeed))
                {
                    builder.AppendLine(retainedSpeed);
                }

                if (!string.IsNullOrEmpty(liftBoost))
                {
                    builder.AppendLine(liftBoost);
                }

                builder.AppendLine(miscStats);
                if (!string.IsNullOrEmpty(statuses))
                {
                    builder.AppendLine(statuses);
                }

                if (!string.IsNullOrEmpty(timers))
                {
                    builder.AppendLine(timers);
                }

                return builder.ToString();
            }
            finally
            {
                ReturnStatusTextBuilder(builder);
            }
        }
    }
}
