using Celeste.Simulation.Runtime.Session;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasGameInfo
    {
        internal ChapterCompletionSession GetSourceSceneSession()
            => SourceTasSceneSessionQuery.GetSession(process.Frame.Scenes.CurrentScene);
    }
}
