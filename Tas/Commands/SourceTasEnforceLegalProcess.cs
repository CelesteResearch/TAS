using StudioCommunication;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Commands
{
    public sealed class SourceTasEnforceLegalProcess
    {
        private readonly TasPlaybackDriver driver;
        public bool EnabledWhenRunning { get; private set; }
        public bool EnabledWhenParsing { get; private set; }

        internal SourceTasEnforceLegalProcess(TasPlaybackDriver driver) => this.driver = driver;

        // The source command registry supplies the original parsing phase, not a second flag.
        internal void EnforceLegal(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            if (driver.CommandRegistry.Parsing) EnabledWhenParsing = true;
            else EnabledWhenRunning = true;
        }

        internal void DisableRun() => EnabledWhenRunning = false;
        internal void ClearInputs() => EnabledWhenParsing = false;
    }
}
