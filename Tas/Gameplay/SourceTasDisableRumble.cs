using Celeste.Input.Devices.GamePads;
using System;
using System.Reflection;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Gameplay
{
    internal static class SourceTasDisableRumble
    {
        [SourceTasLoad]
        private static void Load()
        {
            var driver = CelesteTasModule.Instance.Driver;
            MethodInfo method = typeof(SourceGamePadData).GetMethod("Rumble",
                                    BindingFlags.Public | BindingFlags.Instance,
                                    null, new[] { typeof(float), typeof(float) }, null)
                                ?? throw new InvalidOperationException(
                                    "The actual MInput GamePadData.Rumble method is absent.");
            driver.SourceHooks.SkipMethod(method, () => driver.Running);
        }

        [SourceTasEnableRun]
        private static void EnableRun()
        {
            foreach (SourceGamePadData gamePadData in CelesteTasModule.Instance.Process.MInput.GamePads)
                gamePadData.StopRumble();
        }
    }
}
