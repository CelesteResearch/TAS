using Celeste.Core.Display;
using System;
using System.Linq;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;
using Celeste.Everest.Runtime.Levels.GarbageCollection;
using global::Celeste.Game.Execution;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasOptimizationActualMethodBindings
    {
        private const string GameAssembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        internal static void Register(SourceAssemblyMethodCatalog methods, SourceMemberCatalog members,
            bool installed, EverestLevelGarbageCollectionProcess installedGc)
        {
            if (installed)
            {
                Bind(methods, members, 33555372, 100669798, "Celeste.LightningRenderer", "Update",
                    Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Gameplay.Hazards.Lightning.LightningRendererState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33556455, 100676670, "Celeste.Celeste", "Update",
                    new string[] { "Microsoft.Xna.Framework.GameTime" },
                    typeof(global::Celeste.Game.Execution.GameRuntime), "Update",
                    new Type[] { typeof(global::Celeste.Core.Framework.Components.SourceGameTime) }, false, null);
                Bind(methods, members, 33554777, 100665946, "Monocle.ParticleSystem", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33554777, 100665944, "Monocle.ParticleSystem", "Clear", Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Clear",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33554777, 100665945, "Monocle.ParticleSystem", "ClearRect",
                    new string[] { "Microsoft.Xna.Framework.Rectangle", "System.Boolean" },
                    typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "ClearRect",
                    new Type[] { typeof(global::Celeste.Core.Geometry.IntRect), typeof(global::System.Boolean) }, false,
                    null);
                Bind(methods, members, 33555379, 100669839, "Celeste.SeekerBarrierRenderer", "Update",
                    Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Gameplay.Enemies.Seeker.SeekerBarrierRendererState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33555395, 100669944, "Celeste.SoundEmitter", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Audio.Emitters.SoundEmitterState), "Update", Type.EmptyTypes,
                    false,
                    null);
                Bind(methods, members, 33555934, 100672982, "Celeste.DustGraphic", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Gameplay.Hazards.Spinners.Dust.DustGraphicState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33554713, 100665247, "Monocle.Engine", "OnSceneTransition",
                    new string[] { "Monocle.Scene", "Monocle.Scene" },
                    typeof(global::Celeste.Game.Execution.EngineRuntime), "OnSceneTransition",
                    new Type[]
                    {
                        typeof(global::Celeste.Core.Scheduling.ISourceSceneExecution),
                        typeof(global::Celeste.Core.Scheduling.ISourceSceneExecution)
                    }, false, null);
                Bind(methods, members, 33556389, 100675981, "Celeste.Level", "Reload", Array.Empty<string>(),
                    typeof(global::Celeste.Game.Execution.Levels.OriginalLevelSceneRoot), "Reload", Type.EmptyTypes,
                    false, null);
                Bind(methods, members, 33555263, 100669320, "Celeste.ReflectionTentacles", "Update",
                    Array.Empty<string>(),
                    typeof(global::Celeste.Game.Entities.ReflectionTentacles.OriginalReflectionTentaclesState),
                    "Update", Type.EmptyTypes, false, null);
                Bind(methods, members, 33556058, 100673666, "Celeste.Decal", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Presentation.Decals.Entities.SourceDecalEntityState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33555976, 100673260, "Celeste.FloatingDebris", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Game.Gameplay.Environment.Debris.OriginalFloatingDebrisState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33555426, 100670108, "Celeste.AnimatedTiles", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Graphics.Content.Tiles.AnimatedTiles), "Update", Type.EmptyTypes, false,
                    null);
                Bind(methods, members, 33555833, 100672400, "Celeste.Water+Surface", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Game.Gameplay.Environment.Water.OriginalWaterState.Surface), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33554912, 100667240, "Celeste.LavaRect", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Graphics.Effects.Liquids.LavaRectangleComponentState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33555237, 100669214, "Celeste.CliffsideWindFlag", "Update",
                    Array.Empty<string>(),
                    typeof(global::Celeste.Game.Gameplay.Environment.Ridge.OriginalCliffsideWindFlagState),
                    "Update", Type.EmptyTypes, false, null);
                Bind(methods, members, 33555172, 100668772, "Celeste.CrystalStaticSpinner", "UpdateHue",
                    Array.Empty<string>(),
                    typeof(global::Celeste.Game.Entities.Hazards.Spinners.Crystal.OriginalCrystalStaticSpinner),
                    "UpdateHue", Type.EmptyTypes, false, null);
                Bind(methods, members, 33555534, 100670572, "Celeste.HiresSnow", "Update",
                    new string[] { "Monocle.Scene" },
                    typeof(global::Celeste.Game.Presentation.Environment.HiresSnowState), "Update",
                    new Type[] { typeof(global::System.Object) }, false, null);
                Bind(methods, members, 33555583, 100670898, "Celeste.Snow3D", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Game.Presentation.Mountain.Particles.Snow3DState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33555353, 100669740, "Celeste.AutoSplitterInfo", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Game.Diagnostics.Speedrun.OriginalAutoSplitterInfo), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33555628, 100671152, "Celeste.SeekerBarrier", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Gameplay.Enemies.Seeker.SeekerBarrierState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33554777, 100665951, "Monocle.ParticleSystem", "Emit",
                    new string[] { "Monocle.ParticleType", "Microsoft.Xna.Framework.Vector2" },
                    typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Numerics.Vector2)
                    }, false, null);
                Bind(methods, members, 33554777, 100665952, "Monocle.ParticleSystem", "Emit",
                    new string[] { "Monocle.ParticleType", "Microsoft.Xna.Framework.Vector2", "System.Single" },
                    typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Numerics.Vector2), typeof(global::System.Single)
                    }, false, null);
                Bind(methods, members, 33554777, 100665953, "Monocle.ParticleSystem", "Emit",
                    new string[]
                        { "Monocle.ParticleType", "Microsoft.Xna.Framework.Vector2", "Microsoft.Xna.Framework.Color" },
                    typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Numerics.Vector2), typeof(global::Celeste.Core.Display.PackedColor)
                    }, false, null);
                Bind(methods, members, 33554777, 100665954, "Monocle.ParticleSystem", "Emit",
                    new string[]
                    {
                        "Monocle.ParticleType", "Microsoft.Xna.Framework.Vector2", "Microsoft.Xna.Framework.Color",
                        "System.Single"
                    }, typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Numerics.Vector2), typeof(global::Celeste.Core.Display.PackedColor),
                        typeof(global::System.Single)
                    }, false, null);
                Bind(methods, members, 33554777, 100665955, "Monocle.ParticleSystem", "Emit",
                    new string[]
                    {
                        "Monocle.ParticleType", "System.Int32", "Microsoft.Xna.Framework.Vector2",
                        "Microsoft.Xna.Framework.Vector2"
                    }, typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Int32), typeof(global::System.Numerics.Vector2),
                        typeof(global::System.Numerics.Vector2)
                    }, false, null);
                Bind(methods, members, 33554777, 100665956, "Monocle.ParticleSystem", "Emit",
                    new string[]
                    {
                        "Monocle.ParticleType", "System.Int32", "Microsoft.Xna.Framework.Vector2",
                        "Microsoft.Xna.Framework.Vector2", "System.Single"
                    }, typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Int32), typeof(global::System.Numerics.Vector2),
                        typeof(global::System.Numerics.Vector2), typeof(global::System.Single)
                    }, false, null);
                Bind(methods, members, 33554777, 100665957, "Monocle.ParticleSystem", "Emit",
                    new string[]
                    {
                        "Monocle.ParticleType", "System.Int32", "Microsoft.Xna.Framework.Vector2",
                        "Microsoft.Xna.Framework.Vector2", "Microsoft.Xna.Framework.Color"
                    }, typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Int32), typeof(global::System.Numerics.Vector2),
                        typeof(global::System.Numerics.Vector2), typeof(global::Celeste.Core.Display.PackedColor)
                    }, false, null);
                Bind(methods, members, 33554777, 100665958, "Monocle.ParticleSystem", "Emit",
                    new string[]
                    {
                        "Monocle.ParticleType", "System.Int32", "Microsoft.Xna.Framework.Vector2",
                        "Microsoft.Xna.Framework.Vector2", "Microsoft.Xna.Framework.Color", "System.Single"
                    }, typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Int32), typeof(global::System.Numerics.Vector2),
                        typeof(global::System.Numerics.Vector2), typeof(global::Celeste.Core.Display.PackedColor),
                        typeof(global::System.Single)
                    }, false, null);
                Bind(methods, members, 33554777, 100665959, "Monocle.ParticleSystem", "Emit",
                    new string[]
                    {
                        "Monocle.ParticleType", "Monocle.Entity", "System.Int32", "Microsoft.Xna.Framework.Vector2",
                        "Microsoft.Xna.Framework.Vector2", "System.Single"
                    }, typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::Celeste.Graphics.Particles.Tracking.IParticleTrack),
                        typeof(global::System.Int32),
                        typeof(global::System.Numerics.Vector2), typeof(global::System.Numerics.Vector2),
                        typeof(global::System.Single)
                    }, false, null);
                Bind(methods, members, 33556389, 100676059, "Celeste.Level", "_GCCollect", Array.Empty<string>(),
                    typeof(
                        global::Celeste.Everest.Runtime.Levels.GarbageCollection.EverestLevelGarbageCollectionProcess),
                    "Collect", Type.EmptyTypes, true, installedGc);
            }
            else
            {
                Bind(methods, members, 33554712, 100665548, "Monocle.ParticleSystem", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33554712, 100665546, "Monocle.ParticleSystem", "Clear", Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Clear",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33554712, 100665547, "Monocle.ParticleSystem", "ClearRect",
                    new string[] { "Microsoft.Xna.Framework.Rectangle", "System.Boolean" },
                    typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "ClearRect",
                    new Type[] { typeof(global::Celeste.Core.Geometry.IntRect), typeof(global::System.Boolean) }, false,
                    null);
                Bind(methods, members, 33554938, 100667549, "Celeste.SeekerBarrierRenderer", "Update",
                    Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Gameplay.Enemies.Seeker.SeekerBarrierRendererState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33554945, 100667595, "Celeste.SoundEmitter", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Audio.Emitters.SoundEmitterState), "Update", Type.EmptyTypes,
                    false,
                    null);
                Bind(methods, members, 33555129, 100668798, "Celeste.DustGraphic", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Gameplay.Hazards.Spinners.Dust.DustGraphicState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33554693, 100665176, "Monocle.Engine", "OnSceneTransition",
                    new string[] { "Monocle.Scene", "Monocle.Scene" },
                    typeof(global::Celeste.Game.Execution.EngineRuntime), "OnSceneTransition",
                    new Type[]
                    {
                        typeof(global::Celeste.Core.Scheduling.ISourceSceneExecution),
                        typeof(global::Celeste.Core.Scheduling.ISourceSceneExecution)
                    }, false, null);
                Bind(methods, members, 33555315, 100670414, "Celeste.Level", "Reload", Array.Empty<string>(),
                    typeof(global::Celeste.Game.Execution.Levels.OriginalLevelSceneRoot), "Reload", Type.EmptyTypes,
                    false, null);
                Bind(methods, members, 33554886, 100667269, "Celeste.ReflectionTentacles", "Update",
                    Array.Empty<string>(),
                    typeof(global::Celeste.Game.Entities.ReflectionTentacles.OriginalReflectionTentaclesState),
                    "Update", Type.EmptyTypes, false, null);
                Bind(methods, members, 33555176, 100669169, "Celeste.Decal", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Presentation.Decals.Entities.SourceDecalEntityState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33555148, 100668977, "Celeste.FloatingDebris", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Game.Gameplay.Environment.Debris.OriginalFloatingDebrisState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33554956, 100667668, "Celeste.AnimatedTiles", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Graphics.Content.Tiles.AnimatedTiles), "Update", Type.EmptyTypes, false,
                    null);
                Bind(methods, members, 33555985, 100674062, "Celeste.Water+Surface", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Game.Gameplay.Environment.Water.OriginalWaterState.Surface), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33554776, 100666471, "Celeste.LavaRect", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Graphics.Effects.Liquids.LavaRectangleComponentState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33554866, 100667174, "Celeste.CliffsideWindFlag", "Update",
                    Array.Empty<string>(),
                    typeof(global::Celeste.Game.Gameplay.Environment.Ridge.OriginalCliffsideWindFlagState),
                    "Update", Type.EmptyTypes, false, null);
                Bind(methods, members, 33554843, 100666942, "Celeste.CrystalStaticSpinner", "UpdateHue",
                    Array.Empty<string>(),
                    typeof(global::Celeste.Game.Entities.Hazards.Spinners.Crystal.OriginalCrystalStaticSpinner),
                    "UpdateHue", Type.EmptyTypes, false, null);
                Bind(methods, members, 33554995, 100667880, "Celeste.HiresSnow", "Update",
                    new string[] { "Monocle.Scene" },
                    typeof(global::Celeste.Game.Presentation.Environment.HiresSnowState), "Update",
                    new Type[] { typeof(global::System.Object) }, false, null);
                Bind(methods, members, 33555016, 100668012, "Celeste.Snow3D", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Game.Presentation.Mountain.Particles.Snow3DState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33554928, 100667476, "Celeste.AutoSplitterInfo", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Game.Diagnostics.Speedrun.OriginalAutoSplitterInfo), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33555038, 100668127, "Celeste.SeekerBarrier", "Update", Array.Empty<string>(),
                    typeof(global::Celeste.Simulation.Gameplay.Enemies.Seeker.SeekerBarrierState), "Update",
                    Type.EmptyTypes, false, null);
                Bind(methods, members, 33554712, 100665553, "Monocle.ParticleSystem", "Emit",
                    new string[] { "Monocle.ParticleType", "Microsoft.Xna.Framework.Vector2" },
                    typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Numerics.Vector2)
                    }, false, null);
                Bind(methods, members, 33554712, 100665554, "Monocle.ParticleSystem", "Emit",
                    new string[] { "Monocle.ParticleType", "Microsoft.Xna.Framework.Vector2", "System.Single" },
                    typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Numerics.Vector2), typeof(global::System.Single)
                    }, false, null);
                Bind(methods, members, 33554712, 100665555, "Monocle.ParticleSystem", "Emit",
                    new string[]
                        { "Monocle.ParticleType", "Microsoft.Xna.Framework.Vector2", "Microsoft.Xna.Framework.Color" },
                    typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Numerics.Vector2), typeof(global::Celeste.Core.Display.PackedColor)
                    }, false, null);
                Bind(methods, members, 33554712, 100665556, "Monocle.ParticleSystem", "Emit",
                    new string[]
                    {
                        "Monocle.ParticleType", "Microsoft.Xna.Framework.Vector2", "Microsoft.Xna.Framework.Color",
                        "System.Single"
                    }, typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Numerics.Vector2), typeof(global::Celeste.Core.Display.PackedColor),
                        typeof(global::System.Single)
                    }, false, null);
                Bind(methods, members, 33554712, 100665557, "Monocle.ParticleSystem", "Emit",
                    new string[]
                    {
                        "Monocle.ParticleType", "System.Int32", "Microsoft.Xna.Framework.Vector2",
                        "Microsoft.Xna.Framework.Vector2"
                    }, typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Int32), typeof(global::System.Numerics.Vector2),
                        typeof(global::System.Numerics.Vector2)
                    }, false, null);
                Bind(methods, members, 33554712, 100665558, "Monocle.ParticleSystem", "Emit",
                    new string[]
                    {
                        "Monocle.ParticleType", "System.Int32", "Microsoft.Xna.Framework.Vector2",
                        "Microsoft.Xna.Framework.Vector2", "System.Single"
                    }, typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Int32), typeof(global::System.Numerics.Vector2),
                        typeof(global::System.Numerics.Vector2), typeof(global::System.Single)
                    }, false, null);
                Bind(methods, members, 33554712, 100665559, "Monocle.ParticleSystem", "Emit",
                    new string[]
                    {
                        "Monocle.ParticleType", "System.Int32", "Microsoft.Xna.Framework.Vector2",
                        "Microsoft.Xna.Framework.Vector2", "Microsoft.Xna.Framework.Color"
                    }, typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Int32), typeof(global::System.Numerics.Vector2),
                        typeof(global::System.Numerics.Vector2), typeof(global::Celeste.Core.Display.PackedColor)
                    }, false, null);
                Bind(methods, members, 33554712, 100665560, "Monocle.ParticleSystem", "Emit",
                    new string[]
                    {
                        "Monocle.ParticleType", "System.Int32", "Microsoft.Xna.Framework.Vector2",
                        "Microsoft.Xna.Framework.Vector2", "Microsoft.Xna.Framework.Color", "System.Single"
                    }, typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::System.Int32), typeof(global::System.Numerics.Vector2),
                        typeof(global::System.Numerics.Vector2), typeof(global::Celeste.Core.Display.PackedColor),
                        typeof(global::System.Single)
                    }, false, null);
                Bind(methods, members, 33554712, 100665561, "Monocle.ParticleSystem", "Emit",
                    new string[]
                    {
                        "Monocle.ParticleType", "Monocle.Entity", "System.Int32", "Microsoft.Xna.Framework.Vector2",
                        "Microsoft.Xna.Framework.Vector2", "System.Single"
                    }, typeof(global::Celeste.Simulation.Presentation.Particles.ParticleSystemPresentation), "Emit",
                    new Type[]
                    {
                        typeof(global::Celeste.Simulation.Presentation.Particles.Definitions.ParticleTypePresentation),
                        typeof(global::Celeste.Graphics.Particles.Tracking.IParticleTrack),
                        typeof(global::System.Int32),
                        typeof(global::System.Numerics.Vector2), typeof(global::System.Numerics.Vector2),
                        typeof(global::System.Single)
                    }, false, null);
            }
        }

        private static void Bind(SourceAssemblyMethodCatalog methods, SourceMemberCatalog members,
            int typeToken, int methodToken, string sourceType, string sourceName, string[] sourceParameters,
            Type actualDeclaration, string actualName, Type[] actualParameters, bool originalStatic, object actualOwner)
        {
            var sourceTypeDefinition = members.GetOriginalTypeDefinition(GameAssembly, sourceType);
            var definition = methods.RequireDefinition(GameAssembly, methodToken);
            if (sourceTypeDefinition.MetadataToken != typeToken || definition.TypeDefinitionToken != typeToken
                                                                || definition.DeclaringType != sourceType ||
                                                                definition.Name != sourceName
                                                                || definition.ReturnTypeIdentity != "System.Void" ||
                                                                definition.HasBody != true
                                                                || ((definition.Attributes &
                                                                     (int)MethodAttributes.Static) != 0) !=
                                                                originalStatic
                                                                || !definition.ParameterTypeIdentities.SequenceEqual(
                                                                    sourceParameters))
                throw new InvalidOperationException("The qualified original optimization method is different: " +
                                                    sourceType + "." + sourceName);
            var actual = actualDeclaration.GetMethod(actualName,
                             BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                             BindingFlags.DeclaredOnly,
                             null, actualParameters, null)
                         ?? throw new MissingMethodException(actualDeclaration.FullName, actualName);
            if (actual.DeclaringType != actualDeclaration || actual.ReturnType != typeof(void)
                                                          || actual.IsStatic || actual.GetMethodBody() == null)
                throw new InvalidOperationException(
                    "The actual optimization target has a different physical body or declaration.");
            if (originalStatic)
            {
                // Original Level._GCCollect is static; its complete extracted CPU has one App-owned process.
                // This is an operation binding, not a false whole-process alias to the Level TypeDef.
                if (actualOwner == null || actualOwner.GetType() != actualDeclaration)
                    throw new InvalidOperationException(
                        "The actual original static operation has no exact owned process.");
            }
            else if (actualOwner != null
                     || !ReferenceEquals(members.GetOriginalTypeDefinition(actualDeclaration), sourceTypeDefinition))
                throw new InvalidOperationException(
                    "The actual instance method has a different canonical original declaration.");

            methods.BindActualMethod(GameAssembly, methodToken, actual, actualOwner);
        }
    }
}
