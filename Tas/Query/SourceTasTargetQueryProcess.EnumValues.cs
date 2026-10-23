using System;
using System.Globalization;
using System.Linq;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Types;

namespace Celeste.Mods.Tas.Query
{
    public sealed partial class SourceTasTargetQueryProcess
    {
        private SourceTypeDefinition FindOriginalEnum(SourceQueryValueType valueType)
        {
            if (valueType.OriginalTypeReference is { } reference)
            {
                while (reference.Kind is SourceTypeReferenceKind.RequiredModifier
                       or SourceTypeReferenceKind.OptionalModifier
                       or SourceTypeReferenceKind.Pinned or SourceTypeReferenceKind.Sentinel)
                    reference = reference.ElementType;
                if (reference.Kind != SourceTypeReferenceKind.Named) return null;
                var originalType =
                    Members.GetOriginalTypeDefinition(reference.AssemblyIdentity, reference.TypeIdentity);
                return originalType.EnumUnderlyingTypeIdentity == null ? null : originalType;
            }

            if (valueType.OriginalTypeIdentity == null) return null;
            if (valueType.OriginalAssemblyIdentity == null)
                throw new InvalidOperationException("The original value type has no qualified PE assembly scope.");
            var definition = Members.OriginalTypeDefinitions.FirstOrDefault(definition =>
                definition.AssemblyIdentity == valueType.OriginalAssemblyIdentity
                && definition.Identity == valueType.OriginalTypeIdentity);
            return definition?.EnumUnderlyingTypeIdentity == null ? null : definition;
        }

        // Installed System.Enum.TryParseByValueOrName/TryParseByName. The enum's actual
        // representation may be an integer; the original PE constants still define its names.
        private bool TryParseOriginalEnum(SourceTypeDefinition definition, Type physicalType, string text,
            out object result)
        {
            result = null;
            string value = text.TrimStart();
            if (value.Length == 0) return false;
            Type underlying = GetEnumUnderlyingType(definition.EnumUnderlyingTypeIdentity);
            if ((value[0] >= '0' && value[0] <= '9') || value[0] == '+' || value[0] == '-')
            {
                try
                {
                    var numeric = Convert.ChangeType(value, underlying, CultureInfo.InvariantCulture);
                    result = BoxOriginalEnumValue(physicalType, underlying, numeric);
                    return true;
                }
                catch (OverflowException)
                {
                    return false;
                }
                catch (FormatException)
                {
                }
            }

            var fields = Members.GetOriginalMemberDefinitions(definition.AssemblyIdentity, definition.Identity)
                .Where(field => field.Kind == SourceMemberKind.Field && field.IsLiteral && field.HasConstant)
                .ToArray();
            string[] names = fields.Select(field => field.Name).ToArray();
            ulong[] constants = fields.Select(field => GetEnumStorageBits(field.ConstantValue, underlying)).ToArray();
            bool sorted = true;
            for (int i = 1; i < constants.Length; i++)
                if (constants[i - 1] > constants[i])
                {
                    sorted = false;
                    break;
                }

            if (!sorted) Array.Sort(constants, names);
            ulong bits = 0;
            foreach (string part in value.Split(','))
            {
                string name = part.Trim();
                if (name.Length == 0) return false;
                int index = Array.FindIndex(names,
                    candidate => string.Equals(candidate, name, StringComparison.OrdinalIgnoreCase));
                if (index < 0) return false;
                bits |= constants[index];
            }

            result = BoxOriginalEnumValue(physicalType, underlying, FromEnumStorageBits(bits, underlying));
            return true;
        }

        private static object BoxOriginalEnumValue(Type physicalType, Type underlying, object value)
        {
            if (physicalType.IsEnum)
            {
                if (Enum.GetUnderlyingType(physicalType) != underlying)
                    throw new InvalidOperationException(
                        "The actual enum has a different storage type than its original PE definition.");
                return Enum.ToObject(physicalType, value);
            }

            if (physicalType != underlying)
                throw new InvalidOperationException(
                    "The actual source enum representation has not been qualified for its original storage type.");
            return value;
        }

        private static Type GetEnumUnderlyingType(string identity) => identity switch
        {
            "System.SByte" => typeof(sbyte), "System.Byte" => typeof(byte),
            "System.Int16" => typeof(short), "System.UInt16" => typeof(ushort),
            "System.Int32" => typeof(int), "System.UInt32" => typeof(uint),
            "System.Int64" => typeof(long), "System.UInt64" => typeof(ulong),
            _ => throw new InvalidOperationException("The original enum uses an unsupported underlying storage type: " +
                                                     identity)
        };

        private static ulong GetEnumStorageBits(object value, Type underlying)
        {
            if (underlying == typeof(sbyte)) return unchecked((byte)(sbyte)value);
            if (underlying == typeof(byte)) return (byte)value;
            if (underlying == typeof(short)) return unchecked((ushort)(short)value);
            if (underlying == typeof(ushort)) return (ushort)value;
            if (underlying == typeof(int)) return unchecked((uint)(int)value);
            if (underlying == typeof(uint)) return (uint)value;
            if (underlying == typeof(long)) return unchecked((ulong)(long)value);
            return (ulong)value;
        }

        private static object FromEnumStorageBits(ulong bits, Type underlying)
        {
            if (underlying == typeof(sbyte)) return unchecked((sbyte)bits);
            if (underlying == typeof(byte)) return unchecked((byte)bits);
            if (underlying == typeof(short)) return unchecked((short)bits);
            if (underlying == typeof(ushort)) return unchecked((ushort)bits);
            if (underlying == typeof(int)) return unchecked((int)bits);
            if (underlying == typeof(uint)) return unchecked((uint)bits);
            if (underlying == typeof(long)) return unchecked((long)bits);
            return bits;
        }
    }
}
