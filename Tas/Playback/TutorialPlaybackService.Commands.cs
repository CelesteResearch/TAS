using Celeste.Game.Execution.Levels;
using Celeste.Simulation;

namespace Celeste.Mods.Tas.Playback
{
    public sealed partial class TutorialPlaybackService
    {
        private void ExecuteCommands(OriginalLevelSceneRoot level)
        {
            if (!cursor.Clip.Commands.TryGetValue(cursor.ElapsedFrames, out var commands)) return;
            foreach (var command in commands)
            {
                if (command.SkipLoad)
                {
                    for (int i = 0; i < 37; i++) cursor.Advance();
                    continue;
                }

                var player = level.Scheduler.Entities.GetTrackedEntity<RoomPlayerSession>();
                if (player == null) continue;
                if (command.Animation == null) player.Machine.State = 0;
                else
                {
                    player.Machine.State = 11;
                    player.Dummy.AutoAnimate = false;
                    player.Sprite.Play(command.Animation);
                }
            }
        }
    }
}
