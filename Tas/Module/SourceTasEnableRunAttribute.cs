using System;

namespace Celeste.Mods.Tas.Module
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SourceTasEnableRunAttribute : SourceTasEventAttribute
    {
        public SourceTasEnableRunAttribute(int priority = 0) : base(priority)
        {
        }
    }
}
