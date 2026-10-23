using System;
using System.Collections.Generic;
using System.Linq;
using StudioCommunication;
using TargetQuery = Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess;

namespace Celeste.Mods.Tas.Query.Handlers
{
    internal class SourceEverestModuleSettingsQueryHandler : TargetQuery.Handler
    {
        public SourceEverestModuleSettingsQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
        }

        public override bool CanResolveInstances(Type type) =>
            typeof(global::Celeste.Everest.Modules.Settings.EverestModuleSettings).IsAssignableFrom(type);

        public override (HashSet<Type> Types, string[] MemberArgs)? ResolveBaseTypes(string[] queryArgs)
        {
            if (Query.SourceModules.FirstOrDefault(mod => mod.SettingsType != null && mod.Metadata.Name == queryArgs[0])
                is
                { } module)
            {
                return ([module.SettingsType], queryArgs[1..]);
            }

            return null;
        }

        public override object[] ResolveInstances(Type type)
        {
            return Query.SourceModules.FirstOrDefault(mod => mod.SettingsType == type) is { } module
                ? [module._Settings]
                : [];
        }

        public override IEnumerator<CommandAutoCompleteEntry> ProvideGlobalEntries(string[] queryArgs,
            string queryPrefix,
            TargetQuery.Variant variant)
        {
            if (queryArgs.Length > 1 || variant is not (TargetQuery.Variant.Get or TargetQuery.Variant.Set))
            {
                yield break;
            }

            foreach (var mod in Query.SourceModules)
            {
                if (mod.SettingsType != null && Query.IsTypeViable(mod.SettingsType, variant))
                {
                    yield return new CommandAutoCompleteEntry
                    {
                        Name = $"{mod.Metadata.Name}.",
                        Extra = "Mod Setting",
                        IsDone = false,
                        Suggestion = mod.Metadata.Name == "ExtendedVariantMode",
                        StorageKey = $"{variant}_{GetNamespace(Query.GetOriginalTypeFullName(mod.SettingsType))}",
                        StorageName = Query.GetOriginalTypeFullName(mod.SettingsType),
                    };
                }
            }
        }

        private static string GetNamespace(string typeName)
        {
            int nested = typeName.IndexOf('+');
            if (nested >= 0) typeName = typeName[..nested];
            int separator = typeName.LastIndexOf('.');
            return separator < 0 ? string.Empty : typeName[..separator];
        }

        public override bool IsMemberSuggested(SourceQueryMemberIdentity member, TargetQuery.Variant variant)
        {
            return variant == TargetQuery.Variant.Get && member.DeclaringType != null &&
                   Query.GetOriginalTypeFullName(member.DeclaringType) == "Celeste.Mod.Core.CoreModuleSettings" &&
                   member.Name == "ShowModOptionsInGame";
        }
    }
}
