using Celeste.Mods.Tas.Hitboxes;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        internal SourceTasGameplayEvents SourceGameplayEvents => gameplayEvents;
        private SourceTasCameraHitbox sourceCameraHitbox;
        internal SourceTasCameraHitbox SourceCameraHitbox => sourceCameraHitbox ??= new SourceTasCameraHitbox(this);

        [SourceTasLoad]
        private static void LoadSourceCameraHitbox() => CelesteTasModule.Instance.Driver.SourceCameraHitbox.Load();

        private SourceTasHitboxFixer sourceHitboxFixer;
        internal SourceTasHitboxFixer SourceHitboxFixer => sourceHitboxFixer ??= new SourceTasHitboxFixer(this);

        [SourceTasLoad]
        private static void LoadSourceHitboxFixer() => CelesteTasModule.Instance.Driver.SourceHitboxFixer.Load();

        [SourceTasUnload]
        private static void UnloadSourceHitboxFixer() => CelesteTasModule.Instance.Driver.SourceHitboxFixer.Unload();

        private SourceTasOffscreenHitbox sourceOffscreenHitbox;

        internal SourceTasOffscreenHitbox SourceOffscreenHitbox =>
            sourceOffscreenHitbox ??= new SourceTasOffscreenHitbox(this);

        [SourceTasLoad]
        private static void LoadSourceOffscreenHitbox() =>
            CelesteTasModule.Instance.Driver.SourceOffscreenHitbox.Load();

        private SourceTasHitboxOptimized sourceHitboxOptimized;

        internal SourceTasHitboxOptimized SourceHitboxOptimized =>
            sourceHitboxOptimized ??= new SourceTasHitboxOptimized(this);

        [SourceTasInitialize]
        private static void InitializeSourceHitboxOptimized() =>
            CelesteTasModule.Instance.Driver.SourceHitboxOptimized.Initialize();

        [SourceTasLoad]
        private static void LoadSourceHitboxOptimized() =>
            CelesteTasModule.Instance.Driver.SourceHitboxOptimized.Load();

        [SourceTasUnload]
        private static void UnloadSourceHitboxOptimized() =>
            CelesteTasModule.Instance.Driver.SourceHitboxOptimized.Unload();
    }
}
