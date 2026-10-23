using System;
using System.Collections.Generic;
using System.IO;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Mods.Tas.Tools;

namespace Celeste.Mods.Tas
{
    internal sealed partial class SourceTasSyncChecker
    {
        private readonly TasPlaybackDriver driver;
        private readonly GameApplication process;
        private EverestLoggerProcess Logger => ((IEverestLoggerProcessEnvironment)process).Logger;

        public SourceTasSyncChecker(TasPlaybackDriver driver, GameApplication process)
        {
            this.driver = driver;
            this.process = process;
        }

        /// Whether the game is in sync-check mode and disallows user intervention
        public bool Active { get; private set; } = false;

        /// Promotes invalid room labels from a warning to an error
        public bool ValidateRoomLabels = false;

        private bool waitingForLoad = true;

        private readonly Queue<string> fileQueue = [];
        private string resultFile = string.Empty;

        private SyncCheckResult.Status currentStatus = SyncCheckResult.Status.Success;
        private SyncCheckResult.AdditionalInfo currentAdditionalInformation = new();

        private SyncCheckResult result = new();

        private string? CurrentFilePath => driver.Controller.Current?.FilePath;
        private int? CurrentFileLine => driver.Controller.Current?.FileLine;

        public void AddFile(string file)
        {
            Active = true;

            if (!File.Exists(file))
            {
                Logger.Log(global::Celeste.Everest.EverestLogLevel.Error, "CelesteTAS/SyncCheck",
                    $"TAS file to sync check was not found: '{file}'");
                return;
            }

            Logger.Log(global::Celeste.Everest.EverestLogLevel.Info, "CelesteTAS/SyncCheck",
                $"Registered file for sync checking: '{file}'");

            fileQueue.Enqueue(file);
        }

        public void SetResultFile(string file)
        {
            Active = true;

            if (!string.IsNullOrEmpty(resultFile))
            {
                Logger.Log(global::Celeste.Everest.EverestLogLevel.Warn, "CelesteTAS/SyncCheck",
                    $"Overwriting previously defined result file '{resultFile}' with '{file}");
            }
            else
            {
                Logger.Log(global::Celeste.Everest.EverestLogLevel.Info, "CelesteTAS/SyncCheck",
                    $"Writing sync-check result to file: '{file}'");
            }

            resultFile = file;
        }

        /// Indicates that a time command was updated with another time
        public void ReportWrongTime(string filePath, int fileLine, string oldTime, string newTime)
        {
            if (!Active)
            {
                return;
            }

            Logger.Log(global::Celeste.Everest.EverestLogLevel.Error, "CelesteTAS/SyncCheck",
                $"Detected wrong time in file '{filePath}' line {fileLine}: '{oldTime}' vs '{newTime}'");

            if (currentStatus != SyncCheckResult.Status.WrongTime)
            {
                currentStatus = SyncCheckResult.Status.WrongTime;
                currentAdditionalInformation.Clear();
                currentAdditionalInformation.WrongTime = [];
            }

            currentAdditionalInformation.WrongTime!.Add(
                new SyncCheckResult.WrongTimeInfo(filePath, fileLine, oldTime, newTime));
        }

        /// Indicates that an unsafe action was performed in safe-mode
        public void ReportUnsafeAction()
        {
            if (!Active)
            {
                return;
            }

            Logger.Log(global::Celeste.Everest.EverestLogLevel.Error, "CelesteTAS/SyncCheck", "Detected unsafe action");

            currentStatus = SyncCheckResult.Status.UnsafeAction;
            currentAdditionalInformation.Clear();
            currentAdditionalInformation.Abort = new SyncCheckResult.AbortInfo(CurrentFilePath, CurrentFileLine,
                driver.Controller.Current?.ToString());
        }

        /// Indicates that an Assert-command failed
        public void ReportAssertFailed(string lineText, string filePath, int fileLine, string expected, string actual)
        {
            if (!Active)
            {
                return;
            }

            Logger.Log(global::Celeste.Everest.EverestLogLevel.Error, "CelesteTAS/SyncCheck",
                $"Detected failed assertion '{lineText}' in file '{filePath}' line {fileLine}: Expected '{expected}', got '{actual}'");

            currentStatus = SyncCheckResult.Status.UnsafeAction;
            currentAdditionalInformation.Clear();
            currentAdditionalInformation.AssertFailed =
                new SyncCheckResult.AssertFailedInfo(filePath, fileLine, actual, expected);
        }

        /// Indicates that a crash happened while sync-checking
        public void ReportCrash(string ex)
        {
            if (!Active)
            {
                return;
            }

            Logger.Log(global::Celeste.Everest.EverestLogLevel.Error, "CelesteTAS/SyncCheck",
                $"Detected a crash: {ex}");

            currentStatus = SyncCheckResult.Status.Crash;
            currentAdditionalInformation.Clear();
            currentAdditionalInformation.Crash = new SyncCheckResult.CrashInfo(CurrentFilePath, CurrentFileLine, ex);
        }
    }
}
