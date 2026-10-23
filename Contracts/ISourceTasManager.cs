namespace Celeste.Core.Diagnostics.Tas
{
    public interface ISourceTasManager
    {
        bool Running { get; }
        SourceTasManagerState CurrState { get; set; }
        SourceTasManagerState NextState { get; set; }
    }
}
