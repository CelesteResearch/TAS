using Celeste.Core.Display;
using System;
using System.Linq;
using System.Reflection;
using System.Numerics;
using Celeste.Core.Reflection.Members;
using Celeste.Everest.Reflection;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Everest.Input.Bindings;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Stories.OldSite.DreamMirror;
using Celeste.Game.Gameplay.Enemies.BadelineBoss;
using Celeste.Game.Presentation.Lighting;
using Celeste.Scene.Entities;
using Celeste.Simulation;
using Celeste.Graphics.Display;
using MonoMod.RuntimeDetour;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace Celeste.Mods.Tas.EverestInterop
{
    internal sealed class SourceTasDesyncFixerProcess
    {
        private readonly TasPlaybackDriver driver;
        internal SourceTasDesyncFixerProcess(TasPlaybackDriver driver) => this.driver = driver;
        private Hook setOccluder, setCutout;

        private SourceMemberCatalog Members =>
            ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMembers;

        private MethodInfo Method(Type type, string name) =>
            global::Celeste.Mods.Tas.Reflection.SourceTasDeclaredHookTargets
                .Require(driver.Process, type, name);

        private MethodInfo ForeignMethod(Type type, string name) =>
            driver.SourceModReflection.GetMethodInfo(type, name)?.ActualMethod;

        private T Read<T>(object owner, string originalName) => Members.ContainsSourceType(owner.GetType())
            ? (T)Members.GetFieldInfo(owner.GetType(), originalName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Read(owner)
            : (T)driver.SourceModReflection.GetFieldInfo(owner.GetType(), originalName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Read(owner);

        private void Invoke(object owner, Type canonicalSourceOwner, string originalName) =>
            global::Celeste.Mods.Tas.Reflection.SourceTasDeclaredHookTargets.Require(
                    driver.Process, canonicalSourceOwner, originalName, Array.Empty<string>())
                .Invoke(owner, Array.Empty<object>());

        private ConstructorInfo FirstPublicMemoConstructor()
        {
            var methods = ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMethods;
            string assembly = Members.GetOriginalTypeDefinition(typeof(OriginalLevelSceneRoot)).AssemblyIdentity;
            var sourceCtor = methods.EnumerateDeclaredDefinitions(assembly, "Celeste.CS03_Memo+MemoPage")
                .First(definition => definition.Name == ".ctor" && (definition.Attributes & 7) == 6);
            return (ConstructorInfo)methods.RequireActualMethodBase(assembly, sourceCtor.MethodDefinitionToken);
        }

        private void InstallEmoteHook(Type type)
        {
            var method =
                driver.SourceModReflection.GetMethodInfo(type, "Player_Update");
            if (method != null) driver.SourceHooks.IlHook(method.ActualMethod, PreventEmoteMod);
        }

        private delegate void SetOccluderOrig(LightingRendererPresentation self, Vector3 center, PackedColor mask,
            Vector2 light, Vector2 edgeA, Vector2 edgeB);

        private delegate void SetOccluderHook(SetOccluderOrig original, LightingRendererPresentation self,
            Vector3 center,
            PackedColor mask, Vector2 light, Vector2 edgeA, Vector2 edgeB);

        private delegate void SetCutoutOrig(LightingRendererPresentation self, Vector3 center, PackedColor mask,
            Vector2 light, float x, float y, float width, float height);

        private delegate void SetCutoutHook(SetCutoutOrig original, LightingRendererPresentation self, Vector3 center,
            PackedColor mask, Vector2 light, float x, float y, float width, float height);

        internal void Initialize()
        {
            if (driver.SourceModReflection.GetModule("DeadzoneConfig")?.GetType() is { } deadzoneConfigModuleType)
            {
                driver.SourceHooks.SkipMethod(ForeignMethod(deadzoneConfigModuleType, "OnInputInitialize"),
                    SkipDeadzoneConfig);
            }

            // https://discord.com/channels/403698615446536203/519281383164739594/1154486504475869236
            if (driver.SourceModReflection.TryGetType("EmoteMod", "Celeste.Mod.EmoteMod.EmoteWheelModule") is
                { } emoteModuleType)
            {
                InstallEmoteHook(emoteModuleType);
            }
        }


        internal void Load()
        {
            driver.SourceHooks.HookAfter<DreamMirrorState>(Method(typeof(DreamMirrorState), "Added"),
                FixDreamMirrorDesync);
            driver.SourceHooks.HookAfter<SceneEntityState>(FirstPublicMemoConstructor(), FixMemoPageCrash);
            driver.SourceHooks.HookAfter<FinalBossState>(Method(typeof(FinalBossState), "Added"), FixFinalBossDesync);

            // Avoid advancing time while loading
            driver.SourceHooks.SkipMethod(Method(typeof(OriginalLevelSceneRoot), "UpdateTime"),
                driver.GameHost.Loading);

            // System.IndexOutOfRangeException: Index was outside the bounds of the array.
            // https://discord.com/channels/403698615446536203/1148931167983251466/1148931167983251466
            setOccluder = new Hook(Method(typeof(LightingRendererPresentation), "SetOccluder"),
                (SetOccluderHook)IgnoreSetOccluderCrash);
            setCutout = new Hook(Method(typeof(LightingRendererPresentation), "SetCutout"),
                (SetCutoutHook)IgnoreSetCutoutCrash);
        }


        internal void Unload()
        {
            setOccluder.Dispose();
            setOccluder = null;
            setCutout.Dispose();
            setCutout = null;
        }

        private void FixDreamMirrorDesync(DreamMirrorState mirror)
        {
            mirror.OwnedComponents.Add(new ScenePostUpdateHook(() =>
            {
                if (driver.Running)
                {
                    // DreamMirror does some dirty stuff in BeforeRender.
                    Invoke(mirror, typeof(DreamMirrorState), "BeforeRender");
                }
            }));
        }

        private void FixFinalBossDesync(FinalBossState finalBoss)
        {
            finalBoss.OwnedComponents.Add(new ScenePostUpdateHook(() =>
            {
                if (!driver.Running)
                {
                    return;
                }

                // FinalBoss does some dirty stuff in Render.
                // finalBoss.ShotOrigin => base.Center + Sprite.Position + new Vector2(6f * Sprite.Scale.X, 2f);
                if (finalBoss.Sprite is { } sprite)
                {
                    sprite.Scale.X = Read<int>(finalBoss, "facing");
                    sprite.Scale.Y = 1f;
                    sprite.Scale *= 1f + finalBoss.ScaleWiggler.Value * 0.2f;
                }
            }));
        }

        private void FixMemoPageCrash(SceneEntityState memoPage)
        {
            memoPage.OwnedComponents.Add(new ScenePostUpdateHook(() =>
            {
                if (driver.Running && Read<object>(memoPage, "target") == null)
                {
                    // initialize memoPage.target, fix game crash when fast forward
                    Invoke(memoPage,
                        global::Celeste.Mods.Tas.Reflection.SourceTasSeedAndDesyncActualMethodBindings
                            .MemoPageActualType, "BeforeRender");
                }
            }));
        }

        private bool SkipDeadzoneConfig()
        {
            return driver.Running;
        }

        private void IgnoreSetOccluderCrash(SetOccluderOrig orig, LightingRendererPresentation self, Vector3 center,
            PackedColor mask, Vector2 light, Vector2 edgeA, Vector2 edgeB)
        {
            try
            {
                orig(self, center, mask, light, edgeA, edgeB);
            }
            catch (IndexOutOfRangeException e)
            {
                if (driver.Running)
                {
                    ((IEverestLoggerProcessEnvironment)driver.Process).Logger.LogDetailed(
                        global::Celeste.Everest.EverestLogLevel.Debug, "CelesteTAS", e.ToString());
                }
                else
                {
                    throw;
                }
            }
        }

        private void IgnoreSetCutoutCrash(SetCutoutOrig orig, LightingRendererPresentation self, Vector3 center,
            PackedColor mask, Vector2 light, float x, float y, float width, float height)
        {
            try
            {
                orig(self, center, mask, light, x, y, width, height);
            }
            catch (IndexOutOfRangeException e)
            {
                if (driver.Running)
                {
                    ((IEverestLoggerProcessEnvironment)driver.Process).Logger.LogDetailed(
                        global::Celeste.Everest.EverestLogLevel.Debug, "CelesteTAS", e.ToString());
                }
                else
                {
                    throw;
                }
            }
        }

        private void PreventEmoteMod(ILCursor ilCursor, ILContext ilContext)
        {
            if (ilCursor.TryGotoNext(
                    ins => ins.OpCode == OpCodes.Call,
                    ins => ins.OpCode == OpCodes.Callvirt &&
                           ins.Operand.ToString()!.Contains("::get_EmoteWheelBinding()"),
                    ins => ins.OpCode == OpCodes.Callvirt,
                    ins => ins.OpCode == OpCodes.Callvirt && ins.Operand.ToString()!.Contains("::get_Count()")
                ))
            {
                ilCursor.Index += 2;
                ilCursor.Emit(OpCodes.Dup);
                ilCursor.Index += 2;
                ilCursor.EmitDelegate(IsEmoteWheelBindingPressed);
            }
        }

        private int IsEmoteWheelBindingPressed(EverestButtonBinding binding, int count)
        {
            return binding.Pressed ? count : 0;
        }
    }
}
