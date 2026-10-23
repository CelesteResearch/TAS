using Celeste.Game.Execution.Areas;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed partial class SourceTasConsoleProcess
    {
        private void ClearSourceCollabLobby(OriginalAreaKey areaKey)
        {
            string sid = process.AreaKeys.GetSID(areaKey);
            if (driver.CollabUtils2.Lobby.IsCollabLobby is { } isLobby && isLobby(sid))
            {
                driver.CollabUtils2.Lobby.ClearLobbyMap(sid);
                driver.SourceMetadataCommands.LoadedLobbySID = sid;
            }
        }
    }
}
