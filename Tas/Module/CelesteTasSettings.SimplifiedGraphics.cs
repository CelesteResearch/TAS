using System.Linq;
using Celeste.Mods.Tas.Gameplay;
using Celeste.Mods.Tas.EverestInterop;

namespace Celeste.Mods.Tas.Module
{
    public partial class CelesteTasSettings
    {
        public int? SimplifiedLighting { get; set; } = 10;
        public int? SimplifiedBloomBase { get; set; } = 0;
        public int? SimplifiedBloomStrength { get; set; } = 1;

        private SpinnerColor simplifiedSpinnerColor = SpinnerColor.White;

        public SpinnerColor SimplifiedSpinnerColor
        {
            get => simplifiedSpinnerColor;
            set
            {
                if (simplifiedSpinnerColor == value)
                {
                    return;
                }

                simplifiedSpinnerColor = value;
                CelesteTasModule.Instance.Driver.OnSourceSpinnerColorChanged();
            }
        }

        public bool SimplifiedDustSpriteEdge { get; set; } = true;
        public bool SimplifiedScreenWipe { get; set; } = true;
        public bool SimplifiedColorGrade { get; set; } = true;

        private SourceTasSimplifiedGraphicsFeature.SolidTilesStyle simplifiedSolidTilesStyle;

        public SourceTasSimplifiedGraphicsFeature.SolidTilesStyle SimplifiedSolidTilesStyle
        {
            get => simplifiedSolidTilesStyle;
            set
            {
                if (simplifiedSolidTilesStyle != value &&
                    SourceTasSimplifiedGraphicsFeature.SolidTilesStyle.All.Any(style => style.Value == value.Value))
                {
                    simplifiedSolidTilesStyle = value;
                    if (SimplifiedGraphics)
                    {
                        CelesteTasModule.Instance.Driver.ReplaceSourceSolidTilesStyle();
                    }
                }
            }
        }

        public bool SimplifiedBackgroundTiles { get; set; } = false;
        public bool SimplifiedBackdrop { get; set; } = true;
        public bool SimplifiedParticle { get; set; } = true;
        public bool SimplifiedDistort { get; set; } = true;
        public bool SimplifiedMiniTextbox { get; set; } = true;
        public bool SimplifiedLightningStrike { get; set; } = true;
        public bool SimplifiedClutteredEntity { get; set; } = true;
        public bool SimplifiedHud { get; set; } = true;
        public bool SimplifiedWavedEdge { get; set; } = true;
        public bool SimplifiedSpikes { get; set; } = true;
    }
}
