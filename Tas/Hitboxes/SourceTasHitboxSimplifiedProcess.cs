using Celeste.Core.Display;
#nullable enable annotations
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Collections;
using Celeste.Core.Geometry;
using Celeste.Core.Rendering;
using Celeste.Core.Reflection.Members;
using Celeste.Everest.Reflection;
using Celeste.Scene.Entities;
using Celeste.Scene.Following;
using Celeste.Physics.Collision.Shapes;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Celeste.Game.Gameplay.Clutter.Blocks;
using Celeste.Game.Gameplay.Hazards.FireBalls;
using Celeste.Game.Entities.Platforms.WallBoosters;

namespace Celeste.Mods.Tas.Hitboxes
{
    public sealed partial class SourceTasHitboxSimplifiedProcess
    {
        private readonly TasPlaybackDriver driver;
        private readonly Lazy<Func<object, bool>?> GeckoHostile;
        private readonly Lazy<Func<object, bool>?> CustomClutterBlockBaseEnabled;
        private readonly Lazy<Func<object, bool>> fireBallIceMode;
        private readonly Lazy<Func<object, bool>> strawberryCollected;
        private readonly HashSet<Type> UselessTypes;

        private readonly HashSet<string> UselessTypeNames = new()
        {
            "ExtendedVariants.Entities.DashCountIndicator", "ExtendedVariants.Entities.JumpIndicator",
            "ExtendedVariants.Entities.Speedometer", "Celeste.Mod.JungleHelper.Entities.Firefly",
            "Celeste.Mod.ClutterHelper.CustomClutter", "Celeste.Mod.HonlyHelper.FloatyBgTile",
            "BrokemiaHelper.PixelRendered.Vineinator"
        };

        public Dictionary<FollowerState, bool> Followers = new();

        public SourceTasHitboxSimplifiedProcess(TasPlaybackDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            GeckoHostile = new(() =>
                GetOptionalBoolField("JungleHelper", "Celeste.Mod.JungleHelper.Entities.Gecko", "hostile"));
            CustomClutterBlockBaseEnabled = new(() =>
                GetOptionalBoolField("ClutterHelper", "Celeste.Mod.ClutterHelper.CustomClutterBlockBase", "enabled"));
            fireBallIceMode = new(() => RequireBoolField(typeof(OriginalFireBallState), "iceMode"));
            strawberryCollected = new(() => RequireBoolField(GameType("Celeste.Strawberry"), "collected"));
            UselessTypes = new()
            {
                GameType("Celeste.ClutterBlock"), GameType("Celeste.CrystalDebris"), GameType("Celeste.Debris"),
                GameType("Celeste.Door"), GameType("Celeste.FloatingDebris"), GameType("Celeste.HangingLamp"),
                GameType("Celeste.MoonCreature"), GameType("Celeste.MoveBlock+Debris"),
                GameType("Celeste.PlaybackBillboard"), GameType("Celeste.ResortLantern"),
                GameType("Celeste.Torch"), GameType("Celeste.Trapdoor")
            };
        }

        private SourceMemberCatalog Members =>
            ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMembers;

        private Type GameType(string name) => Members.GetActualSourceType(
                                                  Members.GetOriginalTypeDefinition(typeof(SceneEntityState))
                                                      .AssemblyIdentity, name)
                                              ?? throw new TypeLoadException(name);

        private Func<object, bool> RequireBoolField(Type type, string name) => SourceCompiledFieldGetter.Create<bool>(
                                                                                   Members.GetMember(type, name,
                                                                                       SourceMemberKind.Field,
                                                                                       BindingFlags.Public |
                                                                                       BindingFlags.NonPublic |
                                                                                       BindingFlags.Instance |
                                                                                       BindingFlags.Static, true))
                                                                               ?? throw new MissingFieldException(
                                                                                   Members.GetOriginalTypeFullName(
                                                                                       type),
                                                                                   name);

        private Func<object, bool>? GetOptionalBoolField(string module, string typeName, string field)
        {
            var type = driver.SourceModReflection.TryGetType(module, typeName);
            return type == null
                ? null
                : !Members.ContainsSourceType(type)
                    ? SourceCompiledFieldGetter.CreateReflected<bool>(
                        driver.SourceModReflection.GetFieldInfo(type, field))
                    : SourceCompiledFieldGetter.Create<bool>(Members.GetMember(type, field,
                        SourceMemberKind.Field,
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                        true));
        }

        private bool FireBallIceMode(object entity) => fireBallIceMode.Value(entity);

        private bool IsCollectedStrawberry(object entity) => GameType("Celeste.Strawberry").IsInstanceOfType(entity) &&
                                                             strawberryCollected.Value(entity);

        private bool IsSimplifiedHitboxes() => driver.Settings.ShowHitboxes && driver.Settings.SimplifiedHitboxes;

        private void DrawLine(ISourceRenderContext context, object entity, Vector2 a, Vector2 b, PackedColor color)
            => SourcePrimitiveDrawing.Line(context, entity, a, b, color, driver.Process.VectorArithmetic);

        public bool HideHitbox(SceneEntityState entity)
        {
            if (!driver.Settings.ShowHitboxes ||
                driver.GameInfo.WatchEntities.CurrentlyWatchedEntities.Contains(entity))
            {
                return false;
            }

            if (driver.SourceTriggerHitbox.ShouldHideHitbox(entity))
            {
                return true;
            }

            if (driver.Settings.SimplifiedHitboxes)
            {
                Type type = entity.GetType();
                if (UselessTypes.Contains(type))
                {
                    return true;
                }

                if (entity is OriginalClutterBlockBaseState)
                {
                    return !entity.Collidable;
                }

                if (entity.Get<FollowerState>() is { Leader: not null } follower &&
                    Followers.TryGetValue(follower, out bool delayed) && delayed)
                {
                    return true;
                }

                if (IsCollectedStrawberry(entity))
                {
                    return true;
                }

                switch (global::Celeste.Core.Reflection.Types.TypeReflectionIdentity.Resolve(Members, type)
                            .TypeIdentity)
                {
                    case "Celeste.Mod.JungleHelper.Entities.Gecko":
                        return false == GeckoHostile.Value?.Invoke(entity);
                    case "Celeste.Mod.ClutterHelper.CustomClutterBlockBase":
                        return false == CustomClutterBlockBaseEnabled.Value?.Invoke(entity);
                }
            }

            return false;
        }

        private void ModHitbox(Action<HitboxColliderGeometry, object, PackedColor, ISourceRenderContext> orig,
            HitboxColliderGeometry hitbox, object camera, PackedColor color, ISourceRenderContext context)
        {
            if (!driver.Settings.ShowHitboxes || !driver.Settings.SimplifiedHitboxes)
            {
                orig(hitbox, camera, color, context);
                return;
            }

            SceneEntityState entity = (SceneEntityState)hitbox.Entity;

            if (entity is OriginalFireBallState && !FireBallIceMode(entity) ||
                entity is OriginalWallBoosterState { IceMode: false })
            {
                return;
            }

            if (entity is OriginalWallBoosterState
                || (entity == null ? null : Members.GetOriginalTypeFullName(entity.GetType())) is
                "Celeste.Mod.ShroomHelper.Entities.SlippyWall"
                or "Celeste.Mod.ShroomHelper.Entities.AttachedIceWall"
                or "Celeste.Mod.JungleHelper.Entities.MossyWall"
                or "Celeste.Mod.CavernHelper.IcyFloor"
               )
            {
                color = driver.SourceHitboxColor.EntityColorInverselyLessAlpha;
            }

            orig(hitbox, camera, color, context);
        }

        private void CombineGridHitbox(Action<GridColliderGeometry, object, PackedColor, ISourceRenderContext> orig,
            GridColliderGeometry self, object camera, PackedColor color, ISourceRenderContext context)
        {
            if (!driver.Settings.ShowHitboxes || !driver.Settings.SimplifiedHitboxes)
            {
                orig(self, camera, color, context);
                return;
            }

            if (camera == null)
            {
                for (int x = 0; x < self.CellsX; ++x)
                {
                    for (int y = 0; y < self.CellsY; ++y)
                    {
                        DrawCombineHollowRect(context, self, color, x, y, 0, self.CellsX - 1, 0, self.CellsY - 1);
                    }
                }
            }
            else
            {
                int left = (int)Math.Max(0.0f,
                    (((ISourceCameraBounds)camera).Left - self.AbsoluteLeft) / self.CellWidth);
                int right = (int)Math.Min(self.CellsX - 1,
                    Math.Ceiling((((ISourceCameraBounds)camera).Right - (double)self.AbsoluteLeft) / self.CellWidth));
                int top = (int)Math.Max(0.0f, (((ISourceCameraBounds)camera).Top - self.AbsoluteTop) / self.CellHeight);
                int bottom = (int)Math.Min(self.CellsY - 1,
                    Math.Ceiling((((ISourceCameraBounds)camera).Bottom - (double)self.AbsoluteTop) / self.CellHeight));

                bool hackyFix = ((ISourceCameraBounds)camera).Left < self.AbsoluteLeft ||
                                ((ISourceCameraBounds)camera).Top < self.AbsoluteTop;

                for (int x = left; x <= right; ++x)
                {
                    for (int y = top; y <= bottom; ++y)
                    {
                        DrawCombineHollowRect(context, self, color, x, y, left, right, top, bottom, hackyFix);
                    }
                }
            }
        }

        private void DrawCombineHollowRect(ISourceRenderContext context, GridColliderGeometry grid, PackedColor color,
            int x, int y, int left, int right, int top, int bottom, bool hackyFix = false)
        {
            float topLeftX = grid.AbsoluteLeft + x * grid.CellWidth;
            float topLeftY = grid.AbsoluteTop + y * grid.CellHeight;
            Vector2 width = Vector2.UnitX * grid.CellWidth;
            Vector2 height = Vector2.UnitY * grid.CellHeight;

            Vector2 topLeft = new(topLeftX, topLeftY);
            Vector2 topRight = topLeft + width;
            Vector2 bottomLeft = topLeft + height;
            Vector2 bottomRight = topRight + height;

            bool drawnLeft = false, drawnRight = false, drawnTop = false, drawnBottom = false;

            VirtualMap<bool> data = grid.Data;

            if (data[x, y])
            {
                // left
                if ((x != left || hackyFix) && !data[x - 1, y])
                {
                    DrawLine(context, grid.Entity, topLeft + Vector2.One, bottomLeft + Vector2.UnitX - Vector2.UnitY,
                        color);
                    drawnLeft = true;
                }

                // right
                if (x == right || x + 1 <= right && !data[x + 1, y])
                {
                    DrawLine(context, grid.Entity, topRight + Vector2.UnitY, bottomRight - Vector2.UnitY, color);
                    drawnRight = true;
                }

                // top
                if ((y != top || hackyFix) && !data[x, y - 1])
                {
                    DrawLine(context, grid.Entity, topLeft + Vector2.UnitX, topRight - Vector2.UnitX, color);
                    drawnTop = true;
                }

                // bottom
                if (y == bottom || y + 1 <= bottom && !data[x, y + 1])
                {
                    DrawLine(context, grid.Entity, bottomLeft - Vector2.UnitY + Vector2.UnitX,
                        bottomRight - Vector2.One,
                        color);
                    drawnBottom = true;
                }

                // top left point
                if (drawnTop || drawnLeft)
                {
                    SourcePrimitiveDrawing.Point(context, grid.Entity, topLeft, color);
                }

                // top right point
                if (drawnTop || drawnRight)
                {
                    SourcePrimitiveDrawing.Point(context, grid.Entity, topRight - Vector2.UnitX, color);
                }

                // bottom left point
                if (drawnBottom || drawnLeft)
                {
                    SourcePrimitiveDrawing.Point(context, grid.Entity, bottomLeft - Vector2.UnitY, color);
                }

                // bottom right point
                if (drawnBottom || drawnRight)
                {
                    SourcePrimitiveDrawing.Point(context, grid.Entity, bottomRight - Vector2.One, color);
                }
            }
            else
            {
                // inner hollow top left point
                if (x - 1 >= left && y - 1 >= top && data[x - 1, y - 1] && data[x - 1, y] && data[x, y - 1])
                {
                    SourcePrimitiveDrawing.Point(context, grid.Entity, topLeft - Vector2.One, color);
                }

                // inner hollow top right point
                if (x + 1 <= right && y - 1 >= top && data[x + 1, y - 1] && data[x + 1, y] && data[x, y - 1])
                {
                    SourcePrimitiveDrawing.Point(context, grid.Entity, topRight - Vector2.UnitY, color);
                }

                // inner hollow bottom left point
                if (x - 1 >= left && y + 1 <= bottom && data[x - 1, y + 1] && data[x - 1, y] && data[x, y + 1])
                {
                    SourcePrimitiveDrawing.Point(context, grid.Entity, bottomLeft - Vector2.UnitX, color);
                }

                // inner hollow bottom right point
                if (x + 1 <= right && y + 1 >= top && data[x + 1, y + 1] && data[x + 1, y] && data[x, y + 1])
                {
                    SourcePrimitiveDrawing.Point(context, grid.Entity, bottomRight, color);
                }
            }
        }
    }
}
