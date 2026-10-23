using Celeste.Input.Devices.Keyboard;
using System.Collections.Generic;
using System.Numerics;

namespace Celeste.Mods.Tas.Input.Feeding
{
    public interface ISourceTasFeedInputFrame
    {
        long SourceActions { get; }
        Vector2 FeatherStick { get; }
        Vector2 DashOnlyStick { get; }
        IEnumerable<SourceKeys> PressedKeys { get; }
    }
}
