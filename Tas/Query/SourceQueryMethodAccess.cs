using System;
using System.Linq;
using System.Reflection;
using Celeste.Core.Reflection.Methods;
using Celeste.Core.Reflection.Types;

namespace Celeste.Mods.Tas.Query
{
    internal sealed class SourceQueryMethodAccess
    {
        private readonly SourceOrderedMethod source;
        private readonly SourceMethodDefinition definition;
        private readonly MethodInfo actual;
        private readonly Func<SourceTypeReference, Type> resolveType;
        internal bool OriginalIsStatic => source?.OriginalIsStatic ?? actual.IsStatic;
        internal string Name => definition?.Name ?? actual.Name;
        internal Type DeclaringType { get; }
        internal int GenericParameterCount => definition?.GenericParameterCount ?? actual.GetGenericArguments().Length;
        internal int ParameterCount => definition?.Parameters.Count ?? actual.GetParameters().Length;

        internal bool[] ParameterDefaults => definition != null
            ? definition.Parameters.Select(p => p.HasDefaultValue).ToArray()
            : actual.GetParameters().Select(p => p.HasDefaultValue).ToArray();

        internal SourceQueryValueType[] ParameterTypes => definition != null
            ? definition.Parameters.Select(p => new SourceQueryValueType(resolveType(p.TypeReference),
                p.TypeAssemblyIdentity, p.TypeIdentity, p.TypeReference)).ToArray()
            : actual.GetParameters().Select(p => new SourceQueryValueType(p.ParameterType)).ToArray();

        internal bool HasDeclaredAttribute(string name) => definition != null
            ? definition.HasDeclaredAttribute(name)
            : actual.GetCustomAttributesData().Any(a => a.AttributeType.FullName == name);

        internal string Signature => definition != null
            ? $"{definition.ReturnTypeIdentity} {Name}({string.Join(", ", definition.ParameterTypeIdentities)})"
            : actual.ToString();

        internal SourceQueryMethodAccess(MethodInfo actual)
        {
            this.actual = actual ?? throw new ArgumentNullException(nameof(actual));
            DeclaringType = actual.DeclaringType;
        }

        internal SourceQueryMethodAccess(SourceOrderedMethod source, SourceMethodDefinition definition,
            Type declaringType, Func<SourceTypeReference, Type> resolveType)
        {
            this.source = source;
            this.definition = definition;
            actual = source.ActualMethod;
            DeclaringType = declaringType;
            this.resolveType = resolveType;
        }

        internal object Invoke(object target, object[] arguments) => source != null
            ? source.Invoke(target, arguments)
            : actual.Invoke(actual.IsStatic ? null : target, arguments);
    }
}
