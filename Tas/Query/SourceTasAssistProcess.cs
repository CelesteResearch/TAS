using System;
using Celeste.Core.Scheduling;
using Celeste.Game.Controls.Source;
using Celeste.Game.Save.Data;
using Celeste.Scene.Tracking;
using Celeste.Simulation;
using Celeste.Simulation.Gameplay.Player.Presentation.Sprites;

namespace Celeste.Mods.Tas.Query
{
    public sealed class SourceTasAssistProcess
    {
        private readonly ISourceEngineContext engine;

        public SourceTasAssistProcess(ISourceEngineContext engine)
            => this.engine = engine ?? throw new ArgumentNullException(nameof(engine));

        public void ApplyAssists(FileAssists? oldAssists, FileAssists newAssists)
        {
            if (oldAssists?.MirrorMode != newAssists.MirrorMode)
                engine.Effects.TimeRateB = newAssists.GameSpeed / 10.0f;
            if (oldAssists?.MirrorMode != newAssists.MirrorMode)
            {
                var input = ((IOriginalGameInputProcessEnvironment)engine).InputSource;
                input.Feather.InvertedX = input.Aim.InvertedX = input.MoveX.Inverted = newAssists.MirrorMode;
            }

            var player = ((ISourceScene)engine.Frame.Scenes.CurrentScene).SourceScene.Entities
                .GetTrackedEntity<RoomPlayerSession>(typeof(global::Celeste.Simulation.RoomPlayerSession).FullName);

            if (player != null)
            {
                if (oldAssists?.PlayAsBadeline != newAssists.PlayAsBadeline)
                {
                    var mode = newAssists.PlayAsBadeline
                        ? PlayerSpriteMode.MadelineAsBadeline
                        : player.DefaultSpriteMode;
                    // Original Player.StIntroWakeUp is 15; its coroutine captures the existing sprite.
                    if (player.Machine.State != 15)
                    {
                        if (player.Active) player.ResetSpriteNextFrame(mode);
                        else player.ResetSprite(mode);
                    }
                }

                if (oldAssists?.DashMode != newAssists.DashMode)
                    player.Data.Dashes = Math.Min(player.Data.Dashes, player.Motor.BeforeState.MaxDashes);
            }
        }
    }
}
