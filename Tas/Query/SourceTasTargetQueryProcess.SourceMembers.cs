using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Celeste.Core.Reflection.Methods;
using Celeste.Core.Reflection.Members;

namespace Celeste.Mods.Tas.Query
{
    public sealed partial class SourceTasTargetQueryProcess
    {
        private readonly Dictionary<(Type Type, Type Target), bool> originalTypeInheritanceCache = new();

        private readonly Dictionary<(Type Type, string Assembly, string Identity), bool>
            originalIdentityInheritanceCache = new();

        private long originalInheritanceBindingsRevision = -1;

        private void RefreshOriginalInheritanceCaches()
        {
            long revision = Members.ActualTypeBindingsRevision;
            if (originalInheritanceBindingsRevision == revision) return;
            originalTypeInheritanceCache.Clear();
            originalIdentityInheritanceCache.Clear();
            originalInheritanceBindingsRevision = revision;
        }

        private readonly Dictionary<(Type Type, string Name, BindingFlags Flags), SourceQueryMemberAccess>
            sourceFieldCache = new();

        private readonly Dictionary<(Type Type, string Name, BindingFlags Flags), SourceQueryMemberAccess>
            sourcePropertyCache = new();

        internal SourceQueryMemberAccess GetOriginalField(Type type, string name,
            BindingFlags flags = QueryReflection.StaticInstanceAnyVisibility, bool logFailure = true)
        {
            var key = (type, name, flags);
            if (sourceFieldCache.TryGetValue(key, out var field)) return field;
            field = SourceQueryRuntimeDataTypes.Contains(type)
                ? CreateRuntimeQueryField(Reflection.GetFieldInfo(type, GetRuntimeQueryFieldName(type, name), flags,
                    logFailure: logFailure))
                : Members.UsesOriginalDeclarationMetadata(type)
                    ? SourceQueryMemberAccess.FromSource(Members.GetFieldInfo(type, name, flags, logFailure))
                    : FindPortableQueryMember(type, name, flags, property: false);
            sourceFieldCache[key] = field;
            return field;
        }

        internal SourceQueryMemberAccess GetOriginalProperty(Type type, string name,
            BindingFlags flags = QueryReflection.StaticInstanceAnyVisibility, bool logFailure = true)
        {
            var key = (type, name, flags);
            if (sourcePropertyCache.TryGetValue(key, out var property)) return property;
            property = SourceQueryRuntimeDataTypes.Contains(type)
                ? SourceQueryMemberAccess.FromRuntime(Reflection.GetPropertyInfo(type, name, flags,
                    logFailure: logFailure))
                : Members.UsesOriginalDeclarationMetadata(type)
                    ? SourceQueryMemberAccess.FromSource(Members.GetPropertyInfo(type, name, flags, logFailure))
                    : FindPortableQueryMember(type, name, flags, property: true);
            sourcePropertyCache[key] = property;
            return property;
        }

        private readonly Dictionary<(Type Type, BindingFlags Flags), IEnumerable<SourceQueryMemberAccess>>
            sourceAllFieldCache = new();

        private readonly Dictionary<(Type Type, BindingFlags Flags), IEnumerable<SourceQueryMemberAccess>>
            sourceAllPropertyCache = new();

        internal IEnumerable<SourceQueryMemberAccess> GetAllOriginalFields(Type type, BindingFlags flags)
        {
            flags |= BindingFlags.DeclaredOnly;
            var key = (type, flags);
            if (sourceAllFieldCache.TryGetValue(key, out var fields)) return fields;
            var allFields = new HashSet<SourceQueryMemberAccess>(SourceQueryRuntimeDataTypes.Contains(type)
                ? Reflection.GetAllFieldInfos(type, flags).Select(CreateRuntimeQueryField)
                : EnumeratePortableQueryMembers(type, flags, property: false));
            sourceAllFieldCache[key] = allFields;
            return allFields;
        }

        internal IEnumerable<SourceQueryMemberAccess> GetAllOriginalProperties(Type type, BindingFlags flags)
        {
            flags |= BindingFlags.DeclaredOnly;
            var key = (type, flags);
            if (sourceAllPropertyCache.TryGetValue(key, out var properties)) return properties;
            var allProperties = new HashSet<SourceQueryMemberAccess>(SourceQueryRuntimeDataTypes.Contains(type)
                ? Reflection.GetAllPropertyInfos(type, flags).Select(SourceQueryMemberAccess.FromRuntime)
                : EnumeratePortableQueryMembers(type, flags, property: true));
            sourceAllPropertyCache[key] = allProperties;
            return allProperties;
        }

        internal bool IsRuntimeOwnedQueryType(Type type) =>
            SourceQueryRuntimeDataTypes.Contains(type) || !Members.ContainsSourceType(type);

        public string GetOriginalTypeName(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return Members.ContainsSourceType(type)
                ? Members.GetOriginalTypeName(type)
                : ((Celeste.Everest.Reflection.Runtime.ISourceRuntimeTypeDiscoveryEnvironment)engine)
                .RuntimeTypeDiscovery.TypeAliases.GetActiveRuntimeTypeAlias(type, shortName: true) ?? type.Name;
        }

        internal IReadOnlyList<Celeste.Everest.Modules.Runtime.SourceEverestModule> SourceModules
            => ((Celeste.Everest.Modules.Runtime.ISourceEverestModulesProcessEnvironment)engine).ModulesProcess.Modules;

        public string GetOriginalTypeFullName(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return Members.ContainsSourceType(type)
                ? Members.GetOriginalTypeFullName(type)
                : ((Celeste.Everest.Reflection.Runtime.ISourceRuntimeTypeDiscoveryEnvironment)engine)
                .RuntimeTypeDiscovery.TypeAliases.GetActiveRuntimeTypeAlias(type) ?? type.FullName;
        }

        internal bool IsSameOrSubclassOfOriginal(Type type, string originalAssemblyIdentity,
            string originalTypeIdentity)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            RefreshOriginalInheritanceCaches();
            var key = (type, originalAssemblyIdentity, originalTypeIdentity);
            if (originalIdentityInheritanceCache.TryGetValue(key, out bool cached)) return cached;
            for (; type != null && !Members.ContainsSourceType(type); type = type.BaseType)
            {
            }

            if (type == null) return originalIdentityInheritanceCache[key] = false;
            var definition = Members.GetOriginalTypeDefinition(type);
            while (true)
            {
                if (definition.AssemblyIdentity == originalAssemblyIdentity &&
                    definition.Identity == originalTypeIdentity)
                    return originalIdentityInheritanceCache[key] = true;
                if (definition.BaseTypeIdentity == null) return originalIdentityInheritanceCache[key] = false;
                definition =
                    Members.GetOriginalTypeDefinition(definition.BaseAssemblyIdentity, definition.BaseTypeIdentity);
            }
        }

        internal bool IsSameOrSubclassOfOriginal(Type type, string originalTypeIdentity)
            => IsSameOrSubclassOfOriginal(type,
                "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", originalTypeIdentity);

        internal bool IsSameOrSubclassOfOriginal(Type type, Type originalTargetType)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (originalTargetType == null) throw new ArgumentNullException(nameof(originalTargetType));
            if (originalTargetType.IsAssignableFrom(type)) return true;
            RefreshOriginalInheritanceCaches();
            var key = (type, originalTargetType);
            if (originalTypeInheritanceCache.TryGetValue(key, out bool cached)) return cached;
            if (!Members.ContainsSourceType(originalTargetType)) return originalTypeInheritanceCache[key] = false;
            var target = Members.GetOriginalTypeDefinition(originalTargetType);
            return originalTypeInheritanceCache[key] =
                IsSameOrSubclassOfOriginal(type, target.AssemblyIdentity, target.Identity);
        }

        public IEnumerable<SourcePhysicalMemberBinding> GetOriginalSimpleMembers(Type type, bool declaredOnly)
            => Members.GetOriginalSimpleMembers(type, declaredOnly);

        private static string GetOriginalNamespace(string fullName)
        {
            int nested = fullName.IndexOf('+');
            string outer = nested < 0 ? fullName : fullName[..nested];
            int separator = outer.LastIndexOf('.');
            return separator < 0 ? null : outer[..separator];
        }

        private string GetOriginalModName(SourceTypeDefinition definition)
        {
            // Original ConsoleEnhancements gives vanilla assembly the Celeste name before modules.
            if (new AssemblyName(definition.AssemblyIdentity).Name == "Celeste")
                return definition.Identity.StartsWith("Celeste.Mod.", StringComparison.Ordinal) ? "Everest" : "Celeste";
            foreach (var module in ((Celeste.Everest.Modules.Runtime.ISourceEverestModulesProcessEnvironment)engine)
                     .ModulesProcess.Modules)
            {
                if (module is Celeste.Everest.Modules.Runtime.SourceNullModule) continue;
                if (!Members.ContainsSourceType(module.GetType())) continue;
                var moduleDefinition = Members.GetOriginalTypeDefinition(module.GetType());
                if (moduleDefinition.AssemblyIdentity == definition.AssemblyIdentity) return module.Metadata.Name;
            }

            return "Unknown";
        }

        public string FormatOriginalObject(object value)
        {
            if (value == null) return null;
            Type type = value.GetType();
            if (IsRuntimeOwnedQueryType(type)) return value.ToString();
            var method = type.GetMethod("ToString", BindingFlags.Public | BindingFlags.Instance,
                null, Type.EmptyTypes, null);
            if (Members.ContainsSourceType(type))
            {
                if (method.DeclaringType == typeof(object)) return Members.GetOriginalTypeFullName(type);
                if (method.DeclaringType.Assembly != typeof(object).Assembly)
                    ((Celeste.Everest.Reflection.ISourceEverestReflectionEnvironment)engine).Reflection
                        .SourceMethods.RequireOriginalMethodDefinition(method);
                return (string)method.Invoke(value, null);
            }

            return value.ToString();
        }

        internal global::Celeste.Scene.Tracking.SceneEntityCatalog SceneCatalog =>
            ((global::Celeste.Scene.Tracking.ISourceSceneCatalogEnvironment)engine).SceneCatalog;

        internal SourceAssemblyMethodCatalog OriginalMethods =>
            ((Celeste.Everest.Reflection.ISourceEverestReflectionEnvironment)engine).Reflection.SourceMethods;

        internal SourceMethodDefinition OriginalMethodDefinition(SourceOrderedMethod method)
            => OriginalMethods.RequireDefinition(method.SourceAssemblyIdentity, method.SourceMethodDefinitionToken);

        internal string DescribeOriginalMethodSignature(SourceOrderedMethod method)
        {
            var definition = OriginalMethodDefinition(method);
            return
                $"{definition.ReturnTypeIdentity} {definition.Name}({string.Join(", ", definition.ParameterTypeIdentities)})";
        }

        internal Type ResolveOriginalValueType(string originalAssemblyIdentity, string sourceIdentity)
        {
            if (originalAssemblyIdentity == null)
                throw new NotSupportedException(
                    $"Original type specification '{sourceIdentity}' requires a qualified source type specification resolver.");
            return Members.GetActualSourceType(originalAssemblyIdentity, sourceIdentity);
        }

        internal Type ResolveOriginalValueType(Celeste.Core.Reflection.Types.SourceTypeReference originalTypeReference)
            => sourceTypeReferences.Resolve(originalTypeReference ??
                                            throw new InvalidOperationException(
                                                "The original source type reference has not been published."));

        internal SourceQueryMemberIdentity DescribeOriginalMethod(SourceOrderedMethod method)
        {
            var definition = OriginalMethodDefinition(method);
            var actualDeclaringType =
                Members.GetActualSourceType(definition.AssemblyIdentity, definition.DeclaringType);
            return new SourceQueryMemberIdentity(definition.Name, actualDeclaringType);
        }
    }
}
