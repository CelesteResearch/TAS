using Celeste.Mods.Tas.Interop;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasRestoreSettingsProcess sourceRestoreSettings;

        internal SourceTasRestoreSettingsProcess SourceRestoreSettings =>
            sourceRestoreSettings ??= new SourceTasRestoreSettingsProcess(this);
    }
}
