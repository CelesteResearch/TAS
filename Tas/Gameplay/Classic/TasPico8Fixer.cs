using Celeste.Core.Diagnostics.Tas;
using Celeste.Core.Randomness;
using Celeste.Game.Minigames.CelesteClassic.Extensions;
using Celeste.Game.Minigames.CelesteClassic.Gameplay;
using Celeste.Game.Minigames.CelesteClassic.Emulation;

namespace Celeste.Mods.Tas.Gameplay.Classic
{
    public sealed class TasPico8Fixer : IClassicBehaviorHooks
    {
        public int Seed { get; private set; }
        public int Frames { get; private set; }
        private readonly ISourceTasManagerEnvironment managerEnvironment;
        private readonly ISourceRandomProcessEnvironment randomEnvironment;

        public TasPico8Fixer(ISourceTasManagerEnvironment managerEnvironment,
            ISourceRandomProcessEnvironment randomEnvironment)
        {
            this.managerEnvironment = managerEnvironment;
            this.randomEnvironment = randomEnvironment;
        }

        public void EmulatorConstructing() => Seed = 0;

        public void ClassicInitialized(Pico8Classic game, ClassicConsoleRuntime emulator)
        {
            if (managerEnvironment.TasManager.Running)
            {
                int levelIndex = emulator.BootLevel.X % 8 + emulator.BootLevel.Y * 8;
                bool doubleJump = levelIndex > 21 && levelIndex < 31;
                game.max_djump = doubleJump ? 2 : 1;
                game.new_bg = doubleJump;
            }
        }

        public void ClassicUpdated(Pico8Classic game)
        {
            if (game.level_index() < 30) Frames = game.frames;
        }

        public void BeforeBalloonInit(Pico8Classic game)
        {
            if (managerEnvironment.TasManager.Running)
                randomEnvironment.RandomProcess.PushRandom(unchecked(game.level_index() + Seed));
        }

        public void AfterBalloonInit()
        {
            if (managerEnvironment.TasManager.Running)
                randomEnvironment.RandomProcess.PopRandom();
        }

        public bool BeforeChestUpdate(Pico8Classic game, float timer)
        {
            bool running = game.has_key && managerEnvironment.TasManager.Running;
            if (running)
                randomEnvironment.RandomProcess.PushRandom(unchecked((int)timer + Seed));
            return running;
        }

        public void AfterChestUpdate(bool running)
        {
            if (running) randomEnvironment.RandomProcess.PopRandom();
        }
    }
}
