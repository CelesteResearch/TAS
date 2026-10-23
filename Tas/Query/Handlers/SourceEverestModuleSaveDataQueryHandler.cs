using System;
using System.Collections.Generic;
using System.Linq;
using StudioCommunication;
using TargetQuery = Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess;

namespace Celeste.Mods.Tas.Query.Handlers
{
    internal class SourceEverestModuleSaveDataQueryHandler : TargetQuery.Handler
    {
        public SourceEverestModuleSaveDataQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
        }

        public override bool CanResolveInstances(Type type) =>
            typeof(global::Celeste.Everest.Modules.Persistence.EverestModuleSaveData).IsAssignableFrom(type);

        public override object[] ResolveInstances(Type type)
        {
            return Query.SourceModules.FirstOrDefault(mod => mod.SaveDataType == type) is { } module
                ? [module._SaveData]
                : [];
        }
    }
}
