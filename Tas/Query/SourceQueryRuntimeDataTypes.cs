using System;
using Celeste.Game.Settings;
using Celeste.Game.Save.Data;
using Celeste.Simulation.Runtime.Session;
using Celeste.Everest.Modules.Settings;
using Celeste.Everest.Modules.Persistence;

namespace Celeste.Mods.Tas.Query
{
    internal static class SourceQueryRuntimeDataTypes
    {
        internal static bool Contains(Type type) => type == typeof(SettingsState)
                                                    || type == typeof(OriginalSaveDataState) ||
                                                    type == typeof(FileAssists)
                                                    || type == typeof(ChapterCompletionSession)
                                                    || type ==
                                                    typeof(global::Celeste.Game.Execution.Areas.OriginalAreaKey)
                                                    || typeof(EverestModuleSettings).IsAssignableFrom(type)
                                                    || typeof(EverestModuleSession).IsAssignableFrom(type)
                                                    || typeof(EverestModuleSaveData).IsAssignableFrom(type);
    }
}
