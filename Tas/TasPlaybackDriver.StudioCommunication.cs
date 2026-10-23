using System;
using System.Collections.Generic;
using System.Linq;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasCommunicationAdapter studioCommunication;
        internal bool SourceStudioConnected => studioCommunication is { Connected: true };

        [SourceTasLoad]
        private static void LoadSourceStudioCommunication()
            => CelesteTasModule.Instance.Process.GameEvents.OnExiting +=
                CelesteTasModule.Instance.Driver.StopSourceStudioCommunication;

        [SourceTasUnload]
        private static void UnloadSourceStudioCommunication()
        {
            var driver = CelesteTasModule.Instance.Driver;
            driver.process.GameEvents.OnExiting -= driver.StopSourceStudioCommunication;
            driver.StopSourceStudioCommunication();
        }

        public void StartSourceStudioCommunication()
        {
            if (SyncChecker.Active) return;
            if (studioCommunication != null)
            {
                LogSourceCommunication(global::Celeste.Everest.EverestLogLevel.Warn,
                    "Tried to start the communication adapter while already running!");
                return;
            }

            studioCommunication = new SourceTasCommunicationAdapter(this);
        }

        public void StopSourceStudioCommunication()
        {
            if (SyncChecker.Active) return;
            if (studioCommunication == null)
            {
                LogSourceCommunication(global::Celeste.Everest.EverestLogLevel.Warn,
                    "Tried to stop the communication adapter while not running!");
                return;
            }

            studioCommunication.Dispose();
            studioCommunication = null;
        }

        public void ChangeSourceStudioStatus()
        {
            if (Settings.AttemptConnectStudio && studioCommunication == null)
                StartSourceStudioCommunication();
            else if (studioCommunication != null)
                StopSourceStudioCommunication();
        }

        internal void SendSourceStudioState(StudioState state)
        {
            if (SourceStudioConnected) studioCommunication.WriteState(state);
        }

        internal void SendSourceUpdateLines(Dictionary<int, string> lines)
        {
            if (SourceStudioConnected) studioCommunication.WriteUpdateLines(lines);
        }

        internal void SendSourceCurrentBindings()
        {
            if (!SourceStudioConnected) return;
            Dictionary<int, List<int>> bindings = hotkeys.StudioHotkeys.ToDictionary(
                pair => (int)pair.Key, pair => pair.Value.Cast<int>().ToList());
            studioCommunication.WriteCurrentBindings(bindings);
        }

        internal void SendSourceRecordingFailed(RecordingFailedReason reason)
        {
            if (SourceStudioConnected) studioCommunication.WriteRecordingFailed(reason);
        }

        internal void SendSourceSettings(GameSettings settings)
        {
            if (SourceStudioConnected) studioCommunication.WriteSettings(settings);
        }

        internal void SendSourceCommandList()
        {
            if (SourceStudioConnected) studioCommunication.WriteCommandList(GetSourceCommandList());
        }

        internal void SendSourceThirdPartyPopup(string id, string title, string text)
        {
            if (SourceStudioConnected) studioCommunication.WriteThirdPartyPopup(id, title, text);
        }

        internal void LogSourceCommunication(global::Celeste.Everest.EverestLogLevel level, string message)
            => ((IEverestLoggerProcessEnvironment)process).Logger.Log(level, "CelesteTAS/StudioCom", message);
    }
}
