using Celeste.Core.Display;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Celeste.Physics.Collision;
using Celeste.Everest.Reflection;
using Celeste.Scene.Entities;
using Celeste.Simulation;
using Celeste.Graphics.Display;
using StudioCommunication;

namespace Celeste.Mods.Tas.Hitboxes
{
    public sealed partial class SourceTasHitboxColor
    {
        private readonly TasPlaybackDriver driver;
        public static readonly PackedColor DefaultEntityColor = PackedColor.Red;
        public static readonly PackedColor DefaultTriggerColor = PackedColor.MediumPurple;
        public static readonly PackedColor DefaultPlatformColor = PackedColor.Coral;
        public static readonly PackedColor RespawnTriggerColor = PackedColor.YellowGreen;
        public static readonly PackedColor CameraTriggerColor = PackedColor.DarkGoldenrod;
        public static readonly PackedColor PufferHeightCheckColor = PackedColor.WhiteSmoke;
        public static readonly PackedColor PufferPushRadiusColor = PackedColor.DarkRed;
        private static readonly Regex HexChar = new(@"^[0-9a-f]*$", RegexOptions.IgnoreCase);
        internal SourceTasHitboxColor(TasPlaybackDriver driver) => this.driver = driver;
        public PackedColor EntityColor => driver.Settings.EntityHitboxColor;
        public PackedColor TriggerColor => driver.Settings.TriggerHitboxColor;
        public PackedColor PlatformColor => driver.Settings.PlatformHitboxColor;
        public PackedColor EntityColorInversely => Invert(EntityColor);
        public PackedColor EntityColorInverselyLessAlpha => EntityColorInversely * .6f;
        public float UnCollidableAlpha => driver.Settings.UnCollidableHitboxesOpacity / 10f;

        private static PackedColor Invert(PackedColor color)
            => new PackedColor(255 - color.R, 255 - color.G, 255 - color.B, color.A);

        public static string ColorToHex(PackedColor color)
        {
            return
                $"#{color.A.ToString("X").PadLeft(2, '0')}" +
                $"{color.R.ToString("X").PadLeft(2, '0')}" +
                $"{color.G.ToString("X").PadLeft(2, '0')}" +
                $"{color.B.ToString("X").PadLeft(2, '0')}";
        }

        public static PackedColor HexToColor(string hex, PackedColor defaultColor)
        {
            if (string.IsNullOrWhiteSpace(hex))
            {
                return defaultColor;
            }

            hex = hex.Replace("#", "");
            if (!HexChar.IsMatch(hex))
            {
                return defaultColor;
            }

            // 123456789 => 12345678
            if (hex.Length > 8)
            {
                hex = hex.Substring(0, 8);
            }

            // 123 => 112233
            // 1234 => 11223344
            if (hex.Length == 3 || hex.Length == 4)
            {
                hex = hex.ToCharArray().Select(c => $"{c}{c}").Aggregate((s, s1) => s + s1);
            }

            // 123456 => FF123456
            hex = hex.PadLeft(8, 'F');

            try
            {
                long number = Convert.ToInt64(hex, 16);
                PackedColor color = default;
                color.A = (byte)(number >> 24);
                color.R = (byte)(number >> 16);
                color.G = (byte)(number >> 8);
                color.B = (byte)number;
                return color;
            }
            catch (FormatException)
            {
                return defaultColor;
            }
        }

        private bool IsSourceType(object entity, string identity)
        {
            var members = ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMembers;
            var type = members.GetActualSourceType(
                members.GetOriginalTypeDefinition(typeof(SceneEntityState)).AssemblyIdentity, identity);
            return type.IsInstanceOfType(entity);
        }

        private PackedColor GetCustomColor(PackedColor color, object entity)
        {
            if (!driver.Settings.ShowHitboxes || entity is RoomPlayerSession) return color;
            PackedColor customColor = IsSourceType(entity, "Celeste.Trigger")
                ? driver.SourceTriggerHitbox.GetHitboxColor(entity)
                : IsSourceType(entity, "Celeste.Platform")
                    ? driver.Settings.PlatformHitboxColor
                    : IsSourceType(entity, "Celeste.LookoutBlocker")
                        ? PackedColor.Green
                        : driver.Settings.EntityHitboxColor;
            if (driver.Settings.ShowActualCollideHitboxes != ActualCollideHitboxType.Off
                && driver.SourceActualCollideHitbox.LoadActualCollidable(entity) is { } actualCollidable)
            {
                if (!actualCollidable) customColor *= UnCollidableAlpha;
            }
            else if (!((ISourceEntityCollider)entity).Collidable) customColor *= UnCollidableAlpha;

            return customColor;
        }

        public PackedColor GetCustomColor(object entity) => GetCustomColor(PackedColor.Red, entity);
    }
}
