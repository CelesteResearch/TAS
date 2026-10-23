using System.Linq;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Execution.LevelLoader;
using Celeste.Game.Chapters.Flow;
using Celeste.Game.Minigames.CelesteClassic.Emulation;
using Celeste.Game.Content.Dialogs;
using Celeste.Simulation.UI.Menus.Text;
using Celeste.Everest.UI.Settings;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private bool HasSourceCriticalErrorHandler() => process.CriticalErrors.CurrentHandler != null;

        private bool IsSourceDraft()
        {
            if (IsSourceRecording()) return false;
            return controller.Commands.Values.SelectMany(commands => commands)
                       .All(command => !command.Is("FileTime") && !command.Is("ChapterTime") && !command.Is("RealTime"))
                   && (!controller.Commands.TryGetValue(controller.Inputs.Count, out var endCommands)
                       || endCommands.All(command =>
                           !command.Is("MidwayFileTime") && !command.Is("MidwayChapterTime") &&
                           !command.Is("MidwayRealTime")));
        }

        private void ShowSourceAutoPauseDraftToast()
        {
            float duration = seenAutoPauseToast ? 2.0f : 8.0f;
            string text = SourceTasDialogText.ToDialogText(process, "AutoPauseToast");
            if (autoPauseDraft is not { Active: true }) autoPauseDraft = ShowSourcePopupToast(text, duration);
            else
            {
                autoPauseDraft.Text = text;
                autoPauseDraft.Timeout = duration;
            }

            seenAutoPauseToast = true;
        }

        private void RejectSourceUnsafeSceneOrOptions()
        {
            var scene = process.Frame.Scenes.CurrentScene;
            if (scene is not (OriginalLevelSceneRoot or OriginalLevelLoaderScene or LevelExitSequence
                or ClassicConsoleRuntime or LevelEnterSequence))
            {
                SourceSyncChecker.ReportUnsafeAction();
                DisableRun();
            }
            else if (scene is OriginalLevelSceneRoot level
                     && level.Scheduler.Entities.GetEntitiesTrackIfNeeded<TextMenuPresentation>().FirstOrDefault() is
                         TextMenuPresentation menu)
            {
                var item = menu.Items.FirstOrDefault();
                var dialog = ((IOriginalGameTextEnvironment)process).Text.Dialog;
                if (item is TextMenuPresentation.Header { Title: { } title }
                    && (title == dialog.Clean("OPTIONS_TITLE") || title == dialog.Clean("MENU_VARIANT_TITLE")
                                                               || dialog.Has(
                                                                   "MODOPTIONS_EXTENDEDVARIANTS_PAUSEMENU_BUTTON") &&
                                                               title == dialog
                                                                   .Clean(
                                                                       "MODOPTIONS_EXTENDEDVARIANTS_PAUSEMENU_BUTTON")
                                                                   .ToUpperInvariant())
                    || item is EverestMenuHeaderImage { Image: "menu/everest" })
                {
                    SourceSyncChecker.ReportUnsafeAction();
                    DisableRun();
                }
            }
        }
    }
}
