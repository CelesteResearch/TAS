using Celeste.Everest;
using System;
using System.Collections.Generic;
using Celeste.Core.Reflection.Members;
using Celeste.Game.Settings;
using Celeste.Game.Save.Data;
using Celeste.Game.Save.Runtime;
using Celeste.Game.Execution.Processes;
using Celeste.Game.Content.Dialogs;
using Celeste.Everest.Modules.Runtime;
using Celeste.Everest.Reflection;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Interop
{
    internal sealed class SourceTasRestoreSettingsProcess
    {
        private readonly TasPlaybackDriver driver;
        internal SourceTasRestoreSettingsProcess(TasPlaybackDriver driver) => this.driver = driver;
        private SettingsState Settings => ((ISettingsContext)driver.Process).Settings;
        private OriginalSaveDataState CurrentSave => ((IOriginalGameplayProcess)driver.Process).SaveContext.CurrentSave;

        private SourceMemberCatalog Members =>
            ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMembers;

        private SettingsState? origSettings;
        private FileAssists? origAssists;
        private readonly Dictionary<SourceEverestModule, object> origModSettings = new();
        private readonly Dictionary<object, object?> origExtendedVariants = new();

        internal readonly HashSet<SourceEverestModule> ignoredModules = new();

        internal readonly Dictionary<SourceEverestModule, (Func<object> Backup, Action<object> Restore)>
            customHandlers =
                new();

        [SourceTasEnableRun]
        private static void SourceTryBackup() => CelesteTasModule.Instance.Driver.SourceRestoreSettings.TryBackup();

        internal void TryBackup()
        {
            origSettings = null;
            origAssists = null;

            if (!driver.Settings.RestoreSettings)
            {
                return;
            }

            origSettings = SourceTasCloneUtil.ShallowClone(Settings);
            origAssists = CurrentSave?.Assists;

            origModSettings.Clear();
            foreach (var module in ((ISourceEverestModulesProcessEnvironment)driver.Process).ModulesProcess.Modules)
            {
                if (module._Settings == null || module.SettingsType == null || module._Settings is CelesteTasSettings ||
                    module.Metadata.Name == "ExtendedVariantMode")
                {
                    continue;
                }

                if (ignoredModules.Contains(module))
                {
                    continue;
                }

                if (customHandlers.TryGetValue(module, out var handler))
                {
                    origModSettings.Add(module, handler.Backup());
                    continue;
                }

                // When using savestates, need to deep clone settings, to avoid issues with ButtonBindings breaking
                if (driver.SourceSpeedrunTool.TryDeepClone(module._Settings, out var clonedSettings))
                {
                    origModSettings.Add(module, clonedSettings);
                }
                else
                {
                    origModSettings.Add(module, SourceTasCloneUtil.ShallowClone(module._Settings));
                }

                continue;
            }

            origExtendedVariants.Clear();
            if (driver.SourceExtendedVariants.GetVariantsEnum() is { } variantsEnum)
            {
                foreach (object variant in Enum.GetValues(variantsEnum))
                {
                    try
                    {
                        var lazyVariant = new Lazy<object?>(variant);
                        object? currValue = driver.SourceExtendedVariants.GetCurrentVariantValue(lazyVariant);
                        object? currMapValue =
                            driver.SourceExtendedVariants.GetCurrentMapDefinedVariantValue(lazyVariant);

                        if (currValue != currMapValue)
                        {
                            origExtendedVariants[variant] = currValue;
                        }
                    }
                    catch
                    {
                        // ignore
                    }
                }
            }
        }

        [SourceTasDisableRun]
        private static void SourceTryRestore() => CelesteTasModule.Instance.Driver.SourceRestoreSettings.TryRestore();

        internal void TryRestore()
        {
            if (origSettings != null)
            {
                SourceTasCloneUtil.CopyAllFields(driver.TargetQuery.Reflection, Settings, origSettings);
                Settings.ApplyVolumes(((IOriginalGameplayProcess)driver.Process).AudioSession);
                ((IOriginalGameTextEnvironment)driver.Process).Text.ApplyLanguage(Settings);
                origSettings = null;
            }

            if (origAssists != null)
            {
                var oldAssists = CurrentSave.Assists;
                CurrentSave.Assists = origAssists.Value;
                driver.SourceAssistQuery.ApplyAssists(oldAssists, CurrentSave.Assists);
                origAssists = null;
            }

            if (origModSettings.Count != 0)
            {
                driver.Settings.Enabled = true;
                driver.Settings.RestoreSettings = true;

                foreach (var module in ((ISourceEverestModulesProcessEnvironment)driver.Process).ModulesProcess.Modules)
                {
                    try
                    {
                        if (module._Settings == null || !origModSettings.TryGetValue(module, out object? modSettings))
                        {
                            continue;
                        }

                        if (ignoredModules.Contains(module))
                        {
                            continue;
                        }

                        if (customHandlers.TryGetValue(module, out var handler))
                        {
                            handler.Restore(modSettings);
                            continue;
                        }

                        SourceTasCloneUtil.CopyAllProperties(driver.TargetQuery.Reflection, module._Settings,
                            modSettings,
                            true);
                        SourceTasCloneUtil.CopyAllFields(driver.TargetQuery.Reflection, module._Settings, modSettings,
                            true);
                    }
                    catch (Exception ex)
                    {
                        ((IEverestLoggerProcessEnvironment)driver.Process).Logger.Log(EverestLogLevel.Warn,
                            "CelesteTAS",
                            $"Failed to restore settings for mod '{module.Metadata.Name}'");
                        ((IEverestLoggerProcessEnvironment)driver.Process).Logger.Log(EverestLogLevel.Warn,
                            "CelesteTAS",
                            ex.ToString());
                    }
                }

                origModSettings.Clear();
            }

            if (origExtendedVariants.Count != 0)
            {
                var variantsEnum = driver.SourceExtendedVariants.GetVariantsEnum()!;
                foreach (object variant in Enum.GetValues(variantsEnum))
                {
                    try
                    {
                        // Calling player.ResetSprite during StIntroWakeUp causes the player to be stuck in the state
                        string? name = variant.ToString();
                        if (name is "MadelineBackpackMode" or "PlayAsBadeline" &&
                            SourceTasSceneSessionQuery.GetPlayer(driver.Process.Frame.Scenes.CurrentScene) is
                                { } player &&
                            player.Machine.State == 15)
                        {
                            continue;
                        }

                        if (origExtendedVariants.TryGetValue(variant, out object? origValue))
                        {
                            var lazyVariant = new Lazy<object?>(variant);
                            object? currValue = driver.SourceExtendedVariants.GetCurrentVariantValue(lazyVariant);
                            if (currValue != origValue)
                            {
                                driver.SourceExtendedVariants.SetVariantValue(lazyVariant, origValue);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ((IEverestLoggerProcessEnvironment)driver.Process).Logger.Log(EverestLogLevel.Warn,
                            "CelesteTAS",
                            $"Failed to restore value for Extended Variant '{variant}'");
                        ((IEverestLoggerProcessEnvironment)driver.Process).Logger.Log(EverestLogLevel.Warn,
                            "CelesteTAS",
                            ex.ToString());
                    }
                }

                origExtendedVariants.Clear();
            }
        }

        [SourceTasLoad]
        private static void SourceLoad() => CelesteTasModule.Instance.Driver.SourceRestoreSettings.Load();

        internal void Load()
        {
            driver.Process.SourceSaveStartReturned += SaveDataOnStart;
        }

        [SourceTasUnload]
        private static void SourceUnload() => CelesteTasModule.Instance.Driver.SourceRestoreSettings.Unload();

        internal void Unload()
        {
            driver.Process.SourceSaveStartReturned -= SaveDataOnStart;
        }

        private void SaveDataOnStart(OriginalSaveDataState actualSave)
        {
            // The TAS might've been started outside a save file, so backup save-data now
            origAssists ??= actualSave.Assists;
        }
    }
}
