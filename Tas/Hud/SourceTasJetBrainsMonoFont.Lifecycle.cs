using System;
using System.Collections;
using System.Reflection;
using MonoMod.RuntimeDetour;
using Celeste.Core.Scheduling;
using Celeste.Game.Menus.Overworld;
using Celeste.Everest.UI.Progress;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Hud
{
    public sealed partial class SourceTasJetBrainsMonoFont
    {
        private Hook gotoRoutineHook;

        [SourceTasLoad]
        private static void SourceLoad() => CelesteTasModule.Instance.Driver.HudFont.Load();

        [SourceTasUnload]
        private static void SourceUnload() => CelesteTasModule.Instance.Driver.HudFont.UnloadSourceFontHooks();

        private void Load()
        {
            var method = typeof(OverworldSequence).GetMethod(nameof(OverworldSequence.GotoRoutine),
                             BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly,
                             null, new[] { typeof(OuiState) }, null)
                         ?? throw new InvalidOperationException("The source Overworld.GotoRoutine is absent.");
            if (method.DeclaringType != typeof(OverworldSequence) || method.IsStatic
                                                                  || method.ReturnType != typeof(IEnumerator) ||
                                                                  method.ContainsGenericParameters ||
                                                                  method.GetMethodBody() == null)
                throw new InvalidOperationException(
                    "The actual Overworld.GotoRoutine lost its complete declared transition body.");
            gotoRoutineHook = new Hook(method,
                (Func<Func<OverworldSequence, OuiState, IEnumerator>, OverworldSequence, OuiState, IEnumerator>)
                OverworldOnGotoRoutine);
        }

        private void UnloadSourceFontHooks()
        {
            gotoRoutineHook.Dispose();
            Unload();
        }

        private IEnumerator OverworldOnGotoRoutine(Func<OverworldSequence, OuiState, IEnumerator> original,
            OverworldSequence self, OuiState next)
        {
            if (next is EverestLoggedProgressState)
                BeforeLoggedProgressGoto();
            yield return new SourceSwapImmediately(original(self, next));
        }
    }
}
