using Celeste.Core.Display;
using System;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;
using Celeste.Everest.Modules.Runtime;
using Celeste.Game.Reflection.Metadata;
using Celeste.Mods.Tas.Interop;
using Celeste.Mods.Tas.Module;
using Celeste.Graphics.Display;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasModInteropActualBindings
    {
        internal static void Register(GameApplication process, TasPlaybackDriver driver)
        {
            if (driver == null || !ReferenceEquals(driver.Process, process))
                throw new ArgumentException("TAS ModInterop bindings require their actual same-process driver.",
                    nameof(driver));
            var members = process.SourceMembers;
            var methods = process.Reflection.SourceMethods;
            RegisterType(members, typeof(SourceCelesteTasExports), "TAS.Module.CelesteTasExports", 33554532);
            RegisterType(members, typeof(SourceSpeedrunToolSaveLoadImports),
                "TAS.ModInterop.SpeedrunToolSaveLoadImport",
                33554549);
            var SourceSpeedrunToolSaveLoadImportsOwner = driver.SourceSpeedrunTool.SaveLoadImports;
            BindField(members, SourceSpeedrunToolSaveLoadImportsOwner, typeof(SourceSpeedrunToolSaveLoadImports),
                "TAS.ModInterop.SpeedrunToolSaveLoadImport", 67109217, "RegisterSaveLoadAction",
                "TAS.ModInterop.SpeedrunToolSaveLoadImport+RegisterSaveLoadActionDelegate",
                typeof(SourceSpeedrunToolSaveLoadImports.RegisterSaveLoadActionDelegate));
            BindField(members, SourceSpeedrunToolSaveLoadImportsOwner, typeof(SourceSpeedrunToolSaveLoadImports),
                "TAS.ModInterop.SpeedrunToolSaveLoadImport", 67109218, "RegisterStaticTypes",
                "TAS.ModInterop.SpeedrunToolSaveLoadImport+RegisterStaticTypesDelegate",
                typeof(SourceSpeedrunToolSaveLoadImports.RegisterStaticTypesDelegate));
            BindField(members, SourceSpeedrunToolSaveLoadImportsOwner, typeof(SourceSpeedrunToolSaveLoadImports),
                "TAS.ModInterop.SpeedrunToolSaveLoadImport", 67109219, "Unregister", "System.Action`1<System.Object>",
                typeof(Action<object>));
            BindField(members, SourceSpeedrunToolSaveLoadImportsOwner, typeof(SourceSpeedrunToolSaveLoadImports),
                "TAS.ModInterop.SpeedrunToolSaveLoadImport", 67109220, "DeepClone",
                "TAS.ModInterop.SpeedrunToolSaveLoadImport+DeepCloneDelegate",
                typeof(SourceSpeedrunToolSaveLoadImports.DeepCloneDelegate));
            RegisterType(members, typeof(SourceSpeedrunToolTasActionImports),
                "TAS.ModInterop.SpeedrunToolTasActionImports",
                33554550);
            var SourceSpeedrunToolTasActionImportsOwner = driver.SourceSpeedrunTool.TasActionImports;
            BindField(members, SourceSpeedrunToolTasActionImportsOwner, typeof(SourceSpeedrunToolTasActionImports),
                "TAS.ModInterop.SpeedrunToolTasActionImports", 67109221, "SaveState",
                "System.Func`2<System.String,System.Boolean>", typeof(Func<string, bool>));
            BindField(members, SourceSpeedrunToolTasActionImportsOwner, typeof(SourceSpeedrunToolTasActionImports),
                "TAS.ModInterop.SpeedrunToolTasActionImports", 67109222, "LoadState",
                "System.Func`2<System.String,System.Boolean>", typeof(Func<string, bool>));
            BindField(members, SourceSpeedrunToolTasActionImportsOwner, typeof(SourceSpeedrunToolTasActionImports),
                "TAS.ModInterop.SpeedrunToolTasActionImports", 67109223, "ClearState", "System.Action`1<System.String>",
                typeof(Action<string>));
            BindField(members, SourceSpeedrunToolTasActionImportsOwner, typeof(SourceSpeedrunToolTasActionImports),
                "TAS.ModInterop.SpeedrunToolTasActionImports", 67109224, "TasIsSaved",
                "System.Func`2<System.String,System.Boolean>", typeof(Func<string, bool>));
            BindField(members, SourceSpeedrunToolTasActionImportsOwner, typeof(SourceSpeedrunToolTasActionImports),
                "TAS.ModInterop.SpeedrunToolTasActionImports", 67109225, "InputDeregister", "System.Action",
                typeof(Action));
            BindField(members, SourceSpeedrunToolTasActionImportsOwner, typeof(SourceSpeedrunToolTasActionImports),
                "TAS.ModInterop.SpeedrunToolTasActionImports", 67109226, "OnTasDisableRun", "System.Action",
                typeof(Action));
            RegisterType(members, typeof(SourceTasHelperImports), "TAS.ModInterop.TasHelperInterop+TasHelperImport",
                33554791);
            var SourceTasHelperImportsOwner = driver.SourceTasHelper.Imports;
            BindField(members, SourceTasHelperImportsOwner, typeof(SourceTasHelperImports),
                "TAS.ModInterop.TasHelperInterop+TasHelperImport", 67109889, "InPrediciton",
                "System.Func`1<System.Boolean>", typeof(Func<bool>));
            var exports = driver.SourceExports;
            BindExport(methods, exports, 100664078, "IsTasActive", "System.Boolean", [], typeof(bool), Type.EmptyTypes);
            BindExport(methods, exports, 100664079, "IsTasRunning", "System.Boolean", [], typeof(bool),
                Type.EmptyTypes);
            BindExport(methods, exports, 100664080, "IsTasRecording", "System.Boolean", [], typeof(bool),
                Type.EmptyTypes);
            BindExport(methods, exports, 100664081, "AddSettingsRestoreHandler", "System.Void",
            [
                "Celeste.Mod.EverestModule",
                "System.Nullable`1<System.ValueTuple`2<System.Func`1<System.Object>,System.Action`1<System.Object>>>"
            ], typeof(void), [typeof(SourceEverestModule), typeof((Func<object>, Action<object>)?)]);
            BindExport(methods, exports, 100664082, "RemoveSettingsRestoreHandler", "System.Void",
                ["Celeste.Mod.EverestModule"], typeof(void), [typeof(SourceEverestModule)]);
            BindExport(methods, exports, 100664083, "GetGroupCounter", "System.Int32", [], typeof(int),
                Type.EmptyTypes);
            BindExport(methods, exports, 100664084, "SetGroupCounter", "System.Void", ["System.Int32"], typeof(void),
                [typeof(int)]);
            BindExport(methods, exports, 100664085, "GetLatestSavestateForFrame", "System.Object", ["System.Int32"],
                typeof(object), [typeof(int)]);
            BindExport(methods, exports, 100664086, "GetSavestateFrame", "System.Int32", ["System.Object"], typeof(int),
                [typeof(object)]);
            BindExport(methods, exports, 100664087, "LoadSavestate", "System.Boolean", ["System.Object"], typeof(bool),
                [typeof(object)]);
            BindExport(methods, exports, 100664088, "DrawAccurateLine", "System.Void",
                ["Microsoft.Xna.Framework.Vector2", "Microsoft.Xna.Framework.Vector2", "Microsoft.Xna.Framework.Color"],
                typeof(void), [typeof(Vector2), typeof(Vector2), typeof(PackedColor)]);
            BindExport(methods, exports, 100664089, "ShowStudioPopupMessage", "System.Void",
                ["System.String", "System.String", "System.String"], typeof(void),
                [typeof(string), typeof(string), typeof(string)]);
        }

        private static void RegisterType(SourceMemberCatalog members, Type actualType, string identity, int token)
        {
            var source = members.GetOriginalTypeDefinition(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity, identity);
            if (source.MetadataToken != token || source.BaseTypeIdentity != "System.Object")
                throw new InvalidOperationException("The genuine TAS ModInterop TypeDef differs: " + identity);
            members.RegisterSourceType(actualType, source.AssemblyIdentity, source.Identity);
        }

        private static void BindField(SourceMemberCatalog members, object owner, Type actualType,
            string identity, int token, string name, string originalValueType, Type actualValueType)
        {
            var source = members.RequireDefinition(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity,
                identity, name, SourceMemberKind.Field);
            if (source.MetadataToken != token || !source.IsPublic || !source.IsStatic
                || source.IsInitOnly || source.IsLiteral || source.ValueTypeIdentity != originalValueType)
                throw new InvalidOperationException("The genuine TAS ModInterop import field differs: " +
                                                    source.QualifiedName);
            var actual =
                actualType.GetField(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                ?? throw new MissingFieldException(actualType.FullName, name);
            if (owner == null || owner.GetType() != actualType || actual.IsStatic || actual.IsInitOnly
                || actual.FieldType != actualValueType || !typeof(Delegate).IsAssignableFrom(actualValueType))
                throw new InvalidOperationException("The actual import delegate field or same-process owner differs.");
            members.RegisterBinding(new SourcePhysicalMemberBinding(source, actualType, actual,
                staticSourceOwner: owner));
        }

        private static void BindExport(SourceAssemblyMethodCatalog methods, SourceCelesteTasExports owner,
            int token, string name, string originalReturnType, string[] originalParameters,
            Type actualReturnType, Type[] actualParameters)
        {
            var source = methods.RequireDefinition(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity, token);
            if (source.DeclaringType != "TAS.Module.CelesteTasExports" || source.TypeDefinitionToken != 33554532
                                                                       || source.Name != name ||
                                                                       source.ReturnTypeIdentity !=
                                                                       originalReturnType ||
                                                                       source.GenericParameterCount != 0
                                                                       || ((MethodAttributes)source.Attributes &
                                                                           MethodAttributes.MemberAccessMask) !=
                                                                       MethodAttributes.Public
                                                                       || ((MethodAttributes)source.Attributes &
                                                                           MethodAttributes.Static) == 0
                                                                       || !source.ParameterTypeIdentities.SequenceEqual(
                                                                           originalParameters))
                throw new InvalidOperationException("The genuine TAS export MethodDef differs: " + token);
            var actual = typeof(SourceCelesteTasExports).GetMethod(name,
                             BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly, null,
                             actualParameters, null)
                         ?? throw new MissingMethodException(typeof(SourceCelesteTasExports).FullName, name);
            if (owner == null || actual.IsStatic || actual.ReturnType != actualReturnType ||
                actual.ContainsGenericParameters
                || actual.GetMethodBody() == null ||
                !actual.GetParameters().Select(p => p.ParameterType).SequenceEqual(actualParameters))
                throw new InvalidOperationException("The actual TAS export body, portable ABI or owner differs.");
            methods.BindActualMethod(source.AssemblyIdentity, token, actual, owner);
        }
    }
}
