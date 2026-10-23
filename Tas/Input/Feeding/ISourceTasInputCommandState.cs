using Celeste.Input.Runtime;
using Celeste.Input.Devices.Keyboard;
using System.Collections.Generic;

namespace Celeste.Mods.Tas.Input.Feeding
{
    public interface ISourceTasInputCommandState
    {
        HashSet<SourceKeys> GetSourcePressKeys(ISourceTasFeedInputFrame current, ISourceTasFeedInputFrame next);

        void SetSourceMouseState(SourceMInputProcess input, ISourceTasFeedInputFrame current,
            ISourceTasFeedInputFrame next);
    }
}
