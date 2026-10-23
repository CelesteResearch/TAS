#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StudioCommunication;
using Celeste.Everest;
using Celeste.Everest.Content.Registry;
using Celeste.Everest.Scripting.Lua;
using Celeste.Mods.Tas.Commands;
using Celeste.Framework.Diagnostics.Commands;

namespace Celeste.Mods.Tas.Lua
{
    public sealed class SourceTasEvalLuaCommandState
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;

        public SourceTasEvalLuaCommandState(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        public const string CommandName = "EvalLua";

        private class Meta : ISourceTasCommandMeta
        {
            public string Insert => $"{CommandName}{CommandInfo.Separator}[0;Code]";
            public bool HasArguments => true;
        }

        internal bool ConsoleCommandRunning;

        public void Log(object message)
        {
            if (ConsoleCommandRunning)
            {
                process.Commands.Log(message);
            }

            driver.Logging.Log($"{CommandName} Command Failed: {message}");
        }

        [SourceCommand(CommandName, "Evaluate Lua code (CelesteTAS)")]
        private void EvalLua()
        {
            if (!CommandLine.TryParse(process.Commands.ReadCommandHistory(0), out var commandLine))
            {
                driver.Logging.ConsoleLog($"{CommandName} Command Failed: Couldn't parse arguments of command",
                    EverestLogLevel.Error);
                return;
            }

            try
            {
                ConsoleCommandRunning = true;
                object?[]? result = ExecuteLua(string.Join(commandLine.ArgumentSeparator, commandLine.Arguments));
                LogResult(result);
            }
            finally
            {
                ConsoleCommandRunning = false;
            }
        }

        [SourceTasCommand(CommandName, LegalInFullGame = false, MetaDataProvider = typeof(Meta))]
        private void EvalLua(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            string[] args = commandLine.Arguments;
            if (args.Length == 0)
            {
                return;
            }

            ExecuteLua(string.Join(commandLine.ArgumentSeparator, commandLine.Arguments));
        }

        private string? envCode;

        public object?[]? ExecuteLua(string code)
        {
            // Prepend useful helper functions as environment
            if (envCode == null)
            {
                var asset = ((IEverestContentRegistryEnvironment)process).SourceContentRegistry.Get("bin/env");
                using var reader = new StreamReader(asset.Stream);
                envCode = reader.ReadToEnd() + "\n";
            }

            code = envCode + code;

            object?[]? objects;
            try
            {
                objects = ((IEverestLuaLoaderProcessEnvironment)process).LuaLoader.Run(code, null);
            }
            catch (Exception e)
            {
                driver.Logging.LogException(e);
                return [e];
            }

            return objects;
        }

        internal void LogResult(object?[]? objects)
        {
            if (objects == null || objects.Length == 0)
            {
                return;
            }

            var result = new List<string>();
            if (objects.Length == 1)
            {
                result.Add(objects[0]?.ToString() ?? "null");
            }
            else
            {
                result.AddRange(objects.Select((obj, idx) => $"{idx + 1}: {obj?.ToString() ?? "null"}"));
            }

            process.Commands.Log(string.Join("\n", result));
        }
    }
}
