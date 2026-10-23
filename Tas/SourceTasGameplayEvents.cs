using System;
using Celeste.Scene.Scenes;

namespace Celeste.Mods.Tas
{
    internal sealed class SourceTasGameplayEvents
    {
        public event Action<SourceSceneState> PostUpdate;
        public event Action<SourceSceneState> PostDebugRender;
        public event Action PostRender;
        internal void InvokePostUpdate(SourceSceneState scene) => PostUpdate?.Invoke(scene);
        internal void InvokePostDebugRender(SourceSceneState scene) => PostDebugRender?.Invoke(scene);
        internal void InvokePostRender() => PostRender?.Invoke();
    }
}
