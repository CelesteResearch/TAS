using System;
using System.Collections.Generic;
using System.Numerics;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Mods.Tas.Module;
using Celeste.Everest.Modules.Runtime;
using Celeste.Everest.Modules.Core;
using Celeste.Everest.Content.Registry;
using Celeste.Game.Execution.Graphics;
using Celeste.Formats.Graphics.Sprites;
using Celeste.Simulation.Gameplay.Player.Presentation;
using Celeste.Simulation.Gameplay.Player.Presentation.Hair;
using Celeste.Simulation.Gameplay.Player.Presentation.Sprites;
using Celeste.Graphics.Animation;
using Player = Celeste.Simulation.RoomPlayerSession;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;
using ModAsset = Celeste.Everest.Content.Assets.EverestContentAsset;
using Celeste.Everest.Content.Types.Graphics;

namespace Celeste.Mods.Tas.DesyncFix
{
    /// Fixes desyncs caused by SkinMods changing animation lengths / carry offsets,
    /// by splitting the PlayerSprite into a visual and gameplay component
    internal sealed partial class SourceTasSkinModFixProcess
    {
        private readonly TasPlaybackDriver driver;
        public SourceTasSkinModFixProcess(TasPlaybackDriver driver) => this.driver = driver;

        private OriginalGameGraphicsResources Graphics =>
            (OriginalGameGraphicsResources)((Celeste.Graphics.Content.ISourceGameGraphicsEnvironment)driver.Process)
            .GameGraphics;

        private SourceEverestModulesProcess Modules =>
            ((ISourceEverestModulesProcessEnvironment)driver.Process).ModulesProcess;

        private EverestContentRegistry Content =>
            ((IEverestContentRegistryEnvironment)driver.Process).SourceContentRegistry;

        private Celeste.Game.Execution.Areas.IOriginalAreaKeyPolicy AreaKeys =>
            ((Celeste.Game.Execution.Areas.IOriginalAreaKeyEnvironment)driver.Process).AreaKeys;

        private bool Enabled => driver.Settings.Enabled && driver.Settings.PreventSkinModGameplayChanges switch
        {
            GameplayEnableCondition.Never => false,
            GameplayEnableCondition.Always => true,
            GameplayEnableCondition.DuringTAS => driver.Running,
            _ => throw new ArgumentOutOfRangeException()
        };

        /// The `object` must be a boxed PlayerSpriteMode
        private readonly ConditionalWeakTable<PlayerSprite, object> actualSpriteMode = new();

        private readonly ConditionalWeakTable<PlayerSprite, PlayerSprite> gameplayToVisualSprites = new();

        private readonly Dictionary<string, EverestModuleMetadata> moddedMaps = new();
        private readonly Dictionary<EverestModuleMetadata, SpriteBank> moddedSpriteBanks = new();

        private readonly Dictionary<EverestModuleMetadata, Dictionary<string, PlayerAnimMetadata>> moddedFrameMetadata =
            new();

        internal void Initialize()
        {
            HookAllOriginalMethods(
                driver.SourceModReflection.TryGetType("SkinModHelperPlus",
                    "Celeste.Mod.SkinModHelper.PlayerSkinSystem"),
                (cursor, _) => FixPlayerHairSprite(cursor));
            HookAllOriginalMethods(
                driver.SourceModReflection.TryGetType("SkinModHelperPlus", "Celeste.Mod.SkinModHelper.HairConfig"),
                (cursor, _) => FixPlayerHairSprite(cursor));
            HookOptional("SkinModHelperPlus", "Celeste.Mod.SkinModHelper.SkinsSystem", "SyncColorGrade", (cursor, _) =>
            {
                FixSpriteParameter(cursor, 0);
                FixSpriteParameter(cursor, 1);
            });
            HookOptional("SkinModHelperPlus", "Celeste.Mod.SkinModHelper.PlayerSkinSystem",
                "SpriteRenderHook_ColorGrade",
                (cursor, _) => FixSpriteParameter(cursor, 1));
            HookOptional("SkinModHelperPlus", "Celeste.Mod.SkinModHelper.CharacterConfig", "For",
                (cursor, _) => FixSpriteParameter(cursor, 0));
            HookOptional("SkinModHelperPlus", "Celeste.Mod.SkinModHelper.SomePatches", "DeathEffectDrawHook",
                (cursor, il) =>
                {
                    int spriteIdx = il.Body.Variables.IndexOf(variable =>
                        variable.VariableType.ResolveReflection() == typeof(Sprite));
                    cursor.GotoNext(MoveType.AfterLabel, instruction => instruction.MatchStloc(spriteIdx));
                    cursor.EmitDelegate((Func<Sprite, Sprite>)CorrectVisualSprite);
                });
            HookOptional("SkinModHelperPlus", "Celeste.Mod.SkinModHelper.SomePatches", "DeathEffectRenderHook",
                (cursor, il) =>
                {
                    int spriteIdx = il.Body.Variables.IndexOf(variable =>
                        variable.VariableType.ResolveReflection() == typeof(Sprite));
                    cursor.GotoNext(MoveType.AfterLabel, instruction => instruction.MatchStloc(spriteIdx));
                    cursor.EmitDelegate((Func<Sprite, Sprite>)CorrectVisualSprite);
                });
            HookOptional("Avali-Skinmod", "Celeste.Mod.AvaliSkin.AvaliSkinModule", "trySpriteSwap",
                (cursor, _) => FixSpriteParameter(cursor, 1));

            return;

            Sprite CorrectVisualSprite(Sprite sprite)
            {
                if (sprite is PlayerSprite playerSprite &&
                    gameplayToVisualSprites.TryGetValue(playerSprite, out var visualSprite))
                {
                    return visualSprite;
                }

                return sprite;
            }

            void FixSpriteParameter(ILCursor cursor, int index)
            {
#if DEBUG
            Debug.Assert(cursor.Method.Parameters[index].ParameterType.ResolveReflection()
                .IsAssignableFrom(typeof(PlayerSprite)));
#endif

                cursor.EmitLdarg(index);
                cursor.EmitDelegate((Func<Sprite, Sprite>)CorrectVisualSprite);
                cursor.EmitStarg(index);
            }

            void FixPlayerHairSprite(ILCursor cursor)
            {
                while (cursor.TryGotoNext(MoveType.After,
                           instr => instr.MatchLdfld<PlayerHair>(nameof(PlayerHair.Sprite))))
                {
                    cursor.EmitDelegate((Func<Sprite, Sprite>)CorrectVisualSprite);
                }
            }
        }

        private bool CheckMapRequiresSkin()
        {
            // Check if map set SMH+ skin
            if (driver.Process.Frame.Scenes.CurrentScene is Level &&
                Modules.Modules.FirstOrDefault(module => module.Metadata.Name == "SkinModHelperPlus") is { } smhpModule)
            {
                // Reference: https://github.com/AAA1459/SkinModHelper/blob/8dcf2ab52b3850bbf0872b51a477c932f61be67a/Code/SkinModHelperUI.cs#L70
                var smhSession = smhpModule._Session;
                bool mapSetSkin = Property<string>(smhSession, "SelectedPlayerSkin") != null ||
                                  Property<string>(smhSession, "SelectedOtherSelfSkin") != null ||
                                  Property<string>(smhSession, "SelectedSilhouetteSkin") != null;

                if (mapSetSkin)
                {
                    return true;
                }
            }

            return false;
        }

        /// Adjusted from SpriteBank.LoadSpriteBank (Everest)
        private SpriteBank CreateSpriteBankForMod(EverestModuleMetadata metadata)
        {
            // Collect all direct / transitive dependencies
            var allDependencies = new HashSet<EverestModuleMetadata>(capacity: 1 + metadata.Dependencies.Count);

            var queue = new Stack<EverestModuleMetadata>(capacity: 1 + metadata.Dependencies.Count);
            queue.Push(metadata);
            while (queue.TryPop(out var curr))
            {
                queue.PushRange(curr.Dependencies);
                allDependencies.Add(curr);
            }

            const string spritesXmlFilePath = "Graphics/Sprites.xml";
            const string spritesXmlAssetPath = "Graphics/Sprites";

            var spriteBankXml =
                ((Celeste.Core.Serialization.Xml.ISourceOriginalContentXmlEnvironment)driver.Process)
                .LoadOriginalContentXml(spritesXmlFilePath);
            var originalSpriteBankXml =
                ((Celeste.Core.Serialization.Xml.ISourceOriginalContentXmlEnvironment)driver.Process)
                .LoadOriginalContentXml(spritesXmlFilePath);

            var sprites = spriteBankXml["Sprites"]!;

            // Find all mod files that match this one, EXCEPT for the "shadow structure" asset - the unique "Graphics/Sprites" asset.
            ModAsset[] modAssets;
            lock (Content.Map)
            {
                modAssets = Content.Map
                    .Where(entry => entry.Value.Type == typeof(AssetTypeSpriteBank) &&
                                    entry.Value.PathVirtual.Equals(spritesXmlAssetPath) &&
                                    !entry.Value.PathVirtual.Equals(entry.Key)) // Filter out the unique asset
                    .Select(kvp => kvp.Value)
                    .ToArray();
            }

            foreach (var modAsset in modAssets)
            {
                // If metadata is null, this is a vanilla map
                bool isDependency = metadata != null && allDependencies.Contains(modAsset.Source.Mod);

                string modPath = modAsset.Source.Mod.PathDirectory;
                if (string.IsNullOrEmpty(modPath))
                {
                    modPath = modAsset.Source.Mod.PathArchive;
                }

                using var stream = modAsset.Stream;
                var modXml = new XmlDocument();
                modXml.Load(stream);
                modXml = global::Celeste.Everest.Content.Graphics.EverestSpriteBankXmlFilter.FilterVanillaCopies(
                    originalSpriteBankXml, modXml, modPath,
                    ((Celeste.Everest.Diagnostics.Logging.IEverestLoggerProcessEnvironment)driver.Process).Logger);

                foreach (XmlNode node in modXml["Sprites"]!.ChildNodes)
                {
                    if (node is not XmlElement)
                    {
                        continue;
                    }

                    var importedNode = spriteBankXml.ImportNode(node, true);
                    var existingNode = sprites.SelectSingleNode(node.Name);

                    if (existingNode != null)
                    {
                        if (!isDependency)
                        {
                            continue; // Non-dependencies are not allowed to overwrite sprites
                        }

                        sprites.ReplaceChild(importedNode, existingNode);
                    }
                    else
                    {
                        sprites.AppendChild(importedNode);
                    }
                }
            }

            return new SpriteBank(driver.Process, Graphics.GameAtlas, spriteBankXml) { XMLPath = spritesXmlFilePath };
        }

        private Dictionary<string, PlayerAnimMetadata>? targetFramesMetadata = null;

        /// Modify vanilla method to target our dictionary instead
        /// The method cannot just be copy-pasted, since there are hooks on it, which need to be triggered
        private void IL_PlayerSprite_CreateFramesMetadata(ILContext il)
        {
            var cursor = new ILCursor(il);

            cursor.GotoNext(MoveType.Before,
                instr => instr.MatchCallOrCallvirt<PlayerFramesMetadata>(nameof(PlayerFramesMetadata.Set)));
            cursor.Remove();
            cursor.EmitDelegate((Action<PlayerFramesMetadata, string, PlayerAnimMetadata>)((owner, name, metadata) =>
            {
                var target = targetFramesMetadata;
                if (target != null)
                {
                    target[name] = metadata;
                }
                else
                {
                    owner.Set(name, metadata);
                }
            }));
        }

        private void On_Player_Update(Action<Player> orig, Player self)
        {
            if (Enabled)
            {
                bool shouldBeActive = !CheckMapRequiresSkin();
                bool isActive = gameplayToVisualSprites.TryGetValue(self.Sprite, out _);

                if (shouldBeActive && !isActive)
                {
                    ApplyPlayer(self);
                }
                else if (!shouldBeActive && isActive)
                {
                    RestorePlayer(self);
                }
            }

            orig(self);
        }

        private EverestModuleMetadata GetActiveMod()
        {
            // Use Everest's module to represent vanilla, since null isn't allowed as a key
            var vanillaModule = SourceCoreModule.Instance.Metadata;

            if (SourceTasSceneSessionQuery.GetSession(driver.Process.Frame.Scenes.CurrentScene) is not { } session)
            {
                return vanillaModule;
            }

            // SID -> Mod
            if (!moddedMaps.TryGetValue(AreaKeys.GetSID(AreaKeys.GetSessionArea(session)), out var mod))
            {
                var area = ((Celeste.Game.Content.Maps.OriginalChapterMapData)session.MapData).Area;
                if (Content.TryGet($"Maps/{AreaKeys.GetArea(area).Mode[area.Mode].Path}", out var mapAsset))
                {
                    // The mod source is null for stay .bin maps
                    moddedMaps[AreaKeys.GetSID(AreaKeys.GetSessionArea(session))] =
                        mod = (mapAsset.Source.Mod ?? vanillaModule);
                }
                else
                {
                    moddedMaps[AreaKeys.GetSID(AreaKeys.GetSessionArea(session))] = mod = vanillaModule;
                }
            }

            return mod;
        }

        private bool skipPlayerSpriteHook = false;

        private void On_PlayerSprite_ctor(
            Action<PlayerSprite, Celeste.Core.Scheduling.ISourceEngineContext, PlayerSpriteMode> orig,
            PlayerSprite self,
            Celeste.Core.Scheduling.ISourceEngineContext engine, PlayerSpriteMode mode)
        {
            // Separate gameplay and visual sprite
            if (Enabled && !CheckMapRequiresSkin() && !skipPlayerSpriteHook)
            {
                var mod = GetActiveMod();
                if (!moddedSpriteBanks.TryGetValue(mod, out var spriteBank))
                {
                    moddedSpriteBanks[mod] = spriteBank = CreateSpriteBankForMod(mod);
                }

                var origSpriteBank = Graphics.SpriteBank;
                Graphics.SpriteBank = spriteBank;

                if (!moddedFrameMetadata.ContainsKey(mod))
                {
                    moddedFrameMetadata[mod] = targetFramesMetadata = new Dictionary<string, PlayerAnimMetadata>();
                    Graphics.CreateFramesMetadata("player");
                    Graphics.CreateFramesMetadata("player_no_backpack");
                    Graphics.CreateFramesMetadata("badeline");
                    Graphics.CreateFramesMetadata("player_badeline");
                    Graphics.CreateFramesMetadata("player_playback");
                    targetFramesMetadata = null;
                }

                orig(self, engine, mode);

                // Force-overwrite vanilla sprites
                switch (mode)
                {
                    case PlayerSpriteMode.Madeline:
                        self.SpriteName = "player";
                        spriteBank.CreateOn(self, self.SpriteName);
                        break;
                    case PlayerSpriteMode.MadelineNoBackpack:
                        self.SpriteName = "player_no_backpack";
                        spriteBank.CreateOn(self, self.SpriteName);
                        break;
                    case PlayerSpriteMode.Badeline:
                        self.SpriteName = "badeline";
                        spriteBank.CreateOn(self, self.SpriteName);
                        break;
                    case PlayerSpriteMode.MadelineAsBadeline:
                        self.SpriteName = "player_badeline";
                        spriteBank.CreateOn(self, self.SpriteName);
                        break;
                    case PlayerSpriteMode.Playback:
                        self.SpriteName = "player_playback";
                        spriteBank.CreateOn(self, self.SpriteName);
                        break;
                }

                Graphics.SpriteBank = origSpriteBank;

                skipPlayerSpriteHook = true;
                var visualSprite = new PlayerSprite(driver.Process, mode);
                skipPlayerSpriteHook = false;

                gameplayToVisualSprites.Add(self, visualSprite);
            }
            else
            {
                // Since SkinModHelper+ messes up the PlayerSpriteMode, we have to store it
                actualSpriteMode.Add(self, mode);

                orig(self, engine, mode);
            }
        }

        private void On_Sprite_Update(Action<Sprite> orig, Sprite self)
        {
            if (self is PlayerSprite playerSprite && gameplayToVisualSprites.TryGetValue(playerSprite, out var visual))
            {
                // Forward parameters
                visual.Rate = self.Rate;

                orig(visual);
            }

            orig(self);
        }

        private void On_PlayerSprite_Render(Action<PlayerSprite, Celeste.Core.Rendering.ISourceRenderContext> orig,
            PlayerSprite self, Celeste.Core.Rendering.ISourceRenderContext context)
        {
            if (gameplayToVisualSprites.TryGetValue(self, out var visual))
            {
                // Forward parameters
                visual.SetRawEntity(self.Entity);
                visual.Position = self.Position;
                visual.Justify = self.Justify;
                visual.Origin = self.Origin;
                visual.Scale = self.Scale;
                visual.Color = self.Color;

                orig(visual, context);

                visual.SetRawEntity(null); // Clear to avoid it holding a reference
            }
            else
            {
                orig(self, context);
            }
        }

        // Forward calls to visual sprite
        private void On_Sprite_Play(Action<Sprite, string, bool, bool> orig, Sprite self, string id, bool restart,
            bool randomizeFrame)
        {
            if (self is PlayerSprite playerSprite && gameplayToVisualSprites.TryGetValue(playerSprite, out var visual))
            {
                orig(visual, id, restart, randomizeFrame);
            }

            orig(self, id, restart, randomizeFrame);
        }

        private void On_Sprite_PlayOffset(Action<Sprite, string, float, bool> orig, Sprite self, string id,
            float offset,
            bool randomizeFrame)
        {
            if (self is PlayerSprite playerSprite && gameplayToVisualSprites.TryGetValue(playerSprite, out var visual))
            {
                orig(visual, id, offset, randomizeFrame);
            }

            orig(self, id, offset, randomizeFrame);
        }

        private void On_Sprite_Reverse(Action<Sprite, string, bool> orig, Sprite self, string id, bool restart)
        {
            if (self is PlayerSprite playerSprite && gameplayToVisualSprites.TryGetValue(playerSprite, out var visual))
            {
                orig(visual, id, restart);
            }

            orig(self, id, restart);
        }

        // Fetch values from visual sprite
        private bool On_PlayerSprite_getHasHair(Func<PlayerSprite, bool> orig, PlayerSprite self)
        {
            if (gameplayToVisualSprites.TryGetValue(self, out var visual))
            {
                return orig(visual);
            }

            return orig(self);
        }

        private Vector2 On_PlayerSprite_getHairOffset(Func<PlayerSprite, Vector2> orig, PlayerSprite self)
        {
            if (gameplayToVisualSprites.TryGetValue(self, out var visual))
            {
                return orig(visual);
            }

            return orig(self);
        }

        private int On_PlayerSprite_getHairFrame(Func<PlayerSprite, int> orig, PlayerSprite self)
        {
            if (gameplayToVisualSprites.TryGetValue(self, out var visual))
            {
                return orig(visual);
            }

            return orig(self);
        }

        private float On_PlayerSprite_getCarryYOffset(Func<PlayerSprite, float> orig, PlayerSprite self)
        {
            if (gameplayToVisualSprites.TryGetValue(self, out _))
            {
                if (self.Texture != null && moddedFrameMetadata.TryGetValue(GetActiveMod(), out var frameMetadata) &&
                    frameMetadata.TryGetValue(self.Texture.AtlasPath, out var metadata))
                {
                    return metadata.CarryYOffset * self.Scale.Y;
                }

                return 0.0f;
            }

            return orig(self);
        }

        internal void Apply()
        {
            if (GetScenePlayer() is { } player &&
                driver.Settings.PreventSkinModGameplayChanges == GameplayEnableCondition.DuringTAS)
            {
                ApplyPlayer(player);
            }
        }

        internal void Restore()
        {
            if (GetScenePlayer() is { } player &&
                driver.Settings.PreventSkinModGameplayChanges == GameplayEnableCondition.DuringTAS)
            {
                RestorePlayer(player);
            }
        }

        private void ApplyPlayer(Player player)
        {
            var newSprite = new PlayerSprite(driver.Process,
                actualSpriteMode.TryGetValue(player.Sprite, out object? boxedMode)
                    ? (PlayerSpriteMode)boxedMode
                    : player.Sprite.Mode);
            newSprite.CopyPlaybackPhaseFrom(player.Sprite);

            if (gameplayToVisualSprites.TryGetValue(newSprite, out var visualSprite))
            {
                gameplayToVisualSprites.Remove(newSprite);
                gameplayToVisualSprites.AddOrUpdate(player.Sprite, visualSprite);

                player.Sprite.CloneInto(visualSprite);
            }

            newSprite.CloneInto(player.Sprite);

            driver.Logging.Log("Applied vanilla sprite behaviour to player");
        }

        private void RestorePlayer(Player player)
        {
            if (!gameplayToVisualSprites.TryGetValue(player.Sprite, out var visual))
            {
                return;
            }

            gameplayToVisualSprites.Remove(player.Sprite);
            visual.CloneInto(player.Sprite);

            driver.Logging.Log("Restored default sprite behaviour to player");
        }

        private T Property<T>(object target, string name)
        {
            if (target == null) return default;
            var property = driver.SourceModReflection.GetPropertyInfo(target.GetType(), name,
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic);
            if (property == null) return default;
            if (!property.CanRead)
            {
                driver.Logging.Log($"Property '{name}' on type '{target.GetType()}' is not readable",
                    Celeste.Everest.EverestLogLevel.Error);
                return default;
            }

            return (T)property.Read(target);
        }

        private Player GetScenePlayer()
        {
            var scene = (Celeste.Scene.Scenes.SourceSceneState)driver.Process.Frame.Scenes.CurrentScene;
            var players =
                scene.SourceScene.Entities.GetTrackedEntities(typeof(global::Celeste.Simulation.RoomPlayerSession)
                    .FullName);
            return players.Count == 0 ? null : players[0] as Player;
        }
    }
}
