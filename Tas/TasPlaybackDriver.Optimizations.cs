using MonoMod.Cil;
using Celeste.Mods.Tas.Reflection;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Gameplay;
using Celeste.Mods.Tas.Gameplay.Optimization;
using Celeste.Mods.Tas.EverestInterop;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasModHookProcess sourceModHooks;
        private SourceTasFastForwardOptimization sourceFastForwardOptimization;
        private SourceTasHeadlessOptimizations sourceHeadlessOptimizations;
        private SourceTasFastForwardBoost sourceFastForwardBoost;
        private SourceTasHideGameplay sourceHideGameplay;
        private SourceTasForceAllowAccessibilityTools sourceAccessibilityTools;

        private void InitializeSourceOptimizations()
        {
            var methods = new SourceTasOptimizationMethods(this);
            sourceModHooks = new SourceTasModHookProcess(this);
            sourceFastForwardOptimization = new SourceTasFastForwardOptimization(this, methods);
            sourceHeadlessOptimizations = new SourceTasHeadlessOptimizations(this);
            sourceFastForwardBoost = new SourceTasFastForwardBoost(this, methods);
            sourceHideGameplay = new SourceTasHideGameplay(this, methods);
            sourceAccessibilityTools = new SourceTasForceAllowAccessibilityTools(this, methods);
        }

        [SourceTasInitialize]
        private static void InitializeSourceModOnHooks() =>
            CelesteTasModule.Instance.Driver.sourceModHooks.InitializeOnHooks();

        [SourceTasInitialize]
        private static void InitializeSourceModILHooks() =>
            CelesteTasModule.Instance.Driver.sourceModHooks.InitializeILHooks();

        [SourceTasInitialize]
        private static void InitializeSourceFastForwardOptimization() =>
            CelesteTasModule.Instance.Driver.sourceFastForwardOptimization.Initialize();

        [SourceTasUnload]
        private static void UnloadSourceFastForwardOptimization() =>
            CelesteTasModule.Instance.Driver.sourceFastForwardOptimization.Unload();

        [SourceTasLoadContent]
        private static void LoadContentSourceHeadlessOptimizations() =>
            CelesteTasModule.Instance.Driver.sourceHeadlessOptimizations.LoadContent();

        [SourceTasLoad]
        private static void LoadSourceFastForwardBoost() =>
            CelesteTasModule.Instance.Driver.sourceFastForwardBoost.Load();

        [SourceTasUnload]
        private static void UnloadSourceFastForwardBoost() =>
            CelesteTasModule.Instance.Driver.sourceFastForwardBoost.Unload();

        [SourceTasLoad]
        private static void LoadSourceHideGameplay() => CelesteTasModule.Instance.Driver.sourceHideGameplay.Load();

        [SourceTasUnload]
        private static void UnloadSourceHideGameplay() => CelesteTasModule.Instance.Driver.sourceHideGameplay.Unload();

        [SourceTasLoad]
        private static void LoadSourceAccessibilityTools() =>
            CelesteTasModule.Instance.Driver.sourceAccessibilityTools.Load();

        [SourceTasUnload]
        private static void UnloadSourceAccessibilityTools() =>
            CelesteTasModule.Instance.Driver.sourceAccessibilityTools.Unload();

        [SourceTasInitialize]
        private static void InitializeSourceAccessibilityTools() =>
            CelesteTasModule.Instance.Driver.sourceAccessibilityTools.Initialize();

        [SourceTasModILHook("VivHelper", "Celeste.Mods.VivHelper.Runtime.DebugMaps.HideRoomInMapProcess", "ApplyRooms")]
        private static void SourceAccessibilityPreventHideRoomInDebugViv(ILCursor cursor) =>
            CelesteTasModule.Instance.Driver.sourceAccessibilityTools.PreventHideRoomInDebugViv(cursor);

        [SourceTasModILHook("KoseiHelper", "Celeste.Mod.KoseiHelper.Entities.DebugMapController", "MapEditorCtor")]
        private static void SourceAccessibilityPreventHideRoomInDebugKosei(ILCursor cursor) =>
            CelesteTasModule.Instance.Driver.sourceAccessibilityTools.PreventHideRoomInDebugKosei(cursor);

        [SourceTasModILHook("KoseiHelper", "Celeste.Mod.KoseiHelper.Entities.DebugMapController", "RenderKeys")]
        private static void SourceAccessibilityPreventHideKeysInDebug(ILCursor cursor) =>
            CelesteTasModule.Instance.Driver.sourceAccessibilityTools.PreventHideKeysInDebug(cursor);

        [SourceTasModILHook("KoseiHelper", "Celeste.Mod.KoseiHelper.Entities.DebugMapController", "RenderLevels")]
        private static void SourceAccessibilityPreventRoomAlteredInDebug(ILCursor cursor) =>
            CelesteTasModule.Instance.Driver.sourceAccessibilityTools.PreventRoomAlteredInDebug(cursor);

        [SourceTasModILHook("KoseiHelper", "Celeste.Mod.KoseiHelper.Triggers.RestartGameTrigger", "OnEnter")]
        private static void SourceAccessibilityPreventGameRestart(ILCursor cursor) =>
            CelesteTasModule.Instance.Driver.sourceAccessibilityTools.PreventGameRestart(cursor);

        [SourceTasModILHook("KoseiHelper", "Celeste.Mod.KoseiHelper.Triggers.SetWindowSizeTrigger", "OnStay")]
        private static void SourceAccessibilityPreventWindowResize(ILCursor cursor) =>
            CelesteTasModule.Instance.Driver.sourceAccessibilityTools.PreventWindowResize(cursor);

        [SourceTasModILHook("GameHelper", "Celeste.Mod.GameHelper.Triggers.CrashGameTrigger", "OnEnter")]
        private static void SourceAccessibilityPreventGameCrash(ILCursor cursor) =>
            CelesteTasModule.Instance.Driver.sourceAccessibilityTools.PreventGameCrash(cursor);

        [SourceTasModILHook("XaphanHelper", "Celeste.Mod.XaphanHelper.XaphanModuleSettings", "get_AllowDebug")]
        private static void SourceAccessibilityForceAllowDebug(ILCursor cursor) =>
            CelesteTasModule.Instance.Driver.sourceAccessibilityTools.ForceAllowDebug(cursor);
    }
}
