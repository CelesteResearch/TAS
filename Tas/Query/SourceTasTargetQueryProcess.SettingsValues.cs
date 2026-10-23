using System;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Game.Settings;

namespace Celeste.Mods.Tas.Query
{
    public sealed partial class SourceTasTargetQueryProcess
    {
        private static string GetRuntimeQueryFieldName(Type type, string name) =>
            type == typeof(SettingsState) && name == "Language" ? nameof(SettingsState.LanguageId) : name;

        // Original TAS names are an input protocol. The actual settings fields retain their integer storage.
        private SourceQueryMemberAccess CreateRuntimeQueryField(FieldInfo field)
        {
            if (field == null) return null;
            string enumIdentity = field.DeclaringType == typeof(SettingsState)
                ? field.Name switch
                {
                    nameof(SettingsState.ScreenShake) => "Celeste.ScreenshakeAmount",
                    nameof(SettingsState.Rumble) => "Celeste.RumbleAmount",
                    nameof(SettingsState.SpeedrunClock) => "Celeste.SpeedrunType",
                    _ => null
                }
                : null;
            if (enumIdentity == null) return SourceQueryMemberAccess.FromRuntime(field);

            const string assembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            var definition =
                Members.RequireDefinition(assembly, "Celeste.Settings", field.Name, SourceMemberKind.Field);
            var enumDefinition = Members.GetOriginalTypeDefinition(assembly, enumIdentity);
            if (field.IsStatic || field.FieldType != typeof(int) || definition.IsStatic ||
                definition.ValueTypeAssemblyIdentity != assembly || definition.ValueTypeIdentity != enumIdentity ||
                definition.ValueTypeReference == null || enumDefinition.EnumUnderlyingTypeIdentity != "System.Int32")
                throw new InvalidOperationException(
                    "The original TAS settings enum input does not match actual integer storage.");
            return SourceQueryMemberAccess.FromRuntime(field,
                new SourceQueryValueType(field.FieldType, assembly, enumIdentity, definition.ValueTypeReference));
        }
    }
}
