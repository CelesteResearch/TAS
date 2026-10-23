using System;
using Celeste.Game.Runtime;
using Celeste.Mods.Tas.Reflection;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : ISourceTasHookModuleMetadataEnvironment
    {
        private readonly GameApplication process;

        internal TasRuntimeEnvironment(GameApplication process)
        {
            this.process = process;
            process.RegisterRuntimeExtension(this);
        }

        public static TasRuntimeEnvironment Get(GameApplication process) =>
            process.GetRuntimeExtension<TasRuntimeEnvironment>() ??
            throw new InvalidOperationException("The TAS runtime extension is not loaded.");

        internal static TasRuntimeEnvironment GetOrCreate(GameApplication process) =>
            process.GetRuntimeExtension<TasRuntimeEnvironment>() ?? new TasRuntimeEnvironment(process);

        internal void DetachModule()
        {
            sourceTasDriver.DetachFromHost();
            process.UnregisterRuntimeExtension(this);
            sourceTasDriver = null;
            sourceTasModule = null;
        }

        global::Celeste.Everest.Runtime.Assemblies.SourceRuntimeModuleMetadata
            ISourceTasHookModuleMetadataEnvironment.HookModuleMetadata => process.RuntimeModuleMetadata;
    }
}
