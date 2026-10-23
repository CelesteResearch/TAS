using Celeste.Mods.Tas.Interop;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasHelperInterop sourceTasHelper;
        internal SourceTasHelperInterop SourceTasHelper => sourceTasHelper ??= new SourceTasHelperInterop(this);
    }
}
