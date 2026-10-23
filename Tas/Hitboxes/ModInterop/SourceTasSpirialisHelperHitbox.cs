using System;
using Celeste.Core.Scheduling;
using Celeste.Scene.Scenes;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace Celeste.Mods.Tas.Hitboxes.ModInterop
{
    internal static class SourceTasSpirialisHelperHitbox
    {
        internal static void FixStopwatchRendering(ILCursor cursor, ISourceEngineContext engine,
            Func<bool> drawHitboxes)
        {
            const string rendererType = "Celeste.Mods.SpirialisHelper.Presentation.Time.TimeGameplayRenderer";
            cursor.GotoNext(MoveType.After, instruction => instruction.OpCode == OpCodes.Ldfld
                                                           && instruction.Operand is FieldReference field &&
                                                           field.Name == "RenderDebug"
                                                           && field.DeclaringType.FullName == rendererType);
            cursor.Emit(OpCodes.Ldarg_1);
            cursor.EmitDelegate<Func<bool, object, bool>>((original, scene) => original
                                                                               || ReferenceEquals(
                                                                                   ((SourceSceneState)scene)
                                                                                   .EngineContext, engine) &&
                                                                               drawHitboxes());
        }
    }
}
