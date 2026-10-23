using System;
using System.Collections.Concurrent;
using Celeste.Core.Diagnostics.Tas;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver : ISourceTasManager
    {
        public SourceTasManagerState CurrState, NextState;
        internal long DisableRunRequestRevision { get; private set; }
        public bool Running => CurrState != SourceTasManagerState.Disabled;
        private bool ManagerRunning => Running;
        public bool FastForwarding => Running && PlaybackSpeed >= 5.0f;
        public float PlaybackSpeed { get; internal set; } = 1.0f;

        SourceTasManagerState ISourceTasManager.CurrState
        {
            get => CurrState;
            set => CurrState = value;
        }

        SourceTasManagerState ISourceTasManager.NextState
        {
            get => NextState;
            set => NextState = value;
        }

        private readonly ConcurrentQueue<Action> mainThreadActions = new();
        internal int FrameStepBackTargetFrame = -1;
        private const float FrameStepBackTime = 1.0f;
        private float frameStepBackAmount = 0.0f;
        private float frameStepBackTimeout = 0.0f;

        public void QueueMainThreadAction(Action action) => mainThreadActions.Enqueue(action);
        public void AddMainThreadAction(Action action) => mainThreadActions.Enqueue(action);
    }
}
