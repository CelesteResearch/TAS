using Celeste.Mods.Tas.Input;
using Follower = Celeste.Scene.Following.FollowerState;
using Strawberry = Celeste.Game.Gameplay.Collectibles.Strawberries.OriginalStrawberryState;
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
        public void Update(bool updateVel = false)
        {
            if (driver.SourceTasHelper.InPrediction)
            {
                return;
            }

            ISourceSceneExecution scene = process.Frame.Scenes.CurrentScene;

            // Dynamically show real-time / game-time timer
            bool showRealTime = driver.SourceMetadataCommands.RealTimeInfo != null &&
                                driver.Controller.HasRealTimeDisplayCommand;
            bool showGameTime = !showRealTime || driver.Controller.HasGameTimeDisplayCommand;

            if (scene is Level level)
            {
                Player player =
                    level.SourceScene.Entities.GetTrackedEntity<Player>(
                        typeof(global::Celeste.Simulation.RoomPlayerSession)
                            .FullName);

                if (player != null)
                {
                    string pos = GetAdjustedPos(player, out string exactPos);
                    string speed = GetAdjustedSpeed(player.Data.Motion.Normal.Speed, out string exactSpeed);
                    Vector2Double diff = player.GetMoreExactPosition(false) - LastPos;
                    if (!Frozen && updateVel)
                    {
                        LastDiff = diff;
                    }
                    else
                    {
                        diff = LastDiff;
                    }

                    if (driver.Settings.VelocityUnit == SpeedUnit.PixelPerSecond)
                    {
                        diff *= FramesPerRealSecond;
                    }

                    string velocity = GetAdjustedVelocity(diff, out string exactVelocity);

                    string polarVel = GetAdjustedPolarVel(diff, out string exactPolarVel);

                    string analog = string.Empty;
                    string exactAnalog = string.Empty;
                    if (driver.Running && driver.Controller.Previous is { } inputFrame &&
                        SourceTasEnumExtensions.Has(inputFrame.Actions, Actions.Feather))
                    {
                        analog = GetAdjustedAnalog(inputFrame.StickPosition, out exactAnalog);
                    }

                    string retainedSpeed = GetAdjustedRetainedSpeed(player, out string exactRetainedSpeed);

                    string liftBoost = GetAdjustedLiftBoost(player, out string exactLiftBoost);

                    string miscStats = $"Stamina: {player.Data.Stamina:F2} "
                                       + (player.World.Contacts.WallJumpCheck(1) ? "Wall-R " : string.Empty)
                                       + (player.World.Contacts.WallJumpCheck(-1) ? "Wall-L " : string.Empty)
                                       + PlayerStates.GetCurrentStateName(player);

                    int dashCooldown = ToFloorFrames(player.Data.CooldownTimer);

                    PlayerSeeker? playerSeeker = level.Scheduler.Entities.GetEntityTrackIfNeeded<PlayerSeeker>();
                    if (playerSeeker != null)
                    {
                        pos = GetAdjustedPos(playerSeeker, out exactPos);
                        speed = GetAdjustedSpeed(playerSeeker.Speed, out exactSpeed);
                        diff = playerSeeker.GetMoreExactPosition(false) - LastPlayerSeekerPos;
                        if (!Frozen && updateVel)
                        {
                            LastPlayerSeekerDiff = diff;
                        }
                        else
                        {
                            diff = LastPlayerSeekerDiff;
                        }

                        if (driver.Settings.VelocityUnit == SpeedUnit.PixelPerSecond)
                        {
                            diff *= FramesPerRealSecond;
                        }

                        velocity = GetAdjustedVelocity(diff, out exactVelocity);

                        polarVel = $"Chase: {diff.Length():F2}, {diff.Angle():F5}°";
                        dashCooldown = ToCeilingFrames(playerSeeker.DashTimer);
                    }

                    string statuses = GetStatuses(level, player);

                    string timers = string.Empty;
                    Follower? firstRedBerryFollower =
                        player.Leader.Followers.Find(follower => follower.Entity is Strawberry { Golden: false });
                    if (firstRedBerryFollower?.Entity is Strawberry firstRedBerry)
                    {
                        float collectTimer = (float)driver.TargetQuery
                            .GetOriginalField(firstRedBerry.GetType(), "collectTimer").Read(firstRedBerry);
                        if (collectTimer <= 0.15f)
                        {
                            int collectFrames = ToCeilingFrames(0.15f - collectTimer);
                            if (collectTimer >= 0f)
                            {
                                timers += $"Berry({FormatFrames(collectFrames)}) ";
                            }
                            else
                            {
                                int additionalFrames = ToCeilingFrames(Math.Abs(collectTimer));
                                timers +=
                                    $"Berry({collectFrames - additionalFrames}+{FormatFrames(additionalFrames)}) ";
                            }
                        }
                    }

                    if (dashCooldown > 0)
                    {
                        timers += $"DashCD({FormatFrames(dashCooldown)}) ";
                    }

                    if ((FramesPerGameSecond != 60 || process.SaveContext.CurrentSave.Assists.SuperDashing ||
                         driver.SourceExtendedVariants.SuperDashing) &&
                        ToCeilingFrames(DashTime) >= 1 && player.Machine.State == 2)
                    {
                        var dashCoroutine = (SimulationCoroutine)driver.TargetQuery
                            .GetOriginalField(player.Machine.GetType(), "currentCoroutine").Read(player.Machine);
                        DashTime = (float)driver.TargetQuery.GetOriginalField(dashCoroutine.GetType(), "waitTimer")
                            .Read(dashCoroutine);
                        timers += $"Dash({FormatFrames(ToCeilingFrames(DashTime))}) ";
                    }

                    if (player.Machine.State != 2)
                    {
                        DashTime = 0f;
                    }

                    StatusWithoutTime = GetStatusWithoutTime(
                        pos,
                        speed,
                        velocity,
                        player,
                        playerSeeker,
                        polarVel,
                        analog,
                        retainedSpeed,
                        liftBoost,
                        miscStats,
                        statuses,
                        timers
                    );

                    ExactStatusWithoutTime = GetStatusWithoutTime(
                        exactPos,
                        exactSpeed,
                        exactVelocity,
                        player,
                        playerSeeker,
                        exactPolarVel,
                        exactAnalog,
                        exactRetainedSpeed,
                        exactLiftBoost,
                        miscStats,
                        statuses,
                        timers
                    );

                    if (process.Effects.FreezeTimer <= 0f)
                    {
                        LastPos = player.GetMoreExactPosition(false);
                        LastPlayerSeekerPos = playerSeeker?.GetMoreExactPosition(false) ?? default;
                    }
                }
                else if (level.InCutscene)
                {
                    StatusWithoutTime = "Cutscene";
                }

                LevelName = level.Session.Level;
                ChapterTime = GetChapterTime(level);

                string timer = "";
                if (showGameTime && !showRealTime)
                {
                    timer = $"[{LevelName}] Timer: {ChapterTime}";
                }
                else if (!showGameTime && showRealTime)
                {
                    int realTimeFrames = driver.SourceMetadataCommands.RealTimeInfo!.Value.FrameCount;
                    timer =
                        $"[{LevelName}] Real Timer: {TimeSpan.FromSeconds(realTimeFrames / 60.0f).ShortGameplayFormat()}({realTimeFrames})";
                }
                else if (showGameTime && showRealTime)
                {
                    int realTimeFrames = driver.SourceMetadataCommands.RealTimeInfo!.Value.FrameCount;
                    timer =
                        $"[{LevelName}] Game Timer: {ChapterTime}\n{new string(' ', LevelName.Length + 3)}Real Timer: {TimeSpan.FromSeconds(realTimeFrames / 60.0f).ShortGameplayFormat()}({realTimeFrames})";
                }

                Status = StatusWithoutTime + timer;
                ExactStatus = ExactStatusWithoutTime + timer;
                UpdateAdditionInfo();
            }
            else if (scene is global::Celeste.Game.Minigames.CelesteClassic.Emulation.ClassicConsoleRuntime
                     {
                         Game: { } game
                     } emulator)
            {
                StringBuilder stringBuilder = new();
                global::Celeste.Game.Minigames.CelesteClassic.Gameplay.Pico8Classic.player? player =
                    emulator.Game.Objects.FirstOrDefault(o =>
                            o is global::Celeste.Game.Minigames.CelesteClassic.Gameplay.Pico8Classic.player) as
                        global::Celeste.Game.Minigames.CelesteClassic.Gameplay.Pico8Classic.player;
                if (player != null)
                {
                    stringBuilder.AppendLine($"Pos:   {player.x}, {player.y}");
                    stringBuilder.AppendLine($"Rem:   {player.rem.ToSimpleString(driver.Settings.PositionDecimals)}");
                    stringBuilder.AppendLine($"Speed: {player.spd.ToSimpleString(driver.Settings.SpeedDecimals)}");
                }

                stringBuilder.AppendLine($"Seed:  {driver.Pico8Fixer.Seed}");
                if (player?.grace > 1)
                {
                    stringBuilder.AppendLine($"Coyote({player.grace - 1})");
                }

                LevelName = $"[Level{game.level_index() + 1} X:{game.room.X} Y:{game.room.Y}]";
                ChapterTime =
                    $"{driver.TargetQuery.GetOriginalField(game.GetType(), "minutes").Read(game)}:{driver.TargetQuery.GetOriginalField(game.GetType(), "seconds").Read(game).ToString().PadLeft(2, '0')}{(driver.Pico8Fixer.Frames / 30.0).ToString($"F3").TrimStart('0')}";
                Status = ExactStatus = $"{stringBuilder}{LevelName} Timer: {ChapterTime}";
                UpdateAdditionInfo();
            }
            else
            {
                LevelName = string.Empty;
                ChapterTime = string.Empty;
                HudWatchingInfo = string.Empty;
                StudioWatchingInfo = string.Empty;
                CustomInfo = string.Empty;
                if (scene is global::Celeste.Game.Vignettes.SummitVignetteScene summit)
                {
                    Status = ExactStatus = $"SummitVignette {summit.IsReady}";
                }
                else if (scene is Overworld overworld)
                {
                    string ouiName = "";
                    if ((overworld.Current ?? overworld.Next) is { } oui)
                    {
                        ouiName = $"{driver.TargetQuery.GetOriginalTypeName(oui.GetType())} ";
                    }

                    Status = ExactStatus = ouiName;
                }
                else if (scene != null)
                {
                    Status = ExactStatus = driver.TargetQuery.GetOriginalTypeName(scene.GetType());
                }

                if (showRealTime)
                {
                    int realTimeFrames = driver.SourceMetadataCommands.RealTimeInfo!.Value.FrameCount;
                    Status +=
                        $"\n\nReal Timer: {TimeSpan.FromSeconds(realTimeFrames / 60.0f).ShortGameplayFormat()}({realTimeFrames})";
                }
            }
        }

        private void UpdateAdditionInfo()
        {
            WatchEntities.UpdateInfo();

            if (driver.Settings.InfoHud && (driver.Settings.InfoCustom & HudOptions.HudOnly) != 0 ||
                (driver.Settings.InfoCustom & HudOptions.StudioOnly) != 0 && driver.SourceStudioConnected)
            {
                CustomInfo = CustomInfoProcess.GetInfo();
            }
            else
            {
                CustomInfo = string.Empty;
            }
        }
    }
}
