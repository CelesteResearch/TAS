using Celeste.Core.Display;
using Celeste.Graphics.Rendering.Drawing;
using System;
using System.Linq;
using System.Reflection;
using System.Numerics;
using MonoMod.RuntimeDetour;
using Celeste.Core.Rendering;
using Celeste.Core.Mathematics;
using Celeste.Core.Reflection.Members;
using Celeste.Scene.Entities;
using Celeste.Scene.Tracking;
using Celeste.Simulation;
using Celeste.Physics.Collision.Shapes;
using Celeste.Game.Entities.Birds;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Gameplay.Enemies.BadelineBoss.Projectiles;
using Celeste.Graphics.Rendering.Geometry;
using Celeste.Mods.Tas.Gameplay.Hitboxes;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Celeste.Mods.Tas.Reflection;

namespace Celeste.Mods.Tas.Gameplay.Hitboxes
{
    internal sealed class SourceTasNpcHitboxProcess
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private readonly System.Collections.Generic.Dictionary<string, Type> sourceTypes = new();
        private SourceTasGetDelegate<SceneEntityState, int> clutterSections;
        private SourceTasGetDelegate<SceneEntityState, SceneEntityState> badelineShadow;
        private SourceTasGetDelegate<object, object> sourceDataLevel;
        private SourceTasGetDelegate<object, string> sourceLevelName;
        private Hook birdHook, playerHook, entityHook;
        private static readonly Vector2 DefaultOffset = new(4f, 0f);

        internal SourceTasNpcHitboxProcess(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        private ISourceVectorArithmetic Arithmetic => ((ISourceVectorEnvironment)process).VectorArithmetic;

        internal void Load()
        {
            foreach (string name in new[]
                     {
                         "NPC00_Granny", "NPC03_Oshiro_Hallway1", "NPC03_Oshiro_Hallway2", "NPC06_Granny",
                         "NPC03_Oshiro_Cluttter", "NPC04_Granny", "NPC05_Theo_Mirror", "NPC06_Badeline_Crying",
                         "NPC09_Granny_Inside", "NPC09_Granny_Outside", "NPC03_Theo_Escaping", "Gondola",
                         "NPC05_Badeline"
                     })
                sourceTypes["Celeste." + name] = SourceTasActorDebugMethods.RequireType(process, "Celeste." + name);
            clutterSections =
                driver.SourceFastReflection.CreateGetDelegate<SceneEntityState, int>(
                    sourceTypes["Celeste.NPC03_Oshiro_Cluttter"], "sectionsComplete");
            badelineShadow =
                driver.SourceFastReflection.CreateGetDelegate<SceneEntityState, SceneEntityState>(
                    sourceTypes["Celeste.NPC05_Badeline"], "shadow");
            sourceDataLevel =
                driver.SourceFastReflection.CreateGetDelegate<object, object>(
                    SourceTasActorDebugMethods.RequireType(process, "Celeste.EntityData"), "Level");
            sourceLevelName =
                driver.SourceFastReflection.CreateGetDelegate<object, string>(
                    SourceTasActorDebugMethods.RequireType(process, "Celeste.LevelData"), "Name");
            birdHook = new Hook(SourceTasActorDebugMethods.Require(process, "Celeste.BirdNPC", "DebugRender"),
                (Action<Action<BirdNpcState, ISourceRenderContext, object>, BirdNpcState, ISourceRenderContext, object>)
                BirdDebugRender);
            playerHook = new Hook(SourceTasActorDebugMethods.Require(process, "Celeste.Player", "DebugRender"),
                (Action<Action<RoomPlayerSession, ISourceRenderContext, object>, RoomPlayerSession, ISourceRenderContext
                    ,
                    object>)PlayerDebugRender);
            entityHook = new Hook(SourceTasActorDebugMethods.Require(process, "Monocle.Entity", "DebugRender"),
                (Action<Action<SceneEntityState, ISourceRenderContext, object>, SceneEntityState, ISourceRenderContext,
                    object>)EntityOnDebugRender);
        }

        internal void Unload()
        {
            birdHook.Dispose();
            playerHook.Dispose();
            entityHook.Dispose();
        }

        private bool Is(object entity, string sourceType) => sourceTypes[sourceType].IsInstanceOfType(entity);

        private static RoomPlayerSession GetPlayer(OriginalLevelSceneRoot level)
        {
            var players =
                level.SourceScene.Entities.GetTrackedEntities(typeof(global::Celeste.Simulation.RoomPlayerSession)
                    .FullName);

            return players.Count == 0 ? null : (RoomPlayerSession)players[0];
        }

        private string SourceRoomName(object data)
        {
            if (data == null) return null;
            object level = sourceDataLevel(data);
            return level == null ? null : sourceLevelName(level);
        }

        private void BirdDebugRender(Action<BirdNpcState, ISourceRenderContext, object> original, BirdNpcState bird,
            ISourceRenderContext context, object camera)
        {
            if (driver.Settings.ShowHitboxes)
            {
                if (driver.Settings.ShowTriggerHitboxes && bird.Mode == BirdNpcMode.DashingTutorial &&
                    process.Frame.Scenes.CurrentScene is OriginalLevelSceneRoot
                    {
                        Session: { AreaID: 0, Level: "3" }
                    } level)
                {
                    RoomPlayerSession player = GetPlayer(level);
                    Vector2 offset = player is null ? DefaultOffset : player.Collider.BottomRight;
                    Vector2 position = bird.StartPosition;

                    void DrawTrigger(float xRange, float yRangeStart, float yRangeEnd)
                    {
                        float x1 = position.X - xRange + offset.X;
                        float y1 = position.Y - yRangeStart + offset.Y;
                        float y2 = position.Y - yRangeEnd + offset.Y;
                        SourcePrimitiveDrawing.HollowRect(context, bird, x1 - 1f, y1 - 1f, level.Bounds.Right - x1,
                            y2 - y1 + 1f, PackedColor.Aqua);
                    }

                    DrawTrigger(91f, 11f, 19f);
                    if (process.SaveContext.CurrentSave.Assists.Invincible) DrawTrigger(59f, -33f, -1f);
                }
            }
            else original(bird, context, camera);
        }

        private void PlayerDebugRender(Action<RoomPlayerSession, ISourceRenderContext, object> original,
            RoomPlayerSession self, ISourceRenderContext context, object camera)
        {
            original(self, context, camera);
            if (driver.Settings.ShowHitboxes && driver.Settings.ShowTriggerHitboxes &&
                process.Frame.Scenes.CurrentScene is OriginalLevelSceneRoot level && level.Session.AreaID == 0 &&
                level.Session.Level == "3")
                SourcePrimitiveDrawing.Point(context, self, self.BottomRight - Vector2.One, PackedColor.Aqua);
        }

        private void EntityOnDebugRender(Action<SceneEntityState, ISourceRenderContext, object> original,
            SceneEntityState entity, ISourceRenderContext context, object camera)
        {
            original(entity, context, camera);

            if (driver.Settings.ShowHitboxes && driver.Settings.ShowTriggerHitboxes &&
                process.Frame.Scenes.CurrentScene is OriginalLevelSceneRoot level)
            {
                if (SourceRoomName(entity.SourceData) != level.Session.Level)
                {
                    return;
                }

                var levelBounds = level.Bounds;
                int left = levelBounds.Left;
                int top = levelBounds.Top;
                int bottom = levelBounds.Bottom;
                PackedColor color = driver.Settings.TriggerHitboxColor;
                string levelName = level.Session.Level;

                if (Is(entity, "Celeste.NPC00_Granny"))
                {
                    float x = left + 100;
                    SourcePrimitiveDrawing.HollowRect(context, entity, x - 1, entity.Y - 3, entity.X + 16 - left - 102,
                        3,
                        color);
                }
                else if ((Is(entity, "Celeste.NPC03_Oshiro_Hallway1") || Is(entity, "Celeste.NPC03_Oshiro_Hallway2") ||
                          Is(entity, "Celeste.NPC06_Granny")))
                {
                    float x = entity.X - 55;
                    SourcePrimitiveDrawing.Line((ISourceDrawScope)context, x, top, x, bottom, color, Arithmetic);
                }
                else if (Is(entity, "Celeste.NPC03_Oshiro_Cluttter") && clutterSections(entity) == 3)
                {
                    float x = entity.X + 28;
                    SourcePrimitiveDrawing.Line((ISourceDrawScope)context, x, top, x, entity.Y, color, Arithmetic);
                }
                else if (Is(entity, "Celeste.NPC04_Granny"))
                {
                    float x = entity.X - 35;
                    SourcePrimitiveDrawing.Line((ISourceDrawScope)context, x, top, x, bottom, color, Arithmetic);
                }
                else if (Is(entity, "Celeste.NPC05_Theo_Mirror"))
                {
                    float x = entity.X - 59;
                    SourcePrimitiveDrawing.Line((ISourceDrawScope)context, x, top, x, bottom, color, Arithmetic);
                }
                else if (Is(entity, "Celeste.NPC06_Badeline_Crying"))
                {
                    float x = entity.X - 27;
                    SourcePrimitiveDrawing.Line((ISourceDrawScope)context, x, top, x, bottom, color, Arithmetic);
                }
                else if ((Is(entity, "Celeste.NPC09_Granny_Inside") || Is(entity, "Celeste.NPC09_Granny_Outside")))
                {
                    float x = entity.X - 43;
                    SourcePrimitiveDrawing.Line((ISourceDrawScope)context, x, top, x, bottom, color, Arithmetic);
                }
                else if (Is(entity, "Celeste.NPC03_Theo_Escaping") && levelName == "11-b")
                {
                    float x = 5477;
                    SourcePrimitiveDrawing.Line((ISourceDrawScope)context, x, top, x, bottom, color, Arithmetic);
                }
                else if (level.Session.AreaID == 4 && Is(entity, "Celeste.Gondola"))
                {
                    float x = entity.Left - 11;
                    SourcePrimitiveDrawing.Line((ISourceDrawScope)context, x, top, x, bottom, color, Arithmetic);
                }
                else if (Is(entity, "Celeste.NPC05_Badeline"))
                {
                    if (levelName == "c-00")
                    {
                        float x = entity.X - 59;
                        SourcePrimitiveDrawing.Line((ISourceDrawScope)context, x, top, x, bottom, color, Arithmetic);
                    }
                    else if (levelName == "c-01" && badelineShadow(entity) is SceneEntityState badelineDummy)
                    {
                        SourcePrimitiveDrawing.Circle(context, entity, badelineDummy.Position, 70, color, 4,
                            Arithmetic);
                        if (GetPlayer(level) is { } player &&
                            Arithmetic.Distance(player.Position, badelineDummy.Position) <= 150)
                        {
                            SourcePrimitiveDrawing.Line(context, entity, player.Position, badelineDummy.Position,
                                PackedColor.Aqua, Arithmetic);
                        }
                    }
                }
            }
        }
    }
}
