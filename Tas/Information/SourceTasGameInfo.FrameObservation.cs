using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Core.Scheduling;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;
using Actor = Celeste.Simulation.Gameplay.Actors.Foundation.SourceActorEntityState;
using Player = Celeste.Simulation.RoomPlayerSession;
using LevelData = Celeste.Game.Content.Maps.Levels.OriginalLevelRoomData;
using Overworld = Celeste.Game.Menus.Overworld.OverworldSequence;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasGameInfo
    {
        internal float ObserveSourceDashYield(float dashTime)
        {
            DashTime = dashTime;
            return dashTime;
        }

        internal void AroundSourceEngineUpdate(Action original)
        {
            Frozen = process.Effects.FreezeTimer > 0;
            original();
            if (Frozen) Update();
        }

        internal void AfterSourceSceneAfterUpdate(ISourceSceneExecution scene)
        {
            if (scene is Level level) Update(!level.WasPaused);
            else Update();
        }

        internal void OnSourceLevelTransition(Level level, LevelData next, Vector2 direction)
        {
            TransitionFrames = GetTransitionFrames(level, next);
        }

        internal void AfterSourceLevelUpdate()
        {
            if (TransitionFrames > 0 && TransitionFrames != int.MaxValue) TransitionFrames--;
        }
    }
}
