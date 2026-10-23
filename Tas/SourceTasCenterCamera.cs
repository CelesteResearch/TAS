using System;
using System.Numerics;
using Celeste.Core.Mathematics;
using Celeste.Core.Mathematics.Framework;
using Celeste.Core.Mathematics.LinearAlgebra;
using Celeste.Game.Execution.Engine;
using Celeste.Game.Execution.Startup;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Content.Maps;
using Celeste.Scene.Scenes;
using Celeste.Scene.Tracking;
using Celeste.Simulation;
using Celeste.Simulation.Presentation.Player.Death;
using Celeste.Graphics.Geometry.TwoD.Camera;
using Celeste.Mods.Tas.Input;
using Celeste.Mods.Tas.Interop;
using global::Celeste.Game.Execution;

namespace Celeste.Mods.Tas
{
    internal sealed class SourceTasCenterCamera
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private readonly SourceTasMouseInput mouse;
        private readonly SourceTasExCameraDynamicsInterop exCamera;
        internal bool ExCameraDynamicsInstalled => exCamera.Installed;
        internal bool ExCameraInstalled => exCamera.Installed;
        private readonly SourceTasExtendedVariantsInterop variants;

        internal SourceTasCenterCamera(GameApplication process, TasPlaybackDriver driver,
            SourceTasMouseInput mouse, SourceTasExCameraDynamicsInterop exCamera,
            SourceTasExtendedVariantsInterop variants)
        {
            this.process = process;
            this.driver = driver;
            this.mouse = mouse;
            this.exCamera = exCamera;
            this.variants = variants;
            ScreenCamera = new CameraState(EngineRuntime.Width, EngineRuntime.Height,
                ((ISourceVectorEnvironment)process).VectorArithmetic);
            savedCamera = new CameraState(EngineRuntime.Width, EngineRuntime.Height,
                ((ISourceVectorEnvironment)process).VectorArithmetic);
        }

        private static Vector2 Clamp(Vector2 value, float minX, float minY, float maxX, float maxY)
            => new Vector2(SourceFrameworkMath.Clamp(value.X, minX, maxX),
                SourceFrameworkMath.Clamp(value.Y, minY, maxY));

        private PlayerDeathPresentation GetDeadBodyTrackIfNeeded(SceneEntityRegistry tracker)
        {
            var list = tracker.GetEntitiesTrackIfNeeded<PlayerDeathPresentation>();
            return list.Count == 0 ? null : list[0] as PlayerDeathPresentation;
        }

        public void RenderCore(Action original)
        {
            AdjustCamera();
            original();
            RestoreCamera();
        }

        public void CommandsRender(Action original)
        {
            AdjustCamera();
            original();
            RestoreCamera();
        }

        public void LevelRender(Action original)
        {
            original();
            if (driver.Settings.CenterCamera && !driver.SourceHotkeys.InfoHud.Check && mouse.Right.Check)
                driver.DrawSourceCameraCursor(mouse.Position);
        }

        public void LevelLoadLevel(Action original, bool isFromLoader)
        {
            original();
            if (isFromLoader && driver.Settings.EnableExCameraDynamicsForCenterCamera && driver.Settings.CenterCamera)
                if (!exCamera.Enabled)
                {
                    exCamera.EnableHooks(ZoomLevel);
                    tasEnabledExCameraDynamics = true;
                }
        }

        public float ZoomLevelVariant(float originalArgument, Func<float> original)
            => driver.Settings.CenterCamera ? originalArgument : original();

        public Vector2 ZoomLevelScreenPositionVariant(Vector2 originalArgument, Func<Vector2> original)
            => driver.Settings.CenterCamera ? originalArgument : original();

        private Vector2? cameraTargetPosition;
        private Vector2? lockPosition;

        private Vector2 cameraOffset;
        private Vector2 canvasOffset;
        private float viewportScale = 1.0f;

        public float ZoomLevel => 1.0f / viewportScale;
        public bool ZoomedOut => viewportScale > 1.0f;

        public readonly CameraState ScreenCamera;

        private bool savedView = false;
        private readonly CameraState savedCamera;
        private CameraViewport savedCameraViewport;
        private float savedLevelZoom;
        private float savedLevelZoomTarget;
        private Vector2 savedLevelZoomFocusPoint;
        private float savedLevelScreenPadding;

        // ExCameraDynamics
        private bool tasEnabledExCameraDynamics;
        private bool savedAutomaticZooming;
        private float savedTriggerZoomOverride;

        public void Toggled()
        {
            // Enable ExCameraDynamics if needed
            if (driver.Settings.EnableExCameraDynamicsForCenterCamera && driver.Settings.CenterCamera)
            {
                if (!exCamera.Enabled)
                {
                    exCamera.EnableHooks();
                    tasEnabledExCameraDynamics = true;
                }
            }
            else if (tasEnabledExCameraDynamics)
            {
                if (exCamera.Enabled)
                {
                    exCamera.DisableHooks();
                }

                tasEnabledExCameraDynamics = false;
            }
        }


        // Avoid disabling ExCameraDynamics if a map enabled it

        internal void MarkExCameraDynamicsMapEnabled() => tasEnabledExCameraDynamics = false;

        public void OnExCameraDynamicsEnableHooks(Action orig)
        {
            MarkExCameraDynamicsMapEnabled();
            orig();
        }

        /// Adjust offset and zoom of the centered camera
        public void UpdateMeta()
        {
            if (!driver.Settings.CenterCamera || process.Frame.Scenes.CurrentScene is not OriginalLevelSceneRoot level)
            {
                return;
            }

            // Double press to reset camera
            if (driver.SourceHotkeys.FreeCamera.DoublePressed || mouse.Right.DoublePressed)
            {
                cameraOffset = Vector2.Zero;
                canvasOffset = Vector2.Zero;
                viewportScale = 1.0f;
                lockPosition = null;
                return;
            }

            var moveOffset = Vector2.Zero;
            int zoomDirection = 0;

            const float ArrowKeySensitivity = 2.0f;
            if (driver.SourceHotkeys.CameraLeft.Check)
            {
                moveOffset.X -= ArrowKeySensitivity;
            }

            if (driver.SourceHotkeys.CameraRight.Check)
            {
                moveOffset.X += ArrowKeySensitivity;
            }

            if (driver.SourceHotkeys.CameraUp.Check)
            {
                moveOffset.Y -= ArrowKeySensitivity;
            }

            if (driver.SourceHotkeys.CameraDown.Check)
            {
                moveOffset.Y += ArrowKeySensitivity;
            }

            if (driver.SourceHotkeys.FreeCamera.Check || driver.SourceHotkeys.InfoHud.Check)
            {
                if (driver.SourceHotkeys.CameraZoomIn.Check)
                {
                    zoomDirection -= 1;
                }

                if (driver.SourceHotkeys.CameraZoomOut.Check)
                {
                    zoomDirection += 1;
                }
            }

            if (zoomDirection == 0)
            {
                zoomDirection = -Math.Sign(mouse.WheelDelta);
            }

            // (Un)lock current camera target
            if (driver.SourceHotkeys.LockCamera.Pressed)
            {
                if (lockPosition != null)
                {
                    lockPosition = null;
                }
                else if (cameraTargetPosition != null)
                {
                    lockPosition = cameraTargetPosition.Value;
                }
            }

            // Account for upside down camera
            if (variants.UpsideDown)
            {
                moveOffset.Y *= -1;
            }

            bool changedCamera = false, changedCanvas = false;

            // Keep the gameplay canvas locked while using the FreeCamera hotkey
            if (driver.SourceHotkeys.FreeCamera.Check && ZoomedOut)
            {
                canvasOffset = SourceVector2Math.Add(canvasOffset, moveOffset);
                changedCanvas = moveOffset != Vector2.Zero;
            }
            else if (driver.SourceHotkeys.FreeCamera.Check || driver.SourceHotkeys.InfoHud.Check)
            {
                cameraOffset = SourceVector2Math.Add(cameraOffset, moveOffset);
                changedCamera = moveOffset != Vector2.Zero;
            }

            // Drag support while holding right mouse button
            // Avoid panning while selecting an area
            if (mouse.Right.Check && !driver.SourceHotkeys.InfoHud.Check)
            {
                float scale = ZoomLevel * level.Camera.Zoom *
                    (GameRuntime.TargetWidth / GameRuntime.GameWidth) *
                    EngineRuntime.ViewWidth / EngineRuntime.Width;

                var mouseOffset =
                    ((ISourceVectorEnvironment)process).VectorArithmetic.Divide(mouse.PositionDelta, scale);
                if (variants.UpsideDown)
                {
                    mouseOffset.Y *= -1;
                }

                if (driver.SourceHotkeys.FreeCamera.Check && ZoomedOut)
                {
                    canvasOffset = SourceVector2Math.Subtract(canvasOffset, mouseOffset);
                    changedCanvas = mouseOffset != Vector2.Zero;
                }
                else
                {
                    cameraOffset = SourceVector2Math.Subtract(cameraOffset, mouseOffset);
                    changedCamera = mouseOffset != Vector2.Zero;
                }
            }

            // Adjust camera zoom. Use faster speed for zooming out
            float delta = (viewportScale + zoomDirection * 0.1f) switch
            {
                > 1 => zoomDirection * 0.2f,
                _ => zoomDirection * 0.1f
            };
            viewportScale = Math.Max(0.2f, viewportScale + delta);

            if (cameraTargetPosition is { } target &&
                ((OriginalChapterMapData)level.Session.MapData).Bounds is var bounds)
            {
                if (changedCamera)
                {
                    var result = Clamp(SourceVector2Math.Add(target, cameraOffset), bounds.X, bounds.Y, bounds.Right,
                        bounds.Bottom);
                    cameraOffset = SourceVector2Math.Subtract(result, target);
                }

                if (changedCanvas)
                {
                    var result = Clamp(SourceVector2Math.Add(SourceVector2Math.Add(target, cameraOffset), canvasOffset),
                        bounds.X, bounds.Y, bounds.Right, bounds.Bottom);
                    canvasOffset = SourceVector2Math.Subtract(SourceVector2Math.Subtract(result, target), cameraOffset);
                }
            }
        }

        /// Adjusts the camera to apply target-locking, offset, and zooming
        public void AdjustCamera()
        {
            if (!driver.Settings.CenterCamera || process.Frame.Scenes.CurrentScene is not OriginalLevelSceneRoot level)
            {
                return;
            }

            var camera = level.Camera;
            if (((ISourceScene)process.Frame.Scenes.CurrentScene).SourceScene.Entities
                .GetTrackedEntity<RoomPlayerSession>(
                    typeof(global::Celeste.Simulation.RoomPlayerSession).FullName) is { } player)

            {
                cameraTargetPosition = lockPosition ?? player.Position;
            }
            else if (GetDeadBodyTrackIfNeeded(((ISourceScene)process.Frame.Scenes.CurrentScene).SourceScene.Entities) is
                     { } deadBody)
            {
                cameraTargetPosition = lockPosition ?? deadBody.Position;
            }

            if (cameraTargetPosition is not { } target)
            {
                return;
            }

            if (driver.Settings.CenterCameraHorizontallyOnly)
            {
                target.Y = camera.Position.Y + camera.Viewport.Height / 2.0f;
            }

            // Backup original values
            savedCamera.CopyFrom(camera);
            savedCameraViewport = camera.Viewport;
            savedLevelZoom = level.Zoom;
            savedLevelZoomTarget = level.ZoomTarget;
            savedLevelZoomFocusPoint = level.ZoomFocusPoint;
            savedLevelScreenPadding = level.ScreenPadding;

            savedView = true;

            // Apply camera changes
            camera.Position = SourceVector2Math.Subtract(SourceVector2Math.Add(target, cameraOffset),
                new Vector2(camera.Viewport.Width / 2f, camera.Viewport.Height / 2f));

            if (exCamera.Enabled)
            {
                savedAutomaticZooming = exCamera.AutomaticZooming;
                savedTriggerZoomOverride = exCamera.TriggerZoomOverride;

                exCamera.SetCamera(level,
                    SourceVector2Math.Add(SourceVector2Math.Add(target, cameraOffset), canvasOffset),
                    ZoomLevel);
            }
            else
            {
                level.Zoom = level.ZoomTarget = ZoomLevel;
                level.ZoomFocusPoint = new Vector2(GameRuntime.GameWidth / 2.0f,
                    GameRuntime.GameHeight / 2.0f);
                if (ZoomedOut)
                {
                    level.ZoomFocusPoint = SourceVector2Math.Add(level.ZoomFocusPoint, canvasOffset);
                }

                level.ScreenPadding = 0;
            }

            // Prepare screen-space camera for usage with OffscreenHitbox
            ScreenCamera.Viewport.Width = (int)Math.Round(GameRuntime.GameWidth * viewportScale);
            ScreenCamera.Viewport.Height = (int)Math.Round(GameRuntime.GameHeight * viewportScale);
            ScreenCamera.Position = SourceVector2Math.Subtract(SourceVector2Math.Add(target, cameraOffset),
                new Vector2(ScreenCamera.Viewport.Width / 2f, ScreenCamera.Viewport.Height / 2f));

            if (ZoomedOut)
            {
                ScreenCamera.Position = SourceVector2Math.Add(ScreenCamera.Position, canvasOffset);
            }

            ScreenCamera.UpdateMatrices();
        }

        /// Restore camera settings to previous values, to avoid altering gameplay
        public void RestoreCamera()
        {
            if (process.Frame.Scenes.CurrentScene is not OriginalLevelSceneRoot level || !savedView)
            {
                return;
            }

            level.Camera.CopyFrom(savedCamera);
            level.Camera.Viewport = savedCameraViewport;
            level.Zoom = savedLevelZoom;
            level.ZoomTarget = savedLevelZoomTarget;
            level.ZoomFocusPoint = savedLevelZoomFocusPoint;
            level.ScreenPadding = savedLevelScreenPadding;

            if (exCamera.Enabled)
            {
                exCamera.AutomaticZooming = savedAutomaticZooming;
                exCamera.TriggerZoomOverride = savedTriggerZoomOverride;
            }

            savedView = false;
        }
    }
}
