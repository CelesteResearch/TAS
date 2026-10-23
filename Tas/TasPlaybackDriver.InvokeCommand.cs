using Celeste.Mods.Tas.Commands;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasInvokeProcess sourceInvokeCommand;
        private SourceTasAssertProcess sourceAssertCommand;
        internal SourceTasAssertProcess SourceAssertCommand => sourceAssertCommand ??= new SourceTasAssertProcess(this);

        internal SourceTasInvokeProcess SourceInvokeCommand
            => sourceInvokeCommand ??= new SourceTasInvokeProcess(this);
    }
}
