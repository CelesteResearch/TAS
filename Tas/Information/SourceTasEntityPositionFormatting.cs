using Celeste.Scene.Entities;
using Celeste.Simulation.Gameplay.Actors.Foundation;
using Celeste.Simulation.Gameplay.Platforms.Foundation;

namespace Celeste.Mods.Tas.Information
{
    internal static class SourceTasEntityPositionFormatting
    {
        public static string ToSimplePositionString(this SceneEntityState entity, int decimals)
        {
            if (entity is SourceActorEntityState actor)
            {
                return actor.GetMoreExactPosition(true).ToSimpleString(decimals);
            }
            else if (entity is SourcePlatformEntityState platform)
            {
                return platform.GetMoreExactPosition(true).ToSimpleString(decimals);
            }
            else
            {
                return entity.Position.ToSimpleString(decimals);
            }
        }

        public static Vector2Double GetMoreExactPosition(this SourceActorEntityState actor, bool subpixelRounding)
        {
            return new(actor.Position, actor.Body.Remainder, subpixelRounding);
        }

        public static Vector2Double GetMoreExactPosition(this SourcePlatformEntityState platform, bool subpixelRounding)
        {
            return new(platform.Position, platform.MovementCounter, subpixelRounding);
        }
    }
}
