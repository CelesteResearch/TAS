using Celeste.Input.Devices.Keyboard;
using System.Collections.Generic;
using System.Linq;
using Celeste.Everest.Input.Bindings;
using Celeste.Everest.Input;
using Celeste.Everest.Modules.Settings;
using StudioCommunication;
using InputKeys = Celeste.Input.Devices.Keyboard.SourceKeys;
using Keys = Celeste.Input.Devices.Keyboard.SourceKeys;

namespace Celeste.Mods.Tas
{
    public sealed partial class SourceTasHotkeys
    {
        public SourceTasHotkey StartStop { get; private set; } = null!;
        public SourceTasHotkey Restart { get; private set; } = null!;
        public SourceTasHotkey FastForward { get; private set; } = null!;
        public SourceTasHotkey FastForwardComment { get; private set; } = null!;
        public SourceTasHotkey SlowForward { get; private set; } = null!;
        public SourceTasHotkey FrameAdvance { get; private set; } = null!;
        public SourceTasHotkey FrameStepBack { get; private set; } = null!;
        public SourceTasHotkey PauseResume { get; private set; } = null!;
        public SourceTasHotkey Hitboxes { get; private set; } = null!;
        public SourceTasHotkey TriggerHitboxes { get; private set; } = null!;
        public SourceTasHotkey SimplifiedGraphic { get; private set; } = null!;
        public SourceTasHotkey CenterCamera { get; private set; } = null!;
        public SourceTasHotkey LockCamera { get; private set; } = null!;
        public SourceTasHotkey SaveState { get; private set; } = null!;
        public SourceTasHotkey ClearState { get; private set; } = null!;
        public SourceTasHotkey InfoHud { get; private set; } = null!;
        public SourceTasHotkey FreeCamera { get; private set; } = null!;
        public SourceTasHotkey CameraUp { get; private set; } = null!;
        public SourceTasHotkey CameraDown { get; private set; } = null!;
        public SourceTasHotkey CameraLeft { get; private set; } = null!;
        public SourceTasHotkey CameraRight { get; private set; } = null!;
        public SourceTasHotkey CameraZoomIn { get; private set; } = null!;
        public SourceTasHotkey CameraZoomOut { get; private set; } = null!;
        public SourceTasHotkey OpenConsole { get; private set; } = null!;

        public float RightThumbSticksX => GamePadState.ThumbSticks.Right.X;

        public readonly Dictionary<HotkeyID, SourceTasHotkey> AllHotkeys = new();
        public Dictionary<HotkeyID, List<InputKeys>> StudioHotkeys = new();

        /// SourceTasHotkeys which shouldn't be triggered from Studio
        private readonly List<HotkeyID> StudioIgnoreHotkeys =
        [
            HotkeyID.InfoHud,
            HotkeyID.FreeCamera,
            HotkeyID.CameraUp, HotkeyID.CameraDown, HotkeyID.CameraLeft, HotkeyID.CameraRight,
            HotkeyID.CameraZoomIn, HotkeyID.CameraZoomOut,
            HotkeyID.OpenConsole
        ];


        internal bool Initialized { get; private set; } = false;

        internal void Initialize()
        {
            AllHotkeys.Clear();
            AllHotkeys[HotkeyID.Start] = StartStop = BindingToHotkey(driver.Settings.KeyStart);
            AllHotkeys[HotkeyID.Restart] = Restart = BindingToHotkey(driver.Settings.KeyRestart);
            AllHotkeys[HotkeyID.FastForward] = FastForward = BindingToHotkey(driver.Settings.KeyFastForward, true);
            AllHotkeys[HotkeyID.FastForwardComment] =
                FastForwardComment = BindingToHotkey(driver.Settings.KeyFastForwardComment);
            AllHotkeys[HotkeyID.FrameAdvance] = FrameAdvance = BindingToHotkey(driver.Settings.KeyFrameAdvance);
            AllHotkeys[HotkeyID.FrameStepBack] = FrameStepBack = BindingToHotkey(driver.Settings.KeyFrameStepBack);
            AllHotkeys[HotkeyID.SlowForward] = SlowForward = BindingToHotkey(driver.Settings.KeySlowForward, true);
            AllHotkeys[HotkeyID.Pause] = PauseResume = BindingToHotkey(driver.Settings.KeyPause);
            AllHotkeys[HotkeyID.Hitboxes] = Hitboxes = BindingToHotkey(driver.Settings.KeyHitboxes);
            AllHotkeys[HotkeyID.TriggerHitboxes] =
                TriggerHitboxes = BindingToHotkey(driver.Settings.KeyTriggerHitboxes);
            AllHotkeys[HotkeyID.Graphics] = SimplifiedGraphic = BindingToHotkey(driver.Settings.KeyGraphics);
            AllHotkeys[HotkeyID.Camera] = CenterCamera = BindingToHotkey(driver.Settings.KeyCamera);
            AllHotkeys[HotkeyID.LockCamera] = LockCamera = BindingToHotkey(driver.Settings.KeyLockCamera);
            AllHotkeys[HotkeyID.SaveState] = SaveState = BindingToHotkey(driver.Settings.KeySaveState);
            AllHotkeys[HotkeyID.ClearState] = ClearState = BindingToHotkey(driver.Settings.KeyClearState);
            AllHotkeys[HotkeyID.InfoHud] = InfoHud = BindingToHotkey(driver.Settings.KeyInfoHud);
            AllHotkeys[HotkeyID.FreeCamera] = FreeCamera = BindingToHotkey(driver.Settings.KeyFreeCamera);
            AllHotkeys[HotkeyID.CameraUp] = CameraUp = BindingToHotkey(new EverestButtonBinding(0, Keys.Up));
            AllHotkeys[HotkeyID.CameraDown] = CameraDown = BindingToHotkey(new EverestButtonBinding(0, Keys.Down));
            AllHotkeys[HotkeyID.CameraLeft] = CameraLeft = BindingToHotkey(new EverestButtonBinding(0, Keys.Left));
            AllHotkeys[HotkeyID.CameraRight] = CameraRight = BindingToHotkey(new EverestButtonBinding(0, Keys.Right));
            AllHotkeys[HotkeyID.CameraZoomIn] = CameraZoomIn = BindingToHotkey(new EverestButtonBinding(0, Keys.Home));
            AllHotkeys[HotkeyID.CameraZoomOut] = CameraZoomOut = BindingToHotkey(new EverestButtonBinding(0, Keys.End));

            var debugConsole = ((IEverestCurrentCoreSettingsEnvironment)process).CurrentCoreSettings.DebugConsole;
            var toggleDebugConsole =
                ((IEverestCurrentCoreSettingsEnvironment)process).CurrentCoreSettings.ToggleDebugConsole;
            AllHotkeys[HotkeyID.OpenConsole] = OpenConsole = new SourceTasHotkey(this,
                debugConsole.Keys.Union(toggleDebugConsole.Keys).ToList(),
                debugConsole.Buttons.Union(toggleDebugConsole.Buttons).ToList(),
                keyCombo: false, held: false);


            ((IEverestInputEventsEnvironment)process).InputEvents.OnInitialize += OnInputInitialized;

            StudioHotkeys = AllHotkeys.Where(entry => !StudioIgnoreHotkeys.Contains(entry.Key))
                .ToDictionary(entry => entry.Key, entry => entry.Value.Keys);
            Initialized = true;
            SendCurrentSourceBindings();
        }

        internal void DetachFromHost() =>
            ((IEverestInputEventsEnvironment)process).InputEvents.OnInitialize -= OnInputInitialized;

        private SourceTasHotkey BindingToHotkey(EverestButtonBinding binding, bool held = false)
            => new SourceTasHotkey(this, binding.Keys, binding.Buttons, true, held);

        internal void OnInputInitialized()
        {
            var debugConsole = ((IEverestCurrentCoreSettingsEnvironment)process).CurrentCoreSettings.DebugConsole;
            var toggleDebugConsole =
                ((IEverestCurrentCoreSettingsEnvironment)process).CurrentCoreSettings.ToggleDebugConsole;

            AllHotkeys[HotkeyID.OpenConsole].Keys.Clear();
            AllHotkeys[HotkeyID.OpenConsole].Keys.AddRange(debugConsole.Keys);
            AllHotkeys[HotkeyID.OpenConsole].Keys.AddRange(toggleDebugConsole.Keys);

            AllHotkeys[HotkeyID.OpenConsole].Buttons.Clear();
            AllHotkeys[HotkeyID.OpenConsole].Buttons.AddRange(debugConsole.Buttons);
            AllHotkeys[HotkeyID.OpenConsole].Buttons.AddRange(toggleDebugConsole.Buttons);
        }

        private void SendCurrentSourceBindings() => driver.SendSourceCurrentBindings();

        internal void ReleaseAllKeys()
        {
            foreach (SourceTasHotkey hotkey in AllHotkeys.Values) hotkey.OverrideCheck = false;
        }
    }
}
