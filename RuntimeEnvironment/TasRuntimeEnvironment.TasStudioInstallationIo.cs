using System;
using Celeste.Mods.Tas.Tools;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment
    {
        private ISourceTasStudioInstallationIo sourceTasStudioInstallationIo;

        internal ISourceTasStudioInstallationIo SourceTasStudioInstallationIo => sourceTasStudioInstallationIo
            ?? throw new InvalidOperationException(
                "The physical Studio installation IO is not bound.");

        public void BindSourceTasStudioInstallationIo(ISourceTasStudioInstallationIo actualIo)
        {
            if (sourceTasStudioInstallationIo != null)
                throw new InvalidOperationException("The physical Studio installation IO is already bound.");
            sourceTasStudioInstallationIo = actualIo ?? throw new ArgumentNullException(nameof(actualIo));
        }
    }
}
