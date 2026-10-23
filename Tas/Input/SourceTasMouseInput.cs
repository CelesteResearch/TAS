using Celeste.Input.Devices.GamePads;
using System;
using System.Numerics;
using Celeste.Core.Mathematics.LinearAlgebra;

namespace Celeste.Mods.Tas.Input
{
    internal sealed class SourceTasMouseInput
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;

        internal SourceTasMouseInput(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        public class Button
        {
            private DateTime doublePressTimeout;
            private DateTime repeatTimeout;

            public bool Check { get; private set; }
            public bool Pressed => !LastCheck && Check;
            public bool Released => LastCheck && !Check;

            public bool DoublePressed { get; private set; }
            public bool Repeated { get; private set; }

            public bool LastCheck { get; set; }

            public void Update(SourceButtonState state)
            {
                LastCheck = Check;
                Check = state == SourceButtonState.Pressed;

                var now = DateTime.Now;
                if (Pressed)
                {
                    DoublePressed = now < doublePressTimeout;
                    doublePressTimeout = DoublePressed
                        ? default
                        : now + TimeSpan.FromMilliseconds(SourceTasHotkey.DoublePressTimeoutMS);

                    Repeated = true;
                    repeatTimeout = now + TimeSpan.FromMilliseconds(SourceTasHotkey.RepeatTimeoutMS);
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
        }

        public bool Updating { get; private set; }

        public Vector2 Position { get; private set; }
        public Vector2 PositionDelta => SourceVector2Math.Subtract(Position, lastPosition);
        private Vector2 lastPosition;

        public int WheelDelta { get; private set; }
        private int lastWheel;

        public readonly Button Left = new();
        public readonly Button Middle = new();
        public readonly Button Right = new();


        public void UpdateMeta()
        {
            // Avoid checking mouse inputs while not having focus, or while fast forwarding for performance
            if (!process.MInput.Engine.IsActive || driver.FastForwarding)
            {
                lastPosition = Position;
                Left.Update(SourceButtonState.Released);
                Middle.Update(SourceButtonState.Released);
                Right.Update(SourceButtonState.Released);
                WheelDelta = 0;

                return;
            }

            Updating = true;
            var mouseState = process.MInput.PhysicalMouse.GetState();
            Updating = false;

            lastPosition = Position;
            Position = new Vector2(mouseState.X, mouseState.Y);

            WheelDelta = mouseState.ScrollWheelValue - lastWheel;
            lastWheel = mouseState.ScrollWheelValue;

            Left.Update(mouseState.LeftButton);
            Middle.Update(mouseState.MiddleButton);
            Right.Update(mouseState.RightButton);
        }
    }
}
