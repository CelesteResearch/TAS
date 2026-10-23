using System;
using System.Runtime.ExceptionServices;
using Celeste.Everest.Diagnostics.CriticalErrors;
using Celeste.Mods.Tas;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : IEverestCriticalErrorSourceHooksEnvironment
    {
        private Func<ExceptionDispatchInfo, Func<ExceptionDispatchInfo, ExceptionDispatchInfo>, ExceptionDispatchInfo>
            sourceTasCriticalError;

        internal void InstallSourceTasCriticalErrorHook(TasPlaybackDriver driver)
        {
            sourceTasCriticalError = driver.SourceSyncChecker.HandleCriticalError;
        }

        internal void UninstallSourceTasCriticalErrorHook(TasPlaybackDriver driver)
        {
            sourceTasCriticalError = null;
        }

        ExceptionDispatchInfo IEverestCriticalErrorSourceHooksEnvironment.HandleSourceCriticalError(
            ExceptionDispatchInfo error, Func<ExceptionDispatchInfo, ExceptionDispatchInfo> original)
        {
            var hook = sourceTasCriticalError;
            return hook != null ? hook(error, original) : original(error);
        }
    }
}
