using System;

namespace Celeste.Mods.Tas.Module
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SourceTasEventAttribute : Attribute
    {
        internal readonly int Priority;

        public SourceTasEventAttribute(int priority)
        {
            Priority = priority;
        }
    }
}
