using Celeste.Mods.Tas.Gameplay.Classic;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private TasPico8Fixer pico8Fixer;

        internal TasPico8Fixer Pico8Fixer =>
            pico8Fixer ??= new TasPico8Fixer(TasRuntimeEnvironment.Get(process), process);

        [SourceTasLoad]
        private static void LoadSourcePico8Fixer()
        {
            var module = CelesteTasModule.Instance;
            TasRuntimeEnvironment.Get(module.Process).InstallSourceTasClassicHooks(module.Driver.Pico8Fixer);
        }

        [SourceTasUnload]
        private static void UnloadSourcePico8Fixer() =>
            TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process).UninstallSourceTasClassicHooks();
    }
}
