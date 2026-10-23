using Celeste.Core.Display;
#nullable enable annotations
using System;
using System.Reflection;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Celeste.Core.Geometry;
using Celeste.Core.Rendering;
using Celeste.Core.Reflection.Members;
using Celeste.Everest.Reflection;
using Celeste.Scene.Entities;
using Celeste.Scene.Following;
using Celeste.Physics.Collision.Shapes;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Celeste.Graphics.Rendering.Drawing;
using Celeste.Game.Execution.Levels;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Hitboxes
{
    public sealed partial class SourceTasHitboxSimplifiedProcess
    {
        private Hook? hitboxRenderHook;
        private Hook? gridRenderHook;
        private ILHook? hollowRectHook;
        private Hook? followerUpdateHook;
        private Hook? levelEndHook;

        [SourceTasInitialize]
        private static void SourceInitialize() => CelesteTasModule.Instance.Driver.SourceHitboxSimplified.Initialize();

        [SourceTasLoad]
        private static void SourceLoad() => CelesteTasModule.Instance.Driver.SourceHitboxSimplified.Load();

        [SourceTasUnload]
        private static void SourceUnload() => CelesteTasModule.Instance.Driver.SourceHitboxSimplified.Unload();

        private void Initialize()
        {
            foreach (Type type in driver.SourceModReflection.GetTypes())
                if (global::Celeste.Core.Reflection.Types.TypeReflectionIdentity.Resolve(Members, type).TypeIdentity is
                        { } fullName && UselessTypeNames.Contains(fullName))
                    UselessTypes.Add(type);
            var moon = driver.SourceModReflection.TryGetType("FemtoHelper", "CustomMoonCreature");
            if (moon != null)
            {
                var method = driver.SourceModReflection.GetMethodInfo(moon, "DebugRender");
                if (method != null)
                {
                    // Original SkipMethod is kept alive by the source HookHelper, not disposed by this Unload.
                    driver.SourceHooks.SkipMethod(
                        method.ActualMethod, (Func<bool>)IsSimplifiedHitboxes);
                }
            }
        }

        private void Load()
        {
            driver.SourceHooks.SkipMethod<SceneEntityState>(RequireMethod(typeof(SceneEntityState), "DebugRender",
                false,
                new[] { "Monocle.Camera" },
                typeof(ISourceRenderContext), typeof(object)), HideHitbox);
            hitboxRenderHook = new Hook(RequireMethod(typeof(HitboxColliderGeometry), "Render", false,
                    new[] { "Monocle.Camera", "Microsoft.Xna.Framework.Color" },
                    typeof(object), typeof(PackedColor), typeof(ISourceRenderContext)),
                (Action<Action<HitboxColliderGeometry, object, PackedColor, ISourceRenderContext>,
                    HitboxColliderGeometry,
                    object, PackedColor, ISourceRenderContext>)ModHitbox);
            gridRenderHook = new Hook(RequireMethod(typeof(GridColliderGeometry), "Render", false,
                    new[] { "Monocle.Camera", "Microsoft.Xna.Framework.Color" },
                    typeof(object), typeof(PackedColor), typeof(ISourceRenderContext)),
                (Action<Action<GridColliderGeometry, object, PackedColor, ISourceRenderContext>, GridColliderGeometry,
                    object, PackedColor, ISourceRenderContext>)CombineGridHitbox);
            hollowRectHook = new ILHook(RequireDrawHollowRect(), AvoidRedrawCorners);
            followerUpdateHook = new Hook(RequireMethod(typeof(FollowerState), "Update", false, Array.Empty<string>()),
                (Action<Action<FollowerState>, FollowerState>)FollowerOnUpdate);
            levelEndHook = new Hook(RequireMethod(typeof(OriginalLevelSceneRoot), "End", false, Array.Empty<string>()),
                (Action<Action<OriginalLevelSceneRoot>, OriginalLevelSceneRoot>)LevelOnEnd);
        }

        private void Unload()
        {
            hitboxRenderHook!.Dispose();
            gridRenderHook!.Dispose();
            hollowRectHook!.Dispose();
            followerUpdateHook!.Dispose();
            levelEndHook!.Dispose();
            // Entity skip hook belongs to HookHelper's original unload lifetime.
        }

        private MethodInfo RequireMethod(Type type, string name, bool isStatic,
            string[] originalParameters, params Type[] arguments)
        {
            var method = global::Celeste.Mods.Tas.Reflection.SourceTasDeclaredHookTargets.Require(
                driver.Process, type, name, originalParameters, isStatic);
            var parameters = method.GetParameters();
            if (method.IsStatic != isStatic || parameters.Length != arguments.Length)
                throw new InvalidOperationException("The genuine simplified hitbox target lost its physical ABI: " +
                                                    method);
            for (int index = 0; index < arguments.Length; index++)
                if (parameters[index].ParameterType != arguments[index])
                    throw new InvalidOperationException(
                        "The genuine simplified hitbox target has different physical parameters: " + method);
            return method;
        }

        private MethodInfo RequireDrawHollowRect()
        {
            var method = global::Celeste.Mods.Tas.Reflection.SourceTasDeclaredHookTargets.Require(
                driver.Process, typeof(SourceDrawState), "HollowRect",
                new[]
                {
                    "System.Single", "System.Single", "System.Single", "System.Single", "Microsoft.Xna.Framework.Color"
                },
                originalStatic: true);
            var parameters = method.GetParameters();
            if (method.IsStatic || method.DeclaringType != typeof(SourceDrawState) || parameters.Length != 5 ||
                parameters[0].ParameterType != typeof(float) || parameters[1].ParameterType != typeof(float) ||
                parameters[2].ParameterType != typeof(float) || parameters[3].ParameterType != typeof(float) ||
                parameters[4].ParameterType != typeof(PackedColor))
                throw new InvalidOperationException(
                    "Original Draw.HollowRect must bind to its sole actual same-owner full body.");
            return method;
        }

        private FieldInfo RequireField(Type type, string name)
        {
            var binding = Members.GetMember(type, name, SourceMemberKind.Field,
                              BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                              BindingFlags.Static,
                              true)
                          ?? throw new MissingFieldException(Members.GetOriginalTypeFullName(type), name);
            if (binding.ActualPath.Count == 0 ||
                binding.ActualPath[binding.ActualPath.Count - 1] is not FieldInfo field)
                throw new InvalidOperationException("HollowRect requires a qualified actual source field target.");
            return field;
        }

        private void AvoidRedrawCorners(ILContext il)
        {
            var rectangleY = RequireField(typeof(IntRect), "Y");
            var rectangleHeight = RequireField(typeof(IntRect), "Height");
            // Draw.rect's canonical migrated field, not a second local Rectangle value.
            var rectangle = RequireField(typeof(SourceDrawState), "rect");
            ILCursor cursor = new(il);
            if (!cursor.TryGotoNext(
                    instruction => instruction.MatchLdcI4(1),
                    instruction => instruction.OpCode == OpCodes.Sub,
                    instruction => instruction.OpCode == OpCodes.Sub,
                    instruction => instruction.MatchStfld(rectangleY.DeclaringType, rectangleY.Name))) return;
            ILCursor cloned = cursor.Clone();
            if (!cursor.TryGotoNext(MoveType.After,
                    instruction => instruction.MatchLdflda(rectangle.DeclaringType, rectangle.Name),
                    instruction => instruction.MatchLdarg(4),
                    instruction => instruction.OpCode == OpCodes.Conv_I4,
                    instruction => instruction.MatchStfld(rectangleHeight.DeclaringType, rectangleHeight.Name))) return;
            cloned.Remove().Emit(OpCodes.Ldc_I4_2);
            cursor.Index--;
            cursor.Emit(OpCodes.Ldc_I4_2).Emit(OpCodes.Sub);
        }

        private void FollowerOnUpdate(Action<FollowerState> orig, FollowerState self)
        {
            orig(self);
            if (self.Leader == null) Followers.Remove(self);
            else if (Followers.ContainsKey(self)) Followers[self] = true;
            else Followers[self] = false;
        }

        private void LevelOnEnd(Action<OriginalLevelSceneRoot> orig, OriginalLevelSceneRoot self)
        {
            orig(self);
            Followers.Clear();
        }
    }
}
