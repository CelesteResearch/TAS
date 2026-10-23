using System;
using System.Reflection;
using System.Linq;
using Celeste.Core.Reflection.Members;

namespace Celeste.Mods.Tas.Query
{
    internal sealed class SourceQueryMemberAccess
    {
        private readonly SourcePhysicalMemberBinding source;
        private readonly MemberInfo runtime;
        private readonly SourceQueryValueType? queryValueType;

        internal SourceQueryMemberAccess(SourcePhysicalMemberBinding source) => this.source = source;

        internal SourceQueryMemberAccess(MemberInfo runtime, SourceQueryValueType? queryValueType = null)
        {
            this.runtime = runtime;
            this.queryValueType = queryValueType;
        }

        internal static SourceQueryMemberAccess FromSource(SourcePhysicalMemberBinding member)
            => member == null ? null : new SourceQueryMemberAccess(member);

        internal static SourceQueryMemberAccess FromRuntime(MemberInfo member)
            => member == null ? null : new SourceQueryMemberAccess(member);

        internal static SourceQueryMemberAccess FromRuntime(MemberInfo member, SourceQueryValueType queryValueType)
            => member == null ? null : new SourceQueryMemberAccess(member, queryValueType);

        public string Name => source?.Name ?? runtime.Name;
        public Type OriginalDeclaringType => source?.OriginalDeclaringType ?? runtime.DeclaringType;

        public Type ValueType => source?.ValueType ??
                                 (runtime is FieldInfo fieldInfo
                                     ? fieldInfo.FieldType
                                     : ((PropertyInfo)runtime).PropertyType);

        public FieldAttributes OriginalFieldAttributes =>
            source?.OriginalFieldAttributes ?? ((FieldInfo)runtime).Attributes;

        public bool IsStatic =>
            source?.IsStatic ?? (runtime is FieldInfo fieldInfo ? fieldInfo.IsStatic : Accessor.IsStatic);

        public bool CanRead => source?.CanRead ?? (runtime is FieldInfo || ((PropertyInfo)runtime).CanRead);

        public bool CanWrite => source?.CanWrite ??
                                (runtime is FieldInfo fieldInfo
                                    ? !fieldInfo.IsLiteral
                                    : ((PropertyInfo)runtime).CanWrite);

        private MethodInfo Accessor =>
            ((PropertyInfo)runtime).GetGetMethod(true) ?? ((PropertyInfo)runtime).GetSetMethod(true);

        public bool HasDeclaredAttribute(string name) => source != null
            ? source.Definition.HasDeclaredAttribute(name)
            : runtime.GetCustomAttributesData().Any(attribute => attribute.AttributeType.FullName == name);

        public SourceQueryValueType QueryValueType => queryValueType ?? (source == null
            ? new SourceQueryValueType(ValueType)
            : new SourceQueryValueType(ValueType, source.Definition.ValueTypeAssemblyIdentity,
                source.Definition.ValueTypeIdentity, source.Definition.ValueTypeReference));

        public object Read(object? instance) => source != null ? source.Read(instance)
            : runtime is FieldInfo field ? field.GetValue(IsStatic ? null : instance)
            : ((PropertyInfo)runtime).GetValue(IsStatic ? null : instance);

        public void Write(object? instance, object? value)
        {
            if (source != null) source.Write(instance, value);
            else if (runtime is FieldInfo field) field.SetValue(IsStatic ? null : instance, value);
            else ((PropertyInfo)runtime).SetValue(IsStatic ? null : instance, value);
        }
    }
}
