using Celeste.Input.Runtime;
using Celeste.Input.Devices.Keyboard;
using System.Collections.Generic;
using Celeste.Mods.Tas.Input.Feeding;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver : ISourceTasInputCommandState
    {
        HashSet<SourceKeys> ISourceTasInputCommandState.GetSourcePressKeys(
            ISourceTasFeedInputFrame current, ISourceTasFeedInputFrame next)
            => SourcePressCommand.GetKeys();

        void ISourceTasInputCommandState.SetSourceMouseState(SourceMInputProcess input,
            ISourceTasFeedInputFrame current, ISourceTasFeedInputFrame next)
            => SourceMouseCommand.SetMouseState(input);
    }
}
