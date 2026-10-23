using System;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Reflection.Methods;
using Celeste.Game.Profiles;
using Celeste.Simulation.Gameplay.Player.Presentation.Sprites;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasSkinModGetterActualMethodBindings
    {
        private const string GameAssembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        private const string SourceOwner = "Celeste.PlayerSprite";

        internal static void Register(GameApplication process)
        {
            bool installed = process.SourceProfile == GameProfile.InstalledEverest;
            var methods = process.Reflection.SourceMethods;
            var owner = process.SourceMembers.GetOriginalTypeDefinition(typeof(PlayerSprite));
            if (owner.AssemblyIdentity != GameAssembly || owner.Identity != SourceOwner)
                throw new InvalidOperationException(
                    "The actual PlayerSprite has a different original source identity.");
            Bind(methods, owner.MetadataToken, installed ? 100675790 : 100670291,
                nameof(PlayerSprite.HasHair), "System.Boolean", typeof(bool));
            Bind(methods, owner.MetadataToken, installed ? 100675787 : 100670288,
                nameof(PlayerSprite.HairOffset), "Microsoft.Xna.Framework.Vector2", typeof(Vector2));
            Bind(methods, owner.MetadataToken, installed ? 100675789 : 100670290,
                nameof(PlayerSprite.HairFrame), "System.Int32", typeof(int));
            Bind(methods, owner.MetadataToken, installed ? 100675788 : 100670289,
                nameof(PlayerSprite.CarryYOffset), "System.Single", typeof(float));
        }

        private static void Bind(SourceAssemblyMethodCatalog methods, int sourceOwnerToken, int sourceMethodToken,
            string propertyName, string sourceReturnType, Type actualReturnType)
        {
            var definition = methods.RequireDefinition(GameAssembly, sourceMethodToken);
            if (definition.DeclaringType != SourceOwner || definition.TypeDefinitionToken != sourceOwnerToken
                                                        || definition.Name != "get_" + propertyName ||
                                                        definition.ReturnTypeIdentity != sourceReturnType
                                                        || definition.ParameterTypeIdentities.Count != 0 ||
                                                        definition.GenericParameterCount != 0
                                                        || definition.HasBody != true ||
                                                        (definition.Attributes & (int)MethodAttributes.Static) != 0
                                                        || (definition.Attributes &
                                                            (int)MethodAttributes.MemberAccessMask) !=
                                                        (int)MethodAttributes.Public)
                throw new InvalidOperationException(
                    "The actual getter does not match the original PlayerSprite MethodDef: " + sourceMethodToken);
            var property = typeof(PlayerSprite).GetProperty(propertyName,
                               BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                           ?? throw new MissingMemberException(typeof(PlayerSprite).FullName, propertyName);
            var actual = property.GetMethod;
            if (actual == null || actual.DeclaringType != typeof(PlayerSprite) || actual.IsStatic
                || actual.ReturnType != actualReturnType || actual.GetParameters().Length != 0
                || actual.ContainsGenericParameters || actual.GetMethodBody() == null)
                throw new InvalidOperationException("The complete PlayerSprite getter lost its physical ABI: " +
                                                    propertyName);
            methods.BindActualMethod(GameAssembly, definition.MethodDefinitionToken, actual, null);
            if (!ReferenceEquals(methods.RequireOriginalMethodDefinition(actual), definition)
                || methods.RequireActualMethod(GameAssembly, sourceMethodToken) != actual)
                throw new InvalidOperationException(
                    "The PlayerSprite getter has no exact forward and reverse original method binding.");
        }
    }
}
