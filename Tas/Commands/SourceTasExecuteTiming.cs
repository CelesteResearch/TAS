using System;

namespace Celeste.Mods.Tas.Commands
{
    [Flags]
    public enum SourceTasExecuteTiming : byte
    {
        /// Executes the command while parsing inputs, like Read commands
        Parse = 1 << 0,

        /// Executes the command at runtime while playing inputs, like Console commands
        Runtime = 1 << 1,
    }
}
