using System.Numerics;
using StudioCommunication;
using Celeste.Core.Mathematics.Framework;
using Celeste.Everest.Settings.Metadata;

namespace Celeste.Mods.Tas.Module
{
    public partial class CelesteTasSettings
    {
        public int PositionDecimals
        {
            get => StudioShared.PositionDecimals;
            set
            {
                StudioShared.PositionDecimals =
                    SourceFrameworkMath.Clamp(value, GameSettings.MinDecimals, GameSettings.MaxDecimals);
                CelesteTasModule.Instance.Driver.GameInfo.Update();
                SyncSettings();
            }
        }

        public int SpeedDecimals
        {
            get => StudioShared.SpeedDecimals;
            set
            {
                StudioShared.SpeedDecimals =
                    SourceFrameworkMath.Clamp(value, GameSettings.MinDecimals, GameSettings.MaxDecimals);
                CelesteTasModule.Instance.Driver.GameInfo.Update();
                SyncSettings();
            }
        }

        public int VelocityDecimals
        {
            get => StudioShared.VelocityDecimals;
            set
            {
                StudioShared.VelocityDecimals =
                    SourceFrameworkMath.Clamp(value, GameSettings.MinDecimals, GameSettings.MaxDecimals);
                CelesteTasModule.Instance.Driver.GameInfo.Update();
                SyncSettings();
            }
        }

        public int AngleDecimals
        {
            get => StudioShared.AngleDecimals;
            set
            {
                StudioShared.AngleDecimals =
                    SourceFrameworkMath.Clamp(value, GameSettings.MinDecimals, GameSettings.MaxDecimals);
                CelesteTasModule.Instance.Driver.GameInfo.Update();
                SyncSettings();
            }
        }

        public int CustomInfoDecimals
        {
            get => StudioShared.CustomInfoDecimals;
            set
            {
                StudioShared.CustomInfoDecimals =
                    SourceFrameworkMath.Clamp(value, GameSettings.MinDecimals, GameSettings.MaxDecimals);
                CelesteTasModule.Instance.Driver.GameInfo.Update();
                SyncSettings();
            }
        }

        public int SubpixelIndicatorDecimals
        {
            get => StudioShared.SubpixelIndicatorDecimals;
            set
            {
                StudioShared.SubpixelIndicatorDecimals = SourceFrameworkMath.Clamp(value, 1, GameSettings.MaxDecimals);
                CelesteTasModule.Instance.Driver.GameInfo.Update();
                SyncSettings();
            }
        }

        public SpeedUnit SpeedUnit
        {
            get => StudioShared.SpeedUnit;
            set
            {
                StudioShared.SpeedUnit = value;
                CelesteTasModule.Instance.Driver.GameInfo.Update();
                SyncSettings();
            }
        }

        public SpeedUnit VelocityUnit
        {
            get => StudioShared.VelocityUnit;
            set
            {
                StudioShared.VelocityUnit = value;
                CelesteTasModule.Instance.Driver.GameInfo.Update();
                SyncSettings();
            }
        }
    }
}
