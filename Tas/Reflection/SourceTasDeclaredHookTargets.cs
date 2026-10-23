using System;
using System.Linq;
using System.Reflection;
using Celeste.Core.Reflection.Methods;

namespace Celeste.Mods.Tas.Reflection
{
    // Selects an already published original declaration for a typed TAS hook target.
    // It is not the general reflection API for an arbitrary type's inherited methods.
    internal static class SourceTasDeclaredHookTargets
    {
        internal static MethodInfo Require(GameApplication process, Type canonicalSourceOwner,
            string originalName, string[] originalParameterTypes = null, bool originalStatic = false)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));
            if (canonicalSourceOwner == null) throw new ArgumentNullException(nameof(canonicalSourceOwner));
            var methods = process.Reflection.SourceMethods;
            if (!process.SourceMembers.ContainsSourceType(canonicalSourceOwner))
                return RequirePhysicalDeclaration(process, canonicalSourceOwner, originalName,
                    originalParameterTypes, originalStatic);
            var owner = process.SourceMembers.GetOriginalTypeDefinition(canonicalSourceOwner);
            var candidates = methods.EnumerateDeclaredDefinitions(owner.AssemblyIdentity, owner.Identity)
                .Where(definition => Matches(definition, originalName, originalParameterTypes, originalStatic))
                .ToArray();
            if (candidates.Length == 0) throw new MissingMethodException(owner.Identity, originalName);
            if (candidates.Length != 1) throw new AmbiguousMatchException(owner.Identity + "." + originalName);
            var definition = candidates[0];
            if (definition.TypeDefinitionToken != owner.MetadataToken)
                throw new InvalidOperationException(
                    "The typed hook declaration has a different canonical source owner.");
            var actual = methods.RequireActualMethod(definition.AssemblyIdentity, definition.MethodDefinitionToken);
            ValidateActual(methods, actual, definition);
            return actual;
        }

        private static MethodInfo RequirePhysicalDeclaration(GameApplication process,
            Type physicalDeclarationOwner, string originalName, string[] originalParameterTypes, bool originalStatic)
        {
            var methods = process.Reflection.SourceMethods;
            const BindingFlags declared = BindingFlags.Public | BindingFlags.NonPublic
                                                              | BindingFlags.Instance | BindingFlags.Static |
                                                              BindingFlags.DeclaredOnly;
            // A split portable operation need not impersonate its original whole TypeDef.
            // Only an existing exact physical MethodInfo -> original MethodDef binding admits a candidate.
            var candidates = physicalDeclarationOwner.GetMethods(declared)
                .Where(actual => actual.DeclaringType == physicalDeclarationOwner
                                 && methods.TryGetOriginalMethodDefinition(actual, out var definition)
                                 && Matches(definition, originalName, originalParameterTypes, originalStatic))
                .ToArray();
            if (candidates.Length == 0)
                throw new MissingMethodException(physicalDeclarationOwner.FullName, originalName);
            if (candidates.Length != 1)
                throw new AmbiguousMatchException(physicalDeclarationOwner.FullName + "." + originalName);
            var actual = candidates[0];
            var definition = methods.RequireOriginalMethodDefinition(actual);
            var owner = process.SourceMembers.GetOriginalTypeDefinition(definition.AssemblyIdentity,
                definition.DeclaringType);
            if (definition.TypeDefinitionToken != owner.MetadataToken
                || !ReferenceEquals(methods.RequireDefinition(definition.AssemblyIdentity,
                    definition.MethodDefinitionToken), definition)
                || methods.RequireActualMethod(definition.AssemblyIdentity, definition.MethodDefinitionToken) != actual)
                throw new InvalidOperationException(
                    "The physical hook declaration has no exact original owner and forward method binding.");
            ValidateActual(methods, actual, definition);
            return actual;
        }

        private static bool Matches(SourceMethodDefinition definition, string originalName,
            string[] originalParameterTypes, bool originalStatic)
            => definition.Name == originalName
               && definition.Name is not (".ctor" or ".cctor")
               && ((definition.Attributes & (int)MethodAttributes.Static) != 0) == originalStatic
               && definition.GenericParameterCount == 0
               && (originalParameterTypes == null
                   || definition.ParameterTypeIdentities.SequenceEqual(originalParameterTypes));

        private static void ValidateActual(SourceAssemblyMethodCatalog methods, MethodInfo actual,
            SourceMethodDefinition definition)
        {
            if (!ReferenceEquals(methods.RequireOriginalMethodDefinition(actual), definition)
                || definition.HasBody != true || actual.ContainsGenericParameters || actual.GetMethodBody() == null)
                throw new InvalidOperationException(
                    "The typed hook target has no exact qualified physical implementation.");
        }
    }
}
