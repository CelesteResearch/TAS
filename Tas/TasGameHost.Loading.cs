using Celeste.Core.Scheduling;
using Celeste.Core.Reflection.Members;
using Celeste.Game.Save.Runtime;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Execution.LevelLoader;
using Celeste.Game.Menus.Loading;
using Celeste.Game.Menus.Overworld;
using Celeste.Game.Menus.Files;
using Celeste.Game.Menus.Chapters;
using Celeste.Game.Menus.Title;
using Celeste.Game.Chapters.Flow;
using Celeste.Game.Vignettes;
using Celeste.Game.Minigames.CelesteClassic.Emulation;
using Celeste.Everest.Modules.Persistence.GameSaves;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasGameHost
    {
        // The real-time policy differs from playback's general loading policy.
        internal bool IsActuallyLoadingScene(SourceMemberCatalog members)
        {
            object scene = engine.Frame.Scenes.CurrentScene;
            switch (scene)
            {
                case OriginalLevelSceneRoot level:
                    return level.IsAutoSaving() && level.Session.Level == "end-cinematic" ||
                           level.Paused && ((IOriginalUserIOEnvironment)engine).UserIO.Saving && !level.IsAutoSaving();
                case SummitVignetteScene summit:
                    return !summit.IsReady;
                case OverworldSequence overworld:
                    return overworld.Next is ChapterSelectSequence &&
                           ((IOriginalUserIOEnvironment)engine).UserIO.Saving ||
                           overworld.Next is MainMenuSequence && (((IOriginalUserIOEnvironment)engine).UserIO.Saving ||
                                                                  ((IEverestSaveSettingsEnvironment)engine)
                                                                  .SaveSettingsProcess.SavingSettings);
                case LevelExitSequence exit:
                    return exit.Mode == LevelExitMode.Completed && !exit.IsCompleteLoaded ||
                           ((IOriginalUserIOEnvironment)engine).UserIO.Saving;
                default:
                    if (scene is OriginalLevelLoaderScene || scene is GameLoaderScene) return true;
                    var type = scene.GetType();
                    string name = members.ContainsSourceType(type) ? members.GetOriginalTypeName(type) : type.Name;
                    return name == "LevelExitToLobby" && ((IOriginalUserIOEnvironment)engine).UserIO.Saving;
            }
        }

        public static bool IsLoadingScene(ISourceEngineContext engine)
        {
            object scene = engine.Frame.Scenes.CurrentScene;
            switch (scene)
            {
                case OriginalLevelSceneRoot level:
                    return level.IsAutoSaving() && level.Session.Level == "end-cinematic" ||
                           level.Paused && ((IOriginalUserIOEnvironment)engine).UserIO.Saving && !level.IsAutoSaving();
                case SummitVignetteScene summit:
                    return !summit.IsReady;
                case OverworldSequence overworld:
                    return overworld.Current is FileSelectSequence file && file.SlotIndex >= 0 &&
                           file.Slots[file.SlotIndex].StartingGame ||
                           overworld.Next is ChapterSelectSequence &&
                           ((IOriginalUserIOEnvironment)engine).UserIO.Saving ||
                           overworld.Next is MainMenuSequence && (((IOriginalUserIOEnvironment)engine).UserIO.Saving ||
                                                                  ((IEverestSaveSettingsEnvironment)engine)
                                                                  .SaveSettingsProcess.SavingSettings);
                case ClassicConsoleRuntime emulator:
                    return emulator.IsGameUninitialized;
                default:
                    return scene is LevelExitSequence || scene is OriginalLevelLoaderScene ||
                           scene is GameLoaderScene ||
                           scene.GetType().Name == "LevelExitToLobby";
            }
        }
    }
}
