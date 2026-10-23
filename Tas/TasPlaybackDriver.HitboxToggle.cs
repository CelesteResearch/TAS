using Celeste.Mods.Tas.Hitboxes;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasHitboxToggleProcess sourceHitboxToggle;
        internal SourceTasHitboxToggleProcess SourceHitboxToggle => sourceHitboxToggle;

        private void InitializeSourceHitboxToggle()
        {
            sourceHitboxToggle = new SourceTasHitboxToggleProcess(this);
        }
    }
}
