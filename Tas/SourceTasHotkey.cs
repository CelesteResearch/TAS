using Celeste.Input.Devices.Keyboard;
using Celeste.Input.Devices.GamePads;
using Celeste.Mods.Tas.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using InputKeys = Celeste.Input.Devices.Keyboard.SourceKeys;
using InputButtons = Celeste.Input.Devices.GamePads.SourceGamePadButtonFlags;

namespace Celeste.Mods.Tas
{
    public sealed class SourceTasHotkey
    {
        private readonly SourceTasHotkeys owner;
        private readonly bool keyCombo;
        private readonly bool held;

        public SourceTasHotkey(SourceTasHotkeys owner, List<InputKeys> keys,
            List<InputButtons> buttons, bool keyCombo, bool held)
        {
            this.owner = owner;
            Keys = keys;
            Buttons = buttons;
            this.keyCombo = keyCombo;
            this.held = held;
        }

        public readonly List<InputKeys> Keys;
        public readonly List<InputButtons> Buttons;

        internal bool OverrideCheck;

        private DateTime doublePressTimeout;
        private DateTime repeatTimeout;

        public bool Check { get; private set; }
        public bool Pressed => !LastCheck && Check;
        public bool Released => LastCheck && !Check;

        public bool DoublePressed { get; private set; }
        public bool Repeated { get; private set; }

        public bool LastCheck { get; set; }

        internal const double DoublePressTimeoutMS = 200.0;
        internal const double RepeatTimeoutMS = 500.0;

        internal void Update(bool updateKey = true, bool updateButton = true)
        {
            LastCheck = Check;

            bool keyCheck;
            bool buttonCheck;

            if (OverrideCheck)
            {
                keyCheck = buttonCheck = true;
                if (!held)
                {
                    OverrideCheck = false;
                }
            }
            else
            {
                keyCheck = updateKey && IsKeyDown();
                buttonCheck = updateButton && IsButtonDown();
            }

            Check = keyCheck || buttonCheck;

            var now = DateTime.Now;
            if (Pressed)
            {
                DoublePressed = now < doublePressTimeout;
                doublePressTimeout = DoublePressed ? default : now + TimeSpan.FromMilliseconds(DoublePressTimeoutMS);

                Repeated = true;
                repeatTimeout = now + TimeSpan.FromMilliseconds(RepeatTimeoutMS);
            }
            else if (Check)
            {
                DoublePressed = false;
                Repeated = now >= repeatTimeout;
            }
            else
            {
                DoublePressed = false;
                Repeated = false;
                repeatTimeout = default;
            }
        }

        private bool IsKeyDown()
        {
            if (Keys.Count == 0 || owner.KeyboardState == default)
            {
                return false;
            }

            return keyCombo ? Keys.All(owner.KeyboardState.IsKeyDown) : Keys.Any(owner.KeyboardState.IsKeyDown);
        }

        private bool IsButtonDown()
        {
            if (Buttons.Count == 0 || owner.GamePadState == default)
            {
                return false;
            }

            return keyCombo
                ? Buttons.All(owner.GamePadState.IsButtonDown)
                : Buttons.Any(owner.GamePadState.IsButtonDown);
        }

        public override string ToString()
        {
            List<string> result = new();
            if (Keys.IsNotEmpty())
            {
                result.Add(string.Join("+", Keys.Select(key => key switch
                {
                    InputKeys.D0 => "0",
                    InputKeys.D1 => "1",
                    InputKeys.D2 => "2",
                    InputKeys.D3 => "3",
                    InputKeys.D4 => "4",
                    InputKeys.D5 => "5",
                    InputKeys.D6 => "6",
                    InputKeys.D7 => "7",
                    InputKeys.D8 => "8",
                    InputKeys.D9 => "9",
                    _ => key.ToString(),
                })));
            }

            if (Buttons.IsNotEmpty())
            {
                result.Add(string.Join("+", Buttons));
            }

            return string.Join("/", result);
        }
    }
}
