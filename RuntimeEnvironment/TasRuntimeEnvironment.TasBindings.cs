using System;
using System.Collections.Generic;
using Celeste.Game.Runtime;
using Celeste.Game.Controls.Source;
using Celeste.Input.Runtime;
using Celeste.Input.Bindings;
using Celeste.Everest.Modules.Runtime;
using Celeste.Mods.Tas.Input.Bindings;
using Celeste.Mods.Tas.Module;
using Celeste.Game.Settings;
using Celeste.Everest;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment
    {
        private SourceTasBindingProcess sourceTasBindings;
        private readonly SourceSpeedrunToolInputImports sourceSpeedrunToolInputImports = new();

        public SourceTasBindingProcess TasBindings => sourceTasBindings ??
                                                      throw new InvalidOperationException(
                                                          "The actual TAS binding process is not constructed.");

        private void ConstructSourceTasBindings()
        {
            if (sourceTasBindings != null)
                throw new InvalidOperationException("The actual TAS binding process is already constructed.");
            sourceTasBindings = new SourceTasBindingProcess(new TasBindingEnvironment(process, this));
        }

        private sealed class TasBindingEnvironment : ISourceTasBindingEnvironment
        {
            private readonly GameApplication process;
            private readonly TasRuntimeEnvironment owner;

            public TasBindingEnvironment(GameApplication process, TasRuntimeEnvironment owner)
            {
                this.process = process;
                this.owner = owner;
            }

            public SettingsState CurrentSettings => process.Settings;
            public OriginalGameInputProcess InputSource => process.InputSource;
            public EverestCoreSettings CoreSettings => process.CurrentCoreSettings;
            public SourceEverestModule CoreModule => process.CoreModule;
            public Type ActualTasSettingsType => typeof(CelesteTasSettings);
            public SourceSpeedrunToolInputImports SpeedrunToolInputImports => owner.sourceSpeedrunToolInputImports;
            public SourceMInputProcess MInput => process.MInput;
            public ISourceInputBindingFactory InputBindings => process.InputBindings;
            public List<SourceEverestModule> Modules => process.Modules;
        }
    }
}
