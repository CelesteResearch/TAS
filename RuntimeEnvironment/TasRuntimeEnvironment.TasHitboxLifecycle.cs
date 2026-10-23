using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Interop;
using SavestateData = System.Collections.Generic.Dictionary<string, object?>;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment
    {
        [SourceTasLoadAttribute]
        private static void LoadSourceTasHitboxColor()
            => CelesteTasModule.Instance.Driver.SourceHitboxColor.Load();

        [SourceTasUnloadAttribute]
        private static void UnloadSourceTasHitboxColor()
            => CelesteTasModule.Instance.Driver.SourceHitboxColor.Unload();

        [SourceTasLoadAttribute]
        private static void LoadSourceTasHitboxRoomBoundary()
            => CelesteTasModule.Instance.Driver.SourceHitboxRoomBoundary.Load();

        [SourceTasUnloadAttribute]
        private static void UnloadSourceTasHitboxRoomBoundary()
            => CelesteTasModule.Instance.Driver.SourceHitboxRoomBoundary.Unload();

        [SourceTasInitializeAttribute]
        private static void InitializeSourceTasActualCollideHitbox()
            => CelesteTasModule.Instance.Driver.SourceActualCollideHitbox.Initialize();

        [SourceTasLoadAttribute]
        private static void LoadSourceTasActualCollideHitbox()
            => CelesteTasModule.Instance.Driver.SourceActualCollideHitbox.Load();

        [SourceTasUnloadAttribute]
        private static void UnloadSourceTasActualCollideHitbox()
            => CelesteTasModule.Instance.Driver.SourceActualCollideHitbox.Unload();

        [SourceTasInitializeAttribute]
        private static void InitializeSourceTasTriggerHitbox()
            => CelesteTasModule.Instance.Driver.SourceTriggerHitbox.Initialize();

        [SourceTasLoadAttribute]
        private static void LoadSourceTasTriggerHitbox()
            => CelesteTasModule.Instance.Driver.SourceTriggerHitbox.Load();

        [SourceTasUnloadAttribute]
        private static void UnloadSourceTasTriggerHitbox()
            => CelesteTasModule.Instance.Driver.SourceTriggerHitbox.Unload();

        [SourceTasInitializeAttribute]
        private static void InitializeSourceTasHitboxTriggerSpikes()
            => CelesteTasModule.Instance.Driver.SourceHitboxTriggerSpikes.Initialize();

        [SourceTasLoadAttribute]
        private static void LoadSourceTasHitboxTriggerSpikes()
            => CelesteTasModule.Instance.Driver.SourceHitboxTriggerSpikes.Load();

        [SourceTasUnloadAttribute]
        private static void UnloadSourceTasHitboxTriggerSpikes()
            => CelesteTasModule.Instance.Driver.SourceHitboxTriggerSpikes.Unload();

        [SourceTasLoadStateAttribute]
        private static void LoadStateSourceTasTriggerHitbox(SavestateData savedData)
            => CelesteTasModule.Instance.Driver.SourceTriggerHitbox.LoadState();

        private static void CmdChangeEntityHitboxColor(string color)
            => CelesteTasModule.Instance.Driver.SourceHitboxColor.CmdChangeEntityHitboxColor(color);

        private static void CmdChangeTriggerHitboxColor(string color)
            => CelesteTasModule.Instance.Driver.SourceHitboxColor.CmdChangeTriggerHitboxColor(color);

        private static void CmdChangePlatformHitboxColor(string color)
            => CelesteTasModule.Instance.Driver.SourceHitboxColor.CmdChangePlatformHitboxColor(color);
    }
}
