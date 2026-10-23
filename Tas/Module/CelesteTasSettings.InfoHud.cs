using System.Numerics;
using StudioCommunication;
using Celeste.Core.Mathematics.Framework;
using Celeste.Everest.Settings.Metadata;

namespace Celeste.Mods.Tas.Module
{
    public partial class CelesteTasSettings
    {
        public bool InfoGame
        {
            get => StudioShared.InfoGame;
            set
            {
                StudioShared.InfoGame = value;
                SyncSettings();
            }
        }

        public bool InfoTasInput
        {
            get => StudioShared.InfoTasInput;
            set
            {
                StudioShared.InfoTasInput = value;
                SyncSettings();
            }
        }

        public bool InfoSubpixelIndicator
        {
            get => StudioShared.InfoSubpixelIndicator;
            set
            {
                StudioShared.InfoSubpixelIndicator = value;
                SyncSettings();
            }
        }

        public HudOptions InfoCustom
        {
            get => StudioShared.InfoCustom;
            set
            {
                StudioShared.InfoCustom = value;
                SyncSettings();
            }
        }

        internal bool WatchEntity => HudWatchEntity || StudioWatchEntity;
        internal bool HudWatchEntity => InfoWatchEntityHudType != WatchEntityType.None;
        internal bool StudioWatchEntity => InfoWatchEntityStudioType != WatchEntityType.None;

        public WatchEntityType InfoWatchEntityHudType
        {
            get => StudioShared.InfoWatchEntityHudType;
            set
            {
                StudioShared.InfoWatchEntityHudType = value;
                SyncSettings();
            }
        }

        public WatchEntityType InfoWatchEntityStudioType
        {
            get => StudioShared.InfoWatchEntityStudioType;
            set
            {
                StudioShared.InfoWatchEntityStudioType = value;
                SyncSettings();
            }
        }

        public bool InfoWatchEntityLogToConsole { get; set; } = true;

        [SettingIgnore]
        public string InfoCustomTemplate { get; set; } =
            "Wind: {Level.Wind}\n" +
            "AutoJump: {Player.AutoJump} {Player.AutoJumpTimer.toFrame()}\n" +
            "Theo: {TheoCrystal.ExactPosition}\n" +
            "TheoCantGrab: {TheoCrystal.Hold.cannotHoldTimer.toFrame()}";

        [SettingIgnore] public Vector2 InfoPosition { get; set; } = Vector2.Zero;
        [SettingIgnore] public int InfoTextSize { get; set; } = 10;
        [SettingIgnore] public int InfoSubpixelIndicatorSize { get; set; } = 10;
        [SettingIgnore] public int InfoOpacity { get; set; } = 6;
        [SettingIgnore] public int InfoMaskedOpacity { get; set; } = 4;
    }
}
