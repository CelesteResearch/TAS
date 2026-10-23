using Celeste.Core.Display;
using YamlDotNet.Serialization;
using Celeste.Graphics.Display;
using Celeste.Mods.Tas.Gameplay.Hitboxes.Rooms;

namespace Celeste.Mods.Tas.Module
{
    public partial class CelesteTasSettings : ITasUnloadedRoomHitboxSettings
    {
        [YamlIgnore]
        public bool ShowHitboxes
        {
            get => Enabled && _ShowHitboxes || !ShowGameplay;
            set => _ShowHitboxes = value;
        }

        [YamlMember(Alias = "ShowHitboxes")]
        public bool _ShowHitboxes
        {
            get => StudioShared.Hitboxes;
            set
            {
                StudioShared.Hitboxes = value;
                SyncSettings();
            }
        }

        public bool ShowTriggerHitboxes
        {
            get => StudioShared.TriggerHitboxes;
            set
            {
                StudioShared.TriggerHitboxes = value;
                SyncSettings();
            }
        }

        public bool ShowUnloadedRoomsHitboxes
        {
            get => StudioShared.UnloadedRoomsHitboxes;
            set
            {
                StudioShared.UnloadedRoomsHitboxes = value;
                SyncSettings();
            }
        }

        public bool ShowCameraHitboxes
        {
            get => StudioShared.CameraHitboxes;
            set
            {
                StudioShared.CameraHitboxes = value;
                SyncSettings();
            }
        }

        [YamlMember(Alias = "SimplifiedGraphics")]
        public bool _SimplifiedGraphics
        {
            get => StudioShared.SimplifiedGraphics;
            set
            {
                StudioShared.SimplifiedGraphics = value;
                SyncSettings();
            }
        }

        [YamlIgnore]
        public bool SimplifiedGraphics
        {
            get => Enabled && _SimplifiedGraphics;
            set
            {
                if (_SimplifiedGraphics == value) return;
                _SimplifiedGraphics = value;
                CelesteTasModule.Instance.Driver.OnSourceSpinnerColorChanged();
            }
        }

        [YamlMember(Alias = "ShowGameplay")]
        public bool _ShowGameplay
        {
            get => StudioShared.Gameplay;
            set
            {
                StudioShared.Gameplay = value;
                SyncSettings();
            }
        }

        [YamlIgnore]
        public bool ShowGameplay
        {
            get => _ShowGameplay || !SimplifiedGraphics;
            set => _ShowGameplay = value;
        }

        [YamlMember(Alias = "CenterCamera")]
        public bool _CenterCamera
        {
            get => StudioShared.CenterCamera;
            set
            {
                StudioShared.CenterCamera = value;
                SyncSettings();
                CelesteTasModule.Instance.Driver.OnSourceCenterCameraToggled();
            }
        }

        [YamlIgnore]
        public bool CenterCamera
        {
            get => Enabled && _CenterCamera;
            set => _CenterCamera = value;
        }

        public bool EnableInfoHudFirstTime = true;

        public bool InfoHud
        {
            get => StudioShared.InfoHud;
            set
            {
                StudioShared.InfoHud = value;
                SyncSettings();
            }
        }

        public bool SimplifiedDecal { get; set; } = true;
        public int UnCollidableHitboxesOpacity { get; set; } = 5;
        public PackedColor EntityHitboxColor { get; set; } = PackedColor.Red;
        public PackedColor TriggerHitboxColor { get; set; } = PackedColor.MediumPurple;
        public PackedColor PlatformHitboxColor { get; set; } = PackedColor.Coral;
    }
}
