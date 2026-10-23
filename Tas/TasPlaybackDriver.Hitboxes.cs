using Celeste.Mods.Tas.Hitboxes;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasActualCollideHitbox sourceActualCollideHitbox;
        private SourceTasHitboxColor sourceHitboxColor;
        private SourceTasTriggerHitbox sourceTriggerHitbox;
        private SourceTasHitboxRoomBoundary sourceHitboxRoomBoundary;
        private SourceTasHitboxTriggerSpikes sourceHitboxTriggerSpikes;

        public SourceTasActualCollideHitbox SourceActualCollideHitbox
            => sourceActualCollideHitbox ??= new SourceTasActualCollideHitbox(this);

        public SourceTasHitboxColor SourceHitboxColor => sourceHitboxColor ??= new SourceTasHitboxColor(this);
        internal SourceTasTriggerHitbox SourceTriggerHitbox => sourceTriggerHitbox ??= new SourceTasTriggerHitbox(this);

        internal SourceTasHitboxRoomBoundary SourceHitboxRoomBoundary
            => sourceHitboxRoomBoundary ??= new SourceTasHitboxRoomBoundary(this);

        internal SourceTasHitboxTriggerSpikes SourceHitboxTriggerSpikes
            => sourceHitboxTriggerSpikes ??= new SourceTasHitboxTriggerSpikes(this);
    }
}
