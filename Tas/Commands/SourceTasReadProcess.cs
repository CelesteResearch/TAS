using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Everest;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Mods.Tas.Input;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed class SourceTasReadProcess
    {
        internal static System.Type SourceReadMetaProviderType => typeof(ReadMeta);

        private readonly TasPlaybackDriver driver;
        internal SourceTasReadProcess(TasPlaybackDriver driver) => this.driver = driver;

        internal class ReadMeta : ISourceTasCommandMeta
        {
            private static TasPlaybackDriver driver => CelesteTasModule.Instance.Driver;

            public string Insert =>
                $"Read{CommandInfo.Separator}[0;File Name]{CommandInfo.Separator}[1;Starting Label]{CommandInfo.Separator}[2;(Ending Label)]";

            public bool HasArguments => true;

            public int GetHash(string[] args, string filePath, int fileLine)
            {
                int hash = args[..Math.Max(0, args.Length - 1)]
                    .Aggregate(17, (current, arg) => 31 * current + 17 * arg.GetStableHashCode());

                // Auto-complete entries are based on current file path
                hash = 31 * hash + 17 * driver.Controller.FilePath.GetStableHashCode();

                if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]))
                {
                    if (Path.GetDirectoryName(filePath) is not { } fileDir)
                    {
                        return hash;
                    }

                    string subDir = Path.GetDirectoryName(args[0]) ?? string.Empty;
                    string targetDir = Path.Combine(fileDir, subDir);

                    if (Directory.Exists(targetDir))
                    {
                        hash = Directory.GetDirectories(targetDir)
                            .Aggregate(hash, (current, arg) => 31 * current + 17 * arg.GetStableHashCode());
                        hash = Directory.GetFiles(targetDir)
                            .Aggregate(hash, (current, arg) => 31 * current + 17 * arg.GetStableHashCode());
                    }
                }

                return hash;
            }

            public IEnumerator<CommandAutoCompleteEntry> GetAutoCompleteEntries(string[] args, string filePath,
                int fileLine)
            {
                if (Path.GetDirectoryName(filePath) is not { } fileDir)
                {
                    yield break;
                }

                if (args.Length == 1)
                {
                    // Filename
                    string subDir = Path.GetDirectoryName(args[0]) ?? string.Empty;
                    string targetDir = Path.Combine(fileDir, subDir);

                    if (!Directory.Exists(targetDir))
                    {
                        yield break;
                    }

                    string prefix = Path.GetRelativePath(fileDir, targetDir).Replace('\\', '/') + "/";
                    if (prefix.StartsWith("./"))
                    {
                        prefix = prefix["./".Length..];
                    }

                    yield return new CommandAutoCompleteEntry { Name = "../", Prefix = prefix, IsDone = false };

                    foreach (string dir in Directory.GetDirectories(targetDir).OrderBy(Path.GetFileName))
                    {
                        string dirName = Path.GetFileName(dir);
                        if (string.IsNullOrWhiteSpace(dirName) || dirName.StartsWith("."))
                        {
                            continue; // Ignore hidden directories
                        }

                        yield return new CommandAutoCompleteEntry
                            { Name = $"{dirName}/", Prefix = prefix, IsDone = false };
                    }

                    foreach (string file in Directory.GetFiles(targetDir).OrderBy(Path.GetFileName))
                    {
                        string fileName = Path.GetFileName(file);
                        if (string.IsNullOrWhiteSpace(fileName) || fileName.StartsWith(".") ||
                            Path.GetExtension(fileName) != ".tas")
                        {
                            continue; // Ignore hidden / non-TAS files
                        }

                        yield return new CommandAutoCompleteEntry
                        {
                            Name = Path.GetFileNameWithoutExtension(fileName), Prefix = prefix, IsDone = true,
                            HasNext = true
                        };
                    }
                }
                else if (args.Length is 2 or 3)
                {
                    // Starting / ending labels
                    string fullPath = Path.Combine(fileDir, $"{args[0]}.tas");
                    if (!File.Exists(fullPath))
                    {
                        yield break;
                    }

                    // Don't include labels before the starting one for the ending label
                    bool afterStartingLabel = args.Length == 2;
                    foreach (string line in SourceTasLineEndings.NormalizeToLf(File.ReadAllText(fullPath)).Split('\n'))
                    {
                        if (!CommentLine.IsLabel(line))
                        {
                            continue;
                        }

                        string label = line[1..]; // Remove the #
                        if (!afterStartingLabel)
                        {
                            afterStartingLabel = label == args[1];
                            continue;
                        }

                        yield return new CommandAutoCompleteEntry
                            { Name = label, IsDone = true, HasNext = args.Length == 2 };
                    }
                }
            }
        }

        private readonly List<string> readCommandStack = new();

        [SourceTasClearInputs]
        private static void SourceClear() => CelesteTasModule.Instance.Driver.SourceReadCommand.Clear();

        internal void Clear()
        {
            readCommandStack.Clear();
        }

        // "Read, Path",
        // "Read, Path, StartLabel",
        // "Read, Path, StartLabel, EndLabel"
        [SourceTasCommand("Read", ExecuteTiming = SourceTasExecuteTiming.Parse, MetaDataProvider = typeof(ReadMeta))]
        internal void Read(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            string[] args = commandLine.Arguments;
            if (args.Length == 0)
            {
                return;
            }

            string commandName = $"Read, {string.Join(", ", args)}";

            string? fileDirectory = Path.GetDirectoryName(filePath);
            if (string.IsNullOrWhiteSpace(fileDirectory))
            {
                fileDirectory = Directory.GetCurrentDirectory();
            }

            if (string.IsNullOrWhiteSpace(fileDirectory))
            {
                driver.LogSourceTas(EverestLogLevel.Error, "Failed to get current directory for Read command");
                return;
            }

            if (Parsing.FindReadTargetFile(fileDirectory, args[0], out string errorMessage) is not { } path)
            {
                driver.AbortSourceTas($"""
                                       "{commandName}" failed
                                       {errorMessage}
                                       """, true);
                return;
            }

            if (Path.GetFullPath(path) == Path.GetFullPath(filePath))
            {
                driver.AbortSourceTas($"\"{commandName}\" failed\nDo not allow reading the file itself", true);
                return;
            }

            string[] lines = File.ReadAllLines(path);

            // Find starting and ending lines
            int startLine = 0;
            int endLine = int.MaxValue;
            if (args.Length > 1)
            {
                if (!Parsing.TryGetLineTarget(args[1], lines, out startLine, out _))
                {
                    driver.AbortSourceTas($"\"{commandName}\" failed\n{args[1]} is invalid", true);
                    return;
                }

                if (args.Length > 2)
                {
                    if (!Parsing.TryGetLineTarget(args[2], lines, out endLine, out _))
                    {
                        driver.AbortSourceTas($"\"{commandName}\" failed\n{args[2]} is invalid", true);
                        return;
                    }
                }
            }

            string readCommandDetail = $"{commandName}: line {fileLine} of the file \"{filePath}\"";
            if (readCommandStack.Contains(readCommandDetail))
            {
                driver.LogSourceTas(EverestLogLevel.Warn,
                    $"Multiple read commands lead to dead loops:\n{string.Join("\n", readCommandStack)}");
                driver.AbortSourceTas(
                    "Multiple read commands lead to dead loops\nPlease check log.txt for more details");
                return;
            }

            var controller = driver.Controller;

            // When reading a '#Start' label, assert that the correct level as actually loaded (relevant for fullgame runs)
            if (args.Length > 1 && args[1] == "Start")
            {
                foreach (string line in lines)
                {
                    if (CommandLine.TryParse(line, out var consoleCommandLine) &&
                        consoleCommandLine.IsCommand("console") &&
                        consoleCommandLine.Arguments.Length >= 2 &&
                        SourceTasConsoleProcess.LoadCommandRegex.Match(consoleCommandLine.Arguments[0]) is
                            { Success: true } &&
                        driver.ConsoleProcess.TryGetAreaId(consoleCommandLine.Arguments[1], out int areaId)
                       )
                    {
                        controller.ReadLine(
                            $"Assert,Equal,\"{driver.Process.AreaTable.Areas[areaId].SID}\",{{Session.Area.SID}}",
                            filePath,
                            fileLine, studioLine);
                    }
                }
            }

            // Restore settings changed by read file after we continue with the current one
            var origAnalogMode = driver.SourceAnalog.SourceTasAnalogMode;

            readCommandStack.Add(readCommandDetail);
            driver.Controller.EnableBreakpointParsing = false;
            driver.Controller.ReadFile(path, startLine, endLine, studioLine);
            driver.Controller.EnableBreakpointParsing = true;
            if (readCommandStack.Count > 0)
            {
                readCommandStack.RemoveAt(readCommandStack.Count - 1);
            }

            driver.Controller.ReadLine($"AnalogMode,{origAnalogMode}", filePath, fileLine, studioLine);
        }
    }
}
