using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment
    {
        [SourceTasLoad]
        private static void LoadSourceTasInfoWatchEntities()
            => CelesteTasModule.Instance.Driver.GameInfo.WatchEntities.Load();

        [SourceTasUnload]
        private static void UnloadSourceTasInfoWatchEntities()
            => CelesteTasModule.Instance.Driver.GameInfo.WatchEntities.Unload();
    }
}
