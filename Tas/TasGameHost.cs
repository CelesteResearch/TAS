using System;
using Celeste.Core.Scheduling;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasGameHost
    {
        private readonly ISourceEngineContext engine;

        public TasGameHost(ISourceEngineContext engine)
        {
            this.engine = engine ?? throw new ArgumentNullException(nameof(engine));
        }

        public bool Loading() => IsLoadingScene(engine);
    }
}
