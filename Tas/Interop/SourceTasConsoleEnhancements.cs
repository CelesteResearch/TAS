#nullable enable
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Celeste.Core.Reflection.Types;
using Celeste.Mods.Tas.Module;
using Celeste.Everest.Modules.Runtime;
using Celeste.Everest.Runtime.Startup;
using Celeste.Everest.Runtime.Threading;
using Celeste.Everest.Diagnostics.Commands;
using Celeste.Everest.Reflection;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Execution.Startup;
using Celeste.Scene.Entities;
using StudioCommunication;
using global::Celeste.Game.Execution;

namespace Celeste.Mods.Tas.Interop
{
    internal sealed class SourceTasConsoleEnhancements
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private SourceEverestCommands Commands => (SourceEverestCommands)driver.SourceCommands;
        private EverestDebugRemoteControlProcess DebugRC => ((IEverestDebugRemoteControlEnvironment)process).DebugRC;

        private Celeste.Core.Reflection.Members.SourceMemberCatalog Members =>
            ((ISourceEverestReflectionEnvironment)process).Reflection.SourceMembers;

        internal SourceTasConsoleEnhancements(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        private string clickedEntityInfo = string.Empty;
        private readonly Dictionary<string, string> AllModNames = new();

        internal const int InitializePriority = 0;

        internal void InitializeHelperMethods()
        {
            AllModNames.Clear();
            AllModNames.Add(TypeReflectionIdentity.Resolve(Members, typeof(GameRuntime)).AssemblyIdentity, "Celeste");
            foreach (SourceEverestModule module in
                     ((ISourceEverestModulesProcessEnvironment)process).ModulesProcess.Modules)
            {
                if (module is SourceNullModule)
                {
                    continue;
                }

                string key = TypeReflectionIdentity.Resolve(Members, module.GetType()).AssemblyIdentity;
                if (!AllModNames.ContainsKey(key))
                {
                    AllModNames.Add(key, module.Metadata.Name);
                }
            }
        }

        internal void Load()
        {
            process.SourceCommandsLevelCursorFormatter += ShowExtraInfo;
            DebugRC.ConsoleLineInterception += CheckTasCommand;
        }

        private bool CheckTasCommand(string line, HttpListenerContext c)
        {
            if (!CommandLine.TryParse(line, out var commandLine)) return false;
            Action execute;
            if (line.StartsWith("Set", StringComparison.OrdinalIgnoreCase))
                execute = () => driver.SetProcess.Set(commandLine.Arguments);
            else if (line.StartsWith(CommandInfo.GetCommand, StringComparison.OrdinalIgnoreCase))
                execute = () => driver.TargetQuery.Get(commandLine);
            else if (line.StartsWith("Invoke", StringComparison.OrdinalIgnoreCase))
                execute = () => driver.SourceInvokeCommand.Invoke(commandLine.Arguments);
            else if (line.StartsWith("EvalLua", StringComparison.OrdinalIgnoreCase))
                execute = () =>
                {
                    object?[]? result =
                        driver.LuaCommands.ExecuteLua(string.Join(commandLine.ArgumentSeparator,
                            commandLine.Arguments));
                    driver.LuaCommands.LogResult(result);
                };
            else return false;
            var output = new StringBuilder();
            ((ISourceEverestMainThreadEnvironment)process).MainThreadProcess.Schedule(() =>
            {
                try
                {
                    Commands.DebugRCLog = output;
                    execute();
                }
                finally
                {
                    Commands.DebugRCLog = null;
                }
            }).AsTask().Wait();
            DebugRC.Write(c, output.ToString());
            return true;
        }

        internal void Unload()
        {
            process.SourceCommandsLevelCursorFormatter -= ShowExtraInfo;
            DebugRC.ConsoleLineInterception -= CheckTasCommand;
        }

        internal void EnableRun()
        {
            // Auto-close at start. Can be opened manually again
            Commands.Open = false;
        }

        internal void UpdateMeta()
        {
            if (!driver.Running)
            {
                return; // Only allow inside a TAS, since outside it's already handled
            }

            if (Commands.Open)
            {
                Commands.UpdateOpen();
                if (!Commands.Open)
                {
                    return; // Avoid reopening console
                }
            }
            else if (Commands.Enabled)
            {
                Commands.UpdateClosed();
            }

            if (!Commands.Enabled || Commands.Open)
            {
                return;
            }

            if (driver.SourceHotkeys.OpenConsole.Pressed)
            {
                // Copied from Commands.UpdateClosed
                Commands.OpenFromSourceTasHotkey();
            }
        }

        internal string ShowExtraInfo(string text, object xObj, object yObj)
        {
            OriginalLevelSceneRoot level = (process.Frame.Scenes.CurrentScene as OriginalLevelSceneRoot)!;
            int x = (int)xObj;
            int y = (int)yObj;
            int worldX = (int)Math.Round(x + level.LevelOffset.X);
            int worldY = (int)Math.Round(y + level.LevelOffset.Y);

            if (driver.MouseInput.Left.Pressed)
            {
                object? clickedEntity = driver.GameInfo.WatchEntities.FindClickedEntity();
                if (clickedEntity != null)
                {
                    Type type = clickedEntity.GetType();
                    var sourceType = TypeReflectionIdentity.Resolve(Members, type);
                    clickedEntityInfo = "\n entity type: ";
                    if (sourceType.AssemblyIdentity ==
                        TypeReflectionIdentity.Resolve(Members, typeof(GameRuntime)).AssemblyIdentity)
                    {
                        clickedEntityInfo += sourceType.TypeIdentity;
                    }
                    else
                    {
                        // StartExport uses a comma as a separator, so we can't use comma,
                        // use @ to place it and replace it back with a comma when looking for the type
                        clickedEntityInfo += sourceType.TypeIdentity + "@" +
                                             new System.Reflection.AssemblyName(sourceType.AssemblyIdentity).Name;
                    }

                    if (((Celeste.Core.Components.ISourceEntitySourceData)clickedEntity).SourceData is
                        Celeste.Game.Content.Maps.Entities.OriginalLevelEntityData entityData)
                    {
                        clickedEntityInfo += $"\n entity name: {entityData.Name}";
                        clickedEntityInfo +=
                            $"\n entity id  : {((Celeste.Core.Components.ISourceEntitySourceData)clickedEntity).SourceId}";
                    }

                    clickedEntityInfo += $"\n mod name   : {GetModName(type)}";

                    driver.Logging.Log("Info of clicked entity: " + clickedEntityInfo);
                }
                else
                {
                    clickedEntityInfo = string.Empty;
                }
            }
            else if (driver.MouseInput.Right.Pressed)
            {
                clickedEntityInfo = string.Empty;
            }

            return (string.IsNullOrEmpty(clickedEntityInfo) ? string.Empty : clickedEntityInfo) +
                   $"\n world:       {worldX}, {worldY}" +
                   $"\n level:       {x}, {y}";
        }

        public string GetModName(Type type)
        {
            // tells you where that weird entity/trigger comes from
            var identity = TypeReflectionIdentity.Resolve(Members, type);
            if (AllModNames.TryGetValue(identity.AssemblyIdentity, out string? modName))
            {
                if (modName == "Celeste" && identity.TypeIdentity.StartsWith("Celeste.Mod."))
                {
                    modName = "Everest";
                }

                return modName!;
            }
            else
            {
                // A helper DLL and a late-loaded module retain their actual context owner.
                return process.AssemblyContexts.GetPhysicalAssemblyOwner(type.Assembly)?.Name ?? "Unknown";
            }
        }
    }
}
