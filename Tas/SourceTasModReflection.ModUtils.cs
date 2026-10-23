using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Celeste.Core.Reflection.Methods;
using Celeste.Core.Reflection.Types;
using Celeste.Everest.Modules.Runtime;
using Celeste.Everest.Reflection;

namespace Celeste.Mods.Tas
{
    internal sealed partial class SourceTasModReflection
    {
        internal SourceEverestModule? GetModule(string modName)
            => ((ISourceEverestModulesProcessEnvironment)process).ModulesProcess.Modules.FirstOrDefault(module =>
                module.Metadata?.Name == modName);

        internal bool IsInstalled(string modName) => GetModule(modName) != null;

        internal Type[] GetTypes()
            => ((ISourceEverestReflectionEnvironment)process).Reflection.GetFakeEntryAssembly().GetTypes();

        internal IEnumerable<Type> GetTypes(string modName, params string[] fullTypeNames)
        {
            var module = GetModule(modName);
            if (module == null) yield break;
            foreach (string name in fullTypeNames)
            {
                var type = TryGetType(modName, name);
                if (type != null) yield return type;
            }
        }

        internal ReflectedMethod? TryGetMethod(string modName, string fullTypeName, string methodName,
            Type[] parameterTypes = null)
        {
            var type = TryGetType(modName, fullTypeName);
            return type == null ? null : GetMethodInfo(type, methodName, parameterTypes, logFailure: false);
        }

        internal ReflectedMethod? GetMethod(string modName, string fullTypeName, string methodName,
            Type[] parameterTypes = null)
        {
            var type = GetType(modName, fullTypeName);
            if (type == null) return null;
            var method = GetMethodInfo(type, methodName, parameterTypes);
            if (method == null)
            {
                var members = ((ISourceEverestReflectionEnvironment)process).Reflection.SourceMembers;
                Log(
                    $"Failed to find method '{methodName}' in type '{TypeReflectionIdentity.Resolve(members, type).TypeIdentity}' of mod '{modName}'");
            }

            return method;
        }
    }
}
