using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Core.Scheduling;
using Celeste.Mods.Tas.Input;
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
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;

        internal SourceTasGameInfo(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        internal SourceTasPlayerStates PlayerStates { get; } = new();

        public string Status = string.Empty;
        public string StatusWithoutTime = string.Empty;
        public string ExactStatus = string.Empty;
        public string ExactStatusWithoutTime = string.Empty;
        public string LevelName = string.Empty;
        public string ChapterTime = string.Empty;
        public string HudWatchingInfo = string.Empty;
        public string StudioWatchingInfo = string.Empty;
        public string CustomInfo = string.Empty;
        public Vector2Double LastDiff;
        public Vector2Double LastPos;
        public Vector2Double LastPlayerSeekerDiff;
        public Vector2Double LastPlayerSeekerPos;
        public float DashTime;
        public bool Frozen;
        public int TransitionFrames;

        public string HudInfo
        {
            get
            {
                List<string> infos = new();
                if (driver.Settings.InfoGame && !string.IsNullOrWhiteSpace(Status))
                {
                    infos.Add(Status);
                }

                if (driver.InfoMouse.MouseInfo.IsNotEmpty())
                {
                    infos.Add(driver.InfoMouse.MouseInfo);
                }

                if ((driver.Settings.InfoCustom & HudOptions.HudOnly) != 0 && !string.IsNullOrWhiteSpace(CustomInfo))
                {
                    infos.Add(CustomInfo);
                }

                if (driver.Settings.HudWatchEntity && !string.IsNullOrWhiteSpace(HudWatchingInfo))
                {
                    infos.Add(HudWatchingInfo);
                }

                return string.Join("\n\n", infos);
            }
        }

        public string StudioInfo
        {
            get
            {
                List<string> infos = new() { Status };

                if (driver.InfoMouse.MouseInfo.IsNotEmpty())
                {
                    infos.Add(driver.InfoMouse.MouseInfo);
                }

                if ((driver.Settings.InfoCustom & HudOptions.StudioOnly) != 0 && !string.IsNullOrWhiteSpace(CustomInfo))
                {
                    infos.Add(CustomInfo);
                }

                if (driver.Settings.StudioWatchEntity && !string.IsNullOrWhiteSpace(StudioWatchingInfo))
                {
                    infos.Add(StudioWatchingInfo);
                }

                return string.Join("\n\n", infos);
            }
        }

        public string ExactStudioInfo
        {
            get
            {
                List<string> infos = new() { ExactStatus };

                if (driver.InfoMouse.MouseInfo.IsNotEmpty())
                {
                    infos.Add(driver.InfoMouse.MouseInfo);
                }

                StudioWatchingInfo = WatchEntities.GetInfo(driver.Settings.InfoWatchEntityStudioType,
                    alwaysUpdate: true,
                    decimals: GameSettings.MaxDecimals);
                CustomInfo = CustomInfoProcess.GetInfo(GameSettings.MaxDecimals);

                if (!string.IsNullOrWhiteSpace(CustomInfo))
                {
                    infos.Add(CustomInfo);
                }

                if (!string.IsNullOrWhiteSpace(StudioWatchingInfo))
                {
                    infos.Add(StudioWatchingInfo);
                }

                return string.Join("\n\n", infos);
            }
        }

        public string ExactStudioInfoAllowCodeExecution
        {
            get
            {
                List<string> infos = new() { ExactStatus };

                if (driver.InfoMouse.MouseInfo.IsNotEmpty())
                {
                    infos.Add(driver.InfoMouse.MouseInfo);
                }

                StudioWatchingInfo = WatchEntities.GetInfo(driver.Settings.InfoWatchEntityStudioType,
                    alwaysUpdate: true,
                    decimals: GameSettings.MaxDecimals);
                CustomInfo = CustomInfoProcess.GetInfo(GameSettings.MaxDecimals, forceAllowCodeExecution: true);

                if (!string.IsNullOrWhiteSpace(CustomInfo))
                {
                    infos.Add(CustomInfo);
                }

                if (!string.IsNullOrWhiteSpace(StudioWatchingInfo))
                {
                    infos.Add(StudioWatchingInfo);
                }

                return string.Join("\n\n", infos);
            }
        }

        private int FramesPerGameSecond => (int)Math.Round(1 / process.RawDeltaTime / process.Effects.TimeRateB);
        private int FramesPerRealSecond => (int)Math.Round(1 / process.RawDeltaTime);
    }
}
