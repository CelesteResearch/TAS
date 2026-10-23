using System;
using System.Collections.Generic;
using Celeste.Game.Execution.Levels;

namespace Celeste.Mods.Tas.Interop
{
// Assigned by the actual source ModInterop process.
#pragma warning disable CS0649
    internal sealed class SourceSpeedrunToolTasActionImports
    {
        public Func<string, bool>? SaveState;
        public Func<string, bool>? LoadState;
        public Action<string>? ClearState;
        public Func<string, bool>? TasIsSaved;
        public Action? InputDeregister;
        public Action? OnTasDisableRun;
    }

#pragma warning restore CS0649
}
