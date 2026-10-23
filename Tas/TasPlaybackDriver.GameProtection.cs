using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Tools;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasAutoMute sourceAutoMute;
        internal SourceTasAutoMute SourceAutoMute => sourceAutoMute ??= new(process, this);
        private SourceTasSaveAndQuitReenterProcess sourceSaveAndQuitReenter;

        internal SourceTasSaveAndQuitReenterProcess SourceSaveAndQuitReenter =>
            sourceSaveAndQuitReenter ??= new(process, this);
    }
}
