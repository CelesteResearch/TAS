using System;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Everest.Reflection;
using Celeste.Game.Execution.Levels;
using Celeste.Mods.Tas;
using Celeste.Mods.Tas.Interop;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment
    {
        private bool sourceTasCollabLobbyBindingsPublished;

        // Called once after the complete original TAS PE catalogs are published, before
        // source Initialize enumeration invokes CollabUtils2Interop.Initialize.
        internal void PublishSourceTasCollabLobbyBindings(TasPlaybackDriver driver)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (!ReferenceEquals(driver.Process, process))
                throw new ArgumentException("The original static Lobby members require this actual TAS process.",
                    nameof(driver));
            if (sourceTasCollabLobbyBindingsPublished)
                throw new InvalidOperationException(
                    "The same-process Collab Lobby source bindings are already published.");

            const string assemblyIdentity =
                "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            const string sourceTypeIdentity = "TAS.ModInterop.CollabUtils2Interop+Lobby";
            var reflection = ((ISourceEverestReflectionEnvironment)process).Reflection;
            var members = reflection.SourceMembers;
            var methods = reflection.SourceMethods;
            Type actualType = typeof(SourceTasCollabLobbyInterop);
            var owner = driver.CollabUtils2.Lobby;
            var definition = members.GetOriginalTypeDefinition(assemblyIdentity, sourceTypeIdentity);
            if (definition.MetadataToken != 33554780)
                throw new InvalidOperationException(
                    "The actual Collab source type does not match the published original TAS PE.");
            if (!members.ContainsSourceType(actualType))
                members.RegisterSourceType(actualType, definition.AssemblyIdentity, definition.Identity);
            else if (!ReferenceEquals(members.GetOriginalTypeDefinition(actualType), definition))
                throw new InvalidOperationException(
                    "The actual Lobby type already has a different original source identity.");

            int fieldCount = 0;
            foreach (var field in members.GetOriginalMemberDefinitions(assemblyIdentity, sourceTypeIdentity))
            {
                if (field.Kind != SourceMemberKind.Field) continue;
                if (!field.IsPublic || !field.IsStatic || field.IsInitOnly || field.IsLiteral)
                    throw new InvalidOperationException(
                        "The original Lobby import field is not a mutable public static field.");
                string actualName;
                switch (field.MetadataToken)
                {
                    case 67109867 when field.Name == "IsCollabLevelSet":
                        actualName = nameof(SourceTasCollabLobbyInterop.IsCollabLevelSet);
                        break;
                    case 67109868 when field.Name == "IsCollabLobby":
                        actualName = nameof(SourceTasCollabLobbyInterop.IsCollabLobby);
                        break;
                    default:
                        throw new InvalidOperationException(
                            "The original Lobby field has no complete actual source implementation: " +
                            field.QualifiedName);
                }

                var actualField = actualType.GetField(actualName,
                                      BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                                  ?? throw new MissingFieldException(actualType.FullName, actualName);
                if (actualField.FieldType != typeof(Func<string, bool>) || actualField.IsStatic)
                    throw new InvalidOperationException(
                        "The actual Lobby field does not have its original imported delegate ABI.");
                members.RegisterBinding(new SourcePhysicalMemberBinding(field, actualType, actualField,
                    staticSourceOwner: owner));
                fieldCount++;
            }

            if (fieldCount != 2)
                throw new InvalidOperationException(
                    "The complete original Lobby import field selection is not published.");

            int publicStaticMethodCount = 0;
            foreach (var method in methods.EnumerateDeclaredDefinitions(assemblyIdentity, sourceTypeIdentity))
            {
                var attributes = (MethodAttributes)method.Attributes;
                if ((attributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Public ||
                    (attributes & MethodAttributes.Static) == 0) continue;
                MethodInfo actualMethod;
                switch (method.MethodDefinitionToken)
                {
                    case 100665853 when method.Name == "TryGetActiveWarps":
                        actualMethod = actualType.GetMethod(nameof(SourceTasCollabLobbyInterop.TryGetActiveWarps),
                            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly, null,
                            new[] { typeof(OriginalLevelSceneRoot), typeof(string[]).MakeByRefType() }, null);
                        break;
                    case 100665854 when method.Name == "ClearLobbyMap":
                        actualMethod = actualType.GetMethod(nameof(SourceTasCollabLobbyInterop.ClearLobbyMap),
                            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly, null,
                            new[] { typeof(string) }, null);
                        break;
                    default:
                        throw new InvalidOperationException(
                            "The original Lobby public static method has no complete actual implementation: " +
                            method.Name);
                }

                if (actualMethod == null) throw new MissingMethodException(actualType.FullName, method.Name);
                methods.BindActualMethod(assemblyIdentity, method.MethodDefinitionToken, actualMethod, owner);
                publicStaticMethodCount++;
            }

            if (publicStaticMethodCount != 2)
                throw new InvalidOperationException(
                    "The complete original Lobby public static method selection is not published.");
            sourceTasCollabLobbyBindingsPublished = true;
        }
    }
}
