using System;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Game.Content.Maps.Entities;
using Celeste.Game.Content.Maps.Levels;
using Celeste.Game.Entities.BadelineDummy;
using Celeste.Game.Stories.CelestialResort;
using Celeste.Game.Stories.Temple.Badeline;

namespace Celeste.Mods.Tas.Reflection
{
    public static class SourceTasNpcHitboxPhysicalMemberBindings
    {
        private const string GameAssembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        public static void Register(GameApplication process)
        {
            var members = process.SourceMembers;
            BindField(members, "Celeste.NPC03_Oshiro_Cluttter", "sectionsComplete",
                typeof(NpcOshiroClutterState), typeof(int));
            BindField(members, "Celeste.NPC05_Badeline", "shadow",
                typeof(OriginalTempleBadelineState), typeof(BadelineDummyPresentation));
            BindField(members, "Celeste.EntityData", "Level",
                typeof(OriginalLevelEntityData), typeof(OriginalLevelRoomData));
            BindField(members, "Celeste.LevelData", "Name",
                typeof(OriginalLevelRoomData), typeof(string));
        }

        private static void BindField(SourceMemberCatalog members, string sourceType, string fieldName,
            Type actualType, Type actualValueType)
        {
            var sourceDefinition =
                members.RequireDefinition(GameAssembly, sourceType, fieldName, SourceMemberKind.Field);
            var actualDefinition = members.GetOriginalTypeDefinition(actualType);
            if (actualDefinition.AssemblyIdentity != GameAssembly || actualDefinition.Identity != sourceType ||
                sourceDefinition.IsStatic)
                throw new InvalidOperationException("The genuine NPC hitbox field owner does not match: " + sourceType +
                                                    "." + fieldName);
            var actualField = actualType.GetField(fieldName,
                                  BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                  BindingFlags.DeclaredOnly)
                              ?? throw new MissingFieldException(actualType.FullName, fieldName);
            if (actualField.IsStatic || actualField.FieldType != actualValueType)
                throw new InvalidOperationException("The genuine NPC hitbox physical field shape does not match: " +
                                                    sourceType + "." + fieldName);
            members.RegisterBinding(new SourcePhysicalMemberBinding(sourceDefinition, actualType, actualField));
        }
    }
}
