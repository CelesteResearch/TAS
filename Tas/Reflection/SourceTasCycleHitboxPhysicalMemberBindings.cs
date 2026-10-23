using System;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Game.Entities.Hazards.Spinners.Crystal;

namespace Celeste.Mods.Tas.Reflection
{
    public static class SourceTasCycleHitboxPhysicalMemberBindings
    {
        private const string GameAssembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        public static void Register(GameApplication process)
        {
            var members = process.SourceMembers;
            const string sourceType = "Celeste.CrystalStaticSpinner";
            var definition = members.RequireDefinition(GameAssembly, sourceType, "offset", SourceMemberKind.Field);
            var actualType = typeof(OriginalCrystalStaticSpinner);
            var originalType = members.GetOriginalTypeDefinition(actualType);
            if (originalType.AssemblyIdentity != GameAssembly || originalType.Identity != sourceType ||
                definition.IsStatic)
                throw new InvalidOperationException("The genuine spinner offset field owner does not match.");
            var field = actualType.GetField("offset",
                            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
                        ?? throw new MissingFieldException(actualType.FullName, "offset");
            if (field.IsStatic || field.FieldType != typeof(float) || !field.IsFamily)
                throw new InvalidOperationException("The genuine spinner offset physical field shape does not match.");
            members.RegisterBinding(new SourcePhysicalMemberBinding(definition, actualType, field));
        }
    }
}
