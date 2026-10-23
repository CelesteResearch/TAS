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
    public static class QueryHashCodeExtensions
    {
        public static long GetCustomHashCode<T>(this IEnumerable<T>? enumerable)
        {
            if (enumerable == null)
            {
                return 0;
            }

            unchecked
            {
                long hash = 17;
                foreach (var item in enumerable)
                {
                    hash = hash * -1521134295 + EqualityComparer<T>.Default.GetHashCode(item!);
                }

                return hash;
            }
        }

        public static HashCode Append<T>(this HashCode hash, T value)
        {
            hash.Add(value);
            return hash;
        }
    }
}
