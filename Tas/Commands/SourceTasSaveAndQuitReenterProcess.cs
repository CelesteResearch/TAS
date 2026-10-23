using System.Linq;
using StudioCommunication;
using Celeste.Mods.Tas.Module;
using Celeste.Everest.Modules.Runtime;
using Celeste.Game.Menus.Overworld;
using Celeste.Game.Menus.Files;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Execution.Startup;
using global::Celeste.Game.Execution;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed class SourceTasSaveAndQuitReenterProcess
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private bool justPressedSnQ;

        internal SourceTasSaveAndQuitReenterProcess(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        private int ActiveFileSlot => driver.SourceLibTasHelper.Exporting
            ? 0
            : process.Frame.Scenes.CurrentScene is OverworldSequence { Current: FileSelectSequence select }
                ? select.SlotIndex
                : process.SaveContext.CurrentSave?.FileSlot ?? -1;

        [SourceTasLoad]
        private static void Load() =>
            TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process).InstallSourceTasSaveQuitHooks();

        [SourceTasDisableRun]
        private static void DisableRun() =>
            CelesteTasModule.Instance.Driver.SourceSaveAndQuitReenter.justPressedSnQ = false;

        internal void BeforeSaveAndQuitPressed() => justPressedSnQ = true;
        internal void BeforeLevelUpdate() => justPressedSnQ = false;

        [SourceTasCommand("SaveAndQuitReenter",
            ExecuteTiming = SourceTasExecuteTiming.Parse | SourceTasExecuteTiming.Runtime)]
        internal void Execute(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            var controller = driver.Controller;
            var command =
                controller.Commands[
                    driver.CommandRegistry.Parsing ? controller.CurrentParsingFrame : controller.CurrentFrameInTas][^1];

            if (driver.CommandRegistry.Parsing)
            {
                int slot = ActiveFileSlot;

                bool safe = driver.SourceSafeCommand.DisallowUnsafeInputParsing;
                if (safe)
                {
                    controller.ReadLine("Unsafe", filePath, fileLine, studioLine);
                }

                // Ensure ~DEBUG~ button is available
                if (slot == -1 && GameRuntime.PlayMode != GameRuntime.PlayModes.Debug)
                {
                    controller.ReadLine("Set,Celeste.PlayMode,Debug", filePath, fileLine, studioLine);
                }

                driver.SourceLibTasHelper.AddInputFrame("58");
                controller.AddFrames("31", filePath, fileLine, studioLine, parentCommand: command);
                controller.AddFrames("14", filePath, fileLine, studioLine, parentCommand: command);
                if (slot == -1)
                {
                    // Load debug slot
                    controller.AddFrames("1,D", filePath, fileLine, studioLine, parentCommand: command);
                    // The Randomizer adds a new menu entry between CLIMB and ~DEBUG~
                    if (((ISourceEverestModulesProcessEnvironment)process).ModulesProcess.Modules.Any(module =>
                            module.Metadata?.Name == "Randomizer"))
                    {
                        controller.AddFrames("1,F,180", filePath, fileLine, studioLine, parentCommand: command);
                        controller.AddFrames("1", filePath, fileLine, studioLine, parentCommand: command);
                    }

                    controller.AddFrames("1,O", filePath, fileLine, studioLine, parentCommand: command);
                    controller.AddFrames("33", filePath, fileLine, studioLine, parentCommand: command);
                }
                else
                {
                    // Get to the save files screen
                    controller.AddFrames("1,O", filePath, fileLine, studioLine, parentCommand: command);
                    controller.AddFrames("56", filePath, fileLine, studioLine, parentCommand: command);
                    // Alternate 1,D and 1,F,180 to select the slot
                    for (int i = 0; i < slot; i++)
                    {
                        controller.AddFrames(i % 2 == 0 ? "1,D" : "1,F,180", filePath, fileLine, studioLine,
                            parentCommand: command);
                    }

                    // Load the selected save file
                    controller.AddFrames("1,O", filePath, fileLine, studioLine, parentCommand: command);
                    controller.AddFrames("14", filePath, fileLine, studioLine, parentCommand: command);
                    controller.AddFrames("1,O", filePath, fileLine, studioLine, parentCommand: command);
                    controller.AddFrames("1", filePath, fileLine, studioLine, parentCommand: command);
                    driver.SourceLibTasHelper.AddInputFrame("32");
                }

                // Restore settings
                if (slot == -1 && GameRuntime.PlayMode != GameRuntime.PlayModes.Debug)
                {
                    controller.ReadLine($"Set,Celeste.PlayMode,{GameRuntime.PlayMode}", filePath, fileLine,
                        studioLine);
                    controller.ReadLine("Set,Engine.Commands.Enabled,false", filePath, fileLine, studioLine);
                }

                if (safe)
                {
                    controller.ReadLine("Safe", filePath, fileLine, studioLine);
                }
            }
            else
            {
                if (!justPressedSnQ)
                {
                    driver.AbortSourceTas(
                        "SaveAndQuitReenter must be exactly after pressing the \"Save & Quit\" button");
                    return;
                }

                if (process.Frame.Scenes.CurrentScene is not OriginalLevelSceneRoot)
                {
                    driver.AbortSourceTas("SaveAndQuitReenter can't be used outside levels");
                    return;
                }

                if (driver.SourceMetadataCommands.RealTimeInfo != null)
                {
                    // Always asume best case scenario for a Save & Quit (1st real slot), to keep a consistent time
                    const int bestFrameCount = 58 + 31 + 14 + 1 + 56 + 1 + 14 + 1 + 1 + 32;

                    driver.SourceMetadataCommands.RealTimeInfo = driver.SourceMetadataCommands.RealTimeInfo.Value with
                    {
                        FrameCount = driver.SourceMetadataCommands.RealTimeInfo.Value.FrameCount + bestFrameCount
                    };
                }

                // Ensure the inputs are for the current save slot
                controller.RefreshInputs(forceRefresh: true);
            }
        }
    }
}
