using Celeste.Everest.Content.Extensions.Decals;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Everest.Reflection;
using Celeste.Scene.Tracking;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Gameplay.Hitboxes.Rooms;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : ITasUnloadedRoomHitboxSettingsEnvironment
    {
        private TasUnloadedRoomHitboxProcess sourceTasUnloadedRoomHitboxes;

        ITasUnloadedRoomHitboxSettings ITasUnloadedRoomHitboxSettingsEnvironment.UnloadedRoomHitboxSettings
            => (ITasUnloadedRoomHitboxSettings)sourceTasModule._Settings;

        private TasUnloadedRoomHitboxProcess SourceTasUnloadedRoomHitboxes
            => sourceTasUnloadedRoomHitboxes ??= new TasUnloadedRoomHitboxProcess(process, this,
                new SourceTasEntityTypeHelper(((ISourceSceneCatalogEnvironment)process).SceneCatalog,
                    ((ISourceEverestReflectionEnvironment)process).Reflection,
                    ((IEverestLoggerProcessEnvironment)process).Logger),
                ((IEverestDecalRegistryEnvironment)process).DecalRegistry);

        [SourceTasLoad]
        private static void LoadSourceTasUnloadedRoomHitboxes()
            => TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process).SourceTasUnloadedRoomHitboxes.Load();

        [SourceTasUnload]
        private static void UnloadSourceTasUnloadedRoomHitboxes()
            => TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process).SourceTasUnloadedRoomHitboxes.Unload();
    }
}
