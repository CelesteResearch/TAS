using System;
using System.Collections.Generic;
using System.Reflection;
using MonoMod.Cil;
using Celeste.Everest.Reflection;

namespace Celeste.Mods.Tas.DesyncFix
{
    internal sealed partial class SourceTasSkinModFixProcess
    {
        private void HookOptional(string mod, string type, string method, Action<ILCursor, ILContext> manipulate)
        {
            MethodInfo target = driver.SourceModReflection.TryGetMethod(mod, type, method)?.ActualMethod;
            if (target != null) driver.SourceHooks.IlHook(target, manipulate);
        }

        private void HookAllOriginalMethods(Type type, Action<ILCursor, ILContext> manipulate)
        {
            if (type == null) return;
            var reflection = ((ISourceEverestReflectionEnvironment)driver.Process).Reflection;
            var members = reflection.SourceMembers;
            var methods = reflection.SourceMethods;
            var seen = new HashSet<MethodInfo>();
            Type current = type;
            while (current != null)
            {
                var definition = members.GetOriginalTypeDefinition(current);
                if (definition.Identity == "System.Object") break;
                foreach (var sourceMethod in methods.GetMethods(members, current,
                             BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                             BindingFlags.Instance |
                             BindingFlags.DeclaredOnly))
                    if (seen.Add(sourceMethod.ActualMethod))
                        driver.SourceHooks.IlHook(sourceMethod.ActualMethod, manipulate);
                if (definition.BaseTypeIdentity == null) break;
                current = members.GetActualSourceType(definition.BaseAssemblyIdentity, definition.BaseTypeIdentity)
                          ?? throw new InvalidOperationException(
                              "The original optional skin base type has no qualified actual binding: " +
                              definition.BaseTypeIdentity);
            }
        }
    }
}
