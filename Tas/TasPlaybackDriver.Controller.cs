using Celeste.Everest.Diagnostics.Logging;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        internal void RefreshInputs(bool forceRefresh = false) => controller.RefreshInputs(forceRefresh);
        public void SetSourceFilePath(string path) => controller.FilePath = path;
        internal void SetSourceParseEndBreakpoint(int frame) => controller.SetSourceParseEndBreakpoint(frame);

        internal global::Celeste.Simulation.Runtime.Session.ChapterCompletionSession GetSourceCurrentSceneSession()
            => SourceTasSceneSessionQuery.GetSession(process.Frame.Scenes.CurrentScene);

        internal void LogSourceTas(global::Celeste.Everest.EverestLogLevel level, string message)
            => ((IEverestLoggerProcessEnvironment)process).Logger.Log(level, "CelesteTAS", message);

        private void Log(string message) => LogSourceTas(global::Celeste.Everest.EverestLogLevel.Info, message);
    }
}
