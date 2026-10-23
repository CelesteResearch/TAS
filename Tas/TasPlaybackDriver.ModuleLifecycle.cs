using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Interop;
using System.Collections.Generic;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        internal SourceTasAttributeProcess ModuleAttributes => module.Attributes;

        [SourceTasLoad]
        private static void LoadSourcePlaybackHooks()
        {
            TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process).InstallSourceTasPlaybackHooks();
            CelesteTasModule.Instance.Driver.LoadSourceCoreMethodHooks();
        }

        [SourceTasUnload]
        private static void UnloadSourcePlaybackHooks()
        {
            TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process).UninstallSourceTasPlaybackHooks();
            CelesteTasModule.Instance.Driver.UnloadSourceCoreMethodHooks();
        }

        [SourceTasInitialize]
        private static void InitializeSourceHotkeys()
            => CelesteTasModule.Instance.Driver.hotkeys.Initialize();

        [SourceTasEnableRun(0)]
        private static void SetSourceTasBindings()
            => TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process).TasBindings.SetTasBindings();

        [SourceTasDisableRun]
        private static void RestoreSourcePlayerBindings()
            => TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process).TasBindings.RestorePlayerBindings();

        [SourceTasDisableRun]
        private static void ReleaseSourceForwardKeys()
            => CelesteTasModule.Instance.Driver.hotkeys.ReleaseAllKeys();

        [SourceTasSaveState]
        private static void SaveSourceBindingState(Dictionary<string, object> values)
            => TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process).TasBindings.SaveState(values);

        [SourceTasLoadState]
        private static void LoadSourceBindingState(Dictionary<string, object> values)
            => TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process).TasBindings.LoadState(values);

        [SourceTasInitialize]
        private static void InitializeSourceManagerEvents()
            => CelesteTasModule.Instance.Driver.RunEvents.Initialize();
    }
}
