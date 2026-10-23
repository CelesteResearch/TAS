using Celeste.Core.Reflection.Interop;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Interop
{
    internal sealed class SourceTasHelperInterop
    {
        private readonly TasPlaybackDriver driver;
        internal SourceTasHelperInterop(TasPlaybackDriver driver) => this.driver = driver;
        internal SourceTasHelperImports Imports { get; } = new();
        private bool loaded;
        public bool InPrediction => loaded && Imports.InPrediciton!();

        [SourceTasInitialize]
        private static void SourceInitialize() => CelesteTasModule.Instance.Driver.SourceTasHelper.Initialize();

        internal void Initialize()
        {
            ((ISourceModInteropEnvironment)driver.Process).ModInterop.ModInterop(typeof(SourceTasHelperImports));
            loaded = Imports.InPrediciton is not null;
        }
    }
}
