using System;
using System.Reflection;
using StudioCommunication;
using Celeste.Core.Reflection.Methods;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Input;
using Celeste.Mods.Tas.Gameplay;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasOwnedInputMethodBindings
    {
        private static readonly Type[] CommandParameters =
            { typeof(CommandLine), typeof(int), typeof(string), typeof(int) };

        internal static void BindStaticLifecycle(SourceAssemblyMethodCatalog methods)
        {
            Bind(methods, 100664579, typeof(TasPlaybackDriver), "SourceCenterExCameraEnable", null,
                new[]
                {
                    typeof(TasPlaybackDriver).GetNestedType("SourceExCameraEnableOriginal", BindingFlags.NonPublic)
                });
            Bind(methods, 100665117, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasHitboxSimplifiedProcess),
                "SourceInitialize", null, Type.EmptyTypes);
            Bind(methods, 100665119, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasHitboxSimplifiedProcess),
                "SourceLoad", null, Type.EmptyTypes);
            Bind(methods, 100665120, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasHitboxSimplifiedProcess),
                "SourceUnload", null, Type.EmptyTypes);
            Bind(methods, 100665060, typeof(TasPlaybackDriver), "InitializeSourceConquerorBeamHitbox", null,
                Type.EmptyTypes);
            Bind(methods, 100665061, typeof(TasPlaybackDriver), "UnloadSourceConquerorBeamHitbox", null,
                Type.EmptyTypes);
            Bind(methods, 100665080, typeof(TasPlaybackDriver), "LoadSourceNpcHitbox", null, Type.EmptyTypes);
            Bind(methods, 100665081, typeof(TasPlaybackDriver), "UnloadSourceNpcHitbox", null, Type.EmptyTypes);
            Bind(methods, 100664648, typeof(TasPlaybackDriver), "LoadSourceFinalBossHitbox", null, Type.EmptyTypes);
            Bind(methods, 100664649, typeof(TasPlaybackDriver), "UnloadSourceFinalBossHitbox", null, Type.EmptyTypes);
            Bind(methods, 100664685, typeof(TasPlaybackDriver), "LoadSourceSkinModFix", null, Type.EmptyTypes);
            Bind(methods, 100664686, typeof(TasPlaybackDriver), "UnloadSourceSkinModFix", null, Type.EmptyTypes);
            Bind(methods, 100664687, typeof(TasPlaybackDriver), "InitializeSourceSkinModFix", null, Type.EmptyTypes);
            Bind(methods, 100664703, typeof(TasPlaybackDriver), "ApplySourceSkinModFix", null, Type.EmptyTypes);
            Bind(methods, 100664704, typeof(TasPlaybackDriver), "RestoreSourceSkinModFix", null, Type.EmptyTypes);
            Bind(methods, 100665051, typeof(TasRuntimeEnvironment), "LoadSourceTasHitboxColor",
                null,
                Type.EmptyTypes);
            Bind(methods, 100665052, typeof(TasRuntimeEnvironment), "UnloadSourceTasHitboxColor",
                null, Type.EmptyTypes);
            Bind(methods, 100665114, typeof(TasRuntimeEnvironment),
                "LoadSourceTasHitboxRoomBoundary", null, Type.EmptyTypes);
            Bind(methods, 100665115, typeof(TasRuntimeEnvironment),
                "UnloadSourceTasHitboxRoomBoundary", null, Type.EmptyTypes);
            Bind(methods, 100665138, typeof(TasRuntimeEnvironment),
                "InitializeSourceTasHitboxTriggerSpikes", null, Type.EmptyTypes);
            Bind(methods, 100665139, typeof(TasRuntimeEnvironment),
                "LoadSourceTasHitboxTriggerSpikes", null, Type.EmptyTypes);
            Bind(methods, 100665140, typeof(TasRuntimeEnvironment),
                "UnloadSourceTasHitboxTriggerSpikes", null, Type.EmptyTypes);
            Bind(methods, 100664631, typeof(TasRuntimeEnvironment),
                "InitializeSourceTasActualCollideHitbox", null, Type.EmptyTypes);
            Bind(methods, 100664632, typeof(TasRuntimeEnvironment),
                "LoadSourceTasActualCollideHitbox", null, Type.EmptyTypes);
            Bind(methods, 100664633, typeof(TasRuntimeEnvironment),
                "UnloadSourceTasActualCollideHitbox", null, Type.EmptyTypes);
            Bind(methods, 100664667, typeof(TasRuntimeEnvironment),
                "InitializeSourceTasTriggerHitbox", null, Type.EmptyTypes);
            Bind(methods, 100664662, typeof(TasRuntimeEnvironment), "LoadSourceTasTriggerHitbox",
                null, Type.EmptyTypes);
            Bind(methods, 100664664, typeof(TasRuntimeEnvironment),
                "LoadStateSourceTasTriggerHitbox", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100664663, typeof(TasRuntimeEnvironment),
                "UnloadSourceTasTriggerHitbox",
                null, Type.EmptyTypes);
            Bind(methods, 100665088, typeof(TasPlaybackDriver), "InitializeSourceHitboxOptimized", null,
                Type.EmptyTypes);
            Bind(methods, 100665089, typeof(TasPlaybackDriver), "LoadSourceHitboxOptimized", null, Type.EmptyTypes);
            Bind(methods, 100665090, typeof(TasPlaybackDriver), "UnloadSourceHitboxOptimized", null, Type.EmptyTypes);
            Bind(methods, 100665130, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasHitboxToggleProcess),
                "SourceLoad", null, Type.EmptyTypes);
            Bind(methods, 100665131, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasHitboxToggleProcess),
                "SourceUnload", null, Type.EmptyTypes);
            Bind(methods, 100665026, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasCycleHitboxColorProcess),
                "SourceLoad", null, Type.EmptyTypes);
            Bind(methods, 100665027, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasCycleHitboxColorProcess),
                "SourceUnload", null, Type.EmptyTypes);
            Bind(methods, 100665028, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasCycleHitboxColorProcess),
                "SourceInitialize", null, Type.EmptyTypes);
            Bind(methods, 100665065, typeof(TasPlaybackDriver), "LoadSourceHitboxFixer", null, Type.EmptyTypes);
            Bind(methods, 100665066, typeof(TasPlaybackDriver), "UnloadSourceHitboxFixer", null, Type.EmptyTypes);
            Bind(methods, 100664654, typeof(TasPlaybackDriver), "LoadSourceOffscreenHitbox", null, Type.EmptyTypes);
            Bind(methods, 100664325, typeof(TasRuntimeEnvironment),
                "InitializeSourceTasCollabUtils2", null, Type.EmptyTypes);
            Bind(methods, 100664671, typeof(global::Celeste.Mods.Tas.Hitboxes.ModInterop.SourceTasMelvinHitbox),
                "SourceInitialize", null, Type.EmptyTypes);
            Bind(methods, 100664611, typeof(TasPlaybackDriver), "InitializeSourceSimplifiedGraphics", null,
                Type.EmptyTypes);
            Bind(methods, 100664877, typeof(TasPlaybackDriver), "InitializeSourceSimplifiedGraphicsFeature", null,
                Type.EmptyTypes);
            Bind(methods, 100664878, typeof(TasPlaybackDriver), "UnloadSourceSimplifiedGraphicsFeature", null,
                Type.EmptyTypes);
            Bind(methods, 100664049, typeof(TasPlaybackDriver), "LoadSourcePlaybackHooks", null, Type.EmptyTypes);
            Bind(methods, 100664050, typeof(TasPlaybackDriver), "UnloadSourcePlaybackHooks", null, Type.EmptyTypes);
            Bind(methods, 100663956, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "Load",
                null, Type.EmptyTypes);
            Bind(methods, 100663957, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "Unload",
                null, Type.EmptyTypes);
            Bind(methods, 100663958, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "ResetTimes", null, Type.EmptyTypes);
            Bind(methods, 100663959, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "UpdateTimes", null, Type.EmptyTypes);
            Bind(methods, 100663960, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "SaveState", null, new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100663961, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "LoadState", null, new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100663962, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "AdvanceRealTime", null, Type.EmptyTypes);
            Bind(methods, 100663965, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "RecordCountCommand", null, CommandParameters);
            Bind(methods, 100663966, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "FileTimeCommand", null, CommandParameters);
            Bind(methods, 100663967, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "ChapterTimeCommand", null, CommandParameters);
            Bind(methods, 100663968, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "RealTimeCommand", null, CommandParameters);
            Bind(methods, 100663969, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "MidwayFileTimeCommand", null, CommandParameters);
            Bind(methods, 100663970, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "MidwayChapterTimeCommand", null, CommandParameters);
            Bind(methods, 100663971, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "MidwayRealTimeCommand", null, CommandParameters);
            Bind(methods, 100663972, typeof(global::Celeste.Mods.Tas.Commands.SourceTasMetadataCommands),
                "ActiveWarpsCommand", null, CommandParameters);
            Bind(methods, 100664422, typeof(TasRuntimeEnvironment),
                "LoadSourceTasInfoWatchEntities",
                null, Type.EmptyTypes);
            Bind(methods, 100664423, typeof(TasRuntimeEnvironment),
                "UnloadSourceTasInfoWatchEntities", null, Type.EmptyTypes);
            Bind(methods, 100665145, typeof(TasRuntimeEnvironment),
                "LoadSourceTasUnloadedRoomHitboxes", null, Type.EmptyTypes);
            Bind(methods, 100665146, typeof(TasRuntimeEnvironment),
                "UnloadSourceTasUnloadedRoomHitboxes", null, Type.EmptyTypes);
            Bind(methods, 100664590, typeof(TasPlaybackDriver), "LoadSourceGameplayEvents", null, Type.EmptyTypes);
            Bind(methods, 100664836, typeof(TasPlaybackDriver), "InitializeSourceHotkeys", null, Type.EmptyTypes);
            Bind(methods, 100664840, typeof(TasPlaybackDriver), "ReleaseSourceForwardKeys", null, Type.EmptyTypes);
            Bind(methods, 100664773, typeof(SourceTasDisableRumble), "Load", null, Type.EmptyTypes);
            Bind(methods, 100664775, typeof(SourceTasDisableRumble), "EnableRun", null, Type.EmptyTypes);
            Bind(methods, 100666462,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "SharedUpdateHandler_SaveState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666463,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "SharedUpdateHandler_LoadState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666470,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "FrameCounterHandler_SaveState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666471,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "FrameCounterHandler_LoadState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666477,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "DebrisHandler_SaveState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666478,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "DebrisHandler_LoadState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666486,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "PrologueBridgeHandler_SaveState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666487,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "PrologueBridgeHandler_LoadState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666493,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "SummitLaunchHandler_SaveState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666494,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "SummitLaunchHandler_LoadState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666500,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "AuraHelperLanternHandler_SaveState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666501,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "AuraHelperLanternHandler_LoadState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666508,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "AuraHelperGeneratorHandler_SaveState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666509,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "AuraHelperGeneratorHandler_LoadState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666526,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "PandorasBoxTileGlitcherHandler_SaveState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666527,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "PandorasBoxTileGlitcherHandler_LoadState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666537,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "VortexHelperColorSwitchHandler_SaveState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100666538,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "VortexHelperColorSwitchHandler_LoadState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100664678,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "Initialize", null, Type.EmptyTypes);
            Bind(methods, 100664679,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "EnableRun", null, Type.EmptyTypes);
            Bind(methods, 100664680,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "PreEngineUpdate", null, Type.EmptyTypes);
            Bind(methods, 100664681,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "PostEngineUpdate", null, Type.EmptyTypes);
            Bind(methods, 100664971, typeof(global::Celeste.Mods.Tas.Hud.SourceTasInfoHud), "SourceLoad", null,
                Type.EmptyTypes);
            Bind(methods, 100664972, typeof(global::Celeste.Mods.Tas.Hud.SourceTasInfoHud), "SourceUnload",
                null,
                Type.EmptyTypes);
            Bind(methods, 100665013, typeof(global::Celeste.Mods.Tas.Hud.SourceTasJetBrainsMonoFont),
                "SourceLoad",
                null, Type.EmptyTypes);
            Bind(methods, 100665014, typeof(global::Celeste.Mods.Tas.Hud.SourceTasJetBrainsMonoFont),
                "SourceUnload", null, Type.EmptyTypes);
            Bind(methods, 100664064, typeof(global::Celeste.Mods.Tas.TasPlaybackDriver), "LoadSourcePopupToast",
                null, Type.EmptyTypes);
            Bind(methods, 100664567, typeof(global::Celeste.Mods.Tas.TasPlaybackDriver),
                "LoadSourceCenterCamera",
                null, Type.EmptyTypes);
            Bind(methods, 100664568, typeof(global::Celeste.Mods.Tas.TasPlaybackDriver),
                "UnloadSourceCenterCamera",
                null, Type.EmptyTypes);
            Bind(methods, 100664580, typeof(global::Celeste.Mods.Tas.TasPlaybackDriver),
                "UpdateSourceCenterCameraMeta", null, Type.EmptyTypes);
            Bind(methods, 100663418, typeof(TasPlaybackDriver), "SaveSourceBindingState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100663419, typeof(TasPlaybackDriver), "LoadSourceBindingState", null,
                new[] { typeof(System.Collections.Generic.Dictionary<string, object>) });
            Bind(methods, 100663410, typeof(TasPlaybackDriver), "SetSourceTasBindings", null, Type.EmptyTypes);
            Bind(methods, 100663411, typeof(TasPlaybackDriver), "RestoreSourcePlayerBindings", null, Type.EmptyTypes);
            Bind(methods, 100663530, typeof(TasPlaybackDriver), "InitializeSourceManagerEvents", null, Type.EmptyTypes);
            Bind(methods, 100663535, typeof(TasPlaybackDriver), "RestoreSourceStudioTasFilePath", null,
                Type.EmptyTypes);
            Bind(methods, 100664001, typeof(SourceTasRequireDependencyProcess), "SourceParseFileEnd", null,
                Type.EmptyTypes);
            Bind(methods, 100664002, typeof(SourceTasRequireDependencyProcess), "SourceClearInputs", null,
                Type.EmptyTypes);
            Bind(methods, 100664758, typeof(TasPlaybackDriver), "InitializeSourceDesyncFixer", null, Type.EmptyTypes);
            Bind(methods, 100664759, typeof(TasPlaybackDriver), "LoadSourceDesyncFixer", null, Type.EmptyTypes);
            Bind(methods, 100664760, typeof(TasPlaybackDriver), "UnloadSourceDesyncFixer", null, Type.EmptyTypes);
            Bind(methods, 100664744, typeof(TasPlaybackDriver), "InitializeSourceConsoleEnhancements", null,
                Type.EmptyTypes);
            Bind(methods, 100664745, typeof(TasPlaybackDriver), "LoadSourceConsoleEnhancements", null, Type.EmptyTypes);
            Bind(methods, 100664746, typeof(TasPlaybackDriver), "UnloadSourceConsoleEnhancements", null,
                Type.EmptyTypes);
            Bind(methods, 100664747, typeof(TasPlaybackDriver), "EnableRunSourceConsoleEnhancements", null,
                Type.EmptyTypes);
            Bind(methods, 100664748, typeof(TasPlaybackDriver), "UpdateMetaSourceConsoleEnhancements", null,
                Type.EmptyTypes);
            Bind(methods, 100664753, typeof(TasPlaybackDriver), "LoadSourceDebugRcPages", null, Type.EmptyTypes);
            Bind(methods, 100664754, typeof(TasPlaybackDriver), "UnloadSourceDebugRcPages", null, Type.EmptyTypes);
            Bind(methods, 100664435, typeof(global::Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess),
                "Initialize", null, Type.EmptyTypes);
            Bind(methods, 100664436, typeof(global::Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess),
                "Unload",
                null, Type.EmptyTypes);
            Bind(methods, 100664439, typeof(global::Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess),
                "GetCmd",
                null, Type.EmptyTypes);
            Bind(methods, 100664850, typeof(TasPlaybackDriver), "UpdateSourceMouseMeta", null, Type.EmptyTypes);
            Bind(methods, 100664988, typeof(TasPlaybackDriver), "LoadSourceTasSelectedAreaEntity", null,
                Type.EmptyTypes);
            Bind(methods, 100664989, typeof(TasPlaybackDriver), "UnloadSourceTasSelectedAreaEntity", null,
                Type.EmptyTypes);
            Bind(methods, 100663689, typeof(TasPlaybackDriver), "InitializeSourceModOnHooks", null, Type.EmptyTypes);
            Bind(methods, 100663691, typeof(TasPlaybackDriver), "InitializeSourceModILHooks", null, Type.EmptyTypes);
            Bind(methods, 100664594, typeof(TasPlaybackDriver), "LoadSourceAccessibilityTools", null, Type.EmptyTypes);
            Bind(methods, 100664595, typeof(TasPlaybackDriver), "UnloadSourceAccessibilityTools", null,
                Type.EmptyTypes);
            Bind(methods, 100664597, typeof(TasPlaybackDriver), "InitializeSourceAccessibilityTools", null,
                Type.EmptyTypes);
            Bind(methods, 100664598, typeof(TasPlaybackDriver), "SourceAccessibilityPreventHideRoomInDebugViv", null,
                new[] { typeof(global::MonoMod.Cil.ILCursor) });
            Bind(methods, 100664599, typeof(TasPlaybackDriver), "SourceAccessibilityPreventHideRoomInDebugKosei", null,
                new[] { typeof(global::MonoMod.Cil.ILCursor) });
            Bind(methods, 100664600, typeof(TasPlaybackDriver), "SourceAccessibilityPreventHideKeysInDebug", null,
                new[] { typeof(global::MonoMod.Cil.ILCursor) });
            Bind(methods, 100664601, typeof(TasPlaybackDriver), "SourceAccessibilityPreventRoomAlteredInDebug", null,
                new[] { typeof(global::MonoMod.Cil.ILCursor) });
            Bind(methods, 100664602, typeof(TasPlaybackDriver), "SourceAccessibilityPreventGameRestart", null,
                new[] { typeof(global::MonoMod.Cil.ILCursor) });
            Bind(methods, 100664603, typeof(TasPlaybackDriver), "SourceAccessibilityPreventWindowResize", null,
                new[] { typeof(global::MonoMod.Cil.ILCursor) });
            Bind(methods, 100664604, typeof(TasPlaybackDriver), "SourceAccessibilityPreventGameCrash", null,
                new[] { typeof(global::MonoMod.Cil.ILCursor) });
            Bind(methods, 100664605, typeof(TasPlaybackDriver), "SourceAccessibilityForceAllowDebug", null,
                new[] { typeof(global::MonoMod.Cil.ILCursor) });
            Bind(methods, 100664617, typeof(TasPlaybackDriver), "InitializeSourceFastForwardOptimization", null,
                Type.EmptyTypes);
            Bind(methods, 100664618, typeof(TasPlaybackDriver), "UnloadSourceFastForwardOptimization", null,
                Type.EmptyTypes);
            Bind(methods, 100664624, typeof(TasPlaybackDriver), "LoadContentSourceHeadlessOptimizations", null,
                Type.EmptyTypes);
            Bind(methods, 100664776, typeof(TasPlaybackDriver), "LoadSourceFastForwardBoost", null, Type.EmptyTypes);
            Bind(methods, 100664777, typeof(TasPlaybackDriver), "UnloadSourceFastForwardBoost", null, Type.EmptyTypes);
            Bind(methods, 100664780, typeof(TasPlaybackDriver), "LoadSourceHideGameplay", null, Type.EmptyTypes);
            Bind(methods, 100664781, typeof(TasPlaybackDriver), "UnloadSourceHideGameplay", null, Type.EmptyTypes);
            Bind(methods, 100664646, typeof(TasPlaybackDriver), "LoadSourceCameraHitbox", null, Type.EmptyTypes);
            Bind(methods, 100663656, typeof(SourceTasHookLifecycle), "Unload", null, Type.EmptyTypes);
            Bind(methods, 100664033, typeof(SourceTasStunPauseProcess), "SourceInitialize", null, Type.EmptyTypes);
            Bind(methods, 100664034, typeof(SourceTasStunPauseProcess), "SourceUnload", null, Type.EmptyTypes);
            Bind(methods, 100664043, typeof(SourceTasStunPauseProcess), "SourceReset", null, Type.EmptyTypes);
            Bind(methods, 100664044, typeof(SourceTasStunPauseProcess), "SourceClearGlobalModRuntime", null,
                Type.EmptyTypes);
            Bind(methods, 100664045, typeof(SourceTasStunPauseProcess), "SourceClearGlobalModeParsing", null,
                Type.EmptyTypes);
            Bind(methods, 100664564, typeof(SourceTasBetterInvincibleProcess), "SourceInitialize", null,
                Type.EmptyTypes);
            Bind(methods, 100664565, typeof(SourceTasBetterInvincibleProcess), "SourceEnableRun", null,
                Type.EmptyTypes);
            Bind(methods, 100663383, typeof(SourceTasAnalogProcess), "SourceReset", null, Type.EmptyTypes);
            Bind(methods, 100663848, typeof(SourceTasInputController), "Initialize", null, Type.EmptyTypes);
            Bind(methods, 100663911, typeof(SourceTasAutoInputProcess), "SourceClear", null, Type.EmptyTypes);
            Bind(methods, 100663912, typeof(SourceTasAutoInputProcess), "SourceParseFileEnd", null, Type.EmptyTypes);
            Bind(methods, 100663947, typeof(TasPlaybackDriver), "DisableSourceEnforceLegal", null, Type.EmptyTypes);
            Bind(methods, 100663948, typeof(TasPlaybackDriver), "ClearSourceEnforceLegalInputs", null, Type.EmptyTypes);
            Bind(methods, 100663975, typeof(SourceTasMouseProcess), "SourceLoad", null, Type.EmptyTypes);
            Bind(methods, 100663980, typeof(SourceTasMouseProcess), "SourceClearState", null, Type.EmptyTypes);
            Bind(methods, 100663984, typeof(SourceTasPressProcess), "SourceDisableRun", null, Type.EmptyTypes);
            Bind(methods, 100663987, typeof(SourceTasReadProcess), "SourceClear", null, Type.EmptyTypes);
            Bind(methods, 100663996, typeof(SourceTasRepeatProcess), "SourceParseFileEnd", null, Type.EmptyTypes);
            Bind(methods, 100663997, typeof(SourceTasRepeatProcess), "SourceClear", null, Type.EmptyTypes);
            Bind(methods, 100664015, typeof(SourceTasSafeProcess), "SourceDisableRun", null, Type.EmptyTypes);
            Bind(methods, 100664016, typeof(SourceTasSafeProcess), "SourceClear", null, Type.EmptyTypes);
        }

        internal static void BindCommands(SourceAssemblyMethodCatalog methods, TasPlaybackDriver driver)
        {
            Bind(methods, 100664677, typeof(TasPlaybackDriver), "SourceFixStopwatchRendering", driver,
                new[] { typeof(MonoMod.Cil.ILCursor) });
            Bind(methods, 100664574, typeof(TasPlaybackDriver), "SourceCenterZoomLevelModZoom", driver,
                new[] { typeof(MonoMod.Cil.ILCursor) });
            Bind(methods, 100664575, typeof(TasPlaybackDriver), "SourceCenterZoomLevelScreenPosition", driver,
                new[] { typeof(MonoMod.Cil.ILCursor) });
            Bind(methods, 100664682,
                typeof(global::Celeste.Mods.Tas.Gameplay.DesyncFix.SourceTasSeededLifecycle),
                "SeedRandom", null, CommandParameters);
            Bind(methods, 100664950, typeof(global::Celeste.Mods.Tas.Lua.SourceTasEvalLuaCommandState),
                "EvalLua",
                driver.LuaCommands, CommandParameters);
            Bind(methods, 100663910, typeof(SourceTasAssertProcess), "Assert", driver.SourceAssertCommand,
                CommandParameters);
            Bind(methods, 100663939, typeof(SourceTasDummyCommands), "Author", null, CommandParameters);
            Bind(methods, 100663940, typeof(SourceTasDummyCommands), "FrameCount", null, CommandParameters);
            Bind(methods, 100663941, typeof(SourceTasDummyCommands), "TotalRecordCount", null, CommandParameters);
            Bind(methods, 100663954, typeof(SourceTasInvokeProcess), "Invoke", driver.SourceInvokeCommand,
                CommandParameters);
            Bind(methods, 100664038, typeof(SourceTasStunPauseProcess), "StunPause", driver.SourceStunPauseCommand,
                CommandParameters);
            Bind(methods, 100664041, typeof(SourceTasStunPauseProcess), "EndStunPause", driver.SourceStunPauseCommand,
                CommandParameters);
            Bind(methods, 100664042, typeof(SourceTasStunPauseProcess), "StunPauseCommandMode",
                driver.SourceStunPauseCommand, CommandParameters);
            Bind(methods, 100663382, typeof(SourceTasAnalogProcess), "SourceTasAnalogModeCommand", driver.SourceAnalog,
                CommandParameters);
            Bind(methods, 100663913, typeof(SourceTasAutoInputProcess), "AutoInput", driver.SourceAutoInputCommand,
                CommandParameters);
            Bind(methods, 100663914, typeof(SourceTasAutoInputProcess), "StartAutoInput", driver.SourceAutoInputCommand,
                CommandParameters);
            Bind(methods, 100663915, typeof(SourceTasAutoInputProcess), "EndAutoInput", driver.SourceAutoInputCommand,
                CommandParameters);
            Bind(methods, 100663917, typeof(SourceTasAutoInputProcess), "SkipInput", driver.SourceAutoInputCommand,
                CommandParameters);
            Bind(methods, 100663946, typeof(SourceTasEnforceLegalProcess), "EnforceLegal", driver.EnforceLegal,
                CommandParameters);
            Bind(methods, 100663977, typeof(SourceTasMouseProcess), "MouseControl", driver.SourceMouseCommand,
                CommandParameters);
            Bind(methods, 100663982, typeof(SourceTasPlayProcess), "Play", driver.SourcePlayCommand, CommandParameters);
            Bind(methods, 100663983, typeof(SourceTasPressProcess), "Press", driver.SourcePressCommand,
                CommandParameters);
            Bind(methods, 100663988, typeof(SourceTasReadProcess), "Read", driver.SourceReadCommand, CommandParameters);
            Bind(methods, 100663998, typeof(SourceTasRepeatProcess), "Repeat", driver.SourceRepeatCommand,
                CommandParameters);
            Bind(methods, 100663999, typeof(SourceTasRepeatProcess), "EndRepeat", driver.SourceRepeatCommand,
                CommandParameters);
            Bind(methods, 100664013, typeof(SourceTasSafeProcess), "Safe", driver.SourceSafeCommand, CommandParameters);
            Bind(methods, 100664014, typeof(SourceTasSafeProcess), "Unsafe", driver.SourceSafeCommand,
                CommandParameters);
            Bind(methods, 100664029, typeof(SourceTasSetProcess), "Execute", driver.SetProcess, CommandParameters);
            Bind(methods, 100663950, typeof(SourceTasGunProcess), "Gun", driver.SourceGunCommand, CommandParameters);
            Bind(methods, 100664024, typeof(SourceTasSelectCampaignProcess), "SelectCampaign",
                driver.SourceSelectCampaignCommand, CommandParameters);
            Bind(methods, 100664003, typeof(SourceTasRequireDependencyProcess), "RequireDependency",
                driver.SourceRequireDependencyCommand, CommandParameters);
        }

        private static void Bind(SourceAssemblyMethodCatalog methods, int originalToken, Type actualType,
            string actualName, object actualOwner, Type[] parameters)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                                       BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            MethodInfo actualMethod = actualType.GetMethod(actualName, flags, null, parameters, null)
                                      ?? throw new InvalidOperationException("Actual source method is absent: " +
                                                                             actualType.FullName + "." + actualName);
            methods.BindActualMethod(
                global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity,
                originalToken, actualMethod, actualOwner);
        }
    }
}
