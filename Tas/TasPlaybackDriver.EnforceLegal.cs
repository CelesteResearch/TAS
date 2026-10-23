using StudioCommunication;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasEnforceLegalProcess sourceEnforceLegal;
        public SourceTasEnforceLegalProcess EnforceLegal => sourceEnforceLegal;
        private void InitializeSourceEnforceLegal() => sourceEnforceLegal = new SourceTasEnforceLegalProcess(this);

        [SourceTasDisableRun]
        private static void DisableSourceEnforceLegal() => CelesteTasModule.Instance.Driver.EnforceLegal.DisableRun();

        [SourceTasClearInputs]
        private static void ClearSourceEnforceLegalInputs() =>
            CelesteTasModule.Instance.Driver.EnforceLegal.ClearInputs();
    }
}
