#nullable enable
namespace Celeste.Mods.Tas.Lua
{
    public class LuaValueHolder<T>(T value)
    {
        public T Value = value;
    }
}
