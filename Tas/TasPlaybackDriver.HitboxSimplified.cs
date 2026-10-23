using Celeste.Mods.Tas.Hitboxes;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasHitboxSimplifiedProcess sourceHitboxSimplified;
        internal SourceTasHitboxSimplifiedProcess SourceHitboxSimplified => sourceHitboxSimplified;

        private void InitializeSourceHitboxSimplified() =>
            sourceHitboxSimplified = new SourceTasHitboxSimplifiedProcess(this);
    }
}
