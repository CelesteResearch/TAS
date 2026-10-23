using System;
using System.Linq;
using Celeste.Everest.Modules.Runtime;

namespace Celeste.Mods.Tas.Query.Handlers
{
    internal sealed class SourceEverestModuleQueryHandler : SourceTasTargetQueryProcess.Handler
    {
        public SourceEverestModuleQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
        }

        public override bool CanResolveInstances(Type type) => typeof(SourceEverestModule).IsAssignableFrom(type);

        public override object[] ResolveInstances(Type type) =>
            Query.SourceModules.Where(type.IsInstanceOfType).Cast<object>().ToArray();
    }
}
