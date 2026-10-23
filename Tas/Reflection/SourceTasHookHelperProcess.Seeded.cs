using System;
using System.Reflection;
using MonoMod.Cil;

namespace Celeste.Mods.Tas.Reflection
{
    internal sealed partial class SourceTasHookHelperProcess
    {
        internal void SeedMethod(MethodInfo method, Func<global::Celeste.Core.Randomness.ISourceRandom> random,
            global::Celeste.Core.Randomness.SourceRandomProcessState randomProcess)
        {
            if (method == null) return;
            IlHook(method, (cursor, _) =>
            {
                cursor.EmitDelegate(random);
                cursor.EmitDelegate<Action<global::Celeste.Core.Randomness.ISourceRandom>>(value =>
                    randomProcess.PushRandom(value ?? randomProcess.Random));
                while (cursor.TryGotoNext(MoveType.AfterLabel, instr => instr.MatchRet()))
                {
                    cursor.EmitDelegate<Action>(randomProcess.PopRandom);
                    cursor.Index += 1;
                }
            });
        }

        internal void SeedPositionMethod(MethodBase method, int actualPositionArgument,
            Action<System.Numerics.Vector2> pushRandom, Action popRandom)
        {
            IlHook(method, (cursor, _) =>
            {
                cursor.EmitLdarg(actualPositionArgument);
                cursor.EmitDelegate(pushRandom);
                while (cursor.TryGotoNext(MoveType.AfterLabel, instr => instr.MatchRet()))
                {
                    cursor.EmitDelegate(popRandom);
                    cursor.Index += 1;
                }
            });
        }

        internal void OverrideSeed(MethodInfo method, Func<bool> condition, Func<int> valueProvider)
        {
            if (method == null) return;
            IlHook(method, (cursor, _) =>
            {
                var start = cursor.MarkLabel();
                cursor.MoveBeforeLabels();
                cursor.EmitDelegate(condition);
                cursor.EmitBrfalse(start);
                cursor.EmitDelegate(valueProvider);
                cursor.EmitRet();
            });
        }
    }
}
