using System.Collections.Generic;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Everest.Diagnostics.Commands;
using Celeste.Game.Execution.Areas;
using Celeste.Game.Content.Maps;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Input;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed partial class SourceTasConsoleProcess
    {
        internal sealed class Meta : ISourceTasCommandMeta
        {
            private static TasPlaybackDriver Driver => CelesteTasModule.Instance.Driver;
            public string Insert => "console ";
            public bool HasArguments => true;

            public IEnumerator<CommandAutoCompleteEntry> GetAutoCompleteEntries(string[] args, string filePath,
                int fileLine)
            {
                int? currAreaMode = null;
                int currAreaID = -1;
                if (Driver.GameInfo.GetSourceSceneSession() is { } session)
                {
                    currAreaMode = Driver.Process.AreaKeys.GetSessionArea(session).Mode;
                    currAreaID = Driver.Process.AreaKeys.GetSessionArea(session).ID;
                }

                if (args.Length == 1)
                {
                    // load-commands first as they are the most common
                    yield return new CommandAutoCompleteEntry
                    {
                        Name = "load", Extra = "A-Side", Suggestion = currAreaMode == 0, HasNext = true,
                        StorageKey = "CommandRoot"
                    };
                    yield return new CommandAutoCompleteEntry
                    {
                        Name = "hard", Extra = "B-Side", Suggestion = currAreaMode == 1, HasNext = true,
                        StorageKey = "CommandRoot"
                    };
                    yield return new CommandAutoCompleteEntry
                    {
                        Name = "rmx2", Extra = "C-Side", Suggestion = currAreaMode == 2, HasNext = true,
                        StorageKey = "CommandRoot"
                    };

                    // Remaining commands
                    foreach (var command in ((SourceEverestCommands)Driver.SourceCommands).GetCommands())
                    {
                        if (command.Name is "load" or "hard" or "rmx2")
                        {
                            continue;
                        }

                        yield return new CommandAutoCompleteEntry
                            { Name = command.Name.ToLowerInvariant(), HasNext = false, StorageKey = "CommandRoot" };
                    }
                }
                else if (args.Length >= 2 && LoadCommandRegex.Match(args[0]) is { Success: true } match)
                {
                    // Only support argument auto-complete for load commands
                    string commandName = match.Groups[1].Value.ToLowerInvariant();
                    var areaMode = commandName switch
                    {
                        "hard" => 1,
                        "rmx2" => 2,
                        _ => 0
                    };

                    if (args.Length == 2)
                    {
                        // ID / SID
                        lock (((ISourceAreaReloadLockEnvironment)Driver.Process).AreaReloadLock)
                        {
                            foreach (var area in Driver.Process.AreaTable.Areas)
                            {
                                if (!area.HasMode(areaMode))
                                {
                                    continue;
                                }

                                yield return new CommandAutoCompleteEntry
                                {
                                    // Only use ID for base-game
                                    Name = area.LevelSet == "Celeste"
                                        ? area.ID.ToString()
                                        : area.SID,
                                    Extra = Driver.Process.Text.Dialog.Clean(area.Name,
                                        Driver.Process.Text.Dialog.Languages["english"]),
                                    Suggestion = currAreaID == area.ID,
                                    HasNext = false,
                                    StorageKey = $"Command_{commandName}",
                                };
                            }
                        }
                    }
                    else if (args.Length == 3)
                    {
                        // Starting screen
                        if (!Driver.ConsoleProcess.TryGetAreaId(args[1], out int areaId) ||
                            Driver.Process.AreaKeys.GetAreaById(areaId).Mode.GetValueOrDefault((int)areaMode) is not
                                { } mode)
                        {
                            yield break;
                        }

                        foreach ((string levelName, _) in ((OriginalChapterMapData)mode.MapData).LevelsByName)
                        {
                            yield return new CommandAutoCompleteEntry
                                { Name = levelName, Extra = "Room", HasNext = false };
                        }
                    }
                }
            }
        }
    }
}
