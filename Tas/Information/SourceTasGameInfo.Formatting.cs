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
using LevelData = Celeste.Game.Content.Maps.Levels.OriginalLevelRoomData;
using Overworld = Celeste.Game.Menus.Overworld.OverworldSequence;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasGameInfo
    {
        internal int ToCeilingFrames(float timer)
        {
            if (timer <= 0.0f)
            {
                return 0;
            }

            float frames = MathF.Ceiling(timer / process.DeltaTime);
            return float.IsInfinity(frames) || float.IsNaN(frames) ? int.MaxValue : (int)frames;
        }

        public int ToFloorFrames(float timer)
        {
            if (timer <= 0.0f)
            {
                return 0;
            }

            float frames = MathF.Floor(timer / process.DeltaTime);
            return float.IsInfinity(frames) || float.IsNaN(frames) ? int.MaxValue : (int)frames;
        }

        internal string FormatFrames(int frames)
        {
            return frames == int.MaxValue ? "\u221e" : frames.ToString();
        }

        private string GetAdjustedPos(Actor actor, out string exactPos)
        {
            const string prefix = "Pos:   ";
            exactPos = $"{prefix}{actor.ToSimplePositionString(GameSettings.MaxDecimals)}";
            return $"{prefix}{actor.ToSimplePositionString(driver.Settings.PositionDecimals)}";
        }

        private string GetAdjustedSpeed(Vector2 speed, out string exactSpeed)
        {
            speed = ConvertSpeedUnit(speed, driver.Settings.SpeedUnit);
            exactSpeed = $"Speed: {speed.ToSimpleString(GameSettings.MaxDecimals)}";
            return $"Speed: {speed.ToSimpleString(driver.Settings.SpeedDecimals)}";
        }

        public Vector2 ConvertSpeedUnit(Vector2 speed, SpeedUnit speedUnit)
        {
            if (speedUnit == SpeedUnit.PixelPerFrame)
            {
                speed /= FramesPerGameSecond;
            }
            else
            {
                speed *= process.Effects.TimeRateB;
            }

            return speed;
        }

        public float ConvertSpeedUnit(float speed, SpeedUnit speedUnit)
        {
            if (speedUnit == SpeedUnit.PixelPerFrame)
            {
                speed /= FramesPerGameSecond;
            }
            else
            {
                speed *= process.Effects.TimeRateB;
            }

            return speed;
        }

        private string GetAdjustedVelocity(Vector2Double diff, out string exactVelocity)
        {
            exactVelocity = $"Vel:   {diff.ToSimpleString(GameSettings.MaxDecimals)}";
            return $"Vel:   {diff.ToSimpleString(driver.Settings.VelocityDecimals)}";
        }

        private string GetAdjustedPolarVel(Vector2Double diff, out string exactPolarVel)
        {
            exactPolarVel =
                $"Fly:   {diff.Length().ToFormattedString(GameSettings.MaxDecimals)}, {diff.Angle().ToFormattedString(GameSettings.MaxDecimals)}°";
            return
                $"Fly:   {diff.Length().ToFormattedString(driver.Settings.VelocityDecimals)}, {diff.Angle().ToFormattedString(driver.Settings.AngleDecimals)}°";
        }

        private string GetAdjustedAnalog(Vector2 angleVector2, out string exactAnalog)
        {
            exactAnalog =
                $"Analog: {angleVector2.ToSimpleString(GameSettings.MaxDecimals)}, {GetAngle(new Vector2(angleVector2.X, -angleVector2.Y)).ToFormattedString(GameSettings.MaxDecimals)}°";
            return
                $"Analog: {angleVector2.ToSimpleString(driver.Settings.AngleDecimals)}, {GetAngle(new Vector2(angleVector2.X, -angleVector2.Y)).ToFormattedString(driver.Settings.AngleDecimals)}°";
        }

        private string GetAdjustedRetainedSpeed(Player player, out string exactRetainedSpeed)
        {
            if (player.Data.WallSpeedRetentionTimer > 0f)
            {
                int timer = ToCeilingFrames(player.Data.WallSpeedRetentionTimer);
                float retainedSpeed = ConvertSpeedUnit(player.Data.WallSpeedRetained, driver.Settings.SpeedUnit);
                exactRetainedSpeed =
                    $"Retained({FormatFrames(timer)}): {retainedSpeed.ToString($"F{GameSettings.MaxDecimals}")}";
                return
                    $"Retained({FormatFrames(timer)}): {retainedSpeed.ToString($"F{driver.Settings.SpeedDecimals}")}";
            }
            else
            {
                return exactRetainedSpeed = string.Empty;
            }
        }

        public string GetAdjustedLiftBoost(Player player, out string exactLiftBoost)
        {
            if (global::Celeste.Simulation.Gameplay.Player.Movement.JumpMotion.LiftBoost(player.LiftSpeed) is var
                    liftBoost && liftBoost != Vector2.Zero)
            {
                liftBoost = ConvertSpeedUnit(liftBoost, driver.Settings.SpeedUnit);
                int timer = ToCeilingFrames(player.Body.LiftSpeedTimer);
                exactLiftBoost =
                    $"LiftBoost({FormatFrames(timer)}): {liftBoost.ToSimpleString(GameSettings.MaxDecimals)}";
                return $"LiftBoost({FormatFrames(timer)}): {liftBoost.ToSimpleString(driver.Settings.SpeedDecimals)}";
            }
            else
            {
                return exactLiftBoost = string.Empty;
            }
        }

        public string GetChapterTime(Level level)
        {
            return FormatTime(level.Session.Time);
        }

        public string FormatTime(long time)
        {
            TimeSpan timeSpan = TimeSpan.FromTicks(time);
            return
                $"{(global::Celeste.Simulation.Presentation.Hud.Speedrun.SpeedrunTimerState.ShortGameplayFormat(time))}({ConvertMicroSecondToFrames(time)})";
        }

        private long ConvertMicroSecondToFrames(long time)
        {
            return time / process.RawDeltaTime.SecondsToTicks();
        }

        private double GetAngle(Vector2 vector)
        {
            double angle = 180 / Math.PI * Math.Atan2(vector.Y, vector.X);
            if (angle < -90.01f)
            {
                return 450 + angle;
            }
            else
            {
                return 90 + angle;
            }
        }
    }
}
