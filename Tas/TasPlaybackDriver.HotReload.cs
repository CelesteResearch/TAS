using System;
using System.Reflection;
using System.Threading.Tasks;
using Celeste.Everest.Content.Reloading;
using Celeste.Mods.Tas.Module;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        [SourceTasLoad]
        private static void RestoreSourceStudioTasFilePath()
        {
            var driver = CelesteTasModule.Instance.Driver;
            // The original DEBUG-only DynamicData restore is excluded from the
            // release source. The non-silent reload stop remains active.
            MethodInfo target = typeof(EverestAssetReloadProcess).GetMethod("Do",
                                    BindingFlags.Public | BindingFlags.Instance, null,
                                    new[] { typeof(string), typeof(Func<bool, Task>), typeof(bool), typeof(bool) },
                                    null)
                                ?? throw new MissingMethodException(typeof(EverestAssetReloadProcess).FullName, "Do");
            driver.SourceHooks.IlHook(target, (cursor, _) =>
            {
                var start = cursor.MarkLabel();
                cursor.MoveBeforeLabels();
                // Original static argument 2 is argument 3 in the actual instance
                // producer: the same silent parameter, after the injected receiver.
                cursor.Emit(OpCodes.Ldarg_3);
                cursor.Emit(OpCodes.Brtrue, start);
                cursor.EmitDelegate(driver.DisableRun);
                cursor.EmitDelegate(driver.SourceSavestateManager.ClearAllSavestates);
            });
        }
    }
}
