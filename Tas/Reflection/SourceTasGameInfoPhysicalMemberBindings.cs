using System;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Scheduling;
using Celeste.Game.Gameplay.Collectibles.Strawberries;

namespace Celeste.Mods.Tas.Reflection
{
    public static class SourceTasGameInfoPhysicalMemberBindings
    {
        public static void Register(GameApplication process)
        {
            const string assembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            var members = process.SourceMembers;
            BindField(members, assembly, "Celeste.Level", "Wind", "Microsoft.Xna.Framework.Vector2",
                typeof(global::Celeste.Game.Execution.Levels.OriginalLevelSceneRoot), typeof(System.Numerics.Vector2));
            BindField(members, assembly, "Monocle.StateMachine", "currentCoroutine", "Monocle.Coroutine",
                typeof(SimulationStateMachine), typeof(SimulationCoroutine));
            BindField(members, assembly, "Monocle.Coroutine", "waitTimer", "System.Single",
                typeof(SimulationCoroutine), typeof(float));
            BindField(members, assembly, "Celeste.TheoCrystal", "Hold", "Celeste.Holdable",
                typeof(global::Celeste.Game.Gameplay.Holdables.TheoCrystal.TheoCrystalState),
                typeof(global::Celeste.Simulation.Gameplay.Holdables.HoldableState));
            BindField(members, assembly, "Celeste.Holdable", "cannotHoldTimer", "System.Single",
                typeof(global::Celeste.Simulation.Gameplay.Holdables.HoldableState), typeof(float),
                "<CannotHoldTimer>k__BackingField");
            var actualType = typeof(OriginalStrawberryState);
            var owner = members.GetOriginalTypeDefinition(actualType);
            var definition = members.RequireDefinition(assembly, "Celeste.Strawberry", "collectTimer",
                SourceMemberKind.Field);
            if (owner.AssemblyIdentity != assembly || owner.Identity != "Celeste.Strawberry" || definition.IsStatic ||
                definition.ValueTypeIdentity != "System.Single")
                throw new InvalidOperationException("The genuine Strawberry collectTimer source field does not match.");
            var field = actualType.GetField("collectTimer", BindingFlags.NonPublic | BindingFlags.Instance |
                                                            BindingFlags.DeclaredOnly) ??
                        throw new MissingFieldException(actualType.FullName, "collectTimer");
            if (field.IsStatic || field.FieldType != typeof(float))
                throw new InvalidOperationException(
                    "The genuine Strawberry collectTimer physical field does not match.");
            members.RegisterBinding(new SourcePhysicalMemberBinding(definition, actualType, field));
        }

        private static void BindField(SourceMemberCatalog members, string assembly, string sourceType,
            string fieldName, string sourceValueType, Type actualType, Type actualValueType,
            string actualFieldName = null)
        {
            var owner = members.GetOriginalTypeDefinition(actualType);
            var definition = members.RequireDefinition(assembly, sourceType, fieldName, SourceMemberKind.Field);
            if (owner.AssemblyIdentity != assembly || owner.Identity != sourceType || definition.IsStatic ||
                definition.ValueTypeIdentity != sourceValueType)
                throw new InvalidOperationException("The genuine GameInfo source field does not match: " + sourceType +
                                                    "." + fieldName);
            var field = actualType.GetField(actualFieldName ?? fieldName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.DeclaredOnly) ?? throw new MissingFieldException(actualType.FullName, fieldName);
            if (field.IsStatic || field.FieldType != actualValueType)
                throw new InvalidOperationException("The genuine GameInfo physical field does not match: " +
                                                    sourceType +
                                                    "." + fieldName);
            var actualScope = members.GetActualSourceType(assembly, sourceType);
            if (!actualType.IsAssignableFrom(actualScope) ||
                !ReferenceEquals(members.GetOriginalTypeDefinition(actualScope), owner))
                throw new InvalidOperationException(
                    "The GameInfo field scope is not a genuine selected source derivative.");
            members.RegisterBinding(new SourcePhysicalMemberBinding(definition, actualType, field,
                actualInstanceScope: actualScope));
        }
    }
}
