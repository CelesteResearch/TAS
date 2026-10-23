using Celeste.Mods.Tas.Input;

namespace Celeste.Mods.Tas.Playback
{
    internal sealed class TutorialPlaybackCursor
    {
        public readonly TutorialPlaybackClip Clip;
        public int ElapsedFrames { get; private set; }
        private int inputIndex;
        private int frameIndex;

        public TutorialPlaybackCursor(TutorialPlaybackClip clip)
        {
            Clip = clip;
        }

        public SourceTasInputFrame Current => Clip.Inputs[inputIndex];
        public bool Finished => ElapsedFrames >= Clip.TotalFrames;

        public void Advance()
        {
            if (Finished) return;
            frameIndex++;
            ElapsedFrames++;
            if (frameIndex < Current.Frames) return;
            inputIndex++;
            frameIndex = 0;
        }
    }
}
