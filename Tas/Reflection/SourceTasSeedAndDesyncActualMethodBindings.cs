using Celeste.Core.Display;
using System;
using System.Collections;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;
using Celeste.Core.Scheduling;
using Celeste.Everest.Gameplay.Effects.Debris;
using Celeste.Game.Entities.Ascension;
using Celeste.Game.Entities.Bridges;
using Celeste.Game.Gameplay.Enemies.BadelineBoss;
using Celeste.Game.Presentation.Lighting;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Profiles;
using Celeste.Game.Stories.Interactions;
using Celeste.Game.Stories.OldSite.DreamMirror;
using Celeste.Simulation.Gameplay.Actors.Debris;
using Celeste.Simulation.Gameplay.Effects.Debris;
using Celeste.Simulation.Gameplay.Platforms.BounceBlock;
using Celeste.Simulation.Gameplay.Platforms.MoveBlock;
using Celeste.Graphics.Content.VirtualAssets;
using Celeste.Graphics.Display;

namespace Celeste.Mods.Tas.Reflection
{
    public static class SourceTasSeedAndDesyncActualMethodBindings
    {
        private const string GameAssembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        private const string Vector2Source = "Microsoft.Xna.Framework.Vector2";
        private const string Vector3Source = "Microsoft.Xna.Framework.Vector3";
        private const string ColorSource = "Microsoft.Xna.Framework.Color";

        private const BindingFlags InstanceDeclared = BindingFlags.Public | BindingFlags.NonPublic
                                                                          | BindingFlags.Instance |
                                                                          BindingFlags.DeclaredOnly;

        public static Type MemoPageActualType => typeof(CS03MemoState).GetNestedType("MemoPage", BindingFlags.NonPublic)
                                                 ?? throw new TypeLoadException(
                                                     "The complete source MemoPage type is missing.");

        public static Type AscendRoutineActualStateMachineType => AscendRoutine()
                                                                      .GetCustomAttribute<
                                                                          IteratorStateMachineAttribute>()
                                                                      ?.StateMachineType
                                                                  ?? throw new InvalidOperationException(
                                                                      "The complete source ascend routine has no actual iterator state machine.");

        public static void Register(GameApplication process)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));
            bool installed = process.SourceProfile == GameProfile.InstalledEverest;
            var methods = process.CanonicalSourceMethods;
            var members = process.SourceMembers;
            string[] debrisParameters = { Vector2Source, "System.Char", "System.Boolean" };
            Type[] debrisActualParameters = { typeof(Vector2), typeof(char), typeof(bool) };
            if (installed)
            {
                Bind(methods, 100673257, "Celeste.Debris", "orig_Init", "Celeste.Debris", debrisParameters,
                    typeof(DebrisState), nameof(DebrisState.Init), typeof(DebrisState), debrisActualParameters);
                Bind(methods, 100673250, "Celeste.Debris", "Init", "Celeste.Debris", debrisParameters,
                    typeof(EverestDebrisState), nameof(EverestDebrisState.Init), typeof(DebrisState),
                    debrisActualParameters);
                Bind(methods, 100673256, "Celeste.Debris", "Init", "Celeste.Debris",
                    new[] { Vector2Source, "System.Char" }, typeof(EverestDebrisState), nameof(EverestDebrisState.Init),
                    typeof(DebrisState), new[] { typeof(Vector2), typeof(char) });
            }
            else
                Bind(methods, 100668969, "Celeste.Debris", "Init", "Celeste.Debris", debrisParameters,
                    typeof(DebrisState), nameof(DebrisState.Init), typeof(DebrisState), debrisActualParameters);

            Bind(methods, installed ? 100673251 : 100668970, "Celeste.Debris", "BlastFrom", "Celeste.Debris",
                new[] { Vector2Source }, typeof(DebrisState), nameof(DebrisState.BlastFrom),
                typeof(DebrisState), new[] { typeof(Vector2) });
            Bind(methods, installed ? 100666915 : 100671191, "Celeste.BounceBlock+RespawnDebris", "Init",
                "Celeste.BounceBlock+RespawnDebris",
                new[] { Vector2Source, Vector2Source, "System.Boolean", "System.Single" },
                typeof(BounceBlockRespawnDebrisState), nameof(BounceBlockRespawnDebrisState.Init),
                typeof(BounceBlockRespawnDebrisState),
                new[] { typeof(Vector2), typeof(Vector2), typeof(bool), typeof(float) });
            Bind(methods, installed ? 100666919 : 100671195, "Celeste.BounceBlock+BreakDebris", "Init",
                "Celeste.BounceBlock+BreakDebris", new[] { Vector2Source, Vector2Source, "System.Boolean" },
                typeof(BounceBlockBreakDebrisState), nameof(BounceBlockBreakDebrisState.Init),
                typeof(BounceBlockBreakDebrisState), new[] { typeof(Vector2), typeof(Vector2), typeof(bool) });
            Bind(methods, installed ? 100673227 : 100674344, "Celeste.MoveBlock+Debris", "Init",
                "Celeste.MoveBlock+Debris",
                new[] { Vector2Source, Vector2Source, Vector2Source }, typeof(MoveBlockDebrisState),
                nameof(MoveBlockDebrisState.Init),
                typeof(MoveBlockDebrisState), new[] { typeof(Vector2), typeof(Vector2), typeof(Vector2) });
            Bind(methods, installed ? 100669220 : 100667179, "Celeste.CrystalDebris", "Init", "System.Void",
                new[] { Vector2Source, ColorSource, "System.Boolean" }, typeof(CrystalDebrisState), "Init",
                typeof(void), new[] { typeof(Vector2), typeof(PackedColor), typeof(bool) });
            Bind(methods, installed ? 100675559 : 100670113, "Celeste.Bridge", "Update", "System.Void",
                Array.Empty<string>(), typeof(OriginalBridgeState), nameof(OriginalBridgeState.Update), typeof(void),
                Type.EmptyTypes);
            Bind(methods, installed ? 100669364 : 100667298, "Celeste.AscendManager", "Routine",
                "System.Collections.IEnumerator",
                Array.Empty<string>(), typeof(OriginalAscendManagerState), "Routine", typeof(IEnumerator),
                Type.EmptyTypes);
            Bind(methods, installed ? 100669386 : 100672522, "Celeste.AscendManager+<Routine>d__15", "MoveNext",
                "System.Boolean",
                Array.Empty<string>(), AscendRoutineActualStateMachineType, "MoveNext", typeof(bool), Type.EmptyTypes);

            Bind(methods, installed ? 100673376 : 100669029, "Celeste.DreamMirror", "Added", "System.Void",
                new[] { "Monocle.Scene" }, typeof(DreamMirrorState), nameof(DreamMirrorState.Added), typeof(void),
                new[] { typeof(object) });
            Bind(methods, installed ? 100673378 : 100669031, "Celeste.DreamMirror", "BeforeRender", "System.Void",
                Array.Empty<string>(), typeof(DreamMirrorState), "BeforeRender", typeof(void), Type.EmptyTypes);
            Bind(methods, installed ? 100668809 : 100666972, "Celeste.FinalBoss", "Added", "System.Void",
                new[] { "Monocle.Scene" }, typeof(FinalBossState), nameof(FinalBossState.Added), typeof(void),
                new[] { typeof(object) });
            Bind(methods, installed ? 100675997 : 100670430, "Celeste.Level", "UpdateTime", "System.Void",
                Array.Empty<string>(), typeof(OriginalLevelSceneRoot), nameof(OriginalLevelSceneRoot.UpdateLevelTime),
                typeof(void), Type.EmptyTypes);
            Bind(methods, installed ? 100670819 : 100667972, "Celeste.LightingRenderer", "SetOccluder", "System.Void",
                new[] { Vector3Source, ColorSource, Vector2Source, Vector2Source, Vector2Source },
                typeof(LightingRendererPresentation), "SetOccluder",
                typeof(void),
                new[] { typeof(Vector3), typeof(PackedColor), typeof(Vector2), typeof(Vector2), typeof(Vector2) });
            Bind(methods, installed ? 100670820 : 100667973, "Celeste.LightingRenderer", "SetCutout", "System.Void",
                new[]
                {
                    Vector3Source, ColorSource, Vector2Source, "System.Single", "System.Single", "System.Single",
                    "System.Single"
                },
                typeof(LightingRendererPresentation), "SetCutout", typeof(void),
                new[]
                {
                    typeof(Vector3), typeof(PackedColor), typeof(Vector2), typeof(float), typeof(float), typeof(float),
                    typeof(float)
                });

            Type memoType = MemoPageActualType;
            var memoConstructorDefinition = Require(methods, installed ? 100667464 : 100671458,
                "Celeste.CS03_Memo+MemoPage", ".ctor", "System.Void", Array.Empty<string>());
            if (((MethodAttributes)memoConstructorDefinition.Attributes & MethodAttributes.MemberAccessMask) !=
                MethodAttributes.Public)
                throw new InvalidOperationException("The true source memo constructor must be public.");
            var memoConstructor =
                memoType.GetConstructor(InstanceDeclared, null, new[] { typeof(ISourceEngineContext) }, null)
                ?? throw new MissingMethodException(memoType.FullName, ".ctor(ISourceEngineContext)");
            // This physical constructor adds only the engine loan; the true PE constructor remains zero-parameter.
            methods.BindActualConstructor(GameAssembly, memoConstructorDefinition.MethodDefinitionToken,
                memoConstructor);
            Bind(methods, installed ? 100667468 : 100671462, "Celeste.CS03_Memo+MemoPage", "BeforeRender",
                "System.Void",
                Array.Empty<string>(), memoType, "BeforeRender", typeof(void), Type.EmptyTypes);
            BindField(members, "Celeste.FinalBoss", installed ? 67112661 : 67111314,
                "facing", "System.Int32", typeof(FinalBossState), nameof(FinalBossState.Facing), typeof(int));
            BindField(members, "Celeste.CS03_Memo+MemoPage", installed ? 67111586 : 67117149,
                "target", "Monocle.VirtualRenderTarget", memoType, "target", typeof(VirtualRenderTarget));
        }

        private static MethodInfo AscendRoutine() =>
            typeof(OriginalAscendManagerState).GetMethod("Routine", InstanceDeclared, null, Type.EmptyTypes, null)
            ?? throw new MissingMethodException(typeof(OriginalAscendManagerState).FullName, "Routine");

        private static SourceMethodDefinition Require(SourceAssemblyMethodCatalog methods, int token,
            string sourceType, string sourceName, string sourceReturn, string[] sourceParameters)
        {
            var definition = methods.RequireDefinition(GameAssembly, token);
            if (definition.DeclaringType != sourceType || definition.Name != sourceName
                                                       || definition.ReturnTypeIdentity != sourceReturn ||
                                                       !definition.ParameterTypeIdentities.SequenceEqual(
                                                           sourceParameters)
                                                       || definition.GenericParameterCount != 0 ||
                                                       definition.HasBody != true
                                                       || ((MethodAttributes)definition.Attributes &
                                                           MethodAttributes.Static) != 0)
                throw new InvalidOperationException("The actual target does not match its true source PE method: " +
                                                    token);
            return definition;
        }

        private static void Bind(SourceAssemblyMethodCatalog methods, int token, string sourceType,
            string sourceName, string sourceReturn, string[] sourceParameters,
            Type actualType, string actualName, Type actualReturn, Type[] actualParameters)
        {
            var definition = Require(methods, token, sourceType, sourceName, sourceReturn, sourceParameters);
            var method = actualType.GetMethod(actualName, InstanceDeclared, null, actualParameters, null)
                         ?? throw new MissingMethodException(actualType.FullName, actualName);
            if (method.ReturnType != actualReturn || method.IsStatic || method.DeclaringType != actualType)
                throw new InvalidOperationException(
                    "The genuine shared source target lost its declared physical ABI: " +
                    method);
            methods.BindActualMethod(GameAssembly, definition.MethodDefinitionToken, method, null);
        }

        private static void BindField(SourceMemberCatalog members, string sourceType, int token,
            string sourceName, string sourceValueType, Type actualType, string actualName, Type actualValueType)
        {
            var definition = members.RequireDefinition(GameAssembly, sourceType, sourceName, SourceMemberKind.Field);
            if (definition.MetadataToken != token || definition.IsStatic ||
                definition.ValueTypeIdentity != sourceValueType)
                throw new InvalidOperationException("The actual field does not match its true source PE definition: " +
                                                    token);
            var field = actualType.GetField(actualName, InstanceDeclared)
                        ?? throw new MissingFieldException(actualType.FullName, actualName);
            if (field.FieldType != actualValueType || field.IsStatic)
                throw new InvalidOperationException("The genuine shared source field lost its physical ABI: " + field);
            members.RegisterBinding(new SourcePhysicalMemberBinding(definition, actualType, field));
        }
    }
}
