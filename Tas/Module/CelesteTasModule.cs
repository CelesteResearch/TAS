using Celeste.Everest;
using StudioCommunication;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Celeste.Core.Scheduling;
using Celeste.Everest.Modules.Runtime;
using Celeste.Everest.Diagnostics.Logging;

namespace Celeste.Mods.Tas.Module
{
    public partial class CelesteTasModule : SourceEverestModule
    {
        public static CelesteTasModule Instance { get; private set; }
        public static Version RequiredTasHelperVersion = new Version(2, 2, 1);
        internal readonly SourceTasAttributeProcess Attributes;
        private TasPlaybackDriver driver;
        internal GameApplication Process => (GameApplication)SourceEngine;
        public override Type SettingsType => typeof(CelesteTasSettings);

        public CelesteTasModule(ISourceEngineContext engine) : base(engine)
        {
            Instance = this;
            Attributes = new SourceTasAttributeProcess(Process, typeof(CelesteTasModule));
            Attributes.CollectOwnMethods<SourceTasLoadAttribute>();
            Attributes.CollectOwnMethods<SourceTasLoadContentAttribute>();
            Attributes.CollectOwnMethods<SourceTasUnloadAttribute>();
            Attributes.CollectOwnMethods<SourceTasInitializeAttribute>();
            TasRuntimeEnvironment.GetOrCreate(Process).InitializeModule(this);
        }

        internal TasPlaybackDriver Driver => driver;
        internal void AttachDriver(TasPlaybackDriver actualDriver) => driver = actualDriver;

        public override void Initialize()
        {
            if (((ISourceEverestModulesProcessEnvironment)Process).ModulesProcess.Modules.FirstOrDefault(module =>
                    module.Metadata.Name == "TASHelper") is { } tasHelperModule)
            {
                if (tasHelperModule.Metadata.Version < RequiredTasHelperVersion)
                    throw new Exception(
                        $"TAS Helper v{tasHelperModule.Metadata.Version.ToString(3)} is OUTDATED! Install v{RequiredTasHelperVersion.ToString(3)} or later through Olympus or manually!");
            }

            Attributes.Invoke<SourceTasInitializeAttribute>();
            if (((CelesteTasSettings)_Settings).AttemptConnectStudio)
                driver.StartSourceStudioCommunication();
        }

        public override void Load()
        {
            if (!string.IsNullOrEmpty(Metadata.PathDirectory))
                ((IEverestLoggerProcessEnvironment)Process).Logger.SetLogLevel("CelesteTAS", EverestLogLevel.Verbose);
            var settings = (CelesteTasSettings)_Settings;
            if (settings.ForceAllowAccessibilityTools == StudioEnableCondition.ForCurrentSession)
                settings.ForceAllowAccessibilityTools = StudioEnableCondition.WhileStudioConnected;
            Attributes.Invoke<SourceTasLoadAttribute>();
        }

        public override void Unload()
        {
            Attributes.Invoke<SourceTasUnloadAttribute>();
            TasRuntimeEnvironment.Get(Process).DetachModule();
        }

        public override void LoadContent(bool firstLoad) => Attributes.Invoke<SourceTasLoadContentAttribute>();

        public override bool ParseArg(string arg, Queue<string> args)
        {
            switch (arg)
            {
                case "--tas-file":
                case "--tas":
                {
                    if (args.TryDequeue(out string? path))
                    {
                        if (!File.Exists(path))
                        {
                            ((IEverestLoggerProcessEnvironment)Process).Logger.Log(EverestLogLevel.Error, "CelesteTAS",
                                $"Specified TAS file '{path}' not found");
                        }
                        else
                        {
                            driver.PlayTasAtLaunch.FilePath = path;
                        }
                    }
                    else
                    {
                        ((IEverestLoggerProcessEnvironment)Process).Logger.Log(EverestLogLevel.Error, "CelesteTAS",
                            "Expected file path after --tas CLI argument");
                    }

                    return true;
                }

                case "--sync-check-file":
                {
                    if (args.TryDequeue(out string? path))
                    {
                        driver.SourceSyncChecker.AddFile(path);
                    }
                    else
                    {
                        ((IEverestLoggerProcessEnvironment)Process).Logger.Log(EverestLogLevel.Error, "CelesteTAS",
                            "Expected file path after --sync-check-file CLI argument");
                    }

                    return true;
                }
                case "--sync-check-file-list":
                {
                    if (args.TryDequeue(out string? fileListPath))
                    {
                        if (File.Exists(fileListPath))
                        {
                            foreach (string path in File.ReadLines(fileListPath))
                            {
                                driver.SourceSyncChecker.AddFile(path);
                            }
                        }
                        else
                        {
                            ((IEverestLoggerProcessEnvironment)Process).Logger.Log(EverestLogLevel.Error, "CelesteTAS",
                                $"File-list providing TASes to sync-check not found: '{fileListPath}'");
                        }
                    }
                    else
                    {
                        ((IEverestLoggerProcessEnvironment)Process).Logger.Log(EverestLogLevel.Error, "CelesteTAS",
                            "Expected file path after --sync-check-file CLI argument");
                    }

                    return true;
                }
                case "--sync-check-result":
                {
                    if (args.TryDequeue(out string? path))
                    {
                        driver.SourceSyncChecker.SetResultFile(path);
                    }
                    else
                    {
                        ((IEverestLoggerProcessEnvironment)Process).Logger.Log(EverestLogLevel.Error, "CelesteTAS",
                            "Expected file path after --sync-check-result CLI argument");
                    }

                    return true;
                }

                case "--validate-room-labels":
                {
                    driver.SourceSyncChecker.ValidateRoomLabels = true;
                    return true;
                }

                default:
                    return false;
            }
        }

        public override void OnInputInitialize()
        {
            base.OnInputInitialize();
            OnInputDeregister();
        }
    }
}
