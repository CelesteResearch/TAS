using Celeste.Game.Profiles;
using Celeste.Mods.Tas.Reflection;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment
    {
        internal void PublishSourceTasOptimizationActualMethods()
        {
            bool installed = process.SourceProfile == GameProfile.InstalledEverest;
            SourceTasOptimizationActualMethodBindings.Register(process.Reflection.SourceMethods, process.SourceMembers,
                installed, installed ? process.InstalledLevelGarbageCollection : null);
        }
    }
}
