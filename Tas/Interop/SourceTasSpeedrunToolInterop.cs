using Celeste.Input.Devices.Mouse;
using Celeste.Input.Devices.Keyboard;
using SavestateData = System.Collections.Generic.Dictionary<string, object?>;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Core.Reflection.Interop;
using Celeste.Everest.Modules.Runtime;
using Celeste.Everest.Content.Reloading;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Information;
using Celeste.Scene.Following;

namespace Celeste.Mods.Tas.Interop
{
    internal sealed class SourceTasSpeedrunToolInterop
    {
        private readonly TasPlaybackDriver driver;
        internal SourceTasSpeedrunToolInterop(TasPlaybackDriver driver) => this.driver = driver;
        internal SourceSpeedrunToolSaveLoadImports SaveLoadImports { get; } = new();
        internal SourceSpeedrunToolTasActionImports TasActionImports { get; } = new();
        public bool Installed { get; private set; }
        public bool MultipleSaveSlotsSupported { get; private set; }

        private object saveLoadHandle = null!;

        [SourceTasInitialize]
        private static void SourceInitialize() => CelesteTasModule.Instance.Driver.SourceSpeedrunTool.Initialize();

        internal void Initialize()
        {
            ((ISourceModInteropEnvironment)driver.Process).ModInterop.ModInterop(
                typeof(SourceSpeedrunToolSaveLoadImports));
            ((ISourceModInteropEnvironment)driver.Process).ModInterop.ModInterop(
                typeof(SourceSpeedrunToolTasActionImports));

            driver.ModuleAttributes.CollectOwnMethods<SourceTasSaveStateAttribute>(typeof(Dictionary<string, object?>));
            driver.ModuleAttributes.CollectOwnMethods<SourceTasLoadStateAttribute>(typeof(Dictionary<string, object?>));
            driver.ModuleAttributes.CollectOwnMethods<SourceTasClearStateAttribute>();

            Installed = CheckInstalled();

            // NOTE: SpeedrunToolTasActionImports first appeared in SRT v3.24.4
            //       In v3.24.4, everything is same as before, except that this mod-interop is added for compatibility reasons
            //       After v3.25.0, SRT supports multiple saveslots
            if (((ISourceEverestModulesProcessEnvironment)driver.Process).ModulesProcess.Modules
                .FirstOrDefault(module =>
                    module.Metadata.Name == "SpeedrunTool") is { } srtModule)
            {
                MultipleSaveSlotsSupported = srtModule.Metadata.Version >= new Version(3, 25, 0);
            }

#if DEBUG
        ((IEverestAssetReloadEnvironment)driver.Process).AssetReload.Events.OnBeforeReload += _ =>
        {
            if (Installed)
            {
                ClearSaveLoadAction();
            }

            Installed = false;
        };
        ((IEverestAssetReloadEnvironment)driver.Process).AssetReload.Events.OnAfterReload += _ =>
        {
            Installed = CheckInstalled();

            if (Installed)
            {
                AddSaveLoadAction();
            }
        };
#endif

            if (Installed)
            {
                AddSaveLoadAction();
            }

            return;

            bool CheckInstalled() => SaveLoadImports.DeepClone != null && TasActionImports.SaveState != null;
        }

        [SourceTasUnload]
        private static void SourceUnload() => CelesteTasModule.Instance.Driver.SourceSpeedrunTool.Unload();

        internal void Unload()
        {
            if (Installed)
            {
                ClearSaveLoadAction();
            }
        }

        [SourceTasDisableRun]
        private static void SourceOnTasDisableRun() =>
            CelesteTasModule.Instance.Driver.SourceSpeedrunTool.OnTasDisableRun();

        internal void OnTasDisableRun()
        {
            TasActionImports.OnTasDisableRun?.Invoke();
        }

        public const string DefaultSlot = "CelesteTAS";

        /// Saves the current state into the specified slot. Returns whether it was successful
        public bool SaveState(string? slot = null) => TasActionImports.SaveState?.Invoke(slot ?? DefaultSlot) ?? false;

        /// Loads the specified slot into the current state. Returns whether it was successful
        public bool LoadState(string? slot = null) => TasActionImports.LoadState?.Invoke(slot ?? DefaultSlot) ?? false;

        /// Clears the specified save slot
        public void ClearState(string? slot = null) => TasActionImports.ClearState?.Invoke(slot ?? DefaultSlot);

        /// Checks if something is saved in the specified save slot
        public bool IsSaved(string? slot = null) => TasActionImports.TasIsSaved?.Invoke(slot ?? DefaultSlot) ?? false;

        /// Creates a deep clone of the object. Crashes if SpeedrunTool isn't installed.
        public T DeepClone<T>(T from) where T : notnull
        {
            return (T)SaveLoadImports.DeepClone!(from);
        }

        /// Attempts to create a deep clone of the object. Fails if SpeedrunTool isn't installed.
        public bool TryDeepClone<T>(T from, [NotNullWhen(true)] out T? to) where T : notnull
        {
            if (SaveLoadImports.DeepClone is { } deepClone)
            {
                to = (T)deepClone(from);
                return true;
            }

            to = default;
            return false;
        }

        private void AddSaveLoadAction()
        {
            if (!Installed)
            {
                return;
            }

            saveLoadHandle = SaveLoadImports.RegisterSaveLoadAction!(
                (savedValues, _) =>
                {
                    var saveData = new SavestateData
                    {
                        { nameof(driver.GameInfo.LastPos), driver.GameInfo.LastPos },
                        { nameof(driver.GameInfo.LastDiff), driver.GameInfo.LastDiff },
                        { nameof(driver.GameInfo.LastPlayerSeekerPos), driver.GameInfo.LastPlayerSeekerPos },
                        { nameof(driver.GameInfo.LastPlayerSeekerDiff), driver.GameInfo.LastPlayerSeekerDiff },

                        {
                            nameof(driver.SourceCycleHitboxColor.GroupCounter),
                            driver.SourceCycleHitboxColor.GroupCounter
                        },
                        {
                            nameof(driver.SourceStunPauseCommand.SimulatePauses),
                            driver.SourceStunPauseCommand.SimulatePauses
                        },
                        {
                            nameof(driver.SourceStunPauseCommand.PauseOnCurrentFrame),
                            driver.SourceStunPauseCommand.PauseOnCurrentFrame
                        },
                        { nameof(driver.SourceStunPauseCommand.SkipFrames), driver.SourceStunPauseCommand.SkipFrames },
                        {
                            nameof(driver.SourceStunPauseCommand.WaitingFrames),
                            driver.SourceStunPauseCommand.WaitingFrames
                        },
                        { nameof(driver.SourceStunPauseCommand.LocalMode), driver.SourceStunPauseCommand.LocalMode },
                        {
                            nameof(driver.SourceStunPauseCommand.GlobalModeRuntime),
                            driver.SourceStunPauseCommand.GlobalModeRuntime
                        },
                        { nameof(driver.SourcePressCommand.PressKeys), driver.SourcePressCommand.PressKeys },
                        { nameof(driver.SourceMouseCommand.CurrentState), driver.SourceMouseCommand.CurrentState },
                        { nameof(driver.SourceHitboxSimplified.Followers), driver.SourceHitboxSimplified.Followers },
                        {
                            nameof(driver.SourceSafeCommand.DisallowUnsafeInput),
                            driver.SourceSafeCommand.DisallowUnsafeInput
                        },
                        { "BetterInvincible", driver.Running && driver.SourceBetterInvincible.Invincible },
                    };
                    driver.ModuleAttributes.Invoke<SourceTasSaveStateAttribute>(saveData);

                    savedValues[typeof(SourceTasSpeedrunToolInterop)] = DeepClone(saveData);

                    // Store ourselves to be able to clear it, when the user asks to
                    driver.GameInfo.WatchEntities.WatchedEntities_Save =
                        DeepClone(driver.GameInfo.WatchEntities.WatchedEntities);
                },
                (savedValues, _) =>
                {
                    var saveData = DeepClone(savedValues[typeof(SourceTasSpeedrunToolInterop)]);

                    driver.GameInfo.LastPos = (Vector2Double)saveData[nameof(driver.GameInfo.LastPos)]!;
                    driver.GameInfo.LastDiff = (Vector2Double)saveData[nameof(driver.GameInfo.LastDiff)]!;
                    driver.GameInfo.LastPlayerSeekerPos =
                        (Vector2Double)saveData[nameof(driver.GameInfo.LastPlayerSeekerPos)]!;
                    driver.GameInfo.LastPlayerSeekerDiff =
                        (Vector2Double)saveData[nameof(driver.GameInfo.LastPlayerSeekerDiff)]!;

                    driver.SourceCycleHitboxColor.GroupCounter =
                        (int)saveData[nameof(driver.SourceCycleHitboxColor.GroupCounter)]!;
                    driver.SourceStunPauseCommand.SimulatePauses =
                        (bool)saveData[nameof(driver.SourceStunPauseCommand.SimulatePauses)]!;
                    driver.SourceStunPauseCommand.PauseOnCurrentFrame =
                        (bool)saveData[nameof(driver.SourceStunPauseCommand.PauseOnCurrentFrame)]!;
                    driver.SourceStunPauseCommand.SkipFrames =
                        (int)saveData[nameof(driver.SourceStunPauseCommand.SkipFrames)]!;
                    driver.SourceStunPauseCommand.WaitingFrames =
                        (int)saveData[nameof(driver.SourceStunPauseCommand.WaitingFrames)]!;
                    driver.SourceStunPauseCommand.LocalMode =
                        (SourceTasStunPauseProcess.StunPauseMode?)saveData[
                            nameof(driver.SourceStunPauseCommand.LocalMode)];
                    driver.SourceStunPauseCommand.GlobalModeRuntime =
                        (SourceTasStunPauseProcess.StunPauseMode?)saveData[
                            nameof(driver.SourceStunPauseCommand.GlobalModeRuntime)];

                    driver.SourcePressCommand.PressKeys.Clear();
                    driver.SourcePressCommand.PressKeys.UnionWith(
                        (HashSet<SourceKeys>)saveData[nameof(driver.SourcePressCommand.PressKeys)]!);

                    driver.SourceMouseCommand.CurrentState =
                        (SourceMouseState)saveData[nameof(driver.SourceMouseCommand.CurrentState)]!;
                    driver.SourceHitboxSimplified.Followers =
                        (Dictionary<FollowerState, bool>)saveData[nameof(driver.SourceHitboxSimplified.Followers)]!;
                    driver.SourceSafeCommand.DisallowUnsafeInput =
                        (bool)saveData[nameof(driver.SourceSafeCommand.DisallowUnsafeInput)]!;
                    driver.SourceBetterInvincible.Invincible = driver.Running && (bool)saveData["BetterInvincible"]!;

                    driver.GameInfo.WatchEntities.WatchedEntities =
                        DeepClone(driver.GameInfo.WatchEntities.WatchedEntities_Save);

                    driver.ModuleAttributes.Invoke<SourceTasLoadStateAttribute>(saveData);
                },
                () =>
                {
                    driver.GameInfo.WatchEntities.WatchedEntities_Save.Clear();

                    driver.ModuleAttributes.Invoke<SourceTasClearStateAttribute>();
                },
                beforeSaveState: null,
                beforeLoadState: null,
                preCloneEntities: null
            );
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ClearSaveLoadAction()
        {
            if (Installed)
            {
                SaveLoadImports.Unregister!(saveLoadHandle);
            }
        }

        internal void TryInputDeregister()
        {
            TasActionImports.InputDeregister?.Invoke();
        }
    }
}
