using Celeste.Game.Minigames.CelesteClassic.Extensions;
using Celeste.Game.Minigames.CelesteClassic.Gameplay;
using Celeste.Game.Minigames.CelesteClassic.Emulation;
using Celeste.Mods.Tas.Gameplay.Classic;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : IClassicBehaviorEnvironment, IClassicBehaviorHooks
    {
        private TasPico8Fixer sourceTasClassicHooks;
        IClassicBehaviorHooks IClassicBehaviorEnvironment.ClassicBehaviorHooks => this;

        internal void InstallSourceTasClassicHooks(TasPico8Fixer hooks) => sourceTasClassicHooks = hooks;
        internal void UninstallSourceTasClassicHooks() => sourceTasClassicHooks = null;

        void IClassicBehaviorHooks.EmulatorConstructing() => sourceTasClassicHooks?.EmulatorConstructing();

        void IClassicBehaviorHooks.ClassicInitialized(Pico8Classic game, ClassicConsoleRuntime emulator)
            => sourceTasClassicHooks?.ClassicInitialized(game, emulator);

        void IClassicBehaviorHooks.ClassicUpdated(Pico8Classic game) => sourceTasClassicHooks?.ClassicUpdated(game);

        void IClassicBehaviorHooks.BeforeBalloonInit(Pico8Classic game) =>
            sourceTasClassicHooks?.BeforeBalloonInit(game);

        void IClassicBehaviorHooks.AfterBalloonInit() => sourceTasClassicHooks?.AfterBalloonInit();

        bool IClassicBehaviorHooks.BeforeChestUpdate(Pico8Classic game, float timer)
            => sourceTasClassicHooks?.BeforeChestUpdate(game, timer) ?? false;

        void IClassicBehaviorHooks.AfterChestUpdate(bool running) => sourceTasClassicHooks?.AfterChestUpdate(running);
    }
}
