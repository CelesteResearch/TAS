using System;
using System.Collections.Generic;
using System.Linq;
using Celeste.Core.Reflection.Members;
using Celeste.Game.Controls.Source;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Game.Settings;
using ActualGrabMode = Celeste.Input.Gameplay.GrabMode;
using TargetQuery = Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess;

namespace Celeste.Mods.Tas.Query.Handlers
{
    internal class SourceSettingsQueryHandler : TargetQuery.Handler
    {
        public SourceSettingsQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
        }

        public override bool CanResolveInstances(Type type) => type == typeof(SettingsState);

        public override (HashSet<Type> Types, string[] MemberArgs)? ResolveBaseTypes(string[] queryArgs)
        {
            // Vanilla settings don't need a prefix
            if (Query.GetOriginalField(typeof(SettingsState), queryArgs[0],
                    QueryReflection.InstanceAnyVisibility, logFailure: false) != null)
            {
                return ([typeof(SettingsState)], queryArgs);
            }

            return null;
        }

        public override object[] ResolveInstances(Type type)
        {
            return [((Celeste.Game.Settings.ISettingsContext)Query.Engine).Settings];
        }

        public override Result<bool, TargetQuery.MemberAccessError> SetMember(object? instance, object? value,
            Type type,
            int memberIdx, string[] memberArgs, bool forceAllowCodeExecution)
        {
            if (instance is not SettingsState settings)
            {
                return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
            }

            switch (memberArgs[memberIdx])
            {
                case nameof(SettingsState.Rumble):
                    settings.Rumble = (int)value!;
                    ((Celeste.Game.Controls.Source.IOriginalGameInputProcessEnvironment)Query.Engine).InputSource
                        .Rumble(RumbleStrength.Medium, RumbleLength.Medium);
                    return Result<bool, TargetQuery.MemberAccessError>.Ok(true);

                case nameof(SettingsState.GrabMode):
                    settings.GrabMode = (ActualGrabMode)value!;
                    ((Celeste.Game.Controls.Source.IOriginalGameInputProcessEnvironment)Query.Engine).InputSource
                        .ResetGrab();
                    return Result<bool, TargetQuery.MemberAccessError>.Ok(true);

                case nameof(SettingsState.Fullscreen):
                case nameof(SettingsState.WindowScale):
                case nameof(SettingsState.VSync):
                case nameof(SettingsState.MusicVolume):
                case nameof(SettingsState.SFXVolume):
                    // Intentional no-op. A TAS should not modify these user preferences
                    return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
            }

            return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
        }

        public override IEnumerator<CommandAutoCompleteEntry> ProvideGlobalEntries(string[] queryArgs,
            string queryPrefix,
            TargetQuery.Variant variant)
        {
            if (queryArgs.Length > 1 || variant is not (TargetQuery.Variant.Get or TargetQuery.Variant.Set))
            {
                yield break;
            }

            // Manually selected to filter out useless entries
            var vanillaSettings =
                ((string[])
                [
                    "DisableFlashes", "ScreenShake", "GrabMode", "CrouchDashMode", "SpeedrunClock", "Pico8OnMainMenu",
                    "VariantsUnlocked"
                ]).Select(e => Query.GetOriginalField(typeof(SettingsState), e)!);
            foreach (var f in vanillaSettings)
            {
                yield return new CommandAutoCompleteEntry
                {
                    Name = f.Name,
                    Extra = $"{f.ValueType.CSharpName()} (Settings)",
                    IsDone = true,
                    StorageKey = $"{variant}_{typeof(SettingsState).FullName}"
                };
            }
        }
    }
}
