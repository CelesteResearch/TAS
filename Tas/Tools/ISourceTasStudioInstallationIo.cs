using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Celeste.Mods.Tas.Tools
{
    /// Physical process and filesystem SDK operations absent from the netstandard API.
    public interface ISourceTasStudioInstallationIo
    {
        void Terminate(Process process);
        Task WaitForExitAsync(Process process);
        void AddProcessArgument(ProcessStartInfo startInfo, string argument);
        Process StartProcess(string executable, string[] arguments);
        void MoveFileOverwrite(string source, string destination);
        string[] GetFilesRecursively(string path);
        Process OpenInDefaultApp(string path);
    }
}
