using System;
using Celeste.Mods.Tas.Commands;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasRequireDependencyProcess? sourceRequireDependencyCommand;

        internal SourceTasRequireDependencyProcess SourceRequireDependencyCommand => sourceRequireDependencyCommand ??
            throw new InvalidOperationException("Source RequireDependency command has not been constructed.");

        private void InitializeSourceRequireDependencyCommand()
        {
            if (sourceRequireDependencyCommand != null)
                throw new InvalidOperationException("Source RequireDependency command has already been constructed.");
            sourceRequireDependencyCommand = new SourceTasRequireDependencyProcess(this);
        }
    }
}
