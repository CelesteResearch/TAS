using StudioCommunication;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Commands
{
    internal static class SourceTasDummyCommands
    {
        [SourceTasCommand("Author:", CalcChecksum = false)]
        private static void Author(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
        }

        [SourceTasCommand("FrameCount:", CalcChecksum = false)]
        private static void FrameCount(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
        }

        [SourceTasCommand("TotalRecordCount:", CalcChecksum = false)]
        private static void TotalRecordCount(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
        }
    }
}
