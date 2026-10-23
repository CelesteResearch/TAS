using System.Numerics;
using StudioCommunication;
using Celeste.Core.Mathematics.Framework;
using Celeste.Everest.Settings.Metadata;

namespace Celeste.Mods.Tas.Module
{
    public partial class CelesteTasSettings
    {
        public bool CenterCameraHorizontallyOnly
        {
            get => StudioShared.CenterCameraHorizontallyOnly;
            set
            {
                StudioShared.CenterCameraHorizontallyOnly = value;
                SyncSettings();
            }
        }

        public bool EnableExCameraDynamicsForCenterCamera
        {
            get => CelesteTasModule.Instance.Driver.SourceExCameraInstalled &&
                   StudioShared.EnableExCameraDynamicsForCenterCamera;
            set
            {
                StudioShared.EnableExCameraDynamicsForCenterCamera = value;
                SyncSettings();
            }
        }
    }
}
