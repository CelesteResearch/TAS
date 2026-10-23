using System;
using System.Numerics;
using Celeste.Core.Geometry;
using Celeste.Scene.Entities;
using Celeste.Simulation.Gameplay.Hazards.Spikes;
using Celeste.Simulation.Gameplay.Environment.Wind;
using StudioCommunication;

namespace Celeste.Mods.Tas.Information
{
    internal static class SourceTasGameStateExtensions
    {
        public static GameState.Vec2 ToGameStateVec2(this Vector2 vector) => new(vector.X, vector.Y);

        public static GameState.RectI ToGameStateRectI(this IntRect rectangle) =>
            new(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

        public static GameState.RectF ToGameStateRectF(this SceneEntityState entity) =>
            new(entity.X, entity.Y, entity.Width, entity.Height);

        public static GameState.Direction ToGameStateDirection(this SpikeDirection direction) => direction switch
        {
            SpikeDirection.Up => GameState.Direction.Up,
            SpikeDirection.Down => GameState.Direction.Down,
            SpikeDirection.Left => GameState.Direction.Left,
            SpikeDirection.Right => GameState.Direction.Right,
            _ => throw new InvalidOperationException("Unreachable spike direction.")
        };

        public static GameState.WindPattern ToGameStatePattern(this RoomWindPattern pattern) => pattern switch
        {
            RoomWindPattern.None => GameState.WindPattern.None,
            RoomWindPattern.Left => GameState.WindPattern.Left,
            RoomWindPattern.Right => GameState.WindPattern.Right,
            RoomWindPattern.LeftStrong => GameState.WindPattern.LeftStrong,
            RoomWindPattern.RightStrong => GameState.WindPattern.RightStrong,
            RoomWindPattern.LeftOnOff => GameState.WindPattern.LeftOnOff,
            RoomWindPattern.RightOnOff => GameState.WindPattern.RightOnOff,
            RoomWindPattern.LeftOnOffFast => GameState.WindPattern.LeftOnOffFast,
            RoomWindPattern.RightOnOffFast => GameState.WindPattern.RightOnOffFast,
            RoomWindPattern.Alternating => GameState.WindPattern.Alternating,
            RoomWindPattern.LeftGemsOnly => GameState.WindPattern.LeftGemsOnly,
            RoomWindPattern.RightCrazy => GameState.WindPattern.RightCrazy,
            RoomWindPattern.Down => GameState.WindPattern.Down,
            RoomWindPattern.Up => GameState.WindPattern.Up,
            RoomWindPattern.Space => GameState.WindPattern.Space,
            _ => throw new InvalidOperationException("Unreachable wind pattern.")
        };
    }
}
