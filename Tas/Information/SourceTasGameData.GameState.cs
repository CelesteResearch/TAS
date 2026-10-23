using System;
using System.Linq;
using System.Numerics;
using StudioCommunication;
using Celeste.Physics.Collision.Shapes;
using Celeste.Scene.Tracking;
using Celeste.Simulation;
using Celeste.Simulation.Gameplay.Hazards.Spikes;
using Celeste.Simulation.Gameplay.Environment.Wind;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasGameData
    {
        internal GameState? GetGameState()
        {
            if (process.Frame.Scenes.CurrentScene is not Level level)
                return null;

            var catalog = ((ISourceSceneCatalogEnvironment)process).SceneCatalog;
            var player =
                level.SourceScene.Entities.GetTrackedEntity<RoomPlayerSession>(
                    typeof(global::Celeste.Simulation.RoomPlayerSession).FullName);

            bool Is(object entity, string identity) =>
                driver.TargetQuery.IsSameOrSubclassOfOriginal(entity.GetType(), identity);

            string Name(object entity) => driver.TargetQuery.GetOriginalTypeName(entity.GetType());

            T Field<T>(object entity, string name) =>
                (T)(driver.TargetQuery.GetOriginalField(entity.GetType(), name, logFailure: false) ??
                    throw new MissingFieldException(entity.GetType().FullName, name)).Read(entity);

            T Property<T>(object entity, string name) =>
                (T)(driver.TargetQuery.GetOriginalProperty(entity.GetType(), name, logFailure: false) ??
                    throw new MissingMemberException(entity.GetType().FullName, name)).Read(entity);

            GameState.RectF Rectangle(object entity) => new(Property<float>(entity, "X"), Property<float>(entity, "Y"),
                Property<float>(entity, "Width"), Property<float>(entity, "Height"));

            return new GameState
            {
                DeltaTime = process.DeltaTime,
                Player = new GameState.PlayerState
                {
                    Position = player.Body.Position.ToGameStateVec2(),
                    PositionRemainder = player.Body.Remainder.ToGameStateVec2(),
                    Speed = player.Data.Motion.Normal.Speed.ToGameStateVec2(),
                    starFlySpeedLerp = player.StarFlySpeedLerp,
                    OnGround = player.Data.OnGround,
                    IsHolding = player.Holding.Holding != null,
                    JumpTimer = player.Data.Motion.Normal.VariableJumpTimer,
                    AutoJump = player.Data.Motion.AutoJump,
                    MaxFall = player.Data.Motion.Normal.MaxFall
                },
                Level = new GameState.LevelState
                {
                    Bounds = level.Bounds.ToGameStateRectI(),
                    WindDirection = level.Wind.ToGameStateVec2()
                },
                ChapterTime = driver.GameInfo.GetChapterTime(level),
                RoomName = level.Session.Level,
                PlayerStateName = driver.GameInfo.PlayerStates.GetCurrentStateName(player),
                SolidsData =
                    ((global::Celeste.Game.Content.Maps.Levels.OriginalLevelRoomData)level.Session.LevelData)
                    .Solids,
                StaticSolids = level.SourceScene.Entities
                    .Where(entity => Is(entity, "Celeste.Solid")
                                     && !(Is(entity, "Celeste.StarJumpBlock") && Field<bool>(entity, "sinks"))
                                     && Property<object>(entity, "Collider") is { } collider &&
                                     Is(collider, "Monocle.Hitbox")
                                     && Field<bool>(entity, "Collidable"))
                    .Select(Rectangle).ToArray(),
                Spinners = level.SourceScene.Entities
                    .Where(entity => Is(entity, "Celeste.CrystalStaticSpinner") ||
                                     Is(entity, "Celeste.DustStaticSpinner") || Name(entity) == "CustomSpinner")
                    .Select(entity => Field<Vector2>(entity, "Position").ToGameStateVec2()).ToArray(),
                Lightning = level.SourceScene.Entities.Where(entity => Is(entity, "Celeste.Lightning"))
                    .Select(Rectangle).ToArray(),
                Spikes = level.SourceScene.Entities.Where(entity => Is(entity, "Celeste.Spikes"))
                    .Select(entity => new GameState.Spike(Rectangle(entity),
                        Field<SpikeDirection>(entity, "Direction").ToGameStateDirection())).ToArray(),
                WindTriggers = level.SourceScene.Entities.GetTrackedEntities("Celeste.WindTrigger")
                    .Select(entity => new GameState.WindTrigger(Rectangle(entity),
                        Field<RoomWindPattern>(entity, "Pattern").ToGameStatePattern())).ToArray(),
                JumpThrus = level.SourceScene.Entities
                    .Where(entity =>
                        Is(entity, "Celeste.JumpthruPlatform") ||
                        Name(entity) is "SidewaysJumpThru" or "UpsideDownJumpThru")
                    .Select(entity =>
                    {
                        if (Is(entity, "Celeste.JumpthruPlatform"))
                            return new GameState.JumpThru(Rectangle(entity), GameState.Direction.Up, true);
                        if (Name(entity) == "SidewaysJumpThru")
                            return new GameState.JumpThru(Rectangle(entity), Field<bool>(entity, "AllowLeftToRight")
                                ? GameState.Direction.Right
                                : GameState.Direction.Left, Field<bool>(entity, "pushPlayer"));
                        if (Name(entity) == "UpsideDownJumpThru")
                            return new GameState.JumpThru(Rectangle(entity), GameState.Direction.Down,
                                Field<bool>(entity, "pushPlayer"));
                        throw new InvalidOperationException("Unreachable jump through type.");
                    }).ToArray()
            };
        }
    }
}
