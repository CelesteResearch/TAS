using Celeste.Everest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Celeste.Scene.Tracking;
using Celeste.Everest.Reflection;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Everest.Modules.Entities.Metadata;

namespace Celeste.Mods.Tas.Gameplay.Hitboxes.Rooms
{
    public sealed class SourceTasEntityTypeHelper
    {
        private readonly SceneEntityCatalog catalog;
        private readonly SourceEverestReflectionProcessState reflection;
        private readonly EverestLoggerProcess logger;

        public SourceTasEntityTypeHelper(SceneEntityCatalog catalog,
            SourceEverestReflectionProcessState reflection, EverestLoggerProcess logger)
        {
            this.catalog = catalog;
            this.reflection = reflection;
            this.logger = logger;
        }

        public Type TypeOf(string sourceName)
            => catalog.ResolveRegisteredSourceType(sourceName)
               ?? throw new TypeLoadException("The actual source type is not bound: " + sourceName);

        private readonly Dictionary<string, Type> vanillaEntityNameToType = new();
        private readonly Dictionary<string, Type> modEntityNameToType = new();

        public Type NameToType(string entityName)
        {
            if (vanillaEntityNameToType.Count == 0)
            {
                CreateCache();
            }

            if (vanillaEntityNameToType.TryGetValue(entityName, out var ret))
            {
                return ret;
            }

            if (modEntityNameToType.TryGetValue(entityName, out ret))
            {
                return ret;
            }

            return null;
        }

        private void CreateCache()
        {
            var vanilla = new Dictionary<string, Type>
            {
                ["checkpoint"] = TypeOf("Celeste.Checkpoint"),
                ["jumpThru"] = TypeOf("Celeste.JumpthruPlatform"),
                ["refill"] = TypeOf("Celeste.Refill"),
                ["infiniteStar"] = TypeOf("Celeste.FlyFeather"),
                ["strawberry"] = TypeOf("Celeste.Strawberry"),
                ["memorialTextController"] = TypeOf("Celeste.Strawberry"),
                ["goldenBerry"] = TypeOf("Celeste.Strawberry"),
                ["summitgem"] = TypeOf("Celeste.SummitGem"),
                ["blackGem"] = TypeOf("Celeste.HeartGem"),
                ["dreamHeartGem"] = TypeOf("Celeste.DreamHeartGem"),
                ["spring"] = TypeOf("Celeste.Spring"),
                ["wallSpringLeft"] = TypeOf("Celeste.Spring"),
                ["wallSpringRight"] = TypeOf("Celeste.Spring"),
                ["fallingBlock"] = TypeOf("Celeste.FallingBlock"),
                ["zipMover"] = TypeOf("Celeste.ZipMover"),
                ["crumbleBlock"] = TypeOf("Celeste.CrumblePlatform"),
                ["dreamBlock"] = TypeOf("Celeste.DreamBlock"),
                ["touchSwitch"] = TypeOf("Celeste.TouchSwitch"),
                ["switchGate"] = TypeOf("Celeste.SwitchGate"),
                ["negaBlock"] = TypeOf("Celeste.NegaBlock"),
                ["key"] = TypeOf("Celeste.Key"),
                ["lockBlock"] = TypeOf("Celeste.LockBlock"),
                ["movingPlatform"] = TypeOf("Celeste.MovingPlatform"),
                ["rotatingPlatforms"] = TypeOf("Celeste.RotatingPlatform"),
                ["blockField"] = TypeOf("Celeste.BlockField"),
                ["cloud"] = TypeOf("Celeste.Cloud"),
                ["booster"] = TypeOf("Celeste.Booster"),
                ["moveBlock"] = TypeOf("Celeste.MoveBlock"),
                ["light"] = TypeOf("Celeste.PropLight"),
                ["switchBlock"] = TypeOf("Celeste.SwapBlock"),
                ["swapBlock"] = TypeOf("Celeste.SwapBlock"),
                ["dashSwitchH"] = TypeOf("Celeste.DashSwitch"),
                ["dashSwitchV"] = TypeOf("Celeste.DashSwitch"),
                ["templeGate"] = TypeOf("Celeste.TempleGate"),
                ["torch"] = TypeOf("Celeste.Torch"),
                ["templeCrackedBlock"] = TypeOf("Celeste.TempleCrackedBlock"),
                ["seekerBarrier"] = TypeOf("Celeste.SeekerBarrier"),
                ["theoCrystal"] = TypeOf("Celeste.TheoCrystal"),
                ["glider"] = TypeOf("Celeste.Glider"),
                ["theoCrystalPedestal"] = TypeOf("Celeste.TheoCrystalPedestal"),
                ["badelineBoost"] = TypeOf("Celeste.BadelineBoost"),
                ["cassette"] = TypeOf("Celeste.Cassette"),
                ["cassetteBlock"] = TypeOf("Celeste.CassetteBlock"),
                ["wallBooster"] = TypeOf("Celeste.WallBooster"),
                ["bounceBlock"] = TypeOf("Celeste.BounceBlock"),
                ["coreModeToggle"] = TypeOf("Celeste.CoreModeToggle"),
                ["iceBlock"] = TypeOf("Celeste.IceBlock"),
                ["fireBarrier"] = TypeOf("Celeste.FireBarrier"),
                ["eyebomb"] = TypeOf("Celeste.Puffer"),
                ["flingBird"] = TypeOf("Celeste.FlingBird"),
                ["flingBirdIntro"] = TypeOf("Celeste.FlingBirdIntro"),
                ["birdPath"] = TypeOf("Celeste.BirdPath"),
                ["lightningBlock"] = TypeOf("Celeste.LightningBreakerBox"),
                ["spikesUp"] = TypeOf("Celeste.Spikes"),
                ["spikesDown"] = TypeOf("Celeste.Spikes"),
                ["spikesLeft"] = TypeOf("Celeste.Spikes"),
                ["spikesRight"] = TypeOf("Celeste.Spikes"),
                ["triggerSpikesUp"] = TypeOf("Celeste.TriggerSpikes"),
                ["triggerSpikesDown"] = TypeOf("Celeste.TriggerSpikes"),
                ["triggerSpikesRight"] = TypeOf("Celeste.TriggerSpikes"),
                ["triggerSpikesLeft"] = TypeOf("Celeste.TriggerSpikes"),
                ["darkChaser"] = TypeOf("Celeste.BadelineOldsite"),
                ["rotateSpinner"] = TypeOf("Celeste.BladeRotateSpinner"),
                ["trackSpinner"] = TypeOf("Celeste.TrackSpinner"),
                ["spinner"] = TypeOf("Celeste.CrystalStaticSpinner"),
                ["sinkingPlatform"] = TypeOf("Celeste.SinkingPlatform"),
                ["friendlyGhost"] = TypeOf("Celeste.AngryOshiro"),
                ["seeker"] = TypeOf("Celeste.Seeker"),
                ["seekerStatue"] = TypeOf("Celeste.SeekerStatue"),
                ["slider"] = TypeOf("Celeste.Slider"),
                ["templeBigEyeball"] = TypeOf("Celeste.TempleBigEyeball"),
                ["crushBlock"] = TypeOf("Celeste.CrushBlock"),
                ["bigSpinner"] = TypeOf("Celeste.Bumper"),
                ["starJumpBlock"] = TypeOf("Celeste.StarJumpBlock"),
                ["floatySpaceBlock"] = TypeOf("Celeste.FloatySpaceBlock"),
                ["glassBlock"] = TypeOf("Celeste.GlassBlock"),
                ["goldenBlock"] = TypeOf("Celeste.GoldenBlock"),
                ["fireBall"] = TypeOf("Celeste.FireBall"),
                ["risingLava"] = TypeOf("Celeste.RisingLava"),
                ["sandwichLava"] = TypeOf("Celeste.SandwichLava"),
                ["killbox"] = TypeOf("Celeste.Killbox"),
                ["fakeHeart"] = TypeOf("Celeste.FakeHeart"),
                ["lightning"] = TypeOf("Celeste.Lightning"),
                ["finalBoss"] = TypeOf("Celeste.FinalBoss"),
                ["finalBossFallingBlock"] = TypeOf("Celeste.FallingBlock"),
                ["finalBossMovingBlock"] = TypeOf("Celeste.FinalBossMovingBlock"),
                ["fakeWall"] = TypeOf("Celeste.FakeWall"),
                ["fakeBlock"] = TypeOf("Celeste.FakeWall"),
                ["dashBlock"] = TypeOf("Celeste.DashBlock"),
                ["invisibleBarrier"] = TypeOf("Celeste.InvisibleBarrier"),
                ["exitBlock"] = TypeOf("Celeste.ExitBlock"),
                ["conditionBlock"] = TypeOf("Celeste.ExitBlock"),
                ["coverupWall"] = TypeOf("Celeste.CoverupWall"),
                ["crumbleWallOnRumble"] = TypeOf("Celeste.CrumbleWallOnRumble"),
                ["ridgeGate"] = TypeOf("Celeste.RidgeGate"),
                ["tentacles"] = TypeOf("Celeste.Tentacles"),
                ["starClimbController"] = TypeOf("Celeste.StarClimbGraphicsController"),
                ["playerSeeker"] = TypeOf("Celeste.PlayerSeeker"),
                ["chaserBarrier"] = TypeOf("Celeste.ChaserBarrier"),
                ["introCrusher"] = TypeOf("Celeste.IntroCrusher"),
                ["bridge"] = TypeOf("Celeste.Bridge"),
                ["bridgeFixed"] = TypeOf("Celeste.BridgeFixed"),
                ["bird"] = TypeOf("Celeste.BirdNPC"),
                ["introCar"] = TypeOf("Celeste.IntroCar"),
                ["memorial"] = TypeOf("Celeste.Memorial"),
                ["wire"] = TypeOf("Celeste.Wire"),
                ["cobweb"] = TypeOf("Celeste.Cobweb"),
                ["lamp"] = TypeOf("Celeste.Lamp"),
                ["hanginglamp"] = TypeOf("Celeste.HangingLamp"),
                ["hahaha"] = TypeOf("Celeste.Hahaha"),
                ["bonfire"] = TypeOf("Celeste.Bonfire"),
                ["payphone"] = TypeOf("Celeste.Payphone"),
                ["colorSwitch"] = TypeOf("Celeste.ClutterSwitch"),
                ["clutterDoor"] = TypeOf("Celeste.ClutterDoor"),
                ["dreammirror"] = TypeOf("Celeste.DreamMirror"),
                ["resortmirror"] = TypeOf("Celeste.ResortMirror"),
                ["towerviewer"] = TypeOf("Celeste.Lookout"),
                ["picoconsole"] = TypeOf("Celeste.PicoConsole"),
                ["wavedashmachine"] = TypeOf("Celeste.WaveDashTutorialMachine"),
                ["yellowBlocks"] = TypeOf("Celeste.ClutterBlockBase"),
                ["redBlocks"] = TypeOf("Celeste.ClutterBlockBase"),
                ["greenBlocks"] = TypeOf("Celeste.ClutterBlockBase"),
                ["oshirodoor"] = TypeOf("Celeste.MrOshiroDoor"),
                ["templeMirrorPortal"] = TypeOf("Celeste.TempleMirrorPortal"),
                ["reflectionHeartStatue"] = TypeOf("Celeste.ReflectionHeartStatue"),
                ["resortRoofEnding"] = TypeOf("Celeste.ResortRoofEnding"),
                ["gondola"] = TypeOf("Celeste.Gondola"),
                ["birdForsakenCityGem"] = TypeOf("Celeste.ForsakenCitySatellite"),
                ["whiteblock"] = TypeOf("Celeste.WhiteBlock"),
                ["plateau"] = TypeOf("Celeste.Plateau"),
                ["soundSource"] = TypeOf("Celeste.SoundSourceEntity"),
                ["templeMirror"] = TypeOf("Celeste.TempleMirror"),
                ["templeEye"] = TypeOf("Celeste.TempleEye"),
                ["clutterCabinet"] = TypeOf("Celeste.ClutterCabinet"),
                ["floatingDebris"] = TypeOf("Celeste.FloatingDebris"),
                ["foregroundDebris"] = TypeOf("Celeste.ForegroundDebris"),
                ["moonCreature"] = TypeOf("Celeste.MoonCreature"),
                ["lightbeam"] = TypeOf("Celeste.LightBeam"),
                ["door"] = TypeOf("Celeste.Door"),
                ["trapdoor"] = TypeOf("Celeste.Trapdoor"),
                ["resortLantern"] = TypeOf("Celeste.ResortLantern"),
                ["water"] = TypeOf("Celeste.Water"),
                ["waterfall"] = TypeOf("Celeste.WaterFall"),
                ["bigWaterfall"] = TypeOf("Celeste.BigWaterfall"),
                ["clothesline"] = TypeOf("Celeste.Clothesline"),
                ["cliffflag"] = TypeOf("Celeste.CliffFlags"),
                ["cliffside_flag"] = TypeOf("Celeste.CliffsideWindFlag"),
                ["flutterbird"] = TypeOf("Celeste.FlutterBird"),
                ["SoundTest3d"] = TypeOf("Celeste._3dSoundTest"),
                ["SummitBackgroundManager"] = TypeOf("Celeste.AscendManager"),
                ["summitGemManager"] = TypeOf("Celeste.SummitGem"),
                ["heartGemDoor"] = TypeOf("Celeste.HeartGemDoor"),
                ["summitcheckpoint"] = TypeOf("Celeste.SummitCheckpoint"),
                ["summitcloud"] = TypeOf("Celeste.SummitCloud"),
                ["coreMessage"] = TypeOf("Celeste.CoreMessage"),
                ["playbackTutorial"] = TypeOf("Celeste.PlayerPlayback"),
                ["playbackBillboard"] = TypeOf("Celeste.PlaybackBillboard"),
                ["cutsceneNode"] = TypeOf("Celeste.CutsceneNode"),
                ["kevins_pc"] = TypeOf("Celeste.KevinsPC"),
                ["npc"] = TypeOf("Celeste.NPC"),
                ["eventTrigger"] = TypeOf("Celeste.EventTrigger"),
                ["musicFadeTrigger"] = TypeOf("Celeste.MusicFadeTrigger"),
                ["musicTrigger"] = TypeOf("Celeste.MusicTrigger"),
                ["altMusicTrigger"] = TypeOf("Celeste.AltMusicTrigger"),
                ["cameraOffsetTrigger"] = TypeOf("Celeste.CameraOffsetTrigger"),
                ["lightFadeTrigger"] = TypeOf("Celeste.LightFadeTrigger"),
                ["bloomFadeTrigger"] = TypeOf("Celeste.BloomFadeTrigger"),
                ["cameraTargetTrigger"] = TypeOf("Celeste.CameraTargetTrigger"),
                ["cameraAdvanceTargetTrigger"] = TypeOf("Celeste.CameraAdvanceTargetTrigger"),
                ["respawnTargetTrigger"] = TypeOf("Celeste.RespawnTargetTrigger"),
                ["changeRespawnTrigger"] = TypeOf("Celeste.ChangeRespawnTrigger"),
                ["windTrigger"] = TypeOf("Celeste.WindTrigger"),
                ["windAttackTrigger"] = TypeOf("Celeste.WindAttackTrigger"),
                ["minitextboxTrigger"] = TypeOf("Celeste.MiniTextboxTrigger"),
                ["oshiroTrigger"] = TypeOf("Celeste.OshiroTrigger"),
                ["interactTrigger"] = TypeOf("Celeste.InteractTrigger"),
                ["checkpointBlockerTrigger"] = TypeOf("Celeste.CheckpointBlockerTrigger"),
                ["lookoutBlocker"] = TypeOf("Celeste.LookoutBlocker"),
                ["stopBoostTrigger"] = TypeOf("Celeste.StopBoostTrigger"),
                ["noRefillTrigger"] = TypeOf("Celeste.NoRefillTrigger"),
                ["ambienceParamTrigger"] = TypeOf("Celeste.AmbienceParamTrigger"),
                ["creditsTrigger"] = TypeOf("Celeste.CreditsTrigger"),
                ["goldenBerryCollectTrigger"] = TypeOf("Celeste.GoldBerryCollectTrigger"),
                ["moonGlitchBackgroundTrigger"] = TypeOf("Celeste.MoonGlitchBackgroundTrigger"),
                ["blackholeStrength"] = TypeOf("Celeste.BlackholeStrengthTrigger"),
                ["rumbleTrigger"] = TypeOf("Celeste.RumbleTrigger"),
                ["birdPathTrigger"] = TypeOf("Celeste.BirdPathTrigger"),
                ["spawnFacingTrigger"] = TypeOf("Celeste.SpawnFacingTrigger"),
                ["detachFollowersTrigger"] = TypeOf("Celeste.DetachStrawberryTrigger"),
                ["powerSourceNumber"] = TypeOf("Celeste.PowerSourceNumber"),
            };
            foreach (var pair in vanilla) vanillaEntityNameToType.Add(pair.Key, pair.Value);

            foreach (Type type in reflection.GetFakeEntryAssembly().GetTypes().Where(reflection.TypeSafety.IsSafe))
            {
                CheckCustomEntity(type);
            }
        }

        private void CheckCustomEntity(Type type)
        {
            foreach (string idFull in type.GetCustomAttributes<CustomEntityAttribute>().SelectMany(a => a.IDs))
            {
                string id;
                string[] split = idFull.Split('=');

                if (split.Length is 1 or 2)
                {
                    id = split[0];
                }
                else
                {
                    // invalid
                    continue;
                }

                string idTrim = id.Trim();
                if (vanillaEntityNameToType.TryGetValue(idTrim, out var vanillaType))
                {
                    logger.Log(EverestLogLevel.Warn, "CelesteTAS",
                        $"Found duplicate entity name {idTrim} - {type.FullName} vs {vanillaType.FullName}"
                    );
                }

                modEntityNameToType[idTrim] = type;
            }
        }
    }
}
