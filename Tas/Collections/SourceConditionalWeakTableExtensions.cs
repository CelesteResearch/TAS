using System;
using System.Runtime.CompilerServices;

namespace Celeste.Mods.Tas.Collections
{
    internal static class SourceConditionalWeakTableExtensions
    {
        internal static bool TryAdd<TKey, TValue>(this ConditionalWeakTable<TKey, TValue> table, TKey key, TValue value)
            where TKey : class where TValue : class
        {
            lock (table)
            {
                if (table.TryGetValue(key, out _)) return false;
                table.Add(key, value);
                return true;
            }
        }
    }
}
