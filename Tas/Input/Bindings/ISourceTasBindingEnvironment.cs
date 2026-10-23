using Celeste.Input.Runtime;
using Celeste.Input.Bindings;
using System;
using Celeste.Game.Controls.Source;
using Celeste.Game.Settings;
using Celeste.Everest;
using Celeste.Everest.Modules.Runtime;

namespace Celeste.Mods.Tas.Input.Bindings
{
    public interface ISourceTasBindingEnvironment : ISourceMInputEnvironment, ISourceInputBindingEnvironment,
        ISourceEverestModulesEnvironment
    {
        SettingsState CurrentSettings { get; }
        OriginalGameInputProcess InputSource { get; }
        EverestCoreSettings CoreSettings { get; }
        SourceEverestModule CoreModule { get; }
        Type ActualTasSettingsType { get; }
        SourceSpeedrunToolInputImports SpeedrunToolInputImports { get; }
    }
}
