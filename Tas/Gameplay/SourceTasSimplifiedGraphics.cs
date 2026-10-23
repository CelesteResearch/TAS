using Celeste.Core.Display;
using Celeste.Everest.Modules.Capabilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;
using Celeste.Everest.Reflection;
using Celeste.Scene.Entities;
using Celeste.Scene.Tracking;
using Celeste.Game.Entities.Hazards.Spinners.Crystal;
using Celeste.Simulation.Gameplay.Hazards.Spinners.Crystal;
using Celeste.Graphics.Animation;
using Celeste.Graphics.Display;
using StudioCommunication.Util;
using Celeste.Mods.Tas.Collections;

namespace Celeste.Mods.Tas.Gameplay
{
    /// Disables / simplified rendering of various objects to reduce visual noise.
    /// This helps to identify hitboxes more clearly.
    internal sealed class SourceTasSimplifiedGraphics
    {
        private readonly TasPlaybackDriver driver;
        internal SourceTasSimplifiedGraphics(TasPlaybackDriver driver) => this.driver = driver;

        private SourceMemberCatalog Members =>
            ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMembers;

        private SourceAssemblyMethodCatalog Methods =>
            ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMethods;

        private Type GameType(string originalName) => Members.GetActualSourceType(
            Members.GetOriginalTypeDefinition(typeof(OriginalCrystalStaticSpinner)).AssemblyIdentity, originalName);

        private MethodInfo Method(Type type, string name, bool logFailure = true) =>
            Members.ContainsSourceType(type)
                ? Methods.GetMethodInfo(Members, type, name)?.ActualMethod
                : driver.SourceModReflection.GetMethodInfo(type, name, logFailure: logFailure)?.ActualMethod;

        private T Read<T>(object entity, string name) => Members.ContainsSourceType(entity.GetType())
            ? (T)Members.GetMember(entity.GetType(), name, SourceMemberKind.Field,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Read(entity)
            : (T)driver.SourceModReflection.GetFieldInfo(entity.GetType(), name).Read(entity);

        private void Write(object entity, string name, object value)
        {
            if (Members.ContainsSourceType(entity.GetType()))
                Members.GetMember(entity.GetType(), name, SourceMemberKind.Field,
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                    .Write(entity, value);
            else driver.SourceModReflection.GetFieldInfo(entity.GetType(), name).Write(entity, value);
        }

        private object Invoke(object entity, string name) => Members.ContainsSourceType(entity.GetType())
            ? Methods.GetMethodInfo(Members, entity.GetType(), name).Invoke(entity, Array.Empty<object>())
            : driver.SourceModReflection.GetMethodInfo(entity.GetType(), name).Invoke(entity, Array.Empty<object>());

        private List<object> Tracked(object scene, Type type) =>
            Members.ContainsSourceType(type)
                ? ((ISourceScene)scene).SourceScene.Entities.GetTrackedEntities(Members.GetOriginalTypeFullName(type))
                : ((ISourceScene)scene).SourceScene.Entities.GetTrackedEntities(type);


        private MethodInfo ModMethod(string module, string typeName, string methodName)
        {
            var type = driver.SourceModReflection.TryGetType(module, typeName);
            return type == null ? null : Method(type, methodName, logFailure: false);
        }

        private IEnumerable<ConstructorInfo> Constructors(Type type)
        {
            if (!Members.ContainsSourceType(type))
            {
                foreach (var constructor in type.GetConstructors(BindingFlags.Instance | BindingFlags.Public |
                                                                 BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
                             .OrderBy(constructor => constructor.MetadataToken))
                    yield return constructor;
                yield break;
            }

            var sourceType = Members.GetOriginalTypeDefinition(type);
            foreach (var definition in Methods.EnumerateDeclaredDefinitions(sourceType.AssemblyIdentity,
                         sourceType.Identity))
                if (definition.Name == ".ctor")
                    yield return (ConstructorInfo)Methods.RequireActualMethodBase(sourceType.AssemblyIdentity,
                        definition.MethodDefinitionToken);
        }

        private bool Enabled() => driver.Settings.Enabled && driver.Settings.SimplifiedGraphics;

        private bool SpinnerColorEnabled() =>
            Enabled() && driver.Settings.SimplifiedSpinnerColor != SpinnerColor.Default;

        private readonly ConditionalWeakTable<OriginalCrystalStaticSpinner, ReadonlyBox<CrystalSpinnerColor>>
            origVanillaSpinner = new();

        private readonly ConditionalWeakTable<SceneEntityState, ReadonlyBox<(bool Rainbow, PackedColor Tint)>>
            origFrostHelperSpinner = new();

        /// The object represents a boxes `VivHelper.Entities.CustomSpinner.Types`
        private readonly ConditionalWeakTable<SceneEntityState, object> origVivHelperSpinner = new();

        public void Initialize()
        {
            #region Vanilla

            // Hide Badeline laser since it covers the hitbox
            driver.SourceHooks.SkipMethod(
                global::Celeste.Mods.Tas.Reflection.SourceTasDeclaredHookTargets.Require(driver.Process,
                    GameType("Celeste.FinalBossBeam"), "Render", Array.Empty<string>()), () => false);

            #endregion

            #region Spinner Color

            bool OverwriteCrystalHue() => Enabled() && driver.Settings.SimplifiedSpinnerColor == SpinnerColor.White;

            driver.SourceHooks.HookBefore(
                global::Celeste.Mods.Tas.Reflection.SourceTasDeclaredHookTargets.Require(driver.Process,
                    typeof(OriginalCrystalStaticSpinner), "CreateSprites", Array.Empty<string>()),
                (OriginalCrystalStaticSpinner spinner) =>
                {
                    if (SpinnerColorEnabled())
                    {
                        origVanillaSpinner.TryAdd(spinner, new(Read<CrystalSpinnerColor>(spinner, "color")));
                        Write(spinner, "color", driver.Settings.SimplifiedSpinnerColor.ToCrystalSpinnerColor());
                    }
                });
            driver.SourceHooks.OverrideReturn(
                global::Celeste.Mods.Tas.Reflection.SourceTasDeclaredHookTargets.Require(driver.Process,
                    typeof(OriginalCrystalStaticSpinner), "GetHue", new[] { "Microsoft.Xna.Framework.Vector2" }),
                OverwriteCrystalHue, PackedColor.White);

            var maxHelpingHandHue = ModMethod("MaxHelpingHand",
                "Celeste.Mod.MaxHelpingHand.Entities.RainbowSpinnerColorController", "getModHue");
            if (maxHelpingHandHue != null)
                driver.SourceHooks.OverrideReturn(maxHelpingHandHue, OverwriteCrystalHue, PackedColor.White);
            var springCollabHue = ModMethod("SpringCollab2020",
                "Celeste.Mod.SpringCollab2020.Entities.RainbowSpinnerColorController", "getModHue");
            if (springCollabHue != null)
                driver.SourceHooks.OverrideReturn(springCollabHue, OverwriteCrystalHue, PackedColor.White);

            if (driver.SourceModReflection.TryGetType("FrostHelper", "FrostHelper.CustomSpinner") is
                { } t_FrostHelper_CustomSpinner)
            {
                var m_CustomSpinnerSpriteSource_Get =
                    typeof(ISourceSpinnerPresentation).IsAssignableFrom(t_FrostHelper_CustomSpinner)
                        ? null
                        : ModMethod("FrostHelper", "FrostHelper.CustomSpinnerSpriteSource", "Get");

                foreach (var constructor in Constructors(t_FrostHelper_CustomSpinner))
                    driver.SourceHooks.HookAfter(constructor, (SceneEntityState spinner) =>
                    {
                        if (spinner is ISourceSpinnerPresentation presentation)
                        {
                            if (ReferenceEquals(presentation.EngineContext, driver.Process) && SpinnerColorEnabled())
                            {
                                origFrostHelperSpinner.TryAdd(spinner, new((presentation.Rainbow, presentation.Tint)));
                                presentation.Rainbow = false;
                                presentation.Tint = PackedColor.White;
                            }

                            return;
                        }

                        if (SpinnerColorEnabled())
                        {
                            origFrostHelperSpinner.TryAdd(spinner,
                                new((Read<bool>(spinner, "Rainbow"), Read<PackedColor>(spinner, "Tint"))));
                            Write(spinner, "Rainbow", false);
                            Write(spinner, "Tint", PackedColor.White);
                        }
                    });
                driver.SourceHooks.OnHook(Method(t_FrostHelper_CustomSpinner, "CreateSprites"),
                    (Action<Action<SceneEntityState>, SceneEntityState>)((orig, self) =>
                    {
                        if (self is ISourceSpinnerPresentation presentation)
                        {
                            if (ReferenceEquals(presentation.EngineContext, driver.Process) && SpinnerColorEnabled())
                                presentation.BuildGraphicsWithSource(
                                    $"danger/crystal>_{driver.Settings.SimplifiedSpinnerColor.ToPathName()}", orig);
                            else orig(self);
                            return;
                        }

                        if (SpinnerColorEnabled())
                        {
                            object origSpriteSource = Read<object>(self, "SpriteSource")!;
                            Write(self, "SpriteSource",
                                m_CustomSpinnerSpriteSource_Get!.Invoke(null,
                                [
                                    $"danger/crystal>_{driver.Settings.SimplifiedSpinnerColor.ToPathName()}", "", false
                                ]));
                            orig(self);
                            Write(self, "SpriteSource", origSpriteSource);
                        }
                        else
                        {
                            orig(self);
                        }
                    }));
            }

            if (driver.SourceModReflection.TryGetType("VivHelper", "VivHelper.Entities.CustomSpinner") is
                { } t_VivHelper_CustomSpinner)
            {
                driver.SourceHooks.OnHook(Method(t_VivHelper_CustomSpinner, "CreateSprites"),
                    (Action<Action<SceneEntityState>, SceneEntityState>)((orig, self) =>
                    {
                        if (SpinnerColorEnabled())
                        {
                            string origDirectory = Read<string>(self, "directory")!;
                            string origSubDirectory = Read<string>(self, "subdirectory")!;

                            origVivHelperSpinner.TryAdd(self, Read<object>(self, "type")!);
                            Write(self, "type", /* Types.White */ 0);

                            Write(self, "fgdirectory", "danger/crystal/fg_");
                            Write(self, "bgdirectory", "danger/crystal/bg_");
                            Write(self, "subdirectory", driver.Settings.SimplifiedSpinnerColor.ToPathName());

                            orig(self);

                            // Ensure all sprites have no tint
                            foreach (var image in self.OwnedComponents.GetAll<SpriteImage>())
                            {
                                image.Color = PackedColor.White;
                            }

                            if (Read<SceneEntityState>(self, "filler") is { } filler)
                            {
                                foreach (var image in filler.OwnedComponents.GetAll<SpriteImage>())
                                {
                                    image.Color = PackedColor.White;
                                }
                            }

                            // Remove border
                            Read<SceneEntityState>(self, "border")?.RemoveSelf();
                            Write(self, "border", null);

                            Write(self, "fgdirectory", origDirectory + "/fg");
                            Write(self, "bgdirectory", origDirectory + "/bg");
                            Write(self, "subdirectory", origSubDirectory);
                        }
                        else
                        {
                            orig(self);
                        }
                    }));
            }

            #endregion

            #region Extended Variants

            if (ModMethod("ExtendedVariantMode", "ExtendedVariants.Variants.BlurLevel", "blurLevelBuffer") is
                { } skip158)
                driver.SourceHooks.SkipMethod(skip158, Enabled);
            if (ModMethod("ExtendedVariantMode",
                    "Celeste.Mods.ExtendedVariants.Runtime.VisualVariants.LayerEffectHooks",
                    "BlurBackground") is { } skip159) driver.SourceHooks.SkipMethod(skip159, Enabled);

            if (ModMethod("ExtendedVariantMode",
                    "Celeste.Mods.ExtendedVariants.Runtime.VisualVariants.EffectValueHooks", "GetBloomBase") is
                { } modBloomBase)
                driver.SourceHooks.OverrideReturn(modBloomBase,
                    () => Enabled() && driver.Settings.SimplifiedBloomBase.HasValue,
                    () => driver.Settings.SimplifiedBloomBase!.Value / 10.0f);
            if (ModMethod("ExtendedVariantMode",
                    "Celeste.Mods.ExtendedVariants.Runtime.VisualVariants.EffectValueHooks", "GetBloomStrength") is
                { } modBloomStrength)
                driver.SourceHooks.OverrideReturn(modBloomStrength,
                    () => Enabled() && driver.Settings.SimplifiedBloomStrength.HasValue,
                    () => driver.Settings.SimplifiedBloomStrength!.Value / 10.0f);

            #endregion

            #region Kosei Helper

            if (ModMethod("KoseiHelper", "Celeste.Mod.KoseiHelper.Entities.DebugRenderer",
                    nameof(SceneEntityState.Render)) is { } skip169) driver.SourceHooks.SkipMethod(skip169, Enabled);
            if (ModMethod("KoseiHelper", "Celeste.Mod.KoseiHelper.Entities.DebugRenderer",
                    nameof(SceneEntityState.DebugRender)) is
                { } skip170) driver.SourceHooks.SkipMethod(skip170, Enabled);

            #endregion
        }

        public void OnSpinnerColorChanged()
        {
            if (driver.Process.Frame.Scenes.CurrentScene is not { } scene)
            {
                return;
            }

            var spinnerColor = driver.Settings.SimplifiedSpinnerColor;
            bool enabled = SpinnerColorEnabled();

            foreach (var entity in ((ISourceScene)scene).SourceScene.Entities.GetTrackedEntities(
                         "Celeste.CrystalStaticSpinner"))
            {
                if (entity is ISourceSpinnerPresentation) continue;
                var spinner = (OriginalCrystalStaticSpinner)entity;
                var newColor = enabled
                    ? spinnerColor.ToCrystalSpinnerColor()
                    : origVanillaSpinner.TryGetValue(spinner, out var origColor)
                        ? origColor.Value
                        : Read<CrystalSpinnerColor>(spinner, "color");

                if (Read<CrystalSpinnerColor>(spinner, "color") == newColor)
                {
                    continue;
                }

                origVanillaSpinner.TryAdd(spinner, new(Read<CrystalSpinnerColor>(spinner, "color")));
                Write(spinner, "color", newColor);
                Write(spinner, "expanded", false);
                spinner.OwnedComponents.RemoveAll<SpriteImage>();
                Read<SceneEntityState>(spinner, "border").RemoveSelf();
                global::Celeste.Mods.Tas.Reflection.SourceTasDeclaredHookTargets.Require(
                        driver.Process, typeof(OriginalCrystalStaticSpinner), "CreateSprites", Array.Empty<string>())
                    .Invoke(spinner, Array.Empty<object>());
            }

            if (driver.SourceModReflection.TryGetType("FrostHelper", "FrostHelper.CustomSpinner") is
                { } t_FrostHelper_CustomSpinner)
            {
                var m_CustomSpinnerSpriteSource_Get =
                    typeof(ISourceSpinnerPresentation).IsAssignableFrom(t_FrostHelper_CustomSpinner)
                        ? null
                        : ModMethod("FrostHelper", "FrostHelper.CustomSpinnerSpriteSource", "Get")!;

                foreach (var actualSpinner in Tracked(scene, t_FrostHelper_CustomSpinner))
                {
                    SceneEntityState spinner = (SceneEntityState)actualSpinner;
                    if (spinner is ISourceSpinnerPresentation presentation)
                    {
                        if (!ReferenceEquals(presentation.EngineContext, driver.Process)) continue;
                        if (enabled)
                        {
                            origFrostHelperSpinner.TryAdd(spinner, new((presentation.Rainbow, presentation.Tint)));
                            presentation.Rainbow = false;
                            presentation.Tint = PackedColor.White;
                            presentation.RebuildGraphics(
                                $"danger/crystal>_{driver.Settings.SimplifiedSpinnerColor.ToPathName()}");
                        }
                        else
                        {
                            if (origFrostHelperSpinner.TryGetValue(spinner, out var originalTint))
                            {
                                presentation.Rainbow = originalTint.Value.Rainbow;
                                presentation.Tint = originalTint.Value.Tint;
                            }

                            presentation.RebuildGraphics(null);
                        }

                        continue;
                    }

                    if (!enabled)
                    {
                        if (origFrostHelperSpinner.TryGetValue(spinner, out var origTint))
                        {
                            Write(spinner, "Rainbow", origTint.Value.Rainbow);
                            Write(spinner, "Tint", origTint.Value.Tint);
                        }

                        Read<List<SpriteImage>>(spinner, "_images")?.Clear();
                        Write(spinner, "expanded", false);
                        Write(spinner, "filler", null);
                        Invoke(spinner, "CreateSprites");
                        continue;
                    }

                    origFrostHelperSpinner.TryAdd(spinner,
                        new((Read<bool>(spinner, "Rainbow"), Read<PackedColor>(spinner, "Tint"))));
                    Write(spinner, "Rainbow", false);
                    Write(spinner, "Tint", PackedColor.White);

                    object origSpriteSource = Read<object>(spinner, "SpriteSource")!;
                    Write(spinner, "SpriteSource",
                        m_CustomSpinnerSpriteSource_Get.Invoke(null,
                            [$"danger/crystal>_{driver.Settings.SimplifiedSpinnerColor.ToPathName()}", "", false]));
                    Read<List<SpriteImage>>(spinner, "_images")?.Clear();
                    Write(spinner, "expanded", false);
                    Write(spinner, "filler", null);
                    Invoke(spinner, "CreateSprites");
                    Write(spinner, "SpriteSource", origSpriteSource);
                }
            }

            if (driver.SourceModReflection.TryGetType("VivHelper", "VivHelper.Entities.CustomSpinner") is
                { } t_VivHelper_CustomSpinner)
            {
                foreach (var actualSpinner in Tracked(scene, t_VivHelper_CustomSpinner))
                {
                    SceneEntityState spinner = (SceneEntityState)actualSpinner;
                    if (!enabled)
                    {
                        if (origVivHelperSpinner.TryGetValue(spinner, out object? origType))
                        {
                            Write(spinner, "type", origType);
                        }

                        Write(spinner, "expanded", false);
                        spinner.OwnedComponents.RemoveAll<SpriteImage>();
                        Read<SceneEntityState>(spinner, "border")?.RemoveSelf();
                        Invoke(spinner, "CreateSprites");
                        continue;
                    }

                    string origDirectory = Read<string>(spinner, "directory")!;
                    string origSubDirectory = Read<string>(spinner, "subdirectory")!;

                    origVivHelperSpinner.TryAdd(spinner, Read<object>(spinner, "type")!);
                    Write(spinner, "type", /* Types.White */ 0);

                    Write(spinner, "fgdirectory", "danger/crystal/fg_");
                    Write(spinner, "bgdirectory", "danger/crystal/bg_");
                    Write(spinner, "subdirectory", driver.Settings.SimplifiedSpinnerColor.ToPathName());

                    Write(spinner, "expanded", false);
                    spinner.OwnedComponents.RemoveAll<SpriteImage>();
                    Read<SceneEntityState>(spinner, "border")?.RemoveSelf();
                    Invoke(spinner, "CreateSprites");

                    Write(spinner, "fgdirectory", origDirectory + "/fg");
                    Write(spinner, "bgdirectory", origDirectory + "/bg");
                    Write(spinner, "subdirectory", origSubDirectory);
                }
            }
        }
    }
}
