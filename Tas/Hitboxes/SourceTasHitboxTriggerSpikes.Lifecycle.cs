using System;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Rendering;
using Celeste.Everest.Reflection;
using Celeste.Everest.Gameplay.Hazards.TriggerSpikes;
using Celeste.Simulation.Gameplay.Hazards.TriggerSpikes;
using Celeste.Physics.Collision;
using Celeste.Scene.Entities;
using MonoMod.RuntimeDetour;

namespace Celeste.Mods.Tas.Hitboxes
{
    internal sealed partial class SourceTasHitboxTriggerSpikes
    {
        private Type groupedTriggerSpikesType;
        private Func<object, bool> groupedTriggerSpikesTriggered;
        private Func<object, float> groupedTriggerSpikesLerp;
        private Hook groupedHook, triggerHook;
        private Type triggerSpikesType, originalTriggerSpikesType;

        private delegate void DebugRenderOriginal(SceneEntityState self, ISourceRenderContext context, object camera);

        private delegate void DebugRenderHook(DebugRenderOriginal orig, SceneEntityState self,
            ISourceRenderContext context,
            object camera);

        private System.Reflection.MethodInfo DebugRenderMethod()
        {
            return global::Celeste.Mods.Tas.Reflection.SourceTasDeclaredHookTargets.Require(driver.Process,
                typeof(SceneEntityState), "DebugRender", new[] { "Monocle.Camera" });
        }

        private Func<object, T> CreateFieldGetter<T>(Type type, string name)
        {
            if (type == null) return null;
            var field =
                driver.SourceModReflection.GetFieldInfo(type, name);
            if (field == null) return null;
            if (!typeof(T).IsAssignableFrom(field.ValueType))
                throw new InvalidCastException(
                    field.Name + " is of type " + field.ValueType + ", it cannot be assigned to the type " + typeof(T) +
                    ".");
            return actor => (T)field.Read(actor);
        }

        public void Initialize()
        {
            groupedTriggerSpikesType = driver.SourceModReflection.TryGetType("MaxHelpingHand",
                "Celeste.Mod.MaxHelpingHand.Entities.GroupedTriggerSpikes");
            groupedTriggerSpikesTriggered = CreateFieldGetter<bool>(groupedTriggerSpikesType, "Triggered");
            groupedTriggerSpikesLerp = CreateFieldGetter<float>(groupedTriggerSpikesType, "Lerp");
            if (groupedTriggerSpikesType != null && groupedTriggerSpikesTriggered != null &&
                groupedTriggerSpikesLerp != null)
                groupedHook = new Hook(DebugRenderMethod(), (DebugRenderHook)ShowGroupedTriggerSpikesHitboxes);
        }

        public void Load()
        {
            var members = ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMembers;
            string assembly = members.GetOriginalTypeDefinition(typeof(SceneEntityState)).AssemblyIdentity;
            triggerSpikesType = members.GetActualSourceType(assembly, "Celeste.TriggerSpikes");
            originalTriggerSpikesType =
                members.GetActualSourceType(assembly, "Celeste.Mod.Entities.TriggerSpikesOriginal");
            triggerHook = new Hook(DebugRenderMethod(), (DebugRenderHook)ShowTriggerSpikesHitboxes);
        }

        public void Unload()
        {
            groupedHook?.Dispose();
            triggerHook?.Dispose();
        }

        private void ShowGroupedTriggerSpikesHitboxes(DebugRenderOriginal orig, SceneEntityState self,
            ISourceRenderContext context, object camera)
        {
            if (!driver.Settings.ShowHitboxes || self.GetType() != groupedTriggerSpikesType)
            {
                orig(self, context, camera);
                return;
            }

            self.Collider.Render(camera, driver.SourceHitboxColor.EntityColorInverselyLessAlpha, context);
            if (groupedTriggerSpikesTriggered(self) && groupedTriggerSpikesLerp(self) >= 1f)
                self.Collider.Render(camera, driver.SourceHitboxColor.EntityColor, context);
        }

        private void ShowTriggerSpikesHitboxes(DebugRenderOriginal orig, SceneEntityState self,
            ISourceRenderContext context, object camera)
        {
            if (!driver.Settings.ShowHitboxes || !triggerSpikesType.IsInstanceOfType(self)
                && !originalTriggerSpikesType.IsInstanceOfType(self))
            {
                orig(self, context, camera);
                return;
            }

            if (triggerSpikesType.IsInstanceOfType(self))
                DrawInstalledSpikesHitboxes((TriggerSpikesState)self, camera, context);
            else if (originalTriggerSpikesType.IsInstanceOfType(self))
                DrawOriginalSpikesHitboxes((EverestOriginalTriggerSpikesState)self, camera, context);
        }
    }
}
