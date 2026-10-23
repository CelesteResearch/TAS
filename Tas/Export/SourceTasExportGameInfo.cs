using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Scene.Tracking;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Input;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Information;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;
using Player = Celeste.Simulation.RoomPlayerSession;
using Overworld = Celeste.Game.Menus.Overworld.OverworldSequence;

namespace Celeste.Mods.Tas.Export
{
    internal sealed class SourceTasExportGameInfo
    {
        private readonly TasPlaybackDriver driver;
        private GameApplication Process => driver.Process;
        internal SourceTasExportGameInfo(TasPlaybackDriver driver) => this.driver = driver;

        private class Meta : ISourceTasCommandMeta
        {
            public string Insert => $"ExportGameInfo{CommandInfo.Separator}[0;dump.txt]";
            public bool HasArguments => true;
        }

        private StreamWriter? streamWriter;
        private IDictionary<string, Func<List<object>?>>? trackedEntities;
        private bool exporting;
        private SourceTasInputFrame? exportingInput;

        // "ExportGameInfo"
        // "ExportGameInfo Path"
        // "ExportGameInfo Path EntitiesToTrack"
        [SourceTasCommand("ExportGameInfo", Aliases = ["StartExportGameInfo"], CalcChecksum = false,
            MetaDataProvider = typeof(Meta))]
        private void StartExportCommand(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            string[] args = commandLine.Arguments;
            string path = "dump.txt";
            if (args.Length > 0)
            {
                if (args[0].Contains("."))
                {
                    path = args[0];
                    args = args.Skip(1).ToArray();
                }
            }

            BeginExport(path, args);
        }

        [SourceTasCommand("EndExportGameInfo", Aliases = ["FinishExportGameInfo"], CalcChecksum = false)]
        private void FinishExportCommand(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            FinishExport();
        }

        [SourceTasDisableRun]
        private static void SourceFinishExport() =>
            CelesteTasModule.Instance.Driver.SourceExportGameInfo.FinishExport();

        private void FinishExport()
        {
            exporting = false;
            exportingInput = null;
            streamWriter?.Dispose();
            streamWriter = null;
        }

        [SourceTasLoad]
        private static void Load()
        {
            var driver = CelesteTasModule.Instance.Driver;
            driver.Process.SourceEngineUpdateHooks.AfterUpdate += driver.SourceExportGameInfo.AfterEngineUpdate;
        }

        [SourceTasUnload]
        private static void Unload()
        {
            var driver = CelesteTasModule.Instance.Driver;
            driver.Process.SourceEngineUpdateHooks.AfterUpdate -= driver.SourceExportGameInfo.AfterEngineUpdate;
        }

        private void AfterEngineUpdate()
        {
            if (exportingInput != null)
            {
                ExportInfo(exportingInput);
                exportingInput = null;
            }
        }

        private void BeginExport(string path, string[] tracked)
        {
            streamWriter?.Dispose();

            exporting = true;
            if (Path.GetDirectoryName(path) is { } dir && dir.IsNotEmpty())
            {
                Directory.CreateDirectory(dir);
            }

            try
            {
                streamWriter = new StreamWriter(path);
            }
            catch (Exception e)
            {
                driver.AbortSourceTas($"ExportGameInfo failed\n{e.Message}", true);
                FinishExport();
                return;
            }

            streamWriter.WriteLine(string.Join("\t", "Line", "Inputs", "Frames", "Time", "Position", "Speed", "State",
                "Statuses", "Room", "Entities"));
            trackedEntities = new Dictionary<string, Func<List<object>?>>();
            foreach (string typeName in tracked)
            {
                var types = driver.TargetQuery.ResolveBaseTypes(typeName.Split('.'), out _);
                if (types.IsEmpty())
                {
                    continue;
                }

                foreach (var type in types)
                {
                    if (driver.TargetQuery.IsSameOrSubclassOfOriginal(type, "Monocle.Entity"))
                    {
                        trackedEntities[driver.TargetQuery.GetOriginalTypeFullName(type)] = () =>
                            driver.TargetQuery.ResolveTypeInstances(type).Cast<object>().ToList();
                    }
                }
            }
        }

        public void ExportInfo()
        {
            if (exporting && driver.Controller.Current is { } currentInput)
            {
                exportingInput = currentInput;
            }
        }

        private void ExportInfo(SourceTasInputFrame inputFrame)
        {
            SourceTasInputController controller = driver.Controller;
            string output;
            if (Process.Frame.Scenes.CurrentScene is Level level)
            {
                Player player =
                    level.SourceScene.Entities.GetTrackedEntity<Player>(
                        typeof(global::Celeste.Simulation.RoomPlayerSession)
                            .FullName);

                if (player == null)
                {
                    return;
                }

                string time = driver.GameInfo.GetChapterTime(level);
                string pos =
                    player.ToSimplePositionString(GetDecimals(driver.Settings.PositionDecimals,
                        GameSettings.MaxDecimals));
                string speed =
                    player.Data.Motion.Normal.Speed.ToSimpleString(GetDecimals(driver.Settings.SpeedDecimals,
                        GameSettings.MaxDecimals));
                string statuses = driver.GameInfo.GetStatuses(level, player);
                driver.GameInfo.GetAdjustedLiftBoost(player, out string liftBoost);
                if (liftBoost.IsNotEmpty())
                {
                    if (statuses.IsEmpty())
                    {
                        statuses = liftBoost;
                    }
                    else
                    {
                        statuses += $"\t{liftBoost}";
                    }
                }

                statuses += $"\t[{level.Session.Level}]";

                output = string.Join("\t",
                    inputFrame.StudioLine + 1, $"{controller.CurrentFrameInInput}/{inputFrame}",
                    controller.CurrentFrameInTas, time, pos, speed,
                    driver.GameInfo.PlayerStates.GetCurrentStateName(player),
                    statuses);

                foreach (string typeName in trackedEntities!.Keys)
                {
                    List<object>? entities = trackedEntities[typeName].Invoke();
                    if (entities == null)
                    {
                        continue;
                    }

                    foreach (object entity in entities)
                    {
                        output +=
                            $"\t{typeName}: {entity.ToSimplePositionString(GetDecimals(driver.Settings.PositionDecimals, GameSettings.MaxDecimals))}";
                    }
                }

                if (driver.GameInfo.CustomInfoProcess.GetInfo(GetDecimals(driver.Settings.CustomInfoDecimals,
                        GameSettings.MaxDecimals)) is { } customInfo &&
                    customInfo.IsNotEmpty())
                {
                    output += $"\t{customInfo.ReplaceLineBreak(" ")}";
                }

                if (driver.GameInfo.WatchEntities.GetInfo(driver.Settings.InfoWatchEntityHudType, "\t", true,
                        GetDecimals(driver.Settings.CustomInfoDecimals, GameSettings.MaxDecimals)) is { } watchInfo &&
                    watchInfo.IsNotEmpty())
                {
                    output += $"\t{watchInfo}";
                }
            }
            else
            {
                string sceneName;
                if (Process.Frame.Scenes.CurrentScene is Overworld overworld)
                {
                    sceneName = $"Overworld {SourceTypeName(overworld.Current ?? overworld.Next)}";
                }
                else
                {
                    sceneName = SourceTypeName(Process.Frame.Scenes.CurrentScene);
                }

                output = string.Join("\t", inputFrame.StudioLine + 1, $"{controller.CurrentFrameInInput}/{inputFrame}",
                    controller.CurrentFrameInTas,
                    sceneName);
            }

            streamWriter?.WriteLine(output);
            streamWriter?.Flush();
        }

        private string SourceTypeName(object value)
        {
            string name = driver.TargetQuery.GetOriginalTypeFullName(value.GetType());
            return name.Substring(name.LastIndexOf('.') + 1);
        }

        private int GetDecimals(int current, int max)
        {
            return current == 0 ? current : max;
        }
    }
}
