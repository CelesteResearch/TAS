using Celeste.Mods.Tas.Interop;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasSpeedrunToolInterop sourceSpeedrunTool;

        internal SourceTasSpeedrunToolInterop SourceSpeedrunTool =>
            sourceSpeedrunTool ??= new SourceTasSpeedrunToolInterop(this);
    }
}
