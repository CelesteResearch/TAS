using System;

namespace Celeste.Mods.Tas.Module
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class SourceTasInitializeAttribute : SourceTasEventAttribute
    {
        public SourceTasInitializeAttribute(int priority = 0) : base(priority)
        {
        }
    }
}
