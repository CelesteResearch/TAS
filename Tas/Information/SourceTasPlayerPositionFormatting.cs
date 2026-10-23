using Celeste.Simulation;

namespace Celeste.Mods.Tas.Information
{
    internal static class SourceTasPlayerPositionFormatting
    {
        public static Vector2Double GetMoreExactPosition(this RoomPlayerSession player, bool subpixelRounding)
        {
            return new(player.Body.Position, player.Body.Remainder, subpixelRounding);
        }

        public static string ToSimplePositionString(this RoomPlayerSession player, int decimals)
        {
            return player.GetMoreExactPosition(true).ToSimpleString(decimals);
        }
    }
}
