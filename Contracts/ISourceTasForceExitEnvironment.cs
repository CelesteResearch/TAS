using System;

namespace Celeste.Core.Diagnostics.Tas
{
    public interface ISourceTasForceExitEnvironment
    {
        void InvokeSourceGameOnExiting(object sender, EventArgs args);
        void AbortSourceMainThread();
    }
}
