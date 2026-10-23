using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Celeste.Game.Execution.Levels;
using Celeste.Mods.Tas.Input.Feeding;
using Celeste.Game.Settings;
using Celeste.Input.Bindings;
using Celeste.Input.Devices.GamePads;
using Celeste.Input.Devices.Keyboard;
using Celeste.Input.Runtime;
using Buttons = Celeste.Input.Devices.GamePads.SourceGamePadButtonFlags;

namespace Celeste.Mods.Tas.Playback
{
    public sealed partial class TutorialPlaybackService
    {
        private static readonly Dictionary<string, Buttons[]> playbackButtons = new()
        {
            { "Left", new[] { Buttons.LeftThumbstickLeft, Buttons.DPadLeft } },
            { "Right", new[] { Buttons.LeftThumbstickRight, Buttons.DPadRight } },
            { "Down", new[] { Buttons.LeftThumbstickDown, Buttons.DPadDown } },
            { "Up", new[] { Buttons.LeftThumbstickUp, Buttons.DPadUp } },
            { "Grab", new[] { Buttons.LeftStick } },
            { "Jump", new[] { Buttons.A, Buttons.Y } },
            { "Dash", new[] { Buttons.B, Buttons.X } },
            { "DemoDash", new[] { Buttons.RightShoulder, Buttons.RightStick } },
            { "MenuLeft", Array.Empty<Buttons>() },
            { "MenuRight", Array.Empty<Buttons>() },
            { "MenuDown", Array.Empty<Buttons>() },
            { "MenuUp", Array.Empty<Buttons>() },
            { "Pause", Array.Empty<Buttons>() },
            { "Confirm", Array.Empty<Buttons>() },
            { "Cancel", Array.Empty<Buttons>() },
            { "Journal", Array.Empty<Buttons>() },
            { "QuickRestart", Array.Empty<Buttons>() },
            { "LeftDashOnly", Array.Empty<Buttons>() },
            { "RightDashOnly", Array.Empty<Buttons>() },
            { "UpDashOnly", Array.Empty<Buttons>() },
            { "DownDashOnly", Array.Empty<Buttons>() },
            { "LeftMoveOnly", Array.Empty<Buttons>() },
            { "RightMoveOnly", Array.Empty<Buttons>() },
            { "DownMoveOnly", Array.Empty<Buttons>() },
            { "UpMoveOnly", Array.Empty<Buttons>() }
        };

        private readonly TutorialPlaybackInputCommands inputCommands = new();
        private bool currentPauseCheck;

        private void SwitchToPlaybackInput()
        {
            SettingsState settings = engine.Settings;
            var originalBindings = new Dictionary<FieldInfo, SourceInputBinding>();
            foreach (var pair in playbackButtons)
            {
                FieldInfo field = typeof(SettingsState).GetField(pair.Key);
                originalBindings.Add(field, (SourceInputBinding)field.GetValue(settings));
                SourceInputBinding binding = engine.InputBindings.Create();
                binding.Add(pair.Value);
                field.SetValue(settings, binding);
            }

            try
            {
                engine.InputSource.Initialize();
            }
            finally
            {
                foreach (var pair in originalBindings) pair.Key.SetValue(settings, pair.Value);
            }

            var input = engine.MInput;
            input.Disabled = false;
            input.ControllerHasFocus = true;
            input.GamePads[engine.InputSource.Gamepad].Attached = true;
            input.Keyboard.PreviousState = input.Keyboard.CurrentState = default;
            input.Mouse.PreviousState = input.Mouse.CurrentState = default;
        }

        private void SwitchToPlayerInput()
        {
            engine.InputSource.Initialize();
            var input = engine.MInput;
            input.Keyboard.PreviousState = input.Keyboard.CurrentState = engine.KeyboardState.GetState();
            input.Mouse.PreviousState = input.Mouse.CurrentState = input.PhysicalMouse.GetState();
            foreach (var gamePad in input.GamePads)
                gamePad.PreviousState = gamePad.CurrentState = engine.FnaGamePads.GetState(gamePad.PlayerIndex);
        }

        private bool PausePressed()
        {
            var keyboard = engine.KeyboardState.GetState();
            var physicalGamePad = engine.MInput.GamePads.FirstOrDefault(data => data.Attached);
            SourceGamePadState pad = physicalGamePad == null
                ? default
                : engine.FnaGamePads.GetState(physicalGamePad.PlayerIndex);
            bool previous = currentPauseCheck;
            var binding = engine.Settings.Pause;
            currentPauseCheck = keyboard.IsKeyDown(SourceKeys.Escape);
            foreach (var key in binding.Keyboard) currentPauseCheck |= keyboard.IsKeyDown(key);
            foreach (var button in binding.Controller) currentPauseCheck |= pad.IsButtonDown(button);
            return currentPauseCheck && !previous;
        }

        private void Pause(OriginalLevelSceneRoot level)
        {
            Paused = true;
            SwitchToPlayerInput();
            engine.InputSource.Pause.ConsumePress();
            engine.InputSource.ESC.ConsumePress();
            level.Pause(minimal: true);
        }

        private void FeedInput()
        {
            var gamePad = engine.MInput.GamePads[engine.InputSource.Gamepad];
            gamePad.CurrentState = lastState;
            TasActualSourceInputFeeding.FeedInputs(cursor.Current, cursor.Current.Next,
                engine.MInput, engine.InputSource, inputCommands);
            lastState = gamePad.CurrentState;
        }
    }
}
