using Celeste.Input.Runtime;
using Celeste.Input.Devices.Mouse;
using Celeste.Input.Devices.GamePads;
using Celeste.Core.Geometry;
using StudioCommunication;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Input;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed class SourceTasMouseProcess
    {
        internal static System.Type SourceMetaProviderType => typeof(Meta);

        private readonly TasPlaybackDriver driver;
        internal SourceTasMouseProcess(TasPlaybackDriver driver) => this.driver = driver;

        private class Meta : ISourceTasCommandMeta
        {
            public string Insert => $"Mouse{CommandInfo.Separator}[0;X]{CommandInfo.Separator}[1;Y]";
            public bool HasArguments => true;
        }

        public SourceMouseState CurrentState;

        [SourceTasLoad]
        private static void SourceLoad() => CelesteTasModule.Instance.Driver.SourceMouseCommand.Load();

        internal void Load() => driver.InstallSourceMouseStateOverride(this);
        internal bool IsRunningTas() => driver.Running && !driver.MouseInput.Updating;

        // "Mouse, X, Y, [ScrollWheel]"
        // "Mouse, [L], [R], [M], [X1], [X2]"
        // "Mouse, X, Y, [ScrollWheel], [L], [R], [M], [X1], [X2]"
        // "Mouse, [L], [R], [M], [X1], [X2], X, Y, [ScrollWheel]"
        [SourceTasCommand("Mouse", MetaDataProvider = typeof(Meta))]
        internal void MouseControl(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            string[] args = commandLine.Arguments;
            if (args.IsEmpty())
            {
                return;
            }

            int? x = null;
            int? y = null;
            int? scrollWheel = null;

            foreach (string arg in args)
            {
                if (int.TryParse(arg, out int n))
                {
                    if (x == null)
                    {
                        x = n;
                        if (x is > 319 or < 0)
                        {
                            driver.AbortSourceTas($"{x} is not a valid X position");
                            return;
                        }
                    }
                    else if (y == null)
                    {
                        y = n;
                        if (y is > 179 or < 0)
                        {
                            driver.AbortSourceTas($"{y} is not a valid Y position");
                            return;
                        }
                    }
                    else if (scrollWheel == null)
                    {
                        scrollWheel = n;
                    }
                }
                else
                {
                    string button = arg.ToUpperInvariant();
                    switch (button)
                    {
                        case "L":
                            Set(ref CurrentState, leftButton: SourceButtonState.Pressed);
                            break;
                        case "M":
                            Set(ref CurrentState, middleButton: SourceButtonState.Pressed);
                            break;
                        case "R":
                            Set(ref CurrentState, rightButton: SourceButtonState.Pressed);
                            break;
                        case "X1":
                            Set(ref CurrentState, xButton1: SourceButtonState.Pressed);
                            break;
                        case "X2":
                            Set(ref CurrentState, xButton2: SourceButtonState.Pressed);
                            break;
                    }
                }
            }

            IntRect win = driver.Process.ActualGame.Window.ClientBounds;
            if (win.Width % 320 != 0 || win.Height % 180 != 0)
            {
                driver.AbortSourceTas("Window size isn't an integer multiple of 320x180");
                return;
            }

            if (x != null && y == null)
            {
                driver.AbortSourceTas("No Y mouse coordinate was provided");
            }
            else if (y != null)
            {
                int scale = win.Width / 320;
                Set(ref CurrentState, x * scale, y * scale);
            }

            if (scrollWheel != null)
            {
                Set(ref CurrentState, scrollWheel: scrollWheel);
            }
        }

        public void SetMouseState(SourceMInputProcess input)
        {
            input.Mouse.PreviousState = input.Mouse.CurrentState;
            input.Mouse.CurrentState = CurrentState;
            if (driver.Controller.Current != driver.Controller.Next)
            {
                ReleaseButtons();
            }
        }

        private void ReleaseButtons()
        {
            CurrentState = new SourceMouseState(
                CurrentState.X,
                CurrentState.Y,
                0,
                SourceButtonState.Released,
                SourceButtonState.Released,
                SourceButtonState.Released,
                SourceButtonState.Released,
                SourceButtonState.Released
            );
        }

        [SourceTasDisableRun]
        private static void SourceClearState() => CelesteTasModule.Instance.Driver.SourceMouseCommand.ClearState();

        internal void ClearState()
        {
            CurrentState = default;
        }

        private static void Set(ref SourceMouseState mouseState,
            int? x = null,
            int? y = null,
            int? scrollWheel = null,
            SourceButtonState? leftButton = null,
            SourceButtonState? middleButton = null,
            SourceButtonState? rightButton = null,
            SourceButtonState? xButton1 = null,
            SourceButtonState? xButton2 = null
        )
        {
            mouseState = new SourceMouseState(
                x ?? mouseState.X,
                y ?? mouseState.Y,
                scrollWheel ?? mouseState.ScrollWheelValue,
                leftButton ?? mouseState.LeftButton,
                middleButton ?? mouseState.MiddleButton,
                rightButton ?? mouseState.RightButton,
                xButton1 ?? mouseState.XButton1,
                xButton2 ?? mouseState.XButton2
            );
        }
    }
}
