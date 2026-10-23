using Celeste.Core.Display;
using System;
using System.Linq;
using System.Numerics;
using StudioCommunication.Util;
using Celeste.Core.Rendering;
using Celeste.Scene.Tracking;
using Celeste.Simulation;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Minigames.CelesteClassic.Emulation;
using Celeste.Game.Minigames.CelesteClassic.Gameplay;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;

namespace Celeste.Mods.Tas.Hud
{
    public sealed class SourceTasSubPixelIndicator
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private readonly SourceTasJetBrainsMonoFont font;

        internal SourceTasSubPixelIndicator(GameApplication process, TasPlaybackDriver driver,
            SourceTasJetBrainsMonoFont font)
        {
            this.process = process;
            this.driver = driver;
            this.font = font;
        }

        private float PixelScale => ((ISourceRenderEnvironment)process).CurrentContext.Graphics.ViewWidth / 320f;

        public void DrawIndicator(ISourceRenderContext context, object actualScene, float y, float padding, float alpha)
        {
            if (!driver.Settings.InfoSubpixelIndicator)
            {
                return;
            }

            double subPixelLeft = 0.5;
            double subPixelRight = 0.5;
            double subPixelTop = 0.5;
            double subPixelBottom = 0.5;
            int decimals = driver.Settings.SubpixelIndicatorDecimals;

            var scene = process.Frame.Scenes.CurrentScene;
            Vector2 remainder = Vector2.Zero;
            if (scene is OriginalLevelSceneRoot level &&
                ((ISourceScene)level).SourceScene.Entities.GetTrackedEntity<RoomPlayerSession>(
                    typeof(global::Celeste.Simulation.RoomPlayerSession).FullName) is

                { } player)
            {
                remainder = player.Body.Remainder;
            }
            else if (scene is ClassicConsoleRuntime emulator &&
                     emulator.Game?.Objects.FirstOrDefault(o => o is Pico8Classic.player) is Pico8Classic.player
                         classicPlayer)
            {
                remainder = classicPlayer.rem;
            }

            subPixelLeft = Math.Round(remainder.X + 0.5, decimals, MidpointRounding.AwayFromZero);
            subPixelTop = Math.Round(remainder.Y + 0.5, decimals, MidpointRounding.AwayFromZero);
            subPixelRight = 1.0 - subPixelLeft;
            subPixelBottom = 1.0 - subPixelTop;

            Vector2 textSize = GetSubPixelTextSize();
            float textWidth = textSize.X;
            float textHeight = textSize.Y;
            float rectSide = GetSubPixelRectSize();
            float x = driver.Settings.InfoPosition.X + textWidth + padding * 2;
            y = y - rectSide - padding * 1.5f - textHeight;
            int thickness = Math.Max(1, (int)Math.Round(PixelScale * driver.Settings.InfoSubpixelIndicatorSize / 20f));
            DrawHollowRect(context, actualScene, x, y, rectSide, rectSide,
                PackedColor.Multiply(PackedColor.Green, alpha),
                thickness);
            SourcePrimitiveDrawing.Rect(context, actualScene, (float)(x + (rectSide - thickness) * subPixelLeft),
                (float)(y + (rectSide - thickness) * subPixelTop), thickness, thickness,
                PackedColor.Multiply(PackedColor.Red, alpha));

            int hDecimals = Math.Abs(remainder.X) switch
            {
                0.5f => 0,
                _ => decimals
            };
            int vDecimals = Math.Abs(remainder.Y) switch
            {
                0.5f => 0,
                _ => decimals
            };

            string left = subPixelLeft.ToFormattedString(hDecimals)
                .PadLeft(driver.Settings.SubpixelIndicatorDecimals + 2, ' ');
            string right = subPixelRight.ToFormattedString(hDecimals);
            string top = subPixelTop.ToFormattedString(vDecimals)
                .PadLeft(driver.Settings.SubpixelIndicatorDecimals / 2 + 2, ' ');
            string bottom = subPixelBottom.ToFormattedString(vDecimals)
                .PadLeft(driver.Settings.SubpixelIndicatorDecimals / 2 + 2, ' ');

            font.Draw(context, actualScene, left,
                new Vector2(x - textWidth - padding / 2f, y + (rectSide - textHeight) / 2f),
                Vector2.Zero, new Vector2(GetSubPixelFontSize()), PackedColor.Multiply(PackedColor.White, alpha));
            font.Draw(context, actualScene, right,
                new Vector2(x + rectSide + padding / 2f, y + (rectSide - textHeight) / 2f),
                Vector2.Zero, new Vector2(GetSubPixelFontSize()), PackedColor.Multiply(PackedColor.White, alpha));

            float tweakX = vDecimals == 0 && decimals % 2 == 0 ? padding / 2 : 0;
            font.Draw(context, actualScene, top,
                new Vector2(x + (rectSide - textWidth) / 2f - tweakX, y - textHeight - padding / 2f),
                Vector2.Zero, new Vector2(GetSubPixelFontSize()), PackedColor.Multiply(PackedColor.White, alpha));
            font.Draw(context, actualScene, bottom,
                new Vector2(x + (rectSide - textWidth) / 2f - tweakX, y + rectSide + padding / 2f),
                Vector2.Zero, new Vector2(GetSubPixelFontSize()), PackedColor.Multiply(PackedColor.White, alpha));
        }

        public Vector2 TryExpandSize(Vector2 size, float padding)
        {
            if (driver.Settings.InfoSubpixelIndicator)
            {
                if (size.Y == 0)
                {
                    size.Y = -padding;
                }

                size.Y += GetSubPixelRectSize() + GetSubPixelTextSize().Y * 2 + padding * 2;
                size.X = Math.Max(size.X, GetSubPixelRectSize() + GetSubPixelTextSize().X * 2 + padding * 2);
            }

            return size;
        }

        private float GetSubPixelRectSize()
        {
            if (driver.Settings.InfoSubpixelIndicator)
            {
                return PixelScale * driver.Settings.InfoSubpixelIndicatorSize;
            }
            else
            {
                return 0f;
            }
        }

        private Vector2 GetSubPixelTextSize()
        {
            if (driver.Settings.InfoSubpixelIndicator)
            {
                return font.Measure("0.".PadRight(driver.Settings.SubpixelIndicatorDecimals + 2, '0')) *
                       GetSubPixelFontSize();
            }
            else
            {
                return default;
            }
        }

        private float GetSubPixelFontSize()
        {
            return 0.15f * PixelScale * driver.Settings.InfoTextSize / 10f;
        }

        private static void DrawHollowRect(ISourceRenderContext context, object actualScene, float left, float top,
            float width, float height, PackedColor color, float thickness)
        {
            SourcePrimitiveDrawing.Rect(context, actualScene, left, top, width, thickness, color);
            SourcePrimitiveDrawing.Rect(context, actualScene, left, top, thickness, height, color);
            SourcePrimitiveDrawing.Rect(context, actualScene, left + width - thickness, top, thickness, height, color);
            SourcePrimitiveDrawing.Rect(context, actualScene, left, top + height - thickness, width, thickness, color);
        }
    }
}
