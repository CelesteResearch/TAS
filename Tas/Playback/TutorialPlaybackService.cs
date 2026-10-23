using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Celeste.Game.Execution.Levels;
using Celeste.Input.Devices.GamePads;
using Celeste.Input.Runtime;
using MonoMod.RuntimeDetour;

namespace Celeste.Mods.Tas.Playback
{
    public sealed partial class TutorialPlaybackService : IDisposable
    {
        private readonly GameApplication engine;
        private readonly Dictionary<string, TutorialPlaybackClip> clips = new();
        private readonly List<IDisposable> hooks = new();
        private TutorialPlaybackCursor cursor;
        private SourceGamePadState lastState;

        public bool Active { get; private set; }
        public bool Paused { get; private set; }

        public TutorialPlaybackService(GameApplication engine)
        {
            this.engine = engine ?? throw new ArgumentNullException(nameof(engine));
            try
            {
                hooks.Add(new Hook(typeof(SourceMInputProcess).GetMethod(nameof(SourceMInputProcess.Update)),
                    (Action<Action<SourceMInputProcess>, SourceMInputProcess>)UpdateInput));
                hooks.Add(new Hook(typeof(OriginalLevelSceneRoot).GetMethod("EndPauseEffects",
                        BindingFlags.Instance | BindingFlags.NonPublic),
                    (Action<Action<OriginalLevelSceneRoot>, OriginalLevelSceneRoot>)EndPauseEffects));
                hooks.Add(new Hook(typeof(SourceGamePadData).GetMethod(nameof(SourceGamePadData.Rumble)),
                    (Action<Action<SourceGamePadData, float, float>, SourceGamePadData, float, float>)Rumble));
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        public float TotalTime
        {
            get
            {
                int frames = 0;
                foreach (var clip in clips.Values) frames = checked(frames + clip.TotalFrames);
                return frames * engine.DeltaTime;
            }
        }

        public bool Preload(string path, Stream input)
        {
            var clip = TutorialPlaybackClip.Parse(path, input);
            if (clip == null) return false;
            clips[path] = clip;
            return true;
        }

        public void Clear() => clips.Clear();

        public void Play(string path)
        {
            if (!clips.TryGetValue(path, out var clip))
            {
                if (!engine.SourceContentRegistry.TryGet(path, out var asset)) return;
                using var stream = asset.Stream;
                if (!Preload(path, stream)) return;
                clip = clips[path];
            }

            cursor = new TutorialPlaybackCursor(clip);
            lastState = default;
            if (Active) return;
            Active = true;
            SwitchToPlaybackInput();
        }

        public IEnumerator Wait(float buffer = 0f)
        {
            int bufferFrames = (int)(buffer / engine.DeltaTime);
            while (cursor.ElapsedFrames < cursor.Clip.TotalFrames - bufferFrames) yield return null;
        }

        public void Stop()
        {
            if (!Active) return;
            Active = false;
            Paused = false;
            SwitchToPlayerInput();
        }

        public void Dispose()
        {
            Stop();
            for (int i = hooks.Count - 1; i >= 0; i--) hooks[i].Dispose();
            hooks.Clear();
            clips.Clear();
        }

        private void UpdateInput(Action<SourceMInputProcess> original, SourceMInputProcess input)
        {
            if (!ReferenceEquals(input, engine.MInput) || !Active)
            {
                original(input);
                return;
            }

            if (engine.Frame.Scenes.CurrentScene is not OriginalLevelSceneRoot level || cursor.Finished)
                Stop();
            else if (!Paused)
            {
                if (level.CanPause && PausePressed()) Pause(level);
                else
                {
                    ExecuteCommands(level);
                    if (!cursor.Finished)
                    {
                        FeedInput();
                        cursor.Advance();
                        return;
                    }

                    Stop();
                }
            }

            original(input);
        }

        private void EndPauseEffects(Action<OriginalLevelSceneRoot> original, OriginalLevelSceneRoot level)
        {
            original(level);
            if (!ReferenceEquals(level.EngineContext, engine) || !Active || !Paused) return;
            Paused = false;
            SwitchToPlaybackInput();
            if (cursor.Finished) return;
            FeedInput();
            cursor.Advance();
        }

        private void Rumble(Action<SourceGamePadData, float, float> original, SourceGamePadData gamePad,
            float strength, float time)
        {
            if (Active && Array.IndexOf(engine.MInput.GamePads, gamePad) >= 0) return;
            original(gamePad, strength, time);
        }
    }
}
