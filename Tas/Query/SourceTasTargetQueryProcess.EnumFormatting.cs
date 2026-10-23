using System;
using System.Collections.Generic;
using System.Linq;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Types;

namespace Celeste.Mods.Tas.Query
{
    public sealed partial class SourceTasTargetQueryProcess
    {
        internal bool TryFormatOriginalEnumValue(object? value, SourceTypeReference sourceType, out string text)
        {
            text = null;
            if (value == null || sourceType == null) return false;
            var definition =
                FindOriginalEnum(new SourceQueryValueType(value.GetType(), originalTypeReference: sourceType));
            if (definition == null) return false;
            text = FormatOriginalEnumDefault(value, definition);
            return true;
        }

        internal string FormatOriginalQueryValue(object value, SourceTypeReference sourceType)
        {
            if (value == null) return "null";
            var definition =
                FindOriginalEnum(new SourceQueryValueType(value.GetType(), originalTypeReference: sourceType));
            return definition == null ? FormatOriginalObject(value) : FormatOriginalEnumDefault(value, definition);
        }

        // Original installed TAS uses System.Enum's .NET 8 default formatter. Its source storage
        // comparisons use the unsigned type with the same bit width, even for signed enums.
        private string FormatOriginalEnumDefault(object value, SourceTypeDefinition definition)
        {
            Type underlying = GetEnumUnderlyingType(definition.EnumUnderlyingTypeIdentity);
            object storage = value.GetType().IsEnum ? Convert.ChangeType(value, underlying) : value;
            if (storage.GetType() != underlying)
                throw new InvalidOperationException(
                    "The query value does not have its qualified original enum storage type.");
            ulong bits = GetEnumStorageBits(storage, underlying);
            var fields = Members.GetOriginalMemberDefinitions(definition.AssemblyIdentity, definition.Identity)
                .Where(field => field.Kind == SourceMemberKind.Field && field.IsLiteral && field.HasConstant).ToArray();
            string[] names = fields.Select(field => field.Name).ToArray();
            ulong[] values = fields.Select(field => GetEnumStorageBits(field.ConstantValue, underlying)).ToArray();
            bool sorted = true;
            for (int i = 1; i < values.Length; i++)
                if (values[i - 1] > values[i])
                {
                    sorted = false;
                    break;
                }

            if (!sorted) Array.Sort(values, names);

            if (!definition.HasDeclaredAttribute("System.FlagsAttribute"))
            {
                bool sequential = true;
                for (int i = 0; i < values.Length; i++)
                    if (values[i] != (ulong)i)
                    {
                        sequential = false;
                        break;
                    }

                int index = sequential ? bits < (ulong)values.Length ? (int)bits : -1
                    : values.Length <= 32 ? Array.IndexOf(values, bits) : Array.BinarySearch(values, bits);
                return index >= 0 ? names[index] : storage.ToString();
            }

            if (bits == 0) return values.Length == 0 || values[0] != 0 ? "0" : names[0];
            int candidate = values.Length - 1;
            while (candidate >= 0 && values[candidate] > bits) candidate--;
            if (candidate >= 0 && values[candidate] == bits) return names[candidate];
            ulong remainder = bits;
            var found = new List<int>();
            while (candidate >= 0)
            {
                ulong flag = values[candidate];
                if (candidate == 0 && flag == 0) break;
                if ((remainder & flag) == flag)
                {
                    remainder &= ~flag;
                    found.Add(candidate);
                    if (remainder == 0) break;
                }

                candidate--;
            }

            if (remainder != 0) return storage.ToString();
            found.Reverse();
            return string.Join(", ", found.Select(index => names[index]));
        }
    }
}
