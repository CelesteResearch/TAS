using Celeste.Everest;
using Celeste.Everest.Modules.Capabilities;
using System;
using System.Linq;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;
using Celeste.Everest.Modules.Runtime;
using Celeste.Everest.Reflection;
using Celeste.Everest.Diagnostics.Logging;

namespace Celeste.Mods.Tas.Interop
{
    internal sealed class SourceTasExtendedVariantsInterop
    {
        private readonly TasPlaybackDriver driver;

        private SourceMemberCatalog Members =>
            ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMembers;

        private SourceAssemblyMethodCatalog Methods =>
            ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMethods;

        private const BindingFlags All = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public |
                                         BindingFlags.NonPublic;

        private readonly Lazy<SourceEverestModule?> module;
        private ISourceVariantValues? ActualVariants => module.Value as ISourceVariantValues;
        private readonly Lazy<object?> triggerManager;
        private readonly Lazy<object?> variantHandlers;
        private readonly Lazy<SourceTasBoundFastInvoker?> getCurrentVariantValue;
        private readonly Lazy<SourceTasBoundFastInvoker?> getCurrentMapDefinedVariantValue;
        private readonly Lazy<SourceTasBoundFastInvoker?> setVariantValue;
        private readonly Lazy<SourceTasBoundFastInvoker?> dictionaryGetItem;
        private readonly Lazy<Type?> variantType;
        private readonly Lazy<object?> upsideDownVariant;
        private readonly Lazy<object?> superDashingVariant;
        private readonly Lazy<object?> colorGradingVariant;

        internal SourceTasExtendedVariantsInterop(TasPlaybackDriver driver)
        {
            this.driver = driver;
            module = new(() => ((ISourceEverestModulesProcessEnvironment)driver.Process).ModulesProcess.Modules
                .FirstOrDefault(value => value.Metadata.Name == "ExtendedVariantMode"));
            triggerManager = new(() => module.Value == null
                ? null
                : Members.GetMember(module.Value.GetType(),
                    "TriggerManager", SourceMemberKind.Field, All, true)?.Read(module.Value));
            variantHandlers = new(() => module.Value == null
                ? null
                : Members.GetMember(module.Value.GetType(),
                    "VariantHandlers", SourceMemberKind.Field, All, true)?.Read(module.Value));
            getCurrentVariantValue =
                new(() => GetFastInvoker(triggerManager.Value?.GetType(), "GetCurrentVariantValue"));
            getCurrentMapDefinedVariantValue = new(() =>
                GetFastInvoker(triggerManager.Value?.GetType(), "GetCurrentMapDefinedVariantValue"));
            setVariantValue = new(() =>
                GetFastInvoker(GetModuleType("ExtendedVariants.UI.ModOptionsEntries"), "SetVariantValue"));
            dictionaryGetItem = new(() => GetFastInvoker(variantHandlers.Value?.GetType(), "get_Item"));
            variantType = new(() =>
                ActualVariants?.VariantEnumType ??
                GetModuleType("ExtendedVariants.Module.ExtendedVariantsModule+Variant"));
            upsideDownVariant = new(ParseVariant("UpsideDown"));
            superDashingVariant = new(ParseVariant("SuperDashing"));
            colorGradingVariant = new(ParseVariant("ColorGrading"));
        }

        private Type GetModuleType(string originalTypeIdentity)
        {
            if (module.Value == null) return null;
            return Members.GetActualSourceType(
                Members.GetOriginalTypeDefinition(module.Value.GetType()).AssemblyIdentity,
                originalTypeIdentity);
        }

        private SourceTasBoundFastInvoker GetFastInvoker(Type type, string name)
        {
            if (type == null) return null;
            var method = Methods.GetMethodInfo(Members, type, name, null, All);
            return method == null ? null : new SourceTasBoundFastInvoker(method);
        }

        public Func<object?> ParseVariant(string value) => () =>
        {
            try
            {
                return variantType.Value == null ? null : Enum.Parse(variantType.Value, value);
            }
            catch (Exception exception)
            {
                ((IEverestLoggerProcessEnvironment)driver.Process).Logger.Log(EverestLogLevel.Error, "CelesteTAS",
                    $"Parsing Variant.{value} Failed.");
                ((IEverestLoggerProcessEnvironment)driver.Process).Logger.LogDetailed(exception, "CelesteTAS");
                return null;
            }
        };

        public bool UpsideDown => GetCurrentVariantValue(upsideDownVariant) is { } value && (bool)value;
        public bool SuperDashing => GetCurrentVariantValue(superDashingVariant) is { } value && (bool)value;
        public string? ColorGrading => GetCurrentVariantValue(colorGradingVariant) as string;
        public Type? GetVariantsEnum() => variantType.Value;

        public Type? GetVariantType(Lazy<object?> variant)
        {
            if (variant.Value is null) return null;
            if (ActualVariants is { } actual) return actual.GetVariantValueType(variant.Value);
            var handler = dictionaryGetItem.Value?.Invoke(variantHandlers.Value, variant.Value);
            if (handler == null) return null;
            var method = Methods.GetMethodInfo(Members, handler.GetType(), "GetVariantType", null, All);
            return (Type?)method?.Invoke(handler, Array.Empty<object>());
        }

        public object? GetCurrentVariantValue(Lazy<object?> variant)
        {
            if (variant.Value is null) return null;
            return ActualVariants is { } actual
                ? actual.GetCurrentVariantValue(variant.Value)
                : getCurrentVariantValue.Value?.Invoke(triggerManager.Value, variant.Value);
        }

        public object? GetCurrentMapDefinedVariantValue(Lazy<object?> variant)
        {
            if (variant.Value is null) return null;
            return ActualVariants is { } actual
                ? actual.GetCurrentMapDefinedVariantValue(variant.Value)
                : getCurrentMapDefinedVariantValue.Value?.Invoke(triggerManager.Value, variant.Value);
        }

        public void SetVariantValue(Lazy<object?> variant, object? value)
        {
            if (variant.Value is null) return;
            if (ActualVariants is { } actual) actual.SetVariantValue(variant.Value, value);
            else setVariantValue.Value?.Invoke(null, variant.Value, value);
        }
    }
}
