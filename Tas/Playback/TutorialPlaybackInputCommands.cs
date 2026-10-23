using System.Collections.Generic;
using Celeste.Mods.Tas.Input.Feeding;
using Celeste.Input.Devices.Keyboard;
using Celeste.Input.Runtime;

namespace Celeste.Mods.Tas.Playback
{
    internal sealed class TutorialPlaybackInputCommands : ISourceTasInputCommandState
    {
        private readonly HashSet<SourceKeys> keys = new();

        public HashSet<SourceKeys> GetSourcePressKeys(ISourceTasFeedInputFrame current, ISourceTasFeedInputFrame next)
        {
            keys.Clear();
            return keys;
        }

        public void SetSourceMouseState(SourceMInputProcess input, ISourceTasFeedInputFrame current,
            ISourceTasFeedInputFrame next)
        {
            input.Mouse.PreviousState = input.Mouse.CurrentState;
            input.Mouse.CurrentState = default;
        }
    }
}
