using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Numerics;
using Celeste.Core.Mathematics;
using StudioCommunication;
using System.Collections.Generic;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Module;


namespace Celeste.Mods.Tas.Input
{
    /// Game controllers only support a precision of a short for the X / Y axis
    /// Depending on the SourceTasAnalogMode, angle + magnitude are mapped to a valid short position
    public sealed class SourceTasAnalogProcess
    {
        internal static System.Type SourceMetaProviderType => typeof(Meta);

        private readonly TasPlaybackDriver driver;
        internal SourceTasAnalogProcess(TasPlaybackDriver driver) => this.driver = driver;
        private const double DeadZone = 0.239532471;
        private const double DcMult = (1 - DeadZone) * (1 - DeadZone);
        private const float AmpLowerbound = (float)(0.25 * DcMult);
        private const short Lowerbound = 7849;

        private static readonly Regex Fractional = new(@"\d+\.(\d*)", RegexOptions.Compiled);
        internal SourceTasAnalogMode SourceTasAnalogMode = SourceTasAnalogMode.Ignore;

        public (Vector2, SourceTasVector2Short) ComputeAngleVector(float angle, float magnitude)
        {
            float precision;
            if (angle == 0.0f)
            {
                precision = 1E-6f;
            }
            else
            {
                int digits = 0;

                var match = Fractional.Match(angle.ToString(CultureInfo.InvariantCulture));
                if (match.Success)
                {
                    digits = match.Value.Length;
                }

                precision = float.Parse($"0.5E-{digits + 2}");
            }

            // Exactly map cardinal directions to avoid precision loss
            float x = angle switch
            {
                0.0f => 0.0f,
                90.0f => 1.0f,
                180.0f => 0.0f,
                270.0f => -1.0f,
                360.0f => 0.0f,
                _ => (float)Math.Sin(angle / 180.0 * Math.PI)
            };
            float y = angle switch
            {
                0.0f => 1.0f,
                90.0f => 0.0f,
                180.0f => -1.0f,
                270.0f => 0.0f,
                360.0f => 1.0f,
                _ => (float)Math.Cos(angle / 180.0 * Math.PI)
            };
            if (SourceTasAnalogMode != SourceTasAnalogMode.Precise)
            {
                x *= magnitude;
                y *= magnitude;
            }

            return ComputeFeather(x, y, precision, magnitude);
        }

        private (Vector2, SourceTasVector2Short) ComputeFeather(float x, float y, float precision, float upperLimit)
        {
            static short RoundToValidShort(float f)
            {
                return (short)Math.Round((f * (1.0 - DeadZone) + DeadZone) * 32767);
            }

            // Assure both are positive and x >= y
            if (x < 0)
            {
                var (direction, directionShort) = ComputeFeather(-x, y, precision, upperLimit);
                return (new Vector2(-direction.X, direction.Y),
                    new SourceTasVector2Short((short)-directionShort.X, directionShort.Y));
            }

            if (y < 0)
            {
                var (direction, directionShort) = ComputeFeather(x, -y, precision, upperLimit);
                return (new Vector2(direction.X, -direction.Y),
                    new SourceTasVector2Short(directionShort.X, (short)-directionShort.Y));
            }

            if (x < y)
            {
                var (direction, directionShort) = ComputeFeather(y, x, precision, upperLimit);
                return (new Vector2(direction.Y, direction.X),
                    new SourceTasVector2Short(directionShort.Y, directionShort.X));
            }

            Debug.Assert(x >= 0.0f);
            Debug.Assert(y >= 0.0f);
            Debug.Assert(x >= y);

            short shortX, shortY;
            switch (SourceTasAnalogMode)
            {
                case SourceTasAnalogMode.Ignore:
                case SourceTasAnalogMode.Circle:
                    shortX = RoundToValidShort(x);
                    shortY = RoundToValidShort(y);
                    break;

                case SourceTasAnalogMode.Square:
                    float divisor = Math.Max(Math.Abs(x), Math.Abs(y));
                    x /= divisor;
                    y /= divisor;
                    shortX = RoundToValidShort(x);
                    shortY = RoundToValidShort(y);
                    break;

                case SourceTasAnalogMode.Precise:
                    short upperbound = (short)Math.Round(ScalarMath.Clamp(upperLimit * 32767, Lowerbound, 32767),
                        MidpointRounding.AwayFromZero);
                    SourceTasVector2Short result = ComputePrecise(new Vector2(x, y), precision, upperbound);
                    shortX = result.X;
                    shortY = result.Y;
                    break;

                default:
                    throw new global::Celeste.Core.Diagnostics.SourceUnreachableException();
            }

            if (SourceTasAnalogMode == SourceTasAnalogMode.Ignore)
            {
                return (new Vector2(x, y), new SourceTasVector2Short(shortX, shortY));
            }

            x = (float)(Math.Max(shortX / 32767.0 - DeadZone, 0.0) / (1 - DeadZone));
            y = (float)(Math.Max(shortY / 32767.0 - DeadZone, 0.0) / (1 - DeadZone));
            return (new Vector2(x, y), new SourceTasVector2Short(shortX, shortY));
        }

        private SourceTasVector2Short ComputePrecise(Vector2 direction, float precision, short upperbound)
        {
            // it should hold that direction has x>0, y>0, x>=y
            // we look for the least y/x difference
            if (direction.Y < 1e-10)
            {
                return new SourceTasVector2Short(upperbound, 0);
            }

            double approx = (double)direction.Y / (double)direction.X;
            double multip = (double)direction.X / (double)direction.Y;
            double upperl = (double)upperbound / 32767;
            double leastError = approx;
            short retX = upperbound, retY = 0; // y/x=0, so error is approx
            short y = Lowerbound;
            while (true)
            {
                double ys = (double)y / 32767 - DeadZone;
                double xx = Math.Min(DeadZone + multip * ys, upperl);
                short x = (short)Math.Floor(xx * 32767);
                double xs = (double)x / 32767 - DeadZone;
                double error = Math.Abs(ys / xs - approx);
                if (xs * xs + ys * ys >= AmpLowerbound && (error < leastError || error <= precision))
                {
                    leastError = error;
                    retX = x;
                    retY = y;
                }

                if (x < upperbound)
                {
                    ++x;
                    xs = (double)x / 32767 - DeadZone;
                    error = Math.Abs(ys / xs - approx);
                    if (xs * xs + ys * ys >= AmpLowerbound && (error < leastError || error <= precision))
                    {
                        leastError = error;
                        retX = x;
                        retY = y;
                    }
                }

                if (xx >= upperl)
                {
                    break;
                }

                ++y;
            }

            return new SourceTasVector2Short(retX, retY);
        }

        private class Meta : ISourceTasCommandMeta
        {
            public string Insert => $"AnalogMode{CommandInfo.Separator}[0;Ignore/Circle/Square/Precise]";
            public bool HasArguments => true;

            public IEnumerator<CommandAutoCompleteEntry> GetAutoCompleteEntries(string[] args, string filePath,
                int fileLine)
            {
                if (args.Length != 1)
                {
                    yield break;
                }

                foreach (var mode in (SourceTasAnalogMode[])Enum.GetValues(typeof(SourceTasAnalogMode)))
                {
                    yield return new CommandAutoCompleteEntry { Name = mode.ToString(), IsDone = true };
                }
            }
        }

        // SourceTasAnalogMode, Mode
        [SourceTasCommand("AnalogMode", Aliases = ["AnalogueMode"], ExecuteTiming = SourceTasExecuteTiming.Parse,
            MetaDataProvider = typeof(Meta))]
        internal void SourceTasAnalogModeCommand(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            string[] args = commandLine.Arguments;
            if (args.Length == 0 || !Enum.TryParse(args[0], true, out SourceTasAnalogMode mode))
            {
                driver.AbortSourceTas(
                    $"AnalogMode command failed at line {fileLine}\nMode must be Ignore, Circle, Square or Precise");
            }
            else
            {
                SourceTasAnalogMode = mode;
            }
        }

        [SourceTasClearInputs]
        private static void SourceReset() => CelesteTasModule.Instance.Driver.SourceAnalog.Reset();

        internal void Reset()
        {
            SourceTasAnalogMode = SourceTasAnalogMode.Ignore;
        }
    }
}
