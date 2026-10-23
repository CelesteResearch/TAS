#nullable enable
using Celeste.Mods.Tas.Diagnostics;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasLoggingProcess? sourceLogging;
        internal SourceTasLoggingProcess Logging => sourceLogging ??= new SourceTasLoggingProcess(process, this);
    }
}
