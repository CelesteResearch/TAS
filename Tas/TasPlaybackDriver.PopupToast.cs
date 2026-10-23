using Celeste.Core.Display;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Mods.Tas.Module;
using Celeste.Graphics.Display;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private readonly SourceTasGameplayEvents gameplayEvents = new();
        private SourceTasPopupToast sourcePopupToast;
        private void InitializeSourcePopupToast() => sourcePopupToast = new SourceTasPopupToast(process, this);

        [SourceTasLoad]
        private static void LoadSourcePopupToast()
        {
            var driver = CelesteTasModule.Instance.Driver;
            driver.sourcePopupToast.Load(driver.gameplayEvents);
        }

        internal SourceTasPopupToast.Entry ShowSourcePopupToast(string message,
            float timeout = SourceTasPopupToast.DefaultDuration)
            => sourcePopupToast.Show(message, timeout);

        internal SourceTasPopupToast.Entry ShowSourcePopupToastWithColor(string message, PackedColor color,
            float timeout = SourceTasPopupToast.DefaultDuration)
            => sourcePopupToast.ShowWithColor(message, color, timeout);

        internal SourceTasPopupToast.Entry ShowSourcePopupToastAndLog(string message,
            float timeout = SourceTasPopupToast.DefaultDuration,
            global::Celeste.Everest.EverestLogLevel level = global::Celeste.Everest.EverestLogLevel.Warn)
            => sourcePopupToast.ShowAndLog(message, timeout, level);

        internal void OnSourcePostRender() => gameplayEvents.InvokePostRender();
    }
}
