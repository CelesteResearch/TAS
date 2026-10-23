using Celeste.Core.Display;
using System;
using System.Linq;
using System.Reflection;
using System.Numerics;
using MonoMod.RuntimeDetour;
using Celeste.Core.Rendering;
using Celeste.Core.Mathematics;
using Celeste.Core.Reflection.Members;
using Celeste.Scene.Entities;
using Celeste.Scene.Tracking;
using Celeste.Simulation;
using Celeste.Physics.Collision.Shapes;
using Celeste.Game.Entities.Birds;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Gameplay.Enemies.BadelineBoss.Projectiles;
using Celeste.Graphics.Rendering.Geometry;
using Celeste.Mods.Tas.Gameplay.Hitboxes;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;

namespace Celeste.Mods.Tas.Gameplay.Hitboxes
{
    internal sealed class SourceTasFinalBossHitboxProcess
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private Hook entityHook, hitboxHook;

        internal SourceTasFinalBossHitboxProcess(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        internal void Load()
        {
            entityHook = new Hook(SourceTasActorDebugMethods.Require(process, "Monocle.Entity", "DebugRender"),
                (Action<Action<SceneEntityState, ISourceRenderContext, object>, SceneEntityState, ISourceRenderContext,
                    object>)OnEntityDebugRender);
            hitboxHook = new Hook(SourceTasActorDebugMethods.Require(process, "Monocle.Hitbox", "Render"),
                (Action<Action<HitboxColliderGeometry, object, PackedColor, ISourceRenderContext>,
                    HitboxColliderGeometry,
                    object, PackedColor, ISourceRenderContext>)OnHitboxRender);
        }

        internal void Unload()
        {
            entityHook.Dispose();
            hitboxHook.Dispose();
        }

        public bool IsBeamCollidable(FinalBossBeamState beam) =>
            SourceTasFinalBossBeamHitboxPolicy.IsCollidable(beam, process);

        private void OnEntityDebugRender(Action<SceneEntityState, ISourceRenderContext, object> original,
            SceneEntityState self, ISourceRenderContext context, object camera)
        {
            original(self, context, camera);
            if (!driver.Settings.ShowHitboxes || self is not FinalBossBeamState beam) return;
            SourceTasFinalBossBeamHitboxPolicy.Draw(beam, process,
                driver.SourceActualCollideHitbox.LoadActualCollidable(beam),
                driver.Settings.UnCollidableHitboxesOpacity / 10f, new SourcePixelRectangleOutput(context, beam));
        }

        private void OnHitboxRender(Action<HitboxColliderGeometry, object, PackedColor, ISourceRenderContext> original,
            HitboxColliderGeometry self, object camera, PackedColor color, ISourceRenderContext context)
        {
            if (driver.Settings.ShowHitboxes && self.Entity is FinalBossShotState)
                color = new PackedColor(218, 165, 32, 255);
            original(self, camera, color, context);
        }
    }
}
