using Celeste.Core.Text;
using Celeste.Core.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using StudioCommunication.Util;
using Celeste.Mods.Tas.Module;
using StudioCommunication;
using Celeste.Mods.Tas.Query;
using Celeste.Framework.Diagnostics.Commands;
using Celeste.Graphics.Display;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed class SourceTasSetProcess
    {
        internal static System.Type SourceSetMetaProviderType => typeof(SetMeta);

        public const string CommandName = "Set";

        internal class SetMeta : ISourceTasCommandMeta
        {
            private SourceTasTargetQueryProcess Query => CelesteTasModule.Instance.Driver.TargetQuery;
            public string Insert => $"{CommandName}{CommandInfo.Separator}[0;Query]{CommandInfo.Separator}[1;Value]";
            public bool HasArguments => true;

            public int GetHash(string[] args, string filePath, int fileLine)
            {
                var hash = new StableHashCode();
                hash.Add(Query.GetQueryArgs(args, 0)
                    .Aggregate(new StableHashCode(), (argHash, arg) => argHash.Append(arg))
                    .ToHashCode());
                hash.Add(Query.GetQueryArgs(args, 1)
                    .Aggregate(new StableHashCode(), (argHash, arg) => argHash.Append(arg))
                    .ToHashCode());
                hash.Add(args.Length);
                return hash.ToHashCode();
            }

            public IEnumerator<CommandAutoCompleteEntry> GetAutoCompleteEntries(string[] args, string filePath,
                int fileLine)
            {
                string[] targetQueryArgs = args.Length > 0 ? args[0].Split('.') : [];
                if (args.Length <= 1)
                {
                    using var enumerator =
                        Query.ResolveAutoCompleteEntries(targetQueryArgs, SourceTasTargetQueryProcess.Variant.Set);
                    while (enumerator.MoveNext()) yield return enumerator.Current with { HasNext = true };
                    yield break;
                }

                {
                    string[] paramQueryArgs = args.Length > 1 ? args[1].Split('.') : [];
                    var baseTypes = Query.ResolveBaseTypes(targetQueryArgs, out string[] memberArgs);
                    var targetTypes = baseTypes.Select(type =>
                            Query.RecurseMemberType(type, memberArgs, SourceTasTargetQueryProcess.Variant.Set))
                        .Where(type => type != null).ToArray();
                    using var enumerator = Query.ResolveAutoCompleteEntries(paramQueryArgs,
                        SourceTasTargetQueryProcess.Variant.Get, targetTypes!);
                    while (enumerator.MoveNext()) yield return enumerator.Current;
                }
            }
        }

        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private readonly SourceTasTargetQueryProcess query;
        private (string Name, int Line)? activeFile;

        internal static bool IsPresentationCommand(SourceTasCommand command) =>
            command.Is(CommandName) && TryGetPresentationValue(command.Args, out _);

        private static bool TryGetPresentationValue(string[] args, out bool value)
        {
            value = false;
            return args.Length == 2 &&
                   (args[0] == "CelesteTAS.SimplifiedGraphics" || args[0] == "CelesteTAS.SimplifiedBackdrop") &&
                   bool.TryParse(args[1], out value);
        }

        internal SourceTasSetProcess(GameApplication process, TasPlaybackDriver driver,
            SourceTasTargetQueryProcess actualQuery)
        {
            this.process = process;
            this.driver = driver;
            query = actualQuery;
        }

        internal void SetCmd()
        {
            if (!CommandLine.TryParse(((ISourceCommandsProcessEnvironment)process).Commands.ReadCommandHistory(0),
                    out var commandLine))
            {
                ReportError("Couldn't parse arguments of command");
                return;
            }

            Set(commandLine.Arguments);
        }

        internal void Execute(CommandLine commandLine, int studioLine, string filePath, int fileLine)
            => Execute(commandLine.Arguments, filePath, fileLine);

        internal void Execute(string[] args, string filePath, int fileLine)
        {
            try
            {
                activeFile = (filePath, fileLine);
                Set(args);
            }
            finally
            {
                activeFile = null;
            }
        }

        internal void Set(string[] args)
        {
            // Full-game TAS files use these display switches around skipped visual cutscenes.
            // Keep their typed setters separate from arbitrary query code execution.
            if (driver.EnforceLegal.EnabledWhenRunning && TryGetPresentationValue(args, out bool presentationValue))
            {
                if (args[0] == "CelesteTAS.SimplifiedGraphics")
                    driver.Settings.SimplifiedGraphics = presentationValue;
                else
                    driver.Settings.SimplifiedBackdrop = presentationValue;
                return;
            }

            switch (args.Length)
            {
                case < 1:
                    ReportError("Expected target-query and value");
                    return;
                case < 2:
                    ReportError($"Expected value (only get target-query '{args[0]}')");
                    return;
            }

            var result = query.SetMemberValues(args[0], args[1..]);
            if (result.Failure) ReportError(result.Error.ToString());
        }

        private void ReportError(string message)
        {
            if (activeFile == null)
            {
                try
                {
                    ((ISourceCommandsProcessEnvironment)process).Commands?.Log(
                        $"Set Command Failed: {message}", PackedColor.Red);
                }
                catch (Exception)
                {
                    // Original LogUtil.ConsoleLog ignores console reporting exceptions.
                }
            }
            else
            {
                string errorText = $"Set '{activeFile.Value.Name}' line {activeFile.Value.Line} failed:\n{message}";
                foreach (var line in errorText.AsSpan().EnumerateLines())
                    driver.LogSourceTas(global::Celeste.Everest.EverestLogLevel.Error, line.ToString());
                driver.ShowSourcePopupToastWithColor(errorText, PackedColor.Yellow);
            }
        }
    }
}
