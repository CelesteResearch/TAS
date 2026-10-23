using Celeste.Input.Runtime;
using Celeste.Input.Devices.Mouse;
using System;
using Celeste.Core.Framework.Components;
using Celeste.Game.Execution;
using System.Reflection;
using Celeste.Game.Controls.Source;
using Celeste.Game.Execution.Threading;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private Hook sourceGameUpdateHook;
        private ILHook sourceUnfocusedGamePadsHook;
        private Hook sourceRunThreadHook;

        private void LoadSourceCoreMethodHooks()
        {
            if (sourceGameUpdateHook != null)
                throw new InvalidOperationException("The source TAS game update hook is already loaded.");
            using (new DetourConfigContext(new DetourConfig("CelesteTAS", priority: int.MaxValue)).Use())
            {
                SourceHooks.SkipMethod(
                    RequireCoreMethod(typeof(OriginalGameInputProcess), "UpdateGrab", Type.EmptyTypes),
                    IsPausedAfterInput);
                sourceUnfocusedGamePadsHook = new ILHook(
                    RequireCoreMethod(typeof(SourceMInputProcess), "Update", Type.EmptyTypes),
                    UpdateSourceUnfocusedGamePads);
                sourceRunThreadHook = new Hook(
                    RequireCoreMethod(typeof(OriginalGameThreadSession), "Start",
                        new[] { typeof(Action), typeof(string), typeof(bool) }),
                    (Action<Action<OriginalGameThreadSession, Action, string, bool>, OriginalGameThreadSession, Action,
                        string, bool>)StartSourceThread);
                sourceGameUpdateHook = new Hook(
                    RequireCoreMethod(typeof(GameRuntime), "Update", new[] { typeof(SourceGameTime) }),
                    (Action<Action<GameRuntime, SourceGameTime>, GameRuntime, SourceGameTime>)UpdateSourceGameFrame);
            }
        }

        private void UnloadSourceCoreMethodHooks()
        {
            sourceGameUpdateHook?.Dispose();
            sourceGameUpdateHook = null;
            sourceUnfocusedGamePadsHook.Dispose();
            sourceRunThreadHook.Dispose();
        }

        private void UpdateSourceUnfocusedGamePads(ILContext il)
        {
            var cursor = new ILCursor(il) { Index = il.Instrs.Count - 1 };
            if (cursor.TryGotoPrev(MoveType.After,
                    instruction => instruction.MatchCallvirt<SourceMouseData>("UpdateNull")))
                cursor.EmitDelegate((Action)(() =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (process.MInput.Active) process.MInput.GamePads[i].Update();
                        else process.MInput.GamePads[i].UpdateNull();
                    }
                }));
            if (cursor.TryGotoNext(MoveType.After, instruction => instruction.MatchLdcI4(0)))
                cursor.Emit(OpCodes.Ldc_I4_4).Emit(OpCodes.Add);
        }

        private void StartSourceThread(Action<OriginalGameThreadSession, Action, string, bool> original,
            OriginalGameThreadSession self, Action method, string name, bool highPriority)
        {
            if (Running && name != "USER_IO" && name != "MOD_IO")
            {
                var run = (Action<Action>)RequireCoreMethod(typeof(OriginalGameThreadSession),
                    "RunThreadWithLogging", new[] { typeof(Action) }).CreateDelegate(typeof(Action<Action>), self);
                run(method);
                return;
            }

            original(self, method, name, highPriority);
        }

        private static MethodInfo RequireCoreMethod(Type type, string name, Type[] parameters) =>
            type.GetMethod(name,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly,
                null, parameters, null) ??
            throw new InvalidOperationException("Actual playback hook method is absent: " + type.FullName + "." + name);
    }
}
