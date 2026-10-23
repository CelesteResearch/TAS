using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Globalization;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Celeste.Core.Reflection.Methods;
using Celeste.Everest.Reflection;

namespace Celeste.Mods.Tas.Reflection
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class SourceTasModILHookAttribute : Attribute
    {
        public readonly string Mod, Type, Method;

        public SourceTasModILHookAttribute(string mod, string type, string method)
        {
            Mod = mod;
            Type = type;
            Method = method;
        }
    }
}
