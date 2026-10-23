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
        private int GetTransitionFrames(Level level, LevelData nextLevelData)
        {
            int result = 0;
            var session = level.Session;

            bool darkRoom = nextLevelData.Dark && !session.GetFlag("ignore_darkness_" + nextLevelData.Name);

            float lightingStart = level.Lighting.Alpha;
            float lightingCurrent = lightingStart;
            float lightingEnd = darkRoom ? session.DarkRoomAlpha : level.BaseLightingAlpha + session.LightingAlphaAdd;
            bool lightingWait = lightingStart >= session.DarkRoomAlpha || lightingEnd >= session.DarkRoomAlpha;
            if (lightingWait)
            {
                while (Math.Abs(lightingCurrent - lightingEnd) > 0.000001f)
                {
                    result++;
                    lightingCurrent =
                        global::Celeste.Core.Mathematics.ScalarMath.Approach(lightingCurrent, lightingEnd,
                            2f * process.DeltaTime);
                }
            }

            if (ToCeilingFrames(level.NextTransitionDuration) is var transitionDuration &&
                transitionDuration == int.MaxValue)
            {
                return int.MaxValue;
            }

            result += transitionDuration + 1;

            return result;
        }
    }
}
