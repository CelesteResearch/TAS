using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;
using Celeste.Core.Reflection.Members;

namespace Celeste.Mods.Tas.Reflection
{
    internal sealed class SourceTasFastReflectionProcess
    {
        private readonly GameApplication process;

        internal SourceTasFastReflectionProcess(GameApplication process)
        {
            this.process = process;
        }

        private readonly struct DelegateKey : IEquatable<DelegateKey>
        {
            internal readonly Type Type, InstanceType, ReturnType;
            internal readonly string Name;

            internal DelegateKey(Type type, string name, Type instanceType, Type returnType)
            {
                Type = type;
                Name = name;
                InstanceType = instanceType;
                ReturnType = returnType;
            }

            public bool Equals(DelegateKey other) => Type == other.Type && Name == other.Name &&
                                                     InstanceType == other.InstanceType &&
                                                     ReturnType == other.ReturnType;

            public override bool Equals(object value) => value is DelegateKey other && Equals(other);
            public override int GetHashCode() => HashCode.Combine(Type, Name, InstanceType, ReturnType);
        }

        private readonly ConcurrentDictionary<DelegateKey, Delegate> CachedFieldGetDelegates = new();

        internal SourceTasGetDelegate<TInstance, TResult> CreateGetDelegate<TInstance, TResult>(Type type, string name)
        {
            var field = process.SourceMembers.GetFieldInfo(type, name);
            if (field == null) return null;
            Type returnType = typeof(TResult), fieldType = field.ValueType;
            if (!returnType.IsAssignableFrom(fieldType))
                throw new InvalidCastException(
                    $"{field.Name} is of type {fieldType}, it cannot be assigned to the type {returnType}.");
            var key = new DelegateKey(type, name, typeof(TInstance), returnType);
            if (CachedFieldGetDelegates.TryGetValue(key, out var result))
                return (SourceTasGetDelegate<TInstance, TResult>)result;
            if (field.IsLiteral)
            {
                TResult value = (TResult)field.Read(null);
                Func<TInstance, TResult> func = _ => value;
                var getter =
                    (SourceTasGetDelegate<TInstance, TResult>)func.Method.CreateDelegate(
                        typeof(SourceTasGetDelegate<TInstance, TResult>), func.Target);
                CachedFieldGetDelegates[key] = getter;
                return getter;
            }

            var method = new DynamicMethod(field.Definition.QualifiedName + " Getter", returnType,
                new[] { typeof(object), typeof(TInstance) }, field.ActualInstanceScope, true);
            var il = method.GetILGenerator();
            Type currentType = null;
            for (int index = 0; index < field.ActualPath.Count; index++)
            {
                MemberInfo member = field.ActualPath[index];
                FieldInfo actualField = member as FieldInfo;
                MethodInfo actualGetter = member is PropertyInfo property ? property.GetGetMethod(true) : null;
                bool isStatic = actualField != null ? actualField.IsStatic : actualGetter.IsStatic;
                Type declaringType = member.DeclaringType;
                if (!isStatic)
                {
                    if (index == 0)
                    {
                        if (field.IsStatic)
                        {
                            il.Emit(OpCodes.Ldarg_0);
                            currentType = typeof(object);
                        }
                        else
                        {
                            il.Emit(OpCodes.Ldarg_1);
                            currentType = typeof(TInstance);
                        }
                    }

                    if (currentType.IsValueType)
                    {
                        var local = il.DeclareLocal(currentType);
                        il.Emit(OpCodes.Stloc, local);
                        il.Emit(OpCodes.Ldloca, local);
                    }
                    else if (declaringType.IsValueType) il.Emit(OpCodes.Unbox, declaringType);
                    else if (currentType != declaringType) il.Emit(OpCodes.Castclass, declaringType);
                }
                else if (index > 0) il.Emit(OpCodes.Pop);

                if (actualField != null)
                {
                    il.Emit(isStatic ? OpCodes.Ldsfld : OpCodes.Ldfld, actualField);
                    currentType = actualField.FieldType;
                }
                else
                {
                    il.Emit(isStatic || declaringType.IsValueType ? OpCodes.Call : OpCodes.Callvirt, actualGetter);
                    currentType = actualGetter.ReturnType;
                }
            }

            if (fieldType.IsValueType && !returnType.IsValueType) il.Emit(OpCodes.Box, fieldType);
            il.Emit(OpCodes.Ret);
            result = CachedFieldGetDelegates[key] = method.CreateDelegate(
                typeof(SourceTasGetDelegate<TInstance, TResult>),
                field.ActualStaticSourceOwner);
            return (SourceTasGetDelegate<TInstance, TResult>)result;
        }

        internal SourceTasGetDelegate<TInstance, TResult> CreateGetDelegate<TInstance, TResult>(string name)
            => CreateGetDelegate<TInstance, TResult>(typeof(TInstance), name);
    }
}
