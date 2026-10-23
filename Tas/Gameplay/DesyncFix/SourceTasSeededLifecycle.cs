using System;
using System.Collections.Generic;
using System.Linq;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Interop;
using Celeste.Mods.Tas.Commands;
using StudioCommunication;

namespace Celeste.Mods.Tas.Gameplay.DesyncFix
{
    internal static class SourceTasSeededLifecycle
    {
        private static SourceTasSeededRandomness State => CelesteTasModule.Instance.Driver.SourceSeededRandomness;

        [SourceTasCommand("SeedRandom", CalcChecksum = true,
            ExecuteTiming = SourceTasExecuteTiming.Parse | SourceTasExecuteTiming.Runtime,
            MetaDataProvider = typeof(SourceTasSeededCommandMeta))]
        internal static void SeedRandom(CommandLine line, int studioLine, string path, int fileLine) =>
            State.SeedRandom(line, studioLine, path, fileLine);

        [SourceTasInitialize]
        internal static void Initialize() => State.Initialize();

        [SourceTasEnableRun]
        internal static void EnableRun() => State.EnableRun();

        [SourceTasPreEngineUpdate]
        internal static void PreEngineUpdate() => State.PreEngineUpdate();

        [SourceTasPostEngineUpdate]
        internal static void PostEngineUpdate() => State.PostEngineUpdate();

        [SourceTasSaveState]
        internal static void SharedUpdateHandler_SaveState(Dictionary<string, object> data) =>
            State.SharedUpdateHandler.SaveState(data);

        [SourceTasLoadState]
        internal static void SharedUpdateHandler_LoadState(Dictionary<string, object> data) =>
            State.SharedUpdateHandler.LoadState(data);

        [SourceTasSaveState]
        internal static void FrameCounterHandler_SaveState(Dictionary<string, object> data) =>
            State.FrameCounterHandler.SaveState(data);

        [SourceTasLoadState]
        internal static void FrameCounterHandler_LoadState(Dictionary<string, object> data) =>
            State.FrameCounterHandler.LoadState(data);

        [SourceTasSaveState]
        internal static void DebrisHandler_SaveState(Dictionary<string, object> data) =>
            State.RequireHandler<SourceTasSeededRandomness.DebrisHandler>().SaveState(data);

        [SourceTasLoadState]
        internal static void DebrisHandler_LoadState(Dictionary<string, object> data) =>
            State.RequireHandler<SourceTasSeededRandomness.DebrisHandler>().LoadState(data);

        [SourceTasSaveState]
        internal static void PrologueBridgeHandler_SaveState(Dictionary<string, object> data) =>
            State.RequireHandler<SourceTasSeededRandomness.PrologueBridgeHandler>().SaveState(data);

        [SourceTasLoadState]
        internal static void PrologueBridgeHandler_LoadState(Dictionary<string, object> data) =>
            State.RequireHandler<SourceTasSeededRandomness.PrologueBridgeHandler>().LoadState(data);

        [SourceTasSaveState]
        internal static void SummitLaunchHandler_SaveState(Dictionary<string, object> data) =>
            State.RequireHandler<SourceTasSeededRandomness.SummitLaunchHandler>().SaveState(data);

        [SourceTasLoadState]
        internal static void SummitLaunchHandler_LoadState(Dictionary<string, object> data) =>
            State.RequireHandler<SourceTasSeededRandomness.SummitLaunchHandler>().LoadState(data);

        [SourceTasSaveState]
        internal static void AuraHelperLanternHandler_SaveState(Dictionary<string, object> data) =>
            State.AuraLantern.SaveState(data);

        [SourceTasLoadState]
        internal static void AuraHelperLanternHandler_LoadState(Dictionary<string, object> data) =>
            State.AuraLantern.LoadState(data);

        [SourceTasSaveState]
        internal static void AuraHelperGeneratorHandler_SaveState(Dictionary<string, object> data) =>
            State.AuraGenerator.SaveState(data);

        [SourceTasLoadState]
        internal static void AuraHelperGeneratorHandler_LoadState(Dictionary<string, object> data) =>
            State.AuraGenerator.LoadState(data);

        [SourceTasSaveState]
        internal static void PandorasBoxTileGlitcherHandler_SaveState(Dictionary<string, object> data) =>
            State.Pandoras.SaveState(data);

        [SourceTasLoadState]
        internal static void PandorasBoxTileGlitcherHandler_LoadState(Dictionary<string, object> data) =>
            State.Pandoras.LoadState(data);

        [SourceTasSaveState]
        internal static void VortexHelperColorSwitchHandler_SaveState(Dictionary<string, object> data) =>
            State.Vortex.SaveState(data);

        [SourceTasLoadState]
        internal static void VortexHelperColorSwitchHandler_LoadState(Dictionary<string, object> data) =>
            State.Vortex.LoadState(data);
    }
}
