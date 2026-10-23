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
        /// Checks if the current type could be implicitly converted to the target type
        public bool CanCoerceTo(Type type, Type target)
        {
            // Trivial case
            if (target.IsAssignableFrom(type))
            {
                return true;
            }

            // Implicit conversion operators
            foreach (var method in GetAllMethodInfos(type, StaticAnyVisibility)
                         .Concat(GetAllMethodInfos(target, StaticAnyVisibility)))
            {
                if (method.Name == "op_Implicit" &&
                    target.IsAssignableFrom(method.ReturnType) &&
                    method.GetParameters() is { Length: 1 } parameters &&
                    parameters[0].ParameterType.IsAssignableFrom(type)
                   )
                {
                    return true;
                }
            }

            return false;
        }

        /// Implicitly converts the current object to the target
        public Result<object?, string> CoerceTo(object? obj, Type target)
        {
            if (obj == null)
            {
                return target.IsValueType
                    ? Result<object?, string>.Ok(null)
                    : Result<object?, string>.Fail($"Cannot coerce null into a value type '{target}'");
            }

            var type = obj.GetType();

            // Trivial case
            if (target.IsAssignableFrom(type))
            {
                return Result<object?, string>.Ok(obj);
            }

            // Implicit conversion operators
            foreach (var method in GetAllMethodInfos(type, StaticAnyVisibility)
                         .Concat(GetAllMethodInfos(target, StaticAnyVisibility)))
            {
                if (method.Name == "op_Implicit" &&
                    target.IsAssignableFrom(method.ReturnType) &&
                    method.GetParameters() is { Length: 1 } parameters &&
                    parameters[0].ParameterType.IsAssignableFrom(type)
                   )
                {
                    return Result<object?, string>.Ok(method.Invoke(null, [obj]));
                }
            }

            return Result<object?, string>.Fail($"Cannot coerce value of type '{type}' into '{target}'");
        }
    }
}
