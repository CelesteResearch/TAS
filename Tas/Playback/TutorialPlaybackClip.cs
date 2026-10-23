using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Celeste.Mods.Tas.Input;

namespace Celeste.Mods.Tas.Playback
{
    internal sealed class TutorialPlaybackClip
    {
        private static readonly Regex InputPattern = new(@"^\d+((,[LRDUGJKXCZV])*|(,F,\d+(\.\d+)?))$",
            RegexOptions.IgnoreCase);

        public readonly List<SourceTasInputFrame> Inputs = new();
        public readonly Dictionary<int, List<TutorialPlaybackCommand>> Commands = new();
        public int TotalFrames;

        public static TutorialPlaybackClip Parse(string path, Stream input)
        {
            var clip = new TutorialPlaybackClip();
            int frame = 0;
            int skippedFrames = 0;
            int lineNumber = 0;
            SourceTasInputFrame previous = null;
            using var reader = new StreamReader(input, Encoding.UTF8, true, 1024, true);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;
                line = line.Trim();
                if (InputPattern.IsMatch(line) && SourceTasInputFrame.TryParseGameplayInput(line, path,
                        lineNumber, previous, out var parsed))
                {
                    previous = parsed;
                    clip.Inputs.Add(parsed);
                    frame += parsed.Frames;
                    continue;
                }

                TutorialPlaybackCommand command;
                if (line.StartsWith("console load", StringComparison.Ordinal))
                {
                    command = new TutorialPlaybackCommand(null, true);
                    skippedFrames += 37;
                }
                else if (line.StartsWith("start animation", StringComparison.Ordinal))
                {
                    string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 3) continue;
                    command = new TutorialPlaybackCommand(parts[2]);
                }
                else if (line == "stop animation") command = new TutorialPlaybackCommand(null);
                else continue;

                if (!clip.Commands.TryGetValue(frame, out var commands))
                    clip.Commands.Add(frame, commands = new List<TutorialPlaybackCommand>());
                commands.Add(command);
            }

            clip.TotalFrames = frame - skippedFrames;
            return clip.Inputs.Count == 0 ? null : clip;
        }
    }
}
