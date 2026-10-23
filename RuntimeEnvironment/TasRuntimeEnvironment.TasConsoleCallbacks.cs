using System;
using Celeste.Simulation;
using Celeste.Simulation.Gameplay.Player.Scene;
using Celeste.Mods.Tas;
using Celeste.Game.Entities.StarJump;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : ISourcePlayerCreationExtension,
        IOriginalTheoPlateauAwakeInterceptionEnvironment
    {
        private Func<RoomPlayerSession, RoomPlayerSession> sourceTasPlayerCreatedBeforeIntro;
        private Func<bool> sourceTasTheoPlateauCampfire;

        Func<bool> IOriginalTheoPlateauAwakeInterceptionEnvironment.BeforeTheoPlateauCampfire =>
            sourceTasTheoPlateauCampfire;

        internal void InstallSourceTasTheoPlateauHook(TasPlaybackDriver driver) =>
            sourceTasTheoPlateauCampfire = driver.ConsoleProcess.FixCh6FirstRoomLoad;

        internal void UninstallSourceTasTheoPlateauHook() => sourceTasTheoPlateauCampfire = null;

        internal void InstallSourceTasConsoleCallbacks(TasPlaybackDriver driver)
            => sourceTasPlayerCreatedBeforeIntro += driver.ConsoleProcess.SetPlayerRemainder;

        internal void UninstallSourceTasConsoleCallbacks(TasPlaybackDriver driver)
            => sourceTasPlayerCreatedBeforeIntro -= driver.ConsoleProcess.SetPlayerRemainder;

        RoomPlayerSession ISourcePlayerCreationExtension.AfterPlayerCreatedBeforeIntro(
            RoomPlayerSession actualPlayer)
        {
            if (sourceTasPlayerCreatedBeforeIntro == null) return actualPlayer;
            return sourceTasPlayerCreatedBeforeIntro(actualPlayer);
        }
    }
}
