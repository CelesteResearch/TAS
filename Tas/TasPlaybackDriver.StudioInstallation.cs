namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private Information.SourceTasStudioUpdateBanner sourceStudioUpdateBanner;

        internal Information.SourceTasStudioUpdateBanner StudioUpdateBanner =>
            sourceStudioUpdateBanner ??= new Information.SourceTasStudioUpdateBanner(this);

        private Tools.SourceTasStudioHelper sourceStudioHelper;

        internal Tools.SourceTasStudioHelper StudioHelper =>
            sourceStudioHelper ??= new Tools.SourceTasStudioHelper(this);
    }
}
