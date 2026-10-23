using Celeste.Mods.Tas.Hitboxes.ModInterop;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasMelvinHitbox sourceMelvinHitbox;
        internal SourceTasMelvinHitbox SourceMelvinHitbox => sourceMelvinHitbox ??= new SourceTasMelvinHitbox(this);
    }
}
