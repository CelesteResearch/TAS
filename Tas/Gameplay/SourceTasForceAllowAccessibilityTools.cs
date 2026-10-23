using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Celeste.Everest.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Mods.Tas.Gameplay.Optimization;
using StudioCommunication;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;

namespace Celeste.Mods.Tas.Gameplay
{
    internal sealed partial class SourceTasForceAllowAccessibilityTools
    {
        private readonly TasPlaybackDriver driver;
        private readonly SourceTasOptimizationMethods methods;
        private ILHook update;
        private readonly HashSet<(string SID, int Mode)> beatenMaps = new();

        internal SourceTasForceAllowAccessibilityTools(TasPlaybackDriver driver, SourceTasOptimizationMethods methods)
        {
            this.driver = driver;
            this.methods = methods;
        }

        internal bool Enabled => driver.Settings.Enabled && driver.Settings.ForceAllowAccessibilityTools switch
        {
            StudioEnableCondition.Never => false,
            StudioEnableCondition.Always or StudioEnableCondition.ForCurrentSession => true,
            StudioEnableCondition.WhileStudioConnected => driver.SourceStudioConnected,
            StudioEnableCondition.AfterCasualPlaythrough => HasBeatenMap,
            _ => throw new ArgumentOutOfRangeException()
        };

        private bool EnabledRTA => Enabled && !driver.Running;

        private bool HasBeatenMap
        {
            get
            {
                var process = driver.Process;
                var session = SourceTasSceneSessionQuery.GetSession(process.Frame.Scenes.CurrentScene);
                var save = process.SaveContext.CurrentSave;
                if (session == null || save == null) return false;
                if (save.DebugMode) return true;
                var area = process.AreaKeys.GetSessionArea(session);
                var key = (process.AreaKeys.GetSID(area), area.Mode);
                if (beatenMaps.Contains(key)) return true;
                bool completed = save.Areas[area.ID].Modes[area.Mode].Completed;
                if (completed) beatenMaps.Add(key);
                return completed;
            }
        }

        internal void Load() => update = new ILHook(
            methods.Require("Celeste.Celeste", "Update", "Microsoft.Xna.Framework.GameTime"), il =>
            {
                var cursor = new ILCursor(il);
                // Original On.Update prefix changes only the actual Commands field,
                // then always executes the complete original Update body.
                cursor.EmitDelegate<Action>(() =>
                {
                    if (Enabled) driver.SourceCommands.Enabled = true;
                });
            });

        internal void Unload() => update.Dispose();

        internal void Initialize()
        {
            var type = driver.SourceModReflection.TryGetType("KoseiHelper",
                "Celeste.Mod.KoseiHelper.Entities.CustomPauseController");
            if (type == null) return;
            var reflection = ((ISourceEverestReflectionEnvironment)driver.Process).Reflection;
            var original = reflection.SourceMembers.GetOriginalTypeDefinition(type);
            var ctor = reflection.SourceMethods
                .EnumerateDeclaredDefinitions(original.AssemblyIdentity, original.Identity)
                .Single(d =>
                    d.Name == ".ctor" && d.ParameterTypeIdentities.SequenceEqual(new[]
                        { "Celeste.EntityData", "Microsoft.Xna.Framework.Vector2" }));
            var actual =
                reflection.SourceMethods.RequireActualMethodBase(original.AssemblyIdentity, ctor.MethodDefinitionToken);
            driver.SourceHooks.HookAfter<object>(actual, controller =>
            {
                if (EnabledRTA)
                {
                    driver.SourceModReflection.GetFieldInfo(controller.GetType(), "canPause").Write(controller, true);
                    driver.SourceModReflection.GetFieldInfo(controller.GetType(), "canSaveAndQuit")
                        .Write(controller, true);
                }
            });
        }

        // Called by the original ModILHook collector at its original phase.
        // This table records original declaration attributes, not another registry.
        internal void PreventHideRoomInDebugViv(ILCursor cursor)
        {
            cursor.Index = 0;
            var start = cursor.MarkLabel();
            cursor.MoveBeforeLabels();
            cursor.EmitDelegate(() => Enabled);
            cursor.Emit(OpCodes.Brfalse, start);
            cursor.Emit(OpCodes.Ret);
        }

        internal void PreventHideRoomInDebugKosei(ILCursor cursor)
        {
            cursor.GotoNext(i => i.MatchRet());
            var ret = cursor.Next;
            cursor.Index = 0;
            cursor.GotoNext(MoveType.After,
                i => CallsForeignOriginalDelegate(i, "On.Celeste.Editor.MapEditor/orig_ctor"));
            cursor.EmitDelegate(() => Enabled);
            cursor.Emit(OpCodes.Brtrue, ret);
        }

        internal void PreventHideKeysInDebug(ILCursor cursor)
        {
            var start = cursor.MarkLabel();
            cursor.MoveBeforeLabels();
            cursor.EmitDelegate(() => Enabled);
            cursor.Emit(OpCodes.Brfalse, start);
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.Emit(OpCodes.Ldarg_1);
            var invoke = FindOriginalDelegateInvoke(cursor.Context, "On.Celeste.Editor.MapEditor/orig_RenderKeys");
            cursor.Emit(OpCodes.Call, invoke);
            cursor.Emit(OpCodes.Ret);
        }

        internal void PreventRoomAlteredInDebug(ILCursor cursor)
        {
            ILLabel jump = null;
            cursor.GotoNext(i => i.MatchBrfalse(out jump));
            cursor.Index = 0;
            cursor.MoveBeforeLabels();
            cursor.EmitDelegate(() => Enabled);
            cursor.Emit(OpCodes.Brtrue, jump);
        }

        internal void PreventGameRestart(ILCursor cursor)
        {
            cursor.GotoNext(MoveType.After, i => CallsOriginalTrigger(i, "OnEnter"));
            var start = cursor.MarkLabel();
            cursor.MoveBeforeLabels();
            cursor.EmitDelegate(() => Enabled);
            cursor.Emit(OpCodes.Brfalse, start);
            cursor.EmitDelegate<Action>(() => driver.AbortSourceTas("Prevented 'Restart Game Trigger'"));
            cursor.Emit(OpCodes.Ret);
        }

        internal void PreventWindowResize(ILCursor cursor)
        {
            cursor.GotoNext(MoveType.After, i => CallsOriginalTrigger(i, "OnStay"));
            var start = cursor.MarkLabel();
            cursor.MoveBeforeLabels();
            cursor.EmitDelegate(() => Enabled);
            cursor.Emit(OpCodes.Brfalse, start);
            cursor.Emit(OpCodes.Ret);
        }

        internal void PreventGameCrash(ILCursor cursor)
        {
            var start = cursor.MarkLabel();
            cursor.MoveBeforeLabels();
            cursor.EmitDelegate(() => Enabled);
            cursor.Emit(OpCodes.Brfalse, start);
            cursor.EmitDelegate<Action>(() => driver.AbortSourceTas("Prevented 'Crash Trigger'"));
            cursor.Emit(OpCodes.Ret);
        }

        internal void ForceAllowDebug(ILCursor cursor)
        {
            cursor.GotoNext(i => i.MatchRet());
            cursor.EmitDelegate(() => Enabled);
            cursor.Emit(OpCodes.Or);
        }

        private bool CallsOriginalTrigger(Instruction instruction, string name)
        {
            var original = methods.Require("Celeste.Trigger", name, "Celeste.Player");
            return methods.Calls(instruction, original) || instruction.MatchCall("Celeste.Trigger", name);
        }

        private static bool CallsForeignOriginalDelegate(Instruction instruction, string type)
            => instruction.OpCode == OpCodes.Callvirt && instruction.Operand is MethodReference reference &&
               reference.DeclaringType.FullName == type && reference.Name == "Invoke";

        private static MethodReference FindOriginalDelegateInvoke(ILContext il, string type)
        {
            foreach (var instruction in il.Body.Instructions)
                if (CallsForeignOriginalDelegate(instruction, type))
                    return (MethodReference)instruction.Operand;
            throw new MissingMethodException(type, "Invoke");
        }
    }
}
