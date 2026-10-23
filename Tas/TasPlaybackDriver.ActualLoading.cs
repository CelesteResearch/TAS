namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        internal bool IsActuallyLoading()
        {
            if (Controller.Current is { } current && current.ParentCommand is { } command &&
                command.Is("SaveAndQuitReenter"))
            {
                // SaveAndQuitReenter adds the optimal save-and-quit real-time itself.
                return true;
            }

            return gameHost.IsActuallyLoadingScene(process.SourceMembers);
        }
    }
}
