using System;

namespace Celeste.Mods.Tas.Module
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SourceTasUpdateMetaAttribute : SourceTasEventAttribute
    {
        public SourceTasUpdateMetaAttribute(int priority = 0) : base(priority)
        {
        }
    }
}
