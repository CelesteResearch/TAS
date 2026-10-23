using Celeste.Mods.Tas.Input;
using Celeste.Core.Reflection.Methods;
using Celeste.Core.Reflection.Members;
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
        private class NamespaceComparer : IComparer<(string Name, Type Type)>
        {
            private readonly SourceTasTargetQueryProcess query;
            public NamespaceComparer(SourceTasTargetQueryProcess query) => this.query = query;

            public int Compare((string Name, Type Type) x, (string Name, Type Type) y)
            {
                if (GetOriginalNamespace(query.GetOriginalTypeFullName(x.Type)) == null &&
                    GetOriginalNamespace(query.GetOriginalTypeFullName(y.Type)) != null)
                {
                    return -1;
                }

                if (GetOriginalNamespace(query.GetOriginalTypeFullName(x.Type)) != null &&
                    GetOriginalNamespace(query.GetOriginalTypeFullName(y.Type)) == null)
                {
                    return 1;
                }

                if (GetOriginalNamespace(query.GetOriginalTypeFullName(x.Type)) == null ||
                    GetOriginalNamespace(query.GetOriginalTypeFullName(y.Type)) == null)
                {
                    return StringComparer.Ordinal.Compare(x.Name, y.Name);
                }

                int namespaceCompare = CompareNamespace(GetOriginalNamespace(query.GetOriginalTypeFullName(x.Type)),
                    GetOriginalNamespace(query.GetOriginalTypeFullName(y.Type)));
                if (namespaceCompare != 0)
                {
                    return namespaceCompare;
                }

                return StringComparer.Ordinal.Compare(x.Name, y.Name);
            }

            private int CompareNamespace(string x, string y)
            {
                if (x.StartsWith("Celeste") && y.StartsWith("Celeste")) return 0;
                if (x.StartsWith("Celeste")) return -1;
                if (y.StartsWith("Celeste")) return 1;
                if (x.StartsWith("Monocle") && y.StartsWith("Monocle")) return 0;
                if (x.StartsWith("Monocle")) return -1;
                if (y.StartsWith("Monocle")) return 1;
                return StringComparer.Ordinal.Compare(x, y);
            }
        }

        internal readonly string[] ignoredNamespaces =
        [
            // C# Standard library
            "System",
            // Celeste libraries
            "SimplexNoise", "FMOD",
            // Everest libraries
            "MonoMod",
            // Non-gameplay-relevant utilities
            "Celeste.Editor", "Celeste.Mod.Helpers", "Celeste.Mod.Meta", "Celeste.Mod.UI",
            // Non-gameplay-relevant mods
            "TAS", "StudioCommunication",
            "Celeste.Mod.TASHelper",
            "Celeste.Mod.SpeedrunTool", "Force.DeepCloner",
            "Celeste.Mod.TASRecorder", "FFmpeg",
            "Celeste.Mod.DebugConsole",
            "Celeste.Mod.ImGuiHelper",
            "Celeste.Mod.MappingUtils",
            "Celeste.Mod.CelesteRepl",
            "Snowberry"
        ];

        private const int MaxTypeViabilityRecursion = 3;

        /// Checks if a value for the target type could be resolved and uses that to guess if this is the final type
        private bool GuessIsFinal(Type type)
        {
            return type.IsAssignableFrom(typeof(string))
                   || type.IsPrimitive || type == typeof(decimal)
                   || type.IsEnum
                   || Handlers.Any(handler => handler.CanResolveValue(type));
        }

        internal bool IsTypeViable(Type type, Variant variant)
        {
            var definition = IsRuntimeOwnedQueryType(type) ? null : Members.GetOriginalTypeDefinition(type);
            string fullName = definition?.Identity ?? type.FullName;
            string sourceNamespace = GetOriginalNamespace(fullName);
            // Original class/struct and compiler attributes come from the same original PE.
            if ((((definition?.Attributes ?? (int)type.Attributes) & (int)TypeAttributes.ClassSemanticsMask) ==
                 (int)TypeAttributes.Interface)
                || fullName.Contains('`') ||
                (sourceNamespace != null && ignoredNamespaces.Any(ns => sourceNamespace.StartsWith(ns))))
            {
                return false;
            }

            // Filter-out compiler generated types
            if ((definition != null
                    ? definition.HasDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute")
                    : type.GetCustomAttributesData().Any(a => a.AttributeType.FullName ==
                                                              "System.Runtime.CompilerServices.CompilerGeneratedAttribute")) ||
                fullName.Contains('<') || fullName.Contains('>'))
            {
                return false;
            }

            // Require some viable members
            var bindingFlags = Handlers.Any(handler => handler.CanResolveInstances(type))
                ? QueryReflection.StaticInstanceAnyVisibility
                : QueryReflection.StaticAnyVisibility;

            return EnumerateViableFields(type, variant, bindingFlags).Any() ||
                   EnumerateViableProperties(type, variant, bindingFlags).Any() ||
                   EnumerateViableMethods(type, variant, bindingFlags).Any();
        }

        internal IEnumerable<SourceQueryMemberAccess> EnumerateViableFields(Type type, Variant variant,
            BindingFlags bindingFlags)
        {
            return GetAllOriginalFields(type, bindingFlags)
                .Where(f =>
                    // Filter-out compiler generated fields
                    !f.HasDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute") &&
                    !f.Name.Contains('<') && !f.Name.Contains('>') &&
                    // Require to be usable
                    IsFieldUsable(f, variant, GuessIsFinal(f.ValueType)));
        }

        internal IEnumerable<SourceQueryMemberAccess> EnumerateViableProperties(Type type, Variant variant,
            BindingFlags bindingFlags)
        {
            return GetAllOriginalProperties(type, bindingFlags)
                .Where(p =>
                    // Filter-out compiler generated properties
                    !p.HasDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute") &&
                    !p.Name.Contains('<') && !p.Name.Contains('>') &&
                    // Require to be usable
                    IsPropertyUsable(p, variant, GuessIsFinal(p.ValueType)));
        }

        internal IEnumerable<SourceQueryMethodAccess> EnumerateViableMethods(Type type, Variant variant,
            BindingFlags bindingFlags)
        {
            return GetQueryMethods(type, bindingFlags)
                .Where(m =>
                    // Filter-out compiler generated fields
                    !m.HasDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute") &&
                    !m.Name.Contains('<') && !m.Name.Contains('>') &&
                    // Require to be usable
                    IsMethodUsable(m, variant, isFinal: true));
        }

        internal IEnumerator<CommandAutoCompleteEntry> ResolveAutoCompleteEntries(string[] queryArgs, Variant variant,
            Type[]? targetTypeFilter = null)
        {
            // Process query arguments
            queryArgs = Handlers.Aggregate((IEnumerable<string>)queryArgs,
                (current, handler) => handler.ProcessQueryArguments(current, isAutoComplete: true)).ToArray();
            // Drop last argument for prefix
            string queryPrefix = queryArgs.Length <= 1
                ? string.Empty
                : string.Join('.',
                    Handlers.Aggregate((IEnumerable<string>)queryArgs[..^1],
                        (current, handler) => handler.FormatQueryArguments(current)));
            string memberQueryPrefix = queryArgs.Length <= 1 ? queryPrefix : $"{queryPrefix}.";

            if (variant == Variant.Get && targetTypeFilter != null)
            {
                foreach (var targetType in targetTypeFilter)
                {
                    foreach (var handler in Handlers.Where(handler => handler.CanEnumerateTypeEntries(targetType)))
                    {
                        using var enumerator = handler.EnumerateTypeEntries(targetType);
                        while (enumerator.MoveNext())
                        {
                            yield return enumerator.Current;
                        }

                        goto NextType;
                    }

                    if (targetType == typeof(bool))
                    {
                        yield return new CommandAutoCompleteEntry
                        {
                            Name = "true", Extra = targetType.CSharpName(), IsDone = true,
                            StorageKey = $"{nameof(Variant.Get)}_{typeof(bool).FullName}", Suggestion = true
                        };
                        yield return new CommandAutoCompleteEntry
                        {
                            Name = "false", Extra = targetType.CSharpName(), IsDone = true,
                            StorageKey = $"{nameof(Variant.Get)}_{typeof(bool).FullName}", Suggestion = true
                        };
                    }
                    else if (targetType.IsEnum)
                    {
                        foreach (object value in Enum.GetValues(targetType))
                        {
                            yield return new CommandAutoCompleteEntry
                            {
                                Name = value.ToString()!, Extra = targetType.CSharpName(), IsDone = true,
                                StorageKey = targetType.FullName == null
                                    ? null
                                    : $"{nameof(Variant.Get)}_{targetType.FullName}",
                                Suggestion = true
                            };
                        }
                    }

                    NextType: ;
                }

                // Don't show generic results for parametric 'Get's
                yield break;
            }

            // Global entries
            foreach (var handler in Handlers)
            {
                using var enumerator = handler.ProvideGlobalEntries(queryArgs, queryPrefix, variant);
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }

            {
                using var enumerator = ResolveBaseTypeAutoCompleteEntries(queryArgs, memberQueryPrefix, variant);
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }

            var baseTypes = ResolveBaseTypes(queryArgs, out string[] memberArgs, processArgs: false);
            if (memberArgs.Length == 0)
            {
                yield break;
            }

            foreach (var baseType in baseTypes)
            {
                // Recurse type
                var currentType = RecurseMemberType(baseType, memberArgs[..^1], variant);
                if (currentType == null)
                {
                    yield break;
                }

                foreach (var handler in Handlers.Where(handler =>
                             handler.CanEnumerateMemberEntries(currentType, variant, queryPrefix, memberArgs.Length - 1,
                                 memberArgs).CanEnumerate))
                {
                    using var enumerator = handler.EnumerateMemberEntries(currentType, variant, queryPrefix,
                        memberArgs.Length - 1, memberArgs);
                    while (enumerator.MoveNext())
                    {
                        yield return enumerator.Current;
                    }

                    if (handler.CanEnumerateMemberEntries(currentType, variant, queryPrefix, memberArgs.Length - 1,
                            memberArgs).ShouldOverride)
                    {
                        goto NextType;
                    }

                    break;
                }

                // Generic handler
                {
                    var bindingFlags = memberArgs.Length <= 1
                        ? Handlers.Any(handler => handler.CanResolveInstances(currentType))
                            ? QueryReflection.StaticInstanceAnyVisibility
                            : QueryReflection.StaticAnyVisibility
                        : QueryReflection.InstanceAnyVisibility;

                    foreach (var field in EnumerateViableFields(currentType, variant, bindingFlags)
                                 .OrderBy(f => f.Name))
                    {
                        bool isFinal = GuessIsFinal(field.ValueType) && (targetTypeFilter == null ||
                                                                         targetTypeFilter.Any(type =>
                                                                             Reflection.CanCoerceTo(field.ValueType,
                                                                                 type)));
                        yield return new CommandAutoCompleteEntry
                        {
                            Name = isFinal ? field.Name : field.Name + ".",
                            Extra = field.ValueType.CSharpName(),
                            Prefix = memberQueryPrefix,
                            Suggestion = Handlers.Any(handler => handler.IsMemberSuggested(field, variant)),
                            IsDone = isFinal,
                            StorageKey = currentType.FullName == null ? null : $"{variant}_{currentType.FullName}",
                            StorageName = field.Name,
                        };
                    }

                    foreach (var property in EnumerateViableProperties(currentType, variant, bindingFlags)
                                 .OrderBy(p => p.Name))
                    {
                        bool isFinal = GuessIsFinal(property.ValueType) && (targetTypeFilter == null ||
                                                                            targetTypeFilter.Any(type =>
                                                                                Reflection.CanCoerceTo(
                                                                                    property.ValueType,
                                                                                    type)));
                        yield return new CommandAutoCompleteEntry
                        {
                            Name = isFinal ? property.Name : property.Name + ".",
                            Extra = property.ValueType.CSharpName(),
                            Prefix = memberQueryPrefix,
                            Suggestion = Handlers.Any(handler => handler.IsMemberSuggested(property, variant)),
                            IsDone = isFinal,
                            StorageKey = currentType.FullName == null ? null : $"{variant}_{currentType.FullName}",
                            StorageName = property.Name,
                        };
                    }

                    foreach (var method in EnumerateViableMethods(currentType, variant, bindingFlags)
                                 .OrderBy(m => m.Name))
                    {
                        yield return new CommandAutoCompleteEntry
                        {
                            Name = method.Name,
                            Extra =
                                $"({string.Join(", ", method.ParameterTypes.Select((p, i) => method.ParameterDefaults[i] ? $"[{p.PhysicalType.CSharpName()}]" : p.PhysicalType.CSharpName()))})",
                            Prefix = memberQueryPrefix,
                            Suggestion = Handlers.Any(handler =>
                                handler.IsMemberSuggested(
                                    new SourceQueryMemberIdentity(method.Name, method.DeclaringType),
                                    variant)),
                            IsDone = true,
                            StorageKey = currentType.FullName == null ? null : $"{variant}_{currentType.FullName}",
                            StorageName = method.Name,
                        };
                    }
                }

                NextType: ;
            }
        }

        internal IEnumerator<CommandAutoCompleteEntry> ResolveBaseTypeAutoCompleteEntries(string[] queryArgs,
            string queryPrefix, Variant variant, Predicate<Type>? typeFilterPredicate = null,
            Predicate<Type>? typeSuggestionPredicate = null)
        {
            var types = EnumerateQueryTypes()
                .Where(type => !GetOriginalTypeFullName(type).Contains('`')
                               && !GetOriginalTypeFullName(type).Contains('<')
                               && !GetOriginalTypeFullName(type).Contains('>')
                               && !HasQueryCompilerGeneratedAttribute(type)
                               && (Members.ContainsSourceType(type)
                                   ? (Members.GetOriginalTypeDefinition(type).Attributes &
                                      (int)TypeAttributes.ClassSemanticsMask) != (int)TypeAttributes.Interface
                                   : !type.IsInterface)
                               && GetOriginalTypeFullName(type).StartsWith(queryPrefix)
                               && (GetOriginalNamespace(GetOriginalTypeFullName(type)) is not { } ns
                                   || !ignoredNamespaces.Any(ignored => ns.StartsWith(ignored))))
                .Where(type =>
                    (typeFilterPredicate?.Invoke(type) ?? true) &&
                    IsTypeViable(type, variant) &&
                    // Require query-arguments to match namespace
                    GetOriginalTypeFullName(type).StartsWith(queryPrefix))
                .OrderBy(t => (GetOriginalTypeName(t), t), new NamespaceComparer(this))
                .ToArray();

            string[][] namespaces = types
                .Select<Type, string?>(type => GetOriginalNamespace(GetOriginalTypeFullName(type)))
                .OfType<string>()
                .Distinct()
                .Select(ns => ns.Split('.'))
                .Where(ns => ns.Length > queryArgs.Length - 1)
                .ToArray();

            // Merge the lowest common namespaces (we love triple nested loops!)
            for (int nsIdxA = 0; nsIdxA < namespaces.Length; nsIdxA++)
            {
                for (int compLen = namespaces[nsIdxA].Length; compLen > queryArgs.Length - 1; compLen--)
                {
                    string[] subSeq = namespaces[nsIdxA][..compLen];
                    bool foundAny = false;

                    for (int nsIdxB = 0; nsIdxB < namespaces.Length; nsIdxB++)
                    {
                        if (nsIdxA == nsIdxB || namespaces[nsIdxB].Length < compLen)
                        {
                            continue;
                        }

                        if (namespaces[nsIdxB].SequenceStartsWith(subSeq))
                        {
                            foundAny = true;
                            namespaces[nsIdxB] = [];
                        }
                    }

                    if (foundAny)
                    {
                        namespaces[nsIdxA] = subSeq;
                    }
                }
            }

            foreach (string[] ns in namespaces)
            {
                if (ns.Length < queryArgs.Length)
                {
                    continue;
                }

                yield return new CommandAutoCompleteEntry
                {
                    Name = $"{string.Join('.', ns[(queryArgs.Length - 1)..])}.",
                    Extra = "Namespace",
                    Prefix = queryPrefix,
                    IsDone = false,
                    StorageKey = $"{variant}",
                    StorageName = string.Join('.', ns[(queryArgs.Length - 1)..]),
                };
            }

            foreach (var type in types)
            {
                var definition = global::Celeste.Core.Reflection.Types.TypeReflectionIdentity.Resolve(Members, type);
                string sourceNamespace = GetOriginalNamespace(definition.TypeIdentity);
                if (queryPrefix.Length != 0 &&
                    (sourceNamespace == null || sourceNamespace.Length + 1 != queryPrefix.Length))
                {
                    // Require exact prefix if specified
                    continue;
                }

                string assemblyName = new AssemblyName(definition.AssemblyIdentity).Name!;
                string modName = GetOriginalModName(type);

                // Use '.' instead of '+' for nested types
                string fullName = definition.TypeIdentity.Replace('+', '.');

                // Strip namespace
                int namespaceLen = sourceNamespace != null
                    ? sourceNamespace.Length + 1
                    : 0;
                string shortName = fullName[namespaceLen..];

                bool suggestion = typeSuggestionPredicate?.Invoke(type) ??
                                  Handlers.Any(handler => handler.IsTypeSuggested(type, variant));

                // Use short name if possible, otherwise specify mod name / assembly name
                if (AllTypes[shortName].Count == 1)
                {
                    yield return new CommandAutoCompleteEntry
                    {
                        Name = $"{shortName}.",
                        Extra = sourceNamespace ?? string.Empty,
                        Prefix = queryPrefix,
                        Suggestion = suggestion,
                        IsDone = false,
                        StorageKey = $"{variant}_{sourceNamespace ?? string.Empty}",
                        StorageName = definition.TypeIdentity,
                    };
                }
                else if (AllTypes[$"{shortName}@{modName}"].Count == 1)
                {
                    yield return new CommandAutoCompleteEntry
                    {
                        Name = $"{shortName}@{modName}.",
                        Extra = sourceNamespace ?? string.Empty,
                        Prefix = queryPrefix,
                        Suggestion = suggestion,
                        IsDone = false,
                        StorageKey = $"{variant}_{sourceNamespace ?? string.Empty}",
                        StorageName = definition.TypeIdentity,
                    };
                }
                else if (AllTypes[$"{shortName}@{assemblyName}"].Count == 1)
                {
                    yield return new CommandAutoCompleteEntry
                    {
                        Name = $"{shortName}@{assemblyName}.",
                        Extra = sourceNamespace ?? string.Empty,
                        Prefix = queryPrefix,
                        Suggestion = suggestion,
                        IsDone = false,
                        StorageKey = $"{variant}_{sourceNamespace ?? string.Empty}",
                        StorageName = definition.TypeIdentity,
                    };
                }
            }
        }

        internal Type? RecurseMemberType(Type baseType, string[] memberArgs, Variant variant)
        {
            var currentType = baseType;
            for (int memberIdx = 0; memberIdx < memberArgs.Length; memberIdx++)
            {
                foreach (var handler in Handlers)
                {
                    var result =
                        handler.ResolveTargetTypes(out var targetTypes, currentType, ref memberIdx, memberArgs);
                    if (result.Success && result.Value)
                    {
                        currentType = targetTypes[0]; // Ignore others
                        goto NextMember;
                    }

                    if (result.Failure)
                    {
                        return null;
                    }
                }

                string member = memberArgs[memberIdx];
                var bindingFlags = memberIdx == 0
                    ? Handlers.Any(handler => handler.CanResolveInstances(currentType))
                        ? QueryReflection.StaticInstanceAnyVisibility
                        : QueryReflection.StaticAnyVisibility
                    : QueryReflection.InstanceAnyVisibility;

                if (GetOriginalField(currentType, member, bindingFlags, logFailure: false) is { } field &&
                    IsFieldUsable(field, variant, isFinal: false))
                {
                    currentType = field.ValueType;
                    continue;
                }

                if (GetOriginalProperty(currentType, member, bindingFlags, logFailure: false) is { } property &&
                    IsPropertyUsable(property, variant, isFinal: false))
                {
                    currentType = property.ValueType;
                    continue;
                }

                NextMember: ;
            }

            return currentType;
        }

        private bool IsSettableType(Type type) => !type.IsSameOrSubclassOf(typeof(Delegate));

        private bool IsInvokableMethod(SourceQueryMethodAccess info)
        {
            // Generic methods could probably be supported somehow, but that's probably not worth
            if (info.GenericParameterCount > 0)
            {
                return false;
            }

            try
            {
                // To be invokable, all parameters need to be settable or have a default value from a non-settable onwards
                bool requireDefaults = false;
                var parameterTypes = info.ParameterTypes;
                var defaults = info.ParameterDefaults;
                for (int i = 0; i < parameterTypes.Length; i++)
                {
                    if (!requireDefaults && !IsSettableType(parameterTypes[i].PhysicalType))
                    {
                        requireDefaults = true;
                    }

                    if (requireDefaults && !defaults[i])
                    {
                        return false;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                // Parameters depend on an unloaded optional dependency
                return false;
            }

            return true;
        }

        internal bool IsFieldUsable(SourceQueryMemberAccess field, Variant variant, bool isFinal)
        {
            return variant switch
            {
                Variant.Get => true,
                Variant.Set => isFinal
                    ? (field.OriginalFieldAttributes & (FieldAttributes.InitOnly | FieldAttributes.Literal)) == 0 &&
                      IsSettableType(field.ValueType)
                    : (field.OriginalFieldAttributes & (FieldAttributes.InitOnly | FieldAttributes.Literal)) == 0 ||
                      !field.ValueType.IsValueType,
                Variant.Invoke => !isFinal || field.ValueType.IsSameOrSubclassOf(typeof(Delegate)),
                _ => throw new ArgumentOutOfRangeException(nameof(variant), variant, null)
            };
        }

        internal bool IsPropertyUsable(SourceQueryMemberAccess property, Variant variant, bool isFinal)
        {
            return variant switch
            {
                Variant.Get => property.CanRead,
                Variant.Set => isFinal
                    ? property.CanWrite && IsSettableType(property.ValueType)
                    : property.CanRead && (property.CanWrite || !property.ValueType.IsValueType),
                Variant.Invoke => !isFinal || property.ValueType.IsSameOrSubclassOf(typeof(Delegate)),
                _ => throw new ArgumentOutOfRangeException(nameof(variant), variant, null)
            };
        }

        internal bool IsMethodUsable(SourceQueryMethodAccess method, Variant variant, bool isFinal)
        {
            return variant switch
            {
                Variant.Get => false,
                Variant.Set => false,
                Variant.Invoke => isFinal && IsInvokableMethod(method),
                _ => throw new ArgumentOutOfRangeException(nameof(variant), variant, null)
            };
        }

        internal IEnumerable<string> GetQueryArgs(string[] args, int index)
        {
            if (args.Length <= index)
            {
                return [];
            }

            return Handlers
                .Aggregate((IEnumerable<string>)args[index].Split('.'),
                    (current, handler) => handler.ProcessQueryArguments(current, isAutoComplete: true))
                // Only skip last part if we're currently editing that
                .SkipLast(args.Length == index + 1 ? 1 : 0);
        }
    }
}
