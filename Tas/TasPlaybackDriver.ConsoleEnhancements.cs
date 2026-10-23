using Celeste.Mods.Tas.Interop;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasConsoleEnhancements sourceConsoleEnhancements;

        internal SourceTasConsoleEnhancements ConsoleEnhancements =>
            sourceConsoleEnhancements ??= new SourceTasConsoleEnhancements(process, this);

        private SourceTasDebugRcPage sourceDebugRcPage;
        internal SourceTasDebugRcPage DebugRcPages => sourceDebugRcPage ??= new SourceTasDebugRcPage(this);

        [SourceTasInitialize(SourceTasConsoleEnhancements.InitializePriority)]
        private static void InitializeSourceConsoleEnhancements() =>
            CelesteTasModule.Instance.Driver.ConsoleEnhancements.InitializeHelperMethods();

        [SourceTasLoad]
        private static void LoadSourceConsoleEnhancements() =>
            CelesteTasModule.Instance.Driver.ConsoleEnhancements.Load();

        [SourceTasUnload]
        private static void UnloadSourceConsoleEnhancements() =>
            CelesteTasModule.Instance.Driver.ConsoleEnhancements.Unload();

        [SourceTasEnableRun]
        private static void EnableRunSourceConsoleEnhancements() =>
            CelesteTasModule.Instance.Driver.ConsoleEnhancements.EnableRun();

        [SourceTasUpdateMeta]
        private static void UpdateMetaSourceConsoleEnhancements() =>
            CelesteTasModule.Instance.Driver.ConsoleEnhancements.UpdateMeta();

        [SourceTasLoad]
        private static void LoadSourceDebugRcPages() => CelesteTasModule.Instance.Driver.DebugRcPages.Load();

        [SourceTasUnload]
        private static void UnloadSourceDebugRcPages() => CelesteTasModule.Instance.Driver.DebugRcPages.Unload();
    }
}
