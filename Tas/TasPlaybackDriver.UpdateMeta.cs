using System;
using Celeste.Core.Diagnostics.Tas;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasPopupToast.Entry frameStepEofToast;
        private SourceTasPopupToast.Entry autoPauseDraft;
        private bool seenAutoPauseToast;
        private SourceTasPopupToast.Entry frameStepBackToast;

        private void UpdateMeta()
        {
            if (!hotkeys.Initialized)
            {
                return; // Still loading
            }

            hotkeys.UpdateMeta();
            UpdateSourceSavestateMeta();
            RunEvents.InvokeUpdateMeta();

            SendSourceStudioState();

            // Pending EnableRun/DisableRun. Prevent overwriting
            if (Running && NextState == SourceTasManagerState.Disabled ||
                !Running && NextState != SourceTasManagerState.Disabled)
            {
                return;
            }

            // Check if the TAS should be enabled / disabled
            if (hotkeys.StartStop.Pressed)
            {
                if (Running)
                {
                    DisableRun();
                }
                else
                {
                    EnableRun();
                }

                return;
            }

            if (hotkeys.Restart.Pressed)
            {
                DisableRun();
                EnableRun();
                return;
            }

            if (Running && IsSourceRecording())
            {
                // Force recording at 1x playback
                NextState = SourceTasManagerState.Running;
                PlaybackSpeed = 1.0f;
                return;
            }

            if (frameStepBackAmount > 0.0f)
            {
                int frames = (int)Math.Round(frameStepBackAmount / PlaybackDeltaTime);
                frameStepBackTimeout -= PlaybackDeltaTime;

                // Advance a frame extra, since otherwise 0s would only be rendered AFTER the lag from the TAS restart
                string text =
                    $"Frame Step Back: -{frames}f   (in {Math.Max(0.0f, frameStepBackTimeout - PlaybackDeltaTime):F2}s)";
                if (frameStepBackToast is not { Active: true })
                {
                    frameStepBackToast = ShowSourcePopupToast(text);
                }
                else
                {
                    frameStepBackToast.Text = text;
                }

                frameStepBackToast.Timeout = frameStepBackTimeout;

                if (frameStepBackTimeout <= 0.0f)
                {
                    FrameStepBackTargetFrame = Math.Max(1, controller.CurrentFrameInTas - frames);

                    controller.Stop();
                    CurrState = NextState = SourceTasManagerState.Running;
                    RunEvents.InvokeEnableRun();

                    frameStepBackTimeout = 0.0f;
                    frameStepBackAmount = 0.0f;
                }
            }

            if (Running && hotkeys.FastForwardComment.Pressed)
            {
                controller.FastForwardToNextLabel();
                return;
            }

            switch (CurrState)
            {
                case SourceTasManagerState.Running:
                    if (hotkeys.PauseResume.Pressed || hotkeys.FrameAdvance.Pressed)
                    {
                        NextState = SourceTasManagerState.Paused;
                    }
                    else if (hotkeys.FrameStepBack.Pressed)
                    {
                        NextState = SourceTasManagerState.Paused;
                        frameStepBackTimeout = FrameStepBackTime;
                        frameStepBackAmount = PlaybackDeltaTime;
                    }

                    break;

                case SourceTasManagerState.FrameAdvance:
                    NextState = SourceTasManagerState.Paused;
                    break;

                case SourceTasManagerState.Paused:
                    if (frameStepBackAmount > 0.0f)
                    {
                        if (hotkeys.FrameStepBack.Repeated)
                        {
                            frameStepBackTimeout = FrameStepBackTime;
                            frameStepBackAmount += PlaybackDeltaTime;
                        }
                        else if (hotkeys.FastForward.Check)
                        {
                            // Fast-forward during pause plays at 0.5x speed (due to alternating advancing / pausing)
                            frameStepBackTimeout = FrameStepBackTime;
                            frameStepBackAmount += PlaybackDeltaTime * 2.0f;
                        }
                        else if (hotkeys.SlowForward.Check)
                        {
                            frameStepBackTimeout = FrameStepBackTime;
                            frameStepBackAmount += Settings.SlowForwardSpeed;
                        }
                    }
                    else if (hotkeys.FrameStepBack.Repeated)
                    {
                        frameStepBackTimeout = FrameStepBackTime;
                        frameStepBackAmount = PlaybackDeltaTime;
                    }
                    else if (hotkeys.PauseResume.Pressed)
                    {
                        NextState = SourceTasManagerState.Running;
                    }
                    else if (hotkeys.FrameAdvance.Repeated || hotkeys.FastForward.Check)
                    {
                        // Prevent frame-advancing into the end of the TAS
                        if (!controller.CanPlayback)
                        {
                            RefreshInputs(); // Ensure there aren't any new inputs
                        }

                        if (controller.CanPlayback)
                        {
                            NextState = SourceTasManagerState.FrameAdvance;
                        }
                        else
                        {
                            const string text = "Cannot advance further: Reached end-of-file";
                            const float duration = 1.0f;
                            if (frameStepEofToast is not { Active: true })
                            {
                                frameStepEofToast = ShowSourcePopupToast(text, duration);
                            }
                            else
                            {
                                frameStepEofToast.Text = text;
                                frameStepEofToast.Timeout = duration;
                            }
                        }
                    }

                    break;

                case SourceTasManagerState.Disabled:
                default:
                    break;
            }

            // Allow altering the playback speed with the right thumb-stick
            float normalSpeed = hotkeys.RightThumbSticksX switch
            {
                >= 0.001f => hotkeys.RightThumbSticksX * Settings.FastForwardSpeed,
                <= -0.001f => (1 + hotkeys.RightThumbSticksX) * Settings.SlowForwardSpeed,
                _ => 1.0f,
            };

            // Apply fast / slow forwarding
            switch (NextState)
            {
                case SourceTasManagerState.Running when FrameStepBackTargetFrame != -1:
                    PlaybackSpeed = 400.0f;
                    break;
                case SourceTasManagerState.Running when hotkeys.FastForward.Check:
                    PlaybackSpeed = Settings.FastForwardSpeed;
                    break;
                case SourceTasManagerState.Running when hotkeys.SlowForward.Check:
                    PlaybackSpeed = Settings.SlowForwardSpeed;
                    break;

                case SourceTasManagerState.Paused or SourceTasManagerState.SlowForward
                    when hotkeys.SlowForward.Check && frameStepBackAmount <= 0.0f:
                    PlaybackSpeed = Settings.SlowForwardSpeed;
                    NextState = SourceTasManagerState.SlowForward;
                    break;
                case SourceTasManagerState.Paused or SourceTasManagerState.SlowForward:
                    PlaybackSpeed = normalSpeed;
                    NextState = SourceTasManagerState.Paused;
                    break;

                case SourceTasManagerState.FrameAdvance:
                    PlaybackSpeed = normalSpeed;
                    break;

                default:
                    PlaybackSpeed = controller.HasFastForward ? controller.CurrentFastForward!.Speed : normalSpeed;
                    break;
            }
        }
    }
}
