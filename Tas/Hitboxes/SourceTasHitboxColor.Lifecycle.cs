using Celeste.Core.Display;
using System;
using Celeste.Everest.Reflection;
using Celeste.Scene.Entities;
using Celeste.Graphics.Display;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;

namespace Celeste.Mods.Tas.Hitboxes
{
    public sealed partial class SourceTasHitboxColor
    {
        private ILHook entityDebugRenderHook;

        public void Load()
        {
            var method = global::Celeste.Mods.Tas.Reflection.SourceTasDeclaredHookTargets.Require(
                driver.Process,
                typeof(SceneEntityState), "DebugRender", new[] { "Monocle.Camera" });
            entityDebugRenderHook = new ILHook(method, EntityOnDebugRender);
        }

        public void Unload() => entityDebugRenderHook?.Dispose();

        private void EntityOnDebugRender(ILContext il)
        {
            ILCursor cursor = new(il);
            if (cursor.TryGotoNext(MoveType.After, instruction => instruction.MatchCall<PackedColor>("get_DarkRed")))
            {
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.EmitDelegate<Func<PackedColor, object, PackedColor>>(GetCustomColor);
            }

            if (cursor.TryGotoNext(MoveType.After, instruction => instruction.MatchCall<PackedColor>("get_Red")))
            {
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.EmitDelegate<Func<PackedColor, object, PackedColor>>(GetCustomColor);
            }
        }

        public void CmdChangeEntityHitboxColor(string color)
        {
            driver.Settings.EntityHitboxColor = HexToColor(color, DefaultEntityColor);
            driver.SourceModule.SaveSettings();
        }

        public void CmdChangeTriggerHitboxColor(string color)
        {
            driver.Settings.TriggerHitboxColor = HexToColor(color, DefaultTriggerColor);
            driver.SourceModule.SaveSettings();
        }

        public void CmdChangePlatformHitboxColor(string color)
        {
            driver.Settings.PlatformHitboxColor = HexToColor(color, DefaultPlatformColor);
            driver.SourceModule.SaveSettings();
        }
    }
}
