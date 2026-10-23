using Celeste.Mods.Tas.Hud;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasJetBrainsMonoFont sourceHudFont;
        private SourceTasInfoHud sourceInfoHud;
        public SourceTasInfoHud Hud => sourceInfoHud;
        internal SourceTasJetBrainsMonoFont HudFont => sourceHudFont;

        private void InitializeSourceHud()
        {
            sourceHudFont = new SourceTasJetBrainsMonoFont(process);
            sourceInfoHud = new SourceTasInfoHud(process, this, sourceHudFont);
        }
    }
}
