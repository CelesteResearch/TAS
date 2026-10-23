using Celeste.Everest.Diagnostics.Logging;
using Celeste.Everest;
using StudioCommunication;
using StudioCommunication.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Celeste.Mods.Tas.Input;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed class SourceTasPlayProcess
    {
        internal static System.Type SourcePlayMetaProviderType => typeof(PlayMeta);

        private readonly TasPlaybackDriver driver;
        internal SourceTasPlayProcess(TasPlaybackDriver driver) => this.driver = driver;

        private class PlayMeta : ISourceTasCommandMeta
        {
            public string Insert => $"Play{CommandInfo.Separator}[0;Starting Label]";
            public bool HasArguments => true;

            public int GetHash(string[] args, string filePath, int fileLine)
            {
                // Only file contents and line matters
                return 31 * File.ReadAllText(filePath).GetStableHashCode() + 17 * fileLine;
            }

            public IEnumerator<CommandAutoCompleteEntry> GetAutoCompleteEntries(string[] args, string filePath,
                int fileLine)
            {
                if (args.Length != 1)
                {
                    yield break;
                }

                // Don't include labels before the current line
                foreach (string line in SourceTasLineEndings.NormalizeToLf(File.ReadAllText(filePath)).Split('\n')
                             .Skip(fileLine))
                {
                    if (!StudioCommunication.CommentLine.IsLabel(line))
                    {
                        continue;
                    }

                    string label = line[1..]; // Remove the #
                    yield return new CommandAutoCompleteEntry { Name = label, IsDone = true, HasNext = false };
                }
            }
        }

        // "Play, StartLabel",
        // "Play, StartLabel, FramesToWait"
        [SourceTasCommand("Play", ExecuteTiming = SourceTasExecuteTiming.Parse, MetaDataProvider = typeof(PlayMeta))]
        internal void Play(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            string[] args = commandLine.Arguments;
            string[] lines = File.ReadAllLines(filePath);
            if (!Parsing.TryGetLineTarget(args[0], lines, out int startLine, out _))
            {
                driver.AbortSourceTas($"\"Play, {string.Join(", ", args)}\" failed\n{args[0]} is invalid", true);
                return;
            }

            if (args.Length > 1 && int.TryParse(args[1], out _))
            {
                driver.Controller.AddFrames(args[1], filePath, fileLine, studioLine);
            }

            if (startLine <= studioLine + 1)
            {
                driver.LogSourceTas(EverestLogLevel.Warn,
                    "Play command does not allow playback from before the current line");
                return;
            }

            driver.Controller.ReadFile(filePath, startLine, int.MaxValue,
                filePath == driver.Controller.FilePath ? startLine - 1 : studioLine);
        }
    }
}
