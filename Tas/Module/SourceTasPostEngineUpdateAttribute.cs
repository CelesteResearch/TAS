using System;

namespace Celeste.Mods.Tas.Module
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SourceTasPostEngineUpdateAttribute(int priority = 0) : SourceTasEventAttribute(priority);
}
