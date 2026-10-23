using Celeste.Core.Display;
using System;
using System.Numerics;
using Celeste.Core.Mathematics;
using Celeste.Core.Rendering;
using Celeste.Game.Execution.Engine;
using Celeste.Game.Settings;
using Celeste.Scene.Tracking;
using Celeste.Mods.Tas.Input;
using Celeste.Mods.Tas.Interop;
using Celeste.Mods.Tas.Module;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Celeste.Graphics.Rendering.Drawing;
using global::Celeste.Game.Execution;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasCenterCamera sourceCenterCamera;
        internal bool SourceExCameraDynamicsInstalled => sourceCenterCamera.ExCameraDynamicsInstalled;
        internal bool SourceExCameraInstalled => sourceCenterCamera.ExCameraInstalled;
        private SourceTasMouseInput sourceMouseInput;
        internal SourceTasMouseInput MouseInput => sourceMouseInput;

        private void InitializeSourceCenterCamera()
        {
            sourceMouseInput = new SourceTasMouseInput(process, this);
            sourceCenterCamera = new SourceTasCenterCamera(process, this, sourceMouseInput,
                new SourceTasExCameraDynamicsInterop(process), SourceExtendedVariants);
        }

        internal void OnSourceCenterCameraToggled() => sourceCenterCamera.Toggled();

        [SourceTasUpdateMeta]
        private static void UpdateSourceCenterCameraMeta() =>
            CelesteTasModule.Instance.Driver.sourceCenterCamera.UpdateMeta();

        [SourceTasUpdateMeta]
        private static void UpdateSourceMouseMeta() => CelesteTasModule.Instance.Driver.sourceMouseInput.UpdateMeta();

        [SourceTasLoad]
        private static void LoadSourceCenterCamera() =>
            TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process).InstallSourceCenterCameraHooks();

        [SourceTasUnload]
        private static void UnloadSourceCenterCamera() =>
            TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process).UninstallSourceCenterCameraHooks();

        internal void SourceCenterRenderCore(Action original) => sourceCenterCamera.RenderCore(original);
        internal void SourceCenterCommandsRender(Action original) => sourceCenterCamera.CommandsRender(original);
        internal void SourceCenterLevelRender(Action original) => sourceCenterCamera.LevelRender(original);

        internal void SourceCenterLevelLoadLevel(Action original, bool isFromLoader) =>
            sourceCenterCamera.LevelLoadLevel(original, isFromLoader);

        internal void DrawSourceCameraCursor(Vector2 position)
        {
            var context = ((ISourceRenderEnvironment)process).CurrentContext;
            var scope = (ISourceDrawScope)context;
            scope.Draw.SpriteBatch.Begin();
            int scale = ((ISettingsContext)process).Settings.Fullscreen
                ? 6
                : Math.Min(6, EngineRuntime.ViewWidth / 320);
            PackedColor color = PackedColor.Yellow;
            var arithmetic = ((ISourceVectorEnvironment)process).VectorArithmetic;
            for (int i = -scale / 2; i <= scale / 2; i++)
            {
                SourcePrimitiveDrawing.Line(scope, position.X - 4f * scale, position.Y + i, position.X - 2f * scale,
                    position.Y + i, color, arithmetic);
                SourcePrimitiveDrawing.Line(scope, position.X + 2f * scale - 1f, position.Y + i,
                    position.X + 4f * scale - 1f, position.Y + i, color, arithmetic);
                SourcePrimitiveDrawing.Line(scope, position.X + i, position.Y - 4f * scale + 1f, position.X + i,
                    position.Y - 2f * scale + 1f, color, arithmetic);
                SourcePrimitiveDrawing.Line(scope, position.X + i, position.Y + 2f * scale, position.X + i,
                    position.Y + 4f * scale, color, arithmetic);
            }

            SourcePrimitiveDrawing.Line(scope, position.X - 3f, position.Y, position.X + 2f, position.Y, color,
                arithmetic);
            SourcePrimitiveDrawing.Line(scope, position.X, position.Y - 2f, position.X, position.Y + 3f, color,
                arithmetic);
            scope.Draw.SpriteBatch.End();
        }
    }
}
