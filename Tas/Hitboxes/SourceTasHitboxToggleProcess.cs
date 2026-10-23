using System;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Celeste.Core.Reflection.Members;
using Celeste.Everest.Reflection;
using Celeste.Game.Presentation.Rendering;
using Celeste.Game.Presentation.Effects.Distortion;
using Celeste.Game.Presentation.Effects.Glitch;
using Celeste.Game.Execution.Graphics;
using Celeste.Scene.Scheduling;
using Celeste.Graphics.Rendering.Effects;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Hitboxes
{
    public sealed class SourceTasHitboxToggleProcess
    {
        private readonly TasPlaybackDriver driver;
        private bool origDrawHitboxes;
        public bool DrawHitboxes => origDrawHitboxes || driver.Settings.ShowHitboxes || !driver.Settings.ShowGameplay;
        private ILHook? gameplayRendererHook;
        private ILHook? distortHook;
        private ILHook? glitchHook;

        public SourceTasHitboxToggleProcess(TasPlaybackDriver driver)
            => this.driver = driver ?? throw new ArgumentNullException(nameof(driver));

        [SourceTasLoad]
        private static void SourceLoad() => CelesteTasModule.Instance.Driver.SourceHitboxToggle.Load();

        [SourceTasUnload]
        private static void SourceUnload() => CelesteTasModule.Instance.Driver.SourceHitboxToggle.Unload();

        private SourceMemberCatalog Members =>
            ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMembers;

        private MethodInfo RequireSourceMethod(Type type, string name)
            => ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMethods
                .GetMethodInfo(Members, type, name, null,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static |
                    BindingFlags.DeclaredOnly)
                ?.ActualMethod ?? throw new MissingMethodException(Members.GetOriginalTypeFullName(type), name);

        private void Load()
        {
            gameplayRendererHook = new ILHook(RequireSourceMethod(typeof(GameplayRendererState), "Render"),
                GameplayRendererOnRender);
            distortHook = new ILHook(RequireSourceMethod(typeof(OriginalDistortionRender), "Render"), DistortOnRender);
            glitchHook = new ILHook(RequireSourceMethod(typeof(OriginalGlitchRender), "Apply"), GlitchOnApply);
        }

        private void Unload()
        {
            gameplayRendererHook!.Dispose();
            distortHook!.Dispose();
            glitchHook!.Dispose();
        }

        private void GameplayRendererOnRender(ILContext il)
        {
            ILCursor cursor = new(il);
            // The shared Render contains the selected original complete boolean expression.
            // The original IL patch combines RenderDebug and Commands.Open before this same branch.
            if (!cursor.TryGotoNext(MoveType.Before, instruction => instruction.Operand is MethodReference method
                                                                    && method.DeclaringType.FullName ==
                                                                    typeof(SceneUpdateScheduler).FullName
                                                                    && method.Name == "DebugRenderEntities")
                || !cursor.TryGotoPrev(MoveType.Before,
                    instruction => instruction.OpCode == OpCodes.Brfalse || instruction.OpCode == OpCodes.Brfalse_S))
                throw new InvalidOperationException(
                    "The qualified gameplay renderer is missing its genuine debug-render branch.");
            cursor.EmitDelegate<Func<bool, bool>>(IsShowHitbox);
        }

        private bool IsShowHitbox(bool orig)
        {
            origDrawHitboxes = orig;
            return DrawHitboxes && !driver.SourceOffscreenHitbox.ShouldDraw;
        }

        private void DistortOnRender(ILContext il)
        {
            var binding = Members.GetFieldInfo(typeof(OriginalGameGraphicsResources), "FxDistort",
                BindingFlags.Public | BindingFlags.Static);
            if (binding == null || binding.ActualPath.Count != 1 || binding.ActualPath[0] is not FieldInfo field
                || field.IsStatic || field.FieldType != typeof(SourceEffect))
                throw new InvalidOperationException(
                    "The original GFX.FxDistort requires its genuine canonical graphics-resource field binding.");
            ILCursor cursor = new(il);
            if (!cursor.TryGotoNext(MoveType.After, instruction => instruction.OpCode == OpCodes.Ldfld
                                                                   && instruction.Operand is FieldReference reference &&
                                                                   reference.Name == field.Name
                                                                   && reference.DeclaringType.FullName ==
                                                                   field.DeclaringType.FullName))
                throw new InvalidOperationException(
                    "The qualified distortion renderer does not read its genuine FxDistort field.");
            cursor.EmitDelegate<Func<SourceEffect, SourceEffect>>(DisableDistortWhenShowHitbox);
        }

        private SourceEffect DisableDistortWhenShowHitbox(SourceEffect effect)
            => driver.Settings.ShowHitboxes ? null : effect;

        private void GlitchOnApply(ILContext il)
        {
            ILCursor cursor = new(il);
            Instruction start = cursor.Next;
            cursor.EmitDelegate<Func<bool>>(IsShowHitbox);
            cursor.Emit(OpCodes.Brfalse, start).Emit(OpCodes.Ret);
        }

        private bool IsShowHitbox() => driver.Settings.ShowHitboxes;
    }
}
