using System;

namespace Celeste.Core.Diagnostics.Tas
{
    public interface ISourceTasCameraRenderPhase
    {
        void RenderCore(Action original);
        void CommandsRender(Action original);
        void LevelRender(Action original);
        void LevelLoadLevel(Action original, bool isFromLoader);
    }
}
