using System;
using Celeste.Everest.Menus;
using Celeste.Mods.Tas;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : IEverestMenuStartupHooksEnvironment
    {
        private Func<Func<bool>, bool> sourceTasOobeStart;
        private Func<Func<bool>, Func<bool>, bool> sourceTasTitleStart;

        internal void InstallSourceTasMenuStartupHooks()
        {
            sourceTasOobeStart = SkipSourceOobeStart;
            sourceTasTitleStart = UseOriginalSourceTitleStart;
        }

        internal void UninstallSourceTasMenuStartupHooks()
        {
            sourceTasOobeStart = null;
            sourceTasTitleStart = null;
        }

        private bool SkipSourceOobeStart(Func<bool> original) => false;
        private bool UseOriginalSourceTitleStart(Func<bool> installed, Func<bool> original) => original();

        bool IEverestMenuStartupHooksEnvironment.InvokeSourceOobeStart(Func<bool> original)
        {
            var hook = sourceTasOobeStart;
            return hook != null ? hook(original) : original();
        }

        bool IEverestMenuStartupHooksEnvironment.InvokeSourceTitleStart(Func<bool> installed, Func<bool> original)
        {
            var hook = sourceTasTitleStart;
            return hook != null ? hook(installed, original) : installed();
        }
    }
}
