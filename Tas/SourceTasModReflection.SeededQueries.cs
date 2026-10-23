using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Celeste.Core.Reflection.Assemblies;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;
using Celeste.Core.Reflection.Runtime;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Everest.Reflection;

namespace Celeste.Mods.Tas
{
    internal sealed partial class SourceTasModReflection
    {
        private readonly record struct MethodKey(Type Type, string Name, long ParameterHash, BindingFlags Flags);

        private readonly ConcurrentDictionary<MethodKey, SourceOrderedMethod> methodInfos = new();
        private readonly ConcurrentDictionary<string, SourceLogicalAggregateAssembly> moduleAssemblies = new();

        private sealed class SingleAssemblyOrder : ISourceAggregateAssemblyOrder
        {
            private readonly string identity;
            internal SingleAssemblyOrder(string identity) => this.identity = identity;

            public IEnumerable<string> GetOriginalAggregateAssemblyOrder()
            {
                yield return identity;
            }
        }

        internal ReflectedAssembly GetAssembly(string modName)
        {
            var module = GetModule(modName);
            if (module == null) return null;
            var reflection = ((ISourceEverestReflectionEnvironment)process).Reflection;
            if (!reflection.SourceMembers.ContainsSourceType(module.GetType()))
                return new ReflectedAssembly(module.GetType().Assembly, module.Metadata.AssemblyContext?.Assemblies);
            string identity = reflection.SourceMembers.GetOriginalTypeDefinition(module.GetType()).AssemblyIdentity;
            return new ReflectedAssembly(moduleAssemblies.GetOrAdd(identity, key => new SourceLogicalAggregateAssembly(
                key, reflection.SourceMembers, reflection.SourceResources, new SingleAssemblyOrder(key))));
        }

        internal ReflectedMember GetField(string modName, string fullTypeName, string fieldName)
        {
            var type = GetType(modName, fullTypeName);
            if (type == null) return null;

            var field = GetFieldInfo(type, fieldName);
            if (field == null)
            {
                Log($"Failed to find field '{fieldName}' in type '{fullTypeName}' of mod '{modName}'");
                return null;
            }

            return field;
        }

        private IEnumerable<SourceOrderedMethod> GetOriginalMethodCandidates(Type type, string name,
            Type[] parameterTypes,
            BindingFlags bindingFlags)
        {
            bindingFlags |= BindingFlags.DeclaredOnly;
            var reflection = ((ISourceEverestReflectionEnvironment)process).Reflection;
            HashSet<SourceOrderedMethod> allMethods = new();
            var currentType = reflection.SourceMembers.GetOriginalTypeDefinition(type);
            while (currentType != null && currentType.BaseTypeIdentity != null)
            {
                var actualType =
                    reflection.SourceMembers.GetActualSourceType(currentType.AssemblyIdentity, currentType.Identity);
                foreach (var method in reflection.SourceMethods.GetMethods(reflection.SourceMembers, actualType,
                             bindingFlags, name, parameterTypes)) allMethods.Add(method);
                currentType =
                    reflection.SourceMembers.GetOriginalTypeDefinition(currentType.BaseAssemblyIdentity,
                        currentType.BaseTypeIdentity);
            }

            return allMethods;
        }

        private static long GetParameterHash<T>(IEnumerable<T> enumerable)
        {
            if (enumerable == null) return 0;
            unchecked
            {
                long hash = 17;
                foreach (var item in enumerable)
                    hash = hash * -1521134295 + EqualityComparer<T>.Default.GetHashCode(item);
                return hash;
            }
        }

        private bool IsOriginalSubclass(SourceOrderedMethod derived, SourceOrderedMethod parent)
        {
            var members = ((ISourceEverestReflectionEnvironment)process).Reflection.SourceMembers;
            var current =
                members.GetOriginalTypeDefinition(derived.SourceAssemblyIdentity, derived.SourceDeclaringType);
            while (current.BaseTypeIdentity != null)
            {
                current = members.GetOriginalTypeDefinition(current.BaseAssemblyIdentity, current.BaseTypeIdentity);
                if (current.AssemblyIdentity == parent.SourceAssemblyIdentity &&
                    current.Identity == parent.SourceDeclaringType) return true;
            }

            return false;
        }

        private readonly ConcurrentDictionary<MethodKey, ReflectedMethod> runtimeMethodInfos = new();

        internal IEnumerable<ReflectedMethod> GetAllMethodInfos(Type type,
            BindingFlags bindingFlags = StaticInstanceAnyVisibility)
        {
            var reflection = ((ISourceEverestReflectionEnvironment)process).Reflection;
            return PortableReflection.GetMethods(reflection.SourceMembers, reflection.SourceMethods, type,
                bindingFlags & ~BindingFlags.DeclaredOnly);
        }

        internal ReflectedMethod GetMethodInfo(Type type, string name, Type[] parameterTypes = null,
            BindingFlags bindingFlags = StaticInstanceAnyVisibility, bool logFailure = true)
        {
            var reflection = ((ISourceEverestReflectionEnvironment)process).Reflection;
            if (reflection.SourceMembers.ContainsSourceType(type))
            {
                var binding = GetOriginalMethodInfo(type, name, parameterTypes, bindingFlags, logFailure);
                return binding == null
                    ? null
                    : new ReflectedMethod(binding, name,
                        reflection.SourceMembers.GetActualSourceType(binding.SourceAssemblyIdentity,
                            binding.SourceDeclaringType));
            }

            var key = new MethodKey(type, name, GetParameterHash(parameterTypes), bindingFlags);
            if (runtimeMethodInfos.TryGetValue(key, out var result)) return result;
            try
            {
                result = PortableReflection.GetMethod(reflection.SourceMembers, reflection.SourceMethods,
                    type, name, parameterTypes, bindingFlags);
                if (result == null && logFailure) Log($"Failed to find method '{name}' on type '{type.FullName}'");
            }
            catch (AmbiguousMatchException failure)
            {
                if (logFailure)
                    ((IEverestLoggerProcessEnvironment)process).Logger.LogDetailed(failure, "CelesteTAS");
                result = null;
            }

            return runtimeMethodInfos[key] = result;
        }

        private SourceOrderedMethod GetOriginalMethodInfo(Type type, string name, Type[] parameterTypes = null,
            BindingFlags bindingFlags = StaticInstanceAnyVisibility, bool logFailure = true)
        {
            var key = new MethodKey(type, name, GetParameterHash(parameterTypes), bindingFlags);
            if (methodInfos.TryGetValue(key, out var result)) return result;
            var reflection = ((ISourceEverestReflectionEnvironment)process).Reflection;
            var members = reflection.SourceMembers;
            var methods = reflection.SourceMethods;
            var currentType = members.GetOriginalTypeDefinition(type);
            do
            {
                var actualType = members.GetActualSourceType(currentType.AssemblyIdentity, currentType.Identity);
                if (parameterTypes != null)
                {
                    foreach (var method in GetOriginalMethodCandidates(actualType, name, parameterTypes, bindingFlags))
                    {
                        var definition = methods.RequireDefinition(method.SourceAssemblyIdentity,
                            method.SourceMethodDefinitionToken);
                        if (definition.Name != name) continue;
                        var parameters = definition.ParameterTypeIdentities;
                        if (parameters == null)
                            throw new InvalidOperationException("Original method parameter metadata is not published.");
                        if (parameters.Count != parameterTypes.Length) continue;
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            if (parameterTypes[i] == null) continue;
                            string identity = members.ContainsSourceType(parameterTypes[i])
                                ? members.GetOriginalTypeFullName(parameterTypes[i])
                                : parameterTypes[i].Assembly == typeof(object).Assembly
                                    ? parameterTypes[i].FullName
                                    : throw new InvalidOperationException(
                                        "The query parameter has no qualified original identity.");
                            if (identity != parameters[i]) goto NextMethod;
                        }

                        if (result != null)
                        {
                            if (result.SourceAssemblyIdentity != method.SourceAssemblyIdentity ||
                                result.SourceDeclaringType != method.SourceDeclaringType)
                            {
                                if (IsOriginalSubclass(method, result)) result = method;
                            }
                            else
                            {
                                if (logFailure)
                                    Log(
                                        $"Method '{name}' with parameters ({string.Join<Type>(", ", parameterTypes)}) on type '{members.GetOriginalTypeFullName(type)}' is ambiguous between '{result.SourceDeclaringType}' and '{method.SourceDeclaringType}'");
                                result = null;
                                break;
                            }
                        }
                        else result = method;

                        NextMethod: ;
                    }
                }
                else
                {
                    try
                    {
                        var matches = methods.GetMethods(members, actualType, bindingFlags, name).ToArray();
                        if (matches.Length > 1) throw new AmbiguousMatchException();
                        result = matches.Length == 1 ? matches[0] : null;
                    }
                    catch (Exception ex) when (ex is not InvalidOperationException)
                    {
                        Log($"Failed to get method '{name}' on type '{members.GetOriginalTypeFullName(type)}'");
                        ((IEverestLoggerProcessEnvironment)process).Logger.LogDetailed(ex, "CelesteTAS");
                        return methodInfos[key] = null;
                    }
                }

                currentType = currentType.BaseTypeIdentity == null
                    ? null
                    : members.GetOriginalTypeDefinition(currentType.BaseAssemblyIdentity, currentType.BaseTypeIdentity);
            } while (result == null && currentType != null);

            if (result == null && logFailure)
            {
                if (parameterTypes == null)
                    Log($"Failed to find method '{name}' on type '{members.GetOriginalTypeFullName(type)}'");
                else
                    Log(
                        $"Failed to find method '{name}' with parameters ({string.Join<Type>(", ", parameterTypes)}) on type '{members.GetOriginalTypeFullName(type)}'");
            }

            return methodInfos[key] = result;
        }
    }
}
