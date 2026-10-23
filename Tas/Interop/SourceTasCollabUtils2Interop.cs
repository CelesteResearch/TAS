#nullable enable
using System;
using System.Linq;
using Celeste.Everest.Modules.Runtime;
using Celeste.Core.Reflection.Interop;

namespace Celeste.Mods.Tas.Interop
{
    internal sealed class SourceTasCollabUtils2Interop
    {
        private readonly GameApplication process;
        private readonly Lazy<bool> installed;
        public bool Installed => installed.Value;
        public SourceTasCollabLobbyInterop Lobby { get; }

        internal SourceTasCollabUtils2Interop(GameApplication process, SourceTasModReflection reflection)
        {
            this.process = process;
            installed = new(() =>
                ((ISourceEverestModulesProcessEnvironment)process).ModulesProcess.Modules.Any(module =>
                    module.Metadata?.Name == "CollabUtils2"));
            Lobby = new SourceTasCollabLobbyInterop(process, reflection, () => Installed);
        }

        internal void Initialize()
            => ((ISourceModInteropEnvironment)process).ModInterop.ModInterop(typeof(SourceTasCollabLobbyInterop));
    }
}
