using System;

namespace Celeste.Mods.Tas.Information
{
// Exact long specialization of the original generic formatter for netstandard hosts.
    internal static class SourceTasStudioSizeFormatting
    {
        private static readonly string[] sizeSuffixes = ["B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB", "ZiB", "YiB"];

        public static (string Amount, string Suffix) HumanReadableBytes(this long value, int decimals = 1)
        {
            if (value < 0L)
            {
                (string amount, string suffix) = HumanReadableBytes(-value, decimals);
                return ("-" + amount, suffix);
            }

            if (value == 0L)
            {
                return (string.Format($"{{0:n{decimals}}}", 0), sizeSuffixes[0]);
            }

            // mag is 0 for bytes, 1 for KiB, 2, for MiB, etc.
            int mag = (int)Math.Log((double)value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag)
            // (i.e. the number of bytes in the unit corresponding to mag)
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // Make adjustment when the value is large enough that it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimals) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return (string.Format($"{{0:n{decimals}}}", adjustedSize), sizeSuffixes[mag]);
        }
    }
}
