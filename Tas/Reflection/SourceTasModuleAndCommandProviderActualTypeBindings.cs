using System;
using System.Linq;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;
using Celeste.Everest.Modules.Runtime;
using Celeste.Game.Reflection.Metadata;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasModuleAndCommandProviderActualTypeBindings
    {
        internal static void Register(SourceMemberCatalog members, SourceAssemblyMethodCatalog methods)
        {
            var module = members.GetOriginalTypeDefinition(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity,
                "TAS.Module.CelesteTasModule");
            if (module.MetadataToken != 33554535 || module.BaseTypeIdentity != "Celeste.Mod.EverestModule"
                                                 || module.BaseAssemblyIdentity !=
                                                 "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                                                 || !typeof(SourceEverestModule).IsAssignableFrom(
                                                     typeof(CelesteTasModule)))
                throw new InvalidOperationException("The genuine TAS module TypeDef or actual module body differs.");
            members.RegisterSourceType(typeof(CelesteTasModule), module.AssemblyIdentity, module.Identity);
            RegisterProvider(members, methods, "TAS.AnalogHelper+Meta", 33554634, 100665207,
                RequireNested(typeof(global::Celeste.Mods.Tas.Input.SourceTasAnalogProcess), "Meta"));
            RegisterProvider(members, methods, "TAS.ExportGameInfo+Meta", 33554635, 100665210,
                RequireNested(typeof(global::Celeste.Mods.Tas.Export.SourceTasExportGameInfo), "Meta"));
            RegisterProvider(members, methods, "TAS.ExportRoomInfo+Meta", 33554638, 100665215,
                RequireNested(typeof(global::Celeste.Mods.Tas.Export.SourceTasExportRoomInfo), "Meta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.AssertCommand+Meta", 33554709, 100665547,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasAssertProcess), "Meta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.AutoInputCommand+Meta", 33554711, 100665550,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasAutoInputProcess), "Meta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.ConsoleCommand+Meta", 33554713, 100665565,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasConsoleProcess), "Meta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.GunCommand+Meta", 33554717, 100665579,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasGunProcess), "Meta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.InvokeCommand+InvokeMeta", 33554719, 100665588,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasInvokeProcess), "InvokeMeta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.MetadataCommands+RecordCountMeta", 33554720,
                100665591,
                typeof(global::Celeste.Mods.Tas.Commands.SourceTasRecordCountMeta));
            RegisterProvider(members, methods, "TAS.Input.Commands.MouseCommand+Meta", 33554727, 100665618,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasMouseProcess), "Meta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.PlayCommand+PlayMeta", 33554730, 100665626,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasPlayProcess), "PlayMeta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.PressCommand+PressMeta", 33554731, 100665630,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasPressProcess), "PressMeta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.ReadCommand+ReadMeta", 33554732, 100665635,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasReadProcess), "ReadMeta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.RepeatCommand+Meta", 33554734, 100665650,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasRepeatProcess), "Meta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.RequireDependencyCommand+Meta", 33554736, 100665671,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasRequireDependencyProcess),
                    "Meta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.SelectCampaignCommand+Meta", 33554747, 100665714,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasSelectCampaignProcess),
                    "Meta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.SetCommand+SetMeta", 33554754, 100665737,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasSetProcess), "SetMeta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.StunPauseCommand+Meta", 33554755, 100665741,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasStunPauseProcess), "Meta"));
            RegisterProvider(members, methods, "TAS.Input.Commands.StunPauseCommand+ModeMeta", 33554756, 100665745,
                RequireNested(typeof(global::Celeste.Mods.Tas.Commands.SourceTasStunPauseProcess), "ModeMeta"));
            RegisterProvider(members, methods, "TAS.Gameplay.DesyncFix.SeededRandomness+Meta", 33554919, 100666458,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededCommandMeta));
            RegisterProvider(members, methods, "TAS.EverestInterop.AreaCompleteInfo+Meta", 33554939, 100666571,
                RequireNested(typeof(global::Celeste.Mods.Tas.Information.SourceTasAreaCompleteInfo), "Meta"));
            RegisterProvider(members, methods, "TAS.EverestInterop.Lua.EvalLuaCommand+Meta", 33554982, 100666752,
                RequireNested(typeof(global::Celeste.Mods.Tas.Lua.SourceTasEvalLuaCommandState), "Meta"));
        }

        private static Type RequireNested(Type owner, string name)
            => owner.GetNestedType(name, BindingFlags.Public | BindingFlags.NonPublic)
               ?? throw new MissingMemberException(owner.FullName, name);

        private static void RegisterProvider(SourceMemberCatalog members, SourceAssemblyMethodCatalog methods,
            string identity, int typeToken, int constructorToken, Type actualType)
        {
            var source = members.GetOriginalTypeDefinition(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity, identity);
            var constructor = methods.RequireDefinition(source.AssemblyIdentity, constructorToken);
            if (source.MetadataToken != typeToken
                || ((TypeAttributes)source.Attributes & (TypeAttributes.Abstract | TypeAttributes.Interface)) != 0
                || source.Interfaces.Count != 1 || source.Interfaces[0] != "TAS.Input.ITasCommandMeta"
                || source.InterfaceAssemblyIdentities.Count != 1
                || source.InterfaceAssemblyIdentities[0] != global::Celeste.Mods.Tas.Reflection.Metadata
                    .SourceTasMethodMetadata.TasAssemblyIdentity
                || constructor.TypeDefinitionToken != typeToken || constructor.DeclaringType != identity
                || constructor.Name != ".ctor" || constructor.ParameterTypeIdentities.Count != 0
                || ((MethodAttributes)constructor.Attributes & MethodAttributes.MemberAccessMask) !=
                MethodAttributes.Public
                || ((MethodAttributes)constructor.Attributes & MethodAttributes.Static) != 0
                || actualType.IsAbstract || actualType.IsInterface
                || !typeof(ISourceTasCommandMeta).IsAssignableFrom(actualType)
                || actualType.GetConstructor(Type.EmptyTypes) == null)
                throw new InvalidOperationException(
                    "The qualified TAS command provider differs from its genuine source TypeDef or constructor: " +
                    identity);
            members.RegisterSourceType(actualType, source.AssemblyIdentity, source.Identity);
        }
    }
}
