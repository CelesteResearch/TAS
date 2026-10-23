using Celeste.Mods.Tas.Interop;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasExtendedVariantsInterop sourceExtendedVariants;

        internal SourceTasExtendedVariantsInterop SourceExtendedVariants =>
            sourceExtendedVariants ??= new SourceTasExtendedVariantsInterop(this);
    }
}
