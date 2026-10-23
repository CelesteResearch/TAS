#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Entity = Celeste.Scene.Entities.SceneEntityState;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;
using Session = Celeste.Simulation.Runtime.Session.ChapterCompletionSession;
using LogLevel = Celeste.Everest.EverestLogLevel;
using System.Diagnostics.CodeAnalysis;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Query;
using Celeste.Core.Reflection.Methods;

namespace Celeste.Mods.Tas.Lua
{
    /// Provides helper methods for usage in Lua scripts
    public static class LuaHelpers
    {
        private static TasPlaybackDriver Driver => CelesteTasModule.Instance.Driver;
        private static SourceTasTargetQueryProcess Query => Driver.TargetQuery;

        /// Used to pass a null value from Lua
        public static readonly object? NullValue = new LuaValueHolder<object?>(null);

        /// Gets all members matching the specified target-query
        public static object? Get(string? query)
        {
            if (query == null)
            {
                Driver.Logging.ConsoleLog("No target-query specified", LogLevel.Error);
                return null;
            }

            var result = Query.GetMemberValues(query);
            if (result.Failure)
            {
                Driver.Logging.ConsoleLog(result.Error, LogLevel.Error);
                return null;
            }

            return result.Value.Select(entry => entry.Value).ToArray();
        }

        /// Sets all members matching the specified target-query to the value
        public static void Set(string? query, params string[]? arguments)
        {
            if (query == null)
            {
                Driver.Logging.ConsoleLog("No target-query specified", LogLevel.Error);
                return;
            }

            if (arguments == null)
            {
                Driver.Logging.ConsoleLog("No arguments specified", LogLevel.Error);
                return;
            }

            var result = Query.SetMemberValues(query, arguments);
            if (result.Failure)
            {
                Driver.Logging.ConsoleLog(result.Error, LogLevel.Error);
            }
        }

        /// Invokes all members matching the specified target-query with the arguments
        public static void Invoke(string? query, params string[]? arguments)
        {
            if (query == null)
            {
                Driver.Logging.ConsoleLog("No target-query specified", LogLevel.Error);
                return;
            }

            if (arguments == null)
            {
                Driver.Logging.ConsoleLog("No value specified", LogLevel.Error);
                return;
            }

            var result = Query.InvokeMemberMethods(query, arguments);
            if (result.Failure)
            {
                Driver.Logging.ConsoleLog(result.Error, LogLevel.Error);
            }
        }

        /// Resolves the first entity which matches the specified target-query, e.g. "Player" or "Celeste.Player"
        public static Entity? GetEntity(string targetQuery)
        {
            var result = Query.GetMemberValues(targetQuery);
            if (result.Failure)
            {
                Driver.Logging.ConsoleLog(result.Error.ToString(), LogLevel.Warn);
                return null;
            }

            return (Entity?)result.Value
                .Select(entry => entry.Value)
                .FirstOrDefault(value => value is Entity);
        }

        /// Resolves all entities which match the specified target-query, e.g. "Player" or "Celeste.Player"
        public static List<Entity>? GetEntities(string targetQuery)
        {
            var result = Query.GetMemberValues(targetQuery);
            if (result.Failure)
            {
                Driver.Logging.ConsoleLog(result.Error.ToString(), LogLevel.Warn);
                return null;
            }

            return result.Value
                .Where(entry => entry.Value is Entity)
                .Select(entry => (Entity)entry.Value!)
                .ToList();
        }

        /// Gets the value of a (private) field / property
        public static object? GetValue(object? instanceOrTargetQuery, string memberName)
        {
            if (!TryGetInstance(instanceOrTargetQuery, out var type, out object? instance))
            {
                Driver.Logging.Log($"Failed to get instance for '{instance}", "Lua",
                    Driver.LuaCommands.ConsoleCommandRunning, LogLevel.Error);
                return null;
            }

            try
            {
                if (GetField(type, memberName) is { } fieldInfo)
                {
                    if (fieldInfo.IsStatic)
                    {
                        return fieldInfo.Read(null);
                    }
                    else
                    {
                        return fieldInfo.Read(instance);
                    }
                }

                if (GetProperty(type, memberName) is { } propertyInfo && propertyInfo.CanRead)
                {
                    if (propertyInfo.IsStatic)
                    {
                        return propertyInfo.Read(null);
                    }
                    else
                    {
                        return propertyInfo.Read(instance);
                    }
                }
            }
            catch (Exception e)
            {
                Driver.Logging.LogException(e, $"Failed getting member '{memberName}' on type '{type}'", "Lua",
                    Driver.LuaCommands.ConsoleCommandRunning);
                return null;
            }

            Driver.Logging.Log($"Failed finding member '{memberName}' on type '{type}'", "Lua",
                Driver.LuaCommands.ConsoleCommandRunning, LogLevel.Error);
            return null;
        }

        /// Sets the value of a (private) field / property
        public static void SetValue(object? instanceOrTargetQuery, string memberName, object? value)
        {
            if (!TryGetInstance(instanceOrTargetQuery, out var type, out object? instance))
            {
                Driver.Logging.Log($"Failed to get instance for '{instance}", "Lua",
                    Driver.LuaCommands.ConsoleCommandRunning, LogLevel.Error);
                return;
            }

            try
            {
                if (GetField(type, memberName) is { } fieldInfo)
                {
                    value = ConvertType(value, type, fieldInfo.ValueType);
                    if (fieldInfo.IsStatic)
                    {
                        fieldInfo.Write(null, value);
                    }
                    else
                    {
                        fieldInfo.Write(instance, value);
                    }
                }

                if (GetProperty(type, memberName) is { } propertyInfo && propertyInfo.CanWrite)
                {
                    value = ConvertType(value, type, propertyInfo.ValueType);
                    if (propertyInfo.IsStatic)
                    {
                        propertyInfo.Write(null, value);
                    }
                    else
                    {
                        propertyInfo.Write(instance, value);
                    }
                }
            }
            catch (Exception e)
            {
                Driver.Logging.LogException(e, $"Failed setting member '{memberName}' on type '{type}'", "Lua",
                    Driver.LuaCommands.ConsoleCommandRunning);
                return;
            }

            Driver.Logging.Log($"Failed finding member '{memberName}' on type '{type}'", "Lua",
                Driver.LuaCommands.ConsoleCommandRunning, LogLevel.Error);
        }

        /// Invokes a (private) method
        public static object? InvokeMethod(object? instanceOrTargetQuery, string methodName,
            params object?[] parameters)
        {
            if (!TryGetInstance(instanceOrTargetQuery, out var type, out object? instance))
            {
                Driver.Logging.Log($"Failed to get instance for '{instance}", "Lua",
                    Driver.LuaCommands.ConsoleCommandRunning, LogLevel.Error);
                return null;
            }

            if (instance == null)
            {
                Driver.Logging.Log($"Cannot get value of member '{methodName}' for null-instance", "Lua",
                    Driver.LuaCommands.ConsoleCommandRunning, LogLevel.Error);
                return null;
            }

            if (GetMethod(type, methodName, out var sourceMethod) is { } methodInfo)
            {
                var parameterInfos = methodInfo.GetParameters();
                for (int i = 0; i < parameterInfos.Length; i++)
                {
                    var parameterInfo = parameterInfos[i];
                    if (i < parameters.Length)
                    {
                        parameters[i] = ConvertType(parameters[i], parameters[i]?.GetType(),
                            parameterInfo.ParameterType);
                    }
                    else if (parameterInfo.HasDefaultValue)
                    {
                        Array.Resize(ref parameters, parameters.Length + 1);
                        parameters[i] = parameterInfo.DefaultValue;
                    }
                }

                try
                {
                    return sourceMethod != null
                        ? sourceMethod.Invoke(instance, parameters)
                        : methodInfo.Invoke(instance, parameters);
                }
                catch (Exception e)
                {
                    Driver.Logging.LogException(e, $"Failed invoking method '{methodName}' on type '{type}'", "Lua",
                        Driver.LuaCommands.ConsoleCommandRunning);
                    return null;
                }
            }

            Driver.Logging.Log($"Failed finding method '{methodName}' on type '{type}'", "Lua",
                Driver.LuaCommands.ConsoleCommandRunning, LogLevel.Error);
            return null;
        }

        /// Resolves the enum value for an ordinal or name
        public static object? GetEnum(string enumTargetQuery, object value)
        {
            if (Query.ResolveBaseTypes(enumTargetQuery.Split('.'), out _) is { } types && types.Count != 0 &&
                types.FirstOrDefault(t => t.IsEnum) is { } type)
            {
                if (value is long longValue || long.TryParse(value.ToString(), out longValue))
                {
                    return Enum.ToObject(type, longValue);
                }
                else
                {
                    try
                    {
                        return Enum.Parse(type, value.ToString() ?? string.Empty, true);
                    }
                    catch
                    {
                        Driver.Logging.Log($"Failed finding enum-value '{value}' on type '{type}'", "Lua",
                            Driver.LuaCommands.ConsoleCommandRunning, LogLevel.Error);
                        return null;
                    }
                }
            }

            Driver.Logging.Log($"Failed finding enum '{enumTargetQuery}'", "Lua",
                Driver.LuaCommands.ConsoleCommandRunning, LogLevel.Error);
            return null;
        }

        /// Returns the current level
        public static Level? GetLevel()
        {
            return Driver.Process.Frame.Scenes.CurrentScene switch
            {
                Level level => level,
                Celeste.Game.Execution.LevelLoader.OriginalLevelLoaderScene loader => loader.Level,
                _ => null
            };
        }

        /// Returns the current session
        public static Session? GetSession()
        {
            return SourceTasSceneSessionQuery.GetSession(Driver.Process.Frame.Scenes.CurrentScene);
        }

        /// Casts the value to an int, for usage with setValue / invokeMethod
        public static LuaValueHolder<int> ToInt(long value)
        {
            return new LuaValueHolder<int>((int)value);
        }

        /// Casts the value to a float, for usage with setValue / invokeMethod
        public static LuaValueHolder<float> ToFloat(double value)
        {
            return new LuaValueHolder<float>((float)value);
        }

        private static SourceQueryMemberAccess? GetField(Type type, string name)
        {
            var actual = Query.Reflection.GetFieldInfo(type, name, logFailure: false);
            return actual != null ? SourceQueryMemberAccess.FromRuntime(actual)
                : Query.Members.ContainsSourceType(type) ? Query.GetOriginalField(type, name, logFailure: false) : null;
        }

        private static SourceQueryMemberAccess? GetProperty(Type type, string name)
        {
            var actual = Query.Reflection.GetPropertyInfo(type, name, logFailure: false);
            return actual != null ? SourceQueryMemberAccess.FromRuntime(actual)
                : Query.Members.ContainsSourceType(type) ? Query.GetOriginalProperty(type, name, logFailure: false)
                : null;
        }

        private static MethodInfo? GetMethod(Type type, string name, out SourceOrderedMethod? sourceMethod)
        {
            sourceMethod = null;
            var actual = Query.Reflection.GetMethodInfo(type, name);
            if (actual != null || !Query.Members.ContainsSourceType(type)) return actual;
            sourceMethod = Query.OriginalMethods.GetMethodInfo(Query.Members, type, name);
            return sourceMethod?.ActualMethod;
        }

        private static bool TryGetInstance(object? instanceOrTargetQuery, [NotNullWhen(true)] out Type? type,
            out object? instance)
        {
            if (instanceOrTargetQuery is string targetQuery)
            {
                if (Query.ResolveBaseTypes(targetQuery.Split('.'), out _) is { } types && types.Count != 0)
                {
                    type = types.First();
                    instance = Query.ResolveTypeInstances(type).FirstOrDefault();
                    return true;
                }
                else
                {
                    type = null;
                    instance = null;
                    return false;
                }
            }
            else
            {
                type = instanceOrTargetQuery?.GetType()!;
                instance = instanceOrTargetQuery;
                return true;
            }
        }

        /// Tries to convert the value to the target type
        private static object? ConvertType(object? value, Type? valueType, Type type)
        {
            switch (value)
            {
                case LuaValueHolder<int> intHolder:
                    return intHolder.Value;
                case LuaValueHolder<float> floatHolder:
                    return floatHolder.Value;
                case LuaValueHolder<object> objectHolder:
                    return objectHolder.Value;
            }

            if (valueType != null && (type == valueType || type.IsSubclassOf(valueType)))
            {
                return value;
            }

            try
            {
                if (value is null)
                {
                    return type.IsValueType ? Activator.CreateInstance(type) : null;
                }
                else
                {
                    return type.IsEnum ? Enum.Parse(type, (string)value, true) : Convert.ChangeType(value, type);
                }
            }
            catch
            {
                return value;
            }
        }
    }
}
