using System.Numerics;
using StudioCommunication.Util;

namespace Celeste.Mods.Tas.Information
{
    internal static class SourceTasVector2Extensions
    {
        public static string ToSimpleString(this Vector2 vector2, int decimals)
        {
            return $"{vector2.X.ToFormattedString(decimals)}, {vector2.Y.ToFormattedString(decimals)}";
        }
    }
}
