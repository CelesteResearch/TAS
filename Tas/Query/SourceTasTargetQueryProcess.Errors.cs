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
        /// Value in the result array which is not valid, but was kept in the array to avoid allocations
        internal readonly object InvalidValue = new();

        /// Indicator used to abort query-argument post-processing from an IEnumerable
        internal const string InvalidQueryArgument = "___INVALID___";

        public record QueryError
        {
            public record NoBaseTypes(string Query) : QueryError
            {
                public override string ToString() => $"No base types found for query '{Query}'";
            }

            public record TooManyArguments : QueryError
            {
                public override string ToString() => "Too many arguments specified";
            }

            public record UnknownException(Exception Exception) : QueryError
            {
                public override string ToString() => $"Unknown exception: {Exception}";
            }

            public record Custom(string ErrorMessage) : QueryError
            {
                public override string ToString() => ErrorMessage;
            }

            public record InvalidEnumState(Type EnumType, string Member) : QueryError
            {
                public override string ToString() => $"'{Member}' is not a valid enum state for '{EnumType}'";
            }

            public record InstanceCountMismatch(string Query, int ExpectedCount, int ActualCount) : QueryError
            {
                public override string ToString() => ActualCount == 0
                    ? ExpectedCount == 1
                        ? $"'Query {Query}' returned no values, instead of 1"
                        : $"'Query {Query}' returned no values, instead of a maximum of {ExpectedCount}"
                    : ExpectedCount == 1
                        ? $"'Query {Query}' returned {ActualCount} values, instead of 1"
                        : $"'Query {Query}' returned {ActualCount} values, instead of a maximum of {ExpectedCount}";
            }

            public record ConversionError(string Argument, Type TargetType) : QueryError
            {
                public override string ToString() =>
                    $"Failed to convert argument '{Argument}' into target-type '{TargetType}'";
            }
        }

        public record MemberAccessError(Type type, int memberIndex) : QueryError
        {
            public record NoMembers() : MemberAccessError(typeof(object), -1)
            {
                public override string ToString() => "No members specified";
            }

            public record CodeExecutionNotAllowed : MemberAccessError
            {
                private readonly string[] MemberArgs;

                public CodeExecutionNotAllowed(Type type, int memberIndex, string[] memberArgs) : base(type,
                    memberIndex)
                {
                    MemberArgs = memberArgs;
                }

                public override string ToString() =>
                    $"Cannot safely access member '{MemberArgs[MemberIndex]}' on type '{Type}' during EnforceLegal";
            }

            public new record UnknownException : MemberAccessError
            {
                private readonly string[] MemberArgs;
                private readonly Exception Exception;

                public UnknownException(Type? type, int memberIndex, string[] memberArgs, Exception ex) : base(
                    type ?? typeof(object), memberIndex)
                {
                    MemberArgs = memberArgs;
                    Exception = ex;
                }

                public override string ToString() =>
                    $"Unknown exception while accessing member '{MemberArgs[MemberIndex]}' on type '{Type}': {Exception}";
            }

            public new record Custom : MemberAccessError
            {
                private readonly string ErrorMessage;

                public Custom(Type type, int memberIndex, string errorMessage) : base(type, memberIndex)
                {
                    ErrorMessage = errorMessage;
                }

                public override string ToString() => ErrorMessage;
            }

            public record ReadOnlyCollection : MemberAccessError
            {
                private readonly string[] MemberArgs;

                public ReadOnlyCollection(Type type, int memberIndex, string[] memberArgs) : base(type, memberIndex)
                {
                    MemberArgs = memberArgs;
                }

                public override string ToString() =>
                    $"Cannot modify readonly collection '{MemberArgs[MemberIndex]}', containing value-types, on type '{Type}'";
            }

            public record MemberNotFound : MemberAccessError
            {
                private readonly string[] MemberArgs;
                private readonly BindingFlags BindingFlags;

                public MemberNotFound(Type type, int memberIndex, string[] memberArgs,
                    BindingFlags bindingFlags) : base(
                    type, memberIndex)
                {
                    MemberArgs = memberArgs;
                    BindingFlags = bindingFlags;
                }

                public override string ToString() => BindingFlags switch
                {
                    QueryReflection.InstanceAnyVisibility =>
                        $"Cannot find instance member '{MemberArgs[MemberIndex]}' on type '{Type}'",
                    QueryReflection.StaticAnyVisibility =>
                        $"Cannot find static member '{MemberArgs[MemberIndex]}' on type '{Type}'",
                    QueryReflection.StaticInstanceAnyVisibility =>
                        $"Cannot find instance / static member '{MemberArgs[MemberIndex]}' on type '{Type}'",
                    _ => $"Cannot find member '{MemberArgs[MemberIndex]}' on type '{Type}'",
                };
            }

            public Type Type = type;
            public int MemberIndex = memberIndex;

            public static MemberAccessError Aggregate(MemberAccessError? accum, MemberAccessError error,
                SourceMemberCatalog members)
            {
                if (accum == null)
                {
                    return error;
                }

                // 1. NoMembers
                // 2. CodeExecutionNotAllowed
                // 3. UnknownException
                // 4. Custom
                // 5. ReadOnlyCollection
                // 6. MemberNotFound
                if (accum is NoMembers && error is not NoMembers)
                {
                    return accum;
                }
                else if (error is NoMembers && accum is not NoMembers)
                {
                    return error;
                }

                if (accum is CodeExecutionNotAllowed && error is not CodeExecutionNotAllowed)
                {
                    return accum;
                }
                else if (error is CodeExecutionNotAllowed && accum is not CodeExecutionNotAllowed)
                {
                    return error;
                }

                if (accum is UnknownException && error is not UnknownException)
                {
                    return accum;
                }
                else if (error is UnknownException && accum is not UnknownException)
                {
                    return error;
                }

                if (accum is Custom && error is not Custom)
                {
                    return accum;
                }
                else if (error is Custom && accum is not Custom)
                {
                    return error;
                }

                if (accum is ReadOnlyCollection && error is not ReadOnlyCollection)
                {
                    return accum;
                }
                else if (error is ReadOnlyCollection && accum is not ReadOnlyCollection)
                {
                    return error;
                }

                // Prefer lower member index
                if (accum.MemberIndex < error.MemberIndex)
                {
                    return accum;
                }
                else if (error.MemberIndex < accum.MemberIndex)
                {
                    return error;
                }

                // Resolve common base type
                if (IsAssignableToOriginal(accum.Type, error.Type, members))
                {
                    return error;
                }

                // This will always terminate when ret == typeof(object)
                while (!IsAssignableToOriginal(error.Type, accum.Type, members))
                {
                    if (!members.ContainsSourceType(accum.Type))
                    {
                        accum.Type = accum.Type.BaseType ?? typeof(object);
                        continue;
                    }

                    var definition = members.GetOriginalTypeDefinition(accum.Type);
                    accum.Type = definition.BaseTypeIdentity == null
                        ? typeof(object)
                        : members.GetActualSourceType(definition.BaseAssemblyIdentity, definition.BaseTypeIdentity)
                          ?? throw new TypeLoadException("The original common base type has no qualified actual type.");
                }

                return accum;
            }

            private static bool IsAssignableToOriginal(Type actualType, Type actualTarget, SourceMemberCatalog members)
            {
                if (actualTarget.IsAssignableFrom(actualType)) return true;
                if (!members.ContainsSourceType(actualTarget)) return false;
                for (; actualType != null && !members.ContainsSourceType(actualType); actualType = actualType.BaseType)
                {
                }

                if (actualType == null) return false;
                var target = members.GetOriginalTypeDefinition(actualTarget);
                var current = members.GetOriginalTypeDefinition(actualType);
                while (true)
                {
                    if (current.AssemblyIdentity == target.AssemblyIdentity && current.Identity == target.Identity)
                        return true;
                    if (current.BaseTypeIdentity == null) return false;
                    current = members.GetOriginalTypeDefinition(current.BaseAssemblyIdentity, current.BaseTypeIdentity);
                }
            }
        }

        /// Holder object to allow modifications to value type to be propagated back onto the last reference type
        internal record BoxedValueHolder(object BaseInstance, int Index, Stack<object> ValueStack);
    }
}
