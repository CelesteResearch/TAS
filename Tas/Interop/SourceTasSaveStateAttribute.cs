using System;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Interop
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class SourceTasSaveStateAttribute : SourceTasEventAttribute
    {
        public SourceTasSaveStateAttribute(int priority = 0) : base(priority)
        {
        }
    }
}
