using System;

namespace Celeste.Mods.Tas.Module
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SourceTasPreEngineUpdateAttribute(int priority = 0) : SourceTasEventAttribute(priority);
}
