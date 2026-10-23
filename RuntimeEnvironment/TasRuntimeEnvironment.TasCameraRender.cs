using System;
using Celeste.Core.Diagnostics.Tas;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : ISourceTasCameraRenderPhase,
        ISourceTasCameraRenderPhaseEnvironment
    {
        private Action<Action> sourceTasCenterRenderCore;
        private Action<Action> sourceTasCenterCommandsRender;
        private Action<Action> sourceTasCenterLevelRender;
        private Action<Action, bool> sourceTasCenterLevelLoadLevel;
        private Action sourceTasPostRender;
        ISourceTasCameraRenderPhase ISourceTasCameraRenderPhaseEnvironment.TasCameraRenderPhase => this;

        internal void InstallSourceCenterCameraHooks()
        {
            sourceTasCenterRenderCore = ActualTasDriver.SourceCenterRenderCore;
            sourceTasCenterCommandsRender = ActualTasDriver.SourceCenterCommandsRender;
            sourceTasCenterLevelRender = ActualTasDriver.SourceCenterLevelRender;
            sourceTasCenterLevelLoadLevel = ActualTasDriver.SourceCenterLevelLoadLevel;
        }

        internal void UninstallSourceCenterCameraHooks()
        {
            sourceTasCenterRenderCore = null;
            sourceTasCenterCommandsRender = null;
            sourceTasCenterLevelRender = null;
            sourceTasCenterLevelLoadLevel = null;
        }

        internal void InstallSourceTasGameplayRenderHook() => sourceTasPostRender += ActualTasDriver.OnSourcePostRender;

        [SourceTasLoad]
        private static void LoadSourceTasGameplayRenderHook() =>
            TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process).InstallSourceTasGameplayRenderHook();

        void ISourceTasCameraRenderPhase.RenderCore(Action original)
        {
            if (sourceTasCenterRenderCore == null) original();
            else sourceTasCenterRenderCore(original);
            sourceTasPostRender?.Invoke();
        }

        void ISourceTasCameraRenderPhase.CommandsRender(Action original)
        {
            if (sourceTasCenterCommandsRender == null) original();
            else sourceTasCenterCommandsRender(original);
        }

        void ISourceTasCameraRenderPhase.LevelRender(Action original)
        {
            if (sourceTasCenterLevelRender == null) original();
            else sourceTasCenterLevelRender(original);
        }

        void ISourceTasCameraRenderPhase.LevelLoadLevel(Action original, bool isFromLoader)
        {
            if (sourceTasCenterLevelLoadLevel == null) original();
            else sourceTasCenterLevelLoadLevel(original, isFromLoader);
        }
    }
}
