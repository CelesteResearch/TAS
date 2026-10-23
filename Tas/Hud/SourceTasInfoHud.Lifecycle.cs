using System;
using System.Reflection;
using MonoMod.RuntimeDetour;
using Celeste.Core.Rendering;
using Celeste.Scene.Scenes;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Menus.Overworld;
using Celeste.Game.Minigames.CelesteClassic.Emulation;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Hud
{
    public sealed partial class SourceTasInfoHud
    {
        private Hook levelRenderHook;
        private Hook emulatorRenderHook;
        private Hook sceneRenderHook;

        [SourceTasLoad]
        private static void SourceLoad() => CelesteTasModule.Instance.Driver.Hud.Load();

        [SourceTasUnload]
        private static void SourceUnload() => CelesteTasModule.Instance.Driver.Hud.Unload();

        private void Load()
        {
            levelRenderHook = new Hook(RequireRender(typeof(OriginalLevelSceneRoot)),
                (Action<Action<OriginalLevelSceneRoot>, OriginalLevelSceneRoot>)LevelOnRender);
            emulatorRenderHook = new Hook(RequireRender(typeof(ClassicConsoleRuntime)),
                (Action<Action<ClassicConsoleRuntime>, ClassicConsoleRuntime>)EmulatorOnRender);
            sceneRenderHook = new Hook(RequireRender(typeof(SourceSceneState)),
                (Action<Action<SourceSceneState>, SourceSceneState>)SceneOnRender);
        }

        private void Unload()
        {
            levelRenderHook.Dispose();
            emulatorRenderHook.Dispose();
            sceneRenderHook.Dispose();
        }

        private static MethodInfo RequireRender(Type type) =>
            type.GetMethod("Render", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly,
                null, Type.EmptyTypes, null)
            ?? throw new InvalidOperationException("The original source Render body is absent: " + type.FullName);

        private void LevelOnRender(Action<OriginalLevelSceneRoot> original, OriginalLevelSceneRoot self)
        {
            original(self);
            DrawInfo(self, ((ISourceRenderEnvironment)process).CurrentContext);
            driver.InfoMouse.DragAndDropHud();
        }

        private void EmulatorOnRender(Action<ClassicConsoleRuntime> original, ClassicConsoleRuntime self)
        {
            original(self);
            DrawInfo(self, ((ISourceRenderEnvironment)process).CurrentContext);
            driver.InfoMouse.DragAndDropHud();
        }

        private void SceneOnRender(Action<SourceSceneState> original, SourceSceneState self)
        {
            original(self);
            if (self is OverworldSequence)
            {
                DrawInfo(self, ((ISourceRenderEnvironment)process).CurrentContext, drawSubpixelIndicator: false);
                driver.InfoMouse.DragAndDropHud();
            }
        }
    }
}
