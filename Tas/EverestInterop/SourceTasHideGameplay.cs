using System;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Celeste.Mods.Tas.Gameplay.Optimization;

namespace Celeste.Mods.Tas.EverestInterop
{
    internal sealed class SourceTasHideGameplay
    {
        private readonly TasPlaybackDriver driver;
        private readonly SourceTasOptimizationMethods methods;
        private ILHook render;

        internal SourceTasHideGameplay(TasPlaybackDriver driver, SourceTasOptimizationMethods methods)
        {
            this.driver = driver;
            this.methods = methods;
        }

        internal void Load() => render = new ILHook(
            methods.Require("Celeste.GameplayRenderer", "Render", "Monocle.Scene"),
            il =>
            {
                var cursor = new ILCursor(il).Goto(0);
                var actualRender =
                    typeof(Celeste.Scene.Scheduling.SceneUpdateScheduler).GetMethod("RenderEntitiesExcept");
                if (actualRender == null) throw new MissingMethodException("Actual Scene.RenderEntitiesExcept");
                cursor.GotoNext(i => methods.Calls(i, actualRender));
                // The compiled renderer can retain its profile condition below this call's arguments.
                // Replace only the call, consuming exactly its receiver/context/tag stack operands.
                cursor.Remove();
                cursor.EmitDelegate<Action<Celeste.Scene.Scheduling.SceneUpdateScheduler,
                    Celeste.Core.Rendering.ISourceRenderContext, Celeste.Scene.Scheduling.SceneTagSet>>((scheduler,
                    context, excludedTags) =>
                {
                    if (driver.Settings.ShowGameplay) scheduler.RenderEntitiesExcept(context, excludedTags);
                });
            });

        internal void Unload() => render.Dispose();
    }
}
