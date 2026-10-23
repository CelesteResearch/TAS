#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Numerics;
using Celeste.Everest.Diagnostics.Logging;

namespace Celeste.Mods.Tas.Query
{
    public static class QueryPropertyInfoExtensions
    {
        public static bool IsStatic(this PropertyInfo source, bool nonPublic = true)
            => source.GetAccessors(nonPublic).Any(x => x.IsStatic);
    }
}
