using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Celeste.Mods.Tas.Gameplay.Optimization;

namespace Celeste.Mods.Tas.EverestInterop
{
    internal sealed class SourceTasFastForwardBoost
    {
        private readonly TasPlaybackDriver driver;
        private readonly SourceTasOptimizationMethods methods;
        private ILHook update;

        internal SourceTasFastForwardBoost(TasPlaybackDriver driver, SourceTasOptimizationMethods methods)
        {
            this.driver = driver;
            this.methods = methods;
        }

        internal void Load() => update = new ILHook(methods.Require("Celeste.LightningRenderer", "Update"), il =>
        {
            var cursor = new ILCursor(il);
            var start = cursor.Next;
            cursor.EmitDelegate(() => driver.FastForwarding && driver.Process.Frame.FrameCounter % 30 > 0);
            cursor.Emit(OpCodes.Brfalse, start);
            cursor.Emit(OpCodes.Ret);
        });

        internal void Unload() => update.Dispose();
    }
}
