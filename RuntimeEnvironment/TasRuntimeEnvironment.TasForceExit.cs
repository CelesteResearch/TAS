using System;
using Celeste.Core.Diagnostics.Tas;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : ISourceTasForceExitEnvironment
    {
        void ISourceTasForceExitEnvironment.InvokeSourceGameOnExiting(object sender, EventArgs args)
            => process.ActualGame.InvokeSourceOnExiting(sender, args);

        void ISourceTasForceExitEnvironment.AbortSourceMainThread()
        {
            // Preserve the original Thread.Abort operation and its runtime-specific failure behavior.
#pragma warning disable CS0618, SYSLIB0006
            process.MainThreadProcess.MainThread.Abort();
#pragma warning restore CS0618, SYSLIB0006
        }
    }
}
