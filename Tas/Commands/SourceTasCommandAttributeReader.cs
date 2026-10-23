using System;
using System.IO;
using System.Text;
using System.Globalization;
using Celeste.Core.Reflection.Methods;

namespace Celeste.Mods.Tas.Commands
{
    internal static class SourceTasCommandAttributeReader
    {
        internal static SourceTasCommandAttribute Read(SourceMethodDefinition definition,
            Func<string, Type> resolveProvider)
        {
            string hexadecimal = definition.RequireDeclaredAttributeBlob("TAS.Input.TasCommandAttribute");
            var bytes = new byte[hexadecimal.Length / 2];
            if ((hexadecimal.Length & 1) != 0) throw new FormatException("Invalid custom attribute blob length.");
            for (int index = 0; index < bytes.Length; index++)
                bytes[index] = byte.Parse(hexadecimal.Substring(index * 2, 2), NumberStyles.HexNumber,
                    CultureInfo.InvariantCulture);
            using var stream = new MemoryStream(bytes, writable: false);
            using var reader = new BinaryReader(stream, Encoding.UTF8);
            if (reader.ReadUInt16() != 1) throw new FormatException("Invalid custom attribute prolog.");
            var attribute = new SourceTasCommandAttribute(ReadString(reader));
            ushort count = reader.ReadUInt16();
            for (int index = 0; index < count; index++)
            {
                byte memberKind = reader.ReadByte();
                if (memberKind != 0x53) throw new FormatException("TAS command metadata must declare source fields.");
                byte valueType = reader.ReadByte();
                byte elementType = 0;
                if (valueType == 0x1d) elementType = reader.ReadByte();
                string enumType = valueType == 0x55 ? ReadString(reader) : null;
                string name = ReadString(reader);
                switch (name)
                {
                    case "Aliases" when valueType == 0x1d && elementType == 0x0e:
                        int length = reader.ReadInt32();
                        if (length == -1) attribute.Aliases = null;
                        else
                        {
                            if (length < 0) throw new FormatException("Invalid aliases array length.");
                            attribute.Aliases = new string[length];
                            for (int item = 0; item < length; item++) attribute.Aliases[item] = ReadString(reader);
                        }

                        break;
                    case "CalcChecksum" when valueType == 0x02:
                        attribute.CalcChecksum = reader.ReadBoolean();
                        break;
                    case "LegalInFullGame" when valueType == 0x02:
                        attribute.LegalInFullGame = reader.ReadBoolean();
                        break;
                    case "ExecuteTiming" when valueType == 0x55 && enumType.Split(',')[0] == "TAS.Input.ExecuteTiming":
                        attribute.ExecuteTiming = (SourceTasExecuteTiming)reader.ReadByte();
                        break;
                    case "MetaDataProvider" when valueType == 0x50:
                        string provider = ReadString(reader);
                        attribute.MetaDataProvider = provider == null
                            ? null
                            : resolveProvider(provider) ??
                              throw new InvalidOperationException("TAS metadata provider is not qualified: " +
                                                                  provider);
                        break;
                    default:
                        throw new InvalidOperationException("Original TAS command field is not qualified: " + name);
                }
            }

            if (stream.Position != stream.Length)
                throw new FormatException("Unconsumed TAS command custom attribute data.");
            return attribute;
        }

        private static string ReadString(BinaryReader reader)
        {
            byte leading = reader.ReadByte();
            if (leading == 0xff) return null;
            int length;
            if ((leading & 0x80) == 0) length = leading;
            else if ((leading & 0xc0) == 0x80) length = ((leading & 0x3f) << 8) | reader.ReadByte();
            else if ((leading & 0xe0) == 0xc0)
                length = ((leading & 0x1f) << 24) | (reader.ReadByte() << 16) | (reader.ReadByte() << 8) |
                         reader.ReadByte();
            else throw new FormatException("Invalid serialized custom attribute string length.");
            if (length > reader.BaseStream.Length - reader.BaseStream.Position) throw new EndOfStreamException();
            return Encoding.UTF8.GetString(reader.ReadBytes(length));
        }
    }
}
