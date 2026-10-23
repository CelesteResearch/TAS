using System;
using System.Numerics;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Everest;
using Celeste.Game.Content.Maps;
using Celeste.Game.Execution.Areas;
using Celeste.Game.Execution.LevelLoader;
using Celeste.Game.Save.Runtime;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed partial class SourceTasConsoleProcess
    {
        private void LoadCommand(string command, string[] args, int slot)
        {
            try
            {
                if (!PrepareSourceSaveSlot(slot, args)) return;
                var mode = command switch
                {
                    "hard" => OriginalAreaMode.BSide,
                    "rmx2" => OriginalAreaMode.CSide,
                    _ => OriginalAreaMode.Normal
                };
                if (process.SaveContext.CurrentSave is not { } saveData)
                {
                    AbortTas("Save data is null");
                    return;
                }

                if (args.Length == 0)
                {
                    EnterLevel(((IOriginalLevelLoaderEnvironment)process).LevelLoaders.CreateLevelLoader(
                        ((IOriginalSaveSessionSelectionEnvironment)process).SaveSessionSelection
                        .GetCurrentSessionSafe(saveData)));
                    return;
                }

                if (!TryGetAreaId(args[0], out int areaId))
                {
                    AbortTas($"Map {args[0]} does not exist");
                    return;
                }

                var modes = process.AreaKeys.GetAreaById(areaId).Mode;
                if ((int)mode >= modes.Length || modes[(int)mode] == null)
                {
                    AbortTas($"Map {args[0]} has no {mode}");
                    return;
                }

                var stats = ((IOriginalSaveLevelSetEnvironment)process).SaveLevelSets
                    .GetLevelSetStatsFor(saveData, "Celeste");
                if (areaId > 0 && !saveData.Areas[0].Modes[0].Completed)
                {
                    saveData.Areas[0].Modes[0].Completed = true;
                    stats.UnlockedAreas++;
                }

                if (saveData.FileSlot == -1) saveData.Name = "TAS";
                if (args.Length > 1)
                {
                    if (!double.TryParse(args[1], out double x) || args.Length == 2)
                    {
                        string screen = args[1];
                        if (screen.StartsWith("lvl_")) screen = screen.Substring(4);
                        if (process.AreaTable.Areas[areaId].Mode[(int)mode].MapData is { } mapData &&
                            ((OriginalChapterMapData)mapData).Get(screen) == null)
                        {
                            AbortTas($"Room {screen} does not exist");
                            return;
                        }

                        if (args.Length > 2)
                        {
                            int spawnpoint = int.Parse(args[2]);
                            if (((OriginalChapterMapData)process.AreaTable.Areas[areaId].Mode[(int)mode].MapData)
                                .Get(screen).Spawns is { } spawns)
                            {
                                if (spawnpoint < 0 || spawnpoint >= spawns.Count)
                                    AbortTas($"Room {screen} does not exist spawn point {spawnpoint}");
                            }

                            Load((int)mode, areaId, screen, spawnpoint);
                        }
                        else Load((int)mode, areaId, screen);
                    }
                    else if (args.Length > 2 && double.TryParse(args[2], out double y))
                    {
                        var position = new Vector2((int)Math.Round(x), (int)Math.Round(y));
                        var remainder = new Vector2((float)(x - position.X), (float)(y - position.Y));
                        var speed = Vector2.Zero;
                        if (args.Length > 3 && float.TryParse(args[3], out float speedX)) speed.X = speedX;
                        if (args.Length > 4 && float.TryParse(args[4], out float speedY)) speed.Y = speedY;
                        Load((int)mode, areaId, position, remainder, speed);
                    }
                }
                else Load((int)mode, areaId);
            }
            catch (Exception error)
            {
                var logger = ((IEverestLoggerProcessEnvironment)process).Logger;
                logger.Log(EverestLogLevel.Error, "CelesteTAS", $"Console {command} command failed.");
                logger.LogDetailed(error, "CelesteTAS");
            }
        }
    }
}
