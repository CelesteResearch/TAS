using Celeste.Mods.Tas.Reflection;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment
    {
        internal void PublishSourceTasGameActualTypeQualifications()
        {
            SourceTasGameActualTypeBindings.Register(process.SceneCatalog, process.SourceMembers);
        }
    }
}
