namespace Celeste.Mods.Tas.Playback
{
    internal readonly struct TutorialPlaybackCommand
    {
        public readonly string Animation;
        public readonly bool SkipLoad;

        public TutorialPlaybackCommand(string animation, bool skipLoad = false)
        {
            Animation = animation;
            SkipLoad = skipLoad;
        }
    }
}
