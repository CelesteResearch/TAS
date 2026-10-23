using System;

namespace Celeste.Mods.Tas.Module
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SourceTasDisableRunAttribute : SourceTasEventAttribute
    {
        public SourceTasDisableRunAttribute(int priority = 0) : base(priority)
        {
        }
    }
}
