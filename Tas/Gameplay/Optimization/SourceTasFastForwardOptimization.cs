using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Celeste.Core.Framework.GarbageCollection;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Celeste.Core.Randomness;
using Celeste.Simulation.Audio.Emitters;
using Celeste.Simulation.Presentation.Particles.Definitions;
using Celeste.Simulation.Gameplay.Hazards.Spinners.Dust;

namespace Celeste.Mods.Tas.Gameplay.Optimization
{
    internal sealed class SourceTasFastForwardOptimization
    {
        private readonly TasPlaybackDriver driver;
        private readonly SourceTasOptimizationMethods methods;
        private Hook soundUpdate;
        private ILHook transitionGC, reloadGC;
        private Type sjCreditsType;

        internal SourceTasFastForwardOptimization(TasPlaybackDriver driver, SourceTasOptimizationMethods methods)
        {
            this.driver = driver;
            this.methods = methods;
        }

        internal bool Active => driver.FastForwarding || driver.SourceSyncChecker.Active;
        private bool IgnoreGarbageCollect => Active && driver.Settings.IgnoreGcCollect;

        internal void Initialize()
        {
            SkipMethod(methods.Require("Monocle.ParticleSystem", "Update"));
            SkipMethod(methods.Require("Monocle.ParticleSystem", "Clear"));
            SkipMethod(methods.Require("Monocle.ParticleSystem", "ClearRect", "Microsoft.Xna.Framework.Rectangle",
                "System.Boolean"));
            foreach (var definition in methods.Declared("Monocle.ParticleSystem").Where(m => m.Name == "Emit"))
            {
                var parameters = definition.Parameters;
                var method = methods.Actual(definition);
                if (parameters.All(p => p.Name is not ("type" or "positionRange")))
                {
                    SkipMethod(method);
                    continue;
                }

                driver.SourceHooks.IlHook(method, (cursor, _) =>
                {
                    var start = cursor.MarkLabel();
                    cursor.MoveBeforeLabels();
                    cursor.EmitDelegate(() => Active);
                    cursor.Emit(OpCodes.Brfalse, start);
                    int amountIndex =
                        FindActualArgument(method, parameters.FirstOrDefault(p => p.Name == "amount")?.Name);
                    if (amountIndex != -1)
                    {
                        cursor.Emit(OpCodes.Ldarg, amountIndex);
                        cursor.EmitDelegate<Action<int>>(SimulateRandomRangeCalls);
                    }

                    int typeIndex = FindActualArgument(method, parameters.FirstOrDefault(p => p.Name == "type")?.Name);
                    if (typeIndex != -1)
                    {
                        cursor.Emit(OpCodes.Ldarg, typeIndex);
                        cursor.EmitDelegate<Action<ParticleTypePresentation>>(SimulateTypeCreateCalls);
                    }

                    cursor.Emit(OpCodes.Ret);
                });
            }

            SkipMethod(methods.Require("Celeste.SeekerBarrierRenderer", "Update"));
            soundUpdate = new Hook(methods.Require("Celeste.SoundEmitter", "Update"),
                (Action<Action<SoundEmitterState>, SoundEmitterState>)((original, self) =>
                {
                    if (driver.FastForwarding) self.RemoveSelf();
                    else original(self);
                }));
            foreach (var target in new[]
                     {
                         ("Celeste.ReflectionTentacles", "Update"), ("Celeste.Decal", "Update"),
                         ("Celeste.FloatingDebris", "Update"),
                         ("Celeste.AnimatedTiles", "Update"), ("Celeste.Water+Surface", "Update"),
                         ("Celeste.LavaRect", "Update"),
                         ("Celeste.CliffsideWindFlag", "Update"), ("Celeste.CrystalStaticSpinner", "UpdateHue"),
                         ("Celeste.Snow3D", "Update"), ("Celeste.AutoSplitterInfo", "Update"),
                         ("Celeste.SeekerBarrier", "Update")
                     })
                SkipMethod(methods.Require(target.Item1, target.Item2));
            SkipMethod(methods.Require("Celeste.HiresSnow", "Update", "Monocle.Scene"));
            SkipMethod(methods.Require("Celeste.DustGraphic", "Update"), cursor =>
            {
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.EmitDelegate<Action<DustGraphicState>>(dust =>
                {
                    if (dust.Nodes.Count <= 0 && dust.Parent.Scene != null && !dust.Estableshed)
                        dust.AddDustNodesIfInCamera();
                });
            });
            transitionGC =
                new ILHook(methods.Require("Monocle.Engine", "OnSceneTransition", "Monocle.Scene", "Monocle.Scene"),
                    SkipSceneTransitionCollection);
            reloadGC = new ILHook(methods.Require("Celeste.Level", "Reload"), SkipReloadCollection);
            var collect = methods.Optional("Celeste.Level", "_GCCollect");
            if (collect != null)
                driver.SourceHooks.IlHook(collect, (cursor, _) =>
                {
                    var start = cursor.MarkLabel();
                    cursor.MoveBeforeLabels();
                    cursor.EmitDelegate(() => IgnoreGarbageCollect);
                    cursor.Emit(OpCodes.Brfalse, start);
                    cursor.Emit(OpCodes.Ret);
                });
            var credits = driver.SourceModReflection.TryGetMethod("StrawberryJam2021",
                "Celeste.Mod.StrawberryJam2021.Cutscenes.CS_Credits", "Level_OnLoadEntity");
            if (credits != null) driver.SourceHooks.IlHook(credits.ActualMethod, OptimizeCredits);
        }

        internal void Unload()
        {
            soundUpdate.Dispose();
            transitionGC.Dispose();
            reloadGC.Dispose();
        }

        private static int FindActualArgument(MethodInfo method, string name)
        {
            if (name == null) return -1;
            var parameters = method.GetParameters();
            int index = Array.FindIndex(parameters, p => p.Name == name);
            if (index < 0)
                throw new InvalidOperationException("Actual particle Emit argument lost original name: " + name);
            var expected = name == "amount" ? typeof(int) : typeof(ParticleTypePresentation);
            if (parameters[index].ParameterType != expected)
                throw new InvalidOperationException("Actual particle Emit argument has unqualified type: " + name);
            return index + (method.IsStatic ? 0 : 1);
        }

        private ISourceRandom Random => ((ISourceRandomEnvironment)driver.Process).Random;

        private void SimulateRandomRangeCalls(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Random.NextDouble();
                Random.NextDouble();
            }
        }

        private void SimulateTypeCreateCalls(ParticleTypePresentation type)
        {
            if (type.SourceChooser != null) Random.NextDouble();
            if (type.SizeRange != 0f) Random.NextDouble();
            if (type.ColorMode == ParticleTypePresentation.ColorModes.Choose) Random.Next(2);
            Random.NextDouble();
            Random.NextDouble();
            Random.NextDouble();
            if (type.RotationMode == ParticleTypePresentation.RotationModes.Random) Random.NextDouble();
            Random.NextDouble();
            if (type.SpinFlippedChance) Random.Next(2);
        }

        internal void SkipMethod(MethodInfo method, Action<ILCursor> preSkipCallback = null)
        {
            if (method == null) return;
#if DEBUG
        System.Diagnostics.Debug.Assert(method.ReturnType == typeof(void));
#endif
            driver.SourceHooks.IlHook(method, (cursor, _) =>
            {
                var start = cursor.MarkLabel();
                cursor.MoveBeforeLabels();
                cursor.EmitDelegate(() => Active);
                cursor.Emit(OpCodes.Brfalse, start);
                preSkipCallback?.Invoke(cursor);
                cursor.Emit(OpCodes.Ret);
            });
        }

        internal void SkipMethods(ReadOnlySpan<MethodInfo> targets)
        {
            foreach (var target in targets) SkipMethod(target);
        }

        internal void SkipMethods(IEnumerable<MethodInfo> targets)
        {
            foreach (var target in targets) SkipMethod(target);
        }

        private void SkipSceneTransitionCollection(ILContext il)
            => SkipPolicyCollection(il, nameof(ISourceGarbageCollectionPolicy.CollectOnSceneTransition));

        private void SkipReloadCollection(ILContext il)
            => SkipPolicyCollection(il, nameof(ISourceGarbageCollectionPolicy.CollectOnReload));

        private void SkipPolicyCollection(ILContext il, string collectionMethod)
        {
            var cursor = new ILCursor(il);
            if (!cursor.TryGotoNext(i => i.MatchCallvirt(typeof(ISourceGarbageCollectionPolicy), collectionMethod)))
                throw new InvalidOperationException(
                    "Actual qualified GC hook lost the collection-policy call: " + collectionMethod);
            // The loaded interface receiver is consumed in both branches.
            cursor.Remove();
            Action<ISourceGarbageCollectionPolicy> collect =
                collectionMethod == nameof(ISourceGarbageCollectionPolicy.CollectOnReload)
                    ? policy => policy.CollectOnReload()
                    : policy => policy.CollectOnSceneTransition();
            cursor.EmitDelegate<Action<ISourceGarbageCollectionPolicy>>(policy =>
            {
                if (!IgnoreGarbageCollect) collect(policy);
            });
        }

        private void OptimizeCredits(ILCursor cursor, ILContext _)
        {
            // These are foreign mod source instructions. Match their original signature,
            // never substitute a ported Game assembly name as a foreign type identity.
            if (!cursor.TryGotoNext(i => i.MatchCallvirt("Monocle.Scene", "get_Entities"),
                    i => i.MatchCallvirt("Monocle.EntityList", "get_ToAdd"),
                    i => i.MatchCall(typeof(Enumerable), nameof(Enumerable.OfType)),
                    i => i.MatchCall(typeof(Enumerable), nameof(Enumerable.FirstOrDefault)))) return;
            sjCreditsType =
                driver.SourceModReflection.TryGetType("StrawberryJam2021",
                    "Celeste.Mod.StrawberryJam2021.Cutscenes.CS_Credits");
            cursor.RemoveRange(4);
            cursor.EmitDelegate<Func<object, object>>(level =>
            {
                var entities = driver.SourceModReflection.GetPropertyInfo(level.GetType(), "Entities").Read(level);
                var toAdd = (System.Collections.IEnumerable)driver.SourceModReflection
                    .GetPropertyInfo(entities.GetType(), "ToAdd").Read(entities);
                foreach (var entity in toAdd)
                    if (entity.GetType() == sjCreditsType)
                        return entity;
                return null;
            });
        }
    }
}
