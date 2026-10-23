using Celeste.Mods.Tas.EverestInterop;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasDesyncFixerProcess sourceDesyncFixer;

        internal SourceTasDesyncFixerProcess SourceDesyncFixer =>
            sourceDesyncFixer ??= new SourceTasDesyncFixerProcess(this);

        [SourceTasInitialize]
        private static void InitializeSourceDesyncFixer() =>
            CelesteTasModule.Instance.Driver.SourceDesyncFixer.Initialize();

        [SourceTasLoad]
        private static void LoadSourceDesyncFixer() => CelesteTasModule.Instance.Driver.SourceDesyncFixer.Load();

        [SourceTasUnload]
        private static void UnloadSourceDesyncFixer() => CelesteTasModule.Instance.Driver.SourceDesyncFixer.Unload();
    }
}
