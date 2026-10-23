using System;
using Celeste.Core.Reflection.Members;

namespace Celeste.Mods.Tas.Reflection.Metadata;

public static partial class SourceTasMemberMetadata
{
    private static void PublishTas009(SourceMemberCatalog catalog)
    {
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MButtons", 67109092, "X2", SourceMemberKind.Field, 32854, "TAS.Input.Commands.MButtons",
            -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), true,
            SourceMetadataConstant.Decode("System.Int32", new byte[] { 0x04, 0x00, 0x00, 0x00 }), "System.Int32",
            "04000000", "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.Input.Commands.MButtons")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PlayCommand", 33554518, 1048961,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableContextAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x01, 0x00, 0x00 }),
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PlayCommand+PlayMeta", 33554730, 1048579,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            new string[] { "TAS.Input.ITasCommandMeta" },
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, null,
            new string[] { "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
            Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
            {
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    typeIdentity: "TAS.Input.ITasCommandMeta")
            }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PlayCommand+PlayMeta", 385876347, "Insert", SourceMemberKind.Property, 0,
            "System.String", 2534, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PlayCommand+PlayMeta", 385876348, "HasArguments", SourceMemberKind.Property, 0,
            "System.Boolean", 2534, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PlayCommand+PlayMeta+<GetAutoCompleteEntries>d__5", 33555077, 1048835,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            new string[]
            {
                "System.Collections.Generic.IEnumerator`1<StudioCommunication.CommandAutoCompleteEntry>",
                "System.Collections.IEnumerator", "System.IDisposable"
            },
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "System.Void System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, null,
            new string[]
            {
                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
            }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
            {
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                    elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Collections.Generic.IEnumerator`1"),
                    genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                    {
                        new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                            typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")
                    }),
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Collections.IEnumerator"),
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.IDisposable")
            }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PlayCommand+PlayMeta+<GetAutoCompleteEntries>d__5", 67110911, "<>1__state",
            SourceMemberKind.Field, 1, "System.Int32", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null,
            null, null, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PlayCommand+PlayMeta+<GetAutoCompleteEntries>d__5", 67110912, "<>2__current",
            SourceMemberKind.Field, 1, "StudioCommunication.CommandAutoCompleteEntry", -1, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PlayCommand+PlayMeta+<GetAutoCompleteEntries>d__5", 67110913, "args",
            SourceMemberKind.Field, 6, "System.String[]", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.String"), arrayRank: 1,
                arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                    { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PlayCommand+PlayMeta+<GetAutoCompleteEntries>d__5", 67110914, "filePath",
            SourceMemberKind.Field, 6, "System.String", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PlayCommand+PlayMeta+<GetAutoCompleteEntries>d__5", 67110915, "fileLine",
            SourceMemberKind.Field, 6, "System.Int32", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null,
            null, null, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PlayCommand+PlayMeta+<GetAutoCompleteEntries>d__5", 67110916, "<>7__wrap1",
            SourceMemberKind.Field, 1, "System.Collections.Generic.IEnumerator`1<System.String>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Collections.Generic.IEnumerator`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PlayCommand+PlayMeta+<GetAutoCompleteEntries>d__5", 385876475,
            "System.Collections.Generic.IEnumerator<StudioCommunication.CommandAutoCompleteEntry>.Current",
            SourceMemberKind.Property, 0, "StudioCommunication.CommandAutoCompleteEntry", 2529, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PlayCommand+PlayMeta+<GetAutoCompleteEntries>d__5", 385876476,
            "System.Collections.IEnumerator.Current", SourceMemberKind.Property, 0, "System.Object", 2529, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PressCommand", 33554519, 1048961,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableContextAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x01, 0x00, 0x00 }),
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PressCommand", 67109093, "PressKeys", SourceMemberKind.Field, 54,
            "System.Collections.Generic.HashSet`1<Microsoft.Xna.Framework.Input.Keys>", -1, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Collections, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Collections, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Collections.Generic.HashSet`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Microsoft.Xna.Framework.Input.Keys")
                })));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PressCommand+PressMeta", 33554731, 1048579,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            new string[] { "TAS.Input.ITasCommandMeta" },
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, null,
            new string[] { "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
            Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
            {
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    typeIdentity: "TAS.Input.ITasCommandMeta")
            }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PressCommand+PressMeta", 385876349, "Insert", SourceMemberKind.Property, 0,
            "System.String", 2534, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PressCommand+PressMeta", 385876350, "HasArguments", SourceMemberKind.Property, 0,
            "System.Boolean", 2534, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PressCommand+PressMeta+<GetAutoCompleteEntries>d__4", 33555078, 1048835,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            new string[]
            {
                "System.Collections.Generic.IEnumerator`1<StudioCommunication.CommandAutoCompleteEntry>",
                "System.Collections.IEnumerator", "System.IDisposable"
            },
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "System.Void System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, null,
            new string[]
            {
                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
            }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
            {
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                    elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Collections.Generic.IEnumerator`1"),
                    genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                    {
                        new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                            typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")
                    }),
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Collections.IEnumerator"),
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.IDisposable")
            }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PressCommand+PressMeta+<GetAutoCompleteEntries>d__4", 67110917, "<>1__state",
            SourceMemberKind.Field, 1, "System.Int32", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null,
            null, null, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PressCommand+PressMeta+<GetAutoCompleteEntries>d__4", 67110918, "<>2__current",
            SourceMemberKind.Field, 1, "StudioCommunication.CommandAutoCompleteEntry", -1, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PressCommand+PressMeta+<GetAutoCompleteEntries>d__4", 67110919, "args",
            SourceMemberKind.Field, 6, "System.String[]", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.String"), arrayRank: 1,
                arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                    { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PressCommand+PressMeta+<GetAutoCompleteEntries>d__4", 67110920, "<>7__wrap1",
            SourceMemberKind.Field, 1, "Microsoft.Xna.Framework.Input.Keys[]", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null, "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity: "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                    typeIdentity: "Microsoft.Xna.Framework.Input.Keys"), arrayRank: 1,
                arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                    { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PressCommand+PressMeta+<GetAutoCompleteEntries>d__4", 67110921, "<>7__wrap2",
            SourceMemberKind.Field, 1, "System.Int32", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null,
            null, null, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PressCommand+PressMeta+<GetAutoCompleteEntries>d__4", 385876477,
            "System.Collections.Generic.IEnumerator<StudioCommunication.CommandAutoCompleteEntry>.Current",
            SourceMemberKind.Property, 0, "StudioCommunication.CommandAutoCompleteEntry", 2529, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.PressCommand+PressMeta+<GetAutoCompleteEntries>d__4", 385876478,
            "System.Collections.IEnumerator.Current", SourceMemberKind.Property, 0, "System.Object", 2529, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand", 33554520, 1048961,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableContextAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x01, 0x00, 0x00 }),
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand", 67109094, "readCommandStack", SourceMemberKind.Field, 49,
            "System.Collections.Generic.List`1<System.String>", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(),
            false, null, null, null,
            "System.Collections, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Collections, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Collections.Generic.List`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String")
                })));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta", 33554732, 1048579,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            new string[] { "TAS.Input.ITasCommandMeta" },
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, null,
            new string[] { "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
            Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
            {
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    typeIdentity: "TAS.Input.ITasCommandMeta")
            }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta", 385876351, "Insert", SourceMemberKind.Property, 0,
            "System.String", 2534, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta", 385876352, "HasArguments", SourceMemberKind.Property, 0,
            "System.Boolean", 2534, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<>O", 33555079, 1048963,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "System.Void System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<>O", 67110922, "<0>__GetFileName", SourceMemberKind.Field, 22,
            "System.Func`2<System.String,System.String>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Func`2"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"),
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String")
                })));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<>c", 33555080, 1057027,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "System.Void System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<>c", 67110923, "<>9", SourceMemberKind.Field, 54,
            "TAS.Input.Commands.ReadCommand+ReadMeta+<>c", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.Input.Commands.ReadCommand+ReadMeta+<>c")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<>c", 67110924, "<>9__4_0", SourceMemberKind.Field, 22,
            "System.Func`3<System.Int32,System.String,System.Int32>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Func`3"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"),
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"),
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<>c", 67110925, "<>9__4_1", SourceMemberKind.Field, 22,
            "System.Func`3<System.Int32,System.String,System.Int32>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Func`3"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"),
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"),
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<>c", 67110926, "<>9__4_2", SourceMemberKind.Field, 22,
            "System.Func`3<System.Int32,System.String,System.Int32>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Func`3"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"),
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"),
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32")
                })));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<GetAutoCompleteEntries>d__5", 33555081, 1048835,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            new string[]
            {
                "System.Collections.Generic.IEnumerator`1<StudioCommunication.CommandAutoCompleteEntry>",
                "System.Collections.IEnumerator", "System.IDisposable"
            },
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "System.Void System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, null,
            new string[]
            {
                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
            }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
            {
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                    elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Collections.Generic.IEnumerator`1"),
                    genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                    {
                        new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                            typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")
                    }),
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Collections.IEnumerator"),
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.IDisposable")
            }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<GetAutoCompleteEntries>d__5", 67110927, "<>1__state",
            SourceMemberKind.Field, 1, "System.Int32", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null,
            null, null, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<GetAutoCompleteEntries>d__5", 67110928, "<>2__current",
            SourceMemberKind.Field, 1, "StudioCommunication.CommandAutoCompleteEntry", -1, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<GetAutoCompleteEntries>d__5", 67110929, "filePath",
            SourceMemberKind.Field, 6, "System.String", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<GetAutoCompleteEntries>d__5", 67110930, "args",
            SourceMemberKind.Field, 6, "System.String[]", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.String"), arrayRank: 1,
                arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                    { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<GetAutoCompleteEntries>d__5", 67110931, "<targetDir>5__2",
            SourceMemberKind.Field, 1, "System.String", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<GetAutoCompleteEntries>d__5", 67110932, "<prefix>5__3",
            SourceMemberKind.Field, 1, "System.String", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<GetAutoCompleteEntries>d__5", 67110933, "<>7__wrap3",
            SourceMemberKind.Field, 1, "System.Collections.Generic.IEnumerator`1<System.String>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Collections.Generic.IEnumerator`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<GetAutoCompleteEntries>d__5", 67110934,
            "<afterStartingLabel>5__5", SourceMemberKind.Field, 1, "System.Boolean", -1, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<GetAutoCompleteEntries>d__5", 67110935, "<>7__wrap5",
            SourceMemberKind.Field, 1, "System.String[]", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.String"), arrayRank: 1,
                arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                    { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<GetAutoCompleteEntries>d__5", 67110936, "<>7__wrap6",
            SourceMemberKind.Field, 1, "System.Int32", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null,
            null, null, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<GetAutoCompleteEntries>d__5", 385876479,
            "System.Collections.Generic.IEnumerator<StudioCommunication.CommandAutoCompleteEntry>.Current",
            SourceMemberKind.Property, 0, "StudioCommunication.CommandAutoCompleteEntry", 2529, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ReadCommand+ReadMeta+<GetAutoCompleteEntries>d__5", 385876480,
            "System.Collections.IEnumerator.Current", SourceMemberKind.Property, 0, "System.Object", 2529, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RecordingCommand", 33554521, 1048960,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableContextAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x01, 0x00, 0x00 }),
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RecordingCommand", 67109095, "RecordingTimes", SourceMemberKind.Field, 51,
            "System.Collections.Generic.Dictionary`2<System.Int32,TAS.Input.Commands.RecordingCommand+RecordingTime>",
            -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Collections, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Collections, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Collections.Generic.Dictionary`2"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"),
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Commands.RecordingCommand+RecordingTime")
                })));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RecordingCommand+RecordingTime", 33554733, 1048581,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            new string[] { "System.IEquatable`1<TAS.Input.Commands.RecordingCommand+RecordingTime>" },
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, null,
            new string[] { "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" },
            Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
            {
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                    elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.IEquatable`1"),
                    genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                    {
                        new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                            typeIdentity: "TAS.Input.Commands.RecordingCommand+RecordingTime")
                    })
            }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RecordingCommand+RecordingTime", 67109724, "StartFrame", SourceMemberKind.Field, 6,
            "System.Int32", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RecordingCommand+RecordingTime", 67109725, "StopFrame", SourceMemberKind.Field, 6,
            "System.Int32", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RecordingCommand+RecordingTime", 385876353, "EqualityContract",
            SourceMemberKind.Property, 0, "System.Type", 2500, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "System.Void System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RecordingCommand+RecordingTime", 385876354, "Duration", SourceMemberKind.Property, 0,
            "System.Int32", 2182, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RepeatCommand", 33554522, 1048960,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableContextAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x01, 0x00, 0x00 }),
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RepeatCommand", 67109096, "repeatStack", SourceMemberKind.Field, 49,
            "System.Collections.Generic.Stack`1<TAS.Input.Commands.RepeatCommand+Arguments>", -1, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Collections, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Collections, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Collections.Generic.Stack`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Commands.RepeatCommand+Arguments")
                })));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RepeatCommand+Meta", 33554734, 1048579,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            new string[] { "TAS.Input.ITasCommandMeta" },
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableContextAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, null,
            new string[] { "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
            Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
            {
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    typeIdentity: "TAS.Input.ITasCommandMeta")
            }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RepeatCommand+Meta", 385876355, "Insert", SourceMemberKind.Property, 0, "System.String",
            2534, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x01, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RepeatCommand+Meta", 385876356, "HasArguments", SourceMemberKind.Property, 0,
            "System.Boolean", 2534, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RepeatCommand+Arguments", 33554735, 1048843,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ValueType",
            new string[] { "System.IEquatable`1<TAS.Input.Commands.RepeatCommand+Arguments>" },
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 }),
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.IsReadOnlyAttribute",
                    "System.Void System.Runtime.CompilerServices.IsReadOnlyAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, null,
            new string[] { "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" },
            Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.ValueType"),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
            {
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                    elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.IEquatable`1"),
                    genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                    {
                        new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                            typeIdentity: "TAS.Input.Commands.RepeatCommand+Arguments")
                    })
            }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RepeatCommand+Arguments", 67109726, "<StartFrame>k__BackingField",
            SourceMemberKind.Field, 33, "System.Int32", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "System.Void System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RepeatCommand+Arguments", 67109727, "<Count>k__BackingField", SourceMemberKind.Field,
            33, "System.Int32", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "System.Void System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RepeatCommand+Arguments", 67109728, "<StartFilePath>k__BackingField",
            SourceMemberKind.Field, 33, "System.String", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "System.Void System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RepeatCommand+Arguments", 67109729, "<StartFileLine>k__BackingField",
            SourceMemberKind.Field, 33, "System.Int32", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "System.Void System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RepeatCommand+Arguments", 385876357, "StartFrame", SourceMemberKind.Property, 0,
            "System.Int32", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RepeatCommand+Arguments", 385876358, "Count", SourceMemberKind.Property, 0,
            "System.Int32", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RepeatCommand+Arguments", 385876359, "StartFilePath", SourceMemberKind.Property, 0,
            "System.String", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RepeatCommand+Arguments", 385876360, "StartFileLine", SourceMemberKind.Property, 0,
            "System.Int32", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand", 33554523, 1048960,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableContextAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x01, 0x00, 0x00 }),
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand", 67109097, "CommandName", SourceMemberKind.Field, 32854,
            "System.String", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), true,
            SourceMetadataConstant.Decode("System.String",
                new byte[]
                {
                    0x52, 0x00, 0x65, 0x00, 0x71, 0x00, 0x75, 0x00, 0x69, 0x00, 0x72, 0x00, 0x65, 0x00, 0x44, 0x00,
                    0x65, 0x00, 0x70, 0x00, 0x65, 0x00, 0x6E, 0x00, 0x64, 0x00, 0x65, 0x00, 0x6E, 0x00, 0x63, 0x00,
                    0x79, 0x00
                }), "System.String", "520065007100750069007200650044006500700065006E00640065006E0063007900",
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand", 67109098, "missingDependencies", SourceMemberKind.Field, 49,
            "System.Collections.Generic.Dictionary`2<System.String,System.Version>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x03, 0x00, 0x00, 0x00, 0x01, 0x01, 0x02, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Collections, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Collections, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Collections.Generic.Dictionary`2"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"),
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Version")
                })));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta", 33554736, 1048579,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            new string[] { "TAS.Input.ITasCommandMeta" },
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, null,
            new string[] { "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
            Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
            {
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    typeIdentity: "TAS.Input.ITasCommandMeta")
            }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta", 385876361, "Insert", SourceMemberKind.Property, 0,
            "System.String", 2534, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta", 385876362, "HasArguments", SourceMemberKind.Property, 0,
            "System.Boolean", 2534, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta+<>c__DisplayClass4_0", 33555082, 1048835,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "System.Void System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta+<>c__DisplayClass4_0", 67110937, "args",
            SourceMemberKind.Field, 6, "System.String[]", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.String"), arrayRank: 1,
                arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                    { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) })));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta+<>c__DisplayClass4_1", 33555083, 1048835,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "System.Void System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta+<>c__DisplayClass4_1", 67110938, "mapModAsset",
            SourceMemberKind.Field, 6, "Celeste.Mod.ModAsset", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ModAsset")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta+<GetAutoCompleteEntries>d__4", 33555084, 1048835,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            new string[]
            {
                "System.Collections.Generic.IEnumerator`1<StudioCommunication.CommandAutoCompleteEntry>",
                "System.Collections.IEnumerator", "System.IDisposable"
            },
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "System.Void System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, null,
            new string[]
            {
                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
            }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
            {
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                    elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Collections.Generic.IEnumerator`1"),
                    genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                    {
                        new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                            typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")
                    }),
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Collections.IEnumerator"),
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.IDisposable")
            }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta+<GetAutoCompleteEntries>d__4", 67110939, "<>1__state",
            SourceMemberKind.Field, 1, "System.Int32", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null,
            null, null, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta+<GetAutoCompleteEntries>d__4", 67110940, "<>2__current",
            SourceMemberKind.Field, 1, "StudioCommunication.CommandAutoCompleteEntry", -1, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta+<GetAutoCompleteEntries>d__4", 67110941, "args",
            SourceMemberKind.Field, 6, "System.String[]", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.String"), arrayRank: 1,
                arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                    { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta+<GetAutoCompleteEntries>d__4", 67110942,
            "<bestGuess>5__2", SourceMemberKind.Field, 1, "System.String", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta+<GetAutoCompleteEntries>d__4", 67110943, "<>7__wrap2",
            SourceMemberKind.Field, 1, "System.Collections.Generic.IEnumerator`1<Celeste.Mod.EverestModule>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Collections.Generic.IEnumerator`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Celeste.Mod.EverestModule")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta+<GetAutoCompleteEntries>d__4", 385876481,
            "System.Collections.Generic.IEnumerator<StudioCommunication.CommandAutoCompleteEntry>.Current",
            SourceMemberKind.Property, 0, "StudioCommunication.CommandAutoCompleteEntry", 2529, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+Meta+<GetAutoCompleteEntries>d__4", 385876482,
            "System.Collections.IEnumerator.Current", SourceMemberKind.Property, 0, "System.Object", 2529, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+OuiInstallDependenciesConfirmation", 33554737, 1048579,
            "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Celeste.Oui", Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Oui"), new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+OuiInstallDependenciesConfirmation", 67109730, "onScreenX",
            SourceMemberKind.Field, 32849, "System.Single", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), true,
            SourceMetadataConstant.Decode("System.Single", new byte[] { 0x00, 0x00, 0x70, 0x44 }), "System.Single",
            "00007044", "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Single")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+OuiInstallDependenciesConfirmation", 67109731, "offScreenX",
            SourceMemberKind.Field, 32849, "System.Single", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), true,
            SourceMetadataConstant.Decode("System.Single", new byte[] { 0x00, 0x00, 0x34, 0x45 }), "System.Single",
            "00003445", "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Single")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+OuiInstallDependenciesConfirmation", 67109732, "menu",
            SourceMemberKind.Field, 33, "Celeste.TextMenu", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false,
            null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.TextMenu")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.RequireDependencyCommand+OuiInstallDependenciesConfirmation", 67109733, "alpha",
            SourceMemberKind.Field, 1, "System.Single", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null,
            null, null, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Single")));
    }
}
