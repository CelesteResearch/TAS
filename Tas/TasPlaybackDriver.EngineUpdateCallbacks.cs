using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        internal void PreEngineUpdate()
        {
            module.Attributes.Invoke<SourceTasPreEngineUpdateAttribute>();
            MarkTasBeforeUpdate();
        }

        internal void PostEngineUpdate() => module.Attributes.Invoke<SourceTasPostEngineUpdateAttribute>();

        internal void CollectSourceEngineUpdateCallbacks()
        {
            module.Attributes.CollectOwnMethods<SourceTasPreEngineUpdateAttribute>();
            module.Attributes.CollectOwnMethods<SourceTasPostEngineUpdateAttribute>();
        }
    }
}
