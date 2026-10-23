using Celeste.Core.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Celeste.Core.Components;
using Celeste.Core.Rendering;
using Celeste.Physics.Collision;
using Celeste.Physics.Collision.Shapes;
using Celeste.Game.Execution.Levels;
using Celeste.Simulation;
using Celeste.Simulation.Gameplay.Player.Collision.Components;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using StudioCommunication;

namespace Celeste.Mods.Tas.Hitboxes
{
    public sealed partial class SourceTasActualCollideHitbox
    {
        private readonly TasPlaybackDriver driver;
        private static readonly PackedColor ActualPlayerHitboxColor = Invert(PackedColor.Red);
        private static readonly PackedColor ActualPlayerHurtboxColor = Invert(PackedColor.Lime);
        private readonly Dictionary<object, Vector2> LastPositions = new();
        private readonly Dictionary<object, bool> LastCollidables = new();
        private readonly Dictionary<Type, Func<object, bool>> CollidableHandlers = new();
        private bool playerUpdated;
        private bool colliderListRendering;

        internal SourceTasActualCollideHitbox(TasPlaybackDriver driver) => this.driver = driver;

        public bool Disabled => !driver.Settings.ShowHitboxes
                                || driver.Settings.ShowActualCollideHitboxes == ActualCollideHitboxType.Off
                                || driver.FastForwarding;

        private static PackedColor Invert(PackedColor color)
            => new PackedColor(255 - color.R, 255 - color.G, 255 - color.B, color.A);

        public bool IsCollidable(object entity)
        {
            Type entityType = entity.GetType();
            if (CollidableHandlers.TryGetValue(entityType, out var sameClassHandler))
                return sameClassHandler(entity);
            foreach (var entry in CollidableHandlers)
                if (entry.Key.IsAssignableFrom(entityType))
                    return entry.Value(entity);
            return ((ISourceEntityCollider)entity).Collidable;
        }

        public void Clear()
        {
            playerUpdated = false;
            LastPositions.Clear();
            LastCollidables.Clear();
        }

        public Vector2? LoadActualCollidePosition(object entity)
            => LastPositions.TryGetValue(entity, out Vector2 result) ? result : null;

        public bool? LoadActualCollidable(object entity)
            => LastCollidables.TryGetValue(entity, out bool result) ? result : null;

        public void StoreActualColliderState(object entity)
        {
            if (entity == null || playerUpdated || Disabled) return;
            LastPositions[entity] = ((ISourceEntityCollider)entity).Position;
            LastCollidables[entity] = IsCollidable(entity);
        }

        [Obsolete("Use StoreActualColliderState instead")]
        public void StoreCollider(object entity) => StoreActualColliderState(entity);

        private void OnPlayerDebugRender(Action orig, RoomPlayerSession player, ISourceRenderContext context)
        {
            if (Disabled || player.Scene is OriginalLevelSceneRoot { Transitioning: true }
                         || LoadActualCollidePosition(player) is not { } actualCollidePosition
                         || player.Position == actualCollidePosition)
            {
                orig();
                return;
            }

            if (driver.Settings.ShowActualCollideHitboxes == ActualCollideHitboxType.Override)
            {
                DrawActualPlayerHitbox(player, actualCollidePosition, context);
                return;
            }

            orig();
            if (driver.Settings.ShowActualCollideHitboxes == ActualCollideHitboxType.Append)
                DrawActualPlayerHitbox(player, actualCollidePosition, context);
        }

        private void DrawActualCollider(ColliderGeometry self, PackedColor color, Action<PackedColor> invokeOrig)
        {
            var entity = self.Entity;
            if (entity == null || entity is RoomPlayerSession || Disabled
                || colliderListRendering && self is not ColliderListGeometry
                || LoadActualCollidePosition(entity) is not { } actualCollidePosition
                || entity.Position == actualCollidePosition)
            {
                invokeOrig(color);
                return;
            }

            var actualColliderColor = driver.Settings.ShowActualCollideHitboxes == ActualCollideHitboxType.Append
                ? Invert(color)
                : color;
            if (driver.Settings.ShowActualCollideHitboxes == ActualCollideHitboxType.Append)
                invokeOrig(color);
            var currentPosition = entity.Position;
            var playerColliders = ((ISourceOwnedComponentList)entity).OwnedComponents
                .GetAll<PlayerColliderComponentState>().ToArray();
            if (playerColliders.All(playerCollider => playerCollider.Collider != null))
            {
                if (playerColliders.Any(playerCollider => playerCollider.Collider == self))
                {
                    ((ISourceMutableEntityPosition)entity).Position = actualCollidePosition;
                    invokeOrig(actualColliderColor);
                    ((ISourceMutableEntityPosition)entity).Position = currentPosition;
                }
                else invokeOrig(color);
            }
            else
            {
                ((ISourceMutableEntityPosition)entity).Position = actualCollidePosition;
                invokeOrig(actualColliderColor);
                ((ISourceMutableEntityPosition)entity).Position = currentPosition;
            }
        }

        private void DrawActualPlayerHitbox(RoomPlayerSession player, Vector2 hitboxPosition,
            ISourceRenderContext context)
        {
            var origPosition = player.Position;
            var origCollider = player.Collider;
            player.Position = hitboxPosition;
            SourcePrimitiveDrawing.HollowRect(context, player, origCollider.AbsoluteLeft,
                origCollider.AbsoluteTop, origCollider.Width, origCollider.Height,
                driver.Settings.ShowActualCollideHitboxes == ActualCollideHitboxType.Append
                    ? ActualPlayerHitboxColor
                    : PackedColor.Red);
            player.Collider = player.Hurtbox;
            SourcePrimitiveDrawing.HollowRect(context, player, player.Hurtbox.AbsoluteLeft,
                player.Hurtbox.AbsoluteTop, player.Hurtbox.Width, player.Hurtbox.Height,
                driver.Settings.ShowActualCollideHitboxes == ActualCollideHitboxType.Append
                    ? ActualPlayerHurtboxColor
                    : PackedColor.Lime);
            player.Collider = origCollider;
            player.Position = origPosition;
        }
    }
}
