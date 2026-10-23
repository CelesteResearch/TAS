using System;
using System.Text;

namespace Celeste.Mods.Tas.Commands
{
    internal static class SourceTasLineEndings
    {
        internal static string NormalizeToLf(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            StringBuilder result = null;
            int copied = 0;
            for (int index = 0; index < value.Length; index++)
            {
                char character = value[index];
                // ReplaceLineEndings recognizes CR/LF, FF, NEL, LS and PS. Existing LF is already normalized.
                if (character != '\r' && character != '\f' && character != '\u0085'
                    && character != '\u2028' && character != '\u2029') continue;
                result ??= new StringBuilder(value.Length);
                result.Append(value, copied, index - copied);
                result.Append('\n');
                if (character == '\r' && index + 1 < value.Length && value[index + 1] == '\n') index++;
                copied = index + 1;
            }

            if (result == null) return value;
            result.Append(value, copied, value.Length - copied);
            return result.ToString();
        }
    }
}
