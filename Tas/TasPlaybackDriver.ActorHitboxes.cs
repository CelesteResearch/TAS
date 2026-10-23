using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Gameplay.Hitboxes;
using Celeste.Mods.Tas.Reflection;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasFastReflectionProcess sourceFastReflection;
        private SourceTasNpcHitboxProcess sourceNpcHitbox;
        private SourceTasFinalBossHitboxProcess sourceFinalBossHitbox;
        private SourceTasConquerorBeamHitboxProcess sourceConquerorBeamHitbox;
        internal SourceTasFastReflectionProcess SourceFastReflection => sourceFastReflection;
        internal SourceTasNpcHitboxProcess SourceNpcHitbox => sourceNpcHitbox;
        internal SourceTasFinalBossHitboxProcess SourceFinalBossHitboxes => sourceFinalBossHitbox;
        internal SourceTasConquerorBeamHitboxProcess SourceConquerorBeamHitbox => sourceConquerorBeamHitbox;

        private void InitializeSourceActorHitboxes()
        {
            sourceFastReflection = new SourceTasFastReflectionProcess(process);
            sourceNpcHitbox = new SourceTasNpcHitboxProcess(process, this);
            sourceFinalBossHitbox = new SourceTasFinalBossHitboxProcess(process, this);
            sourceConquerorBeamHitbox = new SourceTasConquerorBeamHitboxProcess(process, this);
        }

        [SourceTasLoad]
        private static void LoadSourceNpcHitbox() => CelesteTasModule.Instance.Driver.SourceNpcHitbox.Load();

        [SourceTasUnload]
        private static void UnloadSourceNpcHitbox() => CelesteTasModule.Instance.Driver.SourceNpcHitbox.Unload();

        [SourceTasLoad]
        private static void LoadSourceFinalBossHitbox() =>
            CelesteTasModule.Instance.Driver.SourceFinalBossHitboxes.Load();

        [SourceTasUnload]
        private static void UnloadSourceFinalBossHitbox() =>
            CelesteTasModule.Instance.Driver.SourceFinalBossHitboxes.Unload();

        [SourceTasInitialize]
        private static void InitializeSourceConquerorBeamHitbox() =>
            CelesteTasModule.Instance.Driver.SourceConquerorBeamHitbox.Initialize();

        [SourceTasUnload]
        private static void UnloadSourceConquerorBeamHitbox() =>
            CelesteTasModule.Instance.Driver.SourceConquerorBeamHitbox.Unload();
    }
}
