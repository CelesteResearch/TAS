using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasMethodNameFormatting
    {
        // The original GetFullName formatting body, shared by query diagnostics
        // and hook diagnostics. Actual source hooks supply the same qualified PE
        // method catalog so renamed portable declarations retain original names.
        internal static string GetFullName(MethodBase method)
            => Format(method.DeclaringType?.FullName, method.Name);

        internal static string GetFullName(SourceAssemblyMethodCatalog methods,
            SourceMemberCatalog members, MethodBase method)
            => methods.TryGetOriginalMethodDefinition(members, method, out var definition)
                ? Format(definition.DeclaringType, definition.Name)
                : GetFullName(method);

        private static string Format(string? declaringType, string methodName)
            => declaringType is { } ? $"{declaringType}::{methodName}" : methodName;
    }
}
