using Celeste.Simulation;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasGameInfo
    {
        private string GetAdjustedPos(RoomPlayerSession player, out string exactPos)
        {
            const string prefix = "Pos:   ";
            exactPos = $"{prefix}{player.ToSimplePositionString(StudioCommunication.GameSettings.MaxDecimals)}";
            return $"{prefix}{player.ToSimplePositionString(driver.Settings.PositionDecimals)}";
        }
    }
}
