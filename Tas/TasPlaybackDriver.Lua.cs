#nullable enable
using Celeste.Mods.Tas.Lua;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        private SourceTasEvalLuaCommandState? sourceLuaCommands;

        public SourceTasEvalLuaCommandState LuaCommands
            => sourceLuaCommands ??= new SourceTasEvalLuaCommandState(process, this);
    }
}
