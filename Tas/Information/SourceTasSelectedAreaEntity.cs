using Celeste.Core.Display;
#nullable enable
using System;
using System.Globalization;
using System.Numerics;
using Celeste.Core.Components;
using Celeste.Core.Rendering;
using Celeste.Scene.Entities;
using Celeste.Scene.Scheduling.ExecutionTags;
using Celeste.Everest.Runtime.Input;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;

namespace Celeste.Mods.Tas.Information
{
    internal sealed class SourceTasSelectedAreaEntity : SceneEntityState
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private static SourceTasSelectedAreaEntity? Instance(TasPlaybackDriver driver) => driver.InfoMouse.SelectedArea;

        internal static string GetRect(TasPlaybackDriver driver)
        {
            if (IsDragging(driver))
            {
                return Instance(driver)!.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        internal static string GetWidth(TasPlaybackDriver driver)
        {
            if (IsDragging(driver))
            {
                return Instance(driver)!.width.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        internal static string GetHeight(TasPlaybackDriver driver)
        {
            if (IsDragging(driver))
            {
                return Instance(driver)!.height.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        internal static string GetDiagonal(TasPlaybackDriver driver)
        {
            if (IsDragging(driver))
            {
                return Math.Sqrt(Instance(driver)!.width * Instance(driver)!.width +
                                 Instance(driver)!.height * Instance(driver)!.height)
                    .ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                return string.Empty;
            }
        }

        private static bool IsDragging(TasPlaybackDriver driver) => Instance(driver)?.start != null &&
                                                                    (Instance(driver)!.width > 1 ||
                                                                     Instance(driver)!.height > 1);

        private Vector2? start;
        private int left;
        private int right;
        private int top;
        private int bottom;
        private int width;
        private int height;

        internal SourceTasSelectedAreaEntity(GameApplication process, TasPlaybackDriver driver)
            : base(((ISourceEntitySourceDataEnvironment)process).EntitySourceDataRegistration)
        {
            this.process = process;
            this.driver = driver;
            Depth = -1000000;
            Tags = ((ISourceGlobalFrozenExecutionTagsEnvironment)process).GlobalFrozenExecutionTags.Global.Value;
        }

        public override void Render(ISourceRenderContext context)
        {
            if (!driver.Settings.ShowHitboxes)
            {
                DrawSelectedArea(context);
            }
        }

        public override void DebugRender(ISourceRenderContext context, object actualCamera)
        {
            DrawSelectedArea(context);
        }

        private void DrawSelectedArea(ISourceRenderContext context)
        {
            if (Scene is not Level level || !driver.SourceHotkeys.InfoHud.Check)
            {
                start = null;
                return;
            }

            if (driver.MouseInput.Right.Pressed)
            {
                start = SourceTasLevelCoordinateConversion.MouseToWorld(level, driver, driver.MouseInput.Position);
            }

            if (start != null)
            {
                Vector2 end =
                    SourceTasLevelCoordinateConversion.MouseToWorld(level, driver, driver.MouseInput.Position);
                left = (int)Math.Min(start.Value.X, end.X);
                right = (int)Math.Max(start.Value.X, end.X);
                top = (int)Math.Min(start.Value.Y, end.Y);
                bottom = (int)Math.Max(start.Value.Y, end.Y);
                width = right - left + 1;
                height = bottom - top + 1;

                if (IsDragging(driver) && driver.MouseInput.Right.Check)
                {
                    SourcePrimitiveDrawing.HollowRect(context, this, left, top, width, height, PackedColor.Yellow);
                }

                if (driver.MouseInput.Right.Released)
                {
                    if (IsDragging(driver))
                    {
                        process.InstalledTextInput.SetClipboardText(ToString());
                    }

                    start = null;
                }
            }
        }

        public override string ToString()
        {
            return start == null ? string.Empty : $"{left}, {top}, {right}, {bottom}";
        }
    }
}
