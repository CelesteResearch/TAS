using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StudioCommunication;
using Celeste.Core.Reflection.Methods;
using Celeste.Everest.Reflection;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Everest;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Reflection;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed class SourceTasCommandRegistry
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private readonly List<SourceTasCommandAttribute> commands = [];
        public bool Parsing { get; internal set; }

        internal SourceTasCommandRegistry(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        [SourceTasInitialize]
        private static void InitializeSourceCommandRegistry()
            => CelesteTasModule.Instance.Driver.CommandRegistry.CollectCommands();

        private void CollectCommands()
        {
            commands.Clear();
            var reflection = ((ISourceEverestReflectionEnvironment)process).Reflection;
            var methods = reflection.SourceMethods;
            foreach (var method in methods.EnumerateDeclaredAttributedMethods(
                         reflection.GetOriginalAggregateAssemblyOrder(), "TAS.Input.TasCommandAttribute"))
            {
                var attr = SourceTasCommandAttributeReader.Read(
                    methods.RequireDefinition(method.SourceAssemblyIdentity, method.SourceMethodDefinitionToken),
                    provider => RequireSourceMetadataProvider(provider, method.SourceAssemblyIdentity));
                attr.m_Execute = method;
#if DEBUG
            attr.Validate();
#endif
                if (attr.MetaDataProvider != null)
                    attr.MetaData = (ISourceTasCommandMeta)Activator.CreateInstance(attr.MetaDataProvider);
                commands.Add(attr);
            }

            driver.SendSourceCommandList();
        }

        private Type RequireSourceMetadataProvider(string originalTypeName, string declaringSourceAssemblyIdentity)
        {
            // Original Type SerStrings may omit the assembly for a provider declared
            // in the same original assembly. That identity comes from this exact PE
            // method definition, rather than the rewritten CLR declaring assembly.
            int separator = originalTypeName.IndexOf(',');
            string originalName = separator < 0
                ? originalTypeName
                : originalTypeName.Substring(0, separator).Trim();
            string originalAssembly = separator < 0
                ? declaringSourceAssemblyIdentity
                : originalTypeName.Substring(separator + 1).Trim();
            if (string.IsNullOrEmpty(originalName) || string.IsNullOrEmpty(originalAssembly))
                throw new InvalidOperationException("Original TAS metadata provider has no qualified identity: " +
                                                    originalTypeName);
            return ((ISourceEverestReflectionEnvironment)process).Reflection.SourceMembers
                   .GetActualSourceType(originalAssembly, originalName)
                   ?? throw new InvalidOperationException(
                       "Original TAS command metadata provider has no actual source binding: " + originalTypeName);
        }

        internal ISourceTasCommandMeta GetMeta(string name)
            => commands.FirstOrDefault(command => command.IsName(name))?.MetaData;

        internal CommandInfo[] GetCommandList()
            => commands.Select(command => new CommandInfo(command.Name,
                command.MetaData?.Insert ?? command.Name, command.MetaData?.HasArguments ?? false)).ToArray();

        internal bool TryParse(string filePath, int fileLine, string lineText, int frame, int studioLine,
            out SourceTasCommand command)
        {
            command = default;
            if (string.IsNullOrWhiteSpace(lineText) || !char.IsLetter(lineText[0])
                                                    || !CommandLine.TryParse(lineText, out var commandLine))
                return false;
            string error =
                $"Failed to parse command \"{lineText.Trim()}\" at line {fileLine} of the file \"{filePath}\"";
            try
            {
                var info = commands.FirstOrDefault(info => info.IsName(commandLine.Command));
                if (info == null)
                {
                    ((IEverestLoggerProcessEnvironment)process).Logger.Log(EverestLogLevel.Info, "CelesteTAS", error);
                    return false;
                }

                command = new SourceTasCommand(commandLine, info, filePath, fileLine, studioLine, frame);
                return true;
            }
            catch (Exception exception)
            {
                ((IEverestLoggerProcessEnvironment)process).Logger.LogDetailed(exception, error);
                return false;
            }
        }
    }
}
