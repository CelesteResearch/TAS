using StudioCommunication;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed class SourceTasSafeProcess
    {
        private readonly TasPlaybackDriver driver;

        internal SourceTasSafeProcess(TasPlaybackDriver driver) => this.driver = driver;

        // stop tas when out of Level/LevelLoader/LevelExit/Pico8/LevelEnter
        // stop tas when entering Options/ModOptions UI
        public bool DisallowUnsafeInput { get; set; } = true;
        public bool DisallowUnsafeInputParsing { get; private set; } = true;

        [SourceTasCommand("Safe", ExecuteTiming = SourceTasExecuteTiming.Parse | SourceTasExecuteTiming.Runtime)]
        internal void Safe(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            if (driver.CommandRegistry.Parsing)
            {
                DisallowUnsafeInputParsing = true;
            }
            else
            {
                DisallowUnsafeInput = true;
            }
        }

        [SourceTasCommand("Unsafe", ExecuteTiming = SourceTasExecuteTiming.Parse | SourceTasExecuteTiming.Runtime)]
        internal void Unsafe(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            if (driver.CommandRegistry.Parsing)
            {
                DisallowUnsafeInputParsing = false;
            }
            else
            {
                DisallowUnsafeInput = false;
            }
        }

        [SourceTasDisableRun]
        private static void SourceDisableRun() => CelesteTasModule.Instance.Driver.SourceSafeCommand.DisableRun();

        internal void DisableRun()
        {
            DisallowUnsafeInput = true;
        }

        [SourceTasParseFileEnd]
        [SourceTasClearInputs]
        private static void SourceClear() => CelesteTasModule.Instance.Driver.SourceSafeCommand.Clear();

        internal void Clear()
        {
            DisallowUnsafeInputParsing = true;
        }
    }
}
