using System;
using System.Xml;
using Celeste.Graphics.Text.BitmapFonts;
using Celeste.Mods.Tas.Information;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : ISourcePixelFontSizeObservationEnvironment
    {
        private Action<SourcePixelFontSize, XmlElement> sourceTasFontSizeAdded;

        internal void InstallSourceTasFontBaselineHook()
            => sourceTasFontSizeAdded = SourceTasCommonBaselineFont.AfterSourceFontSizeAdded;

        internal void UninstallSourceTasFontBaselineHook() => sourceTasFontSizeAdded = null;

        void ISourcePixelFontSizeObservationEnvironment.AfterSourceFontSizeAdded(SourcePixelFontSize size,
            XmlElement data)
            => sourceTasFontSizeAdded?.Invoke(size, data);
    }
}
