#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Celeste.Mods.Tas.Information
{
    internal static class SourceTasStringExtensions
    {
        private static readonly Regex LineBreakRegex = new(@"\r\n?|\n", RegexOptions.Compiled);

        public static string ReplaceLineBreak(this string text, string replacement)
        {
            return LineBreakRegex.Replace(text, replacement);
        }

        public static bool IsNullOrEmpty(this string? text)
        {
            return string.IsNullOrEmpty(text);
        }

        public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? text)
        {
            return !string.IsNullOrEmpty(text);
        }

        public static bool IsNullOrWhiteSpace(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool IsNotNullOrWhiteSpace(this string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }
    }
}
