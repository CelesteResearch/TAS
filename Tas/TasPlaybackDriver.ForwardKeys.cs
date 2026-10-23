namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        public bool HotkeysInitialized => hotkeys.Initialized;

        // Overrides the original held hotkeys. UpdateMeta selects the actual speed.
        public void SetForwardKeys(bool fastForward, bool slowForward)
        {
            hotkeys.FastForward.OverrideCheck = fastForward;
            hotkeys.SlowForward.OverrideCheck = slowForward;
        }
    }
}
