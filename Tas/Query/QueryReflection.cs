using Celeste.Everest;
#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Numerics;
using Celeste.Everest.Diagnostics.Logging;

namespace Celeste.Mods.Tas.Query
{
    public sealed partial class QueryReflection
    {
        private readonly EverestLoggerProcess logger;

        public QueryReflection(EverestLoggerProcess logger) =>
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        private void LogError(string text) => logger.Log(EverestLogLevel.Error, "CelesteTAS", text);

        private void LogException(Exception exception, string header)
        {
            LogError(header);
            logger.LogDetailed(exception, "CelesteTAS");
        }

        public const BindingFlags InstanceAnyVisibility =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public const BindingFlags StaticAnyVisibility =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

        public const BindingFlags StaticInstanceAnyVisibility =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        public const BindingFlags InstanceAnyVisibilityDeclaredOnly = BindingFlags.Public | BindingFlags.NonPublic |
                                                                      BindingFlags.Instance | BindingFlags.DeclaredOnly;

        private readonly record struct MemberKey(Type Type, string Name);

        private readonly record struct MethodKey(Type Type, string Name, long ParameterHash);

        private readonly record struct ConstructorKey(Type Type, long ParameterHash);

        private readonly record struct AllMemberKey(Type Type, BindingFlags BindingFlags);

        private readonly ConcurrentDictionary<MemberKey, MemberInfo?> CachedMemberInfos = new();
        private readonly ConcurrentDictionary<MemberKey, FieldInfo?> CachedFieldInfos = new();
        private readonly ConcurrentDictionary<MemberKey, PropertyInfo?> CachedPropertyInfos = new();
        private readonly ConcurrentDictionary<MethodKey, MethodInfo?> CachedMethodInfos = new();
        private readonly ConcurrentDictionary<ConstructorKey, ConstructorInfo?> CachedConstructorInfos = new();
        private readonly ConcurrentDictionary<MemberKey, EventInfo?> CachedEventInfos = new();

        private readonly ConcurrentDictionary<MemberKey, MethodInfo?> CachedGetMethodInfos = new();
        private readonly ConcurrentDictionary<MemberKey, MethodInfo?> CachedSetMethodInfos = new();

        private readonly ConcurrentDictionary<AllMemberKey, IEnumerable<FieldInfo>> CachedAllFieldInfos = new();
        private readonly ConcurrentDictionary<AllMemberKey, IEnumerable<PropertyInfo>> CachedAllPropertyInfos = new();
        private readonly ConcurrentDictionary<AllMemberKey, IEnumerable<MethodInfo>> CachedAllMethodInfos = new();

        private readonly ConcurrentDictionary<AllMemberKey, IEnumerable<ConstructorInfo>> CachedAllConstructorInfos =
            new();

        /// Resolves the target member on the type, caching the result
        public MemberInfo? GetMemberInfo(Type type, string name,
            BindingFlags bindingFlags = StaticInstanceAnyVisibility,
            bool logFailure = true)
        {
            var key = new MemberKey(type, name);
            if (CachedMemberInfos.TryGetValue(key, out var result))
            {
                return result;
            }

            var currentType = type;
            do
            {
                result = currentType.GetMember(name, bindingFlags).FirstOrDefault();
                currentType = currentType.BaseType;
            } while (result == null && currentType != null);

            if (result == null && logFailure)
            {
                LogError($"Failed to find member '{name}' on type '{type}'");
            }

            return CachedMemberInfos[key] = result;
        }

        /// Resolves the target field on the type, caching the result
        public FieldInfo? GetFieldInfo(Type type, string name, BindingFlags bindingFlags = StaticInstanceAnyVisibility,
            bool logFailure = true)
        {
            var key = new MemberKey(type, name);
            if (CachedFieldInfos.TryGetValue(key, out var result))
            {
                return result;
            }

            var currentType = type;
            do
            {
                result = currentType.GetField(name, bindingFlags);
                currentType = currentType.BaseType;
            } while (result == null && currentType != null);

            if (result == null && logFailure)
            {
                LogError($"Failed to find field '{name}' on type '{type}'");
            }

            return CachedFieldInfos[key] = result;
        }

        /// Resolves the target property on the type, caching the result
        public PropertyInfo? GetPropertyInfo(Type type, string name,
            BindingFlags bindingFlags = StaticInstanceAnyVisibility, bool logFailure = true)
        {
            var key = new MemberKey(type, name);
            if (CachedPropertyInfos.TryGetValue(key, out var result))
            {
                return result;
            }

            var currentType = type;
            do
            {
                result = currentType.GetProperty(name, bindingFlags);
                currentType = currentType.BaseType;
            } while (result == null && currentType != null);

            if (result == null && logFailure)
            {
                LogError($"Failed to find property '{name}' on type '{type}'");
            }

            return CachedPropertyInfos[key] = result;
        }

        /// Resolves the target method on the type, with the specific parameter types, caching the result
        public MethodInfo? GetMethodInfo(Type type, string name, Type?[]? parameterTypes = null,
            BindingFlags bindingFlags = StaticInstanceAnyVisibility, bool logFailure = true)
        {
            var key = new MethodKey(type, name, parameterTypes.GetCustomHashCode());
            if (CachedMethodInfos.TryGetValue(key, out var result))
            {
                return result;
            }

            var currentType = type;
            do
            {
                if (parameterTypes != null)
                {
                    foreach (var method in GetAllMethodInfos(currentType, bindingFlags))
                    {
                        if (method.Name != name)
                        {
                            continue;
                        }

                        var parameters = method.GetParameters();
                        if (parameters.Length != parameterTypes.Length)
                        {
                            continue;
                        }

                        for (int i = 0; i < parameters.Length; i++)
                        {
                            // Treat a null type as a wild card
                            if (parameterTypes[i] != null && parameterTypes[i] != parameters[i].ParameterType)
                            {
                                goto NextMethod;
                            }
                        }

                        if (result != null)
                        {
                            // "Amphibious" matches on different types indicate overrides. Choose the "latest" method
                            if (result.DeclaringType != null && result.DeclaringType != method.DeclaringType)
                            {
                                if (method.DeclaringType!.IsSubclassOf(result.DeclaringType))
                                {
                                    result = method;
                                }
                            }
                            else
                            {
                                if (logFailure)
                                {
                                    LogError(
                                        $"Method '{name}' with parameters ({string.Join<Type?>(", ", parameterTypes)}) on type '{type}' is ambiguous between '{result}' and '{method}'");
                                }

                                result = null;
                                break;
                            }
                        }
                        else
                        {
                            result = method;
                        }

                        NextMethod: ;
                    }
                }
                else
                {
                    try
                    {
                        result = currentType.GetMethod(name, bindingFlags);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex, $"Failed to get method '{name}' on type '{type}'");
                        return CachedMethodInfos[key] = null;
                    }
                }

                currentType = currentType.BaseType;
            } while (result == null && currentType != null);

            if (result == null && logFailure)
            {
                if (parameterTypes == null)
                {
                    LogError($"Failed to find method '{name}' on type '{type}'");
                }
                else
                {
                    LogError(
                        $"Failed to find method '{name}' with parameters ({string.Join<Type?>(", ", parameterTypes)}) on type '{type}'");
                }
            }

            return CachedMethodInfos[key] = result;
        }

        /// Resolves the target method on the type, with the specific parameter types, caching the result
        public ConstructorInfo? GetConstructorInfo(Type type, Type?[] parameterTypes,
            BindingFlags bindingFlags = StaticInstanceAnyVisibility, bool logFailure = true)
        {
            var key = new ConstructorKey(type, parameterTypes.GetCustomHashCode());
            if (CachedConstructorInfos.TryGetValue(key, out var result))
            {
                return result;
            }

            var currentType = type;
            do
            {
                foreach (var constructor in GetAllConstructorInfos(currentType, bindingFlags))
                {
                    var parameters = constructor.GetParameters();
                    if (parameters.Length != parameterTypes.Length)
                    {
                        continue;
                    }

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        // Treat a null type as a wild card
                        if (parameterTypes[i] != null && parameterTypes[i] != parameters[i].ParameterType)
                        {
                            goto NextMethod;
                        }
                    }

                    if (result != null)
                    {
                        // "Amphibious" matches on different types indicate overrides. Choose the "latest" method
                        if (result.DeclaringType != null && result.DeclaringType != constructor.DeclaringType)
                        {
                            if (constructor.DeclaringType!.IsSubclassOf(result.DeclaringType))
                            {
                                result = constructor;
                            }
                        }
                        else
                        {
                            if (logFailure)
                            {
                                LogError(
                                    $"Constructor with parameters ({string.Join<Type?>(", ", parameterTypes)}) on type '{type}' is ambiguous between '{result}' and '{constructor}'");
                            }

                            result = null;
                            break;
                        }
                    }
                    else
                    {
                        result = constructor;
                    }

                    NextMethod: ;
                }

                currentType = currentType.BaseType;
            } while (result == null && currentType != null);

            if (result == null && logFailure)
            {
                LogError(
                    $"Failed to find constructor with parameters ({string.Join<Type?>(", ", parameterTypes)}) on type '{type}'");
            }

            return CachedConstructorInfos[key] = result;
        }

        /// Resolves the target event on the type, with the specific parameter types, caching the result
        public EventInfo? GetEventInfo(Type type, string name, BindingFlags bindingFlags = StaticInstanceAnyVisibility)
        {
            var key = new MemberKey(type, name);
            if (CachedEventInfos.TryGetValue(key, out var result))
            {
                return result;
            }

            var currentType = type;
            do
            {
                result = currentType.GetEvent(name, bindingFlags);
                currentType = currentType.BaseType;
            } while (result == null && currentType != null);

            if (result == null)
            {
                LogError($"Failed to find event '{name}' on type '{type}'");
            }

            return CachedEventInfos[key] = result;
        }

        /// Resolves the target get-method of the property on the type, caching the result
        public MethodInfo? GetGetMethod(Type type, string name, BindingFlags bindingFlags = StaticInstanceAnyVisibility)
        {
            var key = new MemberKey(type, name);
            if (CachedGetMethodInfos.TryGetValue(key, out var result))
            {
                return result;
            }

            result = GetPropertyInfo(type, name, bindingFlags)?.GetGetMethod(nonPublic: true);
            if (result == null)
            {
                LogError($"Failed to find get-method of property '{name}' on type '{type}'");
            }

            return CachedGetMethodInfos[key] = result;
        }

        /// Resolves the target set-method of the property on the type, caching the result
        public MethodInfo? GetSetMethod(Type type, string name, BindingFlags bindingFlags = StaticInstanceAnyVisibility)
        {
            var key = new MemberKey(type, name);
            if (CachedSetMethodInfos.TryGetValue(key, out var result))
            {
                return result;
            }

            result = GetPropertyInfo(type, name, bindingFlags)?.GetSetMethod(nonPublic: true);
            if (result == null)
            {
                LogError($"Failed to find set-method of property '{name}' on type '{type}'");
            }

            return CachedSetMethodInfos[key] = result;
        }

        /// Resolves all fields of the type, caching the result
        public IEnumerable<FieldInfo> GetAllFieldInfos(Type type,
            BindingFlags bindingFlags = StaticInstanceAnyVisibility)
        {
            bindingFlags |= BindingFlags.DeclaredOnly;

            var key = new AllMemberKey(type, bindingFlags);
            if (CachedAllFieldInfos.TryGetValue(key, out var result))
            {
                return result;
            }

            HashSet<FieldInfo> allFields = [];

            var currentType = type;
            while (currentType != null && currentType.IsSubclassOf(typeof(object)))
            {
                allFields.UnionWith(currentType.GetFields(bindingFlags));

                currentType = currentType.BaseType;
            }

            return CachedAllFieldInfos[key] = allFields;
        }

        /// Resolves all properties of the type, caching the result
        public IEnumerable<PropertyInfo> GetAllPropertyInfos(Type type,
            BindingFlags bindingFlags = StaticInstanceAnyVisibility)
        {
            bindingFlags |= BindingFlags.DeclaredOnly;

            var key = new AllMemberKey(type, bindingFlags);
            if (CachedAllPropertyInfos.TryGetValue(key, out var result))
            {
                return result;
            }

            HashSet<PropertyInfo> allProperties = [];

            var currentType = type;
            while (currentType != null && currentType.IsSubclassOf(typeof(object)))
            {
                allProperties.UnionWith(currentType.GetProperties(bindingFlags));

                currentType = currentType.BaseType;
            }

            return CachedAllPropertyInfos[key] = allProperties;
        }

        /// Resolves all methods of the type, caching the result
        public IEnumerable<MethodInfo> GetAllMethodInfos(Type type,
            BindingFlags bindingFlags = StaticInstanceAnyVisibility)
        {
            bindingFlags |= BindingFlags.DeclaredOnly;

            var key = new AllMemberKey(type, bindingFlags);
            if (CachedAllMethodInfos.TryGetValue(key, out var result))
            {
                return result;
            }

            HashSet<MethodInfo> allMethods = [];

            var currentType = type;
            while (currentType != null && currentType.IsSubclassOf(typeof(object)))
            {
                allMethods.UnionWith(currentType.GetMethods(bindingFlags));

                currentType = currentType.BaseType;
            }

            return CachedAllMethodInfos[key] = allMethods;
        }

        /// Resolves all constructors of the type, caching the result
        public IEnumerable<ConstructorInfo> GetAllConstructorInfos(Type type,
            BindingFlags bindingFlags = InstanceAnyVisibility)
        {
            bindingFlags |= BindingFlags.DeclaredOnly;

            var key = new AllMemberKey(type, bindingFlags);
            if (CachedAllConstructorInfos.TryGetValue(key, out var result))
            {
                return result;
            }

            return CachedAllConstructorInfos[key] = type.GetConstructors(bindingFlags);
        }

        /// Gets the value of the instance field on the object
        public T? GetFieldValue<T>(object obj, string name)
        {
            if (GetFieldInfo(obj.GetType(), name, InstanceAnyVisibility) is not { } field)
            {
                return default;
            }

            return (T?)field.GetValue(obj);
        }

        /// Gets the value of the static field on the type
        public T? GetFieldValue<T>(Type type, string name)
        {
            if (GetFieldInfo(type, name, StaticAnyVisibility) is not { } field)
            {
                return default;
            }

            return (T?)field.GetValue(null);
        }

        /// Sets the value of the instance field on the object
        public void SetFieldValue(object obj, string name, object? value)
        {
            if (GetFieldInfo(obj.GetType(), name, InstanceAnyVisibility) is not { } field)
            {
                return;
            }

            field.SetValue(obj, value);
        }

        /// Sets the value of the static field on the type
        public void SetFieldValue(Type type, string name, object? value)
        {
            if (GetFieldInfo(type, name, StaticAnyVisibility) is not { } field)
            {
                return;
            }

            field.SetValue(null, value);
        }

        /// Gets the value of the instance property on the object
        public T? GetPropertyValue<T>(object obj, string name)
        {
            if (GetPropertyInfo(obj.GetType(), name, InstanceAnyVisibility) is not { } property)
            {
                return default;
            }

            if (!property.CanRead)
            {
                LogError($"Property '{name}' on type '{obj.GetType()}' is not readable");
                return default;
            }

            return (T?)property.GetValue(obj);
        }

        /// Gets the value of the static property on the type
        public T? GetPropertyValue<T>(Type type, string name)
        {
            if (GetPropertyInfo(type, name, StaticAnyVisibility) is not { } property)
            {
                return default;
            }

            if (!property.CanRead)
            {
                LogError($"Property '{name}' on type '{type}' is not readable");
                return default;
            }

            return (T?)property.GetValue(null);
        }

        /// Sets the value of the instance property on the object
        public void SetPropertyValue(object obj, string name, object? value)
        {
            if (GetPropertyInfo(obj.GetType(), name, InstanceAnyVisibility) is not { } property)
            {
                return;
            }

            if (!property.CanWrite)
            {
                LogError($"Property '{name}' on type '{obj.GetType()}' is not writable");
                return;
            }

            property.SetValue(obj, value);
        }

        /// Sets the value of the static property on the type
        public void SetPropertyValue(Type type, string name, object? value)
        {
            if (GetPropertyInfo(type, name, StaticAnyVisibility) is not { } property)
            {
                return;
            }

            if (!property.CanWrite)
            {
                LogError($"Property '{name}' on type '{type}' is not writable");
                return;
            }

            property.SetValue(null, value);
        }

        /// Invokes the instance method on the type
        public void InvokeMethod(object obj, string name, params object?[]? parameters)
        {
            if (GetMethodInfo(obj.GetType(), name, parameters?.Select(param => param?.GetType()).ToArray(),
                    InstanceAnyVisibility) is not { } method)
            {
                return;
            }

            method.Invoke(obj, parameters);
        }

        /// Invokes the static method on the type
        public void InvokeMethod(Type type, string name, params object?[]? parameters)
        {
            if (GetMethodInfo(type, name, parameters?.Select(param => param?.GetType()).ToArray(),
                    StaticAnyVisibility) is
                not { } method)
            {
                return;
            }

            method.Invoke(null, parameters);
        }

        /// Invokes the instance method on the type, returning the result
        public T? InvokeMethod<T>(object obj, string name, params object?[]? parameters)
        {
            if (GetMethodInfo(obj.GetType(), name, parameters?.Select(param => param?.GetType()).ToArray(),
                    InstanceAnyVisibility) is not { } method)
            {
                return default;
            }

            return (T?)method.Invoke(obj, parameters);
        }

        /// Invokes the static method on the type, returning the result
        public T? InvokeMethod<T>(Type type, string name, params object?[]? parameters)
        {
            if (GetMethodInfo(type, name, parameters?.Select(param => param?.GetType()).ToArray(),
                    StaticAnyVisibility) is
                not { } method)
            {
                return default;
            }

            return (T?)method.Invoke(null, parameters);
        }

        public string GetFullName(MethodBase method) =>
            global::Celeste.Mods.Tas.Reflection.SourceTasMethodNameFormatting.GetFullName(method);
    }
}
