namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private Tools.SourceTasPlayAtLaunch sourcePlayTasAtLaunch;

        internal Tools.SourceTasPlayAtLaunch PlayTasAtLaunch =>
            sourcePlayTasAtLaunch ??= new Tools.SourceTasPlayAtLaunch(this);
    }
}
