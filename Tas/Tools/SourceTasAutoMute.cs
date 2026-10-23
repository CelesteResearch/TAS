using System;
using Celeste.Core.Diagnostics.Tas;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Celeste.Audio.Fmod.Events;
using Celeste.Audio.Fmod.Interop;
using Celeste.Mods.Tas.Module;
using Celeste.Game.Settings;

namespace Celeste.Mods.Tas.Tools
{
    internal sealed class SourceTasAutoMute
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;

        internal SourceTasAutoMute(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        private static readonly HashSet<string> LoopAudioPaths = new()
        {
            "event:/char/madeline/wallslide",
            "event:/char/madeline/dreamblock_travel",
            "event:/char/madeline/water_move_shallow",
            "event:/char/badeline/wallslide",
            "event:/char/badeline/dreamblock_travel",
            "event:/char/badeline/water_move_shallow",
            "event:/char/badeline/boss_bullet",
            "event:/ui/game/memorial_dream_loop",
            "event:/ui/game/memorial_dream_text_loop",
            "event:/ui/game/memorial_text_loop",
            "event:/game/general/birdbaby_tweet_loop",
            "event:/game/general/crystalheart_blue_get",
            "event:/game/general/crystalheart_red_get",
            "event:/game/general/crystalheart_gold_get",
            "event:/game/00_prologue/bridge_rumble_loop",
            "event:/game/01_forsaken_city/birdbros_fly_loop",
            "event:/game/01_forsaken_city/console_static_loop",
            "event:/game/02_old_site/sequence_phone_ring_loop",
            "event:/game/02_old_site/sequence_phone_ringtone_loop",
            "event:/game/03_resort/platform_vert_down_loop",
            "event:/game/03_resort/platform_vert_up_loop",
            "event:/game/04_cliffside/arrowblock_move",
            "event:/game/04_cliffside/gondola_movement_loop",
            "event:/game/04_cliffside/gondola_halted_loop",
            "event:/game/04_cliffside/gondola_movement_loop",
            "event:/game/05_mirror_temple/mainmirror_torch_loop",
            "event:/game/05_mirror_temple/redbooster_move",
            "event:/game/05_mirror_temple/swapblock_return",
            "event:/game/06_reflection/badeline_pull_rumble_loop",
            "event:/game/06_reflection/crushblock_move_loop",
            "event:/game/06_reflection/crushblock_move_loop_covert",
            "event:/game/06_reflection/crushblock_return_loop",
            "event:/game/06_reflection/feather_state_loop",
            "event:/game/06_reflection/badeline_pull_rumble_loop",
            "event:/game/09_core/conveyor_activate",
            "event:/game/09_core/rising_threat",
            "event:/new_content/game/10_farewell/glider_movement",
            "event:/new_content/game/10_farewell/fakeheart_get",
            "event:/CommunalHelperEvents/game/connectedDreamBlock/dreamblock_fly_travel",
            "event:/CommunalHelperEvents/game/dreamSwapBlock/dream_swap_block_return",
            "event:/CommunalHelperEvents/game/redirectMoveBlock/arrowblock_move",
            "event:/CommunalHelperEvents/game/melvin/move_loop",
            "event:/CommunalHelperEvents/game/railedMoveBlock/railedmoveblock_move",
            "event:/CommunalHelperEvents/game/customBoosters/dreamBooster/dreambooster_move",
        };

        private readonly IDictionary<WeakReference<FmodEventReference>, int> loopAudioInstances =
            new ConcurrentDictionary<WeakReference<FmodEventReference>, int>();

        private bool settingMusic, hasMuted;
        private WeakReference<FmodEventReference> dummy;
        private bool ShouldBeMuted => driver.Running && driver.PlaybackSpeed >= 2.0f && !settingMusic;

        private bool FrameStep =>
            driver.CurrState is SourceTasManagerState.Paused or SourceTasManagerState.FrameAdvance;

        private FmodEventReference DummyEventInstance
        {
            get
            {
                if (dummy == null || !dummy.TryGetTarget(out var dummyInstance))
                {
                    bool temp = settingMusic;
                    settingMusic = true;
                    dummyInstance = process.AudioSession.CreateInstance("event:/char/madeline/footstep", null, null);
                    settingMusic = temp;
                    dummyInstance.SetVolume(0);
                    dummy = new(dummyInstance);
                }

                return dummyInstance;
            }
        }

        [SourceTasLoad]
        private static void Load() =>
            TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process).InstallSourceTasAutoMuteHooks();

        [SourceTasUnload]
        private static void Unload() => TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process)
            .UninstallSourceTasAutoMuteHooks();

        internal bool SetMusic(Func<bool> original)
        {
            settingMusic = true;
            bool result = original();
            settingMusic = false;
            return result;
        }

        internal void SetAltMusic(Action original)
        {
            settingMusic = true;
            original();
            settingMusic = false;
        }

        internal FmodResult CreateInstance(string path, SourceFmodCreateEventInstance original,
            out FmodEventReference instance)
        {
            FmodResult result;
            if (ShouldBeMuted && !string.IsNullOrEmpty(path))
            {
                result = FmodResult.OK;
                instance = DummyEventInstance;
            }
            else result = original(out instance);

            if (!ShouldBeMuted && instance != null && !string.IsNullOrEmpty(path))
            {
                int delayFrames = -1;
                if (LoopAudioPaths.Contains(path)) delayFrames = 10;
                else if (path.StartsWith("event:/env/local/") || path.StartsWith("event:/new_content/env/") ||
                         path.StartsWith("event:/char/dialogue/")) delayFrames = 0;
                if (delayFrames >= 0) loopAudioInstances.Add(new(instance), delayFrames);
            }

            return result;
        }

        internal int MuteBeats(int leadBeats) => ShouldBeMuted ? 1 : leadBeats;

        internal void AfterSceneUpdate()
        {
            if (ShouldBeMuted && !hasMuted)
            {
                process.AudioSession.SfxVolume = 0f;
                hasMuted = true;
            }
            else if (!ShouldBeMuted && hasMuted)
            {
                ((ISettingsContext)process).Settings.ApplySFXVolume(process.AudioSession);
                hasMuted = false;
            }
        }

        internal void AfterCelesteUpdate()
        {
            if (!FrameStep) return;
            process.AudioSession.CurrentAmbienceEventInstance?.SetVolume(0);
            if (loopAudioInstances.Count > 0)
            {
                var copy = loopAudioInstances.Keys.ToArray();
                foreach (var weak in copy)
                {
                    if (weak.TryGetTarget(out var instance))
                    {
                        if (loopAudioInstances[weak] <= 0)
                        {
                            instance.SetVolume(0);
                            loopAudioInstances.Remove(weak);
                        }
                        else loopAudioInstances[weak]--;
                    }
                    else loopAudioInstances.Remove(weak);
                }
            }
        }
    }
}
