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
    public static class QueryTypeExtensions
    {
        public static bool IsSameOrSubclassOf(this Type potentialDescendant, Type potentialBase)
        {
            return potentialDescendant == potentialBase || potentialDescendant.IsSubclassOf(potentialBase);
        }

        public static bool IsSameOrSubclassOf(this Type potentialDescendant, params Type[] potentialBases)
        {
            return potentialBases.Any(potentialDescendant.IsSameOrSubclassOf);
        }

        public static bool IsSimpleType(this Type type)
        {
            return type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal) ||
                   type == typeof(Vector2);
        }

        public static bool IsStructType(this Type type)
        {
            return type.IsValueType && !type.IsEnum && !type.IsPrimitive && !type.IsEquivalentTo(typeof(decimal));
        }

        public static bool IsConst(this FieldInfo fieldInfo)
        {
            return fieldInfo.IsLiteral && !fieldInfo.IsInitOnly;
        }
    }
}
