using System;
using Celeste.Core.Diagnostics.Tas;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        public event Action<SourceTasNaturalEofObservation> NaturalEofReached;

        internal void Update()
        {
            if (!controller.StartupPreloadReady)
            {
                while (mainThreadActions.TryDequeue(out var startupAction))
                    startupAction.Invoke();
                return;
            }

            controller.RefreshPendingStartupPreload();
            if (!Running && NextState == SourceTasManagerState.Running)
                EnableRun();
            if (Running && NextState == SourceTasManagerState.Disabled)
                DisableRun();

            UpdateSourceSavestates();
            CurrState = NextState;
            while (mainThreadActions.TryDequeue(out var action))
                action.Invoke();

            if (!Running || CurrState == SourceTasManagerState.Paused || gameHost.Loading())
                return;
            if (HasSourceCriticalErrorHandler())
            {
                DisableRun();
                return;
            }

            if (FrameStepBackTargetFrame > 0)
            {
                NextState = SourceTasManagerState.Running;
                PlaybackSpeed = 400.0f;
            }
            else if (controller.CurrentFastForward is { } forward && forward.Frame > controller.CurrentFrameInTas)
            {
                NextState = SourceTasManagerState.Running;
                PlaybackSpeed = forward.Speed;
            }

            controller.AdvanceFrame(out bool couldPlayback);
            if (!couldPlayback)
            {
                Log(
                    $"TAS input exhausted: frame={controller.CurrentFrameInTas}, total={controller.Inputs.Count}, reload={controller.NeedsReload}, file={controller.FilePath}");
                NaturalEofReached?.Invoke(new SourceTasNaturalEofObservation(
                    controller.CurrentFrameInTas, controller.Inputs.Count, controller.FilePath,
                    controller.NeedsReload));
                DisableRun();
                return;
            }

            if (FrameStepBackTargetFrame > 0 && controller.CurrentFrameInTas >= FrameStepBackTargetFrame)
            {
                FrameStepBackTargetFrame = -1;
                NextState = SourceTasManagerState.Paused;
            }
            else if (!controller.CanPlayback && Settings.AutoPauseDraft && IsSourceDraft())
            {
                NextState = SourceTasManagerState.Paused;
                if (CurrState == SourceTasManagerState.Running && !FastForwarding)
                    ShowSourceAutoPauseDraftToast();
            }
            else if (FrameStepBackTargetFrame == -1 && controller.Break &&
                     (controller.CanPlayback || IsSourceDraft()))
            {
                controller.NextLabelFastForward = null;
                NextState = SourceTasManagerState.Paused;
            }

            if (SourceSafeCommand.DisallowUnsafeInput && controller.CurrentFrameInTas > 1)
                RejectSourceUnsafeSceneOrOptions();
        }
    }
}
