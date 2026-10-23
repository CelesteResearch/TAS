using System;
using System.Collections.Generic;
using System.Linq;
using StudioCommunication;
using TargetQuery = Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess;

namespace Celeste.Mods.Tas.Query.Handlers
{
    internal class SourceEverestModuleSessionQueryHandler : TargetQuery.Handler
    {
        public SourceEverestModuleSessionQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
        }

        public override bool CanResolveInstances(Type type) =>
            typeof(global::Celeste.Everest.Modules.Persistence.EverestModuleSession).IsAssignableFrom(type);

        public override object[] ResolveInstances(Type type)
        {
            return Query.SourceModules.FirstOrDefault(mod => mod.SessionType == type) is { } module
                ? [module._Session]
                : [];
        }
    }
}
