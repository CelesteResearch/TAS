using System;
using System.Collections.Generic;
using System.Numerics;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Randomness;
using Celeste.Game.Execution.Levels;
using MonoMod.Cil;
using MonoMod.Utils;

namespace Celeste.Mods.Tas.Gameplay.DesyncFix
{
    internal sealed partial class SourceTasSeededRandomness
    {
        internal sealed class AuraHelperLanternHandler : Handler
        {
            public override string Name => "AuraHelper_Lantern";
            internal ISourceRandom SharedRandom;
            private ISourceRandom lanternRandom;

            internal AuraHelperLanternHandler(SourceTasSeededRandomness owner) : base(owner)
            {
                SharedRandom = owner.NewRandom(1234);
            }

            public override void Init()
            {
                var type = owner.driver.SourceModReflection.TryGetType("AuraHelper", "AuraHelper.Lantern");
                if (type == null) return;
                var ctor = owner.AuraConstructor(type);
                if (ctor != null)
                    owner.driver.SourceHooks.IlHook(ctor, (cursor, _) =>
                    {
                        cursor.EmitLdarg(PositionArgument(ctor));
                        cursor.EmitDelegate<Action<Vector2>>(position =>
                        {
                            if (!owner.driver.Running) return;
                            int seed = owner.PositionHash(position);
                            var level = owner.CurrentLevel();
                            if (level != null)
                                seed = unchecked(seed +
                                                 ((global::Celeste.Game.Content.Maps.Levels.OriginalLevelRoomData)
                                                     level.Session.LevelData).LoadSeed);
                            SharedRandom = owner.NewRandom(seed);
                        });
                    });
                var update = owner.driver.SourceModReflection.GetMethodInfo(type, "Update")?.ActualMethod;
                if (update != null)
                    owner.driver.SourceHooks.IlHook(update, (cursor, _) =>
                    {
                        cursor.EmitDelegate<Action>(() =>
                            owner.RandomProcess.PushRandom(lanternRandom ?? SharedRandom));
                        while (cursor.TryGotoNext(MoveType.AfterLabel, instruction => instruction.MatchRet()))
                        {
                            cursor.EmitDelegate<Action>(owner.RandomProcess.PopRandom);
                            cursor.Index++;
                        }
                    });
            }

            public override void Reset() => lanternRandom = null;

            public override void PreUpdate()
            {
                if (NextSeed(out int seed))
                {
                    lanternRandom = owner.NewRandom(seed);
                    AssertNoSeedsRemaining();
                }
            }

            public override void SaveState(Dictionary<string, object> data)
            {
                data["AuraHelperLanternHandler_SharedRandom"] = owner.DeepClone(SharedRandom);
                data["AuraHelperLanternHandler_lanternRandom"] =
                    lanternRandom == null ? null : owner.DeepClone(lanternRandom);
            }

            public override void LoadState(Dictionary<string, object> data)
            {
                SharedRandom = owner.DeepClone((ISourceRandom)data["AuraHelperLanternHandler_SharedRandom"]);
                var random = (ISourceRandom)data["AuraHelperLanternHandler_lanternRandom"];
                lanternRandom = random == null ? null : owner.DeepClone(random);
            }
        }

        internal sealed class AuraHelperGeneratorHandler : Handler
        {
            public override string Name => "AuraHelper_Generator";
            private ISourceRandom generatorRandom;

            internal AuraHelperGeneratorHandler(SourceTasSeededRandomness owner) : base(owner)
            {
            }

            public override void Init()
            {
                var method = owner.driver.SourceModReflection
                    .TryGetMethod("AuraHelper", "AuraHelper.Generator", "Update")
                    ?.ActualMethod?.GetStateMachineTarget();
                if (method == null) return;
                owner.driver.SourceHooks.IlHook(method, (cursor, _) =>
                {
                    cursor.EmitDelegate<Action>(() =>
                        owner.RandomProcess.PushRandom(generatorRandom ?? owner.auraLantern.SharedRandom));
                    while (cursor.TryGotoNext(MoveType.AfterLabel, instruction => instruction.MatchRet()))
                    {
                        cursor.EmitDelegate<Action>(owner.RandomProcess.PopRandom);
                        cursor.Index++;
                    }
                });
            }

            public override void Reset() => generatorRandom = null;

            public override void PreUpdate()
            {
                if (NextSeed(out int seed))
                {
                    generatorRandom = owner.NewRandom(seed);
                    AssertNoSeedsRemaining();
                }
            }

            public override void SaveState(Dictionary<string, object> data) =>
                data["AuraHelperGeneratorHandler_generatorRandom"] =
                    generatorRandom == null ? null : owner.DeepClone(generatorRandom);

            public override void LoadState(Dictionary<string, object> data)
            {
                var random = (ISourceRandom)data["AuraHelperGeneratorHandler_generatorRandom"];
                generatorRandom = random == null ? null : owner.DeepClone(random);
            }
        }

        internal sealed class AurorasHelperHandler : Handler
        {
            public override string Name => "AurorasHelper_Shared";
            private readonly ReflectedMember f_currentSeed;
            private readonly ReflectedMember f_random;

            internal AurorasHelperHandler(SourceTasSeededRandomness owner) : base(owner)
            {
                f_currentSeed = owner.driver.SourceModReflection.GetField("AurorasHelper",
                    "Celeste.Mod.AurorasHelper.AurorasHelperModule", "currentSeed");
                f_random = owner.driver.SourceModReflection.GetField("AurorasHelper",
                    "Celeste.Mod.AurorasHelper.AurorasHelperModule", "random");
            }

            public override void Reset()
            {
                var module = owner.driver.SourceModReflection.GetModule("AurorasHelper");
                if (module != null && f_currentSeed != null && f_random != null)
                {
                    f_currentSeed.Write(null, 0);
                    f_random.Write(module, owner.NewRandom(0));
                }
            }

            public override void PreUpdate()
            {
                var module = owner.driver.SourceModReflection.GetModule("AurorasHelper");
                if (module != null && f_currentSeed != null && f_random != null && NextSeed(out int seed))
                {
                    int actualSeed = seed & 0x7FFFFFFF;
                    if (seed != actualSeed)
                        owner.driver.Controller.ReportWarning(SeedSource,
                            $"Target '{Name}' was provided a negative seed '{seed}', consider using '{actualSeed}' instead");
                    f_currentSeed.Write(null, actualSeed);
                    f_random.Write(module, owner.NewRandom(actualSeed));
                    AssertNoSeedsRemaining();
                }
            }
        }

        internal sealed class BossesHelperHandler : Handler
        {
            public override string Name => "BossesHelper_Shared";
            private readonly ReflectedMember f_TASSeed;

            internal BossesHelperHandler(SourceTasSeededRandomness owner) : base(owner)
            {
                f_TASSeed = owner.driver.SourceModReflection.GetField("BossesHelper",
                    "Celeste.Mod.BossesHelper.BossesHelperModule", "TASSeed");
            }

            public override void Reset()
            {
                var module = owner.driver.SourceModReflection.GetModule("BossesHelper");
                if (module != null && f_TASSeed != null) f_TASSeed.Write(module, 0);
            }

            public override void PreUpdate()
            {
                var module = owner.driver.SourceModReflection.GetModule("BossesHelper");
                if (module != null && f_TASSeed != null && NextSeed(out int seed))
                {
                    if (seed < 0)
                    {
                        owner.driver.Controller.ReportError(SeedSource,
                            $"Target '{Name}' was provided a negative seed '{seed}', which it doesn't support");
                        return;
                    }

                    f_TASSeed.Write(module, seed);
                    AssertNoSeedsRemaining();
                }
            }
        }

        internal sealed class CrystallineHelperHandler : Handler
        {
            public override string Name => "CrystallineHelper_Timer";

            internal CrystallineHelperHandler(SourceTasSeededRandomness owner) : base(owner)
            {
            }

            public override void Init() => owner.driver.SourceHooks.OverrideSeed(
                owner.driver.SourceModReflection.TryGetMethod("CrystallineHelper", "vitmod.TriggerTrigger", "GetSeed",
                        Type.EmptyTypes)
                    ?.ActualMethod, () => HasSeed, () =>
                {
                    NextSeed(out int seed);
                    return seed;
                });
        }

        internal sealed class SpekioToolboxShooterHandler : Handler
        {
            public override string Name => "SpekioToolbox_Shooter";
            private readonly ReflectedMember f_rnd;

            internal SpekioToolboxShooterHandler(SourceTasSeededRandomness owner) : base(owner)
            {
                f_rnd = owner.driver.SourceModReflection.GetField("Spekio's Toolbox",
                    "Celeste.Mod.SpekioToolbox.Shooter",
                    "rnd");
            }

            public override void Reset() => f_rnd?.Write(null, owner.NewRandom(0));

            public override void PreUpdate()
            {
                if (f_rnd != null && NextSeed(out int seed))
                {
                    f_rnd?.Write(null, owner.NewRandom(seed));
                    AssertNoSeedsRemaining();
                }
            }
        }

        internal sealed class PandorasBoxTileGlitcherHandler : Handler
        {
            public override string Name => "PandorasBox_TileGlitcher";
            private ISourceRandom glitcherRandom;

            internal PandorasBoxTileGlitcherHandler(SourceTasSeededRandomness owner) : base(owner)
            {
            }

            public override void Init() => owner.driver.SourceHooks.SeedMethod(
                owner.driver.SourceModReflection
                    .GetMethod("PandorasBox", "Celeste.Mod.PandorasBox.TileGlitcher", "tileGlitcher")?.ActualMethod
                    ?.GetStateMachineTarget(), () => glitcherRandom, owner.RandomProcess);

            public override void Reset() => glitcherRandom = null;

            public override void PreUpdate()
            {
                if (NextSeed(out int seed))
                {
                    glitcherRandom = owner.NewRandom(seed);
                    AssertNoSeedsRemaining();
                }
            }

            public override void SaveState(Dictionary<string, object> data) =>
                data["PandorasBoxTileGlitcherHandler_glitcherRandom"] =
                    glitcherRandom == null ? null : owner.DeepClone(glitcherRandom);

            public override void LoadState(Dictionary<string, object> data)
            {
                var random = (ISourceRandom)data["PandorasBoxTileGlitcherHandler_glitcherRandom"];
                glitcherRandom = random == null ? null : owner.DeepClone(random);
            }
        }

        internal sealed class VortexHelperColorSwitchHandler : Handler
        {
            public override string Name => "VortexHelper_ColorSwitch";
            private ISourceRandom switchRandom;

            internal VortexHelperColorSwitchHandler(SourceTasSeededRandomness owner) : base(owner)
            {
            }

            public override void Init() => owner.driver.SourceHooks.SeedMethod(
                owner.driver.SourceModReflection
                    .GetMethod("VortexHelper", "Celeste.Mod.VortexHelper.Entities.ColorSwitch", "Switch")?.ActualMethod,
                () => switchRandom, owner.RandomProcess);

            public override void Reset() => switchRandom = null;

            public override void PreUpdate()
            {
                if (NextSeed(out int seed))
                {
                    switchRandom = owner.NewRandom(seed);
                    AssertNoSeedsRemaining();
                }
            }

            public override void SaveState(Dictionary<string, object> data) =>
                data["VortexHelperColorSwitchHandler_switchRandom"] =
                    switchRandom == null ? null : owner.DeepClone(switchRandom);

            public override void LoadState(Dictionary<string, object> data)
            {
                var random = (ISourceRandom)data["VortexHelperColorSwitchHandler_switchRandom"];
                switchRandom = random == null ? null : owner.DeepClone(random);
            }
        }
    }
}
