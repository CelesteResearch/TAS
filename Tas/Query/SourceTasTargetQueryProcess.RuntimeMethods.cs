using System;
using System.Collections.Generic;
using System.Reflection;

namespace Celeste.Mods.Tas.Query
{
    public sealed partial class SourceTasTargetQueryProcess
    {
        private SourceQueryMethodAccess WrapQueryMethod(Celeste.Core.Reflection.Methods.SourceOrderedMethod method)
        {
            var definition = OriginalMethodDefinition(method);
            return new SourceQueryMethodAccess(method, definition,
                Members.GetActualSourceType(definition.AssemblyIdentity, definition.DeclaringType),
                ResolveOriginalValueType);
        }

        private IEnumerable<SourceQueryMethodAccess> GetQueryMethods(Type type, BindingFlags flags)
        {
            if (SourceQueryRuntimeDataTypes.Contains(type))
            {
                foreach (var method in Reflection.GetAllMethodInfos(type, flags))
                    yield return new SourceQueryMethodAccess(method);
            }
            else
            {
                foreach (var method in global::Celeste.Core.Reflection.Runtime.PortableReflection.GetMethods(Members,
                             OriginalMethods, type, flags))
                    yield return method.OriginalBinding != null
                        ? WrapQueryMethod(method.OriginalBinding)
                        : new SourceQueryMethodAccess(method.ActualMethod);
            }
        }

        private SourceQueryMethodAccess GetQueryMethod(Type type, string name, BindingFlags flags)
        {
            if (SourceQueryRuntimeDataTypes.Contains(type))
            {
                var method = type.GetMethod(name, flags);
                return method == null ? null : new SourceQueryMethodAccess(method);
            }

            var actual =
                global::Celeste.Core.Reflection.Runtime.PortableReflection.GetMethod(Members, OriginalMethods, type,
                    name, null, flags);
            return actual == null ? null
                : actual.OriginalBinding != null ? WrapQueryMethod(actual.OriginalBinding)
                : new SourceQueryMethodAccess(actual.ActualMethod);
        }
    }
}
