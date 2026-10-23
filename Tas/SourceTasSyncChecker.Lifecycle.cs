using System;
using System.Runtime.ExceptionServices;
using Celeste.Core.Scheduling;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Content.Maps;
using Celeste.Game.Chapters.Flow;
using Celeste.Game.Menus.Overworld;
using Celeste.Game.Menus.Journal;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Tools;

namespace Celeste.Mods.Tas
{
    internal sealed partial class SourceTasSyncChecker
    {
        [SourceTasInitialize]
        private static void InitializeSourceSyncChecker()
            => CelesteTasModule.Instance.Driver.SourceSyncChecker.Initialize();

        [SourceTasUnload]
        private static void UnloadSourceSyncChecker()
            => CelesteTasModule.Instance.Driver.SourceSyncChecker.Unload();

        [SourceTasParseFileEnd]
        private static void ParseSourceFileEnd()
            => CelesteTasModule.Instance.Driver.SourceSyncChecker.ParseFileEnd();

        private void Initialize()
        {
            process.SourceSceneTransitioned += OnSceneTransition;
            TasRuntimeEnvironment.Get(process).InstallSourceTasCriticalErrorHook(driver);
            InitializeSourceStartupPatches();
        }

        private void Unload()
        {
            process.SourceSceneTransitioned -= OnSceneTransition;
            TasRuntimeEnvironment.Get(process).UninstallSourceTasCriticalErrorHook(driver);
            UnloadSourceStartupPatches();
        }

        internal ExceptionDispatchInfo HandleCriticalError(ExceptionDispatchInfo error,
            Func<ExceptionDispatchInfo, ExceptionDispatchInfo> original)
        {
            driver.DisableRun();
            return Active ? error : original(error);
        }

        private void OnSceneTransition(ISourceSceneExecution last, ISourceSceneExecution next)
        {
            if (waitingForLoad && Active && next is OverworldSequence overworld)
            {
                waitingForLoad = false;
                if (string.IsNullOrEmpty(resultFile))
                {
                    Logger.Log(global::Celeste.Everest.EverestLogLevel.Error, "CelesteTAS/SyncCheck",
                        "No result file specified. Aborting sync-check!");
                    process.ActualGame.Exit();
                }

                process.CurrentCoreSettings.DebugMode = Celeste.Everest.Settings.EverestVanillaTristate.Everest;
                overworld.OnEndOfFrame += () =>
                {
                    if (fileQueue.TryDequeue(out string file))
                        CheckFile(file);
                    else
                    {
                        result.Finished = true;
                        result.WriteToFile(resultFile);
                        Environment.Exit(0);
                    }
                };
            }
        }

        private void CheckFile(string file)
        {
            currentStatus = SyncCheckResult.Status.Success;
            currentAdditionalInformation.Clear();
            Logger.Log(global::Celeste.Everest.EverestLogLevel.Info, "CelesteTAS/SyncCheck",
                $"Starting check for file: '{file}'");
            driver.SetSourceFilePath(file);
            driver.EnableRun();
        }

        private void ParseFileEnd()
        {
            if (!Active) return;
            var controller = driver.Controller;
            if (controller.Inputs.Count == 0)
            {
                ReportRunFinished();
                return;
            }

            controller.SetSourceParseEndBreakpoint(controller.Inputs.Count);
        }

        public void ReportRunFinished()
        {
            if (!Active) return;
            var controller = driver.Controller;
            Logger.Log(global::Celeste.Everest.EverestLogLevel.Info, "CelesteTAS/SyncCheck",
                $"Finished check for file: '{controller.FilePath}'");
            object scene = process.Frame.Scenes.CurrentScene;
            bool finishedScene = scene is OriginalLevelSceneRoot { Completed: true }
                                 || scene is OriginalLevelSceneRoot epilogue
                                 && process.AreaKeys.GetSID(process.AreaKeys.GetSessionArea(epilogue.Session)) ==
                                 "Celeste/8-Epilogue"
                                 && epilogue.Session.Level == "inside"
                                 || scene is LevelExitSequence
                                 || scene is AreaCompleteSequence
                                 || scene is OverworldSequence { Current: OuiJournalSequence };
            if (currentStatus == SyncCheckResult.Status.Success && (!finishedScene || controller.CanPlayback))
            {
                currentStatus = SyncCheckResult.Status.NotFinished;
                currentAdditionalInformation.Clear();
                currentAdditionalInformation.Abort = new SyncCheckResult.AbortInfo(CurrentFilePath, CurrentFileLine,
                    controller.Current?.ToString());
            }

            driver.GameInfo.Update(updateVel: false);
            var session = driver.GameInfo.GetSourceSceneSession();
            string infoWithSid = session != null
                ? $"{driver.GameInfo.ExactStatus}\n\nSID: {process.AreaKeys.Format(process.AreaKeys.GetSessionArea(session))} ({((OriginalChapterMapData)session.MapData).Filename})"
                : driver.GameInfo.ExactStatus;
            result.Entries.Add(new SyncCheckResult.Entry(controller.FilePath, currentStatus, infoWithSid,
                currentAdditionalInformation));
            result.WriteToFile(resultFile);
            if (fileQueue.TryDequeue(out string file))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                driver.QueueMainThreadAction(() => CheckFile(file));
            }
            else
            {
                result.Finished = true;
                result.WriteToFile(resultFile);
                Environment.Exit(0);
            }
        }
    }
}
