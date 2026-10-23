using System;
using System.Linq;
using Celeste.Core.Scheduling;
using Celeste.Everest.Reflection;
using Celeste.Game.Execution.Engine;
using Celeste.Scene.Entities;
using Celeste.Scene.Scheduling;
using MonoMod.RuntimeDetour;
using global::Celeste.Game.Execution;

namespace Celeste.Mods.Tas.Hitboxes
{
    internal sealed partial class SourceTasTriggerHitbox
    {
        private Hook updateListsHook, sceneTransitionHook;

        private delegate void UpdateListsOriginal(SceneUpdateScheduler self);

        private delegate void UpdateListsHook(UpdateListsOriginal orig, SceneUpdateScheduler self);

        private delegate void SceneTransitionOriginal(EngineRuntime self,
            ISourceSceneExecution from, ISourceSceneExecution to);

        private delegate void SceneTransitionHook(SceneTransitionOriginal orig,
            EngineRuntime self, ISourceSceneExecution from, ISourceSceneExecution to);

        public void Load()
        {
            var reflection = ((ISourceEverestReflectionEnvironment)driver.Process).Reflection;
            string assembly = reflection.SourceMembers.GetOriginalTypeDefinition(typeof(SceneEntityState))
                .AssemblyIdentity;
            var update = reflection.SourceMethods.EnumerateDeclaredDefinitions(assembly, "Monocle.EntityList")
                .Single(m => m.Name == "UpdateLists");
            updateListsHook = new Hook(
                reflection.SourceMethods.RequireActualMethod(assembly, update.MethodDefinitionToken),
                (UpdateListsHook)OnEntityListUpdateLists);
            var transition = global::Celeste.Mods.Tas.Reflection.SourceTasDeclaredHookTargets.Require(
                driver.Process,
                typeof(EngineRuntime), "OnSceneTransition", new[] { "Monocle.Scene", "Monocle.Scene" });
            sceneTransitionHook = new Hook(transition, (SceneTransitionHook)OnEngineSceneTransition);
        }

        public void Unload()
        {
            updateListsHook?.Dispose();
            sceneTransitionHook?.Dispose();
        }

        public void LoadState() => RecacheTriggers(driver.Process.Frame.Scenes.CurrentScene);

        private void OnEntityListUpdateLists(UpdateListsOriginal orig, SceneUpdateScheduler self)
        {
            if (driver.Settings.SimplifiedHitboxes)
            {
                currentUnimportantTriggers.RemoveWhere(entity => self.GetToRemove().Contains(entity));
                foreach (var entity in self.GetToAdd())
                    if (CheckUnimportant(entity))
                        currentUnimportantTriggers.Add(entity);
            }

            orig(self);
        }

        private void OnEngineSceneTransition(SceneTransitionOriginal orig, EngineRuntime self,
            ISourceSceneExecution from, ISourceSceneExecution to)
        {
            if (driver.Settings.SimplifiedHitboxes) RecacheTriggers(to);
            orig(self, from, to);
        }
    }
}
