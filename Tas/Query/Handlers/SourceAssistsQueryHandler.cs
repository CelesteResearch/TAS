using System;
using System.Collections.Generic;
using System.Linq;
using Celeste.Core.Reflection.Members;
using StudioCommunication;
using StudioCommunication.Util;
using Assists = Celeste.Game.Save.Data.FileAssists;
using SaveData = Celeste.Game.Save.Data.OriginalSaveDataState;
using TargetQuery = Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess;

namespace Celeste.Mods.Tas.Query.Handlers
{
    internal class SourceAssistsQueryHandler : TargetQuery.Handler
    {
        public SourceAssistsQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
        }

        public override bool CanResolveInstances(Type type) => type == typeof(Assists);

        public override (HashSet<Type> Types, string[] MemberArgs)? ResolveBaseTypes(string[] queryArgs)
        {
            // Vanilla settings don't need a prefix
            if (Query.Reflection.GetAllFieldInfos(typeof(Assists))
                .Any(f => f.Name == queryArgs[0]))
            {
                return ([typeof(SaveData)], [nameof(SaveData.Assists), .. queryArgs]);
            }

            return null;
        }

        public override object[] ResolveInstances(Type type)
        {
            return Query.CurrentSave != null ? [Query.CurrentSave.Assists] : [];
        }

        public override Result<bool, TargetQuery.MemberAccessError> SetMember(object? instance, object? value,
            Type type,
            int memberIdx, string[] memberArgs, bool forceAllowCodeExecution)
        {
            switch (instance)
            {
                case SaveData saveData when memberArgs[memberIdx] == nameof(SaveData.Assists):
                    var oldAssists = saveData.Assists;
                    saveData.Assists = (Assists)value!;
                    Query.Driver.SourceAssistQuery.ApplyAssists(oldAssists, saveData.Assists);
                    return Result<bool, TargetQuery.MemberAccessError>.Ok(true);

                case Assists when memberArgs[memberIdx] == nameof(Assists.Invincible) && Query.Driver.Running &&
                                  Query.Driver.Settings.BetterInvincible:
                    Query.Driver.SourceBetterInvincible.Invincible = (bool)value!;
                    return Result<bool, TargetQuery.MemberAccessError>.Ok(true);

                default:
                    return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
            }
        }

        public override IEnumerator<CommandAutoCompleteEntry> ProvideGlobalEntries(string[] queryArgs,
            string queryPrefix,
            TargetQuery.Variant variant)
        {
            if (queryArgs.Length > 1 || variant is not (TargetQuery.Variant.Get or TargetQuery.Variant.Set))
            {
                yield break;
            }

            foreach (var f in Query.GetAllOriginalFields(typeof(Assists), QueryReflection.InstanceAnyVisibility))
            {
                yield return new CommandAutoCompleteEntry
                {
                    Name = f.Name,
                    Extra = $"{f.ValueType.CSharpName()} (Assists)",
                    Suggestion = variant == TargetQuery.Variant.Set && f.Name is nameof(Assists.Invincible),
                    IsDone = true,
                    StorageKey = $"{variant}_{Query.GetOriginalTypeFullName(typeof(Assists))}"
                };
            }
        }
    }
}
