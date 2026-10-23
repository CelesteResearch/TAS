using System;
using Celeste.Game.Profiles;
using Celeste.Mods.Tas.EverestInterop;
using Celeste.Mods.Tas.Gameplay.Simplification;
using Celeste.Mods.Tas.Reflection;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment
    {
        private bool sourceTasGameActualMethodTargetsPublished;

        internal void PublishSourceTasGameActualMethodTargets()
        {
            if (sourceTasGameActualMethodTargetsPublished)
                return;
            if (process.SourceProfile != GameProfile.InstalledEverest)
                throw new InvalidOperationException(
                    "Installed TAS actual targets require the Installed source profile.");

            PublishSourceTasGameActualTypeQualifications();
            SourceTasNpcHitboxPhysicalMemberBindings.Register(process);
            SourceTasCycleHitboxPhysicalMemberBindings.Register(process);
            SourceTasGameInfoPhysicalMemberBindings.Register(process);
            SourceTasActualCollidePhysicalMemberBindings.Register(process);
            SourceTasGameGraphicsActualMemberBindings.Register(process);

            PublishSourceTasOptimizationActualMethods();
            SourceTasSeedAndDesyncActualMethodBindings.Register(process);
            SourceTasSkinModGetterActualMethodBindings.Register(process);
            SourceTasHitboxActualMethodBindings.Register(process);
            SourceTasHitboxLifecycleActualMethodBindings.Register(process);
            SourceTasSimplifiedRenderMethodBindings.Register(process);
            SourceTasSimplifiedHudActualMethodBindings.Register(process);
            SourceTasSimplifiedDecorationActualMethodBindings.Register(process);
            SourceTasSimplifiedTerrainTargets.Register(process);
            SourceTasSimplifiedGraphicsAudioTargets.Register(process);
            SourceTasSimplifiedHazardActualTargets.Register(process);
            SourceTasSimplifiedSceneTargets.Register(process);
            sourceTasGameActualMethodTargetsPublished = true;
        }
    }
}
