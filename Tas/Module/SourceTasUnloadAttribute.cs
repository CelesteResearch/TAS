using System;

namespace Celeste.Mods.Tas.Module
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class SourceTasUnloadAttribute : SourceTasEventAttribute
    {
        public SourceTasUnloadAttribute(int priority = 0) : base(priority)
        {
        }
    }
}
