using System;
using System.Collections.Generic;
using System.Linq;
using Celeste.Core.Randomness;
using Celeste.Mods.Tas.Input;
using Celeste.Mods.Tas.Commands;
using StudioCommunication;

namespace Celeste.Mods.Tas.Gameplay.DesyncFix
{
    internal sealed partial class SourceTasSeededRandomness
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private readonly List<Handler> handlers = new();
        internal IEnumerable<Handler> SourceHandlers => handlers;
        internal readonly SharedUpdateState SharedUpdateHandler;
        internal readonly FrameCounterState FrameCounterHandler;

        internal SourceTasSeededRandomness(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
            SharedUpdateHandler = new SharedUpdateState(this);
            FrameCounterHandler = new FrameCounterState(this);
            handlers.Add(SharedUpdateHandler);
            handlers.Add(FrameCounterHandler);
            ConstructRemainingHandlers();
        }

        private SourceRandomProcessState RandomProcess => ((ISourceRandomProcessEnvironment)process).RandomProcess;
        private ISourceRandom NewRandom(int seed) => RandomProcess.Factory.CreateSeeded(seed);

        internal abstract class Handler
        {
            protected readonly SourceTasSeededRandomness owner;

            protected Handler(SourceTasSeededRandomness owner)
            {
                this.owner = owner;
            }

            public abstract string Name { get; }
            public int[] Seeds = [];
            public int SeedIndex;
            public SourceTasSourceLocation SeedSource;
            public bool HasSeed => SeedIndex < Seeds.Length;

            public virtual void Init()
            {
            }

            public virtual void Reset()
            {
            }

            public virtual void PreUpdate()
            {
            }

            public virtual void PostUpdate()
            {
            }

            public virtual void SaveState(Dictionary<string, object> data)
            {
            }

            public virtual void LoadState(Dictionary<string, object> data)
            {
            }

            protected bool NextSeed(out int seed)
            {
                if (SeedIndex < Seeds.Length)
                {
                    seed = Seeds[SeedIndex++];
                    return true;
                }

                seed = 0;
                return false;
            }

            protected void AssertNoSeedsRemaining()
            {
                if (SeedIndex < Seeds.Length)
                    owner.driver.Controller.ReportError(SeedSource,
                        $"Target '{Name}' was provided more seeds than expected: Expected {SeedIndex}, got {Seeds.Length}");
            }
        }

        internal void EnableRun()
        {
            foreach (var handler in handlers) handler.Reset();
        }

        internal void OnLoadLevel(bool isFromLoader)
        {
            if (driver.Running && isFromLoader)
                foreach (var handler in handlers)
                    handler.Reset();
        }

        internal void PreEngineUpdate()
        {
            foreach (var handler in handlers) handler.PreUpdate();
        }

        internal void PostEngineUpdate()
        {
            foreach (var handler in handlers)
            {
                handler.PostUpdate();
                handler.Seeds = [];
                handler.SeedIndex = 0;
                handler.SeedSource = default;
            }
        }

        internal T RequireHandler<T>() where T : Handler => handlers.OfType<T>().Single();

        internal void SeedRandom(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            var controller = driver.Controller;
            var src = new SourceTasSourceLocation(filePath, fileLine, studioLine);
            if (commandLine.Arguments.Length < 1)
            {
                controller.ReportError(src, "Missing target for SeedRandom");
                return;
            }

            if (commandLine.Arguments.Length < 2)
            {
                controller.ReportError(src, "Missing seed(s) for SeedRandom");
                return;
            }

            string[] args = commandLine.Arguments;
            string target = args[0];
            int[] seeds = new int[args.Length - 1];
            for (int i = 0; i < seeds.Length; i++)
            {
                if (int.TryParse(args[i + 1], out int seed)) seeds[i] = seed;
                else
                {
                    controller.ReportError(src, $"Failed to parse '{args[i + 1]}' as a random seed");
                    return;
                }
            }

            if (handlers.FirstOrDefault(h => h.Name == target) is not { } handler)
            {
                controller.ReportError(src, $"Unknown target '{target}' for SeedRandom");
                return;
            }

            if (!driver.CommandRegistry.Parsing)
            {
                handler.Seeds = seeds;
                handler.SeedIndex = 0;
                handler.SeedSource = src;
            }
        }

        internal sealed class SharedUpdateState : Handler
        {
            public override string Name => "Update";
            public ISourceRandom SharedUpdateRandom;
            private bool pushedRandom;

            internal SharedUpdateState(SourceTasSeededRandomness owner) : base(owner)
            {
                SharedUpdateRandom = owner.RandomProcess.Factory.CreateUnseeded();
            }

            public override void PreUpdate()
            {
                if (NextSeed(out int seed))
                {
                    AssertNoSeedsRemaining();
                    SharedUpdateRandom = owner.NewRandom(seed);
                }

                if (owner.driver.Running && !pushedRandom)
                {
                    owner.RandomProcess.PushRandom(SharedUpdateRandom);
                    pushedRandom = true;
                }
            }

            public override void PostUpdate()
            {
                if (pushedRandom)
                {
                    owner.RandomProcess.PopRandom();
                    pushedRandom = false;
                }
            }

            public override void SaveState(Dictionary<string, object> data)
            {
                data["SharedUpdateHandler_SharedUpdateRandom"] = owner.DeepClone(SharedUpdateRandom);
                data["SharedUpdateHandler_pushedRandom"] = pushedRandom;
            }

            public override void LoadState(Dictionary<string, object> data)
            {
                SharedUpdateRandom = owner.DeepClone((ISourceRandom)data["SharedUpdateHandler_SharedUpdateRandom"]);
                pushedRandom = (bool)data["SharedUpdateHandler_pushedRandom"];
            }
        }

        internal sealed class FrameCounterState : Handler
        {
            public override string Name => "FrameCounter";
            public ulong FrameCounter;
            private ulong? origFrameCounter;

            internal FrameCounterState(SourceTasSeededRandomness owner) : base(owner)
            {
            }

            public override void Reset() => FrameCounter = 0;

            public override void PreUpdate()
            {
                if (NextSeed(out int seed))
                {
                    AssertNoSeedsRemaining();
                    FrameCounter = unchecked((uint)seed);
                }

                if (owner.driver.Running && origFrameCounter == null)
                {
                    origFrameCounter = owner.process.Frame.FrameCounter;
                    owner.process.Frame.FrameCounter = FrameCounter++;
                }
            }

            public override void PostUpdate()
            {
                if (origFrameCounter != null)
                {
                    owner.process.Frame.FrameCounter = origFrameCounter.Value;
                    origFrameCounter = null;
                }
            }

            public override void SaveState(Dictionary<string, object> data)
            {
                data["FrameCounterHandler_FrameCounter"] = FrameCounter;
                data["FrameCounterHandler_origFrameCounter"] = origFrameCounter;
            }

            public override void LoadState(Dictionary<string, object> data)
            {
                FrameCounter = (ulong)data["FrameCounterHandler_FrameCounter"];
                origFrameCounter = (ulong?)data["FrameCounterHandler_origFrameCounter"];
            }
        }
    }
}
