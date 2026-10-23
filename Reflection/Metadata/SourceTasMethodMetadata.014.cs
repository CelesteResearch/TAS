using System.Collections.Generic;
using Celeste.Core.Reflection.Methods;

namespace Celeste.Mods.Tas.Reflection.Metadata;

public static partial class SourceTasMethodMetadata
{
    private static void PublishTas014(SourceAssemblyMethodCatalog catalog)
    {
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+Custom", 33555111, 100667238, "ToString", 198, "20000E",
            new KeyValuePair<string, string>[] { }, "System.String", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+Custom", 33555111, 100667239, "PrintMembers", 196, "2001021280AD",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Text.StringBuilder" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("builder", 0, 0, "System.Text.StringBuilder", false, null,
                    new byte[] { }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Text.StringBuilder"), 1, 134221603)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 19, 48));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+Custom", 33555111, 100667240, "op_Inequality", 2198,
            "000202128A9C128A9C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[] { "TAS.InfoHUD.TargetQuery+QueryError+Custom", "TAS.InfoHUD.TargetQuery+QueryError+Custom" },
            0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0, "TAS.InfoHUD.TargetQuery+QueryError+Custom", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+Custom"), 1, 134221604),
                new SourceMethodParameterDefinition("right", 1, 0, "TAS.InfoHUD.TargetQuery+QueryError+Custom", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+Custom"), 2, 134221605)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 6, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+Custom", 33555111, 100667241, "op_Equality", 2198, "000202128A9C128A9C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[] { "TAS.InfoHUD.TargetQuery+QueryError+Custom", "TAS.InfoHUD.TargetQuery+QueryError+Custom" },
            0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0, "TAS.InfoHUD.TargetQuery+QueryError+Custom", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+Custom"), 1, 134221606),
                new SourceMethodParameterDefinition("right", 1, 0, "TAS.InfoHUD.TargetQuery+QueryError+Custom", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+Custom"), 2, 134221607)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 13, 19));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+Custom", 33555111, 100667242, "GetHashCode", 198, "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 10, 30));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+Custom", 33555111, 100667243, "Equals", 198, "2001021C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Object" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("obj", 0, 0, "System.Object", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object"), 1, 134221608)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 13));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+Custom", 33555111, 100667244, "Equals", 230, "200102128600",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+QueryError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+QueryError", false, null,
                    new byte[] { }, "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError"), 1, 134221609)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+Custom", 33555111, 100667245, "Equals", 454, "200102128A9C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+QueryError+Custom" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+QueryError+Custom", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+Custom"), 1, 134221610)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 18, 40));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+Custom", 33555111, 100667246, "<Clone>$", 454, "2000128A9C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.PreserveBaseOverridesAttribute",
                    "01000000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "TAS.InfoHUD.TargetQuery+QueryError+Custom", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+Custom"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+Custom", 33555111, 100667247, ".ctor", 6276, "200101128A9C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "TAS.InfoHUD.TargetQuery+QueryError+Custom" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("original", 0, 0, "TAS.InfoHUD.TargetQuery+QueryError+Custom",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+Custom"), 1, 134221611)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 8, 20));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+Custom", 33555111, 100667248, "Deconstruct", 134, "200101100E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "System.String&" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("ErrorMessage", 0, 2, "System.String&", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.ByReference,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String")), 1, 134221612)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 5, 9));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667249, ".ctor", 6278, "2002011280C90E",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { "System.Type", "System.String" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("EnumType", 0, 0, "System.Type", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Type"), 1, 134221613),
                new SourceMethodParameterDefinition("Member", 1, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 2, 134221614)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 9, 21));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667250, "get_EqualityContract", 2244,
            "20001280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Type", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667251, "get_EnumType", 2182,
            "20001280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Type", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667252, "set_EnumType", 2182,
            "20011F80A9011280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void modreq(System.Runtime.CompilerServices.IsExternalInit)", new string[] { "System.Type" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("value", 0, 0, "System.Type", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Type"), 1, 134221615)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.RequiredModifier,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Void"),
                modifierType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Runtime.CompilerServices.IsExternalInit")), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667253, "get_Member", 2182, "20000E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.String", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667254, "set_Member", 2182,
            "20011F80A9010E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void modreq(System.Runtime.CompilerServices.IsExternalInit)", new string[] { "System.String" },
            0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("value", 0, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 1, 134221616)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.RequiredModifier,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Void"),
                modifierType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Runtime.CompilerServices.IsExternalInit")), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667255, "ToString", 198, "20000E",
            new KeyValuePair<string, string>[] { }, "System.String", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 24, 80));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667256, "PrintMembers", 196,
            "2001021280AD",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Text.StringBuilder" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("builder", 0, 0, "System.Text.StringBuilder", false, null,
                    new byte[] { }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Text.StringBuilder"), 1, 134221617)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 28, 73));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667257, "op_Inequality", 2198,
            "000202128AA0128AA0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState",
                "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0, "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState"), 1, 134221618),
                new SourceMethodParameterDefinition("right", 1, 0,
                    "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState"), 2, 134221619)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 6, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667258, "op_Equality", 2198,
            "000202128AA0128AA0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState",
                "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0, "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState"), 1, 134221620),
                new SourceMethodParameterDefinition("right", 1, 0,
                    "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState"), 2, 134221621)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 13, 19));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667259, "GetHashCode", 198, "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 17, 53));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667260, "Equals", 198, "2001021C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Object" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("obj", 0, 0, "System.Object", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object"), 1, 134221622)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 13));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667261, "Equals", 230, "200102128600",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+QueryError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+QueryError", false, null,
                    new byte[] { }, "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError"), 1, 134221623)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667262, "Equals", 454, "200102128AA0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0,
                    "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState"), 1, 134221624)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 25, 64));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667263, "<Clone>$", 454, "2000128AA0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.PreserveBaseOverridesAttribute",
                    "01000000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667264, ".ctor", 6276, "200101128AA0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("original", 0, 0,
                    "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState"), 1, 134221625)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 12, 32));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InvalidEnumState", 33555112, 100667265, "Deconstruct", 134,
            "200201101280C9100E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "System.Type&", "System.String&" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("EnumType", 0, 2, "System.Type&", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.ByReference,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.Type")), 1, 134221626),
                new SourceMethodParameterDefinition("Member", 1, 2, "System.String&", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.ByReference,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String")), 2, 134221627)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 9, 17));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667266, ".ctor", 6278,
            "2003010E0808", new KeyValuePair<string, string>[] { }, "System.Void",
            new string[] { "System.String", "System.Int32", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("Query", 0, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 1, 134221628),
                new SourceMethodParameterDefinition("ExpectedCount", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134221629),
                new SourceMethodParameterDefinition("ActualCount", 2, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 3, 134221630)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 12, 28));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667267, "get_EqualityContract",
            2244, "20001280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Type", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667268, "get_Query", 2182,
            "20000E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.String", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667269, "set_Query", 2182,
            "20011F80A9010E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void modreq(System.Runtime.CompilerServices.IsExternalInit)", new string[] { "System.String" },
            0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("value", 0, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 1, 134221631)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.RequiredModifier,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Void"),
                modifierType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Runtime.CompilerServices.IsExternalInit")), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667270, "get_ExpectedCount", 2182,
            "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667271, "set_ExpectedCount", 2182,
            "20011F80A90108",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void modreq(System.Runtime.CompilerServices.IsExternalInit)", new string[] { "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("value", 0, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 1, 134221632)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.RequiredModifier,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Void"),
                modifierType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Runtime.CompilerServices.IsExternalInit")), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667272, "get_ActualCount", 2182,
            "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667273, "set_ActualCount", 2182,
            "20011F80A90108",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void modreq(System.Runtime.CompilerServices.IsExternalInit)", new string[] { "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("value", 0, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 1, 134221633)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.RequiredModifier,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Void"),
                modifierType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Runtime.CompilerServices.IsExternalInit")), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667274, "ToString", 198, "20000E",
            new KeyValuePair<string, string>[] { }, "System.String", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 90, 292));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667275, "PrintMembers", 196,
            "2001021280AD",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Text.StringBuilder" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("builder", 0, 0, "System.Text.StringBuilder", false, null,
                    new byte[] { }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Text.StringBuilder"), 1, 134221634)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 45, 126));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667276, "op_Inequality", 2198,
            "000202128AA4128AA4",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch",
                "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0,
                    "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch"), 1, 134221635),
                new SourceMethodParameterDefinition("right", 1, 0,
                    "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch"), 2, 134221636)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 6, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667277, "op_Equality", 2198,
            "000202128AA4128AA4",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch",
                "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0,
                    "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch"), 1, 134221637),
                new SourceMethodParameterDefinition("right", 1, 0,
                    "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch"), 2, 134221638)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 13, 19));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667278, "GetHashCode", 198,
            "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 24, 76));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667279, "Equals", 198, "2001021C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Object" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("obj", 0, 0, "System.Object", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object"), 1, 134221639)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 13));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667280, "Equals", 230,
            "200102128600",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+QueryError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+QueryError", false, null,
                    new byte[] { }, "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError"), 1, 134221640)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667281, "Equals", 454,
            "200102128AA4",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0,
                    "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch"), 1, 134221641)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 32, 88));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667282, "<Clone>$", 454,
            "2000128AA4",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.PreserveBaseOverridesAttribute",
                    "01000000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667283, ".ctor", 6276,
            "200101128AA4",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("original", 0, 0,
                    "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch"), 1, 134221642)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 16, 44));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+InstanceCountMismatch", 33555113, 100667284, "Deconstruct", 134,
            "200301100E10081008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "System.String&", "System.Int32&", "System.Int32&" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("Query", 0, 2, "System.String&", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.ByReference,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String")), 1, 134221643),
                new SourceMethodParameterDefinition("ExpectedCount", 1, 2, "System.Int32&", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.ByReference,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.Int32")), 2, 134221644),
                new SourceMethodParameterDefinition("ActualCount", 2, 2, "System.Int32&", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.ByReference,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.Int32")), 3, 134221645)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 13, 25));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667285, ".ctor", 6278, "2002010E1280C9",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { "System.String", "System.Type" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("Argument", 0, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 1, 134221646),
                new SourceMethodParameterDefinition("TargetType", 1, 0, "System.Type", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Type"), 2, 134221647)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 9, 21));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667286, "get_EqualityContract", 2244,
            "20001280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Type", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667287, "get_Argument", 2182, "20000E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.String", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667288, "set_Argument", 2182,
            "20011F80A9010E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void modreq(System.Runtime.CompilerServices.IsExternalInit)", new string[] { "System.String" },
            0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("value", 0, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 1, 134221648)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.RequiredModifier,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Void"),
                modifierType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Runtime.CompilerServices.IsExternalInit")), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667289, "get_TargetType", 2182,
            "20001280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Type", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667290, "set_TargetType", 2182,
            "20011F80A9011280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void modreq(System.Runtime.CompilerServices.IsExternalInit)", new string[] { "System.Type" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("value", 0, 0, "System.Type", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Type"), 1, 134221649)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.RequiredModifier,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Void"),
                modifierType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Runtime.CompilerServices.IsExternalInit")), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667291, "ToString", 198, "20000E",
            new KeyValuePair<string, string>[] { }, "System.String", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 24, 80));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667292, "PrintMembers", 196,
            "2001021280AD",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Text.StringBuilder" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("builder", 0, 0, "System.Text.StringBuilder", false, null,
                    new byte[] { }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Text.StringBuilder"), 1, 134221650)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 28, 73));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667293, "op_Inequality", 2198,
            "000202128AA8128AA8",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+QueryError+ConversionError",
                "TAS.InfoHUD.TargetQuery+QueryError+ConversionError"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0, "TAS.InfoHUD.TargetQuery+QueryError+ConversionError",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+ConversionError"), 1, 134221651),
                new SourceMethodParameterDefinition("right", 1, 0, "TAS.InfoHUD.TargetQuery+QueryError+ConversionError",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+ConversionError"), 2, 134221652)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 6, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667294, "op_Equality", 2198,
            "000202128AA8128AA8",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+QueryError+ConversionError",
                "TAS.InfoHUD.TargetQuery+QueryError+ConversionError"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0, "TAS.InfoHUD.TargetQuery+QueryError+ConversionError",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+ConversionError"), 1, 134221653),
                new SourceMethodParameterDefinition("right", 1, 0, "TAS.InfoHUD.TargetQuery+QueryError+ConversionError",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+ConversionError"), 2, 134221654)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 13, 19));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667295, "GetHashCode", 198, "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 17, 53));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667296, "Equals", 198, "2001021C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Object" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("obj", 0, 0, "System.Object", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object"), 1, 134221655)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 13));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667297, "Equals", 230, "200102128600",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+QueryError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+QueryError", false, null,
                    new byte[] { }, "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError"), 1, 134221656)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667298, "Equals", 454, "200102128AA8",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+QueryError+ConversionError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+QueryError+ConversionError",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+ConversionError"), 1, 134221657)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 25, 64));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667299, "<Clone>$", 454, "2000128AA8",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.PreserveBaseOverridesAttribute",
                    "01000000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+ConversionError"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667300, ".ctor", 6276, "200101128AA8",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "TAS.InfoHUD.TargetQuery+QueryError+ConversionError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("original", 0, 0,
                    "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError+ConversionError"), 1, 134221658)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 12, 32));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+QueryError+ConversionError", 33555114, 100667301, "Deconstruct", 134,
            "200201100E101280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "System.String&", "System.Type&" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("Argument", 0, 2, "System.String&", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.ByReference,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String")), 1, 134221659),
                new SourceMethodParameterDefinition("TargetType", 1, 2, "System.Type&", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.ByReference,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.Type")), 2, 134221660)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 9, 17));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666039, ".ctor", 6278, "2002011280C908",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { "System.Type", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("type", 0, 0, "System.Type", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Type"), 1, 134220863),
                new SourceMethodParameterDefinition("memberIndex", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134220864)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 15, 35));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666040, "get_EqualityContract", 2244,
            "20001280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Type", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666041, "get_type", 2182, "20001280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Type", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666042, "set_type", 2182, "20011F80A9011280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void modreq(System.Runtime.CompilerServices.IsExternalInit)", new string[] { "System.Type" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("value", 0, 0, "System.Type", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Type"), 1, 134220865)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.RequiredModifier,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Void"),
                modifierType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Runtime.CompilerServices.IsExternalInit")), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666043, "get_memberIndex", 2182, "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666044, "set_memberIndex", 2182, "20011F80A90108",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void modreq(System.Runtime.CompilerServices.IsExternalInit)", new string[] { "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("value", 0, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 1, 134220866)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.RequiredModifier,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Void"),
                modifierType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Runtime.CompilerServices.IsExternalInit")), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666045, "Aggregate", 150,
            "0002128604128604128604", new KeyValuePair<string, string>[] { },
            "TAS.InfoHUD.TargetQuery+MemberAccessError",
            new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError", "TAS.InfoHUD.TargetQuery+MemberAccessError" },
            0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("accum", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), 1, 134220867),
                new SourceMethodParameterDefinition("error", 1, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), 2, 134220868)
            }, "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), true, 126, 296));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666046, "ToString", 198, "20000E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.String", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 25, 64));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666047, "PrintMembers", 196, "2001021280AD",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Text.StringBuilder" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("builder", 0, 0, "System.Text.StringBuilder", false, null,
                    new byte[] { }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Text.StringBuilder"), 1, 134220869)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 52, 148));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666048, "op_Inequality", 2198,
            "000202128604128604",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError", "TAS.InfoHUD.TargetQuery+MemberAccessError" },
            0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), 1, 134220870),
                new SourceMethodParameterDefinition("right", 1, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), 2, 134220871)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 6, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666049, "op_Equality", 2198, "000202128604128604",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError", "TAS.InfoHUD.TargetQuery+MemberAccessError" },
            0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), 1, 134220872),
                new SourceMethodParameterDefinition("right", 1, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), 2, 134220873)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 13, 19));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666050, "GetHashCode", 198, "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 31, 99));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666051, "Equals", 198, "2001021C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Object" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("obj", 0, 0, "System.Object", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object"), 1, 134220874)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 13));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666052, "Equals", 230, "200102128600",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+QueryError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+QueryError", false, null,
                    new byte[] { }, "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+QueryError"), 1, 134220875)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666053, "Equals", 454, "200102128604",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), 1, 134220876)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 39, 112));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666054, "<Clone>$", 454, "2000128604",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.PreserveBaseOverridesAttribute",
                    "01000000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "TAS.InfoHUD.TargetQuery+MemberAccessError", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666055, ".ctor", 6276, "200101128604",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("original", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), 1, 134220877)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 20, 56));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError", 33554817, 100666056, "Deconstruct", 134, "200201101280C91008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "System.Type&", "System.Int32&" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("type", 0, 2, "System.Type&", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.ByReference,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.Type")), 1, 134220878),
                new SourceMethodParameterDefinition("memberIndex", 1, 2, "System.Int32&", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.ByReference,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.Int32")), 2, 134220879)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 9, 17));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", 33555115, 100667302, ".ctor", 6278, "200001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 6, 18));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", 33555115, 100667303, "get_EqualityContract", 2244,
            "20001280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Type", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", 33555115, 100667304, "ToString", 198, "20000E",
            new KeyValuePair<string, string>[] { }, "System.String", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 2, 6));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", 33555115, 100667305, "PrintMembers", 196,
            "2001021280AD",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Text.StringBuilder" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("builder", 0, 0, "System.Text.StringBuilder", false, null,
                    new byte[] { }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Text.StringBuilder"), 1, 134221661)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", 33555115, 100667306, "op_Inequality", 2198,
            "000202128AAC128AAC",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers",
                "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers"), 1, 134221662),
                new SourceMethodParameterDefinition("right", 1, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers"), 2, 134221663)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 6, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", 33555115, 100667307, "op_Equality", 2198,
            "000202128AAC128AAC",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers",
                "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers"), 1, 134221664),
                new SourceMethodParameterDefinition("right", 1, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers"), 2, 134221665)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 13, 19));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", 33555115, 100667308, "GetHashCode", 198, "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", 33555115, 100667309, "Equals", 198, "2001021C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Object" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("obj", 0, 0, "System.Object", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object"), 1, 134221666)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 13));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", 33555115, 100667310, "Equals", 230, "200102128604",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), 1, 134221667)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", 33555115, 100667311, "Equals", 454, "200102128AAC",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers"), 1, 134221668)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 9, 14));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", 33555115, 100667312, "<Clone>$", 454, "2000128AAC",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.PreserveBaseOverridesAttribute",
                    "01000000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", 33555115, 100667313, ".ctor", 6276, "200101128AAC",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("original", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+NoMembers"), 1, 134221669)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", 33555116, 100667314,
            "get_EqualityContract", 2244, "20001280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Type", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", 33555116, 100667315, ".ctor", 6278,
            "2003011280C9081D0E", new KeyValuePair<string, string>[] { }, "System.Void",
            new string[] { "System.Type", "System.Int32", "System.String[]" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("type", 0, 0, "System.Type", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Type"), 1, 134221670),
                new SourceMethodParameterDefinition("memberIndex", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134221671),
                new SourceMethodParameterDefinition("memberArgs", 2, 0, "System.String[]", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String"), arrayRank: 1,
                        arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                            { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) }), 3,
                    134221672)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 8, 16));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", 33555116, 100667316, "ToString", 198,
            "20000E", new KeyValuePair<string, string>[] { }, "System.String", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 27, 87));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", 33555116, 100667317, "PrintMembers",
            196, "2001021280AD",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Text.StringBuilder" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("builder", 0, 0, "System.Text.StringBuilder", false, null,
                    new byte[] { }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Text.StringBuilder"), 1, 134221673)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", 33555116, 100667318, "op_Inequality",
            2198, "000202128AB0128AB0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed",
                "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed"), 1,
                    134221674),
                new SourceMethodParameterDefinition("right", 1, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed"), 2,
                    134221675)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 6, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", 33555116, 100667319, "op_Equality",
            2198, "000202128AB0128AB0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed",
                "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed"), 1,
                    134221676),
                new SourceMethodParameterDefinition("right", 1, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed"), 2,
                    134221677)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 13, 19));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", 33555116, 100667320, "GetHashCode",
            198, "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 10, 30));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", 33555116, 100667321, "Equals", 198,
            "2001021C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Object" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("obj", 0, 0, "System.Object", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object"), 1, 134221678)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 13));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", 33555116, 100667322, "Equals", 230,
            "200102128604",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), 1, 134221679)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", 33555116, 100667323, "Equals", 454,
            "200102128AB0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed" },
            0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed"), 1,
                    134221680)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 18, 40));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", 33555116, 100667324, "<Clone>$", 454,
            "2000128AB0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.PreserveBaseOverridesAttribute",
                    "01000000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", 33555116, 100667325, ".ctor", 6276,
            "200101128AB0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("original", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+CodeExecutionNotAllowed"), 1,
                    134221681)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 8, 20));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", 33555117, 100667326, "get_EqualityContract",
            2244, "20001280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Type", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", 33555117, 100667327, ".ctor", 6278,
            "2004011280C9081D0E128135", new KeyValuePair<string, string>[] { }, "System.Void",
            new string[] { "System.Type", "System.Int32", "System.String[]", "System.Exception" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("type", 0, 0, "System.Type", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Type"), 1, 134221682),
                new SourceMethodParameterDefinition("memberIndex", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134221683),
                new SourceMethodParameterDefinition("memberArgs", 2, 0, "System.String[]", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String"), arrayRank: 1,
                        arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                            { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) }), 3,
                    134221684),
                new SourceMethodParameterDefinition("ex", 3, 0, "System.Exception", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Exception"), 4, 134221685)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 16, 38));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", 33555117, 100667328, "ToString", 198,
            "20000E", new KeyValuePair<string, string>[] { }, "System.String", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 31, 100));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", 33555117, 100667329, "PrintMembers", 196,
            "2001021280AD",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Text.StringBuilder" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("builder", 0, 0, "System.Text.StringBuilder", false, null,
                    new byte[] { }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Text.StringBuilder"), 1, 134221686)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", 33555117, 100667330, "op_Inequality", 2198,
            "000202128AB4128AB4",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException",
                "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException"), 1, 134221687),
                new SourceMethodParameterDefinition("right", 1, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException"), 2, 134221688)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 6, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", 33555117, 100667331, "op_Equality", 2198,
            "000202128AB4128AB4",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException",
                "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException"), 1, 134221689),
                new SourceMethodParameterDefinition("right", 1, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException"), 2, 134221690)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 13, 19));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", 33555117, 100667332, "GetHashCode", 198,
            "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 17, 53));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", 33555117, 100667333, "Equals", 198,
            "2001021C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Object" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("obj", 0, 0, "System.Object", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object"), 1, 134221691)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 13));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", 33555117, 100667334, "Equals", 230,
            "200102128604",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), 1, 134221692)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", 33555117, 100667335, "Equals", 454,
            "200102128AB4",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException"), 1, 134221693)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 25, 64));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", 33555117, 100667336, "<Clone>$", 454,
            "2000128AB4",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.PreserveBaseOverridesAttribute",
                    "01000000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", 33555117, 100667337, ".ctor", 6276,
            "200101128AB4",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("original", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+UnknownException"), 1, 134221694)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 12, 32));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", 33555118, 100667338, "get_EqualityContract", 2244,
            "20001280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Type", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", 33555118, 100667339, ".ctor", 6278, "2003011280C9080E",
            new KeyValuePair<string, string>[] { }, "System.Void",
            new string[] { "System.Type", "System.Int32", "System.String" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("type", 0, 0, "System.Type", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Type"), 1, 134221695),
                new SourceMethodParameterDefinition("memberIndex", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134221696),
                new SourceMethodParameterDefinition("errorMessage", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134221697)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 8, 16));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", 33555118, 100667340, "ToString", 198, "20000E",
            new KeyValuePair<string, string>[] { }, "System.String", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", 33555118, 100667341, "PrintMembers", 196,
            "2001021280AD",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Text.StringBuilder" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("builder", 0, 0, "System.Text.StringBuilder", false, null,
                    new byte[] { }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Text.StringBuilder"), 1, 134221698)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", 33555118, 100667342, "op_Inequality", 2198,
            "000202128AB8128AB8",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom"), 1, 134221699),
                new SourceMethodParameterDefinition("right", 1, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom"), 2, 134221700)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 6, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", 33555118, 100667343, "op_Equality", 2198,
            "000202128AB8128AB8",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom"), 1, 134221701),
                new SourceMethodParameterDefinition("right", 1, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom"), 2, 134221702)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 13, 19));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", 33555118, 100667344, "GetHashCode", 198, "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 10, 30));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", 33555118, 100667345, "Equals", 198, "2001021C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Object" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("obj", 0, 0, "System.Object", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object"), 1, 134221703)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 13));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", 33555118, 100667346, "Equals", 230, "200102128604",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), 1, 134221704)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", 33555118, 100667347, "Equals", 454, "200102128AB8",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom"), 1, 134221705)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 18, 40));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", 33555118, 100667348, "<Clone>$", 454, "2000128AB8",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.PreserveBaseOverridesAttribute",
                    "01000000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", 33555118, 100667349, ".ctor", 6276, "200101128AB8",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("original", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+Custom"), 1, 134221706)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 8, 20));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", 33555119, 100667350, "get_EqualityContract",
            2244, "20001280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Type", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", 33555119, 100667351, ".ctor", 6278,
            "2003011280C9081D0E", new KeyValuePair<string, string>[] { }, "System.Void",
            new string[] { "System.Type", "System.Int32", "System.String[]" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("type", 0, 0, "System.Type", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Type"), 1, 134221707),
                new SourceMethodParameterDefinition("memberIndex", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134221708),
                new SourceMethodParameterDefinition("memberArgs", 2, 0, "System.String[]", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String"), arrayRank: 1,
                        arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                            { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) }), 3,
                    134221709)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 8, 16));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", 33555119, 100667352, "ToString", 198,
            "20000E", new KeyValuePair<string, string>[] { }, "System.String", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 27, 87));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", 33555119, 100667353, "PrintMembers", 196,
            "2001021280AD",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Text.StringBuilder" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("builder", 0, 0, "System.Text.StringBuilder", false, null,
                    new byte[] { }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Text.StringBuilder"), 1, 134221710)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", 33555119, 100667354, "op_Inequality", 2198,
            "000202128ABC128ABC",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection",
                "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection"), 1, 134221711),
                new SourceMethodParameterDefinition("right", 1, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection"), 2, 134221712)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 6, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", 33555119, 100667355, "op_Equality", 2198,
            "000202128ABC128ABC",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection",
                "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection"), 1, 134221713),
                new SourceMethodParameterDefinition("right", 1, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection"), 2, 134221714)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 13, 19));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", 33555119, 100667356, "GetHashCode", 198,
            "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 10, 30));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", 33555119, 100667357, "Equals", 198,
            "2001021C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Object" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("obj", 0, 0, "System.Object", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object"), 1, 134221715)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 13));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", 33555119, 100667358, "Equals", 230,
            "200102128604",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), 1, 134221716)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", 33555119, 100667359, "Equals", 454,
            "200102128ABC",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection"), 1, 134221717)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 18, 40));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", 33555119, 100667360, "<Clone>$", 454,
            "2000128ABC",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.PreserveBaseOverridesAttribute",
                    "01000000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", 33555119, 100667361, ".ctor", 6276,
            "200101128ABC",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("original", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+ReadOnlyCollection"), 1, 134221718)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 8, 20));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", 33555120, 100667362, "get_EqualityContract",
            2244, "20001280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Type", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", 33555120, 100667363, ".ctor", 6278,
            "2004011280C9081D0E118271", new KeyValuePair<string, string>[] { }, "System.Void",
            new string[] { "System.Type", "System.Int32", "System.String[]", "System.Reflection.BindingFlags" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("type", 0, 0, "System.Type", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Type"), 1, 134221719),
                new SourceMethodParameterDefinition("memberIndex", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134221720),
                new SourceMethodParameterDefinition("memberArgs", 2, 0, "System.String[]", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String"), arrayRank: 1,
                        arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                            { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) }), 3,
                    134221721),
                new SourceMethodParameterDefinition("bindingFlags", 3, 0, "System.Reflection.BindingFlags", false, null,
                    new byte[] { }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Reflection.BindingFlags"), 4, 134221722)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 11, 24));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", 33555120, 100667364, "ToString", 198, "20000E",
            new KeyValuePair<string, string>[] { }, "System.String", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 126, 392));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", 33555120, 100667365, "PrintMembers", 196,
            "2001021280AD",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Text.StringBuilder" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("builder", 0, 0, "System.Text.StringBuilder", false, null,
                    new byte[] { }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Text.StringBuilder"), 1, 134221723)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", 33555120, 100667366, "op_Inequality", 2198,
            "000202128AC0128AC0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound",
                "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound"), 1, 134221724),
                new SourceMethodParameterDefinition("right", 1, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound"), 2, 134221725)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 6, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", 33555120, 100667367, "op_Equality", 2198,
            "000202128AC0128AC0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
            {
                "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound",
                "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound"), 1, 134221726),
                new SourceMethodParameterDefinition("right", 1, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound"), 2, 134221727)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 13, 19));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", 33555120, 100667368, "GetHashCode", 198,
            "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 17, 53));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", 33555120, 100667369, "Equals", 198, "2001021C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "System.Object" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("obj", 0, 0, "System.Object", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object"), 1, 134221728)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 13));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", 33555120, 100667370, "Equals", 230,
            "200102128604",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.InfoHUD.TargetQuery+MemberAccessError", false,
                    null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError"), 1, 134221729)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", 33555120, 100667371, "Equals", 454,
            "200102128AC0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound"), 1, 134221730)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 25, 64));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", 33555120, 100667372, "<Clone>$", 454,
            "2000128AC0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.PreserveBaseOverridesAttribute",
                    "01000000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", 33555120, 100667373, ".ctor", 6276,
            "200101128AC0",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("original", 0, 0,
                    "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.InfoHUD.TargetQuery+MemberAccessError+MemberNotFound"), 1, 134221731)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 12, 32));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+BoxedValueHolder", 33554818, 100666057, ".ctor", 6278, "2003011C08151283F9011C",
            new KeyValuePair<string, string>[] { }, "System.Void",
            new string[] { "System.Object", "System.Int32", "System.Collections.Generic.Stack`1<System.Object>" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("BaseInstance", 0, 0, "System.Object", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object"), 1, 134220880),
                new SourceMethodParameterDefinition("Index", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134220881),
                new SourceMethodParameterDefinition("ValueStack", 2, 0,
                    "System.Collections.Generic.Stack`1<System.Object>", false, null, new byte[] { },
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
                                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                                typeIdentity: "System.Object")
                        }), 3, 134220882)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 12, 28));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+BoxedValueHolder", 33554818, 100666058, "get_EqualityContract", 2500, "20001280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Type", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+BoxedValueHolder", 33554818, 100666059, "get_BaseInstance", 2182, "20001C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Object", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+BoxedValueHolder", 33554818, 100666060, "set_BaseInstance", 2182, "20011F80A9011C",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void modreq(System.Runtime.CompilerServices.IsExternalInit)", new string[] { "System.Object" },
            0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("value", 0, 0, "System.Object", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object"), 1, 134220883)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.RequiredModifier,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Void"),
                modifierType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Runtime.CompilerServices.IsExternalInit")), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+BoxedValueHolder", 33554818, 100666061, "get_Index", 2182, "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.InfoHUD.TargetQuery+BoxedValueHolder", 33554818, 100666062, "set_Index", 2182, "20011F80A90108",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void modreq(System.Runtime.CompilerServices.IsExternalInit)", new string[] { "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("value", 0, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 1, 134220884)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.RequiredModifier,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Void"),
                modifierType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Runtime.CompilerServices.IsExternalInit")), true, 4, 8));
    }
}
