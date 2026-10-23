using System;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Game.Execution.Graphics;
using Celeste.Game.Profiles;
using Celeste.Graphics.Rendering.Effects;

namespace Celeste.Mods.Tas.Reflection
{
    public static class SourceTasGameGraphicsActualMemberBindings
    {
        public static void Register(GameApplication process)
        {
            const string assembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            const string sourceType = "Celeste.GFX";
            var members = process.SourceMembers;
            var definition = members.RequireDefinition(assembly, sourceType, "FxDistort", SourceMemberKind.Field);
            int expectedToken = process.SourceProfile == GameProfile.Original ? 67114957 : 67119574;
            if (definition.MetadataToken != expectedToken || !definition.IsStatic || !definition.IsPublic
                || definition.ValueTypeIdentity != "Microsoft.Xna.Framework.Graphics.Effect")
                throw new InvalidOperationException(
                    "The GFX.FxDistort source FieldDef differs from the selected original PE.");
            var actualType = typeof(OriginalGameGraphicsResources);
            var actualField = actualType.GetField("FxDistort",
                                  BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                              ?? throw new MissingFieldException(actualType.FullName, "FxDistort");
            if (actualField.FieldType != typeof(SourceEffect) || actualField.IsStatic)
                throw new InvalidOperationException(
                    "The distortion effect must be the genuine canonical graphics-resource instance field.");
            members.RegisterBinding(new SourcePhysicalMemberBinding(definition, actualType, actualField,
                staticSourceOwner: process.GraphicsResources));
        }
    }
}
