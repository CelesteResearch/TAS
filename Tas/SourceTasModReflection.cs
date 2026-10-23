using System;
using System.Linq;
using System.Reflection;
using System.Collections.Concurrent;
using Celeste.Everest.Modules.Runtime;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Runtime;
using Celeste.Core.Reflection.Types;
using Celeste.Everest.Reflection;
using Celeste.Everest.Reflection.Runtime;
using Celeste.Everest.Diagnostics.Logging;

namespace Celeste.Mods.Tas
{
    internal sealed partial class SourceTasModReflection
    {
        internal const BindingFlags StaticInstanceAnyVisibility =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        private readonly GameApplication process;

        private readonly ConcurrentDictionary<(Type Type, string Name, BindingFlags Flags), ReflectedMember> fields =
            new();

        private readonly ConcurrentDictionary<(Type Type, string Name, BindingFlags Flags), ReflectedMember>
            properties = new();

        public SourceTasModReflection(GameApplication process)
        {
            this.process = process;
        }

        internal Type? GetType(string modName, string fullTypeName)
            => FindType(modName, fullTypeName, logFailure: true);

        internal Type? TryGetType(string modName, string fullTypeName)
            => FindType(modName, fullTypeName, logFailure: false);

        private Type? FindType(string modName, string fullTypeName, bool logFailure)
        {
            var module = ((ISourceEverestModulesProcessEnvironment)process).ModulesProcess.Modules
                .FirstOrDefault(module => module.Metadata?.Name == modName);
            if (module == null) return null;
            var assembly = module.GetType().Assembly;
            var type = assembly.GetType(fullTypeName) ?? module.Metadata.AssemblyContext?.Assemblies
                .Select(value => value.GetType(fullTypeName)).FirstOrDefault(value => value != null);
            if (type == null && process is ISourceRuntimeTypeDiscoveryEnvironment discovery)
            {
                var alias = discovery.RuntimeTypeDiscovery.ResolveRuntimeTypeAlias(fullTypeName);
                if (alias != null && (alias.Assembly == assembly ||
                                      module.Metadata.AssemblyContext?.Assemblies.Contains(alias.Assembly) == true))
                    type = alias;
            }

            if (type == null)
            {
                var members = ((ISourceEverestReflectionEnvironment)process).Reflection.SourceMembers;
                if (members.ContainsSourceType(module.GetType())) type = GetAssembly(modName).GetType(fullTypeName);
            }

            if (type == null && logFailure)
                Log($"Failed to find type '{fullTypeName}' in assembly '{assembly.FullName}' of mod '{modName}'");
            return type;
        }

        internal ReflectedMember GetFieldInfo(Type type, string name,
            BindingFlags bindingFlags = StaticInstanceAnyVisibility, bool logFailure = true)
        {
            var key = (type, name, bindingFlags);
            if (fields.TryGetValue(key, out var result)) return result;
            var members = ((ISourceEverestReflectionEnvironment)process).Reflection.SourceMembers;
            result = PortableReflection.GetField(members, type, name, bindingFlags);
            if (result == null && logFailure)
                Log(
                    $"Failed to find field '{name}' on type '{TypeReflectionIdentity.Resolve(members, type).TypeIdentity}'");
            return fields[key] = result;
        }

        internal ReflectedMember GetPropertyInfo(Type type, string name,
            BindingFlags bindingFlags = StaticInstanceAnyVisibility, bool logFailure = true)
        {
            var key = (type, name, bindingFlags);
            if (properties.TryGetValue(key, out var result)) return result;
            var members = ((ISourceEverestReflectionEnvironment)process).Reflection.SourceMembers;
            result = PortableReflection.GetProperty(members, type, name, bindingFlags);
            if (result == null && logFailure)
                Log(
                    $"Failed to find property '{name}' on type '{TypeReflectionIdentity.Resolve(members, type).TypeIdentity}'");
            return properties[key] = result;
        }

        private void Log(string message)
            => ((IEverestLoggerProcessEnvironment)process).Logger.Log(global::Celeste.Everest.EverestLogLevel.Error,
                "CelesteTAS", message);
    }
}
