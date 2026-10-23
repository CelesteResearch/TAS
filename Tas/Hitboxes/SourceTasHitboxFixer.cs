using Celeste.Core.Display;
using System;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Rendering;
using Celeste.Core.Geometry;
using Celeste.Core.Mathematics;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Celeste.Graphics.Rendering.Drawing;
using Celeste.Graphics.Rendering.Textures;
using Celeste.Graphics.Geometry.TwoD.Camera;
using Celeste.Physics.Collision.Shapes;
using Celeste.Scene.Entities;
using Celeste.Game.Presentation.Rendering;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;

namespace Celeste.Mods.Tas.Hitboxes
{
    internal sealed partial class SourceTasHitboxFixer
    {
        private readonly TasPlaybackDriver driver;
        private Hook hollowHook;
        private Hook circleHook;
        private ILHook gameplayHook;
        private ISourceRenderContext Context => ((ISourceRenderEnvironment)driver.Process).CurrentContext;
        private ISourceDrawScope Scope => (ISourceDrawScope)Context;
        private ISourceVectorArithmetic Arithmetic => ((ISourceVectorEnvironment)driver.Process).VectorArithmetic;

        internal SourceTasHitboxFixer(TasPlaybackDriver driver)
        {
            this.driver = driver;
        }

        public bool DrawingHitboxes { get; set; }

        internal void Load()
        {
            hollowHook = new Hook(
                typeof(SourcePrimitiveDrawing).GetMethod(nameof(SourcePrimitiveDrawing.HollowRect),
                    new[]
                    {
                        typeof(ISourceDrawScope), typeof(float), typeof(float), typeof(float), typeof(float),
                        typeof(PackedColor)
                    }), (HollowRectHook)ModDrawHollowRect);
            circleHook = new Hook(
                typeof(SourcePrimitiveDrawing).GetMethod(nameof(SourcePrimitiveDrawing.Circle),
                    new[]
                    {
                        typeof(ISourceDrawScope), typeof(Vector2), typeof(float), typeof(PackedColor), typeof(int),
                        typeof(ISourceVectorArithmetic)
                    }), (CircleHook)ModDrawCircle);
            gameplayHook = new ILHook(
                typeof(GameplayRendererState).GetMethod(nameof(GameplayRendererState.Render), new[] { typeof(object) }),
                il =>
                {
                    var cursor = new ILCursor(il);
                    if (cursor.TryGotoNext(i =>
                            i.Operand is Mono.Cecil.MethodReference m && m.Name == "DebugRenderEntities"))
                    {
                        cursor.EmitDelegate((Action)SetDrawingHitboxesTrue);
                        cursor.Index++;
                        cursor.EmitDelegate((Action)SetDrawingHitboxesFalse);
                    }
                });
        }

        internal void Unload()
        {
            gameplayHook.Dispose();
            hollowHook.Dispose();
            circleHook.Dispose();
        }

        private delegate void HollowRectOriginal(ISourceDrawScope scope, float x, float y, float width, float height,
            PackedColor color);

        private delegate void HollowRectHook(HollowRectOriginal orig, ISourceDrawScope scope, float x, float y,
            float width,
            float height, PackedColor color);

        private delegate void CircleOriginal(ISourceDrawScope scope, Vector2 center, float radius, PackedColor color,
            int resolution, ISourceVectorArithmetic arithmetic);

        private delegate void CircleHook(CircleOriginal orig, ISourceDrawScope scope, Vector2 center, float radius,
            PackedColor color, int resolution, ISourceVectorArithmetic arithmetic);

        private void SetDrawingHitboxesTrue()
        {
            DrawingHitboxes = true;
        }

        private void SetDrawingHitboxesFalse()
        {
            DrawingHitboxes = false;
        }

        private void ModDrawHollowRect(HollowRectOriginal orig, ISourceDrawScope scope, float x, float y, float width,
            float height, PackedColor color)
        {
            if (!driver.Settings.ShowHitboxes || !DrawingHitboxes)
            {
                orig(scope, x, y, width, height, color);
                return;
            }

            float fx = (float)Math.Floor(x);
            float fy = (float)Math.Floor(y);
            float cw = (float)Math.Ceiling(width + x - fx);
            float cy = (float)Math.Ceiling(height + y - fy);
            orig(scope, fx, fy, cw, cy, color);
        }

        private void ModDrawCircle(CircleOriginal orig, ISourceDrawScope scope, Vector2 center, float radius,
            PackedColor color,
            int resolution, ISourceVectorArithmetic arithmetic)
        {
            // Adapted from John Kennedy, "A Fast Bresenham Type Algorithm For Drawing Circles"
            // https://web.engr.oregonstate.edu/~sllu/bcircle.pdf
            // Not as fast though because we are forced to use floating point arithmetic anyway
            // since the center and radius aren't necessarily integral.
            // For similar reasons, we can't just assume the circle has 8-fold symmetry.
            // Modified so that instead of minimizing error, we include exactly those pixels which intersect the circle.

            if (!driver.Settings.ShowHitboxes || !DrawingHitboxes)
            {
                orig(scope, center, radius, color, resolution, arithmetic);
                return;
            }

            CircleOctant(center, radius, color, 1, 1, false);
            CircleOctant(center, radius, color, 1, -1, false);
            CircleOctant(center, radius, color, -1, 1, false);
            CircleOctant(center, radius, color, -1, -1, false);
            CircleOctant(center, radius, color, 1, 1, true);
            CircleOctant(center, radius, color, 1, -1, true);
            CircleOctant(center, radius, color, -1, 1, true);
            CircleOctant(center, radius, color, -1, -1, true);
        }

        private void CircleOctant(Vector2 center, float radius, PackedColor color, float flipX, float flipY,
            bool interchangeXy)
        {
            // when flipX = flipY = 1 and interchangeXY = false, we are drawing the [0, pi/4] octant.

            float cx, cy;
            if (interchangeXy)
            {
                cx = center.Y;
                cy = center.X;
            }
            else
            {
                cx = center.X;
                cy = center.Y;
            }

            float x, y;
            if (flipX > 0)
            {
                x = (float)Math.Ceiling(cx + radius - 1);
            }
            else
            {
                x = (float)Math.Floor(cx - radius + 1);
            }

            if (flipY > 0)
            {
                y = (float)Math.Floor(cy);
            }
            else
            {
                y = (float)Math.Ceiling(cy);
            }

            float starty = y;
            float e = (x - cx) * (x - cx) + (y - cy) * (y - cy) - radius * radius;
            float yc = flipY * 2 * (y - cy) + 1;
            float xc = flipX * -2 * (x - cx) + 1;
            while (flipY * (y - cy) <= flipX * (x - cx))
            {
                // Slower than using DrawLine, but more obviously correct:
                //DrawPoint((int)x + (flipX < 0 ? -1 : 0), (int)y + (flipY < 0 ? -1 : 0), interchangeXY, color);
                e += yc;
                y += flipY;
                yc += 2;
                if (e >= 0)
                {
                    // We would have a 1px correction for flipY here (as we do for flipX) except for
                    // the fact that our lines always include the top pixel and exclude the bottom one.
                    // Because of this we would have to make two corrections which cancel each other out,
                    // so we just don't do either of them.
                    DrawLine((int)x + (flipX < 0 ? -1 : 0), (int)starty, (int)y, interchangeXy, color);
                    starty = y;
                    e += xc;
                    x -= flipX;
                    xc += 2;
                }
            }

            DrawLine((int)x + (flipX < 0 ? -1 : 0), (int)starty, (int)y, interchangeXy, color);
        }

        private void DrawLine(int x, int y0, int y1, bool interchangeXy, PackedColor color)
        {
            // x, y0, and y1 must all be integers
            int length = (int)(y1 - y0);
            IntRect rect = default;
            if (interchangeXy)
            {
                rect.X = (int)y0;
                rect.Y = (int)x;
                rect.Width = length;
                rect.Height = 1;
            }
            else
            {
                rect.X = (int)x;
                rect.Y = (int)y0;
                rect.Width = 1;
                rect.Height = length;
            }

            Scope.Draw.SpriteBatch.Draw((SourceTexture2DResource)Scope.Draw.Pixel.Texture.Texture, rect,
                Scope.Draw.Pixel.ClipRect, color.PackedValue);
        }

        /// Draws an exact line, filling all pixels the line actually intersects
        /// Based on the logic of Collide.LineToRect and with the assumption that other colliders are grid-aligned
        internal void DrawExactLine(Vector2 from, Vector2 to, PackedColor color)
        {
            // The line endings can touch the right / bottom side of the collider without colliding,
            // because Collide.GetSector uses >= instead of > for those
            int minX = (int)MathF.Floor(Math.Min(from.X, to.X));
            int minY = (int)MathF.Floor(Math.Min(from.Y, to.Y));
            SourcePrimitiveDrawing.Point(Context, this, new Vector2(MathF.Floor(from.X), MathF.Floor(from.Y)), color);
            SourcePrimitiveDrawing.Point(Context, this, new Vector2(MathF.Floor(to.X), MathF.Floor(to.Y)), color);

            // Straight lines
            if ((int)Math.Floor(from.X) == (int)Math.Floor(to.X))
            {
                int top = Math.Max(minY, (int)MathF.Floor(SourceSingleBitMath.BitDecrement(Math.Min(from.Y, to.Y))));
                int bottom = Math.Max(minY,
                    (int)MathF.Ceiling(SourceSingleBitMath.BitIncrement(Math.Max(from.Y, to.Y))));
                SourcePrimitiveDrawing.Rect(Context, this, MathF.Floor(Math.Min(from.X, to.X)), top, 1, bottom - top,
                    color);
                return;
            }

            if ((int)Math.Floor(from.Y) == (int)Math.Floor(to.Y))
            {
                int left = Math.Max(minX, (int)MathF.Floor(SourceSingleBitMath.BitDecrement(Math.Min(from.X, to.X))));
                int right = Math.Max(minX,
                    (int)MathF.Ceiling(SourceSingleBitMath.BitIncrement(Math.Max(from.X, to.X))));
                SourcePrimitiveDrawing.Rect(Context, this, left, MathF.Floor(Math.Min(from.Y, to.Y)), right - left, 1,
                    color);
                return;
            }

            float dx = to.X - from.X;
            float dy = to.Y - from.Y;

            bool horizontal = Math.Abs(dx) >= Math.Abs(dy);
            float step = Math.Min(Math.Abs(dx), Math.Abs(dy));
            dx /= step;
            dy /= step;

            // Starting / Ending point might not be integer coordinates
            float startDist = horizontal
                ? Math.Sign(dy) * 0.5f + 0.5f - Mod(from.Y, 1.0f)
                : Math.Sign(dx) * 0.5f + 0.5f - Mod(from.X, 1.0f);
            float endDist = horizontal
                ? Math.Sign(dy) * 0.5f + 0.5f - Mod(1.0f - to.Y, 1.0f)
                : Math.Sign(dx) * 0.5f + 0.5f - Mod(1.0f - to.X, 1.0f);

            startDist = Mod(Math.Abs(startDist), 1.0f);
            endDist = Mod(Math.Abs(endDist), 1.0f);

            float startX = from.X + startDist * dx;
            float startY = from.Y + startDist * dy;
            float endX = to.X - endDist * dx;
            float endY = to.Y - endDist * dy;

            DrawSegment(from.X, from.Y, startX, startY);
            DrawSegment(to.X, to.Y, endX, endY);

            int steps = (int)Math.Min(MathF.Round(Math.Abs(endX - startX)), MathF.Round(Math.Abs(endY - startY)));
            for (int i = 0; i < steps; i++)
            {
                DrawSegment(
                    startX + i * dx,
                    startY + i * dy,
                    startX + (i + 1) * dx,
                    startY + (i + 1) * dy);
            }

            void DrawSegment(float ax, float ay, float bx, float by)
            {
                if (horizontal)
                {
                    int left = Math.Max(minX, (int)MathF.Floor(SourceSingleBitMath.BitDecrement(Math.Min(ax, bx))));
                    int right = Math.Max(minX, (int)MathF.Ceiling(SourceSingleBitMath.BitIncrement(Math.Max(ax, bx))));
                    SourcePrimitiveDrawing.Rect(Context, this, left, MathF.Floor(Math.Min(ay, by)), right - left, 1,
                        color);
                }
                else
                {
                    int top = Math.Max(minY, (int)MathF.Floor(SourceSingleBitMath.BitDecrement(Math.Min(ay, by))));
                    int bottom = Math.Max(minY, (int)MathF.Ceiling(SourceSingleBitMath.BitIncrement(Math.Max(ay, by))));
                    SourcePrimitiveDrawing.Rect(Context, this, MathF.Floor(Math.Min(ax, bx)), top, 1, bottom - top,
                        color);
                }
            }
        }

        private static float Mod(float value, float modulo) => (value % modulo + modulo) % modulo;
    }
}
