using Celeste.Core.Display;
#nullable enable
using System;
using Celeste.Everest.Diagnostics.Logging;
using LogLevel = Celeste.Everest.EverestLogLevel;
using Celeste.Graphics.Display;
using Color = Celeste.Core.Display.PackedColor;


namespace Celeste.Mods.Tas.Diagnostics
{
    internal sealed class SourceTasLoggingProcess
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;

        internal SourceTasLoggingProcess(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        private global::Celeste.Everest.Diagnostics.Logging.EverestLoggerProcess Logger =>
            ((IEverestLoggerProcessEnvironment)process).Logger;

        public const string Tag = "CelesteTAS";

#if DEBUG
    public void DebugLog(object text, LogLevel logLevel = LogLevel.Debug) => DebugLog(text, true, logLevel);
    public void DebugLog(object text, bool outputToCommands, LogLevel logLevel = LogLevel.Debug) =>
        Log(text, string.Empty, outputToCommands, logLevel);
#endif

        public void LogException(Exception e) => LogException(e, false);

        public void LogException(Exception e, bool outputToCommands)
        {
            Logger.LogDetailed(e, Tag);

            if (outputToCommands)
            {
                driver.SourceCommands.Log(e.Message, Color.Yellow);
                driver.SourceCommands.LogStackTrace(e.StackTrace);
            }
        }

        public void LogException(Exception e, string header, LogLevel logLevel = LogLevel.Error) =>
            LogException(e, header, string.Empty, false, logLevel);

        public void LogException(Exception e, string header, bool outputToCommands,
            LogLevel logLevel = LogLevel.Error) =>
            LogException(e, header, string.Empty, outputToCommands, logLevel);

        public void LogException(Exception e, string header, string category, LogLevel logLevel = LogLevel.Error) =>
            LogException(e, header, category, false, logLevel);

        public void LogException(Exception e, string header, string category, bool outputToCommands,
            LogLevel logLevel = LogLevel.Error)
        {
            Log(header, category, outputToCommands, logLevel);
            Logger.LogDetailed(e, Tag);

            if (outputToCommands)
            {
                driver.SourceCommands.Log(e.Message, Color.Yellow);
                driver.SourceCommands.LogStackTrace(e.StackTrace);
            }
        }

        public void Log(object? text, LogLevel logLevel = LogLevel.Info) => Log(text, string.Empty, false, logLevel);

        public void Log(object? text, string category, LogLevel logLevel = LogLevel.Info) =>
            Log(text, category, false, logLevel);

        public void Log(object? text, string category, bool outputToCommands, LogLevel logLevel = LogLevel.Info)
        {
            string tag = category == string.Empty
                ? Tag
                : $"{Tag}/{category}";

            string textStr = text?.ToString() ?? "null";
            Logger.Log(logLevel, tag, textStr);

            if (outputToCommands)
            {
                ConsoleLog($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{tag}] {logLevel}: {textStr}", logLevel);
            }
        }

        public void ConsoleLog(object? text, LogLevel logLevel = LogLevel.Verbose)
        {
            var color = logLevel switch
            {
                LogLevel.Warn => Color.Yellow,
                LogLevel.Error => Color.Red,
                _ => Color.Cyan
            };

            try
            {
                driver.SourceCommands?.Log(text?.ToString() ?? "null", color);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
