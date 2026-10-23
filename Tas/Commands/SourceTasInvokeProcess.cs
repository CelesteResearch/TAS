using Celeste.Core.Text;
using Celeste.Core.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Query;
using Celeste.Framework.Diagnostics.Commands;
using Celeste.Graphics.Display;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed class SourceTasInvokeProcess
    {
        internal static System.Type SourceInvokeMetaProviderType => typeof(InvokeMeta);

        private readonly TasPlaybackDriver driver;
        private SourceTasTargetQueryProcess Query => driver.TargetQuery;
        internal SourceTasInvokeProcess(TasPlaybackDriver driver) => this.driver = driver;

        public const string CommandName = "Invoke";

        internal class InvokeMeta : ISourceTasCommandMeta
        {
            private SourceTasTargetQueryProcess Query => CelesteTasModule.Instance.Driver.TargetQuery;

            public string Insert =>
                $"{CommandName}{CommandInfo.Separator}[0;Query]{CommandInfo.Separator}[1;Parameters]";

            public bool HasArguments => true;

            public int GetHash(string[] args, string filePath, int fileLine)
            {
                var hash = new StableHashCode();
                hash.Add(Query.GetQueryArgs(args, 0)
                    .Aggregate(new StableHashCode(), (argHash, arg) => argHash.Append(arg.GetStableHashCode()))
                    .ToHashCode());
                for (int i = 1; i < args.Length; i++)
                {
                    hash.Add(Query.GetQueryArgs(args, i)
                        .Aggregate(new StableHashCode(), (argHash, arg) => argHash.Append(arg.GetStableHashCode()))
                        .ToHashCode());
                }

                hash.Add(args.Length);
                return hash.ToHashCode();
            }

            public IEnumerator<CommandAutoCompleteEntry> GetAutoCompleteEntries(string[] args, string filePath,
                int fileLine)
            {
                // Target
                string[] targetQueryArgs = args.Length > 0 ? args[0].Split('.') : [];
                if (args.Length <= 1)
                {
                    using var enumerator =
                        Query.ResolveAutoCompleteEntries(targetQueryArgs, SourceTasTargetQueryProcess.Variant.Invoke);
                    while (enumerator.MoveNext())
                    {
                        yield return enumerator.Current with { HasNext = true };
                    }

                    yield break;
                }

                // Parameters
                var baseTypes = Query.ResolveBaseTypes(targetQueryArgs, out string[] memberArgs);
                var targetTypes = baseTypes
                    .Select(type =>
                        Query.RecurseMemberType(type, memberArgs, SourceTasTargetQueryProcess.Variant.Invoke))
                    .Where(type => type != null)
                    .ToArray();

                for (int i = 1; i < Math.Min(args.Length, targetTypes.Length + 1); i++)
                {
                    string[] paramQueryArgs = args[i].Split('.');
                    using var enumerator = Query.ResolveAutoCompleteEntries(paramQueryArgs,
                        SourceTasTargetQueryProcess.Variant.Get, [targetTypes[i - 1]!]);
                    while (enumerator.MoveNext())
                    {
                        yield return enumerator.Current with { HasNext = i < targetTypes.Length };
                    }
                }
            }
        }

        private (string Name, int Line)? activeFile;

        private void ConsoleLog(string message)
        {
            try
            {
                ((ISourceCommandsProcessEnvironment)driver.Process).Commands?.Log(message, PackedColor.Red);
            }
            catch (Exception)
            {
            }
        }

        private void ReportError(string message)
        {
            if (activeFile == null)
            {
                ConsoleLog($"{CommandName} Command Failed: {message}");
            }
            else
            {
                string errorText = $"""
                                    {CommandName} '{activeFile.Value.Name}' line {activeFile.Value.Line} failed:
                                    {message}
                                    """;
                foreach (var line in errorText.AsSpan().EnumerateLines())
                    driver.LogSourceTas(global::Celeste.Everest.EverestLogLevel.Error, line.ToString());
                driver.ShowSourcePopupToastWithColor(errorText, PackedColor.Yellow);
            }
        }

        [SourceCommand(CommandName,
            "Invoke level/session/entity method. eg invoke Level.Pause; invoke Player.Jump (CelesteTAS)")]
        private void InvokeCmd()
        {
            if (!CommandLine.TryParse(
                    ((ISourceCommandsProcessEnvironment)driver.Process).Commands.ReadCommandHistory(0),
                    out var commandLine))
            {
                ConsoleLog($"{CommandName} Command Failed: Couldn't parse arguments of command");
                return;
            }

            Invoke(commandLine.Arguments);
        }

        // Invoke, Level.Method, Parameters...
        // Invoke, Session.Method, Parameters...
        // Invoke, Entity.Method, Parameters...
        // Invoke, Type.StaticMethod, Parameters...
        [SourceTasCommand(CommandName, LegalInFullGame = false, MetaDataProvider = typeof(InvokeMeta))]
        private void Invoke(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            try
            {
                activeFile = (filePath, fileLine);
                Invoke(commandLine.Arguments);
            }
            finally
            {
                activeFile = null;
            }
        }

        internal void Invoke(string[] args)
        {
            if (args.Length < 1)
            {
                ReportError("Target-query required");
                return;
            }

            var result = Query.InvokeMemberMethods(args[0], args[1..]);
            if (result.Failure)
            {
                ReportError(result.Error.ToString());
            }
        }
    }
}
