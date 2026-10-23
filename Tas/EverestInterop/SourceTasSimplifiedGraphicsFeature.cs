using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Components;
using Celeste.Core.Randomness;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;
using Celeste.Everest.Reflection;
using Celeste.Game.Entities.Hazards.Spinners.Crystal;
using Celeste.Game.Execution.Levels;
using Celeste.Scene.Entities;
using Celeste.Scene.Tracking;
using MonoMod.RuntimeDetour;

namespace Celeste.Mods.Tas.EverestInterop
{
    public sealed partial class SourceTasSimplifiedGraphicsFeature
    {
        private readonly TasPlaybackDriver driver;
        private readonly List<Type> clutteredTypes;
        private bool lastSimplifiedGraphics;
        private SolidTilesStyle currentSolidTilesStyle;
        private bool creatingSolidTiles;

        internal SourceTasSimplifiedGraphicsFeature(TasPlaybackDriver driver)
        {
            this.driver = driver;
            clutteredTypes =
            [
                GameType("Celeste.FloatingDebris"), GameType("Celeste.MoonCreature"), GameType("Celeste.ResortLantern")
            ];
            lastSimplifiedGraphics = driver.Settings.SimplifiedGraphics;
        }

        private SourceMemberCatalog Members =>
            ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMembers;

        private SourceAssemblyMethodCatalog Methods =>
            ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMethods;

        private Type GameType(string name) =>
            Members.GetActualSourceType(
                Members.GetOriginalTypeDefinition(typeof(OriginalCrystalStaticSpinner)).AssemblyIdentity, name);

        private MethodInfo
            Method(Type type, string name, string[] originalParameters = null, bool originalStatic = false) =>
            global::Celeste.Mods.Tas.Reflection.SourceTasDeclaredHookTargets.Require(driver.Process, type, name,
                originalParameters, originalStatic);

        private MethodInfo ModMethod(string mod, string typeName, string name)
        {
            var type = driver.SourceModReflection.TryGetType(mod, typeName);
            return type == null ? null : driver.SourceModReflection.GetMethodInfo(type, name)?.ActualMethod;
        }

        private SourcePhysicalMemberBinding Field(Type type, string name) => Members.GetMember(type, name,
            SourceMemberKind.Field,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

        private T Read<T>(object target, string name) => Members.ContainsSourceType(target.GetType())
            ? (T)Field(target.GetType(), name).Read(target)
            : (T)driver.SourceModReflection.GetFieldInfo(target.GetType(), name).Read(target);

        private void Write(object target, string name, object value)
        {
            if (Members.ContainsSourceType(target.GetType())) Field(target.GetType(), name).Write(target, value);
            else driver.SourceModReflection.GetFieldInfo(target.GetType(), name).Write(target, value);
        }

        private bool IsSimplifiedGraphics() => driver.Settings.SimplifiedGraphics;
        private bool IsSimplifiedParticle() => driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedParticle;
        private bool IsSimplifiedDistort() => driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedDistort;
        private bool IsSimplifiedDecal() => driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedDecal;

        private bool IsSimplifiedMiniTextbox() =>
            driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedMiniTextbox;

        private bool SimplifiedWavedBlock() =>
            driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedWavedEdge;

        private bool IsSimplifiedLightningStrike() =>
            driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedLightningStrike;

        private bool IsSimplifiedClutteredEntity() =>
            driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedClutteredEntity;

        private bool IsSimplifiedHud() => driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedHud ||
                                          driver.Settings.CenterCamera &&
                                          Math.Abs(driver.SourceCenterCamera.ZoomLevel - 1f) > 1e-3;

        private void OnSimplifiedGraphicsChanged(bool simplifiedGraphics)
        {
            if (driver.Process.Frame.Scenes.CurrentScene is not OriginalLevelSceneRoot level) return;
            if (simplifiedGraphics && driver.Settings.SimplifiedClutteredEntity)
            {
                IEnumerable<object> clutteredEntities =
                    level.SourceScene.Entities.Where(e => clutteredTypes.Any(t => t.IsAssignableFrom(e.GetType())));
                foreach (object entity in clutteredEntities) ((SceneEntityState)entity).RemoveSelf();
            }

            if (simplifiedGraphics && currentSolidTilesStyle != driver.Settings.SimplifiedSolidTilesStyle ||
                !simplifiedGraphics && currentSolidTilesStyle != default) ReplaceSolidTilesStyle();
        }

        public void ReplaceSolidTilesStyle()
        {
            if (driver.Process.Frame.Scenes.CurrentScene is not OriginalLevelSceneRoot
                {
                    SolidTiles: { } solidTiles
                } level) return;
            var random = ((ISourceRandomProcessEnvironment)driver.Process).RandomProcess;
            random.PushRandom();
            var newSolidTiles =
                level.CreateSourceSolidTiles(new Vector2(level.TileBounds.X, level.TileBounds.Y) * 8f,
                    level.SolidsData);
            if (solidTiles.Tiles is { } tiles)
            {
                tiles.RemoveSelf();
                newSolidTiles.Tiles.VisualExtend = tiles.VisualExtend;
                newSolidTiles.Tiles.ClipCamera = tiles.ClipCamera;
            }

            if (solidTiles.AnimatedTiles is { } animatedTiles)
            {
                animatedTiles.RemoveSelf();
                newSolidTiles.AnimatedTiles.ClipCamera = animatedTiles.ClipCamera;
            }

            solidTiles.OwnedComponents.Add(solidTiles.Tiles = newSolidTiles.Tiles);
            solidTiles.OwnedComponents.Add(solidTiles.AnimatedTiles = newSolidTiles.AnimatedTiles);
            random.PopRandom();
        }

        private void LevelUpdated(OriginalLevelSceneRoot level)
        {
            if (lastSimplifiedGraphics != driver.Settings.SimplifiedGraphics)
            {
                OnSimplifiedGraphicsChanged(driver.Settings.SimplifiedGraphics);
                lastSimplifiedGraphics = driver.Settings.SimplifiedGraphics;
            }
        }

        private void SolidTilesConstructing()
        {
            if (driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedSolidTilesStyle != default)
                currentSolidTilesStyle = driver.Settings.SimplifiedSolidTilesStyle;
            else currentSolidTilesStyle = SolidTilesStyle.All[0];
            creatingSolidTiles = true;
        }

        private void SolidTilesConstructed() => creatingSolidTiles = false;

        private void AddClutterRemoval(SceneEntityState entity)
        {
            if (IsSimplifiedClutteredEntity()) entity.OwnedComponents.Add(new SourceTasRemoveSelfComponent());
        }

        private static readonly string[] SolidDecals =
        [
            "3-resort/bridgecolumn",
            "3-resort/bridgecolumntop",
            "3-resort/brokenelevator",
            "3-resort/roofcenter",
            "3-resort/roofcenter_b",
            "3-resort/roofcenter_c",
            "3-resort/roofcenter_d",
            "3-resort/roofedge",
            "3-resort/roofedge_b",
            "3-resort/roofedge_c",
            "3-resort/roofedge_d",
            "4-cliffside/bridge_a",
        ];
    }
}
