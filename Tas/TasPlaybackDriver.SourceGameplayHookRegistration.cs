using Celeste.Input.Devices.Mouse;
using System;
using System.Reflection;
using Celeste.Scene.Scenes;
using Celeste.Simulation;
using Celeste.Simulation.Gameplay.Player.Configuration;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Gameplay;
using Celeste.Everest.Diagnostics.Logging;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        // This is the handle corresponding to the original StunPause On.BeforeUpdate
        // subscription, not an additional scene or simulation state.
        private Hook sourceStunPauseBeforeUpdateHook;

        internal void InstallSourceMouseStateOverride(SourceTasMouseProcess mouse)
        {
            MethodInfo target = typeof(SourceFnaMouse).GetMethod(nameof(SourceFnaMouse.GetState),
                                    BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null)
                                ?? throw new MissingMethodException(typeof(SourceFnaMouse).FullName,
                                    nameof(SourceFnaMouse.GetState));
            SourceHooks.IlHook(target, (cursor, _) =>
            {
                Instruction start = cursor.Next!;
                cursor.EmitDelegate(mouse.IsRunningTas);
                cursor.Emit(OpCodes.Brfalse, start);
                // The original static MInput.Mouse.CurrentState read is the same
                // actual process instance loan here; it remains branch-lazy.
                cursor.EmitDelegate(() => Process.MInput.Mouse.CurrentState);
                cursor.Emit(OpCodes.Ret);
            });
        }

        internal void InstallSourceBetterInvinciblePlayerDieHook(SourceTasBetterInvincibleProcess invincible)
        {
            MethodInfo target = typeof(RoomPlayerSession).GetMethod(nameof(RoomPlayerSession.DieSourceBody),
                                    BindingFlags.Public | BindingFlags.Instance, null,
                                    new[] { typeof(System.Numerics.Vector2), typeof(bool), typeof(bool) }, null)
                                ?? throw new MissingMethodException(typeof(RoomPlayerSession).FullName,
                                    nameof(RoomPlayerSession.DieSourceBody));
            SourceHooks.IlHook(target, il =>
            {
                var cursor = new ILCursor(il);
                // Original Assists.Invincible ldfld has become the same-save
                // IPlayerAssistSettings read in the actual player source producer.
                // This required readpoint must be present; no alternative match.
                if (cursor.TryGotoNext(MoveType.After,
                        ins => ins.MatchCallvirt<IPlayerAssistSettings>("get_Invincible")))
                    cursor.EmitDelegate(invincible.ModifyInvincible);
                else
                    LogSourceTas(global::Celeste.Everest.EverestLogLevel.Error,
                        "Failed to apply BetterInvincible hook!");
            });
        }

        internal void InstallSourceStunPauseBeforeUpdate(SourceTasStunPauseProcess stun)
        {
            MethodInfo target = typeof(SourceSceneState).GetMethod(nameof(SourceSceneState.BeforeUpdate),
                                    BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null)
                                ?? throw new MissingMethodException(typeof(SourceSceneState).FullName,
                                    nameof(SourceSceneState.BeforeUpdate));
            // The hook targets the complete public BeforeUpdate, whose original
            // callback includes the existing CycleHitbox wrapping and source clock.
            using (new DetourConfigContext(new DetourConfig("CelesteTAS", priority: int.MaxValue)).Use())
            {
                sourceStunPauseBeforeUpdateHook = new Hook(target,
                    (Action<Action<SourceSceneState>, SourceSceneState>)((original, scene) =>
                        stun.DoublePauses(original, scene)));
            }
        }

        internal void UninstallSourceStunPauseBeforeUpdate(SourceTasStunPauseProcess stun)
            => sourceStunPauseBeforeUpdateHook.Dispose();
    }
}
