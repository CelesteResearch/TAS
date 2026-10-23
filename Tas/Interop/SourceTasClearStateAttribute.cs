using System;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Interop
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class SourceTasClearStateAttribute : SourceTasEventAttribute
    {
        public SourceTasClearStateAttribute(int priority = 0) : base(priority)
        {
        }
    }
}
