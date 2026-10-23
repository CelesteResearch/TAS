using System;
using Celeste.Scene.Scenes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StudioCommunication;
using Celeste.Mods.Tas.Input;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Information;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Execution.Processes;
using Celeste.Game.Save.Runtime;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed class SourceTasStunPauseProcess
    {
        internal static System.Type SourceModeMetaProviderType => typeof(ModeMeta);

        internal static System.Type SourceMetaProviderType => typeof(Meta);

        private readonly TasPlaybackDriver driver;
        internal SourceTasStunPauseProcess(TasPlaybackDriver driver) => this.driver = driver;
        private bool ParsingCommand => driver.CommandRegistry.Parsing;

        private class Meta : ISourceTasCommandMeta
        {
            public string Insert => """
                                    StunPause [1]
                                        [0]
                                    EndStunPause
                                    """;

            public bool HasArguments => true;

            public IEnumerator<CommandAutoCompleteEntry> GetAutoCompleteEntries(string[] args, string filePath,
                int fileLine)
            {
                if (args.Length != 1)
                {
                    yield break;
                }

                foreach (var mode in (StunPauseMode[])Enum.GetValues(typeof(StunPauseMode)))
                {
                    yield return new CommandAutoCompleteEntry { Name = mode.ToString(), IsDone = true };
                }
            }
        }

        private class ModeMeta : ISourceTasCommandMeta
        {
            public string Insert => $"StunPauseMode{CommandInfo.Separator}[0;Input/Simulate]";
            public bool HasArguments => true;

            public IEnumerator<CommandAutoCompleteEntry> GetAutoCompleteEntries(string[] args, string filePath,
                int fileLine)
            {
                if (args.Length != 1)
                {
                    yield break;
                }

                foreach (var mode in (StunPauseMode[])Enum.GetValues(typeof(StunPauseMode)))
                {
                    yield return new CommandAutoCompleteEntry { Name = mode.ToString(), IsDone = true };
                }
            }
        }

        public enum StunPauseMode
        {
            Input,
            Simulate
        }

        private Dictionary<string, SourceTasAutoInputProcess.Arguments> AutoInputArgs =>
            driver.SourceAutoInputCommand.AutoInputArgs;

        // Actual original/installed Level producer both own the source unpauseTimer field.
        private const float unpauseTime = 0.15f;
        public bool SimulatePauses;
        public bool PauseOnCurrentFrame;
        public int SkipFrames;
        public int WaitingFrames;
        public StunPauseMode? LocalMode;
        public StunPauseMode? GlobalModeParsing;
        public StunPauseMode? GlobalModeRuntime;

        private StunPauseMode Mode
        {
            get
            {
                if (driver.EnforceLegal.EnabledWhenParsing)
                {
                    return StunPauseMode.Input;
                }

                StunPauseMode? globalMode = ParsingCommand ? GlobalModeParsing : GlobalModeRuntime;
                return LocalMode ?? globalMode ?? StunPauseMode.Input;
            }
        }

        [SourceTasInitialize]
        private static void SourceInitialize() => CelesteTasModule.Instance.Driver.SourceStunPauseCommand.Initialize();

        [SourceTasUnload]
        private static void SourceUnload() => CelesteTasModule.Instance.Driver.SourceStunPauseCommand.Unload();

        internal void Initialize() => driver.InstallSourceStunPauseBeforeUpdate(this);
        internal void Unload() => driver.UninstallSourceStunPauseBeforeUpdate(this);

        internal void DoublePauses(Action<SourceSceneState> orig, SourceSceneState self)
        {
            orig(self);

            if (SimulatePauses && self is OriginalLevelSceneRoot level)
            {
                if (SkipFrames < 0 || WaitingFrames >= 0)
                {
                    PauseOnCurrentFrame = !PauseOnCurrentFrame;
                    if (PauseOnCurrentFrame && CanPause(level))
                    {
                        UpdateTime(level, orig);
                    }
                }
            }
        }

        private bool CanPause(OriginalLevelSceneRoot level)
        {
            return level.CanPause && level.UnpauseTimer <= 0f;
        }

        private void UpdateTime(OriginalLevelSceneRoot level, Action<SourceSceneState> orig)
        {
            int gameTimeFrames = (int)Math.Ceiling(unpauseTime / driver.Process.RawDeltaTime) + 2;
            int timeActiveFrames = gameTimeFrames - 1;

            for (int i = 0; i < timeActiveFrames; i++)
            {
                orig(level);
            }

            if (level.InCredits || driver.Process.AreaKeys.GetSessionArea(level.Session).ID == 8 || level.TimerStopped)
            {
                return;
            }

            long ticks = driver.Process.RawDeltaTime.SecondsToTicks() *
                         ((int)Math.Ceiling(unpauseTime / driver.Process.RawDeltaTime) + 2);
            ((IOriginalSaveTimeEnvironment)driver.Process).SaveTime.AddTime(
                ((IOriginalGameplayProcess)driver.Process).SaveContext.CurrentSave,
                driver.Process.AreaKeys.GetSessionArea(level.Session), ticks);

            if (!level.Completed && level.TimerStarted)
            {
                level.Session.Time += ticks;
            }
        }

        [SourceTasCommand("StunPause", ExecuteTiming = SourceTasExecuteTiming.Parse | SourceTasExecuteTiming.Runtime,
            MetaDataProvider = typeof(Meta))]
        private void StunPause(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            string[] args = commandLine.Arguments;
            LocalMode = null;

            if (!args.IsEmpty())
            {
                if (Enum.TryParse(args[0], true, out StunPauseMode value))
                {
                    LocalMode = value;
                }
                else if (ParsingCommand)
                {
                    driver.AbortSourceTas("StunPause command failed.\nMode must be Input or Simulate");
                    return;
                }
            }

            if (ParsingCommand && Mode == StunPauseMode.Input)
            {
                StunPauseAutoInputMode(studioLine, filePath, fileLine);
            }
            else if (!ParsingCommand && Mode == StunPauseMode.Simulate)
            {
                if (!SimulatePauses)
                {
                    SimulatePauses = true;
                    PauseOnCurrentFrame = false;
                }
            }
        }

        private void StunPauseAutoInputMode(int studioLine, string filePath, int fileLine)
        {
            string errorText = $"{Path.GetFileName(filePath)} line {fileLine}\n";
            if (AutoInputArgs.ContainsKey(filePath))
            {
                driver.AbortSourceTas($"{errorText}Nesting StunPause command is not allowed at AutoInput mode");
            }
            else
            {
                SourceTasAutoInputProcess.Arguments arguments = new(fileLine, 2);
                AutoInputArgs[filePath] = arguments;

                List<string> inputs = File.ReadLines(filePath).Take(fileLine - 1).ToList();
                inputs.Add("1,S,N");
                inputs.Add("");
                arguments.Inputs = inputs;
                UpdatePauseInputs(arguments);

                arguments.LockStudioLine = true;
                arguments.StunPause = true;
                driver.SourceAutoInputCommand.ParseInsertedLines(arguments, filePath, studioLine, 0, 0);
            }
        }

        public void UpdatePauseInputs(SourceTasAutoInputProcess.Arguments arguments)
        {
            List<string> inputs = arguments.Inputs!;
            inputs.RemoveAt(inputs.Count - 1);

            if (driver.Controller.Inputs.LastOrDefault() is { } input)
            {
                if (((input.Actions & Actions.Jump) != 0) && ((input.Actions & Actions.Jump2) != 0))
                {
                    inputs.Add("10,J,K");
                }
                else if (((input.Actions & Actions.Jump) != 0))
                {
                    inputs.Add("10,J");
                }
                else if (((input.Actions & Actions.Jump2) != 0))
                {
                    inputs.Add("10,K,O");
                }
                else
                {
                    inputs.Add("10,O");
                }
            }
            else
            {
                inputs.Add("10,O");
            }
        }

        [SourceTasCommand("EndStunPause",
            ExecuteTiming = SourceTasExecuteTiming.Parse | SourceTasExecuteTiming.Runtime)]
        private void EndStunPause(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            if (ParsingCommand && Mode == StunPauseMode.Input)
            {
                driver.SourceAutoInputCommand.EndAutoInputImpl(filePath, fileLine, "EndStunPause", "StunPause");
            }
            else if (!ParsingCommand && Mode == StunPauseMode.Simulate)
            {
                Reset();
            }
        }

        [SourceTasCommand("StunPauseMode",
            ExecuteTiming = SourceTasExecuteTiming.Parse | SourceTasExecuteTiming.Runtime,
            MetaDataProvider = typeof(ModeMeta))]
        private void StunPauseCommandMode(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            string[] args = commandLine.Arguments;
            if (!args.IsEmpty() && Enum.TryParse(args[0], true, out StunPauseMode value))
            {
                if (ParsingCommand)
                {
                    GlobalModeParsing = value;
                }
                else
                {
                    GlobalModeRuntime = value;
                }
            }
            else if (ParsingCommand)
            {
                driver.AbortSourceTas("StunPauseMode command failed.\nMode must be Input or Simulate");
            }
        }

        [SourceTasDisableRun]
        private static void SourceReset() => CelesteTasModule.Instance.Driver.SourceStunPauseCommand.Reset();

        private void Reset()
        {
            SimulatePauses = false;
            PauseOnCurrentFrame = false;
            SkipFrames = 0;
            WaitingFrames = 0;
            LocalMode = null;
        }

        [SourceTasDisableRun]
        private static void SourceClearGlobalModRuntime() =>
            CelesteTasModule.Instance.Driver.SourceStunPauseCommand.ClearGlobalModRuntime();

        private void ClearGlobalModRuntime()
        {
            GlobalModeRuntime = null;
        }

        [SourceTasParseFileEndAttribute]
        [SourceTasClearInputsAttribute]
        private static void SourceClearGlobalModeParsing() =>
            CelesteTasModule.Instance.Driver.SourceStunPauseCommand.ClearGlobalModeParsing();

        private void ClearGlobalModeParsing()
        {
            GlobalModeParsing = null;
        }

        public void UpdateSimulateSkipInput()
        {
            if (WaitingFrames >= 0)
            {
                WaitingFrames--;
            }

            if (WaitingFrames < 0 && SkipFrames >= 0)
            {
                SkipFrames--;
            }
        }

        public void SkipInput(string[] args, string filePath, int fileLine)
        {
            if (!SimulatePauses)
            {
                return;
            }

            string errorText = $"{Path.GetFileName(filePath)} line {fileLine}\nSkipInput command's ";
            if (args.IsEmpty())
            {
                SkipFrames = driver.Controller.Current?.Frames ?? 0;
                WaitingFrames = 0;
            }
            else if (!int.TryParse(args[0], out int frames))
            {
                driver.AbortSourceTas($"{errorText}first parameter is not an integer");
            }
            else
            {
                if (frames <= 0)
                {
                    driver.AbortSourceTas($"{errorText}first parameter must be greater than 0");
                    return;
                }

                SkipFrames = frames;

                if (args.Length >= 2)
                {
                    if (int.TryParse(args[1], out int waitFrames))
                    {
                        if (waitFrames < 0)
                        {
                            driver.AbortSourceTas($"{errorText}second parameter must be greater than or equal 0");
                            return;
                        }

                        WaitingFrames = waitFrames;
                    }
                    else
                    {
                        driver.AbortSourceTas($"{errorText}second parameter is not an integer");
                    }
                }
                else
                {
                    WaitingFrames = 0;
                }
            }
        }
    }
}
