using System.Numerics;
using Celeste.Core.Maps;
using Celeste.Formats.Map.Entities.Structure;
using Celeste.Game.Gameplay.Collectibles.Strawberries;
using Celeste.Game.Execution.Levels;
using Celeste.Simulation;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed partial class SourceTasConsoleProcess
    {
        private static void CmdGiveBerry() => CelesteTasModule.Instance.Driver.ConsoleProcess.GiveBerry();

        private void GiveBerry()
        {
            if (process.Frame.Scenes.CurrentScene is OriginalLevelSceneRoot level
                && level.Scheduler.Entities.GetTrackedEntity<RoomPlayerSession>(
                    typeof(global::Celeste.Simulation.RoomPlayerSession).FullName) is { } player)
            {
                var data = new EntityData
                {
                    Position = player.Body.Position + new Vector2(0f, -16f),
                    ID = process.RandomProcess.Factory.CreateUnseeded().Next(),
                    Name = "strawberry"
                };
                var id = new MapEntityId(level.Session.Level, data.ID);
                var entity = new OriginalStrawberryState(data, Vector2.Zero, id, (IOriginalStrawberryProcess)process);
                level.SourceScene.QueueSourceEntity(entity);
            }
        }
    }
}
