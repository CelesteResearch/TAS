using Celeste.Core.Reflection.Methods;
using Celeste.Core.Reflection.Members;
using Celeste.Mods.Tas.Tools;
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
        private void ResolveTargetObject(object instance, out object targetObject, out Type targetType)
        {
            targetObject = (instance as BoxedValueHolder)?.ValueStack.Peek() ?? instance;
            targetType = targetObject as Type ?? targetObject.GetType();
        }

        /// Evaluates the member arguments on the base-instances and gets the specified value(s)
        internal Result<object?[], QueryError> GetMemberValue(object instance, string[] memberArgs,
            bool forceAllowCodeExecution = false)
            => ResolveMemberValue(instance, memberArgs, forceAllowCodeExecution, needsFlush: false);

        /// Evaluates the member arguments on the base-instances and prepares the specified value(s)
        /// The values can then be modified with SetMemberValue
        internal Result<object?[], QueryError> PrepareMemberValue(object instance, string[] memberArgs,
            bool forceAllowCodeExecution = false)
            => ResolveMemberValue(instance, memberArgs, forceAllowCodeExecution, needsFlush: true);

        private Result<object?[], QueryError> ResolveMemberValue(object instance, string[] memberArgs,
            bool forceAllowCodeExecution, bool needsFlush)
        {
            object?[] values = [instance];

            for (int memberIdx = 0; memberIdx < memberArgs.Length; memberIdx++)
            {
                string member = memberArgs[memberIdx];
                bool isFinal = memberIdx == memberArgs.Length - 1 && !needsFlush;

                var bindingFlags = memberIdx == 0
                    ? instance is Type
                        ? QueryReflection.StaticAnyVisibility
                        : QueryReflection.StaticInstanceAnyVisibility
                    : QueryReflection.InstanceAnyVisibility;

                // Forward to handlers
                try
                {
                    foreach (var handler in Handlers)
                    {
                        var result = handler.ResolveMemberValues(ref values, ref memberIdx, memberArgs);
                        if (result.Success && result.Value)
                        {
                            goto NextMember;
                        }

                        if (result.Failure)
                        {
                            return Result<object?[], QueryError>.Fail(result.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Result<object?[], QueryError>.Fail(
                        new MemberAccessError.UnknownException(null, memberIdx, memberArgs, ex));
                }

                // Resolve members individually
                for (int valueIdx = values.Length - 1; valueIdx >= 0; valueIdx--)
                {
                    if (values[valueIdx] == null || values[valueIdx] == InvalidValue || values[valueIdx] is QueryError)
                    {
                        continue;
                    }

                    ResolveTargetObject(values[valueIdx]!, out object currentValue, out var currentType);

                    try
                    {
                        foreach (var handler in Handlers)
                        {
                            var result = handler.ResolveMember(currentValue, out object? value, currentType, memberIdx,
                                memberArgs);
                            if (result.Success && result.Value)
                            {
                                ProcessValue(ref values, valueIdx, value, currentType, ref memberIdx, memberArgs,
                                    needsFlush);
                                goto NextValue;
                            }

                            if (result.Failure)
                            {
                                values[valueIdx] = result.Error;
                                goto NextValue;
                            }
                        }

                        if (GetOriginalField(currentType, member, bindingFlags, logFailure: false) is { } field &&
                            IsFieldUsable(field, needsFlush ? Variant.Set : Variant.Get, isFinal))
                        {
                            if (field.IsStatic)
                            {
                                ProcessValue(ref values, valueIdx, field.Read(null), currentType, ref memberIdx,
                                    memberArgs,
                                    needsFlush);

                                // Invalidate all other instances of this type to avoid duplicates
                                InvalidateValues(ref values, valueIdx, currentType);
                            }
                            else
                            {
                                ProcessValue(ref values, valueIdx, field.Read(currentValue), currentType, ref memberIdx,
                                    memberArgs, needsFlush);
                            }

                            continue;
                        }

                        if (GetOriginalProperty(currentType, member, bindingFlags, logFailure: false) is { } property &&
                            IsPropertyUsable(property, needsFlush ? Variant.Set : Variant.Get, isFinal))
                        {
                            if (PreventCodeExecution && !forceAllowCodeExecution)
                            {
                                values[valueIdx] =
                                    new MemberAccessError.CodeExecutionNotAllowed(currentType, memberIdx, memberArgs);

                                // Invalidate all other instances of this type to avoid duplicates
                                InvalidateValues(ref values, valueIdx, currentType);
                                continue;
                            }

                            if (property.IsStatic)
                            {
                                ProcessValue(ref values, valueIdx, property.Read(null), currentType, ref memberIdx,
                                    memberArgs, needsFlush);

                                // Invalidate all other instances of this type to avoid duplicates
                                InvalidateValues(ref values, valueIdx, currentType);
                            }
                            else
                            {
                                ProcessValue(ref values, valueIdx, property.Read(currentValue), currentType,
                                    ref memberIdx,
                                    memberArgs, needsFlush);
                            }

                            continue;
                        }

                        values[valueIdx] =
                            new MemberAccessError.MemberNotFound(currentType, memberIdx, memberArgs, bindingFlags);

                        // Invalidate all other instances of this type to avoid duplicates
                        InvalidateValues(ref values, valueIdx, currentType);

                        NextValue: ;
                    }
                    catch (Exception ex)
                    {
                        values[valueIdx] =
                            new MemberAccessError.UnknownException(currentType, memberIdx, memberArgs, ex);

                        // Invalidate all other instances of this type to avoid duplicates
                        InvalidateValues(ref values, valueIdx, currentType);
                    }
                }

                NextMember: ;
            }

            if (values.Any(value => value is not MemberAccessError))
            {
                return Result<object?[], QueryError>.Ok(values);
            }

            // No valid results. Aggregate errors
            return Result<object?[], QueryError>.Fail((MemberAccessError)values.Aggregate((accum, error) =>
            {
                return MemberAccessError.Aggregate((MemberAccessError)accum!, (MemberAccessError)error!, Members);
            })!);

            void InvalidateValues(ref object?[] values, int valueIdx, Type currentType)
            {
                for (int otherValueIdx = valueIdx - 1; otherValueIdx >= 0; otherValueIdx--)
                {
                    if (values[otherValueIdx]?.GetType() == currentType)
                    {
                        values[otherValueIdx] = InvalidValue;
                    }
                }
            }

            void ProcessValue(ref object?[] values, int valueIdx, object? value, Type currentType, ref int memberIdx,
                string[] memberArgs, bool needsFlush)
            {
                foreach (var handler in Handlers)
                {
                    var result = handler.ProcessValue(ref values, valueIdx, value, currentType, ref memberIdx,
                        memberArgs,
                        needsFlush);
                    if (result.Success && result.Value)
                    {
                        return;
                    }

                    if (result.Failure)
                    {
                        values[valueIdx] = result.Error;
                        return;
                    }
                }

                if (needsFlush && value != null && value.GetType().IsValueType)
                {
                    if (values[valueIdx] is BoxedValueHolder holder)
                    {
                        holder.ValueStack.Push(value);
                    }
                    else
                    {
                        holder = new BoxedValueHolder(values[valueIdx]!, -1, new(capacity: 1));
                        holder.ValueStack.Push(value);
                        values[valueIdx] = holder;
                    }
                }
                else
                {
                    values[valueIdx] = value;
                }
            }
        }

        internal VoidResult<MemberAccessError> SetMember(object targetObject, object? value, string[] memberArgs,
            bool forceAllowCodeExecution = false)
        {
            ResolveTargetObject(targetObject, out object target, out var targetType);

            try
            {
                string member = memberArgs[^1];

                var bindingFlags = memberArgs.Length == 1
                    ? target is Type
                        ? QueryReflection.StaticAnyVisibility
                        : QueryReflection.StaticInstanceAnyVisibility
                    : QueryReflection.InstanceAnyVisibility;

                foreach (var handler in Handlers)
                {
                    var result = handler.SetMember(target, value, targetType, memberArgs.Length - 1, memberArgs,
                        forceAllowCodeExecution);
                    if (result.Success && result.Value)
                    {
                        goto PropagateValueStack;
                    }

                    if (result.Failure)
                    {
                        return VoidResult<MemberAccessError>.Fail(result.Error);
                    }
                }

                if (GetOriginalField(targetType, member, bindingFlags, logFailure: false) is { } field &&
                    IsFieldUsable(field, Variant.Set, isFinal: true))
                {
                    if (field.IsStatic)
                    {
                        field.Write(null, value);
                    }
                    else
                    {
                        field.Write(target, value);
                    }

                    goto PropagateValueStack;
                }
                else if (GetOriginalProperty(targetType, member, bindingFlags, logFailure: false) is { } property &&
                         IsPropertyUsable(property, Variant.Set, isFinal: true))
                {
                    if (PreventCodeExecution && !forceAllowCodeExecution)
                    {
                        return VoidResult<MemberAccessError>.Fail(
                            new MemberAccessError.CodeExecutionNotAllowed(targetType, memberArgs.Length - 1,
                                memberArgs));
                    }

                    if (property.IsStatic)
                    {
                        property.Write(null, value);
                    }
                    else
                    {
                        property.Write(target, value);
                    }

                    goto PropagateValueStack;
                }

                return VoidResult<MemberAccessError>.Fail(
                    new MemberAccessError.MemberNotFound(targetType, memberArgs.Length - 1, memberArgs, bindingFlags));
            }
            catch (Exception ex)
            {
                return VoidResult<MemberAccessError>.Fail(
                    new MemberAccessError.UnknownException(targetType, memberArgs.Length - 1, memberArgs, ex));
            }

            PropagateValueStack:
            if (targetObject is BoxedValueHolder holder)
            {
                return PropagateValueStack(holder, memberArgs, forceAllowCodeExecution);
            }

            return VoidResult<MemberAccessError>.Ok;
        }

        internal VoidResult<MemberAccessError> InvokeMember(object targetObject, object?[] parameterValues,
            string[] memberArgs, bool forceAllowCodeExecution = false)
        {
            ResolveTargetObject(targetObject, out object target, out var targetType);

            if (PreventCodeExecution && !forceAllowCodeExecution)
            {
                return VoidResult<MemberAccessError>.Fail(
                    new MemberAccessError.CodeExecutionNotAllowed(targetType, memberArgs.Length - 1, memberArgs));
            }

            try
            {
                string member = memberArgs[^1];

                var bindingFlags = memberArgs.Length == 1
                    ? target is Type
                        ? QueryReflection.StaticAnyVisibility
                        : QueryReflection.StaticInstanceAnyVisibility
                    : QueryReflection.InstanceAnyVisibility;

                foreach (var handler in Handlers)
                {
                    var result = handler.InvokeMember(target, parameterValues, targetType, memberArgs.Length - 1,
                        memberArgs, forceAllowCodeExecution);
                    if (result.Success && result.Value)
                    {
                        goto PropagateValueStack;
                    }

                    if (result.Failure)
                    {
                        return VoidResult<MemberAccessError>.Fail(result.Error);
                    }
                }

                if (GetOriginalField(targetType, member, bindingFlags, logFailure: false) is { } field &&
                    field.ValueType.IsSameOrSubclassOf(typeof(Delegate)))
                {
                    var del = (Delegate?)(field.IsStatic ? field.Read(null) : field.Read(target));
                    del?.DynamicInvoke(parameterValues);

                    goto PropagateValueStack;
                }
                else if (GetOriginalProperty(targetType, member, bindingFlags, logFailure: false) is { } property &&
                         property.ValueType.IsSameOrSubclassOf(typeof(Delegate)))
                {
                    var del = (Delegate?)(property.IsStatic ? property.Read(null) : property.Read(target));
                    del?.DynamicInvoke(parameterValues);

                    goto PropagateValueStack;
                }
                else if (ResolveMethod(targetType, member, bindingFlags, parameterValues.Length) is
                         { Success: true, Value: { } method })
                {
                    if (method.OriginalIsStatic)
                    {
                        method.Invoke(null, parameterValues);
                    }
                    else
                    {
                        method.Invoke(target, parameterValues);
                    }

                    goto PropagateValueStack;
                }

                return VoidResult<MemberAccessError>.Fail(
                    new MemberAccessError.MemberNotFound(targetType, memberArgs.Length - 1, memberArgs, bindingFlags));
            }
            catch (Exception ex)
            {
                return VoidResult<MemberAccessError>.Fail(
                    new MemberAccessError.UnknownException(targetType, memberArgs.Length - 1, memberArgs, ex));
            }

            PropagateValueStack:
            if (targetObject is BoxedValueHolder holder)
            {
                return PropagateValueStack(holder, memberArgs, forceAllowCodeExecution);
            }

            return VoidResult<MemberAccessError>.Ok;
        }

        private VoidResult<MemberAccessError> PropagateValueStack(BoxedValueHolder holder, string[] memberArgs,
            bool forceAllowCodeExecution)
        {
            // Propagate value-type stack
            object currentValue = holder.ValueStack.Pop();

            int memberIdx = memberArgs.Length - 2;
            while (holder.ValueStack.TryPop(out object? currentTarget))
            {
                var currentTargetType = currentTarget as Type ?? currentTarget.GetType();
                string member = memberArgs[memberIdx];

                try
                {
                    foreach (var handler in Handlers)
                    {
                        var result = handler.SetMember(currentTarget, currentValue, currentTargetType, memberIdx,
                            memberArgs, forceAllowCodeExecution);
                        if (result.Success && result.Value)
                        {
                            goto NextValue;
                        }

                        if (result.Failure)
                        {
                            return VoidResult<MemberAccessError>.Fail(result.Error);
                        }
                    }

                    if (GetOriginalField(currentTargetType, member, QueryReflection.InstanceAnyVisibility,
                            logFailure: false) is { } field)
                    {
                        if (field.IsStatic)
                        {
                            field.Write(null, currentValue);
                        }
                        else
                        {
                            field.Write(currentTarget, currentValue);
                        }
                    }
                    else if (GetOriginalProperty(currentTargetType, member, QueryReflection.InstanceAnyVisibility,
                                 logFailure: false) is { } property)
                    {
                        if (PreventCodeExecution && !forceAllowCodeExecution)
                        {
                            return VoidResult<MemberAccessError>.Fail(
                                new MemberAccessError.CodeExecutionNotAllowed(currentTargetType, memberIdx,
                                    memberArgs));
                        }

                        if (property.IsStatic)
                        {
                            property.Write(null, currentValue);
                        }
                        else
                        {
                            property.Write(currentTarget, currentValue);
                        }
                    }

                    NextValue: ;
                }
                catch (Exception ex)
                {
                    return VoidResult<MemberAccessError>.Fail(
                        new MemberAccessError.UnknownException(currentTargetType, memberIdx, memberArgs, ex));
                }

                currentValue = currentTarget;
                memberIdx--;
            }

            if (holder.Index < 0)
            {
                // Regular base instance
                var baseTargetType = holder.BaseInstance as Type ?? holder.BaseInstance.GetType();
                string member = memberArgs[memberIdx];

                var bindingFlags = memberIdx == 0
                    ? holder.BaseInstance is Type
                        ? QueryReflection.StaticAnyVisibility
                        : QueryReflection.StaticInstanceAnyVisibility
                    : QueryReflection.InstanceAnyVisibility;

                try
                {
                    foreach (var handler in Handlers)
                    {
                        var result = handler.SetMember(holder.BaseInstance, currentValue, baseTargetType, memberIdx,
                            memberArgs, forceAllowCodeExecution);
                        if (result.Success && result.Value)
                        {
                            return VoidResult<MemberAccessError>.Ok;
                        }

                        if (result.Failure)
                        {
                            return VoidResult<MemberAccessError>.Fail(result.Error);
                        }
                    }

                    if (GetOriginalField(baseTargetType, member, bindingFlags, logFailure: false) is { } field)
                    {
                        if (field.IsStatic)
                        {
                            field.Write(null, currentValue);
                        }
                        else
                        {
                            field.Write(holder.BaseInstance, currentValue);
                        }
                    }
                    else if (GetOriginalProperty(baseTargetType, member, bindingFlags, logFailure: false) is
                             { } property)
                    {
                        if (PreventCodeExecution && !forceAllowCodeExecution)
                        {
                            return VoidResult<MemberAccessError>.Fail(
                                new MemberAccessError.CodeExecutionNotAllowed(baseTargetType, memberIdx, memberArgs));
                        }

                        if (property.IsStatic)
                        {
                            property.Write(null, currentValue);
                        }
                        else
                        {
                            property.Write(holder.BaseInstance, currentValue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return VoidResult<MemberAccessError>.Fail(
                        new MemberAccessError.UnknownException(baseTargetType, memberIdx, memberArgs, ex));
                }
            }
            else
            {
                // List base instance
                var baseList = (IList)holder.BaseInstance;
                baseList[holder.Index] = currentValue;
            }

            return VoidResult<MemberAccessError>.Ok;
        }

        private Result<SourceQueryMethodAccess?, string> ResolveMethod(Type type, string member,
            BindingFlags bindingFlags,
            int argumentCount)
        {
            try
            {
                if (GetQueryMethod(type, member, bindingFlags) is
                    { } method)
                {
                    return Result<SourceQueryMethodAccess?, string>.Ok(method);
                }

                return Result<SourceQueryMethodAccess?, string>.Ok(null);
            }
            catch (AmbiguousMatchException)
            {
                var methods = GetQueryMethods(type, QueryReflection.StaticInstanceAnyVisibility)
                    .Where(method => method.Name == member &&
                                     method.ParameterCount == argumentCount)
                    .ToArray();

                switch (methods.Length)
                {
                    case 1:
                    {
                        return Result<SourceQueryMethodAccess?, string>.Ok(methods[0]);
                    }

                    case 0:
                    {
                        var builder =
                            new StringBuilder(
                                $"No overload of method '{member}' on type '{type}' has {argumentCount} argument(s). Found");
                        foreach (var candidate in GetQueryMethods(type,
                                     BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).Where(method =>
                                     method.Name == member))
                        {
                            builder.AppendLine($"- {candidate.Signature}");
                        }

                        return Result<SourceQueryMethodAccess?, string>.Fail(builder.ToString());
                    }
                    default:
                    {
                        var builder =
                            new StringBuilder(
                                $"Ambiguous overload of method '{member}' on type '{type}' with {argumentCount} argument(s). Found");
                        foreach (var candidate in GetQueryMethods(type,
                                     BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).Where(method =>
                                     method.Name == member))
                        {
                            builder.AppendLine($"- {candidate.Signature}");
                        }

                        return Result<SourceQueryMethodAccess?, string>.Fail(builder.ToString());
                    }
                }
            }
        }

        internal Result<SourceQueryValueType[], MemberAccessError> ResolveMemberTargetTypes(object instanceObject,
            int memberIdx, string[] memberArgs, Variant variant, int argumentCount,
            bool forceAllowCodeExecution = false)
        {
            ResolveTargetObject(instanceObject, out object instance, out var type);

            string member = memberArgs[memberIdx];
            bool isFinal = memberIdx == memberArgs.Length - 1;

            var bindingFlags = memberIdx == 0
                ? instance is Type
                    ? QueryReflection.StaticAnyVisibility
                    : QueryReflection.StaticInstanceAnyVisibility
                : QueryReflection.InstanceAnyVisibility;

            try
            {
                foreach (var handler in Handlers)
                {
                    var result = handler.ResolveTargetTypes(out var targetTypes, type, ref memberIdx, memberArgs);
                    if (result.Success && result.Value)
                    {
                        return Result<SourceQueryValueType[], MemberAccessError>.Ok(targetTypes
                            .Select(t => new SourceQueryValueType(t)).ToArray());
                    }

                    if (result.Failure)
                    {
                        return Result<SourceQueryValueType[], MemberAccessError>.Fail(result.Error);
                    }
                }

                if (GetOriginalField(type, member, bindingFlags, logFailure: false) is { } field &&
                    IsFieldUsable(field, variant, isFinal))
                {
                    if (field.ValueType.IsSameOrSubclassOf(typeof(Delegate)))
                    {
                        if (PreventCodeExecution && !forceAllowCodeExecution)
                        {
                            return Result<SourceQueryValueType[], MemberAccessError>.Fail(
                                new MemberAccessError.CodeExecutionNotAllowed(type, memberArgs.Length - 1, memberArgs));
                        }

                        if ((field.IsStatic ? field.Read(null) : field.Read(instance)) is Delegate del)
                        {
                            return Result<SourceQueryValueType[], MemberAccessError>.Ok(del.Method.GetParameters()
                                .Select(p => new SourceQueryValueType(p.ParameterType)).ToArray());
                        }
                    }
                    else
                    {
                        return Result<SourceQueryValueType[], MemberAccessError>.Ok([
                            field.QueryValueType
                        ]);
                    }
                }

                if (GetOriginalProperty(type, member, bindingFlags, logFailure: false) is { } property &&
                    IsPropertyUsable(property, variant, isFinal))
                {
                    if (property.ValueType.IsSameOrSubclassOf(typeof(Delegate)))
                    {
                        if (PreventCodeExecution && !forceAllowCodeExecution)
                        {
                            return Result<SourceQueryValueType[], MemberAccessError>.Fail(
                                new MemberAccessError.CodeExecutionNotAllowed(type, memberArgs.Length - 1, memberArgs));
                        }

                        if ((property.IsStatic ? property.Read(null) : property.Read(instance)) is Delegate del)
                        {
                            return Result<SourceQueryValueType[], MemberAccessError>.Ok(del.Method.GetParameters()
                                .Select(p => new SourceQueryValueType(p.ParameterType)).ToArray());
                        }
                    }
                    else
                    {
                        return Result<SourceQueryValueType[], MemberAccessError>.Ok([
                            property.QueryValueType
                        ]);
                    }
                }


                var methodResult = ResolveMethod(type, member, bindingFlags, argumentCount);
                if (!methodResult.Success)
                {
                    return Result<SourceQueryValueType[], MemberAccessError>.Fail(
                        new MemberAccessError.Custom(type, memberIdx, methodResult.Error));
                }

                if (methodResult.Value is { } method && IsMethodUsable(method, variant, isFinal))
                {
                    return Result<SourceQueryValueType[], MemberAccessError>.Ok(method.ParameterTypes);
                }

                return Result<SourceQueryValueType[], MemberAccessError>.Fail(
                    new MemberAccessError.MemberNotFound(type, memberIdx, memberArgs, bindingFlags));
            }
            catch (Exception ex)
            {
                return Result<SourceQueryValueType[], MemberAccessError>.Fail(
                    new MemberAccessError.UnknownException(type, memberIdx, memberArgs, ex));
            }
        }

        internal Result<object?[], QueryError> ResolveValue(string[] valueArgs, Type[] targetTypes)
            => ResolveValue(valueArgs, targetTypes.Select(t => new SourceQueryValueType(t)).ToArray());

        internal Result<object?[], QueryError> ResolveValue(string[] valueArgs, SourceQueryValueType[] targetTypes)
        {
            object?[] values = new object?[targetTypes.Length];
            int valueIdx = 0;

            for (int argIdx = 0; argIdx < valueArgs.Length; argIdx++)
            {
                if (valueIdx >= values.Length)
                {
                    return Result<object?[], QueryError>.Fail(new QueryError.TooManyArguments());
                }

                string arg = valueArgs[argIdx];

                var valueType = targetTypes[valueIdx];
                var targetType = valueType.PhysicalType;
                targetType = Nullable.GetUnderlyingType(targetType) ?? targetType;

                try
                {
                    foreach (var handler in Handlers.Where(handler => handler.CanResolveValue(targetType)))
                    {
                        var result = handler.ResolveValue(targetType, ref argIdx, valueArgs, out object? value);
                        if (result.Success && result.Value)
                        {
                            values[valueIdx++] = value;
                            goto NextArg;
                        }

                        if (result.Failure)
                        {
                            return Result<object?[], QueryError>.Fail(result.Error);
                        }
                    }

                    // Attempt to evaluate as a target-query
                    var queryResult = GetMemberValues(arg);
                    if (queryResult.Success)
                    {
                        if (queryResult.Value.Count == 0 || queryResult.Value.Count > values.Length - valueIdx)
                        {
                            return Result<object?[], QueryError>.Fail(
                                new QueryError.InstanceCountMismatch(arg, values.Length - valueIdx,
                                    queryResult.Value.Count));
                        }

                        foreach ((object? _, object? value) in queryResult.Value)
                        {
                            var coerceResult = Reflection.CoerceTo(value, targetTypes[valueIdx].PhysicalType);
                            if (coerceResult.Failure)
                            {
                                return Result<object?[], QueryError>.Fail(new QueryError.Custom(coerceResult.Error));
                            }

                            values[valueIdx++] = coerceResult.Value;
                        }

                        continue;
                    }

                    if (FindOriginalEnum(valueType) is { } originalEnum)
                    {
                        if (!TryParseOriginalEnum(originalEnum, targetType, arg, out object value))
                        {
                            return Result<object?[], QueryError>.Fail(new QueryError.InvalidEnumState(targetType, arg));
                        }

                        values[valueIdx++] = value;
                        continue;
                    }

                    if (targetType.IsAssignableFrom(typeof(string)))
                    {
                        values[valueIdx++] = arg;
                        continue;
                    }

                    if (targetType.IsPrimitive || targetType == typeof(decimal))
                    {
                        values[valueIdx++] = Convert.ChangeType(arg, targetType);
                        continue;
                    }

                    if (targetType.IsEnum)
                    {
                        if (!Enum.TryParse(targetType, arg, ignoreCase: true, out object? value))
                        {
                            return Result<object?[], QueryError>.Fail(new QueryError.InvalidEnumState(targetType, arg));
                        }

                        values[valueIdx++] = value;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(arg) || arg == "null")
                    {
                        values[valueIdx++] = targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
                        continue;
                    }

                    return Result<object?[], QueryError>.Fail(new QueryError.ConversionError(arg, targetType));

                    NextArg: ;
                }
                catch (Exception ex)
                {
                    ex.LogException($"Failed to resolve value for type '{targetType}'");
                    return Result<object?[], QueryError>.Fail(new QueryError.UnknownException(ex));
                }
            }

            return Result<object?[], QueryError>.Ok(values);
        }
    }
}
