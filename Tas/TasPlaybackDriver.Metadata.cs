#nullable enable
using Celeste.Mods.Tas.Commands;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasMetadataCommands? sourceMetadataCommands;

        internal SourceTasMetadataCommands SourceMetadataCommands =>
            sourceMetadataCommands ??= new SourceTasMetadataCommands(process, this);
    }
}
