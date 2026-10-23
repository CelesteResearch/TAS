using Celeste.Mods.Tas.Information;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasAreaCompleteInfo sourceAreaCompleteInfo;

        internal SourceTasAreaCompleteInfo AreaCompleteInfo =>
            sourceAreaCompleteInfo ??= new SourceTasAreaCompleteInfo(process, this);
    }
}
