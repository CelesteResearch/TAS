using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Gameplay
{
    internal sealed class SourceTasBetterInvincibleProcess
    {
        private readonly TasPlaybackDriver driver;

        internal SourceTasBetterInvincibleProcess(TasPlaybackDriver driver) => this.driver = driver;

        // The source TAS field is deliberately independent of SaveData.Assists.Invincible.
        public bool Invincible = false;

        [SourceTasInitialize]
        private static void SourceInitialize() => CelesteTasModule.Instance.Driver.SourceBetterInvincible.Initialize();

        internal void Initialize() => driver.InstallSourceBetterInvinciblePlayerDieHook(this);

        internal bool ModifyInvincible(bool originalValue)
            => originalValue || (driver.Running && Invincible && driver.Settings.BetterInvincible);

        [SourceTasEnableRun]
        private static void SourceEnableRun() => CelesteTasModule.Instance.Driver.SourceBetterInvincible.EnableRun();

        internal void EnableRun() => Invincible = false;
    }
}
