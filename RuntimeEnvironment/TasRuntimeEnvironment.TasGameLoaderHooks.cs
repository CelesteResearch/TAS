using System;
using Celeste.Everest.Runtime.Startup;
using Celeste.Game.Presentation.Environment;
using Celeste.Scene.Scenes;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : IEverestGameLoaderSourceHooksEnvironment
    {
        private Action<EverestGameLoaderScene>? sourceTasLoaderBeginCompleted;
        private Func<EverestGameLoaderScene, HiresSnowState, SourceSceneState>? sourceTasLoaderNextSceneOverride;

        Action<EverestGameLoaderScene>? IEverestGameLoaderSourceHooksEnvironment.SourceLoaderBeginCompleted
            => sourceTasLoaderBeginCompleted;

        Func<EverestGameLoaderScene, HiresSnowState, SourceSceneState>? IEverestGameLoaderSourceHooksEnvironment.
            SourceLoaderNextSceneOverride
            => sourceTasLoaderNextSceneOverride;

        internal void InstallSourceTasGameLoaderHooks(Action<EverestGameLoaderScene> afterBegin,
            Func<EverestGameLoaderScene, HiresSnowState, SourceSceneState> nextSceneOverride)
        {
            sourceTasLoaderBeginCompleted = afterBegin;
            sourceTasLoaderNextSceneOverride = nextSceneOverride;
        }

        internal void UninstallSourceTasGameLoaderHooks()
        {
            sourceTasLoaderBeginCompleted = null;
            sourceTasLoaderNextSceneOverride = null;
        }
    }
}
