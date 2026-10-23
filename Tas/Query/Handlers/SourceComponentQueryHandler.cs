using System;
using System.Collections.Generic;
using System.Linq;
using StudioCommunication;
using TargetQuery = Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess;

namespace Celeste.Mods.Tas.Query.Handlers
{
    internal class SourceComponentQueryHandler : TargetQuery.Handler
    {
        public SourceComponentQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
        }

        private bool IsApplicableSourceType(Type type, string sourceType)
        {
            Type role = sourceType == "Monocle.Component"
                ? typeof(global::Celeste.Core.Components.SourceComponentState)
                : typeof(global::Celeste.Scene.Entities.SceneEntityState);
            return type != null && role.IsAssignableFrom(type) &&
                   Query.IsSameOrSubclassOfOriginal(type, sourceType);
        }

        public override bool CanResolveInstances(Type type) => !SourceQueryRuntimeDataTypes.Contains(type) &&
                                                               IsApplicableSourceType(type, "Monocle.Component");

        public override bool CanEnumerateTypeEntries(Type type) =>
            type == typeof(PlayerState) || type == typeof(SeekerState) || type == typeof(OshiroState);

        public override object[] ResolveInstances(Type type)
        {
            var scene = ((global::Celeste.Scene.Tracking.ISourceScene)Query.Engine.Frame.Scenes.CurrentScene)
                .SourceScene;
            var definition = Query.SceneCatalog.Find(type)
                             ?? throw new InvalidOperationException(
                                 "Actual component type has no qualified source catalog binding.");
            IEnumerable<global::Celeste.Core.Components.SourceComponentState> componentInstances;
            if (scene.Components.Components.TryGetValue(definition.Name, out var components))
            {
                componentInstances = components;
            }
            else
            {
                componentInstances = scene.Entities
                    .SelectMany(e => ((global::Celeste.Core.Components.ISourceOwnedComponentList)e).OwnedComponents)
                    .Where(c => Query.IsSameOrSubclassOfOriginal(c.GetType(), type));
            }

            return componentInstances.Select(object (c) => c).ToArray();
        }

        internal static Type SourcePlayerStateType => typeof(PlayerState);
        internal static Type SourceSeekerStateType => typeof(SeekerState);
        internal static Type SourceOshiroStateType => typeof(OshiroState);

        // Marker types for StateMachine instances
        private struct PlayerState;

        private struct SeekerState;

        private struct OshiroState;

        private static readonly string[] playerStates =
        [
            "StNormal",
            "StClimb",
            "StDash",
            "StSwim",
            "StBoost",
            "StRedDash",
            "StHitSquash",
            "StLaunch",
            "StPickup",
            "StDreamDash",
            "StSummitLaunch",
            "StDummy",
            "StIntroWalk",
            "StIntroJump",
            "StIntroRespawn",
            "StIntroWakeUp",
            "StBirdDashTutorial",
            "StFrozen",
            "StReflectionFall",
            "StStarFly",
            "StTempleFall",
            "StCassetteFly",
            "StAttract",
            "StIntroMoonJump",
            "StFlingBird",
            "StIntroThinkForABit",
        ];

        private static readonly string[] seekerStates =
        [
            "StIdle",
            "StPatrol",
            "StSpotted",
            "StAttack",
            "StStunned",
            "StSkidding",
            "StRegenerate",
            "StReturned",
        ];

        private static readonly string[] oshiroStates =
        [
            "StChase",
            "StChargeUp",
            "StAttack",
            "StDummy",
            "StWaiting",
            "StHurt",
        ];

        public override Result<bool, TargetQuery.MemberAccessError> ResolveTargetTypes(out Type[] targetTypes,
            Type type,
            ref int memberIdx, string[] memberArgs)
        {
            bool memberAccess = memberIdx + 1 < memberArgs.Length && memberArgs[memberIdx + 1] == "State";
            bool componentAccess = false;
            if (memberIdx + 2 < memberArgs.Length && memberArgs[memberIdx] == SourceEntityQueryHandler.ComponentKey)
            {
                string[] componentQuery = memberArgs[(memberIdx + 1)..];
                var componentTypes = Query.ParseGenericBaseTypes(componentQuery, out string[] componentMemberArgs);
                componentAccess = componentTypes.Any(t => Query.GetOriginalTypeFullName(t) == "Monocle.StateMachine") &&
                                  componentMemberArgs.Length > 0 && componentMemberArgs[0] == "State";
            }

            int advanceIdx = componentAccess ? 2 : 1;

            if (IsApplicableSourceType(type, "Celeste.Player") &&
                (memberAccess && memberArgs[memberIdx] == "StateMachine" || componentAccess))
            {
                memberIdx += advanceIdx; // Skip over parsed arguments
                targetTypes = [typeof(PlayerState)];
                return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
            }

            if (IsApplicableSourceType(type, "Celeste.Seeker") &&
                (memberAccess && memberArgs[memberIdx] == "State" || componentAccess))
            {
                memberIdx += advanceIdx; // Skip over parsed arguments
                targetTypes = [typeof(SeekerState)];
                return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
            }

            if (IsApplicableSourceType(type, "Celeste.AngryOshiro") &&
                (memberAccess && memberArgs[memberIdx] == "state" || componentAccess))
            {
                memberIdx += advanceIdx; // Skip over parsed arguments
                targetTypes = [typeof(OshiroState)];
                return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
            }

            targetTypes = null!;
            return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
        }

        public override IEnumerator<CommandAutoCompleteEntry> EnumerateTypeEntries(Type type)
        {
            if (type == typeof(PlayerState))
            {
                foreach (string state in playerStates)
                {
                    yield return new CommandAutoCompleteEntry
                    {
                        Name = state,
                        Extra = "State",
                        Prefix = $"Player.",
                        Suggestion = true,
                        IsDone = true,
                        StorageKey = $"{nameof(TargetQuery.Variant.Get)}_Celeste.Player",
                        StorageName = state,
                    };
                }
            }
            else if (type == typeof(SeekerState))
            {
                foreach (string state in seekerStates)
                {
                    yield return new CommandAutoCompleteEntry
                    {
                        Name = state,
                        Extra = "State",
                        Prefix = $"Seeker.",
                        Suggestion = true,
                        IsDone = true,
                        StorageKey = $"{nameof(TargetQuery.Variant.Get)}_Celeste.Seeker",
                        StorageName = state,
                    };
                }
            }
            else if (type == typeof(OshiroState))
            {
                foreach (string state in oshiroStates)
                {
                    yield return new CommandAutoCompleteEntry
                    {
                        Name = state,
                        Extra = "State",
                        Prefix = $"AngryOshiro.",
                        Suggestion = true,
                        IsDone = true,
                        StorageKey = $"{nameof(TargetQuery.Variant.Get)}_Celeste.AngryOshiro",
                        StorageName = state,
                    };
                }
            }
        }

        public override bool IsMemberSuggested(SourceQueryMemberIdentity member, TargetQuery.Variant variant)
        {
            if (member.DeclaringType != null &&
                typeof(global::Celeste.Core.Components.SourceComponentState).IsAssignableFrom(member.DeclaringType) &&
                Query.GetOriginalTypeFullName(member.DeclaringType) == "Monocle.StateMachine" && member.Name is "State")
            {
                return true;
            }

            return false;
        }
    }
}
