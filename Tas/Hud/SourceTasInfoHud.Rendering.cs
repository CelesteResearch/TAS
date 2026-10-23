using Celeste.Core.Display;
using System;
using System.Text;
using System.Numerics;
using Celeste.Core.Geometry;
using Celeste.Core.Mathematics;
using Celeste.Core.Mathematics.Extrema;
using Celeste.Core.Rendering;
using Celeste.Scene.Scenes;
using Celeste.Scene.Tracking;
using Celeste.Simulation;
using Celeste.Game.Execution.Levels;
using Celeste.Mods.Tas.Input;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Celeste.Graphics.Rendering.Drawing;

namespace Celeste.Mods.Tas.Hud
{
    public sealed partial class SourceTasInfoHud
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private readonly SourceTasJetBrainsMonoFont font;
        private readonly SourceTasSubPixelIndicator indicator;
        public Vector2 Size { get; private set; }

        internal SourceTasInfoHud(GameApplication process, TasPlaybackDriver driver,
            SourceTasJetBrainsMonoFont font)
        {
            this.process = process;
            this.driver = driver;
            this.font = font;
            indicator = new SourceTasSubPixelIndicator(process, driver, font);
        }

        internal void DrawInfo(SourceSceneState scene, ISourceRenderContext context, bool drawSubpixelIndicator = true)
        {
            if (!driver.Settings.Enabled || !driver.Settings.InfoHud)
            {
                return;
            }

            StringBuilder stringBuilder = new();

            if (driver.Settings.InfoTasInput)
            {
                WriteTasInput(stringBuilder);
            }

            string hudInfo = driver.GameInfo.HudInfo;
            if (!string.IsNullOrEmpty(hudInfo))
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.AppendLine();
                }

                stringBuilder.Append(hudInfo);
            }

            string text = stringBuilder.ToString().Trim();
            if (string.IsNullOrEmpty(text) && !driver.Settings.InfoSubpixelIndicator)
            {
                return;
            }

            int viewWidth = context.Graphics.ViewWidth;
            int viewHeight = context.Graphics.ViewHeight;

            float pixelScale = context.Graphics.ViewWidth / 320f;
            float margin = 2 * pixelScale;
            float padding = 2 * pixelScale;
            float fontSize = 0.15f * pixelScale * driver.Settings.InfoTextSize / 10f;
            float alpha = driver.Settings.InfoOpacity / 10f;
            float infoAlpha = 1f;

            Size = font.Measure(text) * fontSize;
            if (drawSubpixelIndicator)
            {
                Size = indicator.TryExpandSize(Size, padding);
            }

            float maxX = viewWidth - Size.X - margin - padding * 2;
            float maxY = viewHeight - Size.Y - margin - padding * 2;
            if (maxY > 0f)
            {
                driver.Settings.InfoPosition = new Vector2(
                    ScalarMath.Clamp(driver.Settings.InfoPosition.X, margin, maxX,
                        ((ISourceExtremaEnvironment)process).ExtremaArithmetic),
                    ScalarMath.Clamp(driver.Settings.InfoPosition.Y, margin, maxY,
                        ((ISourceExtremaEnvironment)process).ExtremaArithmetic));
            }

            float x = driver.Settings.InfoPosition.X;
            float y = driver.Settings.InfoPosition.Y;

            IntRect bgRect = new((int)x, (int)y, (int)(Size.X + padding * 2), (int)(Size.Y + padding * 2));

            if (driver.Settings.InfoMaskedOpacity < 10 && !driver.SourceHotkeys.InfoHud.Check &&
                (scene.Paused && !process.InputSource.MenuJournal.Check ||
                 scene is OriginalLevelSceneRoot level && CollidePlayer(level, bgRect, context)))
            {
                alpha *= driver.Settings.InfoMaskedOpacity / 10f;
                infoAlpha = alpha;
            }

            ((ISourceDrawScope)context).Draw.SpriteBatch.Begin();

            SourcePrimitiveDrawing.Rect((ISourceDrawScope)context, bgRect,
                PackedColor.Multiply(PackedColor.Black, alpha));

            if (drawSubpixelIndicator)
            {
                indicator.DrawIndicator(context, scene, bgRect.Bottom, padding, infoAlpha);
            }

            Vector2 textPosition = new(x + padding, y + padding);
            Vector2 scale = new(fontSize);

            font.Draw(context, scene, text, textPosition, Vector2.Zero, scale,
                PackedColor.Multiply(PackedColor.White, infoAlpha));

            ((ISourceDrawScope)context).Draw.SpriteBatch.End();
        }

        private bool CollidePlayer(OriginalLevelSceneRoot level, IntRect bgRect, ISourceRenderContext context)
        {
            if (((ISourceScene)level).SourceScene.Entities.GetTrackedEntity<RoomPlayerSession>(
                    typeof(global::Celeste.Simulation.RoomPlayerSession).FullName) is not

                { } player)
            {
                return false;
            }

            Vector2 playerTopLeft =
                level.WorldToScreen(new Vector2(player.Collider.AbsoluteLeft, player.Collider.AbsoluteTop)) /
                context.Graphics.Width * context.Graphics.ViewWidth;
            Vector2 playerBottomRight =
                level.WorldToScreen(new Vector2(player.Collider.AbsoluteRight, player.Collider.AbsoluteBottom)) /
                context.Graphics.Width * context.Graphics.ViewWidth;
            IntRect playerRect = new(
                (int)Math.Min(playerTopLeft.X, playerBottomRight.X),
                (int)Math.Min(playerTopLeft.Y, playerBottomRight.Y),
                (int)Math.Abs(playerTopLeft.X - playerBottomRight.X),
                (int)Math.Abs(playerTopLeft.Y - playerBottomRight.Y)
            );

            return playerRect.Intersects(bgRect);
        }

        // TODO add a setting 'Info TasInputLines'
        private void WriteTasInput(StringBuilder stringBuilder)
        {
            var controller = driver.Controller;
            var inputs = controller.Inputs;

            if (!driver.Running || controller.CurrentFrameInTas < 0 || controller.CurrentFrameInTas >= inputs.Count)
            {
                return;
            }

            var current = controller.Current!;
            if (controller.CurrentFrameInTas >= 1 && current != controller.Previous)
            {
                current = controller.Previous!;
            }

            var previous = current.Previous;
            var next = current.Next;

            int maxLine = Math.Max(current.StudioLine, Math.Max(previous?.StudioLine ?? 0, next?.StudioLine ?? 0)) + 1;
            int linePadLeft = maxLine.ToString().Length;

            int maxFrames = Math.Max(current.Frames, Math.Max(previous?.Frames ?? 0, next?.Frames ?? 0));
            int framesPadLeft = maxFrames.ToString().Length;

            if (previous != null)
            {
                stringBuilder.AppendLine(FormatInputFrame(previous));
            }

            string currentStr = FormatInputFrame(current);
            int currentFrameLength = controller.CurrentFrameInInput.ToString().Length;
            int inputWidth = currentStr.Length + currentFrameLength + 2;
            inputWidth = Math.Max(inputWidth, 20);
            stringBuilder.AppendLine(
                $"{currentStr.PadRight(inputWidth - currentFrameLength)}{controller.CurrentFrameInInput}{current.RepeatString}");

            if (next != null)
            {
                stringBuilder.AppendLine(FormatInputFrame(next));
            }

            return;

            string FormatInputFrame(SourceTasInputFrame inputFrame)
            {
                string line = (inputFrame.StudioLine + 1).ToString().PadLeft(linePadLeft);
                string padding = string.Empty.PadLeft(framesPadLeft - inputFrame.Frames.ToString().Length);
                string input = inputFrame.ToString().TrimStart();

                return $"{line}: {padding}{input}";
            }
        }
    }
}
