using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Runtime;
using Celeste.Core.Reflection.Types;
using Celeste.Everest.Modules.Runtime.Assemblies;

namespace Celeste.Mods.Tas.Query
{
    public sealed partial class SourceTasTargetQueryProcess
    {
        private SourceQueryMemberAccess FindPortableQueryMember(Type type, string name, BindingFlags flags,
            bool property)
        {
            var member = property
                ? PortableReflection.GetProperty(Members, type, name, flags)
                : PortableReflection.GetField(Members, type, name, flags);
            if (member == null) return null;
            if (member.OriginalBinding != null) return SourceQueryMemberAccess.FromSource(member.OriginalBinding);
            return property
                ? SourceQueryMemberAccess.FromRuntime(member.ActualMember)
                : CreateRuntimeQueryField((FieldInfo)member.ActualMember);
        }

        private IEnumerable<SourceQueryMemberAccess> EnumeratePortableQueryMembers(Type type, BindingFlags flags,
            bool property)
        {
            for (var current = type; current != null && current != typeof(object); current = current.BaseType)
            {
                if (Members.UsesOriginalDeclarationMetadata(current))
                {
                    var source = property
                        ? Members.GetAllPropertyInfos(current, flags)
                        : Members.GetAllFieldInfos(current, flags);
                    foreach (var binding in source) yield return SourceQueryMemberAccess.FromSource(binding);
                    yield break;
                }

                if (property)
                    foreach (var member in current.GetProperties(flags | BindingFlags.DeclaredOnly)
                                 .OrderBy(member => member.MetadataToken))
                        yield return SourceQueryMemberAccess.FromRuntime(member);
                else
                    foreach (var member in current.GetFields(flags | BindingFlags.DeclaredOnly)
                                 .OrderBy(member => member.MetadataToken))
                        yield return CreateRuntimeQueryField(member);
            }
        }

        private IEnumerable<Type> EnumerateQueryTypes()
        {
            var reflection = ((Celeste.Everest.Reflection.ISourceEverestReflectionEnvironment)engine).Reflection;
            var seen = new HashSet<Type>();
            foreach (var type in reflection.GetAggregateRuntimeTypes())
                if (seen.Add(type))
                    yield return type;
            foreach (var module in SourceModules)
                if (module.Metadata?.AssemblyContext is { } context)
                    foreach (var assembly in context.Assemblies)
                    foreach (var type in reflection.TypeSafety.GetTypesSafe(assembly))
                        if (seen.Add(type))
                            yield return type;
        }

        private string GetOriginalModName(Type type)
        {
            if (Members.ContainsSourceType(type)) return GetOriginalModName(Members.GetOriginalTypeDefinition(type));
            foreach (var module in SourceModules)
                if (module.GetType().Assembly == type.Assembly)
                    return module.Metadata.Name;
            return ((ISourceEverestAssemblyProcessEnvironment)engine).AssemblyContexts
                .GetPhysicalAssemblyOwner(type.Assembly)?.Name ?? "Unknown";
        }

        private bool HasQueryCompilerGeneratedAttribute(Type type)
            => Members.ContainsSourceType(type)
                ? Members.GetOriginalTypeDefinition(type)
                    .HasDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute")
                : type.GetCustomAttributesData().Any(attribute =>
                    attribute.AttributeType.FullName == "System.Runtime.CompilerServices.CompilerGeneratedAttribute");
    }
}
