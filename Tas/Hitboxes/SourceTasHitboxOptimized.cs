using Celeste.Core.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Components;
using Celeste.Core.Rendering;
using Celeste.Core.Geometry;
using Celeste.Core.Mathematics;
using Celeste.Physics.Collision;
using Celeste.Physics.Collision.Shapes;
using Celeste.Scene.Entities;
using Celeste.Scene.Tracking;
using Celeste.Scene.Scenes;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Celeste.Graphics.Geometry.TwoD.Camera;
using Celeste.Simulation;
using Celeste.Simulation.Gameplay.Player.Intro;
using Celeste.Mods.Tas.Reflection;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;
using Puffer = Celeste.Game.Gameplay.Hazards.Puffer.OriginalPufferState;
using SwitchGate = Celeste.Game.Gameplay.Mechanisms.SwitchGates.OriginalSwitchGateState;
using IntroCrusher = Celeste.Game.Gameplay.Platforms.IntroCrusher.OriginalIntroCrusherState;
using FinalBossBeam = Celeste.Game.Gameplay.Enemies.BadelineBoss.Projectiles.FinalBossBeamState;
using Glider = Celeste.Game.Gameplay.Holdables.Gliders.OriginalGliderState;
using Seeker = Celeste.Game.Gameplay.Enemies.Seekers.OriginalSeekerState;
using LockBlock = Celeste.Game.Entities.Doors.LockBlocks.OriginalLockBlockState;
using FireBall = Celeste.Game.Gameplay.Hazards.FireBalls.OriginalFireBallState;
using SolidTiles = Celeste.Game.Execution.LevelLoader.Terrain.OriginalSolidTiles;
using PlayerCollider = Celeste.Simulation.Gameplay.Player.Collision.Components.PlayerColliderComponentState;
using Holdable = Celeste.Simulation.Gameplay.Holdables.HoldableState;
using HoldableCollider = Celeste.Simulation.Gameplay.Holdables.HoldableColliderState;
using SoundSource = Celeste.Simulation.Audio.Sources.SoundSourceComponentState;

namespace Celeste.Mods.Tas.Hitboxes
{
    internal sealed partial class SourceTasHitboxOptimized
    {
        private readonly TasPlaybackDriver driver;

        internal SourceTasHitboxOptimized(TasPlaybackDriver driver)
        {
            this.driver = driver;
        }

        private ISourceRenderContext Context => ((ISourceRenderEnvironment)driver.Process).CurrentContext;
        private ISourceVectorArithmetic Arithmetic => ((ISourceVectorEnvironment)driver.Process).VectorArithmetic;
        private SourceTasHitboxColor Colors => driver.SourceHitboxColor;

        private T Field<T>(object entity, string field) =>
            (T)driver.SourceModReflection.GetFieldInfo(entity.GetType(), field).Read(entity);

        private void SetField(object entity, string field, object value) =>
            driver.SourceModReflection.GetFieldInfo(entity.GetType(), field).Write(entity, value);

        private static Vector2 Position(object entity) => ((ISourceEntityPosition)entity).Position;
        private static ColliderGeometry Collider(object entity) => ((ISourceEntityCollider)entity).Collider;
        private static bool Collidable(object entity) => ((ISourceEntityCollider)entity).Collidable;

        private static void Collidable(object entity, bool value) =>
            ((ISourceMutableEntityState)entity).Collidable = value;

        private static Level Scene(object entity) => (Level)((ISourceEntityScene)entity).Scene;

        private static Vector2 Center(object entity) =>
            Collider(entity) is { } collider ? collider.Center + Position(entity) : Position(entity);

        private static RoomPlayerSession GetPlayer(Level level) => level.SourceScene.Entities
            .GetTrackedEntities(typeof(global::Celeste.Simulation.RoomPlayerSession).FullName).Cast<RoomPlayerSession>()
            .FirstOrDefault();


        private static T Component<T>(object entity) where T : SourceComponentState =>
            ((ISourceOwnedComponentList)entity).OwnedComponents.Get<T>();

        private void Circle(Vector2 position, float radius, PackedColor color, int resolution) =>
            SourcePrimitiveDrawing.Circle((Celeste.Graphics.Rendering.Drawing.ISourceDrawScope)Context, position,
                radius,
                color, resolution, Arithmetic);

        private void Rect(float x, float y, float w, float h, PackedColor color) =>
            SourcePrimitiveDrawing.Rect(Context, this, x, y, w, h, color);

        private void HollowRect(Vector2 at, float w, float h, PackedColor color) =>
            SourcePrimitiveDrawing.HollowRect(Context, this, at, w, h, color);

        private void HollowRect(float x, float y, float w, float h, PackedColor color) =>
            SourcePrimitiveDrawing.HollowRect(Context, this, x, y, w, h, color);

        private void Line(Vector2 from, Vector2 to, PackedColor color) =>
            SourcePrimitiveDrawing.Line(Context, this, from, to, color, Arithmetic);

        private readonly List<CircleColliderGeometry> pufferPushRadius = new();

        private bool IsShowHitboxes()
        {
            return driver.Settings.ShowHitboxes;
        }

        // Lifecycle and exact physical hook registrations are in the Hooks partial.
        private void ModDebugRender(EntityDebugOriginal orig, object self, ISourceRenderContext context,
            object actualCamera)
        {
            var camera = (CameraState)actualCamera;
            if (!driver.Settings.ShowHitboxes)
            {
                orig(self, context, actualCamera);
                return;
            }

            // Do not draw hitboxes of entities outside the camera
            if (Collider(self) is not GridColliderGeometry && self is not FinalBossBeam)
            {
                int width = camera.Viewport.Width;
                int height = camera.Viewport.Height;
                IntRect bounds = new((int)camera.Left - width / 2, (int)camera.Top - height / 2, width * 2, height * 2);
                if ((Collider(self)?.AbsoluteRight ?? Position(self).X) < bounds.Left ||
                    (Collider(self)?.AbsoluteLeft ?? Position(self).X) > bounds.Right ||
                    (Collider(self)?.AbsoluteTop ?? Position(self).Y) > bounds.Bottom ||
                    (Collider(self)?.AbsoluteBottom ?? Position(self).Y) < bounds.Top)
                {
                    return;
                }
            }

            switch (self)
            {
                case Puffer puffer:
                    DrawPufferHitbox(puffer);
                    break;
                case SwitchGate switchGate:
                    DrawSwitchGateEnd(switchGate);
                    break;
                case IntroCrusher introCrusher:
                    DrawIntroCrusherEnd(introCrusher);
                    break;
            }

            orig(self, context, actualCamera);

            if (self is Puffer puffer2)
            {
                DrawPufferLaunchOrBounceIndicator(puffer2);
                // render this above original hitboxes
            }
        }

        private int HidePufferWhiteLine(int i)
        {
            if (driver.Settings.ShowHitboxes)
            {
                return 28;
            }
            else
            {
                return i;
            }
        }

        private void DrawPufferHitbox(Puffer puffer)
        {
            /*
             * ProximityExplodeCheck: player.CenterY >= base.Y + collider.Bottom - 4f
             *
             * CenterY can be half integer if crouched
             * base.Y is not integer
             *
             * Draw.Line: round to integer, plus an annoying offset depending on angle
             * Draw.Rect: trunc to integer, quite stable
             */

            /*
             * we pretend we are using Hurtbox in collide check, and assume Player's position is on grid
             * b = Hurtbox.Bottom = [player's Position] + ...
             * b'= Rendered Hurtbox Bottom = b - 1
             * c = player.CenterY = [player's Position] + (Hitbox.Top + Hitbox.Height/2)
             * int i = height of Draw.Line
             * i - b + c >= base.Y + collider.Bottom - 4f = puffer.Bottom - 4f
             * i = Ceil(puffer.Bottom - 4f + b' - c)
             *
             * in some weird cases, maddy can have starFlyHitbox + normalHurtbox...so we can't just check Ducking and StateMachine.State == 19
             */

            var player = GetPlayer(Scene(puffer));
            float b = (player == null ? null : Field<ColliderGeometry>(player, "hurtbox"))?.Bottom ?? -2f;
            float c = (player == null ? null : Collider(player))?.CenterY ?? -5.5f;
            Vector2 bottomCenter = new Vector2(puffer.CenterX, (float)Math.Ceiling(puffer.Bottom - 5f + b - c));
            PackedColor hitboxColor = Colors.GetCustomColor(puffer);
            Circle(puffer.Position, 32f, hitboxColor, 32);
            PackedColor heightCheckColor = SourceTasHitboxColor.PufferHeightCheckColor *
                                           (puffer.Collidable ? 1f : Colors.UnCollidableAlpha);
            Rect(bottomCenter.X - 7, bottomCenter.Y, -25f, 1f, heightCheckColor);
            Rect(bottomCenter.X + 7, bottomCenter.Y, 25f, 1f, heightCheckColor);
            // sometimes it will draw an extra pixel at the endpoint..

            /*
             * still one small issue remains: we are pretending that all collide checks are using player's hurtbox
             * but for collide check with the circle detectRadius
             * current implementation can't hold if player's hitbox is starFlyHitbox (which is 1px wider than hurtbox on twosides)
             */
        }

        private void DrawSwitchGateEnd(SwitchGate gate)
        {
            if (gate.Position != Field<Vector2>(gate, "node") && Collider(gate) is { } collider)
            {
                PackedColor color = Colors.PlatformColor * Colors.UnCollidableAlpha;
                HollowRect(Field<Vector2>(gate, "node"), collider.Width, collider.Height, color);
                if (gate.Scene is { } scene &&
                    Celeste.Game.Components.Switches.OriginalSwitchComponent.Check(scene))
                {
                    Line(gate.Center, gate.Center + Field<Vector2>(gate, "node") - gate.Position, color);
                }
            }
        }

        private void DrawIntroCrusherEnd(IntroCrusher crusher)
        {
            if (crusher.Position != Field<Vector2>(crusher, "end") && Collider(crusher) is { } collider)
            {
                PackedColor color = Colors.PlatformColor * Colors.UnCollidableAlpha;
                HollowRect(Field<Vector2>(crusher, "end"), collider.Width, collider.Height, color);
                if (crusher.Position != Field<Vector2>(crusher, "start"))
                {
                    Line(crusher.Center, crusher.Center + Field<Vector2>(crusher, "end") - crusher.Position, color);
                }
            }
        }

        private void DrawPufferLaunchOrBounceIndicator(Puffer puffer)
        {
            // OnPlayer Explode: player.Bottom > lastSpeedPosition.Y + 3f
            if (Component<PlayerCollider>(puffer) is { } pc &&
                driver.SourceModReflection.GetFieldInfo(typeof(Puffer), "lastSpeedPosition", logFailure: false) is
                    { } getLastSpeedPosition)
            {
                float y = ((Vector2)getLastSpeedPosition.Read(puffer)).Y + 3f - 1f;
                // -1f coz player's bottom is "1px lower" than the bottom of hitbox (due to how they render)
                float z = (float)Math.Ceiling(y);
                if (z <= y)
                {
                    z += 1f;
                }

                // it seems pc.Entity will be null, so we have to manually express pc.Collider.AbsoluteBottom
                if (z > pc.Collider.Bottom + puffer.Y)
                {
                    return;
                }

                float top = Math.Max(pc.Collider.Top + puffer.Y, z);
                HollowRect(puffer.X - 7f, top, 14f, pc.Collider.Bottom + puffer.Y - top,
                    puffer.Collidable
                        ? SourceTasHitboxColor.PufferHeightCheckColor
                        : SourceTasHitboxColor.PufferHeightCheckColor * Colors.UnCollidableAlpha);
            }
        }

        private void EntityListOnDebugRender(EntityListDebugOriginal orig, SceneEntityRegistry self,
            ISourceRenderContext context, object actualCamera, object actualScene)
        {
            var camera = (CameraState)actualCamera;
            Level? level = actualScene as Level;
            if (driver.Settings.ShowHitboxes && level != null)
            {
                AddSpawnPointHitbox(level);
            }

            orig(self, context, actualCamera, actualScene);

            if (driver.Settings.ShowHitboxes && level != null)
            {
                AddHoldableColliderHitbox(level, camera);
                AddLockBlockColliderHitbox(level);
                AddPufferPushRadius();
            }
        }

        private void AddSpawnPointHitbox(Level level)
        {
            foreach (Vector2 spawn in ((Celeste.Game.Content.Maps.Levels.OriginalLevelRoomData)level.Session
                         .LevelData).Spawns)
            {
                HollowRect(spawn - new Vector2(4, 11), 8, 11,
                    SourceTasHitboxColor.RespawnTriggerColor * Colors.UnCollidableAlpha
                );
            }
        }

        private void AddHoldableColliderHitbox(Level level, CameraState camera)
        {
            List<Holdable> holdables = level.SourceScene.Components.GetTrackedComponents("Celeste.Holdable")
                .Cast<Holdable>().ToList();
            if (holdables.Count == 0)
            {
                return;
            }

            if (holdables.All(holdable => holdable.Entity is Glider && holdable.IsHeld))
            {
                return;
            }

            foreach (HoldableCollider component in level.SourceScene.Components
                         .GetTrackedComponents("Celeste.HoldableCollider").Cast<HoldableCollider>().ToList())
            {
                if (Field<ColliderGeometry>(component, "collider") is not { } collider || component.Entity is Seeker)
                {
                    continue;
                }

                object entity = component.Entity;

                holdables.Sort((a, b) =>
                    Vector2.DistanceSquared(Position(a.Entity), Position(entity)) >
                    Vector2.DistanceSquared(Position(b.Entity), Position(entity))
                        ? 1
                        : 0);
                Holdable firstHoldable = holdables.First();
                if (firstHoldable.IsHeld && firstHoldable.Entity is Glider)
                {
                    continue;
                }

                PackedColor color = firstHoldable.Entity is Glider
                    ? new PackedColor(104, 142, 255)
                    : new PackedColor(89, 177, 147);
                if (!Collidable(entity))
                {
                    color *= Colors.UnCollidableAlpha;
                }

                ColliderGeometry origCollider = Collider(entity);
                ((ISourceEntityCollider)entity).Collider = collider;
                collider.Render(camera, color, Context);
                ((ISourceEntityCollider)entity).Collider = origCollider;
            }
        }

        private void AddLockBlockColliderHitbox(Level level)
        {
            if (GetPlayer(level) is not { } player)
            {
                return;
            }

            var lockBlocks = level.SourceScene.Entities.GetEntitiesTrackIfNeeded<LockBlock>();
            if (lockBlocks.Count == 0)
            {
                return;
            }

            ColliderGeometry origCollider = player.Collider;
            player.Collider = Field<ColliderGeometry>(player, "hurtbox");
            Vector2 playerCenter = player.Center;
            player.Collider = origCollider;

            foreach (var entity in lockBlocks)
            {
                var lockBlock = (LockBlock)entity;
                if (Component<PlayerCollider>(lockBlock) is not { Collider: CircleColliderGeometry circle })
                {
                    continue;
                }

                if (Vector2.Distance(playerCenter, lockBlock.Center) > circle.Radius * 1.5)
                {
                    continue;
                }

                PackedColor color = PackedColor.HotPink;
                if (Field<bool>(lockBlock, "opening"))
                {
                    color = PackedColor.Aqua;
                }

                bool origCollidable = lockBlock.Collidable;
                lockBlock.Collidable = false;

                List<object> solidTilesList =
                    ((Level)driver.Process.Frame.Scenes.CurrentScene).SourceScene.Entities.GetTrackedEntities(
                        "Celeste.SolidTiles");
                Dictionary<object, bool> solidTilesCollidableDict =
                    solidTilesList.ToDictionary(e => e, e => Collidable(e));

                // check if the line collides with any solid except solid tiles
                solidTilesList.ForEach(e => Collidable(e, false));
                bool collideSolid = SourceSceneCollision.CheckLine(
                    ((Level)driver.Process.Frame.Scenes.CurrentScene).SourceScene.Entities, "Celeste.Solid",
                    playerCenter,
                    lockBlock.Center);

                // check if the line collides with solid tiles
                solidTilesList.ForEach(e => Collidable(e, solidTilesCollidableDict[e]));
                bool collideSolidTiles = SourceSceneCollision.CheckLine(
                    ((Level)driver.Process.Frame.Scenes.CurrentScene).SourceScene.Entities, "Celeste.SolidTiles",
                    playerCenter, lockBlock.Center);

                lockBlock.Collidable = origCollidable;

                if (collideSolid || collideSolidTiles)
                {
                    color *= Colors.UnCollidableAlpha;
                }

                if (!collideSolid)
                {
                    // draw actual checked tiles when checking collision between line and solid tiles
                    solidTilesList.ForEach(e =>
                    {
                        if (e is SolidTiles { Collidable: true } solidTiles)
                        {
                            GridColliderGeometry grid = solidTiles.Grid;
                            SourceTasGridLineCollision
                                .GetCheckedTilesInLineCollision(grid, playerCenter, lockBlock.Center)
                                .ForEach(tuple => HollowRect(tuple.Item1, grid.CellWidth, grid.CellHeight,
                                    PackedColor.HotPink * (tuple.Item2 ? 1f : Colors.UnCollidableAlpha)));
                        }
                    });
                }

                Line(playerCenter, lockBlock.Center, color);
            }
        }

        private void AddPufferPushRadius()
        {
            foreach (CircleColliderGeometry circle in pufferPushRadius)
            {
                Circle(circle.Position, circle.Radius, SourceTasHitboxColor.PufferPushRadiusColor, 4);
            }
        }

        private PackedColor OptimizePlayerColliderHitbox(PackedColor color, SourceComponentState component)
        {
            return Collidable(component.Entity) ? color : color * Colors.UnCollidableAlpha;
        }

        private void AddFeatherHitbox(PlayerColliderDebugOriginal orig, PlayerCollider self,
            ISourceRenderContext context,
            object actualCamera)
        {
            orig(self, context, actualCamera);
            if (driver.Settings.ShowHitboxes && self.FeatherCollider != null &&
                GetPlayer((Level)self.Scene) is { } player &&
                player.Machine.State == 19)
            {
                ColliderGeometry collider = Collider(self.Entity);
                ((ISourceEntityCollider)self.Entity).Collider = self.FeatherCollider;
                self.FeatherCollider.Render(actualCamera,
                    PackedColor.HotPink * (Collidable(self.Entity) ? 1 : Colors.UnCollidableAlpha), context);
                ((ISourceEntityCollider)self.Entity).Collider = collider;
            }
        }

        private void CircleOnRender(CircleRenderOriginal orig, CircleColliderGeometry self, object camera,
            PackedColor color, ISourceRenderContext context)
        {
            if (!driver.Settings.ShowHitboxes)
            {
                orig(self, camera, color, context);
                return;
            }

            if (self.Entity is FireBall fireball && !Field<bool>(fireball, "iceMode"))
            {
                color = PackedColor.Goldenrod;
            }

            orig(self, camera, color, context);
        }

        private void SoundSource_DebugRender(SoundDebugOriginal orig, SoundSource self, ISourceRenderContext context,
            object camera)
        {
            if (!driver.Settings.ShowHitboxes)
            {
                orig(self, context, camera);
            }
        }

        private PackedColor OptimizeSeekerHitbox1(PackedColor color, object entity)
        {
            if (!driver.Settings.ShowHitboxes)
            {
                return color;
            }

            return Collidable(entity) ? Colors.EntityColor : Colors.EntityColor * Colors.UnCollidableAlpha;
        }

        private PackedColor OptimizeSeekerHitbox2(PackedColor color, object entity)
        {
            if (!driver.Settings.ShowHitboxes)
            {
                return color;
            }

            return Collidable(entity) ? PackedColor.HotPink : PackedColor.HotPink * Colors.UnCollidableAlpha;
        }

        private void SeekerOnDebugRender(SeekerDebugOriginal orig, Seeker self, ISourceRenderContext context,
            object camera)
        {
            orig(self, context, camera);

            if (!driver.Settings.ShowHitboxes)
            {
                return;
            }

            ColliderGeometry origCollider = self.Collider;

            if (Field<ColliderGeometry>(self, "physicsHitbox") is { } physicsHitbox)
            {
                self.Collider = physicsHitbox;
                physicsHitbox.Render(camera, PackedColor.Goldenrod, context);
            }

            if (self.Regenerating && Field<CircleColliderGeometry>(self, "pushRadius") is { } pushRadius)
            {
                self.Collider = pushRadius;
                pushRadius.Render(camera, Colors.EntityColor, context);
            }

            self.Collider = origCollider;

            if (!self.Regenerating && self.Scene is Level sourceLevel &&
                Field<List<Vector2>>(sourceLevel.Pathfinder, "lastPath") is { Count: >= 2 } lastPath)
            {
                Vector2 start = lastPath[0];
                for (int i = 1; i < lastPath.Count; i++)
                {
                    Vector2 vector = lastPath[i];
                    Line(start, vector, PackedColor.Goldenrod * Colors.UnCollidableAlpha);
                    start = vector;
                }
            }
        }

        private void LevelOnLoadLevel(LevelLoadOriginal orig, Level self, PlayerIntroType playerIntro,
            bool isFromLoader)
        {
            orig(self, playerIntro, isFromLoader);

            if (driver.Settings.ShowHitboxes && self.Pathfinder is { } pathfinder &&
                Field<List<Vector2>>(pathfinder, "lastPath") != null)
            {
                SetField(pathfinder, "lastPath", null);
            }
        }

        private void SceneOnBeforeUpdate(SceneBeforeUpdateOriginal orig, SourceSceneState self)
        {
            orig(self);
            pufferPushRadius.Clear();
        }
    }
}
