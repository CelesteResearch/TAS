using Celeste.Mods.Tas.Information;
using Celeste.Core.Reflection.Members;
using System.Numerics;
using StudioCommunication;
using StudioCommunication.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TargetQuery = Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess;

namespace Celeste.Mods.Tas.Query.Handlers
{
    internal class SourceEntityQueryHandler : TargetQuery.Handler
    {
        public SourceEntityQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
        }

        /// Holds a position with integer and fractional part separated
        internal struct SubpixelPosition(SubpixelComponent x, SubpixelComponent y)
        {
            public SubpixelComponent X = x;
            public SubpixelComponent Y = y;

            public static implicit operator Vector2(SubpixelPosition value)
            {
                return new Vector2(value.X, value.Y);
            }

            public static implicit operator SubpixelPosition(Vector2 value)
            {
                return new SubpixelPosition(value.X, value.Y);
            }

            public override string ToString() => $"{{X:{X} Y:{Y}}}";

            public string ToFormattedString(int decimals) =>
                $"{X.ToFormattedString(decimals)}, {Y.ToFormattedString(decimals)}";
        }

        /// Holds a single axis with integer and fractional part separated
        internal record struct SubpixelComponent(float position, float remainder)
        {
            public float Position = position;
            public float Remainder = remainder;

            public static implicit operator float(SubpixelComponent value)
            {
                return value.Position;
            }

            public static implicit operator SubpixelComponent(float value)
            {
                int position = (int)Math.Round(value);
                float remainder = value - position;

                return new(position, remainder);
            }

            public static implicit operator SubpixelComponent(double value)
            {
                int position = (int)Math.Round(value);
                float remainder = (float)(value - position);

                return new(position, remainder);
            }

            public override string ToString() => ((double)Position + Remainder).ToString(CultureInfo.InvariantCulture);
            public string ToFormattedString(int decimals) => ((double)Position + Remainder).ToFormattedString(decimals);
        }

        /// Matches an EntityID specification on the base type
        /// e.g. `BaseType[Room:ID]`
        private static readonly Regex EntityIDRegex = new(@"^(.+)(?:\[(.+):(\d+)\])(.*)$", RegexOptions.Compiled);

        private static readonly Regex IncompleteEntityIDRegex = new(@"^([\w@]*)\[(.*)$", RegexOptions.Compiled);

        /// Matches a component specification on the base type / members
        /// e.g. `BaseTypeOrMember:Component@Assembly`
        private static readonly Regex ComponentRegex = new(@"^([\w@]*):([\w@]*)$", RegexOptions.Compiled);

        private const string EntityIDKey = "___EntityIDFilter___";
        private const string IncompleteEntityIDKey = "___IncompleteEntityIDFilter___";
        public const string ComponentKey = "___ComponentAccess___";

        private bool IsApplicableSourceType(Type type, string sourceType)
        {
            Type role = sourceType == "Monocle.Component"
                ? typeof(global::Celeste.Core.Components.SourceComponentState)
                : typeof(global::Celeste.Scene.Entities.SceneEntityState);
            return type != null && role.IsAssignableFrom(type) &&
                   Query.IsSameOrSubclassOfOriginal(type, sourceType);
        }

        public override bool CanResolveInstances(Type type) => !SourceQueryRuntimeDataTypes.Contains(type) &&
                                                               IsApplicableSourceType(type, "Monocle.Entity");

        public override bool CanResolveValue(Type type) =>
            type == typeof(SubpixelComponent) || type == typeof(SubpixelPosition);

        public override (bool CanEnumerate, bool ShouldOverride) CanEnumerateMemberEntries(Type type,
            TargetQuery.Variant variant, string queryPrefix, int memberIdx, string[] memberArgs)
        {
            if (type == typeof(SubpixelPosition))
            {
                return (CanEnumerate: true, ShouldOverride: true);
            }

            if ((memberIdx > 0 && IsApplicableSourceType(type, "Monocle.Entity")))
            {
                // Check for EntityID filter
                if (memberArgs[memberIdx - 1] == IncompleteEntityIDKey)
                {
                    return (CanEnumerate: true, ShouldOverride: true);
                }

                // Search for last component access
                for (int i = memberIdx - 1; i >= 0; i--)
                {
                    if (memberArgs[i] != ComponentKey)
                    {
                        continue;
                    }

                    string[] componentQuery = memberArgs[(i + 1)..];
                    var componentTypes = Query.ParseGenericBaseTypes(componentQuery, out string[] _);

                    // Only auto-complete if base-type is incomplete
                    return (CanEnumerate: !componentTypes.Any(), ShouldOverride: true);
                }
            }

            return (CanEnumerate: false, ShouldOverride: false);
        }

        public override IEnumerable<string> ProcessQueryArguments(IEnumerable<string> queryArgs, bool isAutoComplete)
        {
            using var enumerator = queryArgs.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string arg = enumerator.Current;

                if (EntityIDRegex.Match(arg) is { Success: true } entityMatch &&
                    int.TryParse(entityMatch.Groups[3].Value, out int id))
                {
                    yield return entityMatch.Groups[1].Value;
                    yield return EntityIDKey;
                    yield return $"{entityMatch.Groups[2].Value}:{id}";

                    if (ComponentRegex.Match(entityMatch.Groups[4].Value) is { Success: true } componentMatch)
                    {
                        if (!string.IsNullOrWhiteSpace(componentMatch.Groups[1].Value))
                        {
                            yield return TargetQuery.InvalidQueryArgument;
                            yield break;
                        }

                        yield return ComponentKey;
                        yield return componentMatch.Groups[2].Value;
                    }
                    else if (!string.IsNullOrWhiteSpace(entityMatch.Groups[4].Value))
                    {
                        yield return TargetQuery.InvalidQueryArgument;
                        yield break;
                    }
                }
                else if (isAutoComplete && IncompleteEntityIDRegex.Match(arg) is
                             { Success: true } entityIncompleteMatch)
                {
                    yield return entityIncompleteMatch.Groups[1].Value;
                    yield return IncompleteEntityIDKey;
                    yield return entityIncompleteMatch.Groups[2].Value;

                    if (enumerator.MoveNext())
                    {
                        // Only exists for auto-complete, so disallow arguments following this
                        yield return TargetQuery.InvalidQueryArgument;
                    }
                }
                else if (ComponentRegex.Match(arg) is { Success: true } componentMatch)
                {
                    yield return componentMatch.Groups[1].Value;
                    yield return ComponentKey;
                    yield return componentMatch.Groups[2].Value;
                }
                else
                {
                    yield return arg;
                }
            }
        }

        public override IEnumerable<string> FormatQueryArguments(IEnumerable<string> queryArgs)
        {
            using var enumerator = new PeekEnumerator<string>(queryArgs);
            foreach (string arg in enumerator)
            {
                if (!enumerator.TryPeek(out string? key))
                {
                    yield return arg;
                    yield break;
                }

                switch (key)
                {
                    case ComponentKey:
                        enumerator.MoveNext(); // Drop 'ComponentKey' arg
                        if (enumerator.MoveNext())
                        {
                            yield return $"{arg}:{enumerator.Current}";
                        }
                        else
                        {
                            yield return $"{arg}:";
                        }

                        continue;

                    case EntityIDKey:
                        enumerator.MoveNext(); // Drop 'EntityIDKey' arg
                        enumerator.MoveNext();
                        if (enumerator.TryPeek(out string? next) && next == ComponentKey)
                        {
                            string id = enumerator.Current;
                            enumerator.MoveNext(); // Drop 'ComponentKey' arg
                            if (enumerator.MoveNext())
                            {
                                yield return $"{arg}[{id}]:{enumerator.Current}";
                            }
                            else
                            {
                                yield return $"{arg}[{id}]:";
                            }
                        }
                        else
                        {
                            yield return $"{arg}[{enumerator.Current}]";
                        }

                        continue;

                    case IncompleteEntityIDKey:
                        enumerator.MoveNext(); // Drop 'IncompleteEntityIDKey' arg
                        if (enumerator.MoveNext())
                        {
                            yield return $"{arg}[{enumerator.Current}";
                        }
                        else
                        {
                            yield return $"{arg}[";
                        }

                        continue;

                    default:
                        yield return arg;
                        continue;
                }
            }
        }

        private global::Celeste.Scene.Tracking.SceneEntityRegistry CurrentEntities
            => ((global::Celeste.Scene.Tracking.ISourceScene)Query.Engine.Frame.Scenes.CurrentScene).SourceScene
                .Entities;

        private IEnumerable<object> GetEntityInstances(Type type)
        {
            var metadata = Query.SceneCatalog.Find(type) ??
                           throw new InvalidOperationException(
                               "Actual entity type has no qualified source catalog binding.");
            var registry = CurrentEntities;
            if (registry.IsTracked(metadata.Name)) return registry.GetTrackedEntities(metadata.Name);
            return registry.Where(entity => Query.IsSameOrSubclassOfOriginal(entity.GetType(), type));
        }

        public override object[] ResolveInstances(Type type) => GetEntityInstances(type).ToArray();

        public override Result<bool, TargetQuery.QueryError> ResolveMemberValues(ref object?[] values,
            ref int memberIdx,
            string[] memberArgs)
        {
            switch (memberArgs[memberIdx])
            {
                case ComponentKey when memberIdx + 1 < memberArgs.Length:
                {
                    string[] componentQuery = memberArgs[(memberIdx + 1)..];
                    var componentTypes = Query.ParseGenericBaseTypes(componentQuery, out string[] componentMemberArgs);
                    if (!componentTypes.Any())
                    {
                        return Result<bool, TargetQuery.QueryError>.Fail(
                            new TargetQuery.QueryError.NoBaseTypes(string.Join('.', componentQuery)));
                    }

                    memberIdx = memberArgs.Length - componentMemberArgs.Length - 1;

                    List<object?>? additionalValues = null;
                    for (int valueIdx = 0; valueIdx < values.Length; valueIdx++)
                    {
                        if (values[valueIdx] is not global::Celeste.Core.Components.ISourceEntitySourceData entity)
                        {
                            values[valueIdx] = Query.InvalidValue;
                            continue;
                        }

                        int componentIdx = 0;
                        foreach (var component in ((global::Celeste.Core.Components.ISourceOwnedComponentList)entity)
                                 .OwnedComponents.Where(c => componentTypes.Any(componentType =>
                                     Query.IsSameOrSubclassOfOriginal(c.GetType(), componentType))))
                        {
                            if (componentIdx == 0)
                            {
                                values[valueIdx] = component;
                            }
                            else
                            {
                                if (additionalValues == null)
                                {
                                    additionalValues = [component];
                                }
                                else
                                {
                                    additionalValues.Add(component);
                                }
                            }

                            componentIdx++;
                        }

                        if (componentIdx == 0)
                        {
                            // No component instances
                            values[valueIdx] = Query.InvalidValue;
                        }
                    }

                    if (additionalValues != null)
                    {
                        int startIdx = values.Length;
                        Array.Resize(ref values, values.Length + additionalValues.Count);
                        additionalValues.CopyTo(values, startIdx);
                    }

                    return Result<bool, TargetQuery.QueryError>.Ok(true);
                }

                case EntityIDKey when memberIdx + 1 < memberArgs.Length:
                {
                    string key = memberArgs[memberIdx + 1];
                    memberIdx++; // Skip over 'EntityIDKey'

                    for (int valueIdx = 0; valueIdx < values.Length; valueIdx++)
                    {
                        if (values[valueIdx] is not global::Celeste.Core.Components.ISourceEntitySourceData entity ||
                            entity.SourceId.Key != key)
                        {
                            values[valueIdx] = Query.InvalidValue;
                        }
                    }

                    return Result<bool, TargetQuery.QueryError>.Ok(true);
                }
            }

            return Result<bool, TargetQuery.QueryError>.Ok(false);
        }

        private bool IsMotionEntity(Type type)
            => IsApplicableSourceType(type, "Celeste.Actor")
               || IsApplicableSourceType(type, "Celeste.Platform");

        private SourceQueryMemberAccess MotionField(Type type, string name)
            => Query.GetOriginalField(type, name, QueryReflection.InstanceAnyVisibility, false)
               ?? throw new MissingFieldException(Query.GetOriginalTypeFullName(type), name);

        public override Result<bool, TargetQuery.MemberAccessError> ResolveMember(object? instance, out object? value,
            Type type, int memberIdx, string[] memberArgs)
        {
            if (instance != null && IsMotionEntity(type))
            {
                string member = memberArgs[memberIdx];
                if (member is "X" or "Y" or "Position")
                {
                    var position = (Vector2)MotionField(type, "Position").Read(instance);
                    var counter = (Vector2)MotionField(type, "movementCounter").Read(instance);
                    value = member switch
                    {
                        "X" => new SubpixelComponent(position.X, counter.X),
                        "Y" => new SubpixelComponent(position.Y, counter.Y),
                        _ => new SubpixelPosition(new SubpixelComponent(position.X, counter.X),
                            new SubpixelComponent(position.Y, counter.Y))
                    };
                    return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
                }
            }

            value = null;
            return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
        }

        public override Result<bool, TargetQuery.MemberAccessError> ResolveTargetTypes(out Type[] targetTypes,
            Type type,
            ref int memberIdx, string[] memberArgs)
        {
            if (IsMotionEntity(type))
            {
                switch (memberArgs[memberIdx])
                {
                    case "X":
                    case "Y":
                        targetTypes = [typeof(SubpixelComponent)];
                        return Result<bool, TargetQuery.MemberAccessError>.Ok(true);

                    case "Position":
                        targetTypes = [typeof(SubpixelPosition)];
                        return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
                }
            }

            // Resolve special syntax
            switch (memberArgs[memberIdx])
            {
                case ComponentKey when memberIdx + 1 < memberArgs.Length:
                {
                    string[] componentQuery = memberArgs[(memberIdx + 1)..];
                    var componentTypes = Query.ParseGenericBaseTypes(componentQuery, out string[] componentMemberArgs);
                    if (!componentTypes.Any())
                    {
                        // Incomplete component type. Pass along current type for auto-complete
                        targetTypes = [type];
                        return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
                    }

                    memberIdx = memberArgs.Length - componentMemberArgs.Length - 1;
                    targetTypes = componentTypes.ToArray();
                    return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
                }
                case ComponentKey when memberIdx + 1 == memberArgs.Length:
                {
                    // Missing component type. Pass along current type for auto-complete
                    targetTypes = [type];
                    return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
                }

                case EntityIDKey or IncompleteEntityIDKey:
                {
                    memberIdx += 1; // Skip over special key

                    targetTypes = [type]; // Type stays the same
                    return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
                }
            }

            targetTypes = null!;
            return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
        }

        public override Result<bool, TargetQuery.MemberAccessError> SetMember(object? instance, object? value,
            Type type,
            int memberIdx, string[] memberArgs, bool forceAllowCodeExecution)
        {
            if (instance == null || !IsMotionEntity(type)) return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
            string member = memberArgs[memberIdx];
            if (member is not ("X" or "Y" or "Position")) return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
            var positionField = MotionField(type, "Position");
            var counterField = MotionField(type, "movementCounter");
            var position = (Vector2)positionField.Read(instance);
            var counter = (Vector2)counterField.Read(instance);
            if (member == "X")
            {
                var component = (SubpixelComponent)value;
                position.X = component.Position;
                positionField.Write(instance, position);
                counter.X = component.Remainder;
                counterField.Write(instance, counter);
            }
            else if (member == "Y")
            {
                var component = (SubpixelComponent)value;
                position.Y = component.Position;
                positionField.Write(instance, position);
                counter.Y = component.Remainder;
                counterField.Write(instance, counter);
            }
            else
            {
                var subpixel = (SubpixelPosition)value;
                position.X = subpixel.X.Position;
                positionField.Write(instance, position);
                position.Y = subpixel.Y.Position;
                positionField.Write(instance, position);
                counter.X = subpixel.X.Remainder;
                counterField.Write(instance, counter);
                counter.Y = subpixel.Y.Remainder;
                counterField.Write(instance, counter);
            }

            return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
        }

        public override Result<bool, TargetQuery.QueryError> ResolveValue(Type targetType, ref int argIdx,
            string[] valueArgs, out object? value)
        {
            if (targetType == typeof(SubpixelComponent) &&
                double.TryParse(valueArgs[argIdx], out double doubleValue)
               )
            {
                int position = (int)Math.Round(doubleValue);
                float remainder = (float)(doubleValue - position);

                value = new SubpixelComponent(position, remainder);
                return Result<bool, TargetQuery.QueryError>.Ok(true);
            }

            if (targetType == typeof(SubpixelPosition) &&
                double.TryParse(valueArgs[argIdx + 0], out double doubleValueX) &&
                double.TryParse(valueArgs[argIdx + 1], out double doubleValueY)
               )
            {
                argIdx++;

                int positionX = (int)Math.Round(doubleValueX);
                int positionY = (int)Math.Round(doubleValueY);
                float remainderX = (float)(doubleValueX - positionX);
                float remainderY = (float)(doubleValueY - positionY);

                value = new SubpixelPosition(
                    new SubpixelComponent(positionX, remainderX),
                    new SubpixelComponent(positionY, remainderY));
                return Result<bool, TargetQuery.QueryError>.Ok(true);
            }

            value = null;
            return Result<bool, TargetQuery.QueryError>.Ok(false);
        }

        public override bool IsTypeSuggested(Type type, TargetQuery.Variant variant)
        {
            if (Query.Engine.Frame.Scenes.CurrentScene is not global::Celeste.Game.Execution.Levels
                    .OriginalLevelSceneRoot)
            {
                // Could support any scene, but usually only Level is relevant
                return false;
            }

            return CurrentEntities.Any(e =>
                e.GetType() == type &&
                // Require a collider to avoid controller entities
                MotionField(e.GetType(), "Collider").Read(e) != null
            );
        }

        public override bool IsMemberSuggested(SourceQueryMemberIdentity member, TargetQuery.Variant variant)
        {
            if (!(member.DeclaringType != null && IsApplicableSourceType(member.DeclaringType, "Monocle.Entity")))
            {
                return false;
            }

            if (IsApplicableSourceType(member.DeclaringType, "Celeste.Actor") &&
                member.Name is "MoveH" or "MoveV")
            {
                return true;
            }

            if (Query.GetOriginalTypeFullName(member.DeclaringType) == "Celeste.Player" &&
                variant is TargetQuery.Variant.Get &&
                member.Name is "Speed" or "StateMachine" or "Stamina"
                    or "Dashes" or "Ducking" or "LiftBoost"
                    or "jumpGraceTimer" or "varJumpTimer" or "varJumpSpeed"
               )
            {
                return true;
            }

            if (Query.GetOriginalTypeFullName(member.DeclaringType) == "Celeste.Seeker" &&
                member.Name is "State" or "StateMachine")
            {
                return true;
            }

            return member.Name
                is "Position"
                or "X"
                or "Y";
        }

        public override IEnumerator<CommandAutoCompleteEntry> EnumerateMemberEntries(Type type,
            TargetQuery.Variant variant,
            string queryPrefix, int memberIdx, string[] memberArgs)
        {
            if (type == typeof(SubpixelPosition))
            {
                yield return new CommandAutoCompleteEntry
                    { Name = "X", Extra = typeof(float).CSharpName(), Prefix = $"{queryPrefix}.", IsDone = true };
                yield return new CommandAutoCompleteEntry
                    { Name = "Y", Extra = typeof(float).CSharpName(), Prefix = $"{queryPrefix}.", IsDone = true };
            }

            if (IsApplicableSourceType(type, "Monocle.Entity"))
            {
                IEnumerable<object> entityInstances = GetEntityInstances(type);

                // Check for EntityID filter
                if (memberArgs[memberIdx - 1] == IncompleteEntityIDKey)
                {
                    foreach (var entity in entityInstances)
                    {
                        var sourceEntity = (global::Celeste.Core.Components.ISourceEntitySourceData)entity;
                        if (string.IsNullOrEmpty(sourceEntity.SourceId.Level))
                        {
                            continue;
                        }

                        yield return new CommandAutoCompleteEntry
                        {
                            Name = sourceEntity.SourceId.Key,
                            Extra =
                                $"{Query.GetOriginalTypeName(entity.GetType())}  ({((Vector2)MotionField(entity.GetType(), "Position").Read(entity)).ToSimpleString(decimals: 0)})",
                            Prefix = queryPrefix,
                            Suffix = "].",
                            IsDone = false,
                        };
                    }

                    yield break;
                }

                // Search for key index (must exist since we validated it in CanEnumerateMemberEntries)
                int componentKeyIndex = memberIdx - 1;
                for (; componentKeyIndex >= 0; componentKeyIndex--)
                {
                    if (memberArgs[componentKeyIndex] == ComponentKey)
                    {
                        break;
                    }
                }

                var activeComponents = entityInstances
                    .SelectMany(entity =>
                        ((global::Celeste.Core.Components.ISourceOwnedComponentList)entity).OwnedComponents)
                    .Select(component => component.GetType())
                    .Distinct()
                    .ToArray();

                string[] componentQuery = memberArgs[(componentKeyIndex + 1)..];
                string componentQueryPrefix =
                    componentQuery.Length <= 1 ? string.Empty : $"{string.Join('.', componentQuery[..^1])}.";
                using var enumerator = Query.ResolveBaseTypeAutoCompleteEntries(componentQuery, componentQueryPrefix,
                    variant,
                    typeFilterPredicate: t => IsApplicableSourceType(t, "Monocle.Component"),
                    typeSuggestionPredicate: t => activeComponents.Contains(t));

                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current with
                    {
                        Prefix = componentQuery.Length <= 1 ? queryPrefix : $"{queryPrefix}.",
                        StorageKey =
                        $"{variant}_{Query.GetOriginalTypeFullName(type)}__{enumerator.Current.StorageKey}",
                    };
                }
            }
        }
    }
}
