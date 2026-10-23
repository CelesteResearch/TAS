using Celeste.Core.Display;
using Celeste.Mods.Tas.Query.Handlers;
using Celeste.Core.Reflection.Members;
using Celeste.Everest.Modules.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Celeste.Mods.Tas.Module;
using StudioCommunication.Util;
using StudioCommunication;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Numerics;
using Celeste.Core.Scheduling;
using Celeste.Everest.Diagnostics.Logging;
using TargetQuery = Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess;

namespace Celeste.Mods.Tas.Query
{
    public sealed partial class SourceTasTargetQueryProcess
    {
        /// Prevents invocations of methods / execution of Lua code in the Custom Info
        public bool PreventCodeExecution => Driver.EnforceLegal.EnabledWhenRunning;

        internal readonly Dictionary<string, HashSet<Type>> AllTypes = new();
        internal readonly Dictionary<string, (HashSet<Type> Types, string[] MemberArgs)> BaseTypeCache = new();

        internal readonly Handler[] Handlers;
        private readonly GameApplication process;
        private readonly ISourceEngineContext engine;
        internal ISourceEngineContext Engine => engine;

        internal Celeste.Game.Save.Data.OriginalSaveDataState CurrentSave =>
            ((Celeste.Game.Execution.Processes.IOriginalGameplayProcess)engine).SaveContext.CurrentSave;

        internal readonly QueryReflection Reflection;
        internal readonly SourceMemberCatalog Members;
        private readonly Celeste.Core.Reflection.Types.SourceTypeReferenceResolver sourceTypeReferences;
        internal readonly Celeste.Mods.Tas.TasPlaybackDriver Driver;

        public SourceTasTargetQueryProcess(GameApplication process, ISourceEngineContext engine,
            Celeste.Mods.Tas.TasPlaybackDriver driver, SourceMemberCatalog members)
        {
            this.process = process;
            this.engine = engine;
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
            Members = members ?? throw new ArgumentNullException(nameof(members));
            sourceTypeReferences = new Celeste.Core.Reflection.Types.SourceTypeReferenceResolver(Members);
            Reflection = new QueryReflection(((IEverestLoggerProcessEnvironment)engine).Logger);
            Handlers =
            [
                new SourceSettingsQueryHandler(this),
                new SourceSaveDataQueryHandler(this),
                new SourceAssistsQueryHandler(this),
                new SourceExtendedVariantsQueryHandler(this),
                new SourceEverestModuleSettingsQueryHandler(this),
                new SourceEverestModuleSessionQueryHandler(this),
                new SourceEverestModuleSaveDataQueryHandler(this),
                new SourceEverestModuleQueryHandler(this),
                new SourceSceneQueryHandler(this),
                new SourceSessionQueryHandler(this),
                new SourceComponentQueryHandler(this),
                new SourceEntityQueryHandler(this),
                new SourceCollectionQueryHandler(this),
                new SourceSpecialValueQueryHandler(this),
                new SourceDeterministicVariablesQueryHandler(this),
                new SourceModInteropQueryHandler(this)
            ];
        }

        [SourceTasInitialize(1)]
        private static void Initialize() => CelesteTasModule.Instance.Driver.TargetQuery.InitializeCore();

        private void InitializeCore()
        {
            CollectAllTypes();

            ((Celeste.Everest.Modules.Runtime.ISourceEverestModulesProcessEnvironment)engine).ModulesProcess.Events
                .OnLoadMod += OnModLoad;
        }

        [SourceTasUnload]
        private static void Unload() => CelesteTasModule.Instance.Driver.TargetQuery.UnloadCore();

        private void UnloadCore()
        {
            ((Celeste.Everest.Modules.Runtime.ISourceEverestModulesProcessEnvironment)engine).ModulesProcess.Events
                .OnLoadMod -= OnModLoad;
        }

        // Refresh type cache
        private void OnModLoad(EverestModuleMetadata _) => CollectAllTypes();

        private void CollectAllTypes()
        {
            AllTypes.Clear();
            BaseTypeCache.Clear();

            foreach (var actualType in EnumerateQueryTypes())
            {
                var identity =
                    global::Celeste.Core.Reflection.Types.TypeReflectionIdentity.Resolve(Members, actualType);
                string fullName = identity.TypeIdentity.Replace('+', '.');
                string assemblyName = new AssemblyName(identity.AssemblyIdentity).Name;
                string modName = GetOriginalModName(actualType);
                AllTypes.AddToKey(fullName, actualType);
                AllTypes.AddToKey($"{fullName}@{assemblyName}", actualType);
                AllTypes.AddToKey($"{fullName}@{modName}", actualType);
                string originalNamespace = GetOriginalNamespace(identity.TypeIdentity);
                if (originalNamespace != null)
                {
                    string shortName = fullName[(originalNamespace.Length + 1)..];
                    AllTypes.AddToKey(shortName, actualType);
                    AllTypes.AddToKey($"{shortName}@{assemblyName}", actualType);
                    AllTypes.AddToKey($"{shortName}@{modName}", actualType);
                }
            }
        }

        [Celeste.Framework.Diagnostics.Commands.SourceCommand(CommandInfo.GetCommand,
            "'get Type.fieldOrProperty' -> value | Example: 'get Player.Position', 'get Level.Wind' (CelesteTAS)")]
        private static void GetCmd() => CelesteTasModule.Instance.Driver.TargetQuery.GetCmdCore();

        private void GetCmdCore()
        {
            if (!CommandLine.TryParse(
                    ((Celeste.Framework.Diagnostics.Commands.ISourceCommandsProcessEnvironment)engine).Commands
                    .ReadCommandHistory(0), out var commandLine))
            {
                ConsoleLog("Get Command Failed: Couldn't parse arguments of command", error: true);
                return;
            }

            Get(commandLine);
        }

        internal void Get(CommandLine commandLine)
        {
            if (commandLine.Arguments.Length == 0)
            {
                ConsoleLog("Get Command Failed: No target-query specified", error: true);
                return;
            }

            string query = string.Join(commandLine.ArgumentSeparator, commandLine.Arguments);

            var result = GetMemberValues(query);
            if (result.Failure)
            {
                ConsoleLog($"Get Command Failed: {result.Error}", error: true);
                return;
            }

            if (result.Value.Count == 0)
            {
                ConsoleLog("Get Command Failed: No instances found", error: true);
            }
            else if (result.Value.Count == 1)
            {
                ConsoleLog(result.Value[0].Value);
            }
            else
            {
                foreach ((object baseInstance, object? value) in result.Value)
                {
                    if (baseInstance is Celeste.Core.Components.ISourceEntitySourceData entity &&
                        !string.IsNullOrEmpty(entity.SourceId.Level))
                    {
                        ConsoleLog($"[{entity.SourceId}] {value}");
                    }
                    else
                    {
                        ConsoleLog(value);
                    }
                }
            }
        }

        private void ConsoleLog(object? text, bool error = false)
        {
            // This is the original LogUtil.ConsoleLog call body, with the two levels used by Get.
            try
            {
                ((Celeste.Framework.Diagnostics.Commands.ISourceCommandsProcessEnvironment)engine).Commands?.Log(
                    text?.ToString() ?? "null",
                    error ? Celeste.Core.Display.PackedColor.Red : Celeste.Core.Display.PackedColor.Cyan);
            }
            catch (Exception)
            {
                // The original console helper ignores reporting exceptions.
            }
        }

        /// Parses a target-query and returns the results for that
        internal Result<List<(object BaseInstance, object? Value)>, QueryError> GetMemberValues(string query,
            bool forceAllowCodeExecution = false)
        {
            string[] queryArgs = query.Split('.');

            var baseTypes = ResolveBaseTypes(queryArgs, out string[] memberArgs);
            if (!baseTypes.Any())
            {
                return Result<List<(object BaseInstance, object? Value)>, QueryError>.Fail(
                    new QueryError.NoBaseTypes(query));
            }

            List<(object BaseInstance, object? Value)> allResults = [];
            MemberAccessError? error = null;

            foreach (var baseType in baseTypes)
            {
                object[] instances = ResolveTypeInstances(baseType);
                foreach (object instance in instances)
                {
                    if (instance is Type && memberArgs.Length == 0)
                    {
                        error = MemberAccessError.Aggregate(error, new MemberAccessError.NoMembers(), Members);
                        continue;
                    }

                    var result = GetMemberValue(instance, memberArgs, forceAllowCodeExecution);
                    if (result.Failure)
                    {
                        if (result.Error is MemberAccessError accessError)
                        {
                            error = MemberAccessError.Aggregate(error, accessError, Members);
                        }
                        else
                        {
                            return Result<List<(object BaseInstance, object? Value)>, QueryError>.Fail(result.Error);
                        }

                        continue;
                    }

                    allResults.AddRange(result.Value.Where(value => value != InvalidValue && value is not QueryError)
                        .Select(value => (instance, value)));
                }
            }

            if (error == null || allResults.Count != 0)
            {
                return Result<List<(object BaseInstance, object? Value)>, QueryError>.Ok(allResults);
            }

            return Result<List<(object BaseInstance, object? Value)>, QueryError>.Fail(error);
        }

        /// Parses a target-query and sets the value to the arguments
        public VoidResult<QueryError> SetMemberValues(string query, string[] arguments,
            bool forceAllowCodeExecution = false)
        {
            string[] queryArgs = query.Split('.');

            var baseTypes = ResolveBaseTypes(queryArgs, out string[] memberArgs);
            if (!baseTypes.Any())
            {
                return VoidResult<QueryError>.Fail(new QueryError.NoBaseTypes(query));
            }

            if (!memberArgs.Any())
            {
                return VoidResult<QueryError>.Fail(new MemberAccessError.NoMembers());
            }

            bool anySuccessful = false;
            MemberAccessError? error = null;

            var targetValueCache = new Dictionary<SourceQueryValueType, object?>();

            foreach (var baseType in baseTypes)
            {
                object[] instances = ResolveTypeInstances(baseType);
                foreach (object instance in instances)
                {
                    var memberResult = PrepareMemberValue(instance, memberArgs[..^1]);
                    if (memberResult.Failure)
                    {
                        if (memberResult.Error is MemberAccessError accessError)
                        {
                            error = MemberAccessError.Aggregate(error, accessError, Members);
                        }
                        else
                        {
                            return VoidResult<QueryError>.Fail(memberResult.Error);
                        }

                        continue;
                    }

                    foreach (object? target in memberResult.Value.Where(value =>
                                 value != null && value != InvalidValue && value is not QueryError))
                    {
                        var targetTypeResult = ResolveMemberTargetTypes(target!, memberArgs.Length - 1, memberArgs,
                            Variant.Set, arguments.Length);
                        if (targetTypeResult.Failure)
                        {
                            error = MemberAccessError.Aggregate(error, targetTypeResult.Error, Members);
                            continue;
                        }

                        var targetTypes = targetTypeResult.Value;
                        if (!targetValueCache.TryGetValue(targetTypes[0], out object? value))
                        {
                            var valueResult = ResolveValue(arguments, targetTypes);
                            if (valueResult.Failure)
                            {
                                return VoidResult<QueryError>.Fail(valueResult.Error);
                            }

                            targetValueCache[targetTypes[0]] = value = valueResult.Value[0];
                        }

                        var setResult = SetMember(target!, value, memberArgs);
                        if (setResult.Failure)
                        {
                            error = MemberAccessError.Aggregate(error, setResult.Error, Members);
                        }
                        else
                        {
                            anySuccessful = true;
                        }
                    }
                }
            }

            if (!anySuccessful && error != null)
            {
                return VoidResult<QueryError>.Fail(error);
            }

            return VoidResult<QueryError>.Ok;
        }

        /// Parses a target-query and invokes the method with the arguments on them
        internal VoidResult<QueryError> InvokeMemberMethods(string query, string[] arguments,
            bool forceAllowCodeExecution = false)
        {
            string[] queryArgs = query.Split('.');

            var baseTypes = ResolveBaseTypes(queryArgs, out string[] memberArgs);
            if (!baseTypes.Any())
            {
                return VoidResult<QueryError>.Fail(new QueryError.NoBaseTypes(query));
            }

            if (!memberArgs.Any())
            {
                return VoidResult<QueryError>.Fail(new MemberAccessError.NoMembers());
            }

            bool anySuccessful = false;
            MemberAccessError? error = null;

            var targetValueCache = new Dictionary<SourceQueryValueType[], object?[]>();

            foreach (var baseType in baseTypes)
            {
                object[] instances = ResolveTypeInstances(baseType);
                foreach (object instance in instances)
                {
                    var memberResult = PrepareMemberValue(instance, memberArgs[..^1]);
                    if (memberResult.Failure)
                    {
                        if (memberResult.Error is MemberAccessError accessError)
                        {
                            error = MemberAccessError.Aggregate(error, accessError, Members);
                        }
                        else
                        {
                            return VoidResult<QueryError>.Fail(memberResult.Error);
                        }

                        continue;
                    }

                    foreach (object? target in memberResult.Value.Where(value =>
                                 value != null && value != InvalidValue && value is not QueryError))
                    {
                        var targetTypeResult = ResolveMemberTargetTypes(target!, memberArgs.Length - 1, memberArgs,
                            Variant.Invoke, arguments.Length);
                        if (targetTypeResult.Failure)
                        {
                            error = MemberAccessError.Aggregate(error, targetTypeResult.Error, Members);
                            continue;
                        }

                        var targetTypes = targetTypeResult.Value;
                        if (!targetValueCache.TryGetValue(targetTypes, out object?[]? values))
                        {
                            var valueResult = ResolveValue(arguments, targetTypes);
                            if (valueResult.Failure)
                            {
                                return VoidResult<QueryError>.Fail(valueResult.Error);
                            }

                            targetValueCache[targetTypes] = values = valueResult.Value;
                        }

                        var invokeResult = InvokeMember(target!, values, memberArgs);
                        if (invokeResult.Failure)
                        {
                            error = MemberAccessError.Aggregate(error, invokeResult.Error, Members);
                        }
                        else
                        {
                            anySuccessful = true;
                        }
                    }
                }
            }

            if (!anySuccessful && error != null)
            {
                return VoidResult<QueryError>.Fail(error);
            }

            return VoidResult<QueryError>.Ok;
        }

        /// Sorts types by namespace into Celeste -> Monocle -> other (alphabetically)
        /// Inside the namespace it's sorted alphabetically
    }
}
