using Celeste.Core.Display;
using Celeste.Everest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using StudioCommunication;
using Celeste.Core.Components;
using Celeste.Core.Scheduling;
using Celeste.Core.Mathematics.Easing;
using Celeste.Core.Rendering;
using Celeste.Scene.Entities;
using Celeste.Scene.Lifecycle;
using Celeste.Scene.Tracking;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Celeste.Graphics.Geometry.ThreeD.Camera;
using Celeste.Simulation.UI.Menus.Text;
using Celeste.Simulation.Runtime.Session;
using Celeste.Game.Content.Dialogs;
using Celeste.Localization.Lookup;
using Celeste.Game.Audio.Runtime;
using Celeste.Game.Audio;
using Celeste.Game.Audio.Mixing;
using Celeste.Game.Chapters.Flow;
using Celeste.Game.Menus.Overworld;
using Celeste.Game.Menus.Chapters;
using Celeste.Game.Menus.Title;
using Celeste.Game.Presentation.Environment;
using Celeste.Game.Presentation.Hud;
using Celeste.Game.Presentation.Mountain.Rendering;
using Celeste.Game.Execution.Areas;
using Celeste.Game.Execution.Processes;
using Celeste.Game.Execution.Threading;
using Celeste.Game.Save.Runtime;
using Celeste.Everest.Content.Registry;
using Celeste.Everest.Content.Assets;
using Celeste.Everest.Content.Types.Maps;
using Celeste.Everest.Modules.Runtime;
using Celeste.Everest.Modules.Updating;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Everest.UI.Settings;
using Celeste.Everest.UI.Dependencies;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Input;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed partial class SourceTasRequireDependencyProcess
    {
        private readonly TasPlaybackDriver driver;
        internal SourceTasRequireDependencyProcess(TasPlaybackDriver driver) => this.driver = driver;
        internal static Type SourceCommandMetaType => typeof(Meta);

        private static SourceTasRequireDependencyProcess Owner =>
            CelesteTasModule.Instance.Driver.SourceRequireDependencyCommand;

        internal GameApplication Process => driver.Process;
        private OriginalGameAudioSession Audio => ((IOriginalGameplayProcess)Process).AudioSession;
        private DialogCatalog Dialog => ((IOriginalGameTextEnvironment)Process).Text.Dialog;

        private List<SourceEverestModule> Modules =>
            ((ISourceEverestModulesProcessEnvironment)Process).ModulesProcess.Modules;

        private EverestContentRegistry Content => ((IEverestContentRegistryEnvironment)Process).SourceContentRegistry;
        private string ToDialogText(string key) => Dialog.Clean(key);


        public const string CommandName = "RequireDependency";

        private class Meta : ISourceTasCommandMeta
        {
            public string Insert => $"{CommandName}{CommandInfo.Separator}[0;Name]";
            public bool HasArguments => true;

            public IEnumerator<CommandAutoCompleteEntry> GetAutoCompleteEntries(string[] args, string filePath,
                int fileLine)
            {
                if (args.Length == 1)
                {
                    string bestGuess = null;
                    // basically same as the code in GameData.GetRequireDependency()
                    if (Owner.Process.Frame.Scenes.CurrentScene is Level level
                        && Owner.Content.TryGet<AssetTypeMap>(
                            "Maps/" + ((IOriginalAreaKeyEnvironment)Owner.Process).AreaKeys.GetSID(
                                Owner.Process.AreaKeys.GetSessionArea(level.Session)),
                            out EverestContentAsset mapModAsset)
                        && mapModAsset.Source != null
                        && Owner.Modules.FirstOrDefault(module => module.Metadata.Name == mapModAsset.Source.Name) is
                            { } moduleOfThisLevel)
                    {
                        bestGuess = moduleOfThisLevel.Metadata.Name;
                        yield return CreateFullEntry(moduleOfThisLevel);
                    }

                    foreach (var module in Owner.Modules)
                    {
                        if (bestGuess == module.Metadata.Name)
                        {
                            continue;
                        }

                        yield return CreateFullEntry(module);
                    }
                }
                else if (args.Length == 2 && Owner.Modules.FirstOrDefault(mod => mod.Metadata.Name == args[0]) is
                             { } module)
                {
                    yield return new CommandAutoCompleteEntry
                    {
                        Name = module.Metadata.Version.ToString(3),
                        HasNext = false
                    };
                }

                static CommandAutoCompleteEntry CreateFullEntry(SourceEverestModule module)
                {
                    return new CommandAutoCompleteEntry
                    {
                        Name = module.Metadata.Name,
                        Extra = "v" + module.Metadata.Version.ToString(3),
                        Suffix = CommandInfo.Separator + module.Metadata.Version.ToString(3),
                        HasNext = false,
                    };
                }
            }
        }

        private readonly Dictionary<string, Version?> missingDependencies = new();
        private readonly Dictionary<string, SourceTasSourceLocation> missingDependencySources = new();

        [SourceTasParseFileEnd]
        private static void SourceParseFileEnd() => Owner.ParseFileEnd();

        internal void ParseFileEnd()
        {
            if (missingDependencies.Count == 0)
            {
                return;
            }

            switch (Process.Frame.Scenes.CurrentScene)
            {
                case Level level:
                    ShowTextMenuPresentation(level);
                    break;
                case OverworldSequence overworld:
                    var oui = new SourceTasDependencyConfirmationState(this);
                    overworld.SourceScene.QueueSourceEntity(oui);

                    if (overworld.Current is ChapterPanelSequence chapterPanel)
                    {
                        // Avoid panning camera back to MountainIdle
                        chapterPanel.OwnedComponents.Add(new SimulationCoroutine(Process, chapterPanel.EaseOut()));
                        overworld.RoutineEntity.OwnedComponents.Add(new SimulationCoroutine(Process,
                            SkipLeaveRoutine(overworld, oui)));
                    }
                    else
                    {
                        overworld.RoutineEntity.OwnedComponents.Add(new SimulationCoroutine(Process,
                            overworld.GotoRoutine(oui)));
                    }

                    break;

                default:
                {
                    foreach ((string name, var version) in missingDependencies)
                    {
                        if (version == null)
                        {
                            driver.ShowSourcePopupToastWithColor($"{name} is not loaded.", PackedColor.Red);
                        }
                        else
                        {
                            driver.ShowSourcePopupToastWithColor($"{name} v{version.ToString(3)} is not loaded.",
                                PackedColor.Red);
                        }
                    }

                    break;
                }
            }

            foreach ((string name, var version) in missingDependencies)
            {
                if (version == null)
                {
                    driver.LogSourceTas(EverestLogLevel.Error,
                        $"'{missingDependencySources[name].FilePath}' line {missingDependencySources[name].FileLine}: {name} is not loaded.");
                }
                else
                {
                    driver.LogSourceTas(EverestLogLevel.Error,
                        $"'{missingDependencySources[name].FilePath}' line {missingDependencySources[name].FileLine}: {name} v{version.ToString(3)} is not loaded.");
                }
            }

            driver.DisableRunLater();

            static IEnumerator SkipLeaveRoutine(OverworldSequence overworld, OuiState next)
            {
                overworld.Transitioning = true;
                overworld.Next = next;
                overworld.Last = overworld.Current;
                overworld.Current = null;
                overworld.Last.Focused = false;
                // Avoid calling last.Leave()

                yield return next.Enter(overworld.Last);
                next.Focused = true;
                overworld.Current = next;
                overworld.Transitioning = false;
                overworld.Next = null;
            }
        }

        [SourceTasClearInputs]
        private static void SourceClearInputs() => Owner.Clear();

        internal void Clear()
        {
            missingDependencies.Clear();
            missingDependencySources.Clear();
        }

        // "RequireDependency, ModName"
        // "RequireDependency, ModName, ModVersion"
        [SourceTasCommand(CommandName, ExecuteTiming = SourceTasExecuteTiming.Parse, CalcChecksum = true,
            MetaDataProvider = typeof(Meta))]
        internal void RequireDependency(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            var controller = driver.Controller;
            var src = new SourceTasSourceLocation(filePath, fileLine, studioLine);

            string[] args = commandLine.Arguments;
            if (args.Length == 0)
            {
                controller.ReportError(src, "Expected 'Mod Name' argument");
                return;
            }

            string modName = args[0];
            foreach (var installed in Modules.Where(module => module.Metadata.Name == modName))
            {
                if (args.Length == 1)
                {
                    return; // Accept any version
                }

                if (EverestLoaderProcess.VersionSatisfiesDependency(new Version(args[1]), installed.Metadata.Version))
                {
                    return;
                }
            }

            // No matching mod found
            missingDependencies[modName] = args.Length == 1 ? null : new Version(args[1]);
            missingDependencySources[modName] = src;
        }

        internal TextMenuPresentation CreateTextMenuPresentation(Action<TextMenuPresentation> onConfirm,
            Action<TextMenuPresentation> onCancel)
        {
            var menu = new TextMenuPresentation(Process);
            menu.OnESC = menu.OnCancel = menu.OnPause = () => onCancel(menu);

            menu.Add(new TextMenuPresentation.Header(ToDialogText("MISSING_DEPENDENCIES")));
            menu.Add(new TextMenuPresentation.SubHeader(ToDialogText("INSTALL_DEPENDENCIES"), topPadding: false));
            menu.Add(new EverestMenuSubHeaderExt("") { HeightExtra = 10.0f });

            int count = 0;
            const int maxEntries = 6;
            foreach ((string name, var version) in missingDependencies)
            {
                if (count == maxEntries && missingDependencies.Count > maxEntries)
                {
                    string text = ToDialogText("REMAINING_DEPENDENCIES")
                        .Replace("((left))", (missingDependencies.Count - (maxEntries - 1)).ToString());
                    menu.Add(new TextMenuPresentation.Button(text) { Selectable = false });
                    break;
                }

                menu.Add(new TextMenuPresentation.Button(version == null ? name : $"{name} v{version.ToString(3)}")
                    { Selectable = false });
                count++;
            }

            menu.Add(new EverestMenuSubHeaderExt("") { HeightExtra = 30.0f });

            menu.Add(
                new TextMenuPresentation.Button(Dialog.Clean("menu_return_continue")).Pressed(() => onConfirm(menu)));
            menu.Add(new TextMenuPresentation.Button(Dialog.Clean("menu_return_cancel")).Pressed(menu.OnCancel));

            return menu;
        }

        private void ShowTextMenuPresentation(Level level)
        {
            var dependencies = missingDependencies.Select(entry => new EverestModuleMetadata
            {
                Name = entry.Key,
                Version = entry.Value ?? new Version(0, 0, 1), // A "0.0.*" version is special cased
            }).ToList();

            level.WasPaused = true;
            if (!level.Paused)
            {
                level.StartPauseEffects();
            }
            else if (level.SourceScene.Entities.GetEntitiesTrackIfNeeded(typeof(TextMenuPresentation))
                             .FirstOrDefault() as
                         TextMenuPresentation is { } textMenu)
            {
                textMenu.Close();
            }

            level.Paused = true;
            level.PauseMainMenuOpen = false;
            var menu = CreateTextMenuPresentation(menu =>
            {
                // Adjusted vanilla 'Save & Quit' logic
                menu.Focused = false;
#pragma warning disable CS0618 // Type or member is obsolete
                Process.Effects.TimeRate = 1f;
#pragma warning restore CS0618 // Type or member is obsolete
                Audio.SetMusic(null);
                Audio.BusStopAll("bus:/gameplay_sfx", immediate: true);

                level.Session.InArea = true;
                level.Session.Deaths++;
                level.Session.DeathsInCurrentLevel++;
                var area = Process.AreaKeys.GetSessionArea(level.Session);
                ((IOriginalGameplayProcess)Process).SaveContext.AddDeath(
                    new global::Celeste.Core.Campaigns.CampaignAreaKey(area.ID, area.Mode));

                level.DoScreenWipe(wipeIn: false, () =>
                {
                    Process.Frame.Scenes.NextScene = new SourceTasDependencyLevelExitSequence(LevelExitMode.SaveAndQuit,
                        level.Session, Process, level.HiresSnow)
                    {
                        OverworldLoaderOverride =
                            new SourceTasDependencyOverworldLoaderSequence(Process, OverworldStartMode.MainMenu)
                            {
                                PostLoadAction = overworld =>
                                {
                                    // Setup mountain model
                                    overworld.Mountain.SnapCamera(-1,
                                        new MountainCameraState(new Vector3(0f, 6.0f, 12.0f),
                                            MountainRendererState.RotateLookAt,
                                            ((Celeste.Core.Mathematics.ISourceVectorEnvironment)Process)
                                            .VectorArithmetic));
                                    overworld.Mountain.GotoRotationMode();
                                    overworld.Maddy.Hide();

                                    // Hide main menu
                                    if (overworld.Current is MainMenuSequence mainMenu)
                                    {
                                        mainMenu.MountainStartFront = false;
                                    }

                                    overworld.Current.OwnedComponents.RemoveAll<SimulationCoroutine>();
                                    overworld.Current.Focused = overworld.Current.Visible = false;

                                    // Show dependency installer
                                    ((IEverestDependencyDownloadEnvironment)Process).DependencyDownloads
                                        .MissingDependencies = dependencies;

                                    var oui = overworld.GetUI<EverestDependencyDownloaderState>();
                                    oui.Visible = true;
                                    overworld.Last = overworld.Current = oui;
                                    overworld.RoutineEntity.OwnedComponents.Add(
                                        new SimulationCoroutine(Process, EnterRoutine(oui)));
                                }
                            }
                    };
                }, hiresSnow: true);

                foreach (var component in level.SourceScene.Components.GetTrackedComponents("Celeste.LevelEndingHook"))
                {
                    if (((LevelEndingHookState)component).OnEnd != null)
                    {
                        ((LevelEndingHookState)component).OnEnd();
                    }
                }
            }, menu =>
            {
                menu.RemoveSelf();
                level.Paused = false;
                level.UnpauseTimer = 0.15f;
                Audio.Play(OriginalSfxProcess.ui_game_unpause);
            });

            level.SourceScene.QueueSourceEntity(menu);

            static IEnumerator EnterRoutine(OuiState target)
            {
                var enumerator = target.Enter(null);
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }

                target.Focused = true;
            }
        }
    }
}
