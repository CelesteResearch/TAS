using Celeste.Mods.Tas.Query.Handlers;
using System;
using System.Reflection;
using Celeste.Core.Reflection.Members;

namespace Celeste.Mods.Tas.Query
{
    internal static class SourceTasQueryInternalValueTypeBindings
    {
        internal static void Register(SourceMemberCatalog members)
        {
            const string assembly = "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            Register(members, assembly, "TAS.InfoHUD.EntityQueryHandler+SubpixelPosition",
                typeof(SourceEntityQueryHandler.SubpixelPosition));
            Register(members, assembly, "TAS.InfoHUD.EntityQueryHandler+SubpixelComponent",
                typeof(SourceEntityQueryHandler.SubpixelComponent));
            members.RegisterSourceType(SourceComponentQueryHandler.SourcePlayerStateType, assembly,
                "TAS.InfoHUD.ComponentQueryHandler+PlayerState");
            members.RegisterSourceType(SourceComponentQueryHandler.SourceSeekerStateType, assembly,
                "TAS.InfoHUD.ComponentQueryHandler+SeekerState");
            members.RegisterSourceType(SourceComponentQueryHandler.SourceOshiroStateType, assembly,
                "TAS.InfoHUD.ComponentQueryHandler+OshiroState");
        }

        private static void Register(SourceMemberCatalog members, string assembly, string originalType, Type actualType)
        {
            members.RegisterSourceType(actualType, assembly, originalType);
            foreach (var definition in members.GetOriginalMemberDefinitions(assembly, originalType))
            {
                MemberInfo actualMember = definition.Kind == SourceMemberKind.Field
                    ? actualType.GetField(definition.Name,
                        BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.DeclaredOnly)
                    : actualType.GetProperty(definition.Name,
                        BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.DeclaredOnly);
                if (actualMember == null) throw new MissingMemberException(actualType.FullName, definition.Name);
                members.RegisterBinding(new SourcePhysicalMemberBinding(definition, actualType, actualMember));
            }
        }
    }
}
