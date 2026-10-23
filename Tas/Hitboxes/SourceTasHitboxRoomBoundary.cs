using System;
using Celeste.Core.Rendering;
using Celeste.Everest.Reflection;
using Celeste.Game.Execution.Levels;
using Celeste.Scene.Tracking;
using Celeste.Simulation;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using MonoMod.RuntimeDetour;

namespace Celeste.Mods.Tas.Hitboxes
{
    public sealed class SourceTasHitboxRoomBoundary
    {
        private readonly TasPlaybackDriver driver;
        private Hook debugRenderHook;

        internal SourceTasHitboxRoomBoundary(TasPlaybackDriver driver) => this.driver = driver;

        public void Load()
        {
            var reflection = ((ISourceEverestReflectionEnvironment)driver.Process).Reflection;
            var method = reflection.SourceMethods.RequireActualMethod(
                "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", 100665914);
            debugRenderHook = new Hook(method, (DebugRenderHook)EntityListOnDebugRender);
        }

        public void Unload() => debugRenderHook?.Dispose();

        private delegate void DebugRenderOriginal(SceneEntityRegistry self,
            ISourceRenderContext context, object camera, object scene);

        private delegate void DebugRenderHook(DebugRenderOriginal orig, SceneEntityRegistry self,
            ISourceRenderContext context, object camera, object scene);

        private void EntityListOnDebugRender(DebugRenderOriginal orig, SceneEntityRegistry self,
            ISourceRenderContext context, object camera, object scene)
        {
            orig(self, context, camera, scene);
            if (driver.Settings.ShowHitboxes && driver.Settings.CenterCamera
                                             && scene is OriginalLevelSceneRoot level
                                             && level.SourceScene.Entities.GetTrackedEntity<RoomPlayerSession>(
                                                     typeof(global::Celeste.Simulation.RoomPlayerSession).FullName) is
                                                 { } player)

            {
                var bounds = level.Bounds;
                float topExtra = (float)(Math.Floor(player.CenterY - player.Top) + 1);
                SourcePrimitiveDrawing.HollowRect(context, this, bounds.X - 1,
                    bounds.Y - topExtra, bounds.Width + 2, bounds.Height + topExtra + 1,
                    SourceTasHitboxColor.RespawnTriggerColor);
            }
        }
    }
}
