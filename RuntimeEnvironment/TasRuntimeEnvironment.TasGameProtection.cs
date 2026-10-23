using Celeste.Core.Scheduling;
using Celeste.Core.Audio;
using System;
using Celeste.Core.Diagnostics.Tas;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : ISourceGameplayLifecycleExtension
    {
        private bool sourceTasSaveQuitHooks;
        private bool sourceTasPreventExitHook;
        internal void InstallSourceTasSaveQuitHooks() => sourceTasSaveQuitHooks = true;
        internal void UninstallSourceTasSaveQuitHooks() => sourceTasSaveQuitHooks = false;
        internal void InstallSourceTasPreventExitHook() => sourceTasPreventExitHook = true;
        internal void UninstallSourceTasPreventExitHook() => sourceTasPreventExitHook = false;

        void ISourceGameplayLifecycleExtension.BeforeSaveAndQuit()
        {
            if (sourceTasSaveQuitHooks) sourceTasModule.Driver.SourceSaveAndQuitReenter.BeforeSaveAndQuitPressed();
        }

        void ISourceGameplayLifecycleExtension.BeforeLevelUpdate()
        {
            if (sourceTasSaveQuitHooks) sourceTasModule.Driver.SourceSaveAndQuitReenter.BeforeLevelUpdate();
        }

        void ISourceGameplayLifecycleExtension.InvokeMainMenuExit(Action original)
        {
            if (sourceTasPreventExitHook && sourceTasModule.Driver.Running)
            {
                sourceTasModule.Driver.AbortSourceTas(
                    "To close the game while TAS is running\nuse the ExitGame command instead");
                return;
            }

            original();
        }
    }
}
