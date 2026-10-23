using System.Collections.Generic;
using System.Text;
using StudioCommunication;
using Celeste.Simulation.Runtime.Session;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasAreaCompleteInfo
    {
        private const string TasWasRun = "CelesteTAS_TAS_Was_Run";
        private const string AlwaysShowInfo = nameof(AlwaysShowInfo);
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private readonly Dictionary<string, StringBuilder> completeInfos = new();
        private string text;

        internal SourceTasAreaCompleteInfo(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        internal bool AllowSessionFlagChange(string flag, bool setTo) => setTo || flag != TasWasRun;

        internal void EnableRun() => completeInfos.Clear();

        internal void CompleteInfoCommand(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            string[] args = commandLine.Arguments;
            if (args.Length == 1)
                return;

            string key;
            if (args.Length == 0)
            {
                key = AlwaysShowInfo;
            }
            else
            {
                string side = args[0].ToUpperInvariant();
                string sid = args[1];
                int mode = side switch { "B" => 1, "C" => 2, _ => 0 };
                if (int.TryParse(sid, out int id))
                {
                    if (id < 0 || id >= process.AreaTable.Areas.Count)
                        id = -1;
                }
                else
                {
                    id = process.AreaKeys.GetAreaBySID(sid)?.ID ?? -1;
                }

                if (id == -1)
                    return;
                key = process.AreaKeys.Format(process.AreaKeys.Create(id, mode));
            }

            if (!completeInfos.TryGetValue(key, out var info))
                completeInfos[key] = info = new StringBuilder();
            info.Clear();
            if (driver.Controller.CurrentComments is { Count: > 0 } comments)
            {
                bool firstComment = true;
                foreach (var comment in comments)
                {
                    if (!firstComment)
                        info.AppendLine();
                    info.AppendLine(comment.Text);
                }
            }
        }

        internal string CreateCommand()
        {
            if (SourceTasSceneSessionQuery.GetSession(process.Frame.Scenes.CurrentScene) is not { } session)
                return null;
            var area = process.AreaKeys.GetSessionArea(session);
            string mode = area.Mode switch { 0 => "A", 1 => "B", 2 => "C", _ => "A" };
            string id = area.ID <= 10 ? area.ID.ToString() : process.AreaKeys.GetSID(area);
            string separator = id.Contains(" ") ? ", " : " ";
            List<string> values = new() { "CompleteInfo", mode, id };
            return string.Join(separator, values);
        }

        internal void InitText(ChapterCompletionSession session)
        {
            session ??= SourceTasSceneSessionQuery.GetSession(process.Frame.Scenes.CurrentScene);
            if (session == null)
                return;
            if (!session.GetFlag(TasWasRun))
            {
                text = null;
                return;
            }

            text = $"CelesteTAS v{driver.SourceModule.Metadata.VersionString}";
            if (completeInfos.TryGetValue(AlwaysShowInfo, out var builder) && builder.Length > 0)
                text = $"{text}\n{builder}";
            string key = process.AreaKeys.Format(process.AreaKeys.GetSessionArea(session));
            if (completeInfos.TryGetValue(key, out builder) && builder.Length > 0)
                text = $"{text}\n{builder}";
        }
    }
}
