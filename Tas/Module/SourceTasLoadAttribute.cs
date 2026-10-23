using System;

namespace Celeste.Mods.Tas.Module
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class SourceTasLoadAttribute : SourceTasEventAttribute
    {
        public SourceTasLoadAttribute(int priority = 0) : base(priority)
        {
        }
    }
}
