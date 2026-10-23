using Celeste.Input.Runtime;
using Celeste.Input.Devices.Keyboard;
using Celeste.Input.Devices.GamePads;
using System;
using System.Numerics;
using System.Linq;
using Celeste.Input.Gameplay;
using Actions = StudioCommunication.Actions;
using Buttons = Celeste.Input.Devices.GamePads.SourceGamePadButtonFlags;

namespace Celeste.Mods.Tas.Input.Feeding
{
    public static class TasActualSourceInputFeeding
    {
        public static void FeedInputs(ISourceTasFeedInputFrame input, ISourceTasFeedInputFrame next,
            SourceMInputProcess mInput, ISourceGameInput gameInput, ISourceTasInputCommandState commands)
        {
            var actions = (Actions)input.SourceActions;
            SourceGamePadDPad pad = default;
            SourceGamePadThumbSticks sticks = default;
            if (Has(actions, Actions.Feather))
            {
                pad = new SourceGamePadDPad(SourceButtonState.Released, SourceButtonState.Released,
                    SourceButtonState.Released, SourceButtonState.Released);
                sticks = new SourceGamePadThumbSticks(new Vector2(input.FeatherStick.X, input.FeatherStick.Y),
                    input.DashOnlyStick);
            }
            else
            {
                pad = new SourceGamePadDPad(Button(actions, Actions.Up), Button(actions, Actions.Down),
                    Button(actions, Actions.Left), Button(actions, Actions.Right));
                sticks = new SourceGamePadThumbSticks(new Vector2(0, 0), input.DashOnlyStick);
            }

            var state = new SourceGamePadState(sticks,
                new SourceGamePadTriggers(Has(actions, Actions.Journal) ? 1f : 0f, 0), new SourceGamePadButtons(
                    Flag(actions, Actions.Jump, Buttons.A) | Flag(actions, Actions.Jump2, Buttons.Y) |
                    Flag(actions, Actions.DemoDash, Buttons.RightShoulder) |
                    Flag(actions, Actions.DemoDash2, Buttons.RightStick) |
                    Flag(actions, Actions.Dash, Buttons.B) | Flag(actions, Actions.Dash2, Buttons.X) |
                    Flag(actions, Actions.Grab, Buttons.LeftStick) | Flag(actions, Actions.Grab2, Buttons.Back) |
                    Flag(actions, Actions.Start, Buttons.Start) | Flag(actions, Actions.Restart, Buttons.LeftShoulder) |
                    Flag(actions, Actions.Up, Buttons.DPadUp) | Flag(actions, Actions.Down, Buttons.DPadDown) |
                    Flag(actions, Actions.Left, Buttons.DPadLeft) | Flag(actions, Actions.Right, Buttons.DPadRight) |
                    Flag(actions, Actions.Journal, Buttons.LeftTrigger)), pad);
            var gamePadData = mInput.GamePads[gameInput.Gamepad];
            gamePadData.PreviousState = gamePadData.CurrentState;
            gamePadData.CurrentState = state;
            commands.SetSourceMouseState(mInput, input, next);
            mInput.Keyboard.PreviousState = mInput.Keyboard.CurrentState;
            var keys = commands.GetSourcePressKeys(input, next);
            if (Has(actions, Actions.Confirm)) keys.Add(SourceKeys.NumPad0);
            if (Has(actions, Actions.LeftMoveOnly)) keys.Add(SourceKeys.Left);
            if (Has(actions, Actions.RightMoveOnly)) keys.Add(SourceKeys.Right);
            if (Has(actions, Actions.UpMoveOnly)) keys.Add(SourceKeys.Up);
            if (Has(actions, Actions.DownMoveOnly)) keys.Add(SourceKeys.Down);
            foreach (SourceKeys key in input.PressedKeys) keys.Add(key);
            mInput.Keyboard.CurrentState = new SourceKeyboardState(keys.ToArray());
            mInput.UpdateVirtualInputs();
        }

        private static bool Has(Actions value, Actions flag) => (value & flag) != 0;

        private static SourceButtonState Button(Actions value, Actions flag) =>
            Has(value, flag) ? SourceButtonState.Pressed : SourceButtonState.Released;

        private static Buttons Flag(Actions value, Actions flag, Buttons result) => Has(value, flag) ? result : 0;
    }
}
