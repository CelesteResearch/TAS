using Celeste.Core.Display;
using Celeste.Graphics.Display;

namespace Celeste.Mods.Tas.Gameplay.Hitboxes.Rooms
{
    public interface ITasUnloadedRoomHitboxSettings
    {
        bool Enabled { get; }
        bool CenterCamera { get; }
        bool ShowUnloadedRoomsHitboxes { get; }
        bool ShowTriggerHitboxes { get; }
        bool SimplifiedGraphics { get; }
        bool SimplifiedDecal { get; }
        PackedColor EntityHitboxColor { get; }
        PackedColor TriggerHitboxColor { get; }
        PackedColor PlatformHitboxColor { get; }
    }
}
