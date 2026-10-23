using MonoMod.Cil;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Reflection;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private delegate void SourceExCameraEnableOriginal();

        private bool SourceDisableZoomLevelVariant() => Settings.CenterCamera;

        [SourceTasModILHook("ExtendedVariantMode", "ExtendedVariants.Variants.ZoomLevel", "modZoom")]
        private void SourceCenterZoomLevelModZoom(ILCursor cursor) =>
            SourceTasCenterCameraModHooks.DisableZoom(cursor, SourceDisableZoomLevelVariant);

        [SourceTasModILHook("ExtendedVariantMode", "ExtendedVariants.Variants.ZoomLevel", "getScreenPosition")]
        private void SourceCenterZoomLevelScreenPosition(ILCursor cursor) =>
            SourceTasCenterCameraModHooks.DisableZoom(cursor, SourceDisableZoomLevelVariant);

        [SourceTasModOnHook("ExtendedCameraDynamics", "Celeste.Mod.ExCameraDynamics.Code.Hooks.CameraZoomHooks",
            "Hook")]
        private static void SourceCenterExCameraEnable(SourceExCameraEnableOriginal original)
        {
            CelesteTasModule.Instance.Driver.sourceCenterCamera.MarkExCameraDynamicsMapEnabled();
            original();
        }
    }
}
