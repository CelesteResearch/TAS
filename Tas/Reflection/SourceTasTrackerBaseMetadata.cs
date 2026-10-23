using System;
using Celeste.Core.Reflection.Forwarders;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Types;

namespace Celeste.Mods.Tas.Reflection
{
    /// <summary>Publishes only the genuine external base definitions required by the full TAS type views.</summary>
    internal static class SourceTasTrackerBaseMetadata
    {
        internal static void Publish(SourceMemberCatalog members)
        {
            members.SourceTypeForwarders.RegisterForwarder(new SourceTypeForwarderDefinition("InstalledEverest",
                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                "e8fb5b78dc75b0650520042936fca2165a060cb4afa4b8cce90524c9c9fc3563",
                new Guid("d294728f-162e-4f41-a769-4f8f6bc8a8d8"), 654311471, "System.Attribute", 2097152U, true,
                "AssemblyReference",
                "System.Private.CoreLib, Version=8.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e",
                "System.Attribute", null, 0));
            members.RegisterTypeDefinition(new SourceTypeDefinition(
                "System.Private.CoreLib, Version=8.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e",
                "System.Attribute", 33554542, 1056897,
                "System.Private.CoreLib, Version=8.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e",
                "System.Object", new string[] { },
                new SourceDeclaredAttribute[]
                {
                    new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableContextAttribute",
                        "System.Void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(System.Byte)",
                        new byte[] { 0x01, 0x00, 0x01, 0x00, 0x00 }),
                    new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                        "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                        new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 }),
                    new SourceDeclaredAttribute("System.AttributeUsageAttribute",
                        "System.Void System.AttributeUsageAttribute::.ctor(System.AttributeTargets)",
                        new byte[]
                        {
                            0x01, 0x00, 0xFF, 0x7F, 0x00, 0x00, 0x02, 0x00, 0x54, 0x02, 0x09, 0x49, 0x6E, 0x68, 0x65,
                            0x72,
                            0x69, 0x74, 0x65, 0x64, 0x01, 0x54, 0x02, 0x0D, 0x41, 0x6C, 0x6C, 0x6F, 0x77, 0x4D, 0x75,
                            0x6C,
                            0x74, 0x69, 0x70, 0x6C, 0x65, 0x00
                        }),
                    new SourceDeclaredAttribute("System.Runtime.CompilerServices.TypeForwardedFromAttribute",
                        "System.Void System.Runtime.CompilerServices.TypeForwardedFromAttribute::.ctor(System.String)",
                        new byte[]
                        {
                            0x01, 0x00, 0x4B, 0x6D, 0x73, 0x63, 0x6F, 0x72, 0x6C, 0x69, 0x62, 0x2C, 0x20, 0x56, 0x65,
                            0x72,
                            0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x34, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x30, 0x2C, 0x20, 0x43,
                            0x75,
                            0x6C, 0x74, 0x75, 0x72, 0x65, 0x3D, 0x6E, 0x65, 0x75, 0x74, 0x72, 0x61, 0x6C, 0x2C, 0x20,
                            0x50,
                            0x75, 0x62, 0x6C, 0x69, 0x63, 0x4B, 0x65, 0x79, 0x54, 0x6F, 0x6B, 0x65, 0x6E, 0x3D, 0x62,
                            0x37,
                            0x37, 0x61, 0x35, 0x63, 0x35, 0x36, 0x31, 0x39, 0x33, 0x34, 0x65, 0x30, 0x38, 0x39, 0x00,
                            0x00
                        })
                }, null, new string[] { }, Array.Empty<SourceGenericParameterDefinition>(),
                new SourceTypeReference(SourceTypeReferenceKind.Named,
                    "System.Private.CoreLib, Version=8.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e",
                    "System.Object"), new SourceTypeReference[] { }));
            members.RegisterTypeDefinition(new SourceTypeDefinition(
                "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                "StudioCommunication.CommunicationAdapterBase", 33554444, 1048705,
                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Object",
                new string[] { "System.IDisposable" },
                new SourceDeclaredAttribute[]
                {
                    new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableContextAttribute",
                        "System.Void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(System.Byte)",
                        new byte[] { 0x01, 0x00, 0x01, 0x00, 0x00 }),
                    new SourceDeclaredAttribute("System.Runtime.CompilerServices.NullableAttribute",
                        "System.Void System.Runtime.CompilerServices.NullableAttribute::.ctor(System.Byte)",
                        new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 })
                }, null,
                new string[] { "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" },
                Array.Empty<SourceGenericParameterDefinition>(),
                new SourceTypeReference(SourceTypeReferenceKind.Named,
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    "System.Object"),
                new SourceTypeReference[]
                {
                    new SourceTypeReference(SourceTypeReferenceKind.Named,
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        "System.IDisposable")
                }));
        }
    }
}
