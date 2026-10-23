using System;
using System.Reflection;
using Celeste.Core.Reflection.Members;

namespace Celeste.Mods.Tas.Query
{
    internal readonly struct SourceQueryMemberIdentity
    {
        public string Name { get; }
        public Type DeclaringType { get; }

        internal SourceQueryMemberIdentity(string name, Type declaringType)
        {
            Name = name;
            DeclaringType = declaringType;
        }

        public static implicit operator SourceQueryMemberIdentity(SourceQueryMemberAccess member)
            => new SourceQueryMemberIdentity(member.Name, member.OriginalDeclaringType);

        public static implicit operator SourceQueryMemberIdentity(MethodInfo method)
            => new SourceQueryMemberIdentity(method.Name, method.DeclaringType);
    }
}
