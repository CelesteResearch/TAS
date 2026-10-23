using System.Numerics;
using Celeste.Game.Content.Maps;
using Celeste.Game.Execution.LevelLoader;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed partial class SourceTasConsoleProcess
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private Vector2 initRemainder;
        private Vector2 initSpeed;

        internal SourceTasConsoleProcess(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        internal bool TryGetAreaId(string id, out int areaId)
        {
            if (int.TryParse(id, out areaId))
                return areaId >= 0 && areaId < process.AreaTable.Areas.Count;
            var area = process.AreaKeys.GetAreaBySID(id);
            areaId = area?.ID ?? -1;
            return area != null;
        }

        private void Load(int mode, int areaId, string screen = null, int? spawnPoint = null)
        {
            var areaKey = process.AreaKeys.Create(areaId, mode);
            // The checkpoint query is evaluated before either source Session constructor.
            var session = process.Sessions.GetCheckpoint(areaKey, screen) != null
                ? process.Sessions.CreateSession(areaKey, screen)
                : process.Sessions.CreateSession(areaKey);
            var map = (OriginalChapterMapData)session.MapData;
            if (screen != null)
            {
                session.Level = screen;
                session.FirstLevel = session.LevelData == map.StartLevel();
            }

            Vector2? startPosition = null;
            if (spawnPoint != null)
                startPosition = map.Get(screen).Spawns[spawnPoint.Value];

            ClearSourceCollabLobby(areaKey);
            session.StartedFromBeginning = spawnPoint == null && session.FirstLevel;
            EnterLevel(((IOriginalLevelLoaderEnvironment)process).LevelLoaders
                .CreateLevelLoader(session, startPosition));
        }

        private void Load(int mode, int areaId, Vector2 spawnPoint, Vector2 remainder, Vector2 speed)
        {
            var areaKey = process.AreaKeys.Create(areaId, mode);
            var session = process.Sessions.CreateSession(areaKey);
            if (((OriginalChapterMapData)session.MapData).GetAt(spawnPoint) is not { } levelData)
            {
                AbortTas($"Room does not exist at {spawnPoint}");
                return;
            }

            session.Level = levelData.Name;
            if (process.Sessions.GetCheckpoint(areaKey, session.Level) != null)
                session = process.Sessions.CreateSession(areaKey, session.Level);

            ClearSourceCollabLobby(areaKey);
            session.FirstLevel = false;
            session.StartedFromBeginning = false;
            session.RespawnPoint = spawnPoint;
            initRemainder = remainder;
            initSpeed = speed;
            EnterLevel(((IOriginalLevelLoaderEnvironment)process).LevelLoaders
                .CreateLevelLoader(session));
        }
    }
}
