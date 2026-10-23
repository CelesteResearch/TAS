using System;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Simulation.Gameplay.Hazards.Lightning;

namespace Celeste.Mods.Tas.Reflection
{
    public static class SourceTasActualCollidePhysicalMemberBindings
    {
        public static void Register(GameApplication process)
        {
            const string assembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            var members = process.SourceMembers;
            var definition =
                members.RequireDefinition(assembly, "Celeste.Lightning", "disappearing", SourceMemberKind.Field);
            var owner = members.GetOriginalTypeDefinition(typeof(LightningState));
            if (owner.AssemblyIdentity != assembly || owner.Identity != "Celeste.Lightning" || definition.IsStatic)
                throw new InvalidOperationException("The genuine Lightning disappearing field owner does not match.");
            var actualField = typeof(LightningState).GetField(nameof(LightningState.Disappearing),
                                  BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                              ?? throw new MissingFieldException(typeof(LightningState).FullName,
                                  nameof(LightningState.Disappearing));
            if (actualField.FieldType != typeof(bool) || actualField.IsStatic)
                throw new InvalidOperationException(
                    "The genuine Lightning disappearing physical field is not an instance Boolean.");
            members.RegisterBinding(new SourcePhysicalMemberBinding(definition, typeof(LightningState), actualField));
        }
    }
}
