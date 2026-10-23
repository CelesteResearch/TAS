using System;

namespace Celeste.Mods.Tas.Input.Bindings
{
    public sealed class SourceSpeedrunToolInputImports
    {
        public Action InputDeregister;
        public void TryInputDeregister() => InputDeregister?.Invoke();
    }
}
