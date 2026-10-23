using Celeste.Core.Diagnostics.Tas;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        public void EnableRun()
        {
            if (Running) return;
            if (!controller.StartupPreloadReady)
            {
                NextState = SourceTasManagerState.Running;
                return;
            }

            CurrState = NextState = SourceTasManagerState.Running;
            PlaybackSpeed = 1.0f;
            FrameStepBackTargetFrame = -1;
            controller.Stop();
            RefreshInputs();
            if (controller.Inputs.Count == 0)
            {
                CurrState = NextState = SourceTasManagerState.Disabled;
                SyncChecker.ReportRunFinished();
                return;
            }

            RunEvents.InvokeEnableRun();
            Log($"Starting TAS: {controller.FilePath}");
        }

        public void DisableRun()
        {
            if (!Running) return;
            Log("Stopping TAS");
            RunEvents.InvokeDisableRun();
            SyncChecker.ReportRunFinished();
            CurrState = NextState = SourceTasManagerState.Disabled;
            controller.Stop();
        }

        public void EnableRunLater() => NextState = SourceTasManagerState.Running;

        public void DisableRunLater()
        {
            DisableRunRequestRevision = unchecked(DisableRunRequestRevision + 1);
            NextState = SourceTasManagerState.Disabled;
        }
    }
}
