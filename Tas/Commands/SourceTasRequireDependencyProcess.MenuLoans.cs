using System;
using System.Collections.Generic;
using Celeste.Game.Audio.Runtime;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed partial class SourceTasRequireDependencyProcess
    {
        internal OriginalGameAudioSession DependencyConfirmationAudio => Audio;
        internal IEnumerable<KeyValuePair<string, Version?>> MissingDependenciesForConfirmation => missingDependencies;
    }
}
