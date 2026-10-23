using Celeste.Core.Display;
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
using Celeste.Core.Mathematics;
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
    public sealed class SourceTasDependencyConfirmationState : OuiState
    {
        private readonly SourceTasRequireDependencyProcess owner;
        private GameApplication Process => owner.Process;
        private OriginalGameAudioSession Audio => owner.DependencyConfirmationAudio;
        private const float onScreenX = 1920f / 2.0f;
        private const float offScreenX = onScreenX + 1920f;

        private readonly TextMenuPresentation menu;
        private float alpha = 0.0f;

        private OuiState? previousOui;
        private bool? markerRunning;
        private ChapterSelectIconPresentation? selectedIcon;

        public SourceTasDependencyConfirmationState(ISourceEngineContext engine)
            : this(TasRuntimeEnvironment.Get((GameApplication)engine).ActualTasDriver.SourceRequireDependencyCommand)
        {
        }

        internal SourceTasDependencyConfirmationState(SourceTasRequireDependencyProcess owner) : base(owner.Process)
        {
            this.owner = owner;
            var dependencies = owner.MissingDependenciesForConfirmation.Select(entry => new EverestModuleMetadata
            {
                Name = entry.Key,
                Version = entry.Value ?? new Version(0, 0, 1), // A "0.0.*" version is special cased
            }).ToList();

            menu = owner.CreateTextMenuPresentation(_ =>
            {
                ((IEverestDependencyDownloadEnvironment)Process).DependencyDownloads.MissingDependencies = dependencies;
                Overworld.Goto<EverestDependencyDownloaderState>();
            }, _ =>
            {
                // Return to previous
                Audio.Play(OriginalSfxProcess.ui_main_button_back);
                if (previousOui != null)
                {
                    Overworld.RoutineEntity.OwnedComponents.Add(new SimulationCoroutine(Process,
                        Overworld.GotoRoutine(previousOui)));
                }
                else
                {
                    Overworld.Goto<MainMenuSequence>();
                }
            });
        }

        public override IEnumerator Enter(OuiState from)
        {
            previousOui = from;
            markerRunning = Overworld.Maddy.Show ? Overworld.Maddy.IsRunning : null;
            Overworld.Maddy.Hide();

            // Handle special cases
            foreach (var icon in Overworld.SourceScene.Entities.FindAll<ChapterSelectIconPresentation>())
            {
                if (icon.Selected)
                {
                    selectedIcon = icon;

                    var inspector = Overworld.GetUI<ChapterPanelSequence>();

                    var iconFrom = inspector.OpenPosition + inspector.IconOffset;
                    var iconTo = inspector.ClosePosition + inspector.IconOffset;

                    icon.Scale = Vector2.One;
                    icon.Hidden = true;
                    icon.Selected = false;
                    icon.StartTween(0.25f,
                        tween => icon.Position =
                            Vector2Interpolation.Lerp(iconFrom, iconTo, tween.Eased, Process.ScalarArithmetic));
                }
                else
                {
                    icon.Hide();
                }
            }

            ((global::Celeste.Scene.Tracking.ISourceScene)Scene).SourceScene.QueueSourceEntity(menu);

            menu.Visible = Visible = true;
            menu.Focused = false;

            for (float p = 0.0f; p < 1.0f; p += ((ISourceEngineContext)Process).DeltaTime * 4.0f)
            {
                menu.X = offScreenX - 1920f * ((ISourceEasingEnvironment)Process).EasingArithmetic.CubeOut(p);
                alpha = ((ISourceEasingEnvironment)Process).EasingArithmetic.CubeOut(p);
                yield return null;
            }

            menu.Focused = true;
        }

        public override IEnumerator Leave(OuiState next)
        {
            Audio.Play(OriginalSfxProcess.ui_main_whoosh_large_out);
            menu.Focused = false;

            for (float p = 0.0f; p < 1.0f; p += ((ISourceEngineContext)Process).DeltaTime * 4.0f)
            {
                menu.X = onScreenX + 1920f * ((ISourceEasingEnvironment)Process).EasingArithmetic.CubeIn(p);
                alpha = 1.0f - ((ISourceEasingEnvironment)Process).EasingArithmetic.CubeIn(p);
                yield return null;
            }

            if (markerRunning is { } running)
            {
                if (running)
                {
                    Overworld.Maddy.Running();
                }
                else
                {
                    Overworld.Maddy.Falling();
                }
            }

            // Handle special cases
            if (selectedIcon is { } icon)
            {
                var inspector = Overworld.GetUI<ChapterPanelSequence>();

                var iconFrom = inspector.ClosePosition + inspector.IconOffset;
                var iconTo = inspector.OpenPosition + inspector.IconOffset;

                icon.Scale = Vector2.One;
                icon.Hidden = false;
                icon.Selected = true;
                icon.StartTween(0.25f,
                    tween => icon.Position =
                        Vector2Interpolation.Lerp(iconFrom, iconTo, tween.Eased, Process.ScalarArithmetic));
            }

            menu.Visible = Visible = false;
            menu.RemoveSelf();
            RemoveSelf();
        }

        public override void Render(ISourceRenderContext context)
        {
            if (alpha > 0.0f)
            {
                const float padding = 10.0f;
                SourcePrimitiveDrawing.Rect(context, this, -padding, -padding, 1920f + padding * 2.0f,
                    1080f + padding * 2.0f, PackedColor.Black * alpha * 0.4f);
            }
        }
    }
}
