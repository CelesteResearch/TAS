using Celeste.Mods.Tas.Playback;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasSavestateManager sourceSavestateManager;
        private void UpdateSourceSavestates() => SourceSavestateManager.Update();
        private void UpdateSourceSavestateMeta() => SourceSavestateManager.UpdateMeta();

        internal SourceTasSavestateManager SourceSavestateManager =>
            sourceSavestateManager ??= new SourceTasSavestateManager(this);
    }
}
