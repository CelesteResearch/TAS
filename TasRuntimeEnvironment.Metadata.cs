using Celeste.Mods.Tas.Reflection.Metadata;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment
    {
        private bool sourceTasReflectionMetadataPublished;

        internal void PublishSourceTasReflectionMetadata()
        {
            if (sourceTasReflectionMetadataPublished) return;
            PublishSourceTasExternalBaseClosure();
            global::Celeste.Mods.Tas.Reflection.SourceTasTrackerBaseMetadata.Publish(process.SourceMembers);
            SourceTasMemberMetadata.PublishTas(process.SourceMembers);
            process.SourceMembers.RegisterSourceType(typeof(global::Celeste.Mods.Tas.Lua.LuaHelpers),
                "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                "TAS.EverestInterop.Lua.LuaHelpers");
            process.SourceMembers.RegisterSourceType(typeof(global::Celeste.Mods.Tas.Lua.LuaValueHolder<>),
                "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                "TAS.EverestInterop.Lua.LuaHelpers+ValueHolder`1");
            SourceTasMethodMetadata.PublishTas(process.Reflection.SourceMethods);
            global::Celeste.Mods.Tas.Reflection.SourceTasHitboxLifecycleActualMethodBindings
                .RegisterModCompatibility(process);
            global::Celeste.Mods.Tas.Reflection.SourceTasLogicalTypeFacts.Register(process.SceneCatalog,
                process.SourceMembers,
                process.Reflection.SourceMethods);
            global::Celeste.Mods.Tas.Reflection.SourceTasSceneActorActualTypeBindings.Register(process.SceneCatalog,
                process.SourceMembers);
            global::Celeste.Mods.Tas.Reflection.SourceTasModuleAndCommandProviderActualTypeBindings.Register(
                process.SourceMembers, process.Reflection.SourceMethods);
            global::Celeste.Mods.Tas.Reflection.SourceTasInstalledResourceMetadata.Register(process.Reflection
                .SourceResources);
            sourceTasReflectionMetadataPublished = true;
        }
    }
}
