using Celeste.Core.Display;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Numerics;
using Celeste.Core.Rendering;
using Celeste.Core.Components;
using Celeste.Core.Mathematics;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Celeste.Physics.Collision.Shapes;
using Celeste.Scene.Entities;
using Celeste.Scene.Tracking;
using Celeste.Scene.Scenes;
using Celeste.Simulation.Gameplay.Player.Intro;
using MonoMod.RuntimeDetour;
using MonoMod.Cil;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;
using Puffer = Celeste.Game.Gameplay.Hazards.Puffer.OriginalPufferState;
using Seeker = Celeste.Game.Gameplay.Enemies.Seekers.OriginalSeekerState;
using PlayerCollider = Celeste.Simulation.Gameplay.Player.Collision.Components.PlayerColliderComponentState;
using SoundSource = Celeste.Simulation.Audio.Sources.SoundSourceComponentState;

namespace Celeste.Mods.Tas.Hitboxes
{
    internal sealed partial class SourceTasHitboxOptimized
    {
        private Hook entityHook;
        private readonly List<IDisposable> loadHooks = new();
        private const BindingFlags InstanceAny = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private static MethodInfo Require(Type type, string name, params Type[] args) =>
            type.GetMethod(name, InstanceAny | BindingFlags.Static, null, args, null) ??
            throw new MissingMethodException(type.FullName, name);

        private delegate void EntityDebugOriginal(object entity, ISourceRenderContext context, object camera);

        private delegate void EntityDebugHook(EntityDebugOriginal orig, object entity, ISourceRenderContext context,
            object camera);

        private delegate void EntityListDebugOriginal(SceneEntityRegistry self, ISourceRenderContext context,
            object camera,
            object scene);

        private delegate void EntityListDebugHook(EntityListDebugOriginal orig, SceneEntityRegistry self,
            ISourceRenderContext context, object camera, object scene);

        private delegate void PlayerColliderDebugOriginal(PlayerCollider self, ISourceRenderContext context,
            object camera);

        private delegate void PlayerColliderDebugHook(PlayerColliderDebugOriginal orig, PlayerCollider self,
            ISourceRenderContext context, object camera);

        private delegate void CircleRenderOriginal(CircleColliderGeometry self, object camera, PackedColor color,
            ISourceRenderContext context);

        private delegate void CircleRenderHook(CircleRenderOriginal orig, CircleColliderGeometry self, object camera,
            PackedColor color, ISourceRenderContext context);

        private delegate void SoundDebugOriginal(SoundSource self, ISourceRenderContext context, object camera);

        private delegate void SoundDebugHook(SoundDebugOriginal orig, SoundSource self, ISourceRenderContext context,
            object camera);

        private delegate void SeekerDebugOriginal(Seeker self, ISourceRenderContext context, object camera);

        private delegate void SeekerDebugHook(SeekerDebugOriginal orig, Seeker self, ISourceRenderContext context,
            object camera);

        private delegate void LevelLoadOriginal(Level self, PlayerIntroType intro, bool fromLoader);

        private delegate void LevelLoadHook(LevelLoadOriginal orig, Level self, PlayerIntroType intro, bool fromLoader);

        private delegate void SceneBeforeUpdateOriginal(SourceSceneState self);

        private delegate void SceneBeforeUpdateHook(SceneBeforeUpdateOriginal orig, SourceSceneState self);

        internal void Initialize()
        {
            var wonderland = driver.SourceModReflection.TryGetType("Madeline in Wonderland",
                "Celeste.Mod.TomorrowHelper.TomorrowHelperModule");
            var wonderlandMethod = wonderland == null
                ? null
                : driver.SourceModReflection.TryGetMethod("Madeline in Wonderland",
                    "Celeste.Mod.TomorrowHelper.TomorrowHelperModule", "ModDebugRender");
            if (wonderlandMethod != null)
                driver.SourceHooks.IlHook(wonderlandMethod.ActualMethod, (cursor, _) =>
                {
                    if (cursor.TryGotoNext(MoveType.After, ins => ins.OpCode == OpCodes.Callvirt))
                    {
                        var next = cursor.Next!;
                        cursor.EmitDelegate((Func<bool>)IsShowHitboxes);
                        cursor.Emit(OpCodes.Brfalse, next);
                        cursor.Emit(OpCodes.Ret);
                    }
                });
            driver.SourceHooks.HookBefore<Puffer>(Require(typeof(Puffer), "Explode"),
                self => pufferPushRadius.Add(new CircleColliderGeometry(Arithmetic, 40f, self.X, self.Y)));
            driver.SourceHooks.IlHook(Require(typeof(Puffer), nameof(Puffer.Render), typeof(ISourceRenderContext)),
                (cursor, il) =>
                {
                    if (cursor.TryGotoNext(i => i.MatchLdloc(out _), i => i.MatchLdcI4(28)))
                    {
                        cursor.Index++;
                        cursor.EmitDelegate((Func<int, int>)HidePufferWhiteLine);
                    }
                });
            var customType = driver.SourceModReflection.TryGetType("CrystallineHelper", "vitmod.CustomPuffer");
            if (customType != null)
            {
                var explode =
                    driver.SourceModReflection.TryGetMethod("CrystallineHelper", "vitmod.CustomPuffer", "Explode");
                if (explode != null)
                    driver.SourceHooks.HookBefore<object>(explode.ActualMethod,
                        self => pufferPushRadius.Add(new CircleColliderGeometry(Arithmetic,
                            Field<CircleColliderGeometry>(self, "pushRadius").Radius, Position(self).X,
                            Position(self).Y)));
            }

            using (new DetourConfigContext(new DetourConfig("CelesteTAS", priority: int.MaxValue)).Use())
                entityHook =
                    new Hook(
                        Require(typeof(SourceEntityDebugRendering), nameof(SourceEntityDebugRendering.Render),
                            typeof(object), typeof(ISourceRenderContext), typeof(object)),
                        (EntityDebugHook)ModDebugRender);
        }

        internal void Load()
        {
            loadHooks.Add(new Hook(
                Require(typeof(SceneEntityRegistry), nameof(SceneEntityRegistry.DebugRender),
                    typeof(ISourceRenderContext),
                    typeof(object), typeof(object)), (EntityListDebugHook)EntityListOnDebugRender));
            loadHooks.Add(new ILHook(
                Require(typeof(PlayerCollider), nameof(PlayerCollider.DebugRender), typeof(ISourceRenderContext),
                    typeof(object)), PlayerColliderOnDebugRender));
            loadHooks.Add(new Hook(
                Require(typeof(PlayerCollider), nameof(PlayerCollider.DebugRender), typeof(ISourceRenderContext),
                    typeof(object)), (PlayerColliderDebugHook)AddFeatherHitbox));
            loadHooks.Add(new Hook(
                Require(typeof(CircleColliderGeometry), nameof(CircleColliderGeometry.Render), typeof(object),
                    typeof(PackedColor), typeof(ISourceRenderContext)), (CircleRenderHook)CircleOnRender));
            loadHooks.Add(new Hook(
                Require(typeof(SoundSource), nameof(SoundSource.DebugRender), typeof(ISourceRenderContext),
                    typeof(object)),
                (SoundDebugHook)SoundSource_DebugRender));
            loadHooks.Add(new ILHook(
                Require(typeof(Seeker), nameof(Seeker.DebugRender), typeof(ISourceRenderContext), typeof(object)),
                SeekerOnDebugRender));
            loadHooks.Add(new Hook(
                Require(typeof(Seeker), nameof(Seeker.DebugRender), typeof(ISourceRenderContext), typeof(object)),
                (SeekerDebugHook)SeekerOnDebugRender));
            loadHooks.Add(new Hook(
                Require(typeof(Level), nameof(Level.LoadLevel), typeof(PlayerIntroType), typeof(bool)),
                (LevelLoadHook)LevelOnLoadLevel));
            loadHooks.Add(new Hook(Require(typeof(SourceSceneState), nameof(SourceSceneState.BeforeUpdate)),
                (SceneBeforeUpdateHook)SceneOnBeforeUpdate));
        }

        internal void Unload()
        {
            entityHook.Dispose();
            foreach (var hook in loadHooks) hook.Dispose();
            loadHooks.Clear();
        }

        private void PlayerColliderOnDebugRender(ILContext il)
        {
            var cursor = new ILCursor(il);
            if (cursor.TryGotoNext(MoveType.After, ins => ins.MatchCall<PackedColor>("get_HotPink")))
            {
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.EmitDelegate((Func<PackedColor, SourceComponentState, PackedColor>)OptimizePlayerColliderHitbox);
            }
        }

        private void SeekerOnDebugRender(ILContext il)
        {
            var cursor = new ILCursor(il);
            // The complete actual Seeker source uses new PackedColor(red/aqua) rather than the original static getters.
            // These are the two original color birth points immediately before the same collider Render calls.
            if (cursor.TryGotoNext(MoveType.After,
                    ins => ins.OpCode == OpCodes.Newobj && ins.Operand is MethodReference m &&
                           m.DeclaringType.FullName == typeof(PackedColor).FullName))
            {
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.EmitDelegate((Func<PackedColor, object, PackedColor>)OptimizeSeekerHitbox1);
            }

            if (cursor.TryGotoNext(MoveType.After,
                    ins => ins.OpCode == OpCodes.Newobj && ins.Operand is MethodReference m &&
                           m.DeclaringType.FullName == typeof(PackedColor).FullName))
            {
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.EmitDelegate((Func<PackedColor, object, PackedColor>)OptimizeSeekerHitbox2);
            }
        }
    }
}
