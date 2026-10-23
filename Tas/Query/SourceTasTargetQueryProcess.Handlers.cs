using Celeste.Core.Reflection.Members;
using JetBrains.Annotations;
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
        internal enum Variant
        {
            Get,
            Set,
            Invoke
        }

        /// Handler to provide support for Celeste-specific special cases
        internal abstract class Handler
        {
            protected readonly SourceTasTargetQueryProcess Query;
            protected QueryReflection Reflection => Query.Reflection;
            protected Handler(SourceTasTargetQueryProcess query) => Query = query;
            public virtual bool CanResolveInstances(Type type) => false;
            public virtual bool CanResolveValue(Type type) => false;

            public virtual (bool CanEnumerate, bool ShouldOverride) CanEnumerateMemberEntries(Type type,
                Variant variant,
                string queryPrefix, int memberIdx, string[] memberArgs)
                => (CanEnumerate: false, ShouldOverride: false);

            public virtual bool CanEnumerateTypeEntries(Type type) => false;

            /// Provide currently active instances for the specified type. <br/>
            /// Only invoked if <see cref="CanResolveInstances"/> returned <c>true</c> for the type.
            public virtual object[] ResolveInstances(Type type) => [];

            /// Can process the query arguments into special members, to allow for custom syntax
            public virtual IEnumerable<string>
                ProcessQueryArguments(IEnumerable<string> queryArgs, bool isAutoComplete) =>
                queryArgs;

            /// Should format the query arguments back into their original custom syntax form
            public virtual IEnumerable<string> FormatQueryArguments(IEnumerable<string> queryArgs) => queryArgs;

            /// Resolve a set of base target-types for a query. <br/>
            /// <c>null</c> should be returned when no base-types could be resolved.
            public virtual (HashSet<Type> Types, string[] MemberArgs)? ResolveBaseTypes(string[] queryArgs) => null;

            /// Attempts to resolve the next member value for all current objects in parallel. <br/>
            /// <c>true</c> should be returned when the handler could resolve the next member, otherwise <c>false</c>.
            public virtual Result<bool, QueryError> ResolveMemberValues(ref object?[] values, ref int memberIdx,
                string[] memberArgs)
            {
                return Result<bool, QueryError>.Ok(false);
            }

            /// Attempts to resolve the next member for a single value. <br/>
            /// <c>true</c> should be returned when the handler could resolve the next member, otherwise <c>false</c>.
            public virtual Result<bool, MemberAccessError> ResolveMember(object? instance, out object? value, Type type,
                int memberIdx, string[] memberArgs)
            {
                value = null;
                return Result<bool, MemberAccessError>.Ok(false);
            }

            /// Attempts to process the new value for the current instance slot. <br/>
            /// <c>true</c> should be returned when the handler could process the value, otherwise <c>false</c>.
            public virtual Result<bool, MemberAccessError> ProcessValue(ref object?[] values, int valueIdx,
                object? value,
                Type currentType, ref int memberIdx, string[] memberArgs, bool needsFlush)
            {
                return Result<bool, MemberAccessError>.Ok(false);
            }

            /// Attempts to resolve the target types of the parameters for the next member. <br/>
            /// <c>true</c> should be returned when the handler could resolve the target types, otherwise <c>false</c>.
            public virtual Result<bool, MemberAccessError> ResolveTargetTypes(out Type[] targetTypes, Type type,
                ref int memberIdx, string[] memberArgs)
            {
                targetTypes = [];
                return Result<bool, MemberAccessError>.Ok(false);
            }

            /// Attempts to set the value of the next member to the target value. <br/>
            /// <c>true</c> should be returned when the handler could set the next member, otherwise <c>false</c>.
            public virtual Result<bool, MemberAccessError> SetMember(object? instance, object? value, Type type,
                int memberIdx, string[] memberArgs, bool forceAllowCodeExecution)
            {
                return Result<bool, MemberAccessError>.Ok(false);
            }

            /// Attempts to invoke the next member with the target parameter values. <br/>
            /// <c>true</c> should be returned when the handler could invoke the next member, otherwise <c>false</c>.
            public virtual Result<bool, MemberAccessError> InvokeMember(object? instance, object?[] parameterValue,
                Type type, int memberIdx, string[] memberArgs, bool forceAllowCodeExecution)
            {
                return Result<bool, MemberAccessError>.Ok(false);
            }

            /// Attempts to resolve a value from a string for the target type. <br/>
            /// Only invoked if <see cref="CanResolveValue"/> returned <c>true</c> for the type. <br/>
            /// <c>true</c> should be returned when the handler could a value, otherwise <c>false</c>.
            public virtual Result<bool, QueryError> ResolveValue(Type targetType, ref int argIdx, string[] valueArgs,
                out object? value)
            {
                value = null;
                return Result<bool, QueryError>.Ok(false);
            }

            /// Should mark the type as a "suggested option" if applicable in the current context
            public virtual bool IsTypeSuggested(Type type, Variant variant)
            {
                return false;
            }

            /// Should mark the member as a "suggested option" if applicable in the current context
            public virtual bool IsMemberSuggested(SourceQueryMemberIdentity member, Variant variant)
            {
                return false;
            }

            /// Provide a list of auto-complete entries which should be listed along base-types.
            [MustDisposeResource]
            public virtual IEnumerator<CommandAutoCompleteEntry> ProvideGlobalEntries(string[] queryArgs,
                string queryPrefix, Variant variant)
            {
                yield break;
            }

            /// Overwrite the list of auto-complete entries which are provided for the members of the type
            /// Only invoked if <see cref="CanEnumerateMemberEntries"/> returned <c>true</c> for the type. <br/>
            [MustDisposeResource]
            public virtual IEnumerator<CommandAutoCompleteEntry> EnumerateMemberEntries(Type type, Variant variant,
                string queryPrefix, int memberIdx, string[] memberArgs)
            {
                yield break;
            }

            /// Overwrite the list of auto-complete entries which are provided for the values of the type
            /// Only invoked if <see cref="CanEnumerateTypeEntries"/> returned <c>true</c> for the type. <br/>
            [MustDisposeResource]
            public virtual IEnumerator<CommandAutoCompleteEntry> EnumerateTypeEntries(Type type)
            {
                yield break;
            }
        }
    }
}
