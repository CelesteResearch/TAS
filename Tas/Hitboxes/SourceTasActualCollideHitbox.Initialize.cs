using System;
using Celeste.Core.Reflection.Members;
using Celeste.Everest.Reflection;
using Celeste.Game.Gameplay.Enemies.BadelineBoss.Projectiles;
using Celeste.Physics.Collision;
using Celeste.Scene.Entities;
using MonoMod.Cil;

namespace Celeste.Mods.Tas.Hitboxes
{
    public sealed partial class SourceTasActualCollideHitbox
    {
        public void Initialize()
        {
            var reflection = ((ISourceEverestReflectionEnvironment)driver.Process).Reflection;
            var customType =
                driver.SourceModReflection.TryGetType("SpirialisHelper", "Celeste.Mod.Spirialis.TimeController");
            if (customType != null && driver.SourceModReflection.GetMethodInfo(
                    customType, "CustomELUpdate", logFailure: false) is { } customELUpdate)
                driver.SourceHooks.IlHook(customELUpdate.ActualMethod, (cursor, _) => cursor.EmitDelegate(Clear));
            CollidableHandlers.Clear();
            CollidableHandlers.Add(typeof(FinalBossBeamState),
                e => driver.SourceFinalBossHitboxes.IsBeamCollidable((FinalBossBeamState)e));
            var members = reflection.SourceMembers;
            Type lightningType = members.GetActualSourceType(
                members.GetOriginalTypeDefinition(typeof(SceneEntityState)).AssemblyIdentity,
                "Celeste.Lightning");
            var disappearing = members.GetFieldInfo(lightningType, "disappearing");
            CollidableHandlers.Add(lightningType,
                e => ((ISourceEntityCollider)e).Collidable && !(bool)disappearing.Read(e));
            if (driver.SourceModReflection.TryGetType("ChronoHelper", "Celeste.Mod.ChronoHelper.Entities.DarkLightning")
                is
                { } chronoLightningType)
            {
                var field = driver.SourceModReflection.GetFieldInfo(chronoLightningType, "disappearing");
                CollidableHandlers.Add(chronoLightningType, e =>
                {
                    if (!((ISourceEntityCollider)e).Collidable) return false;
                    if (field.Read(e) is bool b) return !b;
                    return true;
                });
            }

            if (driver.SourceModReflection.TryGetType("Glyph", "Celeste.Mod.AcidHelper.Entities.AcidLightning") is
                { } acidLightningType)
            {
                var field = driver.SourceModReflection.GetFieldInfo(acidLightningType, "disappearing");
                CollidableHandlers.Add(acidLightningType, e =>
                {
                    if (!((ISourceEntityCollider)e).Collidable) return false;
                    if (field.Read(e) is bool b) return !b;
                    return true;
                });
            }
        }
    }
}
