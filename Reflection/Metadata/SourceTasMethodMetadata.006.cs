using System.Collections.Generic;
using Celeste.Core.Reflection.Methods;

namespace Celeste.Mods.Tas.Reflection.Metadata;

public static partial class SourceTasMethodMetadata
{
    private static void PublishTas006(SourceAssemblyMethodCatalog catalog)
    {
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.ITasCommandMeta+<GetAutoCompleteEntries>d__5", 33554708, 100665541,
            "System.Collections.Generic.IEnumerator<StudioCommunication.CommandAutoCompleteEntry>.get_Current", 2529,
            "20001180D5",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "StudioCommunication.CommandAutoCompleteEntry", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.CommandAutoCompleteEntry"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.ITasCommandMeta+<GetAutoCompleteEntries>d__5", 33554708, 100665542,
            "System.Collections.IEnumerator.Reset", 481, "200001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "System.Void", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 2, 6));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.ITasCommandMeta+<GetAutoCompleteEntries>d__5", 33554708, 100665543,
            "System.Collections.IEnumerator.get_Current", 2529, "20001C",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "System.Object", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), true, 4, 12));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AssertCommand", 33554507, 100663908, "get_Running", 2198, "000002",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 2, 6));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AssertCommand", 33554507, 100663909, "set_Running", 2193, "00010102",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "System.Boolean" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("value", 0, 0, "System.Boolean", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Boolean"), 1, 134218533)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AssertCommand", 33554507, 100663910, "Assert", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100010000"),
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "01000641737365727402005355175441532E496E7075742E4578656375746554696D696E670D4578656375746554696D696E67035350104D6574614461746150726F7669646572255441532E496E7075742E436F6D6D616E64732E417373657274436F6D6D616E642B4D657461")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218534),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218535),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218536),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218537)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 339, 976));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AssertCommand+Meta", 33554709, 100665544, "get_Insert", 2534, "20000E",
            new KeyValuePair<string, string>[] { }, "System.String", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 2, 6));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AssertCommand+Meta", 33554709, 100665545, "get_HasArguments", 2534, "200002",
            new KeyValuePair<string, string>[] { }, "System.Boolean", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 2, 2));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AssertCommand+Meta", 33554709, 100665546, "GetAutoCompleteEntries", 486,
            "2003151275011180D51D0E0E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.IteratorStateMachineAttribute",
                    "0100425441532E496E7075742E436F6D6D616E64732E417373657274436F6D6D616E642B4D6574612B3C4765744175746F436F6D706C657465456E74726965733E645F5F340000")
            }, "System.Collections.Generic.IEnumerator`1<StudioCommunication.CommandAutoCompleteEntry>",
            new string[] { "System.String[]", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("args", 0, 0, "System.String[]", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String"), arrayRank: 1,
                        arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                            { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) }), 1,
                    134220431),
                new SourceMethodParameterDefinition("filePath", 1, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 2, 134220432),
                new SourceMethodParameterDefinition("fileLine", 2, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 3, 134220433)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
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
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")
                }), true, 6, 14));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AssertCommand+Meta", 33554709, 100665547, ".ctor", 6278, "200001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AssertCommand+Meta+<GetAutoCompleteEntries>d__4", 33555072, 100667022, ".ctor", 6278,
            "20010108",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "System.Void", new string[] { "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("<>1__state", 0, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 1, 134221497)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 6, 14));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AssertCommand+Meta+<GetAutoCompleteEntries>d__4", 33555072, 100667023,
            "System.IDisposable.Dispose", 481, "200001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "System.Void", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 7, 16));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AssertCommand+Meta+<GetAutoCompleteEntries>d__4", 33555072, 100667024, "MoveNext", 481,
            "200002", new KeyValuePair<string, string>[] { }, "System.Boolean", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 77, 184));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AssertCommand+Meta+<GetAutoCompleteEntries>d__4", 33555072, 100667025,
            "System.Collections.Generic.IEnumerator<StudioCommunication.CommandAutoCompleteEntry>.get_Current", 2529,
            "20001180D5",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "StudioCommunication.CommandAutoCompleteEntry", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.CommandAutoCompleteEntry"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AssertCommand+Meta+<GetAutoCompleteEntries>d__4", 33555072, 100667026,
            "System.Collections.IEnumerator.Reset", 481, "200001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "System.Void", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 2, 6));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AssertCommand+Meta+<GetAutoCompleteEntries>d__4", 33555072, 100667027,
            "System.Collections.IEnumerator.get_Current", 2529, "20001C",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "System.Object", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), true, 4, 12));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand", 33554508, 100663911, "Clear", 145, "000001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("TAS.Input.ClearInputsAttribute", "01000000") }, "System.Void",
            new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand", 33554508, 100663912, "ParseFileEnd", 145, "000001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("TAS.Input.ParseFileEndAttribute", "01000000") }, "System.Void",
            new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 95, 316));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand", 33554508, 100663913, "AutoInput", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "0100094175746F496E70757402005355175441532E496E7075742E4578656375746554696D696E670D4578656375746554696D696E67015350104D6574614461746150726F7669646572285441532E496E7075742E436F6D6D616E64732E4175746F496E707574436F6D6D616E642B4D657461")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218538),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218539),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218540),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218541)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 75, 219));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand", 33554508, 100663914, "StartAutoInput", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "01000E53746172744175746F496E70757401005355175441532E496E7075742E4578656375746554696D696E670D4578656375746554696D696E6701")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218542),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218543),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218544),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218545)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 56, 169));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand", 33554508, 100663915, "EndAutoInput", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "01000C456E644175746F496E70757401005355175441532E496E7075742E4578656375746554696D696E670D4578656375746554696D696E6701")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218546),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218547),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218548),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218549)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 6, 18));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand", 33554508, 100663916, "EndAutoInputImpl", 150, "0004010E080E0E",
            new KeyValuePair<string, string>[] { }, "System.Void",
            new string[] { "System.String", "System.Int32", "System.String", "System.String" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("filePath", 0, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 1, 134218550),
                new SourceMethodParameterDefinition("fileLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218551),
                new SourceMethodParameterDefinition("name", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218552),
                new SourceMethodParameterDefinition("pairedName", 3, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 4, 134218553)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 69, 210));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand", 33554508, 100663917, "SkipInput", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "010009536B6970496E7075740200531D0E07416C6961736573010000000D536B69704175746F496E7075745355175441532E496E7075742E4578656375746554696D696E670D4578656375746554696D696E6703")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218554),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218555),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218556),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218557)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 115, 309));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand", 33554508, 100663918, "TryInsert", 150, "0006020E080E080808",
            new KeyValuePair<string, string>[] { }, "System.Boolean",
            new string[]
                { "System.String", "System.Int32", "System.String", "System.Int32", "System.Int32", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("filePath", 0, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 1, 134218558),
                new SourceMethodParameterDefinition("fileLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218559),
                new SourceMethodParameterDefinition("lineText", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218560),
                new SourceMethodParameterDefinition("studioLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218561),
                new SourceMethodParameterDefinition("repeatIndex", 4, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 5, 134218562),
                new SourceMethodParameterDefinition("repeatCount", 5, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 6, 134218563)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 153, 348));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand", 33554508, 100663919, "ParseInsertedLines", 150,
            "0005011284600E080808", new KeyValuePair<string, string>[] { }, "System.Void",
            new string[]
            {
                "TAS.Input.Commands.AutoInputCommand+Arguments", "System.String", "System.Int32", "System.Int32",
                "System.Int32"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("arguments", 0, 0, "TAS.Input.Commands.AutoInputCommand+Arguments",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Commands.AutoInputCommand+Arguments"), 1, 134218564),
                new SourceMethodParameterDefinition("filePath", 1, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 2, 134218565),
                new SourceMethodParameterDefinition("studioLine", 2, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 3, 134218566),
                new SourceMethodParameterDefinition("repeatIndex", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218567),
                new SourceMethodParameterDefinition("repeatCount", 4, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 5, 134218568)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 34, 90));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand", 33554508, 100663920, ".cctor", 6289, "000001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand+Meta", 33554711, 100665548, "get_Insert", 2534, "20000E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100010000")
            }, "System.String", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 2, 6));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand+Meta", 33554711, 100665549, "get_HasArguments", 2534, "200002",
            new KeyValuePair<string, string>[] { }, "System.Boolean", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 2, 2));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand+Meta", 33554711, 100665550, ".ctor", 6278, "200001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand+Arguments", 33554712, 100665551, "get_EqualityContract", 2500,
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
            "TAS.Input.Commands.AutoInputCommand+Arguments", 33554712, 100665552, ".ctor", 6278, "2002010808",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { "System.Int32", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("startLine", 0, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 1, 134220434),
                new SourceMethodParameterDefinition("cycleLength", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134220435)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 14, 30));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand+Arguments", 33554712, 100665553, "ToString", 198, "20000E",
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
            "TAS.Input.Commands.AutoInputCommand+Arguments", 33554712, 100665554, "PrintMembers", 452, "2001021280AD",
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
                        typeIdentity: "System.Text.StringBuilder"), 1, 134220436)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 111, 356));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand+Arguments", 33554712, 100665555, "op_Inequality", 2198,
            "000202128460128460",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
                { "TAS.Input.Commands.AutoInputCommand+Arguments", "TAS.Input.Commands.AutoInputCommand+Arguments" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0, "TAS.Input.Commands.AutoInputCommand+Arguments",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Commands.AutoInputCommand+Arguments"), 1, 134220437),
                new SourceMethodParameterDefinition("right", 1, 0, "TAS.Input.Commands.AutoInputCommand+Arguments",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Commands.AutoInputCommand+Arguments"), 2, 134220438)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 6, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand+Arguments", 33554712, 100665556, "op_Equality", 2198,
            "000202128460128460",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean",
            new string[]
                { "TAS.Input.Commands.AutoInputCommand+Arguments", "TAS.Input.Commands.AutoInputCommand+Arguments" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("left", 0, 0, "TAS.Input.Commands.AutoInputCommand+Arguments",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Commands.AutoInputCommand+Arguments"), 1, 134220439),
                new SourceMethodParameterDefinition("right", 1, 0, "TAS.Input.Commands.AutoInputCommand+Arguments",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Commands.AutoInputCommand+Arguments"), 2, 134220440)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 13, 19));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand+Arguments", 33554712, 100665557, "GetHashCode", 198, "200008",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Int32", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 75, 247));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand+Arguments", 33554712, 100665558, "Equals", 198, "2001021C",
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
                        typeIdentity: "System.Object"), 1, 134220441)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 13));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand+Arguments", 33554712, 100665559, "Equals", 454, "200102128460",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000"),
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { "TAS.Input.Commands.AutoInputCommand+Arguments" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("other", 0, 0, "TAS.Input.Commands.AutoInputCommand+Arguments",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Commands.AutoInputCommand+Arguments"), 1, 134220442)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 85, 290));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand+Arguments", 33554712, 100665560, "<Clone>$", 454, "2000128460",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "TAS.Input.Commands.AutoInputCommand+Arguments", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "TAS.Input.Commands.AutoInputCommand+Arguments"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.AutoInputCommand+Arguments", 33554712, 100665561, ".ctor", 6276, "200101128460",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "TAS.Input.Commands.AutoInputCommand+Arguments" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("original", 0, 0, "TAS.Input.Commands.AutoInputCommand+Arguments",
                    false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Commands.AutoInputCommand+Arguments"), 1, 134220443)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 43, 127));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663921, "Load", 145, "000001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("TAS.Module.LoadAttribute", "0100000000000000") }, "System.Void",
            new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 48, 153));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663922, "Unload", 145, "000001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("TAS.Module.UnloadAttribute", "0100000000000000") }, "System.Void",
            new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 31, 97));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663923, "HookLevelOrigLoadLevel", 145,
            "00020112817D128179", new KeyValuePair<string, string>[] { }, "System.Void",
            new string[] { "MonoMod.Cil.ILCursor", "MonoMod.Cil.ILContext" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("ilCursor", 0, 0, "MonoMod.Cil.ILCursor", false, null,
                    new byte[] { }, "MonoMod.Utils, Version=25.0.9.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "MonoMod.Utils, Version=25.0.9.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "MonoMod.Cil.ILCursor"), 1, 134218569),
                new SourceMethodParameterDefinition("ilContext", 1, 0, "MonoMod.Cil.ILContext", false, null,
                    new byte[] { }, "MonoMod.Utils, Version=25.0.9.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "MonoMod.Utils, Version=25.0.9.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "MonoMod.Cil.ILContext"), 2, 134218570)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 48, 130));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663924, "SetPlayerRemainder", 145, "0001128145128145",
            new KeyValuePair<string, string>[] { }, "Celeste.Player", new string[] { "Celeste.Player" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("player", 0, 0, "Celeste.Player", false, null, new byte[] { },
                    "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Celeste.Player"), 1, 134218571)
            }, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "Celeste.Player"), true, 11, 40));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663925, "PlayerOnIntroRespawnEnd", 145,
            "00020112839D128145", new KeyValuePair<string, string>[] { }, "System.Void",
            new string[] { "On.Celeste.Player+orig_IntroRespawnEnd", "Celeste.Player" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("orig", 0, 0, "On.Celeste.Player+orig_IntroRespawnEnd", false, null,
                    new byte[] { }, "MMHOOK_Celeste, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "MMHOOK_Celeste, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "On.Celeste.Player+orig_IntroRespawnEnd"), 1, 134218572),
                new SourceMethodParameterDefinition("self", 1, 0, "Celeste.Player", false, null, new byte[] { },
                    "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Celeste.Player"), 2, 134218573)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 25, 84));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663926, "NPC06_Theo_PlateauOnAwake", 145, "000101128179",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { "MonoMod.Cil.ILContext" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("il", 0, 0, "MonoMod.Cil.ILContext", false, null, new byte[] { },
                    "MonoMod.Utils, Version=25.0.9.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "MonoMod.Utils, Version=25.0.9.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "MonoMod.Cil.ILContext"), 1, 134218574)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 62, 166));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663927, "FixCh6FirstRoomLoad", 145, "000002",
            new KeyValuePair<string, string>[] { }, "System.Boolean", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 148, 479));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663928, "LevelOnEnd", 145, "0002011283A9128141",
            new KeyValuePair<string, string>[] { }, "System.Void",
            new string[] { "On.Celeste.Level+orig_End", "Celeste.Level" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("orig", 0, 0, "On.Celeste.Level+orig_End", false, null,
                    new byte[] { }, "MMHOOK_Celeste, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "MMHOOK_Celeste, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "On.Celeste.Level+orig_End"), 1, 134218575),
                new SourceMethodParameterDefinition("self", 1, 0, "Celeste.Level", false, null, new byte[] { },
                    "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Celeste.Level"), 2, 134218576)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 17, 51));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663929, "Console", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "010007636F6E736F6C65020053020F4C6567616C496E46756C6C47616D65005350104D6574614461746150726F7669646572265441532E496E7075742E436F6D6D616E64732E436F6E736F6C65436F6D6D616E642B4D657461")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218577),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218578),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218579),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218580)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 79, 234));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663930, "LoadCommand", 145, "0003010E1D0E08",
            new KeyValuePair<string, string>[] { }, "System.Void",
            new string[] { "System.String", "System.String[]", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("command", 0, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 1, 134218581),
                new SourceMethodParameterDefinition("args", 1, 0, "System.String[]", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String"), arrayRank: 1,
                        arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                            { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) }), 2,
                    134218582),
                new SourceMethodParameterDefinition("slot", 2, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 3, 134218583)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 481, 1329));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663931, "CheckValid", 145, "0002021283AD1D0E",
            new KeyValuePair<string, string>[] { }, "System.Boolean",
            new string[] { "Celeste.SaveData", "System.String[]" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("saveData", 0, 0, "Celeste.SaveData", false, null, new byte[] { },
                    "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Celeste.SaveData"), 1, 134218584),
                new SourceMethodParameterDefinition("args", 1, 0, "System.String[]", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String"), arrayRank: 1,
                        arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                            { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) }), 2,
                    134218585)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 51, 134));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663932, "TryGetAreaId", 147, "0002020E1008",
            new KeyValuePair<string, string>[] { }, "System.Boolean", new string[] { "System.String", "System.Int32&" },
            0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("id", 0, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 1, 134218586),
                new SourceMethodParameterDefinition("areaId", 1, 2, "System.Int32&", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.ByReference,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.Int32")), 2, 134218587)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 31, 57));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663933, "Load", 145, "000401118169080E151180E50108",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000")
            }, "System.Void",
            new string[] { "Celeste.AreaMode", "System.Int32", "System.String", "System.Nullable`1<System.Int32>" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("mode", 0, 0, "Celeste.AreaMode", false, null, new byte[] { },
                    "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Celeste.AreaMode"), 1, 134218588),
                new SourceMethodParameterDefinition("areaId", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218589),
                new SourceMethodParameterDefinition("screen", 2, 4112, "System.String", true, null,
                    new byte[] { 0x00, 0x00, 0x00, 0x00 },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218590),
                new SourceMethodParameterDefinition("spawnPoint", 3, 4112, "System.Nullable`1<System.Int32>", true,
                    null, new byte[] { 0x00, 0x00, 0x00, 0x00 },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
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
                        }), 4, 134218591)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 75, 210));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663934, "Load", 145, "000501118169081180BD1180BD1180BD",
            new KeyValuePair<string, string>[] { }, "System.Void",
            new string[]
            {
                "Celeste.AreaMode", "System.Int32", "Microsoft.Xna.Framework.Vector2",
                "Microsoft.Xna.Framework.Vector2", "Microsoft.Xna.Framework.Vector2"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("mode", 0, 0, "Celeste.AreaMode", false, null, new byte[] { },
                    "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Celeste.AreaMode"), 1, 134218592),
                new SourceMethodParameterDefinition("areaId", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218593),
                new SourceMethodParameterDefinition("spawnPoint", 2, 0, "Microsoft.Xna.Framework.Vector2", false, null,
                    new byte[] { }, "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Microsoft.Xna.Framework.Vector2"), 3, 134218594),
                new SourceMethodParameterDefinition("remainder", 3, 0, "Microsoft.Xna.Framework.Vector2", false, null,
                    new byte[] { }, "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Microsoft.Xna.Framework.Vector2"), 4, 134218595),
                new SourceMethodParameterDefinition("speed", 4, 0, "Microsoft.Xna.Framework.Vector2", false, null,
                    new byte[] { }, "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "FNA, Version=24.1.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Microsoft.Xna.Framework.Vector2"), 5, 134218596)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 83, 232));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663935, "EnterLevel", 145, "0001011282BD",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { "Celeste.LevelLoader" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("levelLoader", 0, 0, "Celeste.LevelLoader", false, null,
                    new byte[] { }, "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Celeste.LevelLoader"), 1, 134218597)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 76, 248));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663936, "CreateConsoleCommand", 150, "00010E02",
            new KeyValuePair<string, string>[] { }, "System.String", new string[] { "System.Boolean" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("simple", 0, 0, "System.Boolean", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Boolean"), 1, 134218598)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 295, 999));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663937, "CmdGiveBerry", 145, "000001",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("Monocle.Command",
                    "01000967697665626572727925476976657320706C61796572206120726564206265727279202843656C65737465544153290000")
            }, "System.Void", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 44, 141));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand", 33554509, 100663938, ".cctor", 6289, "000001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 5, 18));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+Meta", 33554713, 100665562, "get_Insert", 2534, "20000E",
            new KeyValuePair<string, string>[] { }, "System.String", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 2, 6));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+Meta", 33554713, 100665563, "get_HasArguments", 2534, "200002",
            new KeyValuePair<string, string>[] { }, "System.Boolean", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 2, 2));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+Meta", 33554713, 100665564, "GetAutoCompleteEntries", 486,
            "2003151275011180D51D0E0E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.IteratorStateMachineAttribute",
                    "0100435441532E496E7075742E436F6D6D616E64732E436F6E736F6C65436F6D6D616E642B4D6574612B3C4765744175746F436F6D706C657465456E74726965733E645F5F340000")
            }, "System.Collections.Generic.IEnumerator`1<StudioCommunication.CommandAutoCompleteEntry>",
            new string[] { "System.String[]", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("args", 0, 0, "System.String[]", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String"), arrayRank: 1,
                        arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                            { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) }), 1,
                    134220444),
                new SourceMethodParameterDefinition("filePath", 1, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 2, 134220445),
                new SourceMethodParameterDefinition("fileLine", 2, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 3, 134220446)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
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
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")
                }), true, 6, 14));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+Meta", 33554713, 100665565, ".ctor", 6278, "200001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+Meta+<GetAutoCompleteEntries>d__4", 33555073, 100667028, ".ctor", 6278,
            "20010108",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "System.Void", new string[] { "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("<>1__state", 0, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 1, 134221499)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 6, 14));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+Meta+<GetAutoCompleteEntries>d__4", 33555073, 100667029,
            "System.IDisposable.Dispose", 481, "200001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "System.Void", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 53, 173));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+Meta+<GetAutoCompleteEntries>d__4", 33555073, 100667030, "MoveNext", 481,
            "200002", new KeyValuePair<string, string>[] { }, "System.Boolean", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 462, 1422));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+Meta+<GetAutoCompleteEntries>d__4", 33555073, 100667031, "<>m__Finally1",
            129, "200001", new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 10, 27));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+Meta+<GetAutoCompleteEntries>d__4", 33555073, 100667032, "<>m__Finally2",
            129, "200001", new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 10, 27));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+Meta+<GetAutoCompleteEntries>d__4", 33555073, 100667033, "<>m__Finally3",
            129, "200001", new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 8, 26));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+Meta+<GetAutoCompleteEntries>d__4", 33555073, 100667034, "<>m__Finally4",
            129, "200001", new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 8, 25));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+Meta+<GetAutoCompleteEntries>d__4", 33555073, 100667035,
            "System.Collections.Generic.IEnumerator<StudioCommunication.CommandAutoCompleteEntry>.get_Current", 2529,
            "20001180D5",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "StudioCommunication.CommandAutoCompleteEntry", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.CommandAutoCompleteEntry"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+Meta+<GetAutoCompleteEntries>d__4", 33555073, 100667036,
            "System.Collections.IEnumerator.Reset", 481, "200001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "System.Void", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 2, 6));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+Meta+<GetAutoCompleteEntries>d__4", 33555073, 100667037,
            "System.Collections.IEnumerator.get_Current", 2529, "20001C",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "System.Object", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), true, 4, 12));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+<>c", 33554715, 100665566, ".cctor", 6289, "000001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+<>c", 33554715, 100665567, ".ctor", 6278, "200001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+<>c", 33554715, 100665568, "<HookLevelOrigLoadLevel>b__7_0", 131,
            "2001021281D5", new KeyValuePair<string, string>[] { }, "System.Boolean",
            new string[] { "Mono.Cecil.Cil.Instruction" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("i", 0, 0, "Mono.Cecil.Cil.Instruction", false, null,
                    new byte[] { }, "Mono.Cecil, Version=0.11.6.0, Culture=neutral, PublicKeyToken=50cebf1cceb9d05e",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "Mono.Cecil, Version=0.11.6.0, Culture=neutral, PublicKeyToken=50cebf1cceb9d05e",
                        typeIdentity: "Mono.Cecil.Cil.Instruction"), 1, 134220447)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 12));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+<>c", 33554715, 100665569, "<HookLevelOrigLoadLevel>b__7_1", 131,
            "2001021281D5", new KeyValuePair<string, string>[] { }, "System.Boolean",
            new string[] { "Mono.Cecil.Cil.Instruction" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("i", 0, 0, "Mono.Cecil.Cil.Instruction", false, null,
                    new byte[] { }, "Mono.Cecil, Version=0.11.6.0, Culture=neutral, PublicKeyToken=50cebf1cceb9d05e",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "Mono.Cecil, Version=0.11.6.0, Culture=neutral, PublicKeyToken=50cebf1cceb9d05e",
                        typeIdentity: "Mono.Cecil.Cil.Instruction"), 1, 134220448)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 17));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+<>c", 33554715, 100665570, "<NPC06_Theo_PlateauOnAwake>b__10_0", 131,
            "2001021281D5", new KeyValuePair<string, string>[] { }, "System.Boolean",
            new string[] { "Mono.Cecil.Cil.Instruction" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("ins", 0, 0, "Mono.Cecil.Cil.Instruction", false, null,
                    new byte[] { }, "Mono.Cecil, Version=0.11.6.0, Culture=neutral, PublicKeyToken=50cebf1cceb9d05e",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "Mono.Cecil, Version=0.11.6.0, Culture=neutral, PublicKeyToken=50cebf1cceb9d05e",
                        typeIdentity: "Mono.Cecil.Cil.Instruction"), 1, 134220449)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 12));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+<>c", 33554715, 100665571, "<NPC06_Theo_PlateauOnAwake>b__10_1", 131,
            "2001021281D5", new KeyValuePair<string, string>[] { }, "System.Boolean",
            new string[] { "Mono.Cecil.Cil.Instruction" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("ins", 0, 0, "Mono.Cecil.Cil.Instruction", false, null,
                    new byte[] { }, "Mono.Cecil, Version=0.11.6.0, Culture=neutral, PublicKeyToken=50cebf1cceb9d05e",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "Mono.Cecil, Version=0.11.6.0, Culture=neutral, PublicKeyToken=50cebf1cceb9d05e",
                        typeIdentity: "Mono.Cecil.Cil.Instruction"), 1, 134220450)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 12));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+<>c", 33554715, 100665572, "<EnterLevel>b__19_0", 131, "200101128121",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { "Monocle.Entity" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("entity", 0, 0, "Monocle.Entity", false, null, new byte[] { },
                    "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Monocle.Entity"), 1, 134220451)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 10, 31));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+<>c", 33554715, 100665573, "<EnterLevel>b__19_1", 131, "200101128121",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { "Monocle.Entity" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("entity", 0, 0, "Monocle.Entity", false, null, new byte[] { },
                    "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Monocle.Entity"), 1, 134220452)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 10, 31));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+<>c", 33554715, 100665574, "<CreateConsoleCommand>b__20_0", 131,
            "20010E0E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100000000")
            }, "System.String", new string[] { "System.String" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("val", 0, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 1, 134220453)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 11, 29));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+<>c__DisplayClass9_0", 33554716, 100665575, ".ctor", 6278, "200001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ConsoleCommand+<>c__DisplayClass9_0", 33554716, 100665576,
            "<PlayerOnIntroRespawnEnd>b__0", 131, "200001", new KeyValuePair<string, string>[] { }, "System.Void",
            new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 7, 27));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.DummyCommands", 33554510, 100663939, "Author", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "010007417574686F723A010053020C43616C63436865636B73756D00")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218599),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218600),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218601),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218602)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 1, 1));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.DummyCommands", 33554510, 100663940, "FrameCount", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "01000B4672616D65436F756E743A010053020C43616C63436865636B73756D00")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218603),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218604),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218605),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218606)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 1, 1));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.DummyCommands", 33554510, 100663941, "TotalRecordCount", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "010011546F74616C5265636F7264436F756E743A010053020C43616C63436865636B73756D00")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218607),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218608),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218609),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218610)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 1, 1));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.EnforceLegalCommand", 33554511, 100663942, "get_EnabledWhenRunning", 2198, "000002",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 2, 6));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.EnforceLegalCommand", 33554511, 100663943, "set_EnabledWhenRunning", 2193, "00010102",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "System.Boolean" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("value", 0, 0, "System.Boolean", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Boolean"), 1, 134218611)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.EnforceLegalCommand", 33554511, 100663944, "get_EnabledWhenParsing", 2198, "000002",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Boolean", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 2, 6));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.EnforceLegalCommand", 33554511, 100663945, "set_EnabledWhenParsing", 2193, "00010102",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { "System.Boolean" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("value", 0, 0, "System.Boolean", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Boolean"), 1, 134218612)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.EnforceLegalCommand", 33554511, 100663946, "EnforceLegal", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100010000"),
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "01000C456E666F7263654C6567616C0200531D0E07416C6961736573010000000F456E666F7263654D61696E47616D655355175441532E496E7075742E4578656375746554696D696E670D4578656375746554696D696E6703")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218613),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218614),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218615),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218616)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 8, 21));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.EnforceLegalCommand", 33554511, 100663947, "DisableRun", 145, "000001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("TAS.DisableRunAttribute", "0100000000000000") }, "System.Void",
            new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.EnforceLegalCommand", 33554511, 100663948, "ClearInputs", 145, "000001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("TAS.Input.ClearInputsAttribute", "01000000") }, "System.Void",
            new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.ExitGameCommand", 33554512, 100663949, "ExitGame", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100010000"),
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute", "0100084578697447616D650000")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218617),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218618),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218619),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218620)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 18, 54));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.GunCommand", 33554513, 100663950, "Gun", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100010000"),
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "01000347756E020053020F4C6567616C496E46756C6C47616D65005350104D6574614461746150726F7669646572225441532E496E7075742E436F6D6D616E64732E47756E436F6D6D616E642B4D657461")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218621),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218622),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218623),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218624)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 69, 179));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.GunCommand", 33554513, 100663951, ".cctor", 6289, "000001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 11, 53));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.GunCommand+Meta", 33554717, 100665577, "get_Insert", 2534, "20000E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100010000")
            }, "System.String", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 2, 6));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.GunCommand+Meta", 33554717, 100665578, "get_HasArguments", 2534, "200002",
            new KeyValuePair<string, string>[] { }, "System.Boolean", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 2, 2));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.GunCommand+Meta", 33554717, 100665579, ".ctor", 6278, "200001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.GunCommand+<>c", 33554718, 100665580, ".cctor", 6289, "000001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.GunCommand+<>c", 33554718, 100665581, ".ctor", 6278, "200001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.GunCommand+<>c", 33554718, 100665582, "<.cctor>b__4_0", 131, "20001280FD",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000")
            }, "System.Reflection.PropertyInfo", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Reflection.PropertyInfo"), true, 13, 35));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.GunCommand+<>c", 33554718, 100665583, "<.cctor>b__4_1", 131, "2000128241",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100020000")
            }, "System.Reflection.MethodInfo", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Reflection.MethodInfo"), true, 14, 36));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand", 33554514, 100663952, "ReportError", 145, "0001010E",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { "System.String" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("message", 0, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 1, 134218625)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 48, 172));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand", 33554514, 100663953, "InvokeCmd", 145, "000001",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("Monocle.Command",
                    "010006496E766F6B655A496E766F6B65206C6576656C2F73657373696F6E2F656E74697479206D6574686F642E20656720696E766F6B65204C6576656C2E50617573653B20696E766F6B6520506C617965722E4A756D70202843656C65737465544153290000")
            }, "System.Void", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 15, 50));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand", 33554514, 100663954, "Invoke", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "010006496E766F6B65020053020F4C6567616C496E46756C6C47616D65005350104D6574614461746150726F76696465722B5441532E496E7075742E436F6D6D616E64732E496E766F6B65436F6D6D616E642B496E766F6B654D657461")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218626),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218627),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218628),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218629)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 13, 44));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand", 33554514, 100663955, "Invoke", 147, "0001011D0E",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { "System.String[]" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("args", 0, 0, "System.String[]", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String"), arrayRank: 1,
                        arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                            { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) }), 1,
                    134218630)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 27, 71));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta", 33554719, 100665584, "get_Insert", 2534, "20000E",
            new KeyValuePair<string, string>[] { }, "System.String", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 2, 6));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta", 33554719, 100665585, "get_HasArguments", 2534, "200002",
            new KeyValuePair<string, string>[] { }, "System.Boolean", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 2, 2));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta", 33554719, 100665586, "GetHash", 486, "2003081D0E0E08",
            new KeyValuePair<string, string>[] { }, "System.Int32",
            new string[] { "System.String[]", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("args", 0, 0, "System.String[]", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String"), arrayRank: 1,
                        arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                            { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) }), 1,
                    134220454),
                new SourceMethodParameterDefinition("filePath", 1, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 2, 134220455),
                new SourceMethodParameterDefinition("fileLine", 2, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 3, 134220456)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Int32"), true, 64, 174));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta", 33554719, 100665587, "GetAutoCompleteEntries", 486,
            "2003151275011180D51D0E0E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.IteratorStateMachineAttribute",
                    "0100485441532E496E7075742E436F6D6D616E64732E496E766F6B65436F6D6D616E642B496E766F6B654D6574612B3C4765744175746F436F6D706C657465456E74726965733E645F5F350000")
            }, "System.Collections.Generic.IEnumerator`1<StudioCommunication.CommandAutoCompleteEntry>",
            new string[] { "System.String[]", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("args", 0, 0, "System.String[]", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.SzArray,
                        elementType: new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                            global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                            assemblyIdentity:
                            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                            typeIdentity: "System.String"), arrayRank: 1,
                        arrayDimensions: new global::Celeste.Core.Reflection.Types.SourceArrayDimension[]
                            { new global::Celeste.Core.Reflection.Types.SourceArrayDimension(null, null) }), 1,
                    134220457),
                new SourceMethodParameterDefinition("filePath", 1, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 2, 134220458),
                new SourceMethodParameterDefinition("fileLine", 2, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 3, 134220459)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
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
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandAutoCompleteEntry")
                }), true, 6, 14));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta", 33554719, 100665588, ".ctor", 6278, "200001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<>c", 33555074, 100667038, ".cctor", 6289, "000001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<>c", 33555074, 100667039, ".ctor", 6278, "200001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<>c", 33555074, 100667040, "<GetHash>b__4_0", 131,
            "20021183E51183E50E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100000000")
            }, "StudioCommunication.Util.StableHashCode",
            new string[] { "StudioCommunication.Util.StableHashCode", "System.String" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("argHash", 0, 0, "StudioCommunication.Util.StableHashCode", false,
                    null, new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.Util.StableHashCode"), 1, 134221501),
                new SourceMethodParameterDefinition("arg", 1, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 2, 134221502)
            }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.Util.StableHashCode"), true, 5, 14));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<>c", 33555074, 100667041, "<GetHash>b__4_1", 131,
            "20021183E51183E50E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100000000")
            }, "StudioCommunication.Util.StableHashCode",
            new string[] { "StudioCommunication.Util.StableHashCode", "System.String" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("argHash", 0, 0, "StudioCommunication.Util.StableHashCode", false,
                    null, new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.Util.StableHashCode"), 1, 134221503),
                new SourceMethodParameterDefinition("arg", 1, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 2, 134221504)
            }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.Util.StableHashCode"), true, 5, 14));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<>c", 33555074, 100667042, "<GetAutoCompleteEntries>b__5_1",
            131, "2001021280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100000000")
            }, "System.Boolean", new string[] { "System.Type" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("type", 0, 0, "System.Type", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Type"), 1, 134221505)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 4, 8));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<>c__DisplayClass5_0", 33555075, 100667043, ".ctor", 6278,
            "200001", new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<>c__DisplayClass5_0", 33555075, 100667044,
            "<GetAutoCompleteEntries>b__0", 131, "20011280C91280C9",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100000000")
            }, "System.Type", new string[] { "System.Type" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("type", 0, 0, "System.Type", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Type"), 1, 134221506)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Type"), true, 6, 14));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<GetAutoCompleteEntries>d__5", 33555076, 100667045, ".ctor",
            6278, "20010108",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "System.Void", new string[] { "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("<>1__state", 0, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 1, 134221507)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 6, 14));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<GetAutoCompleteEntries>d__5", 33555076, 100667046,
            "System.IDisposable.Dispose", 481, "200001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "System.Void", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 28, 89));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<GetAutoCompleteEntries>d__5", 33555076, 100667047, "MoveNext",
            481, "200002", new KeyValuePair<string, string>[] { }, "System.Boolean", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 195, 518));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<GetAutoCompleteEntries>d__5", 33555076, 100667048,
            "<>m__Finally1", 129, "200001", new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 10, 27));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<GetAutoCompleteEntries>d__5", 33555076, 100667049,
            "<>m__Finally2", 129, "200001", new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 10, 27));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<GetAutoCompleteEntries>d__5", 33555076, 100667050,
            "System.Collections.Generic.IEnumerator<StudioCommunication.CommandAutoCompleteEntry>.get_Current", 2529,
            "20001180D5",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "StudioCommunication.CommandAutoCompleteEntry", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                typeIdentity: "StudioCommunication.CommandAutoCompleteEntry"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<GetAutoCompleteEntries>d__5", 33555076, 100667051,
            "System.Collections.IEnumerator.Reset", 481, "200001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "System.Void", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 2, 6));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.InvokeCommand+InvokeMeta+<GetAutoCompleteEntries>d__5", 33555076, 100667052,
            "System.Collections.IEnumerator.get_Current", 2529, "20001C",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("System.Diagnostics.DebuggerHiddenAttribute", "01000000") },
            "System.Object", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Object"), true, 4, 12));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663956, "Load", 145, "000001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("TAS.Module.LoadAttribute", "0100000000000000") }, "System.Void",
            new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 45, 145));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663957, "Unload", 145, "000001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("TAS.Module.UnloadAttribute", "0100000000000000") }, "System.Void",
            new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 11, 33));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663958, "ResetTimes", 145, "000001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("TAS.EnableRunAttribute", "0100000000000000") }, "System.Void",
            new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 7, 29));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663959, "UpdateTimes", 145, "000001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("TAS.DisableRunAttribute", "0100000000000000") }, "System.Void",
            new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 81, 295));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663960, "SaveState", 145, "0001011512809D020E1C",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("TAS.ModInterop.SaveStateAttribute", "0100000000000000") },
            "System.Void", new string[] { "System.Collections.Generic.Dictionary`2<System.String,System.Object>" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("data", 0, 0,
                    "System.Collections.Generic.Dictionary`2<System.String,System.Object>", false, null, new byte[] { },
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
                                typeIdentity: "System.Object")
                        }), 1, 134218631)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 15, 59));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663961, "LoadState", 145, "0001011512809D020E1C",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("TAS.ModInterop.LoadStateAttribute", "0100000000000000") },
            "System.Void", new string[] { "System.Collections.Generic.Dictionary`2<System.String,System.Object>" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("data", 0, 0,
                    "System.Collections.Generic.Dictionary`2<System.String,System.Object>", false, null, new byte[] { },
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
                                typeIdentity: "System.Object")
                        }), 1, 134218632)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 16, 64));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663962, "AdvanceRealTime", 145, "000001",
            new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("TAS.Gameplay.Events+PreEngineUpdate", "0100000000000000") },
            "System.Void", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 41, 140));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663963, "UpdateChapterTime", 145, "000101128141",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { "Celeste.Level" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("level", 0, 0, "Celeste.Level", false, null, new byte[] { },
                    "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "Celeste.Level"), 1, 134218633)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 20, 63));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663964, "UpdateRecordCount", 150, "000101128120",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { "TAS.Input.InputController" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("inputController", 0, 0, "TAS.Input.InputController", false, null,
                    new byte[] { }, "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.InputController"), 1, 134218634)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 21, 73));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663965, "RecordCountCommand", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "01000B5265636F7264436F756E740300531D0E07416C6961736573020000000C5265636F7264436F756E743A0E5265636F7264436F756E74EFBC9A53020C43616C63436865636B73756D005350104D6574614461746150726F7669646572335441532E496E7075742E436F6D6D616E64732E4D65746164617461436F6D6D616E64732B5265636F7264436F756E744D657461")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218635),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218636),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218637),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218638)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 1, 1));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663966, "FileTimeCommand", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "01000846696C6554696D650200531D0E07416C6961736573020000000946696C6554696D653A0B46696C6554696D65EFBC9A53020C43616C63436865636B73756D00")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218639),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218640),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218641),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218642)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 1, 1));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663967, "ChapterTimeCommand", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "01000B4368617074657254696D650200531D0E07416C6961736573020000000C4368617074657254696D653A0E4368617074657254696D65EFBC9A53020C43616C63436865636B73756D00")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218643),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218644),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218645),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218646)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 1, 1));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663968, "RealTimeCommand", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "0100085265616C54696D650200531D0E07416C696173657302000000095265616C54696D653A0B5265616C54696D65EFBC9A53020C43616C63436865636B73756D00")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218647),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218648),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218649),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218650)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 1, 1));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663969, "MidwayFileTimeCommand", 145,
            "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "01000E4D696477617946696C6554696D650200531D0E07416C6961736573020000000F4D696477617946696C6554696D653A114D696477617946696C6554696D65EFBC9A53020C43616C63436865636B73756D00")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218651),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218652),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218653),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218654)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 27, 93));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663970, "MidwayChapterTimeCommand", 145,
            "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "0100114D69647761794368617074657254696D650200531D0E07416C696173657302000000124D69647761794368617074657254696D653A144D69647761794368617074657254696D65EFBC9A53020C43616C63436865636B73756D00")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218655),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218656),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218657),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218658)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 29, 94));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663971, "MidwayRealTimeCommand", 145,
            "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "01000E4D69647761795265616C54696D650200531D0E07416C6961736573020000000F4D69647761795265616C54696D653A114D69647761795265616C54696D65EFBC9A53020C43616C63436865636B73756D00")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218659),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218660),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218661),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218662)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 25, 86));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663972, "ActiveWarpsCommand", 145, "0004011180CD080E08",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("TAS.Input.TasCommandAttribute",
                    "0100134163746976617465644C6F62627957617270730200531D0E07416C696173657302000000144163746976617465644C6F62627957617270733A164163746976617465644C6F6262795761727073EFBC9A53020C43616C63436865636B73756D00")
            }, "System.Void",
            new string[] { "StudioCommunication.CommandLine", "System.Int32", "System.String", "System.Int32" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandLine", 0, 0, "StudioCommunication.CommandLine", false, null,
                    new byte[] { }, "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity: "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "StudioCommunication.CommandLine"), 1, 134218663),
                new SourceMethodParameterDefinition("studioLine", 1, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 2, 134218664),
                new SourceMethodParameterDefinition("filePath", 2, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 3, 134218665),
                new SourceMethodParameterDefinition("fileLine", 3, 0, "System.Int32", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.Int32"), 4, 134218666)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 1, 1));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663973, "UpdateAllMetadata", 145,
            "0003010E151281D1021181040E151281D10211810402", new KeyValuePair<string, string>[] { }, "System.Void",
            new string[]
            {
                "System.String", "System.Func`2<TAS.Input.Command,System.String>",
                "System.Func`2<TAS.Input.Command,System.Boolean>"
            }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("commandName", 0, 0, "System.String", false, null, new byte[] { },
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                        typeIdentity: "System.String"), 1, 134218667),
                new SourceMethodParameterDefinition("getMetadata", 1, 0,
                    "System.Func`2<TAS.Input.Command,System.String>", false, null, new byte[] { },
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
                                "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                                typeIdentity: "TAS.Input.Command"),
                            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                                assemblyIdentity:
                                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                                typeIdentity: "System.String")
                        }), 2, 134218668),
                new SourceMethodParameterDefinition("predicate", 2, 4112,
                    "System.Func`2<TAS.Input.Command,System.Boolean>", true, null,
                    new byte[] { 0x00, 0x00, 0x00, 0x00 },
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
                                "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                                typeIdentity: "TAS.Input.Command"),
                            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                                assemblyIdentity:
                                "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                                typeIdentity: "System.Boolean")
                        }), 3, 134218669)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 120, 355));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands", 33554515, 100663974, "<Load>g__StartFileTime|3_0", 147, "000001",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.CompilerGeneratedAttribute",
                    "01000000")
            }, "System.Void", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 24, 80));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands+RecordCountMeta", 33554720, 100665589, "get_Insert", 2534, "20000E",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("System.Runtime.CompilerServices.NullableContextAttribute",
                    "0100010000")
            }, "System.String", new string[] { }, 0, new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 2, 6));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands+RecordCountMeta", 33554720, 100665590, "get_HasArguments", 2534,
            "200002", new KeyValuePair<string, string>[] { }, "System.Boolean", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 2, 2));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands+RecordCountMeta", 33554720, 100665591, ".ctor", 6278, "200001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands+<>c", 33554722, 100665592, ".cctor", 6289, "000001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 11));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands+<>c", 33554722, 100665593, ".ctor", 6278, "200001",
            new KeyValuePair<string, string>[] { }, "System.Void", new string[] { }, 0,
            new SourceMethodParameterDefinition[] { },
            "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Void"), true, 3, 7));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands+<>c", 33554722, 100665594, "<UpdateTimes>b__7_0", 131, "20010E118104",
            new KeyValuePair<string, string>[] { }, "System.String", new string[] { "TAS.Input.Command" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("_", 0, 0, "TAS.Input.Command", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Command"), 1, 134220460)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 8, 32));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands+<>c", 33554722, 100665595, "<UpdateTimes>b__7_1", 131, "20010E118104",
            new KeyValuePair<string, string>[] { }, "System.String", new string[] { "TAS.Input.Command" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("_", 0, 0, "TAS.Input.Command", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Command"), 1, 134220461)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 29, 103));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands+<>c", 33554722, 100665596, "<UpdateRecordCount>b__12_0", 131,
            "20010E118104", new KeyValuePair<string, string>[] { }, "System.String",
            new string[] { "TAS.Input.Command" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("command", 0, 0, "TAS.Input.Command", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Command"), 1, 134220462)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 14, 37));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands+<>c", 33554722, 100665597, "<UpdateRecordCount>b__12_1", 131,
            "200102118104", new KeyValuePair<string, string>[] { }, "System.Boolean",
            new string[] { "TAS.Input.Command" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("command", 0, 0, "TAS.Input.Command", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Command"), 1, 134220463)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 10, 29));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands+<>c", 33554722, 100665598, "<MidwayFileTimeCommand>b__18_0", 131,
            "20010E118104", new KeyValuePair<string, string>[] { }, "System.String",
            new string[] { "TAS.Input.Command" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("_", 0, 0, "TAS.Input.Command", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Command"), 1, 134220464)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 8, 32));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands+<>c", 33554722, 100665599, "<MidwayFileTimeCommand>b__18_1", 131,
            "200102118104", new KeyValuePair<string, string>[] { }, "System.Boolean",
            new string[] { "TAS.Input.Command" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("command", 0, 0, "TAS.Input.Command", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Command"), 1, 134220465)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 17));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands+<>c", 33554722, 100665600, "<MidwayChapterTimeCommand>b__19_1", 131,
            "200102118104", new KeyValuePair<string, string>[] { }, "System.Boolean",
            new string[] { "TAS.Input.Command" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("command", 0, 0, "TAS.Input.Command", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Command"), 1, 134220466)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 17));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands+<>c", 33554722, 100665601, "<MidwayRealTimeCommand>b__20_0", 131,
            "20010E118104", new KeyValuePair<string, string>[] { }, "System.String",
            new string[] { "TAS.Input.Command" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("_", 0, 0, "TAS.Input.Command", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Command"), 1, 134220467)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.String"), true, 29, 103));
        catalog.RegisterDefinition(new SourceMethodDefinition(
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "TAS.Input.Commands.MetadataCommands+<>c", 33554722, 100665602, "<MidwayRealTimeCommand>b__20_1", 131,
            "200102118104", new KeyValuePair<string, string>[] { }, "System.Boolean",
            new string[] { "TAS.Input.Command" }, 0,
            new SourceMethodParameterDefinition[]
            {
                new SourceMethodParameterDefinition("command", 0, 0, "TAS.Input.Command", false, null, new byte[] { },
                    "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                        global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                        assemblyIdentity:
                        "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        typeIdentity: "TAS.Input.Command"), 1, 134220468)
            }, "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            new global::Celeste.Core.Reflection.Types.SourceTypeReference(
                global::Celeste.Core.Reflection.Types.SourceTypeReferenceKind.Named,
                assemblyIdentity: "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                typeIdentity: "System.Boolean"), true, 5, 17));
    }
}
