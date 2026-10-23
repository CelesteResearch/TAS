using System;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Input;
using Celeste.Mods.Tas.Input.Feeding;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private readonly GameApplication process;
        private readonly CelesteTasModule module;
        private readonly SourceTasInputController controller;
        private readonly TasGameHost gameHost;
        private readonly SourceTasSyncChecker SyncChecker;
        private readonly SourceTasRunEvents RunEvents;
        private readonly SourceTasHotkeys hotkeys;

        public TasPlaybackDriver(GameApplication process, CelesteTasModule module)
        {
            this.process = process ?? throw new ArgumentNullException(nameof(process));
            this.module = module ?? throw new ArgumentNullException(nameof(module));
            controller = new SourceTasInputController(this);
            gameHost = new TasGameHost(process);
            SyncChecker = new SourceTasSyncChecker(this, process);
            RunEvents = new SourceTasRunEvents(this, process);
            hotkeys = new SourceTasHotkeys(process, this);
            InitializeSourceEnforceLegal();
            InitializeSourcePopupToast();
            InitializeSourceCenterCamera();
            InitializeSourceInputParsingProcesses();
            InitializeSourceHooks();
            InitializeSourceSeededRandomness();
            InitializeSourceHud();
            InitializeSourceOptionalCampaignCommands();
            InitializeSourceRequireDependencyCommand();
            InitializeSourceOptimizations();
            InitializeSourceCycleHitboxColor();
            InitializeSourceHitboxToggle();
            InitializeSourceActorHitboxes();
            InitializeSourceHitboxSimplified();
            process.SourceFrameCompleted += OnSourceFrameCompleted;
        }

        internal CelesteTasSettings Settings => (CelesteTasSettings)module._Settings;

        internal void DetachFromHost()
        {
            process.SourceFrameCompleted -= OnSourceFrameCompleted;
            hotkeys.DetachFromHost();
        }

        internal bool Enabled => Settings.Enabled;
        public SourceTasInputController Controller => controller;
        public TasGameHost GameHost => gameHost;

        internal void FeedSourceInput(SourceTasInputFrame input)
        {
            TasActualSourceInputFeeding.FeedInputs(input, controller.Next,
                process.MInput, process.InputSource, this);
            // The source controller increments its cursor only after FeedInput returns.
            pendingSampleFeedIndex = frameSnapshotSampler.Enabled ? controller.CurrentFrameInTas : -1;
            sampledTasUpdateCounter = false;
        }
    }
}
