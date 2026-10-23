using System;
using System.Reflection;
using Celeste.Core.Rendering;
using Celeste.Core.Scheduling;
using Celeste.Scene.Tracking;
using Celeste.Scene.Scenes;
using Celeste.Game.Execution.Engine.Rendering;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        [SourceTasLoad]
        private static void LoadSourceGameplayEvents()
        {
            var driver = CelesteTasModule.Instance.Driver;
            driver.SourceHooks.HookAfter<SceneEntityRegistry>(RequireSourceEventMethod(typeof(SceneEntityRegistry),
                    "Update", new[] { typeof(ISourceEngineContext) }),
                list => driver.gameplayEvents.InvokePostUpdate((SourceSceneState)list.ActualSceneIdentity));
            driver.SourceHooks.HookAfter<SceneEntityRegistry>(RequireSourceEventMethod(typeof(SceneEntityRegistry),
                    "DebugRender", new[] { typeof(ISourceRenderContext), typeof(object), typeof(object) }),
                list => driver.gameplayEvents.InvokePostDebugRender((SourceSceneState)list.ActualSceneIdentity));
            driver.SourceHooks.HookAfter(RequireSourceEventMethod(typeof(OriginalEngineDraw),
                "RenderCore", Type.EmptyTypes), () => driver.gameplayEvents.InvokePostRender());
            driver.ModuleAttributes.CollectOwnMethods<SourceTasPreEngineUpdateAttribute>();
            driver.ModuleAttributes.CollectOwnMethods<SourceTasPostEngineUpdateAttribute>();
        }

        private static MethodInfo RequireSourceEventMethod(Type type, string name, Type[] parameters)
            => type.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                    BindingFlags.DeclaredOnly, null, parameters, null)
               ?? throw new InvalidOperationException(
                   "Actual TAS event method is absent: " + type.FullName + "." + name);
    }
}
