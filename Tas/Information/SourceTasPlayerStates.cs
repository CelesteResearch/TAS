using System;
using System.Collections.Generic;
using Celeste.Simulation;
using Celeste.Core.Scheduling;

namespace Celeste.Mods.Tas.Information
{
    public sealed class SourceTasPlayerStates
    {
        private readonly IDictionary<int, string> States = new Dictionary<int, string>
        {
            { 0, "StNormal" },
            { 1, "StClimb" },
            { 2, "StDash" },
            { 3, "StSwim" },
            { 4, "StBoost" },
            { 5, "StRedDash" },
            { 6, "StHitSquash" },
            { 7, "StLaunch" },
            { 8, "StPickup" },
            { 9, "StDreamDash" },
            { 10, "StSummitLaunch" },
            { 11, "StDummy" },
            { 12, "StIntroWalk" },
            { 13, "StIntroJump" },
            { 14, "StIntroRespawn" },
            { 15, "StIntroWakeUp" },
            { 16, "StBirdDashTutorial" },
            { 17, "StFrozen" },
            { 18, "StReflectionFall" },
            { 19, "StStarFly" },
            { 20, "StTempleFall" },
            { 21, "StCassetteFly" },
            { 22, "StAttract" },
            { 23, "StIntroMoonJump" },
            { 24, "StFlingBird" },
            { 25, "StIntroThinkForABit" },
        };

        [Obsolete("GetStateName(int) is deprecated, please use GetCurrentStateName(Player) instead.")]
        public string GetStateName(int state)
        {
            return States.TryGetValue(state, out string? name) ? name : state.ToString();
        }

        public string GetCurrentStateName(RoomPlayerSession player)
        {
            if (!States.TryGetValue(player.Machine.State, out string? name))
            {
                name = ((ISourceNamedStateMachine)player.Machine).GetCurrentStateName();
            }

            // Ensure "St" prefix
            if (!name.StartsWith("St"))
            {
                name = $"St{name}";
            }

            return name;
        }

        // ReSharper disable once UnusedMember.Global
        public void Register(int state, string stateName)
        {
            States[state] = stateName;
        }

        // ReSharper disable once UnusedMember.Global
        public bool Unregister(int state)
        {
            return States.Remove(state);
        }
    }
}
