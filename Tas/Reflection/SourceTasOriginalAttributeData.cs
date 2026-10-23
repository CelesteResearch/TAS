using System;
using System.Globalization;
using Celeste.Core.Reflection.Methods;

namespace Celeste.Mods.Tas.Reflection
{
    public static class SourceTasOriginalAttributeData
    {
        public static int ReadEventPriority(SourceMethodDefinition definition, string originalAttributeIdentity)
        {
            string blob = definition.RequireDeclaredAttributeBlob(originalAttributeIdentity);
            if (blob == "01000000") return 0; // Original parameterless event attributes.
            if (blob.Length != 16 || !blob.StartsWith("0100", StringComparison.Ordinal) ||
                !blob.EndsWith("0000", StringComparison.Ordinal))
                throw new InvalidOperationException("Original TAS event attribute constructor data is not qualified.");
            uint value = 0;
            for (int index = 0; index < 4; index++)
                value |= uint.Parse(blob.Substring(4 + index * 2, 2), NumberStyles.HexNumber,
                    CultureInfo.InvariantCulture) << (index * 8);
            return unchecked((int)value);
        }
    }
}
