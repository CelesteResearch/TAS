using System;
using Celeste.Core.Reflection.Members;

namespace Celeste.Mods.Tas.Reflection.Metadata;

public static partial class SourceTasMemberMetadata
{
    private static void PublishTas015(SourceMemberCatalog catalog)
    {
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876180, "VelocityUnit", SourceMemberKind.Property, 0,
            "StudioCommunication.SpeedUnit", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null,
            null, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.SpeedUnit")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876181, "SlowForwardSpeeds", SourceMemberKind.Property, 0,
            "System.Collections.Generic.List`1<System.Single>", 2182, -1, 0, Array.Empty<SourceDeclaredAttribute>(),
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
                        typeIdentity: "System.Single")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876182, "FastForwardSpeed", SourceMemberKind.Property, 0,
            "System.Int32", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876183, "SlowForwardSpeed", SourceMemberKind.Property, 0,
            "System.Single", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Single")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876184, "_CenterCamera", SourceMemberKind.Property, 0, "System.Boolean",
            2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("YamlDotNet.Serialization.YamlMemberAttribute",
                    "System.Void YamlDotNet.Serialization.YamlMemberAttribute::.ctor()",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x54, 0x0E, 0x05, 0x41, 0x6C, 0x69, 0x61, 0x73, 0x0C, 0x43, 0x65, 0x6E,
                        0x74, 0x65, 0x72, 0x43, 0x61, 0x6D, 0x65, 0x72, 0x61
                    })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876185, "CenterCamera", SourceMemberKind.Property, 0, "System.Boolean",
            2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("YamlDotNet.Serialization.YamlIgnoreAttribute",
                    "System.Void YamlDotNet.Serialization.YamlIgnoreAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876186, "CenterCameraHorizontallyOnly", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876187, "EnableExCameraDynamicsForCenterCamera",
            SourceMemberKind.Property, 0, "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(),
            false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876188, "RestoreSettings", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876189, "AutoPauseDraft", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876190, "LaunchStudioAtBoot", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876191, "ShowStudioUpdateBanner", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876192, "_AttemptConnectStudio", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("YamlDotNet.Serialization.YamlMemberAttribute",
                    "System.Void YamlDotNet.Serialization.YamlMemberAttribute::.ctor()",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x54, 0x0E, 0x05, 0x41, 0x6C, 0x69, 0x61, 0x73, 0x14, 0x41, 0x74, 0x74,
                        0x65, 0x6D, 0x70, 0x74, 0x43, 0x6F, 0x6E, 0x6E, 0x65, 0x63, 0x74, 0x53, 0x74, 0x75, 0x64, 0x69,
                        0x6F
                    })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876193, "AttemptConnectStudio", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("YamlDotNet.Serialization.YamlIgnoreAttribute",
                    "System.Void YamlDotNet.Serialization.YamlIgnoreAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876194, "BetterInvincible", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("YamlDotNet.Serialization.YamlIgnoreAttribute",
                    "System.Void YamlDotNet.Serialization.YamlIgnoreAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876195, "HideFreezeFrames", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876196, "IgnoreGcCollect", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings+<>c__DisplayClass179_0", 33554779, 1048835,
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
            "TAS.Module.CelesteTasSettings+<>c__DisplayClass179_0", 67109866, "value", SourceMemberKind.Field, 6,
            "TAS.EverestInterop.SimplifiedGraphicsFeature+SolidTilesStyle", -1, -1, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.EverestInterop.SimplifiedGraphicsFeature+SolidTilesStyle")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.CollabUtils2Interop", 33554541, 1048960,
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
            "TAS.ModInterop.CollabUtils2Interop", 67109199, "installed", SourceMemberKind.Field, 49,
            "System.Lazy`1<System.Boolean>", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Lazy`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Boolean")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.CollabUtils2Interop", 385876197, "Installed", SourceMemberKind.Property, 0,
            "System.Boolean", 2198, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.CollabUtils2Interop+Lobby", 33554780, 1048962,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 }),
                new SourceDeclaredAttribute("MonoMod.ModInterop.ModImportNameAttribute",
                    "System.Void MonoMod.ModInterop.ModImportNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x18, 0x43, 0x6F, 0x6C, 0x6C, 0x61, 0x62, 0x55, 0x74, 0x69, 0x6C, 0x73, 0x32, 0x2E,
                        0x4C, 0x6F, 0x62, 0x62, 0x79, 0x48, 0x65, 0x6C, 0x70, 0x65, 0x72, 0x00, 0x00
                    })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.CollabUtils2Interop+Lobby", 67109867, "IsCollabLevelSet", SourceMemberKind.Field, 22,
            "System.Func`2<System.String,System.Boolean>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x01, 0x00, 0x00 })
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
                        typeIdentity: "System.Boolean")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.CollabUtils2Interop+Lobby", 67109868, "IsCollabLobby", SourceMemberKind.Field, 22,
            "System.Func`2<System.String,System.Boolean>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x01, 0x00, 0x00 })
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
                        typeIdentity: "System.Boolean")
                })));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.CollabUtils2Interop+Lobby+<>c", 33555099, 1057027,
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
            "TAS.ModInterop.CollabUtils2Interop+Lobby+<>c", 67110992, "<>9", SourceMemberKind.Field, 54,
            "TAS.ModInterop.CollabUtils2Interop+Lobby+<>c", -1, -1, 0,
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
                typeIdentity: "TAS.ModInterop.CollabUtils2Interop+Lobby+<>c")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.CollabUtils2Interop+Lobby+<>c", 67110993, "<>9__4_0", SourceMemberKind.Field, 22,
            "System.Func`2<Monocle.Entity,Celeste.Mod.CollabUtils2.Entities.LobbyMapController+MarkerInfo>", -1, -1, 0,
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
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Monocle.Entity"),
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "CollabUtils2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Celeste.Mod.CollabUtils2.Entities.LobbyMapController+MarkerInfo")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.CollabUtils2Interop+Lobby+<>c", 67110994, "<>9__4_2", SourceMemberKind.Field, 22,
            "System.Func`2<Celeste.Mod.CollabUtils2.Entities.LobbyMapController+MarkerInfo,System.String>", -1, -1, 0,
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
                        assemblyIdentity: "CollabUtils2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Celeste.Mod.CollabUtils2.Entities.LobbyMapController+MarkerInfo"),
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String")
                })));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.CollabUtils2Interop+Lobby+<>c__DisplayClass3_0", 33555100, 1048835,
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
            "TAS.ModInterop.CollabUtils2Interop+Lobby+<>c__DisplayClass3_0", 67110995, "sid", SourceMemberKind.Field, 6,
            "System.String", -1, -1, 0,
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
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.CollabUtils2Interop+Lobby+<>c__DisplayClass4_0", 33555101, 1048835,
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
            "TAS.ModInterop.CollabUtils2Interop+Lobby+<>c__DisplayClass4_0", 67110996, "visitedTiles",
            SourceMemberKind.Field, 6, "Celeste.Mod.CollabUtils2.ByteArray2D", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null, "CollabUtils2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CollabUtils2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.CollabUtils2.ByteArray2D")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.CollabUtils2Interop+Lobby+<>c__DisplayClass4_0", 67110997, "controller",
            SourceMemberKind.Field, 6, "Celeste.Mod.CollabUtils2.Entities.LobbyMapController", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
            }, false, null, null, null, "CollabUtils2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CollabUtils2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.CollabUtils2.Entities.LobbyMapController")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.CollabUtils2Interop+<>c", 33554781, 1057027,
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
            "TAS.ModInterop.CollabUtils2Interop+<>c", 67109869, "<>9", SourceMemberKind.Field, 54,
            "TAS.ModInterop.CollabUtils2Interop+<>c", -1, -1, 0,
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
                typeIdentity: "TAS.ModInterop.CollabUtils2Interop+<>c")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExCameraDynamicsInterop", 33554542, 1048960,
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
            "TAS.ModInterop.ExCameraDynamicsInterop", 67109200, "installed", SourceMemberKind.Field, 49,
            "System.Lazy`1<System.Boolean>", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Lazy`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Boolean")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExCameraDynamicsInterop", 385876198, "Installed", SourceMemberKind.Property, 0,
            "System.Boolean", 2198, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExCameraDynamicsInterop", 385876199, "Enabled", SourceMemberKind.Property, 0,
            "System.Boolean", 2198, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExCameraDynamicsInterop", 385876200, "AutomaticZooming", SourceMemberKind.Property, 0,
            "System.Boolean", 2198, 2198, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExCameraDynamicsInterop", 385876201, "TriggerZoomOverride", SourceMemberKind.Property, 0,
            "System.Single", 2198, 2198, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Single")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExCameraDynamicsInterop", 385876202, "automaticZooming", SourceMemberKind.Property, 0,
            "System.Boolean", 2193, 2193, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExCameraDynamicsInterop", 385876203, "triggerZoomOverride", SourceMemberKind.Property, 0,
            "System.Single", 2193, 2193, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Single")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExCameraDynamicsInterop+<>c", 33554782, 1057027,
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
            "TAS.ModInterop.ExCameraDynamicsInterop+<>c", 67109870, "<>9", SourceMemberKind.Field, 54,
            "TAS.ModInterop.ExCameraDynamicsInterop+<>c", -1, -1, 0,
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
                typeIdentity: "TAS.ModInterop.ExCameraDynamicsInterop+<>c")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop", 33554543, 1048960,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableContextAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00 }),
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
            "TAS.ModInterop.ExtendedVariantsInterop", 67109201, "module", SourceMemberKind.Field, 49,
            "System.Lazy`1<Celeste.Mod.EverestModule>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Lazy`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Celeste.Mod.EverestModule")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop", 67109202, "triggerManager", SourceMemberKind.Field, 49,
            "System.Lazy`1<System.Object>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Lazy`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop", 67109203, "variantHandlers", SourceMemberKind.Field, 49,
            "System.Lazy`1<System.Object>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Lazy`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop", 67109204, "getCurrentVariantValue", SourceMemberKind.Field, 49,
            "System.Lazy`1<MonoMod.Utils.FastReflectionHelper+FastInvoker>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Lazy`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "MonoMod.Utils, Version=25.0.9.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "MonoMod.Utils.FastReflectionHelper+FastInvoker")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop", 67109205, "getCurrentMapDefinedVariantValue",
            SourceMemberKind.Field, 49, "System.Lazy`1<MonoMod.Utils.FastReflectionHelper+FastInvoker>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Lazy`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "MonoMod.Utils, Version=25.0.9.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "MonoMod.Utils.FastReflectionHelper+FastInvoker")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop", 67109206, "setVariantValue", SourceMemberKind.Field, 49,
            "System.Lazy`1<MonoMod.Utils.FastReflectionHelper+FastInvoker>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Lazy`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "MonoMod.Utils, Version=25.0.9.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "MonoMod.Utils.FastReflectionHelper+FastInvoker")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop", 67109207, "dictionaryGetItem", SourceMemberKind.Field, 49,
            "System.Lazy`1<MonoMod.Utils.FastReflectionHelper+FastInvoker>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Lazy`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "MonoMod.Utils, Version=25.0.9.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "MonoMod.Utils.FastReflectionHelper+FastInvoker")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop", 67109208, "variantType", SourceMemberKind.Field, 49,
            "System.Lazy`1<System.Type>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Lazy`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Type")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop", 67109209, "upsideDownVariant", SourceMemberKind.Field, 49,
            "System.Lazy`1<System.Object>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Lazy`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop", 67109210, "superDashingVariant", SourceMemberKind.Field, 49,
            "System.Lazy`1<System.Object>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Lazy`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop", 67109211, "colorGradingVariant", SourceMemberKind.Field, 49,
            "System.Lazy`1<System.Object>", -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[] { 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Lazy`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Object")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop", 385876204, "UpsideDown", SourceMemberKind.Property, 0,
            "System.Boolean", 2198, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop", 385876205, "SuperDashing", SourceMemberKind.Property, 0,
            "System.Boolean", 2198, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop", 385876206, "ColorGrading", SourceMemberKind.Property, 0,
            "System.String", 2198, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop+<>c", 33554783, 1057027,
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
            "TAS.ModInterop.ExtendedVariantsInterop+<>c", 67109871, "<>9", SourceMemberKind.Field, 54,
            "TAS.ModInterop.ExtendedVariantsInterop+<>c", -1, -1, 0,
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
                typeIdentity: "TAS.ModInterop.ExtendedVariantsInterop+<>c")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ExtendedVariantsInterop+<>c__DisplayClass8_0", 33554784, 1048835,
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
            "TAS.ModInterop.ExtendedVariantsInterop+<>c__DisplayClass8_0", 67109872, "value", SourceMemberKind.Field, 6,
            "System.String", -1, -1, 0,
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
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ModUtils", 33554544, 1048960,
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
            "TAS.ModInterop.ModUtils", 67109212, "VanillaAssembly", SourceMemberKind.Field, 54,
            "System.Reflection.Assembly", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Reflection.Assembly")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ModUtils+<>c__DisplayClass6_0", 33554785, 1048835,
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
            "TAS.ModInterop.ModUtils+<>c__DisplayClass6_0", 67109873, "modName", SourceMemberKind.Field, 6,
            "System.String", -1, -1, 0,
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
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ModUtils+<GetTypes>d__2", 33554786, 1048835,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            new string[]
            {
                "System.Collections.Generic.IEnumerable`1<System.Type>", "System.Collections.IEnumerable",
                "System.Collections.Generic.IEnumerator`1<System.Type>", "System.Collections.IEnumerator",
                "System.IDisposable"
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
                        typeIdentity: "System.Collections.Generic.IEnumerable`1"),
                    genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                    {
                        new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.Type")
                    }),
                new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Collections.IEnumerable"),
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
                            typeIdentity: "System.Type")
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
            "TAS.ModInterop.ModUtils+<GetTypes>d__2", 67109874, "<>1__state", SourceMemberKind.Field, 1, "System.Int32",
            -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ModUtils+<GetTypes>d__2", 67109875, "<>2__current", SourceMemberKind.Field, 1,
            "System.Type", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ModUtils+<GetTypes>d__2", 67109876, "<>l__initialThreadId", SourceMemberKind.Field, 1,
            "System.Int32", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ModUtils+<GetTypes>d__2", 67109877, "modName", SourceMemberKind.Field, 1, "System.String",
            -1, -1, 0,
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
            "TAS.ModInterop.ModUtils+<GetTypes>d__2", 67109878, "<>3__modName", SourceMemberKind.Field, 6,
            "System.String", -1, -1, 0,
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
            "TAS.ModInterop.ModUtils+<GetTypes>d__2", 67109879, "fullTypeNames", SourceMemberKind.Field, 1,
            "System.String[]", -1, -1, 0,
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
            "TAS.ModInterop.ModUtils+<GetTypes>d__2", 67109880, "<>3__fullTypeNames", SourceMemberKind.Field, 6,
            "System.String[]", -1, -1, 0,
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
            "TAS.ModInterop.ModUtils+<GetTypes>d__2", 67109881, "<asm>5__2", SourceMemberKind.Field, 1,
            "System.Reflection.Assembly", -1, -1, 0,
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
                typeIdentity: "System.Reflection.Assembly")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ModUtils+<GetTypes>d__2", 67109882, "<>7__wrap2", SourceMemberKind.Field, 1,
            "System.String[]", -1, -1, 0,
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
            "TAS.ModInterop.ModUtils+<GetTypes>d__2", 67109883, "<>7__wrap3", SourceMemberKind.Field, 1, "System.Int32",
            -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ModUtils+<GetTypes>d__2", 385876385,
            "System.Collections.Generic.IEnumerator<System.Type>.Current", SourceMemberKind.Property, 0, "System.Type",
            2529, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ModUtils+<GetTypes>d__2", 385876386, "System.Collections.IEnumerator.Current",
            SourceMemberKind.Property, 0, "System.Object", 2529, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false,
            null, null, null, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.SaveStateAttribute", 33554545, 1048576,
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Utils.EventAttribute", Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.AttributeUsageAttribute",
                    "System.Void System.AttributeUsageAttribute::.ctor(System.AttributeTargets)",
                    new byte[] { 0x01, 0x00, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.Utils.EventAttribute"),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.LoadStateAttribute", 33554546, 1048576,
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Utils.EventAttribute", Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.AttributeUsageAttribute",
                    "System.Void System.AttributeUsageAttribute::.ctor(System.AttributeTargets)",
                    new byte[] { 0x01, 0x00, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.Utils.EventAttribute"),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.ClearStateAttribute", 33554547, 1048576,
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Utils.EventAttribute", Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.AttributeUsageAttribute",
                    "System.Void System.AttributeUsageAttribute::.ctor(System.AttributeTargets)",
                    new byte[] { 0x01, 0x00, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.Utils.EventAttribute"),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.SpeedrunToolInterop", 33554548, 1048960,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
            Array.Empty<string>(),
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableContextAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x01, 0x00, 0x00 }),
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                    new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 }),
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.ExtensionAttribute",
                    "System.Void System.Runtime.CompilerServices.ExtensionAttribute::.ctor()",
                    new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), new global::Celeste.Core.Reflection.Types.SourceTypeReference[] { }));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.SpeedrunToolInterop", 67109213, "<Installed>k__BackingField", SourceMemberKind.Field, 17,
            "System.Boolean", -1, -1, 0,
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
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.SpeedrunToolInterop", 67109214, "<MultipleSaveSlotsSupported>k__BackingField",
            SourceMemberKind.Field, 17, "System.Boolean", -1, -1, 0,
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
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.SpeedrunToolInterop", 67109215, "saveLoadHandle", SourceMemberKind.Field, 17,
            "System.Object", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.SpeedrunToolInterop", 67109216, "DefaultSlot", SourceMemberKind.Field, 32854,
            "System.String", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), true,
            SourceMetadataConstant.Decode("System.String",
                new byte[]
                {
                    0x43, 0x00, 0x65, 0x00, 0x6C, 0x00, 0x65, 0x00, 0x73, 0x00, 0x74, 0x00, 0x65, 0x00, 0x54, 0x00,
                    0x41, 0x00, 0x53, 0x00
                }), "System.String", "430065006C006500730074006500540041005300",
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.SpeedrunToolInterop", 385876207, "Installed", SourceMemberKind.Property, 0,
            "System.Boolean", 2198, 2193, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.SpeedrunToolInterop", 385876208, "MultipleSaveSlotsSupported", SourceMemberKind.Property, 0,
            "System.Boolean", 2198, 2193, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterTypeDefinition(new SourceTypeDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.SpeedrunToolInterop+<>c", 33554787, 1057027,
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
            "TAS.ModInterop.SpeedrunToolInterop+<>c", 67109884, "<>9", SourceMemberKind.Field, 54,
            "TAS.ModInterop.SpeedrunToolInterop+<>c", -1, -1, 0,
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
                typeIdentity: "TAS.ModInterop.SpeedrunToolInterop+<>c")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.SpeedrunToolInterop+<>c", 67109885, "<>9__9_0", SourceMemberKind.Field, 22,
            "System.Func`2<Celeste.Mod.EverestModule,System.Boolean>", -1, -1, 0,
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
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Celeste.Mod.EverestModule"),
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Boolean")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.ModInterop.SpeedrunToolInterop+<>c", 67109886, "<>9__19_0", SourceMemberKind.Field, 22,
            "System.Action`2<System.Collections.Generic.Dictionary`2<System.Type,System.Collections.Generic.Dictionary`2<System.String,System.Object>>,Celeste.Level>",
            -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                    "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x01, 0x01, 0x01, 0x01, 0x02, 0x01, 0x00, 0x00
                    })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Action`2"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
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
                                typeIdentity: "System.Type"),
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
                                        typeIdentity: "System.Object")
                                })
                        }),
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Celeste.Level")
                })));
    }
}
