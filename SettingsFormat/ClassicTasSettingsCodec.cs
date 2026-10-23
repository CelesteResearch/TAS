#nullable enable
using System;
using System.IO;
using System.Text;

namespace Celeste.Tas.SettingsFormat
{
    // Each host owns one live settings object. This codec preserves the original 1/0
    // setting lines while the host supplies its own storage path and other options.
    public static class ClassicTasSettingsCodec
    {
        public static bool ReadLine(string key, string value, ref bool enabled)
        {
            if (!string.Equals(key, "Enabled", StringComparison.Ordinal)) return false;
            if (value == "1") enabled = true;
            else if (value == "0") enabled = false;
            else if (bool.TryParse(value, out bool parsed)) enabled = parsed;
            return true;
        }

        public static bool ReadLine(string key, string value, ref bool enabled,
            ref bool hideFreezeFrames)
        {
            if (ReadLine(key, value, ref enabled)) return true;
            if (!string.Equals(key, "HideFreezeFrames", StringComparison.Ordinal)) return false;
            if (value == "1") hideFreezeFrames = true;
            else if (value == "0") hideFreezeFrames = false;
            else if (bool.TryParse(value, out bool parsed)) hideFreezeFrames = parsed;
            return true;
        }

        public static bool ReadLine(string key, string value, ref bool enabled,
            ref bool hideFreezeFrames, ref bool autoPauseDraft)
        {
            if (ReadLine(key, value, ref enabled, ref hideFreezeFrames)) return true;
            if (!string.Equals(key, "AutoPauseDraft", StringComparison.Ordinal)) return false;
            if (value == "1") autoPauseDraft = true;
            else if (value == "0") autoPauseDraft = false;
            else if (bool.TryParse(value, out bool parsed)) autoPauseDraft = parsed;
            return true;
        }

        public static void AppendLine(StringBuilder text, bool enabled)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            text.Append("Enabled=").AppendLine(enabled ? "1" : "0");
        }

        public static void AppendLine(StringBuilder text, bool enabled, bool hideFreezeFrames)
        {
            AppendLine(text, enabled);
            text.Append("HideFreezeFrames=").AppendLine(hideFreezeFrames ? "1" : "0");
        }

        public static void AppendLine(StringBuilder text, bool enabled, bool hideFreezeFrames,
            bool autoPauseDraft)
        {
            AppendLine(text, enabled, hideFreezeFrames);
            text.Append("AutoPauseDraft=").AppendLine(autoPauseDraft ? "1" : "0");
        }

        public static bool Load(string path, ref bool enabled)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path)) return false;
            foreach (string line in File.ReadLines(path))
            {
                int separator = line.IndexOf('=');
                if (separator <= 0) continue;
                ReadLine(line.Substring(0, separator).Trim(), line.Substring(separator + 1).Trim(), ref enabled);
            }

            return true;
        }

        public static bool Load(string path, ref bool enabled, ref bool hideFreezeFrames)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path)) return false;
            foreach (string line in File.ReadLines(path))
            {
                int separator = line.IndexOf('=');
                if (separator <= 0) continue;
                ReadLine(line.Substring(0, separator).Trim(),
                    line.Substring(separator + 1).Trim(), ref enabled, ref hideFreezeFrames);
            }

            return true;
        }

        public static bool Load(string path, ref bool enabled, ref bool hideFreezeFrames,
            ref bool autoPauseDraft)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path)) return false;
            foreach (string line in File.ReadLines(path))
            {
                int separator = line.IndexOf('=');
                if (separator <= 0) continue;
                ReadLine(line.Substring(0, separator).Trim(),
                    line.Substring(separator + 1).Trim(), ref enabled, ref hideFreezeFrames,
                    ref autoPauseDraft);
            }

            return true;
        }

        // For the Godot TAS settings file. Unity calls AppendLine while writing its full
        // settings document, preserving all its existing unrelated options.
        public static void Save(string path, bool enabled)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            string? directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);
            var text = new StringBuilder(16);
            AppendLine(text, enabled);
            File.WriteAllText(path, text.ToString());
        }

        public static void Save(string path, bool enabled, bool hideFreezeFrames)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            string? directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);
            var text = new StringBuilder(48);
            AppendLine(text, enabled, hideFreezeFrames);
            File.WriteAllText(path, text.ToString());
        }

        public static void Save(string path, bool enabled, bool hideFreezeFrames,
            bool autoPauseDraft)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            string? directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);
            var text = new StringBuilder(72);
            AppendLine(text, enabled, hideFreezeFrames, autoPauseDraft);
            File.WriteAllText(path, text.ToString());
        }
    }
}
