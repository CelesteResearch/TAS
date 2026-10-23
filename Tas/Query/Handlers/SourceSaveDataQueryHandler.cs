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
    internal class SourceSaveDataQueryHandler : TargetQuery.Handler
    {
        public SourceSaveDataQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
        }

        public override bool CanResolveInstances(Type type) => type == typeof(SaveData);

        public override (HashSet<Type> Types, string[] MemberArgs)? ResolveBaseTypes(string[] queryArgs)
        {
            // Vanilla settings don't need a prefix
            if (Query.Reflection.GetAllFieldInfos(typeof(SaveData))
                .Any(f => f.Name == queryArgs[0]))
            {
                return ([typeof(SaveData)], queryArgs);
            }

            return null;
        }

        public override object[] ResolveInstances(Type type)
        {
            return Query.CurrentSave != null ? [Query.CurrentSave] : [];
        }

        public override Result<bool, TargetQuery.MemberAccessError> SetMember(object? instance, object? value,
            Type type,
            int memberIdx, string[] memberArgs, bool forceAllowCodeExecution)
        {
            if (instance is not SaveData saveData)
            {
                return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
            }

            switch (memberArgs[memberIdx])
            {
                case nameof(SaveData.VariantMode):
                    saveData.VariantMode = (bool)value!;
                    saveData.AssistMode = false;
                    if (!saveData.VariantMode)
                    {
                        Query.Driver.SourceAssistQuery.ApplyAssists(null, Assists.Default);
                    }

                    break;

                case nameof(SaveData.AssistMode):
                    saveData.AssistMode = (bool)value!;
                    saveData.VariantMode = false;
                    if (!saveData.AssistMode)
                    {
                        Query.Driver.SourceAssistQuery.ApplyAssists(null, Assists.Default);
                    }

                    break;
            }

            return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
        }

        public override IEnumerator<CommandAutoCompleteEntry> ProvideGlobalEntries(string[] queryArgs,
            string queryPrefix,
            TargetQuery.Variant variant)
        {
            if (queryArgs.Length > 1 || variant is not (TargetQuery.Variant.Get or TargetQuery.Variant.Set))
            {
                yield break;
            }

            // Manually selected to filter out useless entries
            var vanillaSaveData =
                ((string[])["CheatMode", "AssistMode", "VariantMode", "UnlockedAreas", "RevealedChapter9", "DebugMode"])
                .Select(e => Query.GetOriginalField(typeof(SaveData), e)!);
            foreach (var f in vanillaSaveData)
            {
                yield return new CommandAutoCompleteEntry
                {
                    Name = f.Name,
                    Extra = $"{f.ValueType.CSharpName()} (Save Data)",
                    IsDone = true,
                    StorageKey = $"{variant}_{Query.GetOriginalTypeFullName(typeof(SaveData))}"
                };
            }
        }
    }
}
