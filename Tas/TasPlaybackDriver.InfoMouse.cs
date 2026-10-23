#nullable enable
using Celeste.Mods.Tas.Information;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasInfoMouse? sourceInfoMouse;
        internal SourceTasInfoMouse InfoMouse => sourceInfoMouse ??= new SourceTasInfoMouse(process, this);

        [SourceTasLoad]
        private static void LoadSourceTasSelectedAreaEntity() => CelesteTasModule.Instance.Driver.InfoMouse.Load();

        [SourceTasUnload]
        private static void UnloadSourceTasSelectedAreaEntity() => CelesteTasModule.Instance.Driver.InfoMouse.Unload();
    }
}
