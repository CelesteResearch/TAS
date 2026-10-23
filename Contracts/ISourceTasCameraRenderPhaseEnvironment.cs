using System;

namespace Celeste.Core.Diagnostics.Tas
{
    public interface ISourceTasCameraRenderPhaseEnvironment
    {
        ISourceTasCameraRenderPhase TasCameraRenderPhase { get; }
    }
}
