using System;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;
using Celeste.Everest.Modules.Runtime;
using Celeste.Everest.Reflection;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Everest;
using Celeste.Game.Entities.Triggers.Respawning;
using Celeste.Mods.Tas.Module;
using StudioCommunication;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed class SourceTasGunProcess
    {
        private readonly TasPlaybackDriver driver;

        private const BindingFlags All = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                                         BindingFlags.Instance;

        private SourceMemberCatalog Members =>
            ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMembers;

        private SourceAssemblyMethodCatalog Methods =>
            ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMethods;

        internal static Type SourceCommandMetaType => typeof(Meta);

        private class Meta : ISourceTasCommandMeta
        {
            public string Insert => $"Gun{CommandInfo.Separator}[0;X]{CommandInfo.Separator}[1;Y]";
            public bool HasArguments => true;
        }

        private readonly Lazy<SourcePhysicalMemberBinding?> GunInputCursorPosition;
        private readonly Lazy<SourceOrderedMethod?> GunelineGunshot;

        internal SourceTasGunProcess(TasPlaybackDriver driver)
        {
            this.driver = driver;
            GunInputCursorPosition = new(() =>
            {
                var type = GetModType("Guneline", "Guneline.GunInput");
                if (type == null) return null;
                var property = Members.GetMember(type, "CursorPosition", SourceMemberKind.Property, All, true);
                if (property == null)
                    driver.LogSourceTas(EverestLogLevel.Error,
                        $"Failed to find property 'CursorPosition' on type '{Members.GetOriginalTypeFullName(type)}'");
                return property;
            });
            GunelineGunshot = new(() =>
            {
                var type = GetModType("Guneline", "Guneline.Guneline");
                if (type == null) return null;
                var method = Methods.GetMethodInfo(Members, type, "Gunshot", null, All);
                if (method == null)
                    driver.LogSourceTas(EverestLogLevel.Error,
                        $"Failed to find method 'Gunshot' on type '{Members.GetOriginalTypeFullName(type)}'");
                return method;
            });
        }

        private Type? GetModType(string name, string originalTypeIdentity)
        {
            var module = ((ISourceEverestModulesProcessEnvironment)driver.Process).ModulesProcess.Modules
                .FirstOrDefault(module => module.Metadata?.Name == name);
            if (module == null) return null;
            string assembly = Members.GetOriginalTypeDefinition(module.GetType()).AssemblyIdentity;
            var type = Members.GetActualSourceType(assembly, originalTypeIdentity);
            if (type == null)
            {
                driver.LogSourceTas(EverestLogLevel.Error,
                    $"Failed to find type '{originalTypeIdentity}' in assembly '{assembly}' of mod '{name}'");
                return null;
            }

            return type;
        }

        [SourceTasCommand("Gun", LegalInFullGame = false, MetaDataProvider = typeof(Meta))]
        internal void Gun(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            string[] args = commandLine.Arguments;
            if (args.Length < 2)
            {
                return;
            }

            if (float.TryParse(args[0], out float x)
                && float.TryParse(args[1], out float y)
                && SourceTasSceneSessionQuery.GetPlayer(driver.Process.Frame.Scenes.CurrentScene) is { } player
                && GunInputCursorPosition.Value != null
                && GunelineGunshot.Value != null
               )
            {
                Vector2 pos = new(x, y);
                GunInputCursorPosition.Value.Write(null, pos);
                GunelineGunshot.Value.Invoke(new object[]
                    { player, pos, global::Celeste.Simulation.Gameplay.Actors.Facing.FacingDirection.Left });
            }
        }
    }
}
