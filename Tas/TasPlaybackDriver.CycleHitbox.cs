using Celeste.Mods.Tas.Hitboxes;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasCycleHitboxColorProcess sourceCycleHitboxColor;
        internal SourceTasCycleHitboxColorProcess SourceCycleHitboxColor => sourceCycleHitboxColor;

        private void InitializeSourceCycleHitboxColor()
        {
            sourceCycleHitboxColor = new SourceTasCycleHitboxColorProcess(this);
        }
    }
}
