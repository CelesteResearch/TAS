using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceCelesteTasExports sourceExports;
        internal SourceCelesteTasExports SourceExports => sourceExports ??= new SourceCelesteTasExports(this);
    }
}
