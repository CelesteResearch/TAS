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
        /// Parses the first part of a query into types and an optional EntityID
        public HashSet<Type> ResolveBaseTypes(string[] queryArgs, out string[] memberArgs, bool processArgs = true)
        {
            if (queryArgs.Length == 0)
            {
                memberArgs = queryArgs;
                return [];
            }

            if (processArgs)
            {
                queryArgs = Handlers.Aggregate((IEnumerable<string>)queryArgs,
                    (current, handler) => handler.ProcessQueryArguments(current, isAutoComplete: false)).ToArray();
            }

            if (queryArgs.Any(arg => arg == InvalidQueryArgument))
            {
                memberArgs = queryArgs;
                return [];
            }

            string fullQueryArgs = string.Join('.', queryArgs);
            if (BaseTypeCache.TryGetValue(fullQueryArgs, out var cache))
            {
                memberArgs = cache.MemberArgs;
                return cache.Types;
            }

            foreach (var handler in Handlers)
            {
                if (handler.ResolveBaseTypes(queryArgs) is { } result)
                {
                    BaseTypeCache[fullQueryArgs] = result;

                    memberArgs = result.MemberArgs;
                    return result.Types;
                }
            }

            return ParseGenericBaseTypes(queryArgs, out memberArgs);
        }

        /// Parses query-arguments into a list of types, while only searching for generic .NET types
        /// Does not reference any defined special-case handlers
        internal HashSet<Type> ParseGenericBaseTypes(string[] queryArgs, out string[] memberArgs)
        {
            string fullQueryArgs = string.Join('.', queryArgs);

            if (BaseTypeCache.TryGetValue(fullQueryArgs, out var cache))
            {
                memberArgs = cache.MemberArgs;
                return cache.Types;
            }

            // Decrease query arguments until a match is found
            for (int i = queryArgs.Length; i > 0; i--)
            {
                bool isFirst = i == queryArgs.Length;
                string typeName = isFirst
                    ? fullQueryArgs
                    : string.Join('.', queryArgs, startIndex: 0, count: i);

                if (!isFirst && BaseTypeCache.TryGetValue(typeName, out cache) && cache.MemberArgs.Length == 0)
                {
                    memberArgs = queryArgs[i..];
                    return cache.Types;
                }

                if (AllTypes.TryGetValue(typeName, out var sourceDefinitions))
                {
                    var types = sourceDefinitions.ToHashSet();
                    memberArgs = queryArgs[i..];

                    BaseTypeCache[typeName] = (types, []);
                    BaseTypeCache[fullQueryArgs] = (types, memberArgs);
                    return types;
                }
            }

            // No matching type found
            memberArgs = queryArgs;
            return [];
        }

        /// Resolves a type into all applicable instances of it
        /// Returns null for types which are always in a static context
        public object[] ResolveTypeInstances(Type type)
        {
            if (Handlers.FirstOrDefault(h => h.CanResolveInstances(type)) is { } handler)
            {
                return handler.ResolveInstances(type);
            }

            // No instances available
            return [type];
        }
    }
}
