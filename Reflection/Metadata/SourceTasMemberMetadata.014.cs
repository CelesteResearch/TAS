using System;
using Celeste.Core.Reflection.Members;

namespace Celeste.Mods.Tas.Reflection.Metadata;

public static partial class SourceTasMemberMetadata
{
    private static void PublishTas014(SourceMemberCatalog catalog)
    {
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 67109185, "<InfoSubpixelIndicatorSize>k__BackingField",
            SourceMemberKind.Field, 1, "System.Int32", -1, -1, 0,
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
            "TAS.Module.CelesteTasSettings", 67109186, "<InfoOpacity>k__BackingField", SourceMemberKind.Field, 1,
            "System.Int32", -1, -1, 0,
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
            "TAS.Module.CelesteTasSettings", 67109187, "<InfoMaskedOpacity>k__BackingField", SourceMemberKind.Field, 1,
            "System.Int32", -1, -1, 0,
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
            "TAS.Module.CelesteTasSettings", 67109188, "slowForwardSpeeds", SourceMemberKind.Field, 33,
            "System.Single[]", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Single"), arrayRank: 1,
                arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                    { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 67109189, "<RestoreSettings>k__BackingField", SourceMemberKind.Field, 1,
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
            "TAS.Module.CelesteTasSettings", 67109190, "<AutoPauseDraft>k__BackingField", SourceMemberKind.Field, 1,
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
            "TAS.Module.CelesteTasSettings", 67109191, "<LaunchStudioAtBoot>k__BackingField", SourceMemberKind.Field, 1,
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
            "TAS.Module.CelesteTasSettings", 67109192, "<ShowStudioUpdateBanner>k__BackingField",
            SourceMemberKind.Field, 1, "System.Boolean", -1, -1, 0,
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
            "TAS.Module.CelesteTasSettings", 67109193, "<_AttemptConnectStudio>k__BackingField", SourceMemberKind.Field,
            1, "System.Boolean", -1, -1, 0,
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
            "TAS.Module.CelesteTasSettings", 67109194, "_BetterInvincible", SourceMemberKind.Field, 6, "System.Boolean",
            -1, -1, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("YamlDotNet.Serialization.YamlMemberAttribute",
                    "System.Void YamlDotNet.Serialization.YamlMemberAttribute::.ctor()",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x54, 0x0E, 0x05, 0x41, 0x6C, 0x69, 0x61, 0x73, 0x10, 0x42, 0x65, 0x74,
                        0x74, 0x65, 0x72, 0x49, 0x6E, 0x76, 0x69, 0x6E, 0x63, 0x69, 0x62, 0x6C, 0x65
                    })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 67109195, "ForceAllowAccessibilityTools", SourceMemberKind.Field, 6,
            "StudioCommunication.StudioEnableCondition", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null,
            null, null, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.StudioEnableCondition")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 67109196, "PreventSkinModGameplayChanges", SourceMemberKind.Field, 6,
            "StudioCommunication.GameplayEnableCondition", -1, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false,
            null, null, null, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.GameplayEnableCondition")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 67109197, "<HideFreezeFrames>k__BackingField", SourceMemberKind.Field, 1,
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
            "TAS.Module.CelesteTasSettings", 67109198, "<IgnoreGcCollect>k__BackingField", SourceMemberKind.Field, 1,
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
            "TAS.Module.CelesteTasSettings", 385876098, "Instance", SourceMemberKind.Property, 0,
            "TAS.Module.CelesteTasSettings", 2198, 2193, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null,
            null, "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.Module.CelesteTasSettings")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876099, "Enabled", SourceMemberKind.Property, 0, "System.Boolean", 2182,
            2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876100, "ShowHitboxes", SourceMemberKind.Property, 0, "System.Boolean",
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
            "TAS.Module.CelesteTasSettings", 385876101, "_ShowHitboxes", SourceMemberKind.Property, 0, "System.Boolean",
            2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("YamlDotNet.Serialization.YamlMemberAttribute",
                    "System.Void YamlDotNet.Serialization.YamlMemberAttribute::.ctor()",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x54, 0x0E, 0x05, 0x41, 0x6C, 0x69, 0x61, 0x73, 0x0C, 0x53, 0x68, 0x6F,
                        0x77, 0x48, 0x69, 0x74, 0x62, 0x6F, 0x78, 0x65, 0x73
                    })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876102, "ShowTriggerHitboxes", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876103, "ShowUnloadedRoomsHitboxes", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876104, "ShowCameraHitboxes", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876105, "SimplifiedHitboxes", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876106, "ShowActualCollideHitboxes", SourceMemberKind.Property, 0,
            "StudioCommunication.ActualCollideHitboxType", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false,
            null, null, null, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.ActualCollideHitboxType")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876107, "UnCollidableHitboxesOpacity", SourceMemberKind.Property, 0,
            "System.Int32", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876108, "EntityHitboxColor", SourceMemberKind.Property, 0,
            "Microsoft.Xna.Framework.Color", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null,
            null, "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Microsoft.Xna.Framework.Color")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876109, "TriggerHitboxColor", SourceMemberKind.Property, 0,
            "Microsoft.Xna.Framework.Color", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null,
            null, "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Microsoft.Xna.Framework.Color")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876110, "PlatformHitboxColor", SourceMemberKind.Property, 0,
            "Microsoft.Xna.Framework.Color", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null,
            null, "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Microsoft.Xna.Framework.Color")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876111, "ShowCycleHitboxColors", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876112, "CycleHitboxColor1", SourceMemberKind.Property, 0,
            "Microsoft.Xna.Framework.Color", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null,
            null, "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Microsoft.Xna.Framework.Color")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876113, "CycleHitboxColor2", SourceMemberKind.Property, 0,
            "Microsoft.Xna.Framework.Color", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null,
            null, "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Microsoft.Xna.Framework.Color")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876114, "CycleHitboxColor3", SourceMemberKind.Property, 0,
            "Microsoft.Xna.Framework.Color", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null,
            null, "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Microsoft.Xna.Framework.Color")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876115, "OtherCyclesHitboxColor", SourceMemberKind.Property, 0,
            "Microsoft.Xna.Framework.Color", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null,
            null, "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Microsoft.Xna.Framework.Color")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876116, "KeyStart", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x12, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x53, 0x54, 0x41, 0x52, 0x54,
                        0x5F, 0x53, 0x54, 0x4F, 0x50, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0xA3, 0x00,
                        0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876117, "KeyRestart", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x0F, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x52, 0x45, 0x53, 0x54, 0x41,
                        0x52, 0x54, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0xBB, 0x00,
                        0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876118, "KeyFastForward", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x14, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x46, 0x41, 0x53, 0x54, 0x5F,
                        0x46, 0x4F, 0x52, 0x57, 0x41, 0x52, 0x44, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0xA1, 0x00,
                        0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876119, "KeyFastForwardComment", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x1C, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x46, 0x41, 0x53, 0x54, 0x5F,
                        0x46, 0x4F, 0x52, 0x57, 0x41, 0x52, 0x44, 0x5F, 0x43, 0x4F, 0x4D, 0x4D, 0x45, 0x4E, 0x54, 0x00,
                        0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0xA5, 0x00,
                        0x00, 0x00, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876120, "KeySlowForward", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x14, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x53, 0x4C, 0x4F, 0x57, 0x5F,
                        0x46, 0x4F, 0x52, 0x57, 0x41, 0x52, 0x44, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0xDC, 0x00,
                        0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876121, "KeyFrameAdvance", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x15, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x46, 0x52, 0x41, 0x4D, 0x45,
                        0x5F, 0x41, 0x44, 0x56, 0x41, 0x4E, 0x43, 0x45, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0xDB, 0x00,
                        0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876122, "KeyFrameStepBack", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x17, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x46, 0x52, 0x41, 0x4D, 0x45,
                        0x5F, 0x53, 0x54, 0x45, 0x50, 0x5F, 0x42, 0x41, 0x43, 0x4B, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0xA2, 0x00,
                        0x00, 0x00, 0xDB, 0x00, 0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876123, "KeyPause", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x14, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x50, 0x41, 0x55, 0x53, 0x45,
                        0x5F, 0x52, 0x45, 0x53, 0x55, 0x4D, 0x45, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0xDD, 0x00,
                        0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876124, "KeyHitboxes", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x10, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x48, 0x49, 0x54, 0x42, 0x4F,
                        0x58, 0x45, 0x53, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0xA2, 0x00,
                        0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876125, "KeyTriggerHitboxes", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x18, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x54, 0x52, 0x49, 0x47, 0x47,
                        0x45, 0x52, 0x5F, 0x48, 0x49, 0x54, 0x42, 0x4F, 0x58, 0x45, 0x53, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0xA4, 0x00,
                        0x00, 0x00, 0x54, 0x00, 0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876126, "KeyGraphics", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x1B, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x53, 0x49, 0x4D, 0x50, 0x4C,
                        0x49, 0x46, 0x49, 0x45, 0x44, 0x5F, 0x47, 0x52, 0x41, 0x50, 0x48, 0x49, 0x43, 0x53, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0xA2, 0x00,
                        0x00, 0x00, 0x4E, 0x00, 0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876127, "KeyCamera", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x15, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x43, 0x45, 0x4E, 0x54, 0x45,
                        0x52, 0x5F, 0x43, 0x41, 0x4D, 0x45, 0x52, 0x41, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0xA2, 0x00,
                        0x00, 0x00, 0x4D, 0x00, 0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876128, "KeyLockCamera", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x13, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x4C, 0x4F, 0x43, 0x4B, 0x5F,
                        0x43, 0x41, 0x4D, 0x45, 0x52, 0x41, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0xA2, 0x00,
                        0x00, 0x00, 0x48, 0x00, 0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876129, "KeySaveState", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x12, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x53, 0x41, 0x56, 0x45, 0x5F,
                        0x53, 0x54, 0x41, 0x54, 0x45, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0xA5, 0x00,
                        0x00, 0x00, 0xBD, 0x00, 0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876130, "KeyClearState", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x13, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x43, 0x4C, 0x45, 0x41, 0x52,
                        0x5F, 0x53, 0x54, 0x41, 0x54, 0x45, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0xA5, 0x00,
                        0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876131, "KeyInfoHud", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x10, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x49, 0x4E, 0x46, 0x4F, 0x5F,
                        0x48, 0x55, 0x44, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0xA2, 0x00,
                        0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876132, "KeyFreeCamera", SourceMemberKind.Property, 0,
            "Celeste.Mod.ButtonBinding", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingNameAttribute",
                    "System.Void Celeste.Mod.SettingNameAttribute::.ctor(System.String)",
                    new byte[]
                    {
                        0x01, 0x00, 0x13, 0x54, 0x41, 0x53, 0x5F, 0x4B, 0x45, 0x59, 0x5F, 0x46, 0x52, 0x45, 0x45, 0x5F,
                        0x43, 0x41, 0x4D, 0x45, 0x52, 0x41, 0x00, 0x00
                    }),
                new SourceDeclaredAttribute("Celeste.Mod.DefaultButtonBindingAttribute",
                    "System.Void Celeste.Mod.DefaultButtonBindingAttribute::.ctor(Microsoft.Xna.Framework.Input.Buttons[],Microsoft.Xna.Framework.Input.Keys[])",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0xA4, 0x00,
                        0x00, 0x00, 0x00, 0x00
                    })
            }, false, null, null, null, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Mod.ButtonBinding")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876133, "_SimplifiedGraphics", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("YamlDotNet.Serialization.YamlMemberAttribute",
                    "System.Void YamlDotNet.Serialization.YamlMemberAttribute::.ctor()",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x54, 0x0E, 0x05, 0x41, 0x6C, 0x69, 0x61, 0x73, 0x12, 0x53, 0x69, 0x6D,
                        0x70, 0x6C, 0x69, 0x66, 0x69, 0x65, 0x64, 0x47, 0x72, 0x61, 0x70, 0x68, 0x69, 0x63, 0x73
                    })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876134, "SimplifiedGraphics", SourceMemberKind.Property, 0,
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
            "TAS.Module.CelesteTasSettings", 385876135, "_ShowGameplay", SourceMemberKind.Property, 0, "System.Boolean",
            2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("YamlDotNet.Serialization.YamlMemberAttribute",
                    "System.Void YamlDotNet.Serialization.YamlMemberAttribute::.ctor()",
                    new byte[]
                    {
                        0x01, 0x00, 0x01, 0x00, 0x54, 0x0E, 0x05, 0x41, 0x6C, 0x69, 0x61, 0x73, 0x0C, 0x53, 0x68, 0x6F,
                        0x77, 0x47, 0x61, 0x6D, 0x65, 0x70, 0x6C, 0x61, 0x79
                    })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876136, "ShowGameplay", SourceMemberKind.Property, 0, "System.Boolean",
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
            "TAS.Module.CelesteTasSettings", 385876137, "SimplifiedLighting", SourceMemberKind.Property, 0,
            "System.Nullable`1<System.Int32>", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null,
            null, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Nullable`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876138, "SimplifiedBloomBase", SourceMemberKind.Property, 0,
            "System.Nullable`1<System.Int32>", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null,
            null, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Nullable`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876139, "SimplifiedBloomStrength", SourceMemberKind.Property, 0,
            "System.Nullable`1<System.Int32>", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null,
            null, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.GenericInstance,
                elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                    global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                    assemblyIdentity:
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    typeIdentity: "System.Nullable`1"),
                genericArguments: new global::Celeste.Core.Reflection.Types.SourceTypeReference[]
                {
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32")
                })));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876140, "SimplifiedSpinnerColor", SourceMemberKind.Property, 0,
            "TAS.Gameplay.SpinnerColor", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.Gameplay.SpinnerColor")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876141, "SimplifiedDustSpriteEdge", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876142, "SimplifiedScreenWipe", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876143, "SimplifiedColorGrade", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876144, "SimplifiedSolidTilesStyle", SourceMemberKind.Property, 0,
            "TAS.EverestInterop.SimplifiedGraphicsFeature+SolidTilesStyle", 2182, 2182, 0,
            Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.EverestInterop.SimplifiedGraphicsFeature+SolidTilesStyle")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876145, "SimplifiedBackgroundTiles", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876146, "SimplifiedBackdrop", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876147, "SimplifiedDecal", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876148, "SimplifiedParticle", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876149, "SimplifiedDistort", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876150, "SimplifiedMiniTextbox", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876151, "SimplifiedLightningStrike", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876152, "SimplifiedClutteredEntity", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876153, "SimplifiedHud", SourceMemberKind.Property, 0, "System.Boolean",
            2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876154, "SimplifiedWavedEdge", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876155, "SimplifiedSpikes", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876156, "InfoHud", SourceMemberKind.Property, 0, "System.Boolean", 2182,
            2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876157, "InfoGame", SourceMemberKind.Property, 0, "System.Boolean",
            2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876158, "InfoTasInput", SourceMemberKind.Property, 0, "System.Boolean",
            2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876159, "InfoSubpixelIndicator", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876160, "InfoCustom", SourceMemberKind.Property, 0,
            "StudioCommunication.HudOptions", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null,
            null, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.HudOptions")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876161, "WatchEntity", SourceMemberKind.Property, 0, "System.Boolean",
            2179, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876162, "HudWatchEntity", SourceMemberKind.Property, 0,
            "System.Boolean", 2179, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876163, "StudioWatchEntity", SourceMemberKind.Property, 0,
            "System.Boolean", 2179, -1, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876164, "InfoWatchEntityHudType", SourceMemberKind.Property, 0,
            "StudioCommunication.WatchEntityType", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null,
            null, null, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.WatchEntityType")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876165, "InfoWatchEntityStudioType", SourceMemberKind.Property, 0,
            "StudioCommunication.WatchEntityType", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null,
            null, null, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.WatchEntityType")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876166, "InfoWatchEntityLogToConsole", SourceMemberKind.Property, 0,
            "System.Boolean", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876167, "InfoCustomTemplate", SourceMemberKind.Property, 0,
            "System.String", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingIgnoreAttribute",
                    "System.Void Celeste.Mod.SettingIgnoreAttribute::.ctor()", new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876168, "InfoPosition", SourceMemberKind.Property, 0,
            "Microsoft.Xna.Framework.Vector2", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingIgnoreAttribute",
                    "System.Void Celeste.Mod.SettingIgnoreAttribute::.ctor()", new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, false, null, null, null, "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Microsoft.Xna.Framework.Vector2")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876169, "InfoTextSize", SourceMemberKind.Property, 0, "System.Int32",
            2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingIgnoreAttribute",
                    "System.Void Celeste.Mod.SettingIgnoreAttribute::.ctor()", new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876170, "InfoSubpixelIndicatorSize", SourceMemberKind.Property, 0,
            "System.Int32", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingIgnoreAttribute",
                    "System.Void Celeste.Mod.SettingIgnoreAttribute::.ctor()", new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876171, "InfoOpacity", SourceMemberKind.Property, 0, "System.Int32",
            2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingIgnoreAttribute",
                    "System.Void Celeste.Mod.SettingIgnoreAttribute::.ctor()", new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876172, "InfoMaskedOpacity", SourceMemberKind.Property, 0,
            "System.Int32", 2182, 2182, 0,
            new SourceDeclaredAttribute[]
            {
                new SourceDeclaredAttribute("Celeste.Mod.SettingIgnoreAttribute",
                    "System.Void Celeste.Mod.SettingIgnoreAttribute::.ctor()", new byte[] { 0x01, 0x00, 0x00, 0x00 })
            }, false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876173, "PositionDecimals", SourceMemberKind.Property, 0,
            "System.Int32", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876174, "SpeedDecimals", SourceMemberKind.Property, 0, "System.Int32",
            2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876175, "VelocityDecimals", SourceMemberKind.Property, 0,
            "System.Int32", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876176, "AngleDecimals", SourceMemberKind.Property, 0, "System.Int32",
            2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876177, "CustomInfoDecimals", SourceMemberKind.Property, 0,
            "System.Int32", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876178, "SubpixelIndicatorDecimals", SourceMemberKind.Property, 0,
            "System.Int32", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null, null,
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32")));
        catalog.RegisterDefinition(new SourceMemberDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Module.CelesteTasSettings", 385876179, "SpeedUnit", SourceMemberKind.Property, 0,
            "StudioCommunication.SpeedUnit", 2182, 2182, 0, Array.Empty<SourceDeclaredAttribute>(), false, null, null,
            null, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.SpeedUnit")));
    }
}
