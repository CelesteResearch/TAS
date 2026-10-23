using Celeste.Input.Devices.Keyboard;
using Celeste.Input.Devices.GamePads;
using Celeste.Everest.Input.Bindings;
using Celeste.Everest.Settings.Metadata;
using Keys = Celeste.Input.Devices.Keyboard.SourceKeys;

namespace Celeste.Mods.Tas.Module
{
    public partial class CelesteTasSettings
    {
        [SettingName("TAS_KEY_START_STOP")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.RightControl })]
        public EverestButtonBinding KeyStart { get; set; } = null!;

        [SettingName("TAS_KEY_RESTART")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.OemPlus })]
        public EverestButtonBinding KeyRestart { get; set; } = null!;

        [SettingName("TAS_KEY_FAST_FORWARD")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.RightShift })]
        public EverestButtonBinding KeyFastForward { get; set; } = null!;

        [SettingName("TAS_KEY_FAST_FORWARD_COMMENT")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.RightAlt, Keys.RightShift })]
        public EverestButtonBinding KeyFastForwardComment { get; set; } = null!;

        [SettingName("TAS_KEY_SLOW_FORWARD")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.OemPipe })]
        public EverestButtonBinding KeySlowForward { get; set; } = null!;

        [SettingName("TAS_KEY_FRAME_ADVANCE")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.OemOpenBrackets })]
        public EverestButtonBinding KeyFrameAdvance { get; set; } = null!;

        [SettingName("TAS_KEY_FRAME_STEP_BACK")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 },
            new Keys[] { Keys.LeftControl, Keys.OemOpenBrackets })]
        public EverestButtonBinding KeyFrameStepBack { get; set; } = null!;

        [SettingName("TAS_KEY_PAUSE_RESUME")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.OemCloseBrackets })]
        public EverestButtonBinding KeyPause { get; set; } = null!;

        [SettingName("TAS_KEY_HITBOXES")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.LeftControl, Keys.B })]
        public EverestButtonBinding KeyHitboxes { get; set; } = null!;

        [SettingName("TAS_KEY_TRIGGER_HITBOXES")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.LeftAlt, Keys.T })]
        public EverestButtonBinding KeyTriggerHitboxes { get; set; } = null!;

        [SettingName("TAS_KEY_SIMPLIFIED_GRAPHICS")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.LeftControl, Keys.N })]
        public EverestButtonBinding KeyGraphics { get; set; } = null!;

        [SettingName("TAS_KEY_CENTER_CAMERA")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.LeftControl, Keys.M })]
        public EverestButtonBinding KeyCamera { get; set; } = null!;

        [SettingName("TAS_KEY_LOCK_CAMERA")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.LeftControl, Keys.H })]
        public EverestButtonBinding KeyLockCamera { get; set; } = null!;

        [SettingName("TAS_KEY_SAVE_STATE")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.RightAlt, Keys.OemMinus })]
        public EverestButtonBinding KeySaveState { get; set; } = null!;

        [SettingName("TAS_KEY_CLEAR_STATE")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.RightAlt, Keys.Back })]
        public EverestButtonBinding KeyClearState { get; set; } = null!;

        [SettingName("TAS_KEY_INFO_HUD")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.LeftControl })]
        public EverestButtonBinding KeyInfoHud { get; set; } = null!;

        [SettingName("TAS_KEY_FREE_CAMERA")]
        [DefaultButtonBinding(new SourceGamePadButtonFlags[] { 0 }, new Keys[] { Keys.LeftAlt })]
        public EverestButtonBinding KeyFreeCamera { get; set; } = null!;
    }
}
