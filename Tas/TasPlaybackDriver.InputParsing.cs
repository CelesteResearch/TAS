using Celeste.Core.Text;
using System;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Gameplay;
using Celeste.Mods.Tas.Input;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasAnalogProcess sourceAnalog;
        private SourceTasAutoInputProcess sourceAutoInputCommand;
        private SourceTasRepeatProcess sourceRepeatCommand;
        private SourceTasReadProcess sourceReadCommand;
        private SourceTasPlayProcess sourcePlayCommand;
        private SourceTasSafeProcess sourceSafeCommand;
        private SourceTasPressProcess sourcePressCommand;
        private SourceTasMouseProcess sourceMouseCommand;
        private SourceTasStunPauseProcess sourceStunPauseCommand;
        private SourceTasBetterInvincibleProcess sourceBetterInvincible;
        internal SourceTasAnalogProcess SourceAnalog => sourceAnalog;
        internal SourceTasAutoInputProcess SourceAutoInputCommand => sourceAutoInputCommand;
        internal SourceTasRepeatProcess SourceRepeatCommand => sourceRepeatCommand;
        internal SourceTasReadProcess SourceReadCommand => sourceReadCommand;
        internal SourceTasPlayProcess SourcePlayCommand => sourcePlayCommand;
        internal SourceTasSafeProcess SourceSafeCommand => sourceSafeCommand;
        internal SourceTasPressProcess SourcePressCommand => sourcePressCommand;
        internal SourceTasMouseProcess SourceMouseCommand => sourceMouseCommand;
        internal SourceTasStunPauseProcess SourceStunPauseCommand => sourceStunPauseCommand;
        internal SourceTasBetterInvincibleProcess SourceBetterInvincible => sourceBetterInvincible;

        private void InitializeSourceInputParsingProcesses()
        {
            sourceAnalog = new SourceTasAnalogProcess(this);
            sourceAutoInputCommand = new SourceTasAutoInputProcess(this);
            sourceRepeatCommand = new SourceTasRepeatProcess(this);
            sourceReadCommand = new SourceTasReadProcess(this);
            sourcePlayCommand = new SourceTasPlayProcess(this);
            sourceSafeCommand = new SourceTasSafeProcess(this);
            sourcePressCommand = new SourceTasPressProcess(this);
            sourceMouseCommand = new SourceTasMouseProcess(this);
            sourceStunPauseCommand = new SourceTasStunPauseProcess(this);
            sourceBetterInvincible = new SourceTasBetterInvincibleProcess(this);
        }

        internal SourceTasSourceLocation? ActiveCommandSource { get; set; }

        internal void AbortSourceTas(string message, bool log = false,
            float duration = SourceTasPopupToast.DefaultDuration)
        {
#if DEBUG
            log = true;
#endif
            string logMessage = ActiveCommandSource is { } source
                ? $"'{source.FilePath}' line {source.FileLine}: {message}"
                : message;
            foreach (var line in logMessage.AsSpan().EnumerateLines())
                LogSourceTas(global::Celeste.Everest.EverestLogLevel.Error, line.ToString());
            if (log)
                ShowSourcePopupToastWithColor(message, global::Celeste.Core.Display.PackedColor.Red, duration);
            else
                ShowSourcePopupToast(message, duration);
            DisableRunLater();
        }
    }
}
