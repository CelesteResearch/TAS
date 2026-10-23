using System;
using Celeste.Core.Diagnostics.Tas;
using Celeste.Mods.Tas;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Reflection;
using Celeste.Everest.Reflection;
using Celeste.Everest.Modules.Runtime;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : ISourceTasManagerEnvironment
    {
        private CelesteTasModule sourceTasModule;
        private TasPlaybackDriver sourceTasDriver;

        public TasPlaybackDriver ActualTasDriver => sourceTasDriver ??
                                                    throw new InvalidOperationException(
                                                        "The actual TAS module is not constructed.");

        public ISourceTasManager ActualTasManager => ActualTasDriver;
        ISourceTasManager ISourceTasManagerEnvironment.TasManager => ActualTasManager;

        // Called by the actual module-registration phase, before SettingsLoad/Load.
        // The registered module and its settings are the objects subsequently read here.
        internal void InitializeModule(CelesteTasModule module)
        {
            if (sourceTasModule != null)
                throw new InvalidOperationException("The actual TAS module is already constructed.");

            var methods = process.Reflection.SourceMethods;
            sourceTasModule = module;
            sourceTasDriver = new TasPlaybackDriver(process, sourceTasModule);
            sourceTasModule.AttachDriver(sourceTasDriver);

            SourceTasOwnedInputMethodBindings.BindCommands(methods, sourceTasDriver);

            SourceTasStudioMethodBindings.BindCommands(methods, sourceTasDriver);

            SourceTasCoreMethodBindings.BindCommands(methods, sourceTasDriver);

            SourceTasGameProtectionMethodBindings.BindCommands(methods, sourceTasDriver);
            PublishSourceTasCollabLobbyBindings(sourceTasDriver);
            SourceTasModInteropActualBindings.Register(process, sourceTasDriver);
            ConstructSourceTasBindings();
        }

        internal void PrepareLifecycleBindings()
        {
            var methods = ((ISourceEverestReflectionEnvironment)process).Reflection.SourceMethods;

            SourceTasOwnedInputMethodBindings.BindStaticLifecycle(methods);

            SourceTasStudioMethodBindings.BindStaticLifecycle(methods);

            SourceTasCoreMethodBindings.BindStaticLifecycle(methods);
            SourceTasPlayAtLaunchMethodBindings.BindStaticLifecycle(methods);

            SourceTasCommonBaselineMethodBindings.BindStaticLifecycle(methods);
            SourceTasStudioInstallationMethodBindings.BindStaticLifecycle(methods);
            SourceTasConsoleMethodBindings.BindStaticConsoleCommands(methods);
            SourceTasMenuMethodBindings.BindCommands(methods, process.OriginalGameAssemblyIdentity);
            SourceTasGameProtectionMethodBindings.BindStaticLifecycle(methods);
        }

        // Module.Load/Unload installs/removes the original playback hooks. Construction
        // does not initialize input bindings or enable a run.
        internal void InstallSourceTasPlaybackHooks()
        {
            if (process.InputUpdateOverride != null)
                throw new InvalidOperationException("The source TAS playback hooks are already installed.");
            process.InputUpdateOverride = UpdateInstalledSourceTasInputFrame;
            process.EngineUpdatePaused = ActualTasDriver.IsPausedAfterInput;
            process.EngineUpdateBeginning += ActualTasDriver.PreEngineUpdate;
            process.EngineUpdateEnded += ActualTasDriver.PostEngineUpdate;
        }

        internal void UninstallSourceTasPlaybackHooks()
        {
            process.InputUpdateOverride = null;
            process.EngineUpdatePaused = null;
            process.EngineUpdateBeginning -= ActualTasDriver.PreEngineUpdate;
            process.EngineUpdateEnded -= ActualTasDriver.PostEngineUpdate;
        }

        private void UpdateInstalledSourceTasInputFrame()
        {
            if (!sourceTasDriver.Enabled)
            {
                process.MInput.Update();
                return;
            }

            if (!sourceTasDriver.Running)
                process.MInput.Update();
            sourceTasDriver.Update();
        }
    }
}
