using System.Linq;
using System.Numerics;
using Celeste.Game.Entities.StarJump;
using Celeste.Game.Execution.Levels;
using Celeste.Simulation;
using Celeste.Game.Entities.StarJump.Cutscenes;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed partial class SourceTasConsoleProcess
    {
        internal bool FixCh6FirstRoomLoad()
        {
            Vector2 startPoint = new(-176, 312);
            var session = driver.GameInfo.GetSourceSceneSession();
            bool skip = driver.Enabled && (session.GetFlag("campfire_chat") || session.RespawnPoint != startPoint);
            if (skip && process.Frame.Scenes.CurrentScene is OriginalLevelSceneRoot level &&
                level.Scheduler.Entities.GetTrackedEntity<RoomPlayerSession>(
                    typeof(global::Celeste.Simulation.RoomPlayerSession).FullName) is { } player &&
                level.SourceScene.Entities.OfType<OriginalTheoPlateauState>().FirstOrDefault() is { } theo &&
                level.Scheduler.Entities.GetTrackedEntity<OriginalBonfireState>("Celeste.Bonfire") is { } bonfire)
            {
                session.SetFlag("campfire_chat");
                level.Session.BloomBaseAdd = 1f;
                level.Bloom.Base = process.AreaKeys.GetArea(process.AreaKeys.GetSessionArea(session)).BloomBase + 1f;
                level.Session.Dreaming = true;
                level.Scheduler.QueueSourceEntity(new OriginalStarJumpControllerState(process));
                level.Scheduler.QueueSourceEntity(new CS06StarJumpEndState(theo, player, new Vector2(-4, 312),
                    new Vector2(-184, 177.6818f), process));
                level.Scheduler.QueueSourceEntity(new OriginalFlyFeatherState(new Vector2(88, 256), shielded: false,
                    singleUse: false, process));
                bonfire.Activated = false;
                bonfire.SetMode(OriginalBonfireState.Mode.Lit);
                theo.Position = new Vector2(-40, 312);
                theo.Sprite.Play("sleep");
                theo.Sprite.SetAnimationFrame(theo.Sprite.CurrentAnimationTotalFrames - 1);
                if (level.Session.RespawnPoint == startPoint)
                {
                    player.Body.Position = new Vector2(-4, 312);
                    player.Data.Facing = -1;
                }
            }

            return skip;
        }
    }
}
