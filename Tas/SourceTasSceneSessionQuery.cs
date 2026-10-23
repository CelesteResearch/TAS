using Celeste.Game.Execution.Levels;
using Celeste.Game.Execution.LevelLoader;
using Celeste.Game.Chapters.Flow;
using Celeste.Simulation.Runtime.Session;

namespace Celeste.Mods.Tas
{
    internal static class SourceTasSceneSessionQuery
    {
        internal static ChapterCompletionSession GetSession(object actualScene) => actualScene switch
        {
            OriginalLevelSceneRoot level => level.Session,
            OriginalLevelLoaderScene levelLoader => levelLoader.Session,
            LevelExitSequence levelExit => levelExit.Session,
            AreaCompleteSequence areaComplete => areaComplete.Session,
            _ => null,
        };

        internal static global::Celeste.Simulation.RoomPlayerSession GetPlayer(object actualScene)
            => actualScene is global::Celeste.Scene.Tracking.ISourceScene scene
                ? scene.SourceScene.Entities.GetTrackedEntity<global::Celeste.Simulation.RoomPlayerSession>(
                    typeof(global::Celeste.Simulation.RoomPlayerSession).FullName)
                : null;
    }
}
