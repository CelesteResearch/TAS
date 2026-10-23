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
using Celeste.Mods.Tas.Reflection;

namespace Celeste.Mods.Tas.Gameplay.Hitboxes
{
    internal sealed class SourceTasConquerorBeamHitboxProcess
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private SourceTasGetDelegate<SceneEntityState, float> chargeTimer, activeTimer, angle;
        private SourceTasGetDelegate<SceneEntityState, SceneEntityState> boss;
        private Type conquerorBeamType;
        private Hook entityHook;

        internal SourceTasConquerorBeamHitboxProcess(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        internal void Initialize()
        {
            conquerorBeamType =
                driver.SourceModReflection.TryGetType("Conqueror's Peak",
                    "Celeste.Mod.ricky06ModPack.Entities.ConquerorBeam");
            if (conquerorBeamType != null)
            {
                chargeTimer =
                    driver.SourceFastReflection.CreateGetDelegate<SceneEntityState, float>(conquerorBeamType,
                        "chargeTimer");
                activeTimer =
                    driver.SourceFastReflection.CreateGetDelegate<SceneEntityState, float>(conquerorBeamType,
                        "activeTimer");
                angle = driver.SourceFastReflection.CreateGetDelegate<SceneEntityState, float>(conquerorBeamType,
                    "angle");
                boss = driver.SourceFastReflection.CreateGetDelegate<SceneEntityState, SceneEntityState>(
                    conquerorBeamType,
                    "boss");
                entityHook = new Hook(SourceTasActorDebugMethods.Require(process, "Monocle.Entity", "DebugRender"),
                    (Action<Action<SceneEntityState, ISourceRenderContext, object>, SceneEntityState,
                        ISourceRenderContext,
                        object>)ModHitbox);
            }
        }

        internal void Unload() => entityHook?.Dispose();

        private void ModHitbox(Action<SceneEntityState, ISourceRenderContext, object> original, SceneEntityState self,
            ISourceRenderContext context, object camera)
        {
            if (!driver.Settings.ShowHitboxes)
            {
                original(self, context, camera);
                return;
            }

            original(self, context, camera);
            if (self.GetType() == conquerorBeamType && chargeTimer(self) <= 0f && activeTimer(self) > 0f)
            {
                float sourceAngle = angle(self);
                SceneEntityState sourceBoss = boss(self);
                Vector2 from = sourceBoss.Center + Vector2Geometry.AngleToVector(sourceAngle, 12f);
                Vector2 to = sourceBoss.Center + Vector2Geometry.AngleToVector(sourceAngle, 2000f);
                var arithmetic = ((ISourceVectorEnvironment)process).VectorArithmetic;
                Vector2 perp = arithmetic.SafeNormalize(Vector2Geometry.Perpendicular(to - from), 2f);
                SourcePrimitiveDrawing.Line(context, self, from + perp, to + perp, driver.Settings.EntityHitboxColor,
                    arithmetic);
                SourcePrimitiveDrawing.Line(context, self, from - perp, to - perp, driver.Settings.EntityHitboxColor,
                    arithmetic);
                SourcePrimitiveDrawing.Line(context, self, from, to, driver.Settings.EntityHitboxColor, arithmetic);
            }
        }
    }
}
