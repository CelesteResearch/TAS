using System.Reflection;
using Celeste.Core.Scheduling;
using Celeste.Everest.Modules.Runtime;
using Celeste.Game.Runtime;

namespace Celeste.Mods.Tas
{
    public sealed class SourceTasModuleMetadataProvider : ISourceModuleMetadataProvider
    {
        public void Publish(ISourceEngineContext engine, Assembly assembly)
        {
            var process = (GameApplication)engine;
            var environment = process.GetRuntimeExtension<TasRuntimeEnvironment>() ??
                              new TasRuntimeEnvironment(process);
            environment.PublishSourceTasReflectionMetadata();
            environment.PublishSourceTasGameActualMethodTargets();
            environment.PrepareLifecycleBindings();
        }
    }
}
