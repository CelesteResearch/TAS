using System;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Interop
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class SourceTasLoadStateAttribute : SourceTasEventAttribute
    {
        public SourceTasLoadStateAttribute(int priority = 0) : base(priority)
        {
        }
    }
}
