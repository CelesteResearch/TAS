using System;
using Celeste.Scene.Entities;
using Celeste.Simulation;

namespace Celeste.Mods.Tas.Information
{
    internal static class SourceTasObjectPositionFormatting
    {
        public static string ToSimplePositionString(this object actualEntity, int decimals)
        {
            if (actualEntity is RoomPlayerSession player)
            {
                return player.ToSimplePositionString(decimals);
            }
            else if (actualEntity is SceneEntityState entity)
            {
                return entity.ToSimplePositionString(decimals);
            }

            throw new InvalidOperationException("The actual entity has no qualified original position formatter: " +
                                                actualEntity.GetType().FullName);
        }
    }
}
