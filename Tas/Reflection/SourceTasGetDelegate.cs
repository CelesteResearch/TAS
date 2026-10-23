using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;
using Celeste.Core.Reflection.Members;

namespace Celeste.Mods.Tas.Reflection
{
    internal delegate TResult SourceTasGetDelegate<in TInstance, out TResult>(TInstance instance);
}
