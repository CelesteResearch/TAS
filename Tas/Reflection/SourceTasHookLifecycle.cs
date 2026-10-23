using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasHookLifecycle
    {
        [SourceTasUnload]
        internal static void Unload()
        {
            var module = CelesteTasModule.Instance;
            module.Driver.SourceHooks.Unload();
            // Typed equivalents of the original IL hooks retire in HookHelper's original unload phase.
            TasRuntimeEnvironment.Get(module.Process).UninstallSourceTasStudioBannerHooks();
            TasRuntimeEnvironment.Get(module.Process).UninstallSourceTasSaveQuitHooks();
        }
    }
}
