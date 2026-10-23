#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace Celeste.Mods.Tas.Input
{
    internal static class SourceTasEnumerableExtension
    {
        /// Iterates each entry of the IEnumerable and invokes the callback Action
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        /// Returns the first matching element; otherwise null
        public static T? FirstOrNull<T>(this IEnumerable<T> enumerable) where T : struct
        {
            using var enumerator = enumerable.GetEnumerator();
            if (enumerator.MoveNext())
            {
                return enumerator.Current;
            }

            return null;
        }

        /// Returns the first matching element; otherwise null
        public static T? FirstOrNull<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) where T : struct
        {
            foreach (var item in enumerable)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            return null;
        }

        private readonly struct DynamicComparer<T>(Func<T, T, int> compare) : IComparer<T>
        {
            public int Compare(T? x, T? y) => compare(x!, y!);
        }

        /// Sorts the elements according to the comparision function
        public static IEnumerable<T> Sort<T>(this IEnumerable<T> enumerable, Func<T, T, int> compare)
        {
            return enumerable.OrderBy(static value => value, new DynamicComparer<T>(compare));
        }
    }
}
