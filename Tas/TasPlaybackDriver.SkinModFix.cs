using Celeste.Mods.Tas.DesyncFix;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasSkinModFixProcess sourceSkinModFix;

        internal SourceTasSkinModFixProcess SourceSkinModFix =>
            sourceSkinModFix ??= new SourceTasSkinModFixProcess(this);

        [SourceTasLoad]
        private static void LoadSourceSkinModFix() => CelesteTasModule.Instance.Driver.SourceSkinModFix.Load();

        [SourceTasUnload]
        private static void UnloadSourceSkinModFix() => CelesteTasModule.Instance.Driver.SourceSkinModFix.Unload();

        [SourceTasInitialize]
        private static void InitializeSourceSkinModFix() =>
            CelesteTasModule.Instance.Driver.SourceSkinModFix.Initialize();

        [SourceTasEnableRun]
        private static void ApplySourceSkinModFix() => CelesteTasModule.Instance.Driver.SourceSkinModFix.Apply();

        [SourceTasDisableRun]
        private static void RestoreSourceSkinModFix() => CelesteTasModule.Instance.Driver.SourceSkinModFix.Restore();
    }
}
