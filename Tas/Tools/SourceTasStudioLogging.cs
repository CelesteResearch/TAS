using System;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Tools
{
    internal static class SourceTasStudioLogging
    {
        internal static void Log(this string message,
            global::Celeste.Everest.EverestLogLevel level = global::Celeste.Everest.EverestLogLevel.Info)
            => ((IEverestLoggerProcessEnvironment)CelesteTasModule.Instance.Process).Logger.Log(level, "CelesteTAS",
                message);

        internal static void LogException(this Exception error, string message)
        {
            var logger = ((IEverestLoggerProcessEnvironment)CelesteTasModule.Instance.Process).Logger;
            logger.Log(global::Celeste.Everest.EverestLogLevel.Error, "CelesteTAS", message);
            logger.LogDetailed(error, "CelesteTAS");
        }
    }
}
