using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment
    {
        [SourceTasInitialize]
        private static void InitializeSourceTasCollabUtils2()
            => CelesteTasModule.Instance.Driver.CollabUtils2.Initialize();
    }
}
