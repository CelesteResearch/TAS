namespace Celeste.Mods.Tas.Commands
{
    internal sealed class SourceTasRecordCountMeta : ISourceTasCommandMeta
    {
        public SourceTasRecordCountMeta()
        {
        }

        public string Insert => "RecordCount: 1";
        public bool HasArguments => false;
    }
}
