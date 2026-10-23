using System.Collections.Generic;
using System.Linq;

namespace Celeste.Mods.Tas.Input
{
    internal static class SourceTasInputCollectionExtensions
    {
        public static T? GetValueOrDefault<T>(this IList<T> list, int index, T? defaultValue = default) =>
            index >= 0 && index < list.Count ? list[index] : defaultValue;

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable) => !enumerable.Any();

        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable) =>
            enumerable == null || !enumerable.Any();

        public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable) => !enumerable.IsEmpty();

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> enumerable) => !enumerable.IsNullOrEmpty();

        /// Checks if the first sequence starts with the second sequence
        public static bool SequenceStartsWith<T>(this IEnumerable<T> first, IEnumerable<T> second,
            IEqualityComparer<T>? comparer = null)
        {
            // Optimize for certain cases
            if (first is ICollection<T> firstCollection && second is ICollection<T> secondCollection)
            {
                if (firstCollection.Count < secondCollection.Count)
                {
                    return false;
                }

                if (first is T[] firstArray && second is T[] secondArray)
                {
                    int count = secondArray.Length;
                    comparer ??= EqualityComparer<T>.Default;
                    for (int i = 0; i < count; ++i)
                    {
                        if (!comparer.Equals(firstArray[i], secondArray[i])) return false;
                    }

                    return true;
                }

                if (first is IList<T> firstList && second is IList<T> secondList)
                {
                    comparer ??= EqualityComparer<T>.Default;

                    int count = secondList.Count;
                    for (int i = 0; i < count; ++i)
                    {
                        if (!comparer.Equals(firstList[i], secondList[i]))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            // Generic case
            comparer ??= EqualityComparer<T>.Default;

            using var firstEnumerator = first.GetEnumerator();
            using var secondEnumerator = second.GetEnumerator();

            while (secondEnumerator.MoveNext())
            {
                if (!firstEnumerator.MoveNext() || !comparer.Equals(firstEnumerator.Current, secondEnumerator.Current))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
