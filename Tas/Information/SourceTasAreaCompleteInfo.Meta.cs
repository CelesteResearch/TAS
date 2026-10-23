using StudioCommunication;
using Celeste.Mods.Tas.Commands;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasAreaCompleteInfo
    {
        internal sealed class Meta : ISourceTasCommandMeta
        {
            public string Insert => $"CompleteInfo{CommandInfo.Separator}[0;A{CommandInfo.Separator}1]";
            public bool HasArguments => true;
        }
    }
}
