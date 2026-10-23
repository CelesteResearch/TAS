using Celeste.Core.Display;
using Celeste.Graphics.Rendering.Drawing;
using System;
using Celeste.Mods.Tas.Module;
using System.Numerics;
using Celeste.Core.Geometry;
using Celeste.Core.Rendering;
using Celeste.Core.Mathematics;
using Celeste.Scene.Scenes;
using Celeste.Scene.Entities;
using Celeste.Simulation;
using Celeste.Simulation.Gameplay.Platforms.Foundation;
using Celeste.Game.Execution.Levels;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;

namespace Celeste.Mods.Tas.Hitboxes.ModInterop
{
    internal sealed class SourceTasMelvinHitbox
    {
        [SourceTasInitialize]
        private static void SourceInitialize() => CelesteTasModule.Instance.Driver.SourceMelvinHitbox.Initialize();

        private readonly TasPlaybackDriver driver;
        private ISourceRenderContext Context => ((ISourceRenderEnvironment)driver.Process).CurrentContext;
        private ISourceDrawScope DrawScope => (ISourceDrawScope)Context;
        private ISourceVectorArithmetic Arithmetic => ((ISourceVectorEnvironment)driver.Process).VectorArithmetic;
        internal SourceTasMelvinHitbox(TasPlaybackDriver driver) => this.driver = driver;

        private bool ReadTriggered(object melvin)
            => (bool)driver.SourceModReflection.GetFieldInfo(melvin.GetType(), "triggered").Read(melvin);

        internal void Initialize()
        {
            Type t_Melvin =
                driver.SourceModReflection.TryGetType("CommunalHelper", "Celeste.Mod.CommunalHelper.Entities.Melvin");
            if ((object)t_Melvin == null)
            {
                return;
            }

            driver.SourceGameplayEvents.PostDebugRender += delegate(SourceSceneState scene)
            {
                RoomPlayerSession player =
                    scene.SourceScene.Entities.GetTrackedEntity<RoomPlayerSession>(
                        typeof(global::Celeste.Simulation.RoomPlayerSession).FullName);

                if (player != null)
                {
                    bool flag = false;
                    foreach (object item in scene.SourceScene.Entities.GetEntitiesTrackIfNeeded(t_Melvin))
                    {
                        flag = true;
                        RenderSightLines(player, (SourceSolidEntityState)item);
                    }

                    if (flag)
                    {
                        SourcePrimitiveDrawing.Point(DrawScope,
                            new Vector2((float)Math.Floor(player.Center.X), (float)Math.Floor(player.Center.Y)),
                            PackedColor.Red);
                    }
                }
            };
        }

        private void RenderSightLines(RoomPlayerSession player, SourceSolidEntityState melvin)
        {
            IntRect bounds = ((OriginalLevelSceneRoot)melvin.Scene).Bounds;
            int num = (int)melvin.Left;
            int num2 = (int)melvin.Right;
            int num3 = (int)melvin.Top;
            int num4 = (int)melvin.Bottom;
            PackedColor orchid = new PackedColor(218, 112, 214, 255);
            PackedColor val = (ReadTriggered(melvin) ? orchid * driver.SourceHitboxColor.UnCollidableAlpha : orchid);
            Span<int> span = stackalloc int[num4 - num3];
            Span<int> span2 = stackalloc int[num2 - num];
            int num5 = ((player.CenterX == (float)(int)player.CenterX) ? 1 : 0);
            int num6 = ((player.CenterY == (float)(int)player.CenterY) ? 1 : 0);
            float num7 = player.Width / 2f;
            float num8 = player.Height / 2f;
            int num9 = (int)(0.0 - Math.Floor(num7));
            int num10 = (int)Math.Ceiling(num7);
            int num11 = (int)(0.0 - Math.Floor(num8));
            int num12 = (int)Math.Ceiling(num8);
            int left = bounds.Left;
            for (int i = 0; i < span.Length; i++)
            {
                span[i] = CollideBisectLeft((SourceSceneState)melvin.Scene, left, num, num3 + i);
            }

            int num13 = num;
            int num14 = num3 + num6;
            int num15 = num14;
            bool flag = false;
            for (int j = num14; j <= num4; j++)
            {
                int num16 = num;
                if (j != num4)
                {
                    int num17 = j - num3;
                    int num18 = Math.Max(0, num17 + num11);
                    int num19 = Math.Min(span.Length, num17 + num12);
                    for (int k = num18; k < num19; k++)
                    {
                        num16 = Math.Min(num16, span[k] + num9 + 1);
                    }
                }

                if (num16 != num13)
                {
                    int num20 = ((num13 < num16) ? (j - 1) : j);
                    SourcePrimitiveDrawing.Line(DrawScope, (float)Math.Min(num13, num16), (float)num20,
                        (float)Math.Max(num13, num16), (float)num20, val, Arithmetic);
                    bool flag2 = num13 < num16;
                    if (j != num14)
                    {
                        SourcePrimitiveDrawing.Line(DrawScope, (float)num13, (float)(num20 + ((!flag2) ? 1 : 0)),
                            (float)num13, (float)(num15 + ((!flag) ? 1 : 0)), val, Arithmetic);
                    }

                    num13 = num16;
                    num14 = j;
                    num15 = num20;
                    flag = flag2;
                }
            }

            SourcePrimitiveDrawing.Line(DrawScope, (float)(num - 1), (float)num4, (float)(num - 1),
                (float)(num3 + num6),
                val, Arithmetic);
            int right = bounds.Right;
            for (int l = 0; l < span.Length; l++)
            {
                span[l] = CollideBisectRight((SourceSceneState)melvin.Scene, num2, right, num3 + l);
            }

            num13 = num2 + num5;
            num14 = num3 + num6;
            num15 = num14;
            flag = false;
            for (int m = num14; m <= num4; m++)
            {
                int num21 = num2 + num5;
                if (m != num4)
                {
                    int num22 = m - num3;
                    int num23 = Math.Max(0, num22 + num11);
                    int num24 = Math.Min(span.Length, num22 + num12);
                    for (int n = num23; n < num24; n++)
                    {
                        num21 = Math.Max(num21, span[n] + num10 - 1);
                    }
                }

                if (num21 != num13)
                {
                    int num25 = ((num13 > num21) ? (m - 1) : m);
                    SourcePrimitiveDrawing.Line(DrawScope, (float)Math.Min(num13, num21), (float)num25,
                        (float)Math.Max(num13, num21), (float)num25, val, Arithmetic);
                    bool flag3 = num13 < num21;
                    if (m != num14)
                    {
                        SourcePrimitiveDrawing.Line(DrawScope, (float)num13, (float)(num25 + ((!flag3) ? 1 : 0)),
                            (float)num13, (float)(num15 + ((!flag) ? 1 : 0)), val, Arithmetic);
                    }

                    num13 = num21;
                    num14 = m;
                    num15 = num25;
                    flag = flag3;
                }
            }

            SourcePrimitiveDrawing.Line(DrawScope, (float)(num2 + num5), (float)num4, (float)(num2 + num5),
                (float)(num3 + num6), val, Arithmetic);
            int top = bounds.Top;
            for (int num26 = 0; num26 < span2.Length; num26++)
            {
                span2[num26] = CollideBisectTop((SourceSceneState)melvin.Scene, num + num26, top, num3);
            }

            num13 = num + num5;
            num14 = num3;
            int num27 = num13;
            bool flag4 = false;
            for (int num28 = num13; num28 <= num2; num28++)
            {
                int num29 = num3;
                if (num28 != num2)
                {
                    int num30 = num28 - num;
                    int num31 = Math.Max(0, num30 + num9);
                    int num32 = Math.Min(span2.Length, num30 + num10);
                    for (int num33 = num31; num33 < num32; num33++)
                    {
                        num29 = Math.Min(num29, span2[num33] + num11 + 1);
                    }
                }

                if (num29 != num14)
                {
                    int num34 = ((num14 < num29) ? (num28 - 1) : num28);
                    SourcePrimitiveDrawing.Line(DrawScope, (float)num34, (float)Math.Max(num14, num29), (float)num34,
                        (float)Math.Min(num14, num29), val, Arithmetic);
                    bool flag5 = num14 > num29;
                    if (num28 != num13)
                    {
                        SourcePrimitiveDrawing.Line(DrawScope, (float)(num34 + ((!flag5) ? 1 : 0)), (float)num14,
                            (float)(num27 + ((!flag4) ? 1 : 0)), (float)num14, val, Arithmetic);
                    }

                    num13 = num28;
                    num14 = num29;
                    num27 = num34;
                    flag4 = flag5;
                }
            }

            SourcePrimitiveDrawing.Line(DrawScope, (float)(num + num5), (float)(num3 - 1), (float)num2,
                (float)(num3 - 1),
                val, Arithmetic);
            int bottom = bounds.Bottom;
            for (int num35 = 0; num35 < span2.Length; num35++)
            {
                span2[num35] = CollideBisectBottom((SourceSceneState)melvin.Scene, num + num35, num4, bottom);
            }

            num13 = num + num5;
            num14 = num4 + num6;
            num27 = num13;
            flag4 = false;
            for (int num36 = num13; num36 <= num2; num36++)
            {
                int num37 = num4 + num6;
                if (num36 != num2)
                {
                    int num38 = num36 - num;
                    int num39 = Math.Max(0, num38 + num9);
                    int num40 = Math.Min(span2.Length, num38 + num10);
                    for (int num41 = num39; num41 < num40; num41++)
                    {
                        num37 = Math.Max(num37, span2[num41] + num12 - 1);
                    }
                }

                if (num37 != num14)
                {
                    int num42 = ((num14 > num37) ? (num36 - 1) : num36);
                    SourcePrimitiveDrawing.Line(DrawScope, (float)num42, (float)Math.Max(num14, num37), (float)num42,
                        (float)Math.Min(num14, num37), val, Arithmetic);
                    bool flag6 = num14 > num37;
                    if (num36 != num13)
                    {
                        SourcePrimitiveDrawing.Line(DrawScope, (float)(num42 + ((!flag6) ? 1 : 0)), (float)num14,
                            (float)(num27 + ((!flag4) ? 1 : 0)), (float)num14, val, Arithmetic);
                    }

                    num13 = num36;
                    num14 = num37;
                    num27 = num42;
                    flag4 = flag6;
                }
            }

            SourcePrimitiveDrawing.Line(DrawScope, (float)(num + num5), (float)(num4 + num6), (float)num2,
                (float)(num4 + num6), val, Arithmetic);
        }

        private static int CollideBisectLeft(SourceSceneState scene, int x1, int x2, int y)
        {
            IntRect val = default;
            IntRect val2 = default;
            while (x1 != x2)
            {
                int num = x1 / 2 + x2 / 2;
                if (x1 == num || x2 == num)
                {
                    break;
                }

                val = new IntRect(num, y, x2 - num, 1);
                if (SourceSceneCollision.CheckRect(scene.SourceScene.Entities, "Celeste.Solid", val))
                {
                    x1 = num;
                    continue;
                }

                val2 = new IntRect(x1, y, num - x1, 1);
                if (SourceSceneCollision.CheckRect(scene.SourceScene.Entities, "Celeste.Solid", val2))
                {
                    x2 = num;
                    continue;
                }

                return x1;
            }

            return x1;
        }

        private static int CollideBisectRight(SourceSceneState scene, int x1, int x2, int y)
        {
            IntRect val = default;
            IntRect val2 = default;
            while (x1 != x2)
            {
                int num = x1 / 2 + x2 / 2;
                if (x1 == num || x2 == num)
                {
                    break;
                }

                val = new IntRect(x1, y, num - x1, 1);
                if (SourceSceneCollision.CheckRect(scene.SourceScene.Entities, "Celeste.Solid", val))
                {
                    x2 = num;
                    continue;
                }

                val2 = new IntRect(num, y, x2 - num, 1);
                if (SourceSceneCollision.CheckRect(scene.SourceScene.Entities, "Celeste.Solid", val2))
                {
                    x1 = num;
                    continue;
                }

                return x2;
            }

            return x2;
        }

        private static int CollideBisectTop(SourceSceneState scene, int x, int y1, int y2)
        {
            IntRect val = default;
            IntRect val2 = default;
            while (y1 != y2)
            {
                int num = y1 / 2 + y2 / 2;
                if (y1 == num || y2 == num)
                {
                    break;
                }

                val = new IntRect(x, num, 1, y2 - num);
                if (SourceSceneCollision.CheckRect(scene.SourceScene.Entities, "Celeste.Solid", val))
                {
                    y1 = num;
                    continue;
                }

                val2 = new IntRect(x, y1, 1, num - y1);
                if (SourceSceneCollision.CheckRect(scene.SourceScene.Entities, "Celeste.Solid", val2))
                {
                    y2 = num;
                    continue;
                }

                return y1;
            }

            return y1;
        }

        private static int CollideBisectBottom(SourceSceneState scene, int x, int y1, int y2)
        {
            IntRect val = default;
            IntRect val2 = default;
            while (y1 != y2)
            {
                int num = y1 / 2 + y2 / 2;
                if (y1 == num || y2 == num)
                {
                    break;
                }

                val = new IntRect(x, y1, 1, num - y1);
                if (SourceSceneCollision.CheckRect(scene.SourceScene.Entities, "Celeste.Solid", val))
                {
                    y2 = num;
                    continue;
                }

                val2 = new IntRect(x, num, 1, y2 - num);
                if (SourceSceneCollision.CheckRect(scene.SourceScene.Entities, "Celeste.Solid", val2))
                {
                    y1 = num;
                    continue;
                }

                return y2;
            }

            return y2;
        }
    }
}
