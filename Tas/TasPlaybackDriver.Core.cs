using System;
using Celeste.Core.Framework.Components;
using Celeste.Game.Execution;
using Celeste.Core.Diagnostics.Tas;
using Celeste.Core.Mathematics;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        public static readonly float PlaybackDeltaTime = (float)TimeSpan.FromTicks(166667L).TotalSeconds;
        private float playbackOverhead;
        private float TargetFrames => PlaybackSpeed - playbackOverhead;

        // This original delegate is the downstream Celeste.Update hook chain with the same GameTime.
        // Neither this hook nor the input hook invokes RunOneFrame or advances another clock.
        private void UpdateSourceGameFrame(Action<GameRuntime, SourceGameTime> original, GameRuntime self,
            SourceGameTime gameTime)
        {
            if (!ReferenceEquals(self.Process, process) || !Settings.Enabled)
            {
                original(self, gameTime);
                return;
            }

            if (!ManagerRunning)
            {
                UpdateMeta();
                InvokeOriginalUpdate(original, self, gameTime);
                return;
            }

            UpdateMeta();
            var lastMetaUpdate = DateTime.UtcNow;
            int frames = 0;
            while (frames < (int)MathF.Ceiling(TargetFrames))
            {
                frames += 1;
                if (!InvokeOriginalUpdate(original, self, gameTime)) return;
                var now = DateTime.UtcNow;
                if ((now - lastMetaUpdate).TotalSeconds > PlaybackDeltaTime)
                {
                    process.PhysicalIO.Clock.PollEvents();
                    UpdateMeta();
                    lastMetaUpdate = now;
                }
            }

            playbackOverhead = ScalarMath.Mod(frames - TargetFrames, 1.0f);

            if (!Settings.HideFreezeFrames) return;
            while (process.Effects.FreezeTimer > 0.0f && ManagerRunning
                                                      && CurrState != SourceTasManagerState.Paused)
            {
                if (!InvokeOriginalUpdate(original, self, gameTime)) return;
            }
        }

        private bool InvokeOriginalUpdate(Action<GameRuntime, SourceGameTime> original, GameRuntime self,
            SourceGameTime gameTime)
        {
            try
            {
                original(self, gameTime);
                // The paused engine returns before its framework components are updated.
                // Keep queued main-thread services available without advancing gameplay.
                if (IsPausedAfterInput())
                    process.MainThreadProcess.Update(null);
                return true;
            }
            catch (Exception error)
            {
                if (!SyncChecker.Active) throw;
                SyncChecker.ReportCrash(error.ToString());
                DisableRun();
                return false;
            }
        }

        internal bool IsPausedAfterInput()
            => CurrState == SourceTasManagerState.Paused && !gameHost.Loading();
    }
}
