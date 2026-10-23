using Celeste.Core.Display;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Celeste.Core.Geometry;
using Celeste.Core.Scheduling;
using Celeste.Core.Rendering.Diagnostics;
using Celeste.Core.Mathematics;
using Celeste.Core.Mathematics.Extrema;
using Celeste.Graphics.Content;
using Celeste.Graphics.Content.Atlas;
using Celeste.Graphics.Display;
using Celeste.Graphics.Display.Colors;
using Celeste.Graphics.Geometry.TwoD.Camera;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Content.Maps;
using Celeste.Game.Content.Maps.Levels;
using Celeste.Game.Content.Maps.Entities;
using Celeste.Game.Entities.Decoration.Decals;
using Celeste.Everest.Content.Extensions.Decals;

namespace Celeste.Mods.Tas.Gameplay.Hitboxes.Rooms
{
    public sealed partial class TasUnloadedRoomHitboxProcess
    {
        private readonly HashSet<Type> IgnoreTypes;

        private HashSet<Type> CreateIgnoreTypes() => new()
        {
            types.TypeOf("Celeste.Bonfire"),
            types.TypeOf("Celeste.Checkpoint"),
            types.TypeOf("Celeste.CrystalDebris"),
            types.TypeOf("Celeste.Debris"),
            types.TypeOf("Celeste.Door"),
            types.TypeOf("Celeste.DreamMirror"),
            types.TypeOf("Celeste.FloatingDebris"),
            types.TypeOf("Celeste.ForegroundDebris"),
            types.TypeOf("Celeste.HangingLamp"),
            types.TypeOf("Celeste.LightBeam"),
            types.TypeOf("Celeste.Memorial"),
            types.TypeOf("Celeste.MoonCreature"),
            types.TypeOf("Celeste.PlaybackBillboard"),
            types.TypeOf("Celeste.ResortLantern"),
            types.TypeOf("Celeste.ResortMirror"),
            types.TypeOf("Celeste.SoundSourceEntity"),
            types.TypeOf("Celeste.TempleMirror"),
            types.TypeOf("Celeste.Torch"),
            types.TypeOf("Celeste.Trapdoor"),
            types.TypeOf("Celeste.Wire"),
            types.TypeOf("Celeste.Killbox"),
        };

        private readonly Dictionary<OriginalLevelRoomData, List<Action>> DecalActions = new();
        private readonly Dictionary<OriginalLevelRoomData, List<Action<OriginalLevelSceneRoot>>> EntityActions = new();

        private string lastRoom = "";
        private string currentRoom = "";

        private const float colorAlpha = 0.5f;

        public void Load() =>
            ((ISourceEntityListDebugRenderHooksEnvironment)engine).EntityListDebugRenderHooks.BeforeDebugRender +=
            BeforeEntityListDebugRender;

        public void Unload() =>
            ((ISourceEntityListDebugRenderHooksEnvironment)engine).EntityListDebugRenderHooks.BeforeDebugRender -=
            BeforeEntityListDebugRender;

        private void BeforeEntityListDebugRender(object actualCamera)
        {
            var camera = (CameraState)actualCamera;
            if (!settings.Enabled || !settings.CenterCamera || !settings.ShowUnloadedRoomsHitboxes ||
                engine.Frame.Scenes.CurrentScene is not OriginalLevelSceneRoot level)
                return;

            if (currentRoom != level.Session.Level)
            {
                lastRoom = currentRoom;
                currentRoom = level.Session.Level;
            }

            IntRect cameraRect = new((int)camera.Left, (int)camera.Top, camera.Viewport.Width, camera.Viewport.Height);

            List<OriginalLevelRoomData> levelDatasInCamera = ((OriginalChapterMapData)level.Session.MapData).Levels
                .Where(data => data.Name != level.Session.Level && data.Bounds.Intersects(cameraRect)).ToList();

            if (level.Transitioning && !string.IsNullOrEmpty(lastRoom))
            {
                levelDatasInCamera = levelDatasInCamera.Where(data => data.Name != lastRoom).ToList();
            }

            foreach (OriginalLevelRoomData levelData in levelDatasInCamera)
            {
                if (!DecalActions.TryGetValue(levelData, out var decalActions))
                {
                    DecalActions.Add(levelData, decalActions = new List<Action>());
                    DrawDecalHitbox(levelData, decalActions);
                }

                decalActions.ForEach(action => action());

                if (!EntityActions.TryGetValue(levelData, out var entityActions))
                {
                    EntityActions.Add(levelData, entityActions = new List<Action<OriginalLevelSceneRoot>>());
                    DrawEntityHitbox(levelData, entityActions);
                    DrawTriggerHitbox(levelData, entityActions);
                }

                entityActions.ForEach(action => action(level));
            }
        }

        private void DrawEntityHitbox(OriginalLevelRoomData levelData, List<Action<OriginalLevelSceneRoot>> actions)
        {
            string levelName = levelData.Name ?? "";
            foreach (OriginalLevelEntityData data in levelData.Entities)
            {
                string dataName = data.Name ?? "";
                Vector2 levelPosition = levelData.Position;
                Vector2 position = levelData.Position + data.Position;
                IntRect rect = CreateRect(position, data.Width, data.Height, 0, 0);
                PackedColor color = settings.EntityHitboxColor;
                string textureId = "";
                Vector2 textureOffset = Vector2.Zero;
                Vector2 scale = Vector2.One;
                double rotation = 0;
                bool outline = false;

                if (types.NameToType(dataName) is { } type)
                {
                    if (IgnoreTypes.Contains(type))
                    {
                        continue;
                    }

                    string lowercaseDataName = dataName.ToLowerInvariant();

                    if (lowercaseDataName.Contains("controller"))
                    {
                        IgnoreTypes.Add(type);
                        continue;
                    }

                    if (lowercaseDataName.Contains("spinner"))
                    {
                        if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Bumper")))
                        {
                            rect = IntRect.Empty;
                            actions.Add((level) => Circle(position, 12, settings.EntityHitboxColor, 4));

                            if ((engine.Frame.Scenes.CurrentScene as OriginalLevelSceneRoot)?.Session is
                                { CoreMode: > 0 })
                            {
                                textureId = "objects/Bumper/Evil00";
                            }
                            else
                            {
                                textureId = "objects/Bumper/idle00";
                            }
                        }
                        else
                        {
                            PackedColor spinnerColor = settings.EntityHitboxColor * colorAlpha;
                            actions.Add((_) => Circle(position, 6, spinnerColor, 4));
                            rect = dataName == "rotateSpinner" ? IntRect.Empty : CreateRect(position, 16, 4, -8, -3);

                            if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.TrackSpinner"),
                                    types.TypeOf("Celeste.RotateSpinner")))
                            {
                                textureId = "danger/blade00";
                                if ((engine.Frame.Scenes.CurrentScene as OriginalLevelSceneRoot)?.Session is
                                    { } session)
                                {
                                    if (session.AreaID == 10 || data.Bool("star"))
                                    {
                                        textureId = "danger/starfish00";
                                    }
                                    else if (session.AreaID == 3 || session.AreaID == 7 && levelName.StartsWith("d-") ||
                                             data.Bool("dust"))
                                    {
                                        textureId = "danger/dustcreature/center00";
                                    }
                                }
                            }
                            else
                            {
                                textureId = "danger/crystal/fg_blue00";
                                if ((engine.Frame.Scenes.CurrentScene as OriginalLevelSceneRoot)?.Session is
                                    { } session)
                                {
                                    if (session.AreaID == 3 || session.AreaID == 7 && levelName.StartsWith("d-") ||
                                        data.Bool("dust"))
                                    {
                                        textureId = "danger/dustcreature/base01";
                                    }
                                    else
                                    {
                                        string dataColor = data.Attr("color");
                                        if (session.AreaID == 5 || dataColor == "Red")
                                        {
                                            textureId = "danger/crystal/fg_red00";
                                        }
                                        else if (session.AreaID == 6 || dataColor == "Purple")
                                        {
                                            textureId = "danger/crystal/fg_purple00";
                                        }
                                        else if (session.AreaID == 10 || dataColor == "Rainbow")
                                        {
                                            textureId = "danger/crystal/fg_white00";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Spikes"),
                                 types.TypeOf("Celeste.TriggerSpikes"),
                                 types.TypeOf("Celeste.TriggerSpikesOriginal")))
                    {
                        string direction;
                        int size = IsSameOrSubclassOf(type, types.TypeOf("Celeste.TriggerSpikes")) ? 4 : 3;
                        if (dataName.EndsWith("Up"))
                        {
                            rect.Y -= size;
                            rect.Height = size;
                            direction = "up";
                        }
                        else if (dataName.EndsWith("Down"))
                        {
                            rect.Height = size;
                            direction = "down";
                        }
                        else if (dataName.EndsWith("Right"))
                        {
                            rect.Width = size;
                            direction = "right";
                        }
                        else
                        {
                            rect.X -= size;
                            rect.Width = size;
                            direction = "left";
                        }

                        if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Spikes")))
                        {
                            string spikeType = "outline";
                            if ((engine.Frame.Scenes.CurrentScene as OriginalLevelSceneRoot)?.AreaData is
                                { } areaData &&
                                areaData.Spike != null)
                            {
                                spikeType = areaData.Spike;
                            }

                            AtlasEntry texture = gameAtlas[$"danger/spikes/{spikeType}_{direction}00"];
                            int length = Math.Max(rect.Width / 8, rect.Height / 8);
                            for (int i = 0; i < length; i++)
                            {
                                Vector2 offset;
                                if (dataName.EndsWith("Up"))
                                {
                                    offset = new(4 + i * 8, -4);
                                }
                                else if (dataName.EndsWith("Down"))
                                {
                                    offset = new(4 + i * 8, 4);
                                }
                                else if (dataName.EndsWith("Right"))
                                {
                                    offset = new(4, 4 + i * 8);
                                }
                                else
                                {
                                    offset = new(-4, 4 + i * 8);
                                }

                                actions.Insert(0, (level) => DrawCentered(texture, position + offset));
                            }
                        }
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Spring")))
                    {
                        if (dataName.EndsWith("Left"))
                        {
                            rect = CreateRect(position, 6, 16, 0, -8);
                            textureOffset = new Vector2(8, 0);
                            rotation = Math.PI / 2;
                        }
                        else if (dataName.EndsWith("Right"))
                        {
                            rect = CreateRect(position, 6, 16, -6, -8);
                            textureOffset = new Vector2(-8, 0);
                            rotation = -Math.PI / 2;
                        }
                        else if (dataName.EndsWith("Ceiling"))
                        {
                            rect = CreateRect(position, 16, 6, -8, 0);
                        }
                        else
                        {
                            rect = CreateRect(position, 16, 6, -8, -6);
                            textureOffset = new Vector2(0, -8);
                        }

                        outline = true;
                        textureId = "objects/spring/00";
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Refill")))
                    {
                        rect = CreateRect(position, 16, 16, -8, -8);
                        textureId = data.Bool("twoDash") ? "objects/refillTwo/idle00" : "objects/refill/idle00";
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.HeartGem")) &&
                             (engine.Frame.Scenes.CurrentScene as OriginalLevelSceneRoot)?.Session is { } session)
                    {
                        rect = CreateRect(position, 16, 16, -8, -8);

                        int id = (int)session.Mode;
                        if (data.Bool("fake"))
                        {
                            id = 3;
                        }

                        id = ScalarMath.Clamp(id, 0, 3);
                        textureId = $"collectables/heartGem/{id}/00";
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Cassette")))
                    {
                        rect = CreateRect(position, 16, 16, -8, -8);
                        textureId = "collectables/cassette/idle00";
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.TouchSwitch")))
                    {
                        rect = CreateRect(position, 16, 16, -8, -8);
                        textureId = "objects/touchswitch/container";
                        AtlasEntry texture = gameAtlas["objects/touchswitch/icon00"];
                        actions.Insert(0, (level) => DrawCentered(texture, position));
                    }
                    else if (types.TypeOf("Celeste.Mod.IStrawberry").IsAssignableFrom(type))
                    {
                        rect = CreateRect(position, 14, 14, -7, -7);

                        if (data.Nodes != null && data.Nodes.Length != 0)
                        {
                            AtlasEntry texture = gameAtlas["collectables/strawberry/seed00"];
                            foreach (Vector2 node in data.Nodes)
                            {
                                actions.Add((level) =>
                                    HollowRect(levelPosition + node - new Vector2(6, 6), 12, 12,
                                        settings.EntityHitboxColor * colorAlpha));
                                actions.Add((level) => DrawCentered(texture, levelPosition + node));
                            }
                        }

                        if (data.Bool("moon"))
                        {
                            textureId = "collectables/moonBerry/normal00";
                        }
                        else if (data.Name is "memorialTextController" or "goldenBerry")
                        {
                            textureId = "collectables/goldberry/idle00";
                        }
                        else
                        {
                            textureId = "collectables/strawberry/normal00";
                        }
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.CrumblePlatform")))
                    {
                        rect.Height = 8;
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Lookout")))
                    {
                        rect = CreateRect(position, 4, 4, -2, -4);
                        Vector2 talkPosition = position - new Vector2(24, 8);
                        actions.Add((level) => HollowRect(talkPosition, 48, 8, PackedColor.Green * colorAlpha));
                        textureId = "objects/lookout/lookout05";
                        textureOffset = new Vector2(0, -16);
                    }
                    else if (type == types.TypeOf("Celeste.BadelineOldsite"))
                    {
                        rect = CreateRect(position, 6, 6, -3, -7);
                        textureId = "characters/player_badeline/fallPose10";
                        textureOffset = new Vector2(0, -16);
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Key")))
                    {
                        rect = CreateRect(position, 12, 12, -6, -6);
                        textureId = "collectables/key/idle00";
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.LockBlock")))
                    {
                        rect = CreateRect(position, 32, 32, 0, 0);

                        textureId = data.Attr("sprite", "wood") switch
                        {
                            "temple_a" => "objects/door/lockdoorTempleA00",
                            "temple_b" => "objects/door/lockdoorTempleB00",
                            "moon" => "objects/door/moonDoor11",
                            _ => "objects/door/lockdoor00"
                        };

                        textureOffset = new Vector2(16, 16);
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.MrOshiroDoor")))
                    {
                        rect = CreateRect(position, 32, 32, 0, 0);
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.AngryOshiro")))
                    {
                        actions.Add((level) =>
                            Circle(position + new Vector2(3, 4), 14, settings.EntityHitboxColor * colorAlpha, 4));
                        rect = CreateRect(position, 28, 6, -11, -11);
                        color = PackedColor.HotPink;

                        textureId = "characters/oshiro/boss12";
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Booster")) ||
                             lowercaseDataName.EndsWith("booster") && !lowercaseDataName.EndsWith("wallbooster"))
                    {
                        rect = IntRect.Empty;
                        actions.Add((level) =>
                            Circle(position + new Vector2(0, 2), 10, settings.EntityHitboxColor * colorAlpha, 4));
                        if (data.Bool("red"))
                        {
                            textureId = "objects/booster/boosterRed00";
                        }
                        else
                        {
                            textureId = "objects/booster/booster00";
                        }

                        outline = true;
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Cloud")))
                    {
                        if (data.Bool("small") ||
                            (engine.Frame.Scenes.CurrentScene as OriginalLevelSceneRoot)?.Session?.Mode != 0)
                        {
                            rect = CreateRect(position, 28, 5, -14, 0);
                            textureId = data.Bool("fragile")
                                ? "objects/clouds/fragileRemix00"
                                : "objects/clouds/cloudRemix00";
                        }
                        else
                        {
                            rect = CreateRect(position, 32, 5, -16, 0);
                            textureId = data.Bool("fragile") ? "objects/clouds/fragile00" : "objects/clouds/cloud00";
                        }
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.IntroCar")))
                    {
                        rect = CreateRect(position, 25, 4, -15, -17);
                        actions.Add((level) => HollowRect(CreateRect(position, 19, 4, 8, -11),
                            settings.PlatformHitboxColor * colorAlpha));
                        textureId = "scenery/car/wheels";
                        textureOffset = new Vector2(0, -9);

                        AtlasEntry carBody = gameAtlas["scenery/car/body"];
                        actions.Add((level) => DrawCentered(carBody, position + textureOffset));
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Bridge")))
                    {
                        rect.Height = 5;
                        color = settings.PlatformHitboxColor;
                    }
                    else if (type.IsSubclassOf(types.TypeOf("Celeste.JumpThru")))
                    {
                        rect.Height = 5;
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.TempleGate")))
                    {
                        rect.Width = 8;
                        switch (data.Attr("sprite", "default"))
                        {
                            case "mirror":
                                textureId = "objects/door/TempleDoorB00";
                                textureOffset = new Vector2(4, 24);
                                break;
                            case "theo":
                                textureId = "objects/door/TempleDoorC00";
                                textureOffset = new Vector2(4, 24);
                                break;
                            default:
                                textureId = "objects/door/TempleDoor00";
                                textureOffset = new Vector2(4, 16);
                                break;
                        }
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.DashSwitch")))
                    {
                        if (data.Attr("sprite", "default") == "default")
                        {
                            textureId = "objects/temple/dashButton00";
                        }
                        else
                        {
                            textureId = "objects/temple/dashButtonMirror00";
                        }

                        textureOffset = new Vector2(8, 8);

                        if (dataName == "dashSwitchH")
                        {
                            rect.Width = 8;
                            rect.Height = 16;
                            if (data.Bool("leftSide"))
                            {
                                scale = new Vector2(-1, 1);
                                textureOffset = new Vector2(0, 8);
                            }
                        }
                        else
                        {
                            rect.Width = 16;
                            rect.Height = 8;
                            if (data.Bool("ceiling"))
                            {
                                rotation = -Math.PI / 2;
                                textureOffset = new Vector2(8, 0);
                            }
                            else
                            {
                                rotation = Math.PI / 2;
                            }
                        }
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Seeker")))
                    {
                        rect = CreateRect(position, 12, 8, -6, -2);
                        actions.Add((level) =>
                            HollowRect(CreateRect(position, 12, 6, -6, -8), PackedColor.HotPink * colorAlpha));
                        textureId = "characters/monsters/predator00";
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.SeekerBarrier")))
                    {
                        actions.Add((level) => FilledRect(rect, PackedColor.LightBlue * 0.2f));
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.TheoCrystal")))
                    {
                        rect = CreateRect(position, 8, 10, -4, -10);
                        actions.Add((level) =>
                            HollowRect(CreateRect(position, 16, 22, -8, -16), PackedColor.Pink * colorAlpha));
                        textureId = "characters/theoCrystal/idle00";
                        textureOffset = new Vector2(0, -10);
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.TempleBigEyeball")))
                    {
                        rect = CreateRect(position, 48, 64, -24, -32);
                        textureId = "danger/templeeye/body04";
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.FlyFeather")))
                    {
                        rect = CreateRect(position, 20, 20, -10, -10);
                        textureId = "objects/flyFeather/idle00";
                        if (data.Bool("shielded"))
                        {
                            actions.Add((level) => Circle(position, 10f, PackedColor.White, 3));
                        }
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.FinalBoss")))
                    {
                        rect = IntRect.Empty;
                        actions.Add((level) =>
                            Circle(position - new Vector2(0, 6), 14, settings.EntityHitboxColor * colorAlpha, 4));
                        textureId = "characters/badelineBoss/boss00";
                        textureOffset = new Vector2(0, -14);
                        scale = new Vector2(-1, 1);
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.BadelineBoost")))
                    {
                        rect = IntRect.Empty;
                        actions.Add((level) => Circle(position, 16, settings.EntityHitboxColor, 4));
                        textureId = "objects/badelineboost/idle00";

                        if (data.Nodes != null && data.Nodes.Length != 0)
                        {
                            AtlasEntry texture = gameAtlas[textureId];
                            foreach (Vector2 node in data.Nodes)
                            {
                                actions.Insert(0, (level) => DrawCentered(texture, levelPosition + node));
                                actions.Add((level) =>
                                    Circle(levelPosition + node, 16, settings.EntityHitboxColor * colorAlpha, 4));
                            }
                        }
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.SummitGem")))
                    {
                        rect = CreateRect(position, 12, 12, -6, -6);
                        int gemId = data.Int("gem");
                        textureId = $"collectables/summitgems/{gemId}/gem00";
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.WallBooster")))
                    {
                        rect = CreateRect(position, data.Width, data.Height, data.Bool("left") ? 1 : 7, 0);
                        color = EntityColorInverselyLessAlpha;
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.FireBall")))
                    {
                        rect = IntRect.Empty;
                        actions.Add((level) => Circle(position, 6, PackedColor.Goldenrod * colorAlpha, 4));
                        textureId = "objects/fireball/fireball00";
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.CoreModeToggle")))
                    {
                        rect = CreateRect(position, 16, 24, -8, -12);
                        textureId = "objects/coreFlipSwitch/o1";
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.CassetteBlock")))
                    {
                        PackedColor cassetteColor = data.Int("index") switch
                        {
                            1 => SourceHexColor.HexToColor("f049be"),
                            2 => SourceHexColor.HexToColor("fcdc3a"),
                            3 => SourceHexColor.HexToColor("38e04e"),
                            _ => SourceHexColor.HexToColor("49aaf0")
                        };
                        actions.Insert(0, (level) => FilledRect(rect, cassetteColor));
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Lightning")))
                    {
                        rect.X += 1;
                        rect.Y += 1;
                        rect.Width -= 2;
                        rect.Height -= 2;
                        actions.Insert(0, (level) => FilledRect(rect, PackedColor.Yellow * 0.1f));
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Puffer")))
                    {
                        rect = CreateRect(position, 14, 12, -7, -7);
                        color = PackedColor.HotPink;
                        actions.Add((level) =>
                            HollowRect(CreateRect(position, 12, 10, -6, -5), settings.EntityHitboxColor * colorAlpha));
                        textureId = "objects/puffer/idle00";
                        if (!data.Bool("right"))
                        {
                            scale = new Vector2(-1, 1);
                        }
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.Glider")))
                    {
                        rect = CreateRect(position, 8, 10, -4, -10);
                        actions.Add((level) =>
                            HollowRect(CreateRect(position, 20, 22, -10, -16), PackedColor.Pink * colorAlpha));
                        textureId = "objects/glider/idle0";
                        textureOffset = new Vector2(0, -4);
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.FlingBird")))
                    {
                        rect = IntRect.Empty;
                        actions.Add((level) => Circle(position, 16, settings.EntityHitboxColor * colorAlpha, 4));
                        textureId = "characters/bird/fly03";
                        scale = new Vector2(-1, 1);
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.LightningBreakerBox")))
                    {
                        rect.Width = rect.Height = 32;
                        textureId = "objects/breakerBox/idle00";
                        textureOffset = new(16, 16);
                    }
                    else if (type == types.TypeOf("Celeste.ClutterSwitch"))
                    {
                        rect.Width = 32;
                        rect.Height = 16;
                    }

                    if (type.IsSubclassOf(types.TypeOf("Monocle.Platform")))
                    {
                        color = settings.PlatformHitboxColor;
                    }
                    else if (IsSameOrSubclassOf(type, types.TypeOf("Celeste.TriggerSpikes"),
                                 types.TypeOf("Celeste.TriggerSpikesOriginal")))
                    {
                        color = EntityColorInverselyLessAlpha;
                    }

                    if (color.A == 255)
                    {
                        color *= colorAlpha;
                    }

                    if (type == types.TypeOf("Celeste.ClutterBlockBase"))
                    {
                        color *= colorAlpha;
                        actions.Add((level) =>
                        {
                            switch (dataName)
                            {
                                case "redBlocks" when !level.Session.GetFlag("oshiro_clutter_cleared_0"):
                                case "greenBlocks" when !level.Session.GetFlag("oshiro_clutter_cleared_1"):
                                case "yellowBlocks" when !level.Session.GetFlag("oshiro_clutter_cleared_2"):
                                    HollowRect(rect, color);
                                    break;
                            }
                        });
                        continue;
                    }
                }
                else
                {
                    type = null;
                }

                if (!string.IsNullOrEmpty(textureId))
                {
                    AtlasEntry texture = gameAtlas[textureId];
                    if (outline)
                    {
                        actions.Insert(0,
                            (level) => DrawOutlineCentered(texture, position + textureOffset, PackedColor.White, scale,
                                (float)rotation));
                    }
                    else
                    {
                        actions.Insert(0,
                            (level) => DrawCentered(texture, position + textureOffset, PackedColor.White, scale,
                                (float)rotation));
                    }
                }

                if (rect.Width == 0 || rect.Height == 0)
                {
                    continue;
                }

                actions.Add((level) => { HollowRect(rect, color); });
            }
        }

        private void DrawTriggerHitbox(OriginalLevelRoomData levelData, List<Action<OriginalLevelSceneRoot>> actions)
        {
            PackedColor respawnTriggerColor = PackedColor.YellowGreen * colorAlpha;

            foreach (OriginalLevelEntityData data in levelData.Triggers)
            {
                PackedColor triggerColor = settings.TriggerHitboxColor * colorAlpha;
                if (data.Name == "changeRespawnTrigger")
                {
                    triggerColor = respawnTriggerColor;
                }

                float width = data.Width;
                float height = data.Height;

                actions.Add((level) =>
                {
                    if (settings.ShowTriggerHitboxes)
                    {
                        HollowRect(levelData.Position + data.Position, width, height, triggerColor);
                    }
                });
            }

            foreach (Vector2 spawn in levelData.Spawns)
            {
                actions.Add((level) =>
                {
                    if (settings.ShowTriggerHitboxes)
                    {
                        HollowRect(spawn - new Vector2(4, 11), 8, 11, respawnTriggerColor);
                    }
                });
            }
        }

        private void DrawDecalHitbox(OriginalLevelRoomData levelData, List<Action> actions)
        {
            PackedColor color = settings.PlatformHitboxColor * colorAlpha;

            foreach (OriginalDecalPlacement data in levelData.FgDecals)
            {
                IntRect rect = new();
                Vector2 position = levelData.Position + data.Position;

                string texture = data.Texture;
                if (string.IsNullOrEmpty(Path.GetExtension(texture)))
                {
                    texture += ".png";
                }

                string extension = Path.GetExtension(texture);
                string input = Path.Combine("decals", texture.Replace(extension, "")).Replace('\\', '/');
                string name = Regex.Replace(input, "\\d+$", string.Empty);
                string decalName = name.ToLower().Replace("decals/", "");

                actions.Add(() =>
                {
                    if (!settings.SimplifiedGraphics || !settings.SimplifiedDecal)
                    {
                        DrawCentered(gameAtlas.GetAtlasSubtextures(name)[0], position, PackedColor.White, data.Scale);
                    }
                });

                switch (decalName)
                {
                    case "3-resort/roofcenter":
                    case "3-resort/roofcenter_b":
                    case "3-resort/roofcenter_c":
                    case "3-resort/roofcenter_d":
                        rect = CreateRect(position, 16, 8, -8, -4);
                        break;
                    case "3-resort/roofedge":
                    case "3-resort/roofedge_b":
                    case "3-resort/roofedge_c":
                    case "3-resort/roofedge_d":
                        rect = CreateRect(position, 8, 8, data.Scale.X >= 0 ? -8 : 0, -4);
                        break;
                    case "3-resort/bridgecolumntop":
                        actions.Add(() => HollowRect(CreateRect(position, 10, 8, -5, 0), color));
                        rect = CreateRect(position, 16, 8, -8, -8);
                        break;
                    case "3-resort/bridgecolumn":
                        rect = CreateRect(position, 10, 16, -5, -8);
                        break;
                    case "3-resort/brokenelevator":
                        rect = CreateRect(position, 32, 48, -16, -20);
                        break;
                    case "4-cliffside/bridge_a":
                        rect = CreateRect(position, 48, 8, -24, 0);
                        break;
                    default:
                        if (!decals.RegisteredDecals.TryGetValue(decalName, out var decalInfo))
                        {
                            break;
                        }

                        if (decalInfo.CustomProperties.FirstOrDefault(pair => pair.Key == "solid").Value is { } attrs)
                        {
                            float.TryParse(attrs["x"]?.Value, out float x);
                            float.TryParse(attrs["y"]?.Value, out float y);
                            float.TryParse(attrs["width"]?.Value, out float width);
                            float.TryParse(attrs["height"]?.Value, out float height);
                            rect = CreateRect(position, (int)width, (int)height, (int)x, (int)y);
                        }

                        break;
                }

                if (rect.Equals(default(IntRect)))
                {
                    continue;
                }

                actions.Add(() => HollowRect(rect, color));
            }
        }

        private static IntRect CreateRect(Vector2 position, int width, int height, int x, int y)
        {
            return new IntRect((int)(position.X + x), (int)(position.Y + y), width, height);
        }
    }
}
