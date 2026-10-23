using Celeste.Mods.Tas.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StudioCommunication.Util;
using Celeste.Everest.Content.Assets;
using Celeste.Everest.Content.Registry;
using Celeste.Everest.Content.Types.Maps;
using Celeste.Everest.Modules.Runtime;
using Celeste.Game.Execution.Areas;
using Celeste.Mods.Tas.Module;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasGameData
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;

        internal SourceTasGameData(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        internal string GetConsoleCommand(bool simple)
        {
            return driver.ConsoleProcess.CreateConsoleCommand(simple);
        }

        internal string GetModInfo()
        {
            if (process.Frame.Scenes.CurrentScene is not Level level)
            {
                return string.Empty;
            }

            string MetaToString(EverestModuleMetadata metadata, int indentation = 0, bool comment = true)
            {
                return (comment ? "# " : string.Empty) + string.Empty.PadLeft(indentation) +
                       $"{metadata.Name} {metadata.VersionString}\n";
            }

            HashSet<string> ignoreMetaNames =
            [
                "DialogCutscene",
                "UpdateChecker",
                "InfiniteSaves",
                "DebugRebind",
                "RebindPeriod"
            ];

            List<EverestModuleMetadata> metas = ((ISourceEverestModulesProcessEnvironment)process).ModulesProcess
                .Modules
                .Where(module => !ignoreMetaNames.Contains(module.Metadata.Name) &&
                                 module.Metadata.VersionString != "0.0.0-dummy")
                .Select(module => module.Metadata).ToList();
            metas.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));

            var areaData = process.AreaKeys.GetArea(process.AreaKeys.GetSessionArea(level.Session));
            string moduleName = string.Empty;
            EverestModuleMetadata? mapMeta = null;
            if (((IEverestContentRegistryEnvironment)process).SourceContentRegistry.TryGet<AssetTypeMap>(
                    "Maps/" + areaData.SID, out EverestContentAsset mapModAsset) && mapModAsset.Source != null)
            {
                moduleName = mapModAsset.Source.Name;
                mapMeta = metas.FirstOrDefault(meta => meta.Name == moduleName);
            }

            string modInfo = "";

            if (mapMeta != null)
            {
                modInfo += DependencyToString(mapMeta);
                modInfo += "\n\n";
            }

            EverestModuleMetadata celesteMeta = metas.First(metadata => metadata.Name == "Celeste");
            EverestModuleMetadata everestMeta = metas.First(metadata => metadata.Name == "Everest");
            EverestModuleMetadata tasMeta = metas.First(metadata => metadata.Name == "CelesteTAS");
            modInfo += MetaToString(celesteMeta);
            modInfo += MetaToString(everestMeta);
            modInfo += MetaToString(tasMeta);
            metas.Remove(celesteMeta);
            metas.Remove(everestMeta);
            metas.Remove(tasMeta);

            EverestModuleMetadata? speedrunToolMeta = metas.FirstOrDefault(metadata => metadata.Name == "SpeedrunTool");
            if (speedrunToolMeta != null)
            {
                modInfo += MetaToString(speedrunToolMeta);
                metas.Remove(speedrunToolMeta);
            }

            ignoreMetaNames.UnionWith(new HashSet<string>
            {
                "Celeste",
                "Everest",
                "CelesteTAS",
                "SpeedrunTool"
            });

            modInfo += "\n# Map:\n";
            if (mapMeta != null)
            {
                modInfo += MetaToString(mapMeta, 2);
                if (TasRuntimeEnvironment.Get(process).SourceTasModUpdateInfos
                        ?.TryGetValue(mapMeta.Name, out var modUpdateInfo) == true &&
                    modUpdateInfo.GameBananaId > 0)
                {
                    modInfo += $"#   https://gamebanana.com/mods/{modUpdateInfo.GameBananaId}\n";
                }
            }

            string mode = process.AreaKeys.GetSessionArea(level.Session).Mode == 0
                ? "ASide"
                : ((OriginalAreaMode)process.AreaKeys.GetSessionArea(level.Session).Mode).ToString();
            modInfo += $"#   {areaData.SID} {mode}\n";

            if (!string.IsNullOrEmpty(moduleName) && mapMeta != null)
            {
                List<EverestModuleMetadata> dependencies = mapMeta.Dependencies
                    .Where(metadata =>
                        !ignoreMetaNames.Contains(metadata.Name) && metadata.VersionString != "0.0.0-dummy")
                    .ToList();
                dependencies.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));
                if (dependencies.Count > 0)
                {
                    modInfo += "\n# Dependencies:\n";
                    modInfo += string.Join(string.Empty,
                        dependencies.Select(meta => metas.First(metadata => metadata.Name == meta.Name))
                            .Select(meta => MetaToString(meta, 2)));
                }

                modInfo += "\n# Other Installed Mods:\n";
                modInfo += string.Join(string.Empty,
                    metas.Where(meta =>
                            meta.Name != moduleName && dependencies.All(metadata => metadata.Name != meta.Name))
                        .Select(meta => MetaToString(meta, 2)));
            }
            else if (metas.IsNotEmpty())
            {
                modInfo += "\n# Other Installed Mods:\n";
                modInfo += string.Join(string.Empty, metas.Select(meta => MetaToString(meta, 2)));
            }

            return modInfo;
        }

        internal string GetSettingValue(string settingName)
        {
            if (driver.SourceModReflection.GetPropertyInfo(typeof(CelesteTasSettings), settingName) is { } property)
            {
                return property.Read(driver.Settings)!.ToString()!;
            }
            else
            {
                return string.Empty;
            }
        }

        internal string GetModUrl()
        {
            if (process.Frame.Scenes.CurrentScene is not Level level)
            {
                return string.Empty;
            }

            var areaData = process.AreaKeys.GetArea(process.AreaKeys.GetSessionArea(level.Session));
            string moduleName = string.Empty;
            SourceEverestModule? mapModule = null;
            if (((IEverestContentRegistryEnvironment)process).SourceContentRegistry.TryGet<AssetTypeMap>(
                    "Maps/" + areaData.SID, out EverestContentAsset mapModAsset) && mapModAsset.Source != null)
            {
                moduleName = mapModAsset.Source.Name;
                mapModule =
                    ((ISourceEverestModulesProcessEnvironment)process).ModulesProcess.Modules.FirstOrDefault(module =>
                        module.Metadata?.Name == moduleName);
            }

            if (mapModule == null)
            {
                return string.Empty;
            }

            if (TasRuntimeEnvironment.Get(process).SourceTasModUpdateInfos
                    ?.TryGetValue(moduleName, out var modUpdateInfo) == true &&
                modUpdateInfo.GameBananaId > 0)
            {
                return $"# {moduleName}\n# https://gamebanana.com/mods/{modUpdateInfo.GameBananaId}\n\n";
            }

            return string.Empty;
        }

        internal string DependencyToString(EverestModuleMetadata metadata)
        {
            return $"RequireDependency, {metadata.Name}, {metadata.VersionString}";
        }

        internal string GetStartingRoom()
        {
            if (process.Frame.Scenes.CurrentScene is not Level level)
            {
                return string.Empty;
            }

            var mapData = (global::Celeste.Game.Content.Maps.OriginalChapterMapData)level.Session.MapData;
            return mapData.StartLevel()?.Name ?? string.Empty;
        }

        internal string GetRequireDependency()
        {
            if (process.Frame.Scenes.CurrentScene is Level level &&
                ((IEverestContentRegistryEnvironment)process).SourceContentRegistry.TryGet<AssetTypeMap>(
                    "Maps/" + process.AreaKeys.GetArea(process.AreaKeys.GetSessionArea(level.Session)).SID,
                    out EverestContentAsset mapModAsset) && mapModAsset.Source != null &&
                ((ISourceEverestModulesProcessEnvironment)process).ModulesProcess.Modules.FirstOrDefault(module =>
                    module.Metadata.Name == mapModAsset.Source.Name) is { } mod)
            {
                return DependencyToString(mod.Metadata);
            }

            return string.Empty;
        }
    }
}
