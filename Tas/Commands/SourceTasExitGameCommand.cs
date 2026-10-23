using System;
using StudioCommunication;
using Celeste.Core.Diagnostics.Tas;
using Celeste.Mods.Tas.Module;
using Celeste.Game.Execution.Processes;
using Celeste.Game.Execution.Engine;
using global::Celeste.Game.Execution;

namespace Celeste.Mods.Tas.Commands
{
    internal static class SourceTasExitGameCommand
    {
        [SourceTasCommand("ExitGame")]
        private static void ExitGame(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            var process = CelesteTasModule.Instance.Process;
            var exit = (ISourceTasForceExitEnvironment)TasRuntimeEnvironment.Get(process);
            exit.InvokeSourceGameOnExiting(EngineRuntime.Instance, EventArgs.Empty);
            exit.AbortSourceMainThread();
            Environment.Exit(0);
        }
    }
}
