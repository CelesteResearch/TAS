using System.Diagnostics.CodeAnalysis;
using System;
using System.Numerics;
using StudioCommunication;

namespace Celeste.Mods.Tas.Input
{
    public partial record SourceTasInputFrame
    {
        // Input-only parsing uses the default analog profile, independently of a live TAS run.
        // ComputeAngleVector does not use the driver's command/UI services.
        private static readonly SourceTasAnalogProcess gameplayInputAnalog = new(null!);

        public static bool TryParseGameplayInput(string text, string path, int line,
            SourceTasInputFrame? previous, [NotNullWhen(true)] out SourceTasInputFrame? frame)
        {
            frame = null;
            if (!ActionLine.TryParse(text, out var actions)) return false;
            Vector2? originalStick = null;
            int feather = text.IndexOf(",F,", StringComparison.OrdinalIgnoreCase);
            if (feather >= 0)
            {
                // Original tutorial TAS uses Calc.ToRad(float), without normal TAS analog rounding/clamping.
                float radians = float.Parse(text.Substring(feather + 3)) * .0174532924f;
                originalStick = new Vector2((float)Math.Sin(radians), (float)Math.Cos(radians));
            }

            frame = new SourceTasInputFrame(gameplayInputAnalog, actions, path, line, line, 0, 0, 0, null,
                originalStick);
            frame.Previous = previous;
            if (previous != null) previous.Next = frame;
            return true;
        }
    }
}
