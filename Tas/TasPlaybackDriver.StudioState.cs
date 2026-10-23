using System.Linq;
using StudioCommunication;
using Celeste.Simulation;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;
using Emulator = Celeste.Game.Minigames.CelesteClassic.Emulation.ClassicConsoleRuntime;
using Classic = Celeste.Game.Minigames.CelesteClassic.Gameplay.Pico8Classic;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        public bool PreventSendStudioState;

        internal void SendSourceStudioState()
        {
            if (PreventSendStudioState)
                return;

            var previous = Controller.Previous;
            var scene = process.Frame.Scenes.CurrentScene;
            var state = new StudioState
            {
                CurrentLine = previous?.StudioLine ?? -1,
                CurrentLineSuffix =
                    $"{Controller.CurrentFrameInInput + (previous?.FrameOffset ?? 0)}{previous?.RepeatString ?? ""}",
                CurrentFrameInTas = Controller.CurrentFrameInTas,
                SaveStateLines = SourceSavestateManager.AllSavestates.Select(savedState => savedState.StudioLine)
                    .ToArray(),
                PlaybackRunning = CurrState == global::Celeste.Core.Diagnostics.Tas.SourceTasManagerState.Running,
                FileNeedsReload = Controller.NeedsReload,
                TotalFrames = Controller.Inputs.Count,
                GameInfo = GameInfo.StudioInfo,
                LevelName = GameInfo.LevelName,
                ChapterTime = GameInfo.ChapterTime,
                ShowSubpixelIndicator = Settings.InfoSubpixelIndicator && scene is Level or Emulator
            };

            if (scene is Level level &&
                level.SourceScene.Entities.GetTrackedEntity<RoomPlayerSession>(
                        typeof(global::Celeste.Simulation.RoomPlayerSession).FullName) is
                    { } player)
            {
                state.PlayerPosition = (player.Body.Position.X, player.Body.Position.Y);
                state.PlayerPositionRemainder = (player.Body.Remainder.X, player.Body.Remainder.Y);
                state.PlayerSpeed = (player.Data.Motion.Normal.Speed.X, player.Data.Motion.Normal.Speed.Y);
            }
            else if (scene is Emulator emulator &&
                     emulator.Game?.Objects.FirstOrDefault(entity => entity is Classic.player) is Classic.player
                         classicPlayer)
            {
                state.PlayerPosition = (classicPlayer.x, classicPlayer.y);
                state.PlayerPositionRemainder = (classicPlayer.rem.X, classicPlayer.rem.Y);
                state.PlayerSpeed = (classicPlayer.spd.X, classicPlayer.spd.Y);
            }

            SendSourceStudioState(state);
        }
    }
}
