using Celeste.Everest.Runtime.Assemblies;

namespace Celeste.Mods.Tas.Reflection
{
    public interface ISourceTasHookModuleMetadataEnvironment
    {
        SourceRuntimeModuleMetadata HookModuleMetadata { get; }
    }
}
