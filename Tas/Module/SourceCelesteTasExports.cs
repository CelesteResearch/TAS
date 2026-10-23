using Celeste.Core.Display;
using Celeste.Core.Diagnostics.Tas;
using Celeste.Everest;
using System;
using System.Linq;
using System.Numerics;
using Celeste.Core.Reflection.Interop;
using Celeste.Graphics.Display;
using Celeste.Everest.Modules.Runtime;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Mods.Tas.Playback;

namespace Celeste.Mods.Tas.Module
{
    internal sealed class SourceCelesteTasExports
    {
        private readonly TasPlaybackDriver driver;
        internal SourceCelesteTasExports(TasPlaybackDriver driver) => this.driver = driver;

        [SourceTasLoad]
        private static void SourceLoad() => CelesteTasModule.Instance.Driver.SourceExports.Load();

        internal void Load()
        {
            ((ISourceModInteropEnvironment)driver.Process).ModInterop.ModInterop(typeof(SourceCelesteTasExports));
        }

        public bool IsTasActive() => driver.Running;
        public bool IsTasRunning() => driver.CurrState == SourceTasManagerState.Running;
        public bool IsTasRecording() => driver.Recorder.IsRecording;

        public void AddSettingsRestoreHandler(SourceEverestModule module,
            (Func<object> Backup, Action<object> Restore)? handler)
        {
            if (driver.SourceRestoreSettings.ignoredModules.Contains(module) ||
                driver.SourceRestoreSettings.customHandlers.ContainsKey(module))
            {
                ((IEverestLoggerProcessEnvironment)driver.Process).Logger.Log(EverestLogLevel.Warn, "CelesteTAS",
                    $"Tried to register a custom setting-restore handler for mod '{module.Metadata.Name}', while already having a handler registered");
                return;
            }

            if (handler == null)
            {
                driver.SourceRestoreSettings.ignoredModules.Add(module);
            }
            else
            {
                driver.SourceRestoreSettings.customHandlers[module] = handler.Value;
            }
        }

        public void RemoveSettingsRestoreHandler(SourceEverestModule module)
        {
            if (driver.SourceRestoreSettings.ignoredModules.Contains(module))
            {
                driver.SourceRestoreSettings.ignoredModules.Remove(module);
            }
            else if (driver.SourceRestoreSettings.customHandlers.ContainsKey(module))
            {
                driver.SourceRestoreSettings.customHandlers.Remove(module);
            }
            else
            {
                ((IEverestLoggerProcessEnvironment)driver.Process).Logger.Log(EverestLogLevel.Warn, "CelesteTAS",
                    $"Tried to de-register a custom setting-restore handler for mod '{module.Metadata.Name}', without having a handler previously registered");
            }
        }

        public int GetGroupCounter() => driver.SourceCycleHitboxColor.GroupCounter;

        public void SetGroupCounter(int counter)
        {
            driver.SourceCycleHitboxColor.GroupCounter = counter;
        }

        public object? GetLatestSavestateForFrame(int frame)
        {
            foreach (var state in driver.SourceSavestateManager.AllSavestates.Reverse())
            {
                if (frame <= state.Frame)
                {
                    return state;
                }
            }

            return null;
        }

        public int GetSavestateFrame(object savestate)
        {
            var state = (SourceTasSavestate)savestate;
            return state.Frame;
        }

        public bool LoadSavestate(object savestate)
        {
            var state = (SourceTasSavestate)savestate;
            return state.Load();
        }

        public void DrawAccurateLine(Vector2 from, Vector2 to, PackedColor color) =>
            driver.SourceHitboxFixer.DrawExactLine(from, to, color);

        public void ShowStudioPopupMessage(string id, string title, string text) =>
            driver.SendSourceThirdPartyPopup(id, title, text);
    }
}
