using System.Globalization;
using System.Linq;
using StudioCommunication;
using Celeste.Framework.Diagnostics.Commands;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed partial class SourceTasConsoleProcess
    {
        internal void Execute(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            if (commandLine.Arguments.Length == 0)
            {
                AbortTas("Need to specify arguments for \"Console\" command");
                return;
            }

            string commandName = commandLine.Arguments[0].ToLower(CultureInfo.InvariantCulture);
            string[] commandArgs = commandLine.Arguments[1..];
            var match = LoadCommandRegex.Match(commandName);
            if (match.Success)
            {
                commandName = match.Groups[1].Value;
                int slot = int.TryParse(match.Groups[2].Value, out int parsedSlot) ? parsedSlot - 1 : -1;
                LoadCommand(commandName, commandArgs, slot);
            }
            else
            {
                var console = ((ISourceCommandsProcessEnvironment)process).Commands;
                console.InsertSourceCommandHistory(0, commandLine.OriginalText[commandLine.Regions[1].StartCol..]);
                console.ExecuteCommand(commandName, commandArgs);
                console.RemoveSourceCommandHistoryAt(0);
            }
        }

        private void AbortTas(string text) => driver.AbortSourceTas(text);
    }
}
