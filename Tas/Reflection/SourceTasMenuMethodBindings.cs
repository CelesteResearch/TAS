using System;
using System.Reflection;
using Celeste.Core.Reflection.Methods;
using Celeste.Game.UI.Menus.Buttons;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasMenuMethodBindings
    {
        internal static void BindCommands(SourceAssemblyMethodCatalog methods, string assemblyIdentity)
        {
            const int token = 100672663;
            var definition = methods.RequireDefinition(assemblyIdentity, token);
            if (definition.DeclaringType != "Celeste.MenuButton" || definition.TypeDefinitionToken != 33555883 ||
                definition.Name != "Confirm" || definition.Attributes != 454 ||
                definition.ReturnTypeIdentity != "System.Void" || definition.GenericParameterCount != 0 ||
                definition.Parameters.Count != 0)
                throw new InvalidOperationException("The original MenuButton.Confirm contract does not match.");
            var actual = typeof(MenuButtonPresentation).GetMethod("Confirm",
                             BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly, null,
                             Type.EmptyTypes, null)
                         ?? throw new MissingMethodException(typeof(MenuButtonPresentation).FullName, "Confirm");
            if (!actual.IsVirtual || actual.ReturnType != typeof(void))
                throw new InvalidOperationException("The actual MenuButton.Confirm contract does not match.");
            methods.BindActualMethod(assemblyIdentity, token, actual, null);
        }
    }
}
