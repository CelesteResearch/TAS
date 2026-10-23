using Celeste.Core.Display;
#nullable enable
using System;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Rendering;
using Celeste.Core.Geometry;
using Celeste.Core.Rendering.States;
using Celeste.Scene.Scenes;
using Celeste.Everest.Presentation.Hud;
using Celeste.Game.Execution.Engine;
using Celeste.Game.Execution.Graphics;
using Celeste.Game.Execution.Startup;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Presentation.PostProcessing.ColorGrading;
using Celeste.Simulation.Save.Runtime;
using Celeste.Graphics.Content;
using Celeste.Graphics.Content.Atlas;
using Celeste.Graphics.Content.VirtualAssets;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering.Drawing;
using Celeste.Graphics.Rendering.Devices;
using Celeste.Graphics.Rendering.Textures;
using global::Celeste.Game.Execution;

namespace Celeste.Mods.Tas.Hitboxes
{
    internal sealed class SourceTasOffscreenHitbox
    {
        private readonly TasPlaybackDriver driver;
        private VirtualRenderTarget? offscreenBuffer;

        internal SourceTasOffscreenHitbox(TasPlaybackDriver driver)
        {
            this.driver = driver;
        }

        public bool ShouldDraw => driver.Settings.CenterCamera && driver.SourceCenterCamera.ZoomedOut;
        private ISourceRenderContext Context => ((ISourceRenderEnvironment)driver.Process).CurrentContext;
        private SourceGraphicsDevice Device => ((ISourceGraphicsDeviceEnvironment)driver.Process).GraphicsDevice;

        private OriginalGameGraphicsResources Graphics =>
            (OriginalGameGraphicsResources)((ISourceGameGraphicsEnvironment)driver.Process).GameGraphics;

        private OriginalColorGradeProcess ColorGrade =>
            ((IOriginalColorGradeEnvironment)driver.Process).ColorGradeProcess;

        internal void Load()
        {
            var before =
                typeof(SubHudRendererState).GetMethod(nameof(SubHudRendererState.BeforeRender),
                    new[] { typeof(object) });
            if (before != null)
                driver.SourceHooks.IlHook(before, (cursor, _) =>
                {
                    cursor.EmitLdarg1();
                    cursor.EmitDelegate((Action<object>)DrawHitboxesToBuffer);
                });
            var render =
                typeof(SubHudRendererState).GetMethod(nameof(SubHudRendererState.Render), new[] { typeof(object) });
            if (render != null)
                driver.SourceHooks.IlHook(render, (cursor, _) =>
                {
                    cursor.EmitLdarg1();
                    cursor.EmitDelegate((Action<object>)DrawBufferToScreen);
                });
        }

        private float BufferScale => Math.Min(1.0f,
            (offscreenBuffer?.Width ?? 0) / (GameRuntime.GameWidth / driver.SourceCenterCamera.ZoomLevel));

        private void DrawHitboxesToBuffer(object actualScene)
        {
            if (actualScene is not OriginalLevelSceneRoot scene || !driver.SourceHitboxToggle.DrawHitboxes ||
                !ShouldDraw) return;
            offscreenBuffer ??= ((ISourceVirtualContentEnvironment)driver.Process).VirtualContent.CreateRenderTarget(
                "CelesteTAS/offscreen-hitboxes", GameRuntime.TargetWidth + 2,
                GameRuntime.TargetHeight + 2, depth: true, preserve: true);
            Device.SetRenderTarget(offscreenBuffer.Target);
            Device.Clear(PackedColor.Transparent);
            Context.BeginBatch(new SourceRenderBatchState(SourceSpriteSortMode.Deferred, SourceBlendState.AlphaBlend,
                SourceSamplerState.LinearClamp, SourceDepthStencilState.Default, SourceRasterizerState.CullNone, null,
                driver.SourceCenterCamera.ScreenCamera.Matrix * Matrix4x4.CreateScale(BufferScale)));
            driver.SourceHitboxFixer.DrawingHitboxes = true;
            scene.SourceScene.Entities.DebugRender(Context, driver.SourceCenterCamera.ScreenCamera, scene);
            driver.SourceHitboxFixer.DrawingHitboxes = false;
            Context.EndBatch();
        }

        private void DrawBufferToScreen(object actualScene)
        {
            if (actualScene is not OriginalLevelSceneRoot level || !driver.SourceHitboxToggle.DrawHitboxes ||
                !ShouldDraw ||
                offscreenBuffer == null) return;
            var effects = SourceSpriteEffects.None;
            if (((ISourceCurrentSaveEnvironment)driver.Process).CurrentSave?.MirrorMode ?? false)
                effects |= SourceSpriteEffects.FlipHorizontally;
            if (driver.SourceExtendedVariants.UpsideDown) effects |= SourceSpriteEffects.FlipVertically;
            string? colorGradeOverwrite = driver.SourceExtendedVariants.ColorGrading;
            var lastColorTex = Graphics.ColorGradesAtlas.GetOrDefault(colorGradeOverwrite ?? level.ColorGrade.LastGrade,
                Graphics.ColorGradesAtlas["none"]);
            var nextColorTex = Graphics.ColorGradesAtlas.GetOrDefault(colorGradeOverwrite ?? level.Session.ColorGrade,
                Graphics.ColorGradesAtlas["none"]);
            if (level.ColorGrade.Ease > 0f && lastColorTex != nextColorTex)
                ColorGrade.Set(lastColorTex, nextColorTex, level.ColorGrade.Ease);
            else ColorGrade.Set(nextColorTex);
            Context.BeginBatch(new SourceRenderBatchState(SourceSpriteSortMode.Deferred, SourceBlendState.AlphaBlend,
                SourceSamplerState.PointClamp, SourceDepthStencilState.Default, SourceRasterizerState.CullNone,
                ColorGrade.Effect, ((IOriginalEngineGraphicsEnvironment)driver.Process).EngineGraphics.ScreenMatrix));
            ((ISourceDrawScope)Context).Draw.SpriteBatch.Draw(offscreenBuffer.Target, Vector2.Zero,
                new IntRect(driver.SourceCenterCamera.ScreenCamera.Viewport.X,
                    driver.SourceCenterCamera.ScreenCamera.Viewport.Y,
                    driver.SourceCenterCamera.ScreenCamera.Viewport.Width,
                    driver.SourceCenterCamera.ScreenCamera.Viewport.Height), PackedColor.White.PackedValue, 0.0f,
                Vector2.Zero,
                driver.SourceCenterCamera.ZoomLevel *
                (GameRuntime.TargetWidth / GameRuntime.GameWidth) / BufferScale, effects, 0.0f);
            Context.EndBatch();
        }
    }
}
