using StudioCommunication;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Celeste.Mods.Tas.Input;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Commands
{
    /// Repeats everything between "Repeat, N" and "EndRepeat" N times
    internal sealed class SourceTasRepeatProcess
    {
        internal static System.Type SourceMetaProviderType => typeof(Meta);

        private readonly TasPlaybackDriver driver;
        internal SourceTasRepeatProcess(TasPlaybackDriver driver) => this.driver = driver;

        private class Meta : ISourceTasCommandMeta
        {
            public string Insert => $"""
                                     Repeat{CommandInfo.Separator}[0;2]
                                         [1]
                                     EndRepeat
                                     """;

            public bool HasArguments => true;
        }

        private readonly record struct Arguments(int StartFrame, int Count, string StartFilePath, int StartFileLine);

        private readonly Stack<Arguments> repeatStack = new();

        [SourceTasParseFileEndAttribute]
        private static void SourceParseFileEnd() => CelesteTasModule.Instance.Driver.SourceRepeatCommand.ParseFileEnd();

        internal void ParseFileEnd()
        {
            if (repeatStack.IsEmpty())
            {
                return;
            }

            foreach (var arguments in repeatStack)
            {
                string errorText = $"{Path.GetFileName(arguments.StartFilePath)} line {arguments.StartFileLine}\n";
                driver.AbortSourceTas($"{errorText}Repeat command does not have a paired EndRepeat command");
            }
        }

        [SourceTasClearInputsAttribute]
        private static void SourceClear() => CelesteTasModule.Instance.Driver.SourceRepeatCommand.Clear();

        internal void Clear()
        {
            repeatStack.Clear();
        }

        // "Repeat, Count"
        [SourceTasCommand("Repeat", ExecuteTiming = SourceTasExecuteTiming.Parse, MetaDataProvider = typeof(Meta))]
        internal void Repeat(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            string[] args = commandLine.Arguments;

            string errorText = $"{Path.GetFileName(filePath)} line {fileLine}\n";
            if (args.IsEmpty())
            {
                driver.AbortSourceTas($"{errorText}Repeat command has no count specified");
                return;
            }

            if (!int.TryParse(args[0], out int count))
            {
                driver.AbortSourceTas($"{errorText}Repeat command's count is not an integer");
                return;
            }

            // Count must be a valid value
            // Needs to be kept in sync with the Studio's clamp
            if (count < 1)
            {
                driver.AbortSourceTas($"{errorText}Repeat command's count must not be less than 1");
                return;
            }

            if (count > 10_000_000)
            {
                driver.AbortSourceTas($"{errorText}Repeat command's count must not be greater than 10 million");
                return;
            }

            repeatStack.Push(new Arguments(driver.Controller.CurrentParsingFrame, count, filePath, fileLine));
        }

        // "EndRepeat"
        [SourceTasCommand("EndRepeat", ExecuteTiming = SourceTasExecuteTiming.Parse)]
        internal void EndRepeat(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            string errorText = $"{Path.GetFileName(filePath)} line {fileLine}\n";
            if (!repeatStack.TryPop(out var arguments))
            {
                driver.AbortSourceTas($"{errorText}EndRepeat command does not have a paired Repeat command");
                return;
            }

            // Exclude Repeat/EndRepeat from the lines
            int startLine = arguments.StartFileLine + 1;
            int endLine = fileLine - 1;
            int count = arguments.Count;
            int startFrame = arguments.StartFrame;

            if (endLine < startLine)
            {
                driver.ShowSourcePopupToastAndLog($"{errorText}Ending line is less than starting line");
            }

            if (!File.Exists(filePath))
            {
                driver.ShowSourcePopupToastAndLog($"{errorText}Target file '{filePath}' does not exist");
            }

            if (count <= 1 || endLine < startLine || !File.Exists(filePath))
            {
                return;
            }

            var inputController = driver.Controller;
            bool mainFile = filePath == inputController.FilePath;

            // Update repeat info for inputs of first loop
            // Only included main TAS file, since Read files shouldn't show repeat info
            if (mainFile)
            {
                for (int frame = startFrame; frame < inputController.CurrentParsingFrame; frame++)
                {
                    inputController.Inputs[frame].RepeatIndex = 1;
                    inputController.Inputs[frame].RepeatCount = count;
                }
            }

            string[] lines = File.ReadLines(filePath).Take(endLine).ToArray();
            for (int i = 2; i <= count; i++)
            {
                inputController.ReadLines(
                    lines,
                    filePath,
                    startLine,
                    // Only display repeat info for main file
                    mainFile ? startLine - 1 : studioLine,
                    mainFile ? i : 0,
                    mainFile ? count : 0
                );
            }
        }
    }
}
