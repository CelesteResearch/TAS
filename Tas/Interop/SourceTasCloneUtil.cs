using System;
using System.Reflection;
using Celeste.Mods.Tas.Query;

namespace Celeste.Mods.Tas.Interop
{
    internal static class SourceTasCloneUtil
    {
        private static class Cache<T>
        {
            internal static readonly Func<T, object> Clone = (Func<T, object>)typeof(T)
                .GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic)
                .CreateDelegate(typeof(Func<T, object>));
        }

        internal static T ShallowClone<T>(T value) => (T)Cache<T>.Clone(value);

        internal static void CopyAllFields(QueryReflection reflection, object to, object from,
            bool onlyDifferent = false)
        {
            if (to.GetType() != from.GetType()) throw new ArgumentException("object to and from must be the same type");
            foreach (var field in reflection.GetAllFieldInfos(to.GetType(),
                         BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                object fromValue = field.GetValue(from);
                if (onlyDifferent && fromValue == field.GetValue(to)) continue;
                field.SetValue(to, fromValue);
            }
        }

        internal static void CopyAllProperties(QueryReflection reflection, object to, object from,
            bool onlyDifferent = false)
        {
            if (to.GetType() != from.GetType()) throw new ArgumentException("object to and from must be the same type");
            foreach (var property in reflection.GetAllPropertyInfos(to.GetType(),
                         BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (!property.CanRead || !property.CanWrite) continue;
                object fromValue = property.GetValue(from);
                if (onlyDifferent && fromValue == property.GetValue(to)) continue;
                property.SetValue(to, fromValue);
            }
        }
    }
}
