using System;
using Celeste.Core.Diagnostics.Commands;
using Celeste.Mods.Tas.Information;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : ISourceCommandsObservationEnvironment
    {
        private Action sourceTasStudioBannerUpdate;
        private Action sourceTasStudioBannerRender;

        internal void InstallSourceTasStudioBannerHooks(SourceTasStudioUpdateBanner actualBanner)
        {
            sourceTasStudioBannerUpdate = actualBanner.Update;
            sourceTasStudioBannerRender = actualBanner.Render;
        }

        internal void UninstallSourceTasStudioBannerHooks()
        {
            sourceTasStudioBannerUpdate = null;
            sourceTasStudioBannerRender = null;
        }

        void ISourceCommandsObservationEnvironment.AfterSourceCommandsUpdateClosed() =>
            sourceTasStudioBannerUpdate?.Invoke();

        void ISourceCommandsObservationEnvironment.AfterSourceCommandsRendered() =>
            sourceTasStudioBannerRender?.Invoke();
    }
}
