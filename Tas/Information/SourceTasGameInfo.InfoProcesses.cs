#nullable enable
namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasGameInfo
    {
        private SourceTasInfoCustom? customInfoProcess;
        internal SourceTasInfoCustom CustomInfoProcess => customInfoProcess ??= new SourceTasInfoCustom(driver);
        private SourceTasInfoWatchEntity? watchEntities;

        internal SourceTasInfoWatchEntity WatchEntities =>
            watchEntities ??= new SourceTasInfoWatchEntity(process, driver);
    }
}
