using Celeste.Mods.Tas.Gameplay.DesyncFix;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasSeededRandomness sourceSeededRandomness;
        internal SourceTasSeededRandomness SourceSeededRandomness => sourceSeededRandomness;

        private void InitializeSourceSeededRandomness()
            => sourceSeededRandomness = new SourceTasSeededRandomness(process, this);
    }
}
