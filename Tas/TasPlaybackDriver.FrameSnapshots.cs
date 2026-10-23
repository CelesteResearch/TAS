using Celeste.Simulation;
using Celeste.Simulation.Diagnostics.Frames;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private readonly FrameSnapshotSampler frameSnapshotSampler = new FrameSnapshotSampler();
        private readonly FrameSceneGeneration frameSceneGeneration = new FrameSceneGeneration();
        private int pendingSampleFeedIndex = -1;
        private bool sampledTasUpdateCounter;
        private ulong tasUpdateCounter;


        public IFrameSnapshotObserver FrameSnapshotObserver
        {
            get => frameSnapshotSampler.Observer;
            set => frameSnapshotSampler.Observer = value;
        }

        internal void MarkTasBeforeUpdate()
        {
            if (pendingSampleFeedIndex < 0 || !frameSnapshotSampler.Enabled) return;
            tasUpdateCounter = SourceSeededRandomness.FrameCounterHandler.FrameCounter;
            sampledTasUpdateCounter = true;
        }

        private void ResetFrameSnapshotAfterWorldRestore()
        {
            frameSceneGeneration.BeginNewSceneEpoch();
            pendingSampleFeedIndex = -1;
            sampledTasUpdateCounter = false;
            tasUpdateCounter = 0;
        }

        private void OnSourceFrameCompleted(global::Celeste.Core.Scheduling.ISourceEngineContext actualEngine)
        {
            int feed = pendingSampleFeedIndex;
            pendingSampleFeedIndex = -1;
            if (feed < 0 || !frameSnapshotSampler.Enabled) return;
            var room = actualEngine.Frame.Scenes.CurrentScene
                as global::Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;
            if (room == null)
            {
                frameSnapshotSampler.PublishBoundary(new FrameBoundary(feed, frameSceneGeneration.Current,
                    FrameBoundaryKind.SceneHandoffWithoutRoomTick));
                return;
            }

            var players =
                ((global::Celeste.Scene.Tracking.ISourceScene)room).SourceScene.Entities.GetTrackedEntities(
                    typeof(global::Celeste.Simulation.RoomPlayerSession).FullName);

            if (players.Count == 0)
            {
                frameSnapshotSampler.PublishBoundary(new FrameBoundary(feed, frameSceneGeneration.Current,
                    FrameBoundaryKind.SceneHandoffWithoutRoomTick));
                return;
            }

            RoomPlayerSession player = (RoomPlayerSession)players[0];
            int generation =
                frameSceneGeneration.Observe(player, room.Session.MapData.GetLevel(room.Session.Level), feed);
            string roomName = room.Session.Level;
            var areaKeys = ((global::Celeste.Game.Execution.Areas.IOriginalAreaKeyEnvironment)process).AreaKeys;
            string areaSid = areaKeys.GetSID(areaKeys.GetSessionArea(room.Session));
            var key = new FrameSnapshotKey(feed, FrameSamplePhase.AfterRoomTick, areaSid, roomName, generation);
            var rng = new RngSnapshot(sampledTasUpdateCounter, tasUpdateCounter, default, default, default);
            frameSnapshotSampler.Publish(key, player, rng);
        }
    }
}
