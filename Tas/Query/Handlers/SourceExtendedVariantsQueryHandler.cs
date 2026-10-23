using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Celeste.Everest.Modules.Runtime;
using StudioCommunication;
using StudioCommunication.Util;
using TargetQuery = Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess;

namespace Celeste.Mods.Tas.Query.Handlers
{
    internal class SourceExtendedVariantsQueryHandler : TargetQuery.Handler
    {
        public SourceExtendedVariantsQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
        }

        private bool IsExtVars(Type type, [NotNullWhen(true)] out SourceEverestModule? module)
        {
            if (!typeof(global::Celeste.Everest.Modules.Settings.EverestModuleSettings).IsAssignableFrom(type))
            {
                module = null;
                return false;
            }

            module = Query.SourceModules.FirstOrDefault(mod => mod.SettingsType == type);
            return module is { Metadata.Name: "ExtendedVariantMode" };
        }

        public override (bool CanEnumerate, bool ShouldOverride) CanEnumerateMemberEntries(Type type,
            TargetQuery.Variant variant, string queryPrefix, int memberIdx, string[] memberArgs)
        {
            return (CanEnumerate: variant is TargetQuery.Variant.Set && IsExtVars(type, out _), ShouldOverride: true);
        }

        public override Result<bool, TargetQuery.MemberAccessError> ResolveMember(object? instance, out object? value,
            Type type, int memberIdx, string[] memberArgs)
        {
            if (!IsExtVars(type, out var module))
            {
                value = null;
                return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
            }

            string variantName = memberArgs[memberIdx];
            var variant = new Lazy<object?>(Query.Driver.SourceExtendedVariants.ParseVariant(variantName));
            var variantType = Query.Driver.SourceExtendedVariants.GetVariantType(variant);
            if (variantType is null)
            {
                value = null;
                return Result<bool, TargetQuery.MemberAccessError>.Fail(
                    new TargetQuery.MemberAccessError.Custom(module.SettingsType, memberIdx,
                        $"Extended Variant '{variantName}' not found"));
            }

            value = Query.Driver.SourceExtendedVariants.GetCurrentVariantValue(variant);
            return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
        }

        public override Result<bool, TargetQuery.MemberAccessError> ResolveTargetTypes(out Type[] targetTypes,
            Type type,
            ref int memberIdx, string[] memberArgs)
        {
            if (!IsExtVars(type, out var module))
            {
                targetTypes = [];
                return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
            }

            string variantName = memberArgs[memberIdx];
            var variant = new Lazy<object?>(Query.Driver.SourceExtendedVariants.ParseVariant(variantName));
            var variantType = Query.Driver.SourceExtendedVariants.GetVariantType(variant);
            if (variantType is null)
            {
                targetTypes = null!;
                return Result<bool, TargetQuery.MemberAccessError>.Fail(
                    new TargetQuery.MemberAccessError.Custom(module.SettingsType, memberIdx,
                        $"Extended Variant '{variantName}' not found"));
            }

            targetTypes = [variantType];
            return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
        }

        public override Result<bool, TargetQuery.MemberAccessError> SetMember(object? instance, object? value,
            Type type,
            int memberIdx, string[] memberArgs, bool forceAllowCodeExecution)
        {
            if (!IsExtVars(type, out _))
            {
                return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
            }

            string variantName = memberArgs[memberIdx];
            var variant = new Lazy<object?>(Query.Driver.SourceExtendedVariants.ParseVariant(variantName));

            Query.Driver.SourceExtendedVariants.SetVariantValue(variant, value);
            return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
        }

        public override IEnumerator<CommandAutoCompleteEntry> EnumerateMemberEntries(Type type,
            TargetQuery.Variant variant,
            string queryPrefix, int memberIdx, string[] memberArgs)
        {
            if (Query.Driver.SourceExtendedVariants.GetVariantsEnum() is { } variantsEnum)
            {
                foreach (object extendedVariant in Enum.GetValues(variantsEnum))
                {
                    string typeName = string.Empty;

                    try
                    {
                        var variantType =
                            Query.Driver.SourceExtendedVariants.GetVariantType(new Lazy<object?>(extendedVariant));
                        if (variantType != null)
                        {
                            typeName = variantType.CSharpName();
                        }
                    }
                    catch
                    {
                        // ignore
                    }

                    yield return new CommandAutoCompleteEntry
                    {
                        Name = extendedVariant.ToString()!,
                        Extra = typeName,
                        Prefix = $"{queryPrefix}.",
                        IsDone = true,
                        StorageKey = $"{variant}_{Query.GetOriginalTypeFullName(type)}",
                    };
                }
            }
        }
    }
}
