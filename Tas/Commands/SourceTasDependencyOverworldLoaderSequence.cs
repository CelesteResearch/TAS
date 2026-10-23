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
    internal sealed class SourceTasDependencyOverworldLoaderSequence : OverworldLoaderSequence
    {
        public Action<OverworldSequence>? PostLoadAction;

        internal SourceTasDependencyOverworldLoaderSequence(ISourceEngineContext engine,
            OverworldStartMode startMode, HiresSnowState? snow = null) : base(engine, startMode, snow)
        {
            Snow = null;
            fadeIn = false;
        }

        public override void Begin()
        {
            Renderers.Add(new HudRendererState());
            Renderers.UpdateLists();
            ChapterCompletionSession? session = null;
            var save = ((IOriginalGameplayProcess)EngineContext).SaveContext.CurrentSave;
            if (save != null) session = save.CurrentSession;
            var entity =
                new SceneEntityState(((ISourceEntitySourceDataEnvironment)EngineContext).EntitySourceDataRegistration);
            entity.OwnedComponents.Add(new SimulationCoroutine(EngineContext, Routine(session)));
            SourceScene.QueueSourceEntity(entity);
            activeThread = Thread.CurrentThread;
            activeThread.Priority = ThreadPriority.Lowest;
            ((IOriginalThreadEnvironment)EngineContext).Threads.Start(LoadThreadExt, "OVERWORLD_LOADER_EXT", true);
        }

        private void LoadThreadExt()
        {
            LoadThread();
            PostLoadAction?.Invoke(overworld);
        }
    }
}
