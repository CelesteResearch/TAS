using Celeste.Core.Display;
using Celeste.Mods.Tas.Input;
#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Types;
using Celeste.Everest.Reflection;
using System.Numerics;
using System.Diagnostics.CodeAnalysis;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Core.Components;
using Celeste.Core.Geometry;
using Celeste.Core.Rendering.States;
using Celeste.Core.Maps;
using Celeste.Scene.Entities;
using Celeste.Scene.Tracking;
using Celeste.Scene.Scheduling.ExecutionTags;
using Celeste.Game.Content.Maps.Entities;
using Celeste.Game.Execution.Areas;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Execution.Levels.Hooks;
using Celeste.Physics.Collision;
using Celeste.Physics.Collision.Shapes;
using Celeste.Simulation.Gameplay.Holdables;
using Celeste.Simulation.Presentation.Particles;
using Celeste.Simulation.Gameplay.Triggers.Foundation;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Celeste.Graphics.Rendering.Drawing;
using Celeste.Graphics.Rendering.Textures;
using Celeste.Mods.Tas.Query;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;
using Rectangle = Celeste.Core.Geometry.IntRect;
using EntityID = Celeste.Core.Maps.MapEntityId;

namespace Celeste.Mods.Tas.Information
{
    /// Displays information about specific entities inside the Info HUD
    internal sealed class SourceTasInfoWatchEntity
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;

        internal SourceTasInfoWatchEntity(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        private readonly record struct MemberKey(Type Type, bool DeclaredOnly);

        private readonly Dictionary<MemberKey, List<ReflectedMember>> CachedMemberInfos = new();
        private SourceTypeDefinition[]? simpleTypeDefinitions;
        private SourceMemberCatalog Members => ((ISourceEverestReflectionEnvironment)process).Reflection.SourceMembers;
        private SourceTasGameInfo GameInfo => driver.GameInfo;
        private SourceTasTargetQueryProcess TargetQuery => driver.TargetQuery;

        private record UniqueEntityId
        {
            public readonly EntityID EntityId;
            public readonly bool GlobalOrPersistent;
            public readonly Type Type;

            private UniqueEntityId(object entity, GameApplication process)
            {
                Type = entity.GetType();
                GlobalOrPersistent = (((ISceneEntityTags)entity).Tags.Bits &
                                      ((ISourceGlobalFrozenExecutionTagsEnvironment)process).GlobalFrozenExecutionTags
                                      .Global.Value) != 0
                                     || (((ISceneEntityTags)entity).Tags.Bits &
                                         ((ISourcePersistentTransitionExecutionTagsEnvironment)process)
                                         .PersistentTransitionExecutionTags.Persistent.Value) != 0
                                     || ((ISourceOwnedComponentList)entity).OwnedComponents.Get<HoldableState>() !=
                                     null;
                EntityId = ((ISourceEntitySourceData)entity).SourceId;
            }

            public static bool TryCreate(object entity, GameApplication process,
                [NotNullWhen(true)] out UniqueEntityId? uniqueId)
            {
                if (string.IsNullOrEmpty(((ISourceEntitySourceData)entity).SourceId.Level))
                {
                    uniqueId = null;
                    return false;
                }

                uniqueId = new(entity, process);
                return true;
            }
        }

        /// Called when an entity has been added to the watch list
        public event Action<object>? StartWatching;

        /// Called when an entity has been removed from the watch list
        public event Action<object>? StopWatching;

        /// Called when the watch list has been cleared
        public event Action? ClearWatching;

        private readonly WeakReference<object?> LastClickedEntity = new(null);

        // Store the entities which should be watched in the current level
        // Fallback to using weak references when a unique ID is not available
        private OriginalAreaKey currentAreaKey;
        internal List<WeakReference> WatchedEntities = [];
        internal List<WeakReference> WatchedEntities_Save = []; // Used for save-states

        private readonly HashSet<UniqueEntityId> WatchedEntityIds = [];

        /// Entities which are actively watched for the current frame
        internal readonly HashSet<object> CurrentlyWatchedEntities = [];

        public bool IsWatching(object entity)
        {
            return CurrentlyWatchedEntities.Contains(entity) ||
                   (UniqueEntityId.TryCreate(entity, process, out var uniqueId) && WatchedEntityIds.Contains(uniqueId));
        }

        internal void CheckMouseButtons()
        {
            if (driver.MouseInput.Right.Pressed)
            {
                ClearWatchEntities();
            }

            if (driver.MouseInput.Left.Pressed && !MouseOverHud() && FindClickedEntity() is { } entity)
            {
                AddOrRemoveWatching(entity);
                PrintAllSimpleValues(entity);
            }
        }

        private bool MouseOverHud()
        {
            var hudRect = new Rectangle(
                (int)driver.Settings.InfoPosition.X, (int)driver.Settings.InfoPosition.Y,
                (int)driver.Hud.Size.X, (int)driver.Hud.Size.Y);

            return hudRect.Contains((int)driver.MouseInput.Position.X, (int)driver.MouseInput.Position.Y);
        }

        /// Resolves the entity, which the mouse is currently over
        internal object? FindClickedEntity()
        {
            var clickedEntities = FindEntitiesAt(driver.MouseInput.Position)
                // Sort triggers after entities
                .Sort((a, b) => (a is SourceTriggerEntityState ? 1 : -1) - (b is SourceTriggerEntityState ? 1 : -1))
                .ToArray();

            object? clickedEntity;
            if (LastClickedEntity.TryGetTarget(out var lastClicked) &&
                Array.IndexOf(clickedEntities, lastClicked) is var index and >= 0)
            {
                // Cycle through when clicking multiple times
                clickedEntity = clickedEntities[(index + 1) % clickedEntities.Length];
            }
            else
            {
                clickedEntity = clickedEntities.FirstOrDefault();
            }

            LastClickedEntity.SetTarget(clickedEntity);
            return clickedEntity;
        }

        /// Resolves all entities which overlay with the screen position
        private IEnumerable<object> FindEntitiesAt(Vector2 screenPosition)
        {
            if (process.Frame.Scenes.CurrentScene is not Level level)
            {
                yield break;
            }

            var worldPosition = SourceTasLevelCoordinateConversion.MouseToWorld(level, driver, screenPosition);
            // Custom colliders are more likely to implement rectangles than points
            var checkRect = new Rectangle((int)worldPosition.X, (int)worldPosition.Y, 1, 1);

            foreach (var entity in level.SourceScene.Entities.Where(e => !IgnoreEntity(e)))
            {
                if (((ISourceEntityScene)entity).Scene == null)
                {
                    // Some cursed entity, which we don't want to deal with
                    continue;
                }

                if (((ISourceEntityCollider)entity).Collider == null)
                {
                    // Attempt to reconstruct collider from entity data
                    if (((ISourceEntitySourceData)entity).SourceData is OriginalLevelEntityData data)
                    {
                        ((ISourceEntityCollider)entity).Collider = new HitboxColliderGeometry(data.Width, data.Height);

                        if (SourceEntityCollision.CheckRect((ISourceEntityCollider)entity, checkRect))
                        {
                            yield return entity;
                        }

                        ((ISourceEntityCollider)entity).Collider = null;
                    }

                    continue;
                }

                if (SourceEntityCollision.CheckRect((ISourceEntityCollider)entity, checkRect))
                {
                    yield return entity;
                }
            }

            yield break;

            bool IgnoreEntity(object entity)
            {
                return entity.GetType() == typeof(SceneEntityState)
                       || entity is ParticleSystemPresentation;
            }
        }

        private void AddOrRemoveWatching(object clickedEntity)
        {
            currentAreaKey =
                ((IOriginalAreaKeyEnvironment)process).AreaKeys.GetSessionArea(
                    ((Level)((ISourceEntityScene)clickedEntity).Scene).Session);

            if (UniqueEntityId.TryCreate(clickedEntity, process, out var uniqueId))
            {
                if (!WatchedEntityIds.Add(uniqueId))
                {
                    StopWatching?.Invoke(clickedEntity);
                    WatchedEntityIds.Remove(uniqueId);
                }
                else
                {
                    StartWatching?.Invoke(clickedEntity);
                }
            }
            else
            {
                if (WatchedEntities.FirstOrDefault(reference => reference.Target == clickedEntity) is { } alreadyAdded)
                {
                    StopWatching?.Invoke(clickedEntity);
                    WatchedEntities.Remove(alreadyAdded);
                }
                else
                {
                    WatchedEntities.Add(new WeakReference(clickedEntity));
                    StartWatching?.Invoke(clickedEntity);
                }
            }

            GameInfo.Update();
        }

        internal void ClearWatchEntities()
        {
            LastClickedEntity.SetTarget(null);
            WatchedEntities.Clear();
            WatchedEntities_Save.Clear();
            WatchedEntityIds.Clear();
            CurrentlyWatchedEntities.Clear();
            GameInfo.Update();

            ClearWatching?.Invoke();
        }

        internal string GetInfo(WatchEntityType watchEntityType, string separator = "\n", bool alwaysUpdate = false,
            int? decimals = null)
        {
            CurrentlyWatchedEntities.Clear();
            if (process.Frame.Scenes.CurrentScene is not Level level || !driver.Settings.WatchEntity && !alwaysUpdate)
            {
                return string.Empty;
            }

            if (WatchedEntities.Count == 0 && WatchedEntityIds.Count == 0)
            {
                return string.Empty;
            }

            decimals ??= driver.Settings.CustomInfoDecimals;

            var allEntities =
                WatchedEntities
                    .Where(reference => reference.IsAlive)
                    .Select(reference => (object)reference.Target!)
                    .Concat(ResolveEntityIds(level).Values);

            return string.Join(separator,
                allEntities
                    .Select(entity =>
                    {
                        CurrentlyWatchedEntities.Add(entity);
                        return GetEntityValues(entity, watchEntityType, separator, decimals.Value);
                    }));
        }

        public bool ForceUpdateInfo = false; // for TasHelper.AutoWatchEntity

        internal void UpdateInfo(string separator = "\n", int? decimals = null)
        {
            CurrentlyWatchedEntities.Clear();
            GameInfo.HudWatchingInfo = string.Empty;
            GameInfo.StudioWatchingInfo = string.Empty;
            if (process.Frame.Scenes.CurrentScene is not Level level ||
                !driver.Settings.WatchEntity && !ForceUpdateInfo)
            {
                return;
            }

            if (WatchedEntities.Count == 0 && WatchedEntityIds.Count == 0)
            {
                return;
            }

            decimals ??= driver.Settings.CustomInfoDecimals;

            var allEntities =
                WatchedEntities
                    .Where(reference => reference.IsAlive)
                    .Select(reference => (object)reference.Target!)
                    .Concat(ResolveEntityIds(level).Values);

            CurrentlyWatchedEntities.AddRange(allEntities);

            if (!driver.Settings.HudWatchEntity)
            {
                GameInfo.HudWatchingInfo = string.Empty;
            }
            else
            {
                GameInfo.HudWatchingInfo = string.Join(separator,
                    allEntities
                        .Select(entity =>
                            GetEntityValues(entity, driver.Settings.InfoWatchEntityHudType, separator, decimals.Value))
                );
            }

            if (!driver.Settings.StudioWatchEntity || !driver.SourceStudioConnected)
            {
                GameInfo.StudioWatchingInfo = string.Empty;
            }
            else if (driver.Settings.InfoWatchEntityStudioType == driver.Settings.InfoWatchEntityHudType)
            {
                GameInfo.StudioWatchingInfo = GameInfo.HudWatchingInfo;
            }
            else
            {
                GameInfo.StudioWatchingInfo = string.Join(separator,
                    allEntities
                        .Select(entity => GetEntityValues(entity, driver.Settings.InfoWatchEntityStudioType, separator,
                            decimals.Value))
                );
            }
        }

        internal void Load()
        {
            ((global::Celeste.Core.Rendering.Diagnostics.ISourceEntityListDebugRenderHooksEnvironment)process)
                .EntityListDebugRenderHooks.AfterDebugRender += OnEntityListDebugRender;
            process.LevelSourceHooks.AfterBegin += OnLevelBegin;
            process.LevelSourceHooks.AfterEnd += OnLevelEnd;
            process.LevelSourceHooks.AfterLoadLevel += OnLevelLoadLevel;
        }

        internal void Unload()
        {
            ((global::Celeste.Core.Rendering.Diagnostics.ISourceEntityListDebugRenderHooksEnvironment)process)
                .EntityListDebugRenderHooks.AfterDebugRender -= OnEntityListDebugRender;
            process.LevelSourceHooks.AfterBegin -= OnLevelBegin;
            process.LevelSourceHooks.AfterEnd -= OnLevelEnd;
            process.LevelSourceHooks.AfterLoadLevel -= OnLevelLoadLevel;
        }

        private void OnEntityListDebugRender(object actualEntities, object actualCamera)
        {
            if (!driver.Settings.ShowHitboxes) return;
            var draw = ((ISourceDrawScope)process).Draw;
            foreach (var entity in (SceneEntityRegistry)actualEntities)
            {
                if (CurrentlyWatchedEntities.Contains(entity))
                {
                    var color = driver.Settings.EntityHitboxColor;
                    color = new PackedColor(255 - color.R, 255 - color.G, 255 - color.B, color.A);
                    draw.SpriteBatch.Draw((SourceTexture2DResource)draw.Pixel.Texture.Texture,
                        ((ISourceEntityPosition)entity).Position, draw.Pixel.ClipRect, color.PackedValue,
                        0f, Vector2.Zero, 1f, SourceSpriteEffects.None, 0f);
                }
            }
        }

        private void OnLevelBegin(Level self)
        {
            if (((IOriginalAreaKeyEnvironment)process).AreaKeys.GetSessionArea(self.Session) != currentAreaKey)
            {
                ClearWatchEntities();
            }
        }

        private void OnLevelEnd(Level self)
        {
            CurrentlyWatchedEntities.Clear();
        }

        private void OnLevelLoadLevel(Level self)
        {
            WatchedEntities.RemoveAll(reference =>
                !reference.IsAlive || reference.Target is ISourceEntityScene { Scene: null });
        }

        private void PrintAllSimpleValues(object entity)
        {
            driver.Logging.Log("Info of Clicked Entity:\n" + GetEntityValues(entity, WatchEntityType.All), string.Empty,
                driver.Settings.InfoWatchEntityLogToConsole);
        }

        /// Formats all member values into a multi-line string
        private string GetEntityValues(object entity, WatchEntityType watchEntityType, string separator = "\n",
            int decimals = 2)
        {
            if (watchEntityType == WatchEntityType.None)
            {
                return "";
            }

            var entityType = entity.GetType();

            string entityId = "";
            if (!string.IsNullOrEmpty(((ISourceEntitySourceData)entity).SourceId.Level))
            {
                entityId =
                    $"[{((ISourceEntitySourceData)entity).SourceId.Level}:{((ISourceEntitySourceData)entity).SourceId.ID}]";
            }

            string entityPrefix = $"{TargetQuery.GetOriginalTypeName(entityType)}{entityId}";
            string positionInfo = $"{entityPrefix}: {entity.ToSimplePositionString(decimals)}";

            if (watchEntityType == WatchEntityType.Position)
            {
                return positionInfo;
            }

            List<string> values = [positionInfo];
            values.AddRange(ResolveAllSimpleMembers(entityType, watchEntityType == WatchEntityType.DeclaredOnly)
                .Select(info =>
                {
                    object? value;
                    try
                    {
                        value = info.Read(entity);
                    }
                    catch
                    {
                        value = string.Empty;
                    }

                    if (value is float floatValue)
                    {
                        if (info.Name.EndsWith("Timer"))
                        {
                            value =
                                $"{GameInfo.FormatFrames(GameInfo.ToCeilingFrames(floatValue))}f ({floatValue.ToFormattedString(decimals)})";
                        }
                        else
                        {
                            value = floatValue.ToFormattedString(decimals);
                        }
                    }
                    else if (value is Vector2 vector2)
                    {
                        value = vector2.ToSimpleString(decimals);
                    }
                    else if (info.OriginalBinding != null && TargetQuery.TryFormatOriginalEnumValue(value,
                                 info.OriginalBinding.Definition.ValueTypeReference,
                                 out string originalEnumText))
                    {
                        value = originalEnumText;
                    }

                    if (separator == "\t" && value != null)
                    {
                        value = value.ToString()?.ReplaceLineBreak(" ");
                    }

                    return $"{entityPrefix}.{info.Name}: {value}";
                }));

            return string.Join(separator, values);
        }

        private IEnumerable<ReflectedMember> ResolveAllSimpleMembers(Type type, bool declaredOnly = false)
        {
            var key = new MemberKey(type, declaredOnly);
            if (CachedMemberInfos.TryGetValue(key, out var result))
            {
                return result;
            }

            CachedMemberInfos[key] = result = [];
            if (!Members.ContainsSourceType(type))
            {
                result.AddRange(ResolveRuntimeSimpleMembers(type, declaredOnly));
                return result;
            }

            var sourceType = Members.GetOriginalTypeDefinition(type);
            var definitions = ResolveInstanceMemberDefinitions(sourceType, declaredOnly).ToArray();
            List<SourcePhysicalMemberBinding> memberInfos = [];
            memberInfos.AddRange(definitions.Where(info => info.Kind == SourceMemberKind.Field
                                                           && IsSimpleType(info.ValueTypeReference) &&
                                                           !info.Name.EndsWith("k__BackingField"))
                .Select(info => Members.RequireBinding(type, info)));
            memberInfos.AddRange(definitions.Where(info => info.Kind == SourceMemberKind.Property
                                                           && IsSimpleType(info.ValueTypeReference))
                .Select(info => Members.RequireBinding(type, info)));

            foreach (var grouping in memberInfos.GroupBy(info =>
                         info.Definition.AssemblyIdentity == sourceType.AssemblyIdentity
                         && info.Definition.DeclaringTypeIdentity == sourceType.Identity))
            {
                var infos = grouping.OrderBy(info => info,
                    Comparer<SourcePhysicalMemberBinding>.Create((a, b) =>
                        string.Compare(a.Name, b.Name, StringComparison.InvariantCultureIgnoreCase)));
                if (grouping.Key)
                {
                    result.InsertRange(0, infos.Select(info => new ReflectedMember(info)));
                }
                else
                {
                    result.AddRange(infos.Select(info => new ReflectedMember(info)));
                }
            }

            return result;
        }

        private IEnumerable<ReflectedMember> ResolveRuntimeSimpleMembers(Type type, bool declaredOnly)
        {
            var members = new List<ReflectedMember>();
            for (var current = type; current != null && current != typeof(object); current = current.BaseType)
            {
                if (Members.ContainsSourceType(current))
                {
                    foreach (var definition in ResolveInstanceMemberDefinitions(
                                 Members.GetOriginalTypeDefinition(current), declaredOnly))
                        if (IsSimpleType(definition.ValueTypeReference)
                            && (definition.Kind != SourceMemberKind.Field ||
                                !definition.Name.EndsWith("k__BackingField")))
                            members.Add(new ReflectedMember(Members.RequireBinding(current, definition)));
                    break;
                }

                const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                           BindingFlags.DeclaredOnly;
                members.AddRange(current.GetFields(flags)
                    .Where(field => IsRuntimeSimpleType(field.FieldType) && !field.Name.EndsWith("k__BackingField"))
                    .Select(field => new ReflectedMember(field)));
                members.AddRange(current.GetProperties(flags)
                    .Where(property => property.CanRead && property.GetIndexParameters().Length == 0
                                                        && IsRuntimeSimpleType(property.PropertyType))
                    .Select(property => new ReflectedMember(property)));
                if (declaredOnly) break;
            }

            return members.OrderBy(member => member.DeclaringType == type ? 0 : 1)
                .ThenBy(member => member.Name, StringComparer.InvariantCultureIgnoreCase);
        }

        private static bool IsRuntimeSimpleType(Type type) => type.IsEnum || type == typeof(bool)
                                                                          || type == typeof(byte) ||
                                                                          type == typeof(sbyte) || type == typeof(char)
                                                                          || type == typeof(short) ||
                                                                          type == typeof(ushort) ||
                                                                          type == typeof(int) || type == typeof(uint)
                                                                          || type == typeof(long) ||
                                                                          type == typeof(ulong) ||
                                                                          type == typeof(IntPtr) ||
                                                                          type == typeof(UIntPtr)
                                                                          || type == typeof(float) ||
                                                                          type == typeof(double) ||
                                                                          type == typeof(string)
                                                                          || type == typeof(decimal) ||
                                                                          type == typeof(Vector2);

        private IEnumerable<SourceMemberDefinition> ResolveInstanceMemberDefinitions(
            SourceTypeDefinition sourceType, bool declaredOnly)
        {
            // GetAllFieldInfos/GetAllPropertyInfos stop before System.Object and interfaces.
            // DeclaredOnly instead uses the original type's own reflection declarations.
            do
            {
                if (!declaredOnly && (sourceType.BaseTypeIdentity == null
                                      || ((TypeAttributes)sourceType.Attributes & TypeAttributes.Interface) != 0))
                {
                    yield break;
                }

                foreach (var definition in Members.GetOriginalMemberDefinitions(
                             sourceType.AssemblyIdentity, sourceType.Identity, true))
                {
                    if (!definition.IsStatic)
                    {
                        yield return definition;
                    }
                }

                if (declaredOnly || sourceType.BaseTypeIdentity == null)
                {
                    yield break;
                }

                sourceType = Members.GetOriginalTypeDefinition(
                    sourceType.BaseAssemblyIdentity, sourceType.BaseTypeIdentity);
            } while (true);
        }

        private bool IsSimpleType(SourceTypeReference sourceType)
        {
            while (sourceType.Kind is SourceTypeReferenceKind.RequiredModifier
                   or SourceTypeReferenceKind.OptionalModifier)
            {
                sourceType = sourceType.ElementType;
            }

            if (sourceType.Kind == SourceTypeReferenceKind.GenericInstance)
            {
                // A nested enum in a closed generic declaring type is still Type.IsEnum.
                return Members.ResolveNamedTypeDefinition(sourceType.ElementType).EnumUnderlyingTypeIdentity != null;
            }

            if (sourceType.Kind != SourceTypeReferenceKind.Named)
            {
                return false;
            }

            var definition = Members.ResolveNamedTypeDefinition(sourceType);
            if (definition.EnumUnderlyingTypeIdentity != null)
            {
                return true;
            }

            if (simpleTypeDefinitions == null)
            {
                Type[] simpleTypes =
                [
                    typeof(bool), typeof(byte), typeof(sbyte), typeof(char),
                    typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong),
                    typeof(IntPtr), typeof(UIntPtr), typeof(float), typeof(double), typeof(string),
                    typeof(decimal), typeof(Vector2)
                ];
                simpleTypeDefinitions = simpleTypes.Select(type =>
                {
                    var qualifiedType = Members.GetOriginalTypeDefinition(type);
                    return Members.GetOriginalTypeDefinition(qualifiedType.AssemblyIdentity, qualifiedType.Identity);
                }).ToArray();
            }

            foreach (var simpleType in simpleTypeDefinitions)
            {
                if (definition.AssemblyIdentity == simpleType.AssemblyIdentity &&
                    definition.Identity == simpleType.Identity)
                {
                    return true;
                }
            }

            return false;
        }

        /// Tries to resolve all entity instances for the currently watched entity IDs
        private Dictionary<UniqueEntityId, object> ResolveEntityIds(Level level)
        {
            Dictionary<UniqueEntityId, object> result = new();
            string currentRoom = level.Session.Level;

            var possibleTypes = WatchedEntityIds
                .Where(id => id.GlobalOrPersistent || id.EntityId.Level == currentRoom)
                .Select(id => id.Type)
                .ToHashSet();

            if (possibleTypes.IsEmpty())
            {
                return result;
            }

            List<object> possibleEntities = [];
            if (possibleTypes.All(type =>
                    process.SceneCatalog.SourceTrackerInitialization.TrackedEntityTypes.TryGetValue(type,
                        out var targetTypes) && targetTypes.Contains(type)))
            {
                foreach (var type in possibleTypes)
                {
                    possibleEntities.AddRange(level.SourceScene.Entities.GetTrackedEntities(
                        (process.SceneCatalog.Find(type) ??
                         throw new InvalidOperationException(
                             "The watched actual type has no source tracker registration."))
                        .Name));
                }
            }
            else
            {
                possibleEntities.AddRange(
                    level.SourceScene.Entities.Where(entity => possibleTypes.Contains(entity.GetType())));
            }

            foreach (var entity in possibleEntities)
            {
                if (!UniqueEntityId.TryCreate(entity, process, out var uniqueId))
                {
                    continue;
                }

                if (!WatchedEntityIds.Contains(uniqueId) || !result.TryAdd(uniqueId, entity))
                {
                    continue;
                }

                if (result.Count == WatchedEntityIds.Count)
                {
                    return result;
                }
            }

            return result;
        }
    }
}
