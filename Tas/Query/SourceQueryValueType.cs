using System;
using Celeste.Core.Reflection.Types;

namespace Celeste.Mods.Tas.Query
{
    // Original value-type identity remains part of the value cache when distinct source enums
    // intentionally share the same physical integer representation.
    internal readonly struct SourceQueryValueType : IEquatable<SourceQueryValueType>
    {
        public Type PhysicalType { get; }
        public string OriginalAssemblyIdentity { get; }
        public string OriginalTypeIdentity { get; }
        public SourceTypeReference OriginalTypeReference { get; }

        public SourceQueryValueType(Type physicalType, string originalAssemblyIdentity = null,
            string originalTypeIdentity = null,
            SourceTypeReference originalTypeReference = null)
        {
            PhysicalType = physicalType ?? throw new ArgumentNullException(nameof(physicalType));
            OriginalAssemblyIdentity = originalAssemblyIdentity;
            OriginalTypeIdentity = originalTypeIdentity;
            OriginalTypeReference = originalTypeReference;
        }

        public bool Equals(SourceQueryValueType other) => PhysicalType == other.PhysicalType
                                                          && OriginalAssemblyIdentity ==
                                                          other.OriginalAssemblyIdentity &&
                                                          OriginalTypeIdentity == other.OriginalTypeIdentity
                                                          && SourceTypeReferenceEqualityComparer.Instance.Equals(
                                                              OriginalTypeReference, other.OriginalTypeReference);

        public override bool Equals(object other) => other is SourceQueryValueType value && Equals(value);

        public override int GetHashCode() => HashCode.Combine(PhysicalType, OriginalAssemblyIdentity,
            OriginalTypeIdentity,
            SourceTypeReferenceEqualityComparer.Instance.GetHashCode(OriginalTypeReference));
    }
}
