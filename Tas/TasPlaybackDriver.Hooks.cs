using Celeste.Mods.Tas.Reflection;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasHookHelperProcess sourceHooks;
        internal SourceTasHookHelperProcess SourceHooks => sourceHooks;

        private void InitializeSourceHooks()
        {
            SourceTasDetourCreatorFrameOwners.Register(process);
            sourceHooks = new SourceTasHookHelperProcess(process);
        }
    }
}
