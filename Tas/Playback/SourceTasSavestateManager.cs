using Celeste.Core.Diagnostics.Tas;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using StudioCommunication.Util;
using Celeste.Mods.Tas.Input;
using Celeste.Mods.Tas.Module;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;

namespace Celeste.Mods.Tas.Playback
{
    /// Handles saving / loading game state with SpeedrunTool
    internal sealed class SourceTasSavestateManager
    {
        private readonly TasPlaybackDriver driver;
        internal TasPlaybackDriver Driver => driver;
        internal SourceTasSavestateManager(TasPlaybackDriver driver) => this.driver = driver;

        public IEnumerable<SourceTasSavestate> AllSavestates => BreakpointSavestates
            .Concat(ManualSavestate.HasValue ? [ManualSavestate.Value] : [])
            .OrderBy(state => state.Frame);

        private SourceTasSavestate? ManualSavestate;
        private readonly List<SourceTasSavestate> BreakpointSavestates = [];

        // Only included in hot-reloading support
        public void ClearAllSavestates()
        {
            ManualSavestate?.Clear();
            ManualSavestate = null;

            foreach (var state in BreakpointSavestates)
            {
                state.Clear();
            }

            BreakpointSavestates.Clear();
        }

        /// Update for each TAS frame
        public void Update()
        {
            var controller = driver.Controller;

            if (driver.CurrState != SourceTasManagerState.Running)
            {
                // Only savestate while TAS is actively running and not while paused
                return;
            }

            if (!driver.SourceSpeedrunTool.Installed)
            {
                // Validate that no savestate breakpoints are used
                if (controller.FastForwards.GetValueOrDefault(controller.CurrentFrameInTas) is
                    { SaveState: true } fastForward)
                {
                    driver.ShowSourcePopupToastAndLog($"""
                                                       {Path.GetFileName(fastForward.FilePath)} line {fastForward.FileLine}:
                                                       Found savestate breakpoint '{fastForward.Format()}'.
                                                       The 'Speedrun Tool' mod is required to use savestates with CelesteTAS.
                                                       """, timeout: 5.0f);
                }

                return;
            }

            // Only save-state when the current breakpoint is new
            if (controller.CurrentFrameInTas < controller.Inputs.Count
                && controller.FastForwards.GetValueOrDefault(controller.CurrentFrameInTas) is
                    { SaveState: true } currentFastForward
                && controller.CurrentFrameInTas == currentFastForward.Frame
                && Save(byBreakpoint: true, out var savestate)
               )
            {
                if (driver.SourceSpeedrunTool.MultipleSaveSlotsSupported)
                {
                    BreakpointSavestates.Add(savestate);
                }
                else
                {
                    ManualSavestate = savestate;
                }

                return;
            }

            // Autoload state after entering the level, if the TAS was started outside the level
            if (driver.Process.Frame.Scenes.CurrentScene is Level)
            {
                // Load ideal savestate to start playing from for frame step back
                if (driver.FrameStepBackTargetFrame > 0)
                {
                    foreach (var state in AllSavestates.Reverse())
                    {
                        if (state.Frame > driver.FrameStepBackTargetFrame ||
                            state.Frame <= controller.CurrentFrameInTas)
                        {
                            continue;
                        }

                        state.Load();
                        return;
                    }

                    // No viable state found
                    return;
                }

                foreach (var state in AllSavestates.Reverse())
                {
                    if (state.Frame <= controller.CurrentFrameInTas
                        || driver.Controller.FastForwards.Values.Any(val =>
                            val.Frame >= controller.CurrentFrameInTas && val.Frame < state.Frame && val.ForceStop)
                        || controller.FilePath != state.Controller.FilePath || state.BreakpointCommented)
                    {
                        continue;
                    }

                    state.Load();
                    if (state.Frame == controller.CurrentFrameInTas)
                    {
                        driver.CurrState = driver.NextState = SourceTasManagerState.Paused;
                    }

                    return;
                }
            }
        }

        /// Update for checking hotkeys
        internal void UpdateMeta()
        {
            if (!driver.SourceSpeedrunTool.Installed)
            {
                return;
            }

            if (driver.Running && driver.SourceHotkeys.SaveState.Pressed)
            {
                ManualSavestate?.Clear();
                ManualSavestate = Save(byBreakpoint: false, out var savestate) ? savestate : null;
                return;
            }

            if (driver.SourceHotkeys.ClearState.Pressed)
            {
                if (ManualSavestate != null)
                {
                    ManualSavestate.Value.Clear();
                    ManualSavestate = null;
                }
                else
                {
                    foreach (var state in BreakpointSavestates)
                    {
                        state.Clear();
                    }

                    BreakpointSavestates.Clear();
                }

                return;
            }

            if (driver.Running)
            {
                ClearDeletedSavestates();
            }
        }

        /// Clean-up deleted breakpoint savestates, to free unused memory
        private void ClearDeletedSavestates()
        {
            if (driver.SourceSpeedrunTool.MultipleSaveSlotsSupported)
            {
                foreach (var state in BreakpointSavestates)
                {
                    if (state.BreakpointDeleted)
                    {
                        state.Clear();
                    }
                }

                BreakpointSavestates.RemoveAll(state => state.BreakpointDeleted);
            }
            else if (ManualSavestate is { SavedByBreakpoint: true, BreakpointDeleted: true } manual)
            {
                manual.Clear();
                ManualSavestate = null;
            }
        }

        internal const int EnableRunPriority = 1;

        [SourceTasEnableRun(EnableRunPriority)]
        private static void SourceEnableRun() => CelesteTasModule.Instance.Driver.SourceSavestateManager.EnableRun();

        internal void EnableRun()
        {
            if (!driver.SourceSpeedrunTool.Installed || driver.Process.Frame.Scenes.CurrentScene is not Level)
            {
                return;
            }

            // If the file was changed, there might be leftover breakpoints
            ClearDeletedSavestates();

            // Collect savestates which are stored but no longer working
            var staleSavestates = new HashSet<SourceTasSavestate>();

            // Load ideal savestate to start playing from for frame step back
            if (driver.FrameStepBackTargetFrame > 0)
            {
                foreach (var state in AllSavestates.Reverse())
                {
                    if (state.Frame > driver.FrameStepBackTargetFrame)
                    {
                        continue;
                    }

                    if (!state.Load())
                    {
                        staleSavestates.Add(state);
                        continue;
                    }

                    if (state.Frame == driver.FrameStepBackTargetFrame)
                    {
                        driver.CurrState = driver.NextState = SourceTasManagerState.Paused;
                    }

                    goto CleanupStates;
                }

                // No viable state found
                goto CleanupStates;
            }

            foreach (var state in AllSavestates.Reverse())
            {
                if (state.BreakpointCommented
                    || driver.Controller.FastForwards.Any(entry =>
                        entry.Value.Frame < state.Frame && entry.Value.ForceStop)
                   )
                {
                    continue;
                }

                if (!state.Load())
                {
                    staleSavestates.Add(state);
                    continue;
                }

                // Pause TAS if latest breakpoint
                if (driver.Controller.FastForwards.LastOrDefault().Value?.Frame <= state.Frame)
                {
                    driver.CurrState = driver.NextState = SourceTasManagerState.Paused;
                }
                else
                {
                    driver.CurrState = driver.NextState = SourceTasManagerState.Running;
                }

                goto CleanupStates;
            }

            CleanupStates:
            foreach (var state in staleSavestates)
            {
                state.Clear();

                if (state.SavedByBreakpoint)
                {
                    BreakpointSavestates.Remove(state);
                }
                else
                {
                    ManualSavestate?.Clear();
                    ManualSavestate = null;
                }
            }
        }

        private bool Save(bool byBreakpoint, out SourceTasSavestate savestate)
        {
            int checksum = driver.Controller.CalcChecksum(driver.Controller.CurrentFrameInTas);

            // Check for already existing savestate
            if (AllSavestates.Any(state =>
                    state.Frame == driver.Controller.CurrentFrameInTas && state.Checksum == checksum))
            {
                savestate = default;
                return false;
            }

            savestate = new SourceTasSavestate(this, driver.Controller.Clone(),
                driver.Controller.CalcChecksum(driver.Controller.CurrentFrameInTas), byBreakpoint);
            if (!driver.SourceSpeedrunTool.SaveState(savestate.Slot))
            {
                return false;
            }

            UpdateStudio();
            return true;
        }

        internal bool Load(SourceTasSavestate savestate)
        {
            // Don't load save-states while recording
            if (driver.Recorder.IsRecording)
            {
                return false;
            }

            if (savestate.BreakpointDeleted ||
                savestate.Checksum != driver.Controller.CalcChecksum(savestate.Controller.CurrentFrameInTas))
            {
                return false; // Invalid
            }

            if (driver.Controller.CurrentFrameInTas == savestate.Controller.CurrentFrameInTas)
            {
                // Don't repeat loading the state, just play
                driver.NextState = SourceTasManagerState.Running;
                return true;
            }

            if (!driver.SourceSpeedrunTool.LoadState(savestate.Slot))
            {
                return false;
            }

            driver.Controller.CopyProgressFrom(savestate.Controller);

            UpdateStudio();
            return true;
        }

        internal void Clear(SourceTasSavestate savestate)
        {
            driver.SourceSpeedrunTool.ClearState(savestate.Slot);

            UpdateStudio();
        }

        private void UpdateStudio()
        {
            driver.GameInfo.Update();
            driver.SendSourceStudioState();
        }
    }
}
