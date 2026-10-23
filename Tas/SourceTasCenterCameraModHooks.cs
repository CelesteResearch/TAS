using System;
using MonoMod.Cil;

namespace Celeste.Mods.Tas
{
    internal static class SourceTasCenterCameraModHooks
    {
        internal static void DisableZoom(ILCursor cursor, Func<bool> disableZoom)
        {
            var start = cursor.MarkLabel();
            cursor.MoveBeforeLabels();
            cursor.EmitDelegate(disableZoom);
            cursor.EmitBrfalse(start);
            cursor.EmitLdarg1();
            cursor.EmitRet();
        }
    }
}
