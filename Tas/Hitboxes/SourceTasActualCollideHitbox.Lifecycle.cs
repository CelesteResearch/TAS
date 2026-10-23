using Celeste.Core.Display;
using System;
using System.Collections.Generic;
using System.Reflection;
using Celeste.Core.Rendering;
using Celeste.Core.Scheduling;
using Celeste.Everest.Reflection;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Gameplay.Enemies.BadelineBoss.Projectiles;
using Celeste.Physics.Collision.Shapes;
using Celeste.Scene.Tracking;
using Celeste.Simulation;
using Celeste.Graphics.Display;
using MonoMod.RuntimeDetour;

namespace Celeste.Mods.Tas.Hitboxes
{
    public sealed partial class SourceTasActualCollideHitbox
    {
        private bool sourceRecordingInstalled;
        private Hook playerUpdateHook, entityListUpdateHook, playerDebugRenderHook;
        private Hook hitboxRenderHook, circleRenderHook, colliderListRenderHook, levelEndHook;

        private MethodInfo Method(Type type, string name)
        {
            return global::Celeste.Mods.Tas.Reflection.SourceTasDeclaredHookTargets.Require(
                driver.Process, type, name);
        }

        public void Load()
        {
            sourceRecordingInstalled = true;
            playerUpdateHook = new Hook(Method(typeof(RoomPlayerSession), "Update"), (PlayerUpdateHook)OnPlayerUpdate);
            var reflection = ((ISourceEverestReflectionEnvironment)driver.Process).Reflection;
            entityListUpdateHook = new Hook(reflection.SourceMethods.RequireActualMethod(
                    "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", 100665909),
                (EntityListUpdateHook)OnEntityListUpdate);
            playerDebugRenderHook = new Hook(Method(typeof(RoomPlayerSession), "DebugRender"),
                (PlayerDebugRenderHook)OnPlayerDebugRenderHook);
            hitboxRenderHook = new Hook(Method(typeof(HitboxColliderGeometry), "Render"),
                (HitboxRenderHook)OnHitboxRender);
            circleRenderHook = new Hook(Method(typeof(CircleColliderGeometry), "Render"),
                (CircleRenderHook)OnCircleRender);
            colliderListRenderHook = new Hook(Method(typeof(ColliderListGeometry), "Render"),
                (ColliderListRenderHook)OnColliderListRender);
            levelEndHook = new Hook(Method(typeof(OriginalLevelSceneRoot), "End"), (LevelEndHook)OnLevelEnd);
        }

        public void Unload()
        {
            sourceRecordingInstalled = false;
            playerUpdateHook?.Dispose();
            entityListUpdateHook?.Dispose();
            playerDebugRenderHook?.Dispose();
            hitboxRenderHook?.Dispose();
            circleRenderHook?.Dispose();
            colliderListRenderHook?.Dispose();
            levelEndHook?.Dispose();
        }

        internal void RecordSourcePhase(object actualEntity)
        {
            if (sourceRecordingInstalled) StoreActualColliderState(actualEntity);
        }

        internal void RecordOtherSourcePhase(object actualPlayer)
        {
            if (!sourceRecordingInstalled) return;
            var player = (RoomPlayerSession)actualPlayer;
            foreach (var beam in ((Celeste.Scene.Tracking.ISourceScene)player.Scene).SourceScene.Entities
                     .GetTrackedEntities("Celeste.FinalBossBeam"))
                StoreActualColliderState(beam);
        }

        private delegate void PlayerUpdateOriginal(RoomPlayerSession self);

        private delegate void PlayerUpdateHook(PlayerUpdateOriginal orig, RoomPlayerSession self);

        private void OnPlayerUpdate(PlayerUpdateOriginal orig, RoomPlayerSession self)
        {
            orig(self);
            playerUpdated = true;
        }

        private delegate void EntityListUpdateOriginal(SceneEntityRegistry self, ISourceEngineContext engine);

        private delegate void EntityListUpdateHook(EntityListUpdateOriginal orig, SceneEntityRegistry self,
            ISourceEngineContext engine);

        private void OnEntityListUpdate(EntityListUpdateOriginal orig, SceneEntityRegistry self,
            ISourceEngineContext engine)
        {
            Clear();
            orig(self, engine);
        }

        private delegate void
            PlayerDebugRenderOriginal(RoomPlayerSession self, ISourceRenderContext context, object camera);

        private delegate void PlayerDebugRenderHook(PlayerDebugRenderOriginal orig, RoomPlayerSession self,
            ISourceRenderContext context, object camera);

        private void OnPlayerDebugRenderHook(PlayerDebugRenderOriginal orig, RoomPlayerSession self,
            ISourceRenderContext context, object camera)
            => OnPlayerDebugRender(() => orig(self, context, camera), self, context);

        private delegate void HitboxRenderOriginal(HitboxColliderGeometry self, object camera, PackedColor color,
            ISourceRenderContext context);

        private delegate void HitboxRenderHook(HitboxRenderOriginal orig, HitboxColliderGeometry self, object camera,
            PackedColor color, ISourceRenderContext context);

        private void OnHitboxRender(HitboxRenderOriginal orig, HitboxColliderGeometry self, object camera,
            PackedColor color, ISourceRenderContext context)
            => DrawActualCollider(self, color, actualColor => orig(self, camera, actualColor, context));

        private delegate void CircleRenderOriginal(CircleColliderGeometry self, object camera, PackedColor color,
            ISourceRenderContext context);

        private delegate void CircleRenderHook(CircleRenderOriginal orig, CircleColliderGeometry self, object camera,
            PackedColor color, ISourceRenderContext context);

        private void OnCircleRender(CircleRenderOriginal orig, CircleColliderGeometry self, object camera,
            PackedColor color, ISourceRenderContext context)
            => DrawActualCollider(self, color, actualColor => orig(self, camera, actualColor, context));

        private delegate void ColliderListRenderOriginal(ColliderListGeometry self, object camera, PackedColor color,
            ISourceRenderContext context);

        private delegate void ColliderListRenderHook(ColliderListRenderOriginal orig, ColliderListGeometry self,
            object camera, PackedColor color, ISourceRenderContext context);

        private void OnColliderListRender(ColliderListRenderOriginal orig, ColliderListGeometry self, object camera,
            PackedColor color, ISourceRenderContext context)
        {
            colliderListRendering = true;
            DrawActualCollider(self, color, actualColor => orig(self, camera, actualColor, context));
            colliderListRendering = false;
        }

        private delegate void LevelEndOriginal(OriginalLevelSceneRoot self);

        private delegate void LevelEndHook(LevelEndOriginal orig, OriginalLevelSceneRoot self);

        private void OnLevelEnd(LevelEndOriginal orig, OriginalLevelSceneRoot self)
        {
            Clear();
            orig(self);
        }
    }
}
