using Celeste.Core.Display;
using System.Numerics;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Celeste.Core.Rendering;
using Celeste.Game.Execution.Engine;
using Celeste.Game.Execution.Levels;
using Celeste.Scene.Scenes;
using global::Celeste.Game.Execution;

namespace Celeste.Mods.Tas.Hitboxes
{
    internal sealed class SourceTasCameraHitbox
    {
        private readonly TasPlaybackDriver driver;
        private readonly PackedColor color = PackedColor.LightBlue * 0.75f;
        private Vector2 cameraTopLeft = Vector2.Zero;
        private Vector2 cameraBottomRight = Vector2.Zero;

        internal SourceTasCameraHitbox(TasPlaybackDriver driver)
        {
            this.driver = driver;
        }

        internal void Load()
        {
            driver.SourceGameplayEvents.PostUpdate += scene =>
            {
                if (scene is not OriginalLevelSceneRoot level) return;
                cameraTopLeft =
                    Information.SourceTasLevelCoordinateConversion.MouseToWorld(level, driver, Vector2.Zero);
                cameraBottomRight = Information.SourceTasLevelCoordinateConversion.MouseToWorld(level, driver,
                    new Vector2(EngineRuntime.ViewWidth, EngineRuntime.ViewHeight));
            };
            driver.SourceGameplayEvents.PostDebugRender += scene =>
            {
                if (!driver.Settings.CenterCamera || !driver.Settings.ShowCameraHitboxes ||
                    scene is not OriginalLevelSceneRoot) return;
                SourcePrimitiveDrawing.HollowRect(((ISourceRenderEnvironment)driver.Process).CurrentContext, this,
                    cameraTopLeft,
                    cameraBottomRight.X - cameraTopLeft.X, cameraBottomRight.Y - cameraTopLeft.Y, color);
            };
        }
    }
}
