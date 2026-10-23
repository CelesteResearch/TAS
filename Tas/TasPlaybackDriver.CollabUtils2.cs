#nullable enable
using Celeste.Mods.Tas.Interop;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasCollabUtils2Interop? sourceCollabUtils2;

        internal SourceTasCollabUtils2Interop CollabUtils2 =>
            sourceCollabUtils2 ??= new SourceTasCollabUtils2Interop(process, SourceModReflection);
    }
}
