using Celeste.Everest.Runtime;
using Celeste.Everest.Runtime.Startup;
using Celeste.Game.Menus.Overworld;
using Celeste.Game.Presentation.Environment;
using Celeste.Scene.Scenes;

namespace Celeste.Mods.Tas
{
    internal sealed partial class SourceTasSyncChecker
    {
        private void InitializeSourceStartupPatches()
        {
            if (!Active || process.Flags.IsHeadless) return;
            TasRuntimeEnvironment.Get(process).InstallSourceTasGameLoaderHooks(SkipSourceIntro, GetSourceOverworld);
            TasRuntimeEnvironment.Get(process).InstallSourceTasMenuStartupHooks();
        }

        private void UnloadSourceStartupPatches()
        {
            TasRuntimeEnvironment.Get(process).UninstallSourceTasGameLoaderHooks();
            TasRuntimeEnvironment.Get(process).UninstallSourceTasMenuStartupHooks();
        }

        private void SkipSourceIntro(EverestGameLoaderScene loader) => loader.SkipIntro();

        private SourceSceneState GetSourceOverworld(EverestGameLoaderScene loader, HiresSnowState snow)
            => ((IOriginalOverworldLoaderEnvironment)process).OverworldLoaders.CreateOverworldLoader(
                OverworldStartMode.Titlescreen, snow);
    }
}
