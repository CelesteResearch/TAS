using Celeste.Mods.Tas.Gameplay;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.EverestInterop;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        internal SourceTasCenterCamera SourceCenterCamera => sourceCenterCamera;
        private SourceTasSimplifiedGraphics sourceSimplifiedGraphics;
        private SourceTasSimplifiedGraphicsFeature sourceSimplifiedFeature;

        internal SourceTasSimplifiedGraphics SourceSimplifiedGraphics =>
            sourceSimplifiedGraphics ??= new SourceTasSimplifiedGraphics(this);

        internal SourceTasSimplifiedGraphicsFeature SourceSimplifiedGraphicsFeature =>
            sourceSimplifiedFeature ??= new SourceTasSimplifiedGraphicsFeature(this);

        internal void OnSourceSpinnerColorChanged() => SourceSimplifiedGraphics.OnSpinnerColorChanged();
        internal void ReplaceSourceSolidTilesStyle() => SourceSimplifiedGraphicsFeature.ReplaceSolidTilesStyle();

        [SourceTasInitialize]
        private static void InitializeSourceSimplifiedGraphics() =>
            CelesteTasModule.Instance.Driver.SourceSimplifiedGraphics.Initialize();

        [SourceTasInitialize]
        private static void InitializeSourceSimplifiedGraphicsFeature() =>
            CelesteTasModule.Instance.Driver.SourceSimplifiedGraphicsFeature.Initialize();

        [SourceTasUnload]
        private static void UnloadSourceSimplifiedGraphicsFeature() =>
            CelesteTasModule.Instance.Driver.SourceSimplifiedGraphicsFeature.Unload();
    }
}
