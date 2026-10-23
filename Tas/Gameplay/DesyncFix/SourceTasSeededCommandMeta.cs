using System.Collections.Generic;
using StudioCommunication;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Gameplay.DesyncFix
{
    internal sealed class SourceTasSeededCommandMeta : ISourceTasCommandMeta
    {
        public SourceTasSeededCommandMeta()
        {
        }

        public string Insert => $"SeedRandom{CommandInfo.Separator}[0;Target]{CommandInfo.Separator}[1;Seed]";
        public bool HasArguments => true;

        public IEnumerator<CommandAutoCompleteEntry> GetAutoCompleteEntries(string[] args, string path, int line)
        {
            if (args.Length != 1) yield break;
            foreach (var handler in CelesteTasModule.Instance.Driver.SourceSeededRandomness.SourceHandlers)
                yield return new CommandAutoCompleteEntry { Name = handler.Name, IsDone = true };
        }
    }
}
