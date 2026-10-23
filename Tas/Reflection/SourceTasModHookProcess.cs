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
    internal sealed class SourceTasModHookProcess
    {
        private const string TasAssembly =
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        private readonly TasPlaybackDriver driver;

        internal SourceTasModHookProcess(TasPlaybackDriver driver)
        {
            this.driver = driver;
        }

        internal void InitializeOnHooks()
        {
            var reflection = ((ISourceEverestReflectionEnvironment)driver.Process).Reflection;
            foreach (var definition in reflection.SourceMethods.EnumerateDeclaredAttributedDefinitions(
                         new[] { TasAssembly },
                         "TAS.Utils.ModOnHook"))
            {
                var attribute = Read(definition, "TAS.Utils.ModOnHook");
                var targetType = driver.SourceModReflection.TryGetType(attribute.Mod, attribute.Type);
                if (targetType == null || !HasTargetMethod(targetType, attribute.Method)) continue;
                var info = reflection.SourceMethods.RequireActualOrderedMethod(definition.AssemblyIdentity,
                    definition.MethodDefinitionToken);
                var parameterTypes = info.ActualMethod.GetParameters().Skip(2).Select(p => p.ParameterType).ToArray();
                var target =
                    driver.SourceModReflection.TryGetMethod(attribute.Mod, attribute.Type, attribute.Method,
                        parameterTypes);
                if (target == null) continue;
                // Original handlers are static; physical signature/owner must retain
                // an actual compatible delegate rather than invoking via DynamicInvoke.
                if (!info.ActualMethod.IsStatic)
                    throw new InvalidOperationException("ModOnHook requires a qualified actual static hook target.");
                driver.SourceHooks.OnHook(target.ActualMethod, info.ActualMethod);
            }
        }

        internal void InitializeILHooks()
        {
            var reflection = ((ISourceEverestReflectionEnvironment)driver.Process).Reflection;
            foreach (var definition in reflection.SourceMethods.EnumerateDeclaredAttributedDefinitions(
                         new[] { TasAssembly },
                         "TAS.Utils.ModILHook"))
            {
                var info = reflection.SourceMethods.RequireActualOrderedMethod(definition.AssemblyIdentity,
                    definition.MethodDefinitionToken);
                var actualAttribute = info.ActualMethod.GetCustomAttribute<SourceTasModILHookAttribute>();
                var attribute = actualAttribute == null
                    ? Read(definition, "TAS.Utils.ModILHook")
                    : (Mod: actualAttribute.Mod, Type: actualAttribute.Type, Method: actualAttribute.Method);
                var target = driver.SourceModReflection.TryGetMethod(attribute.Mod, attribute.Type, attribute.Method);
                if (target == null) continue;
                var parameters = info.ActualMethod.GetParameters();
                switch (parameters.Length)
                {
                    case 1 when parameters[0].ParameterType == typeof(ILContext):
                        driver.SourceHooks.IlHook(target.ActualMethod, il => info.Invoke(new object[] { il })); break;
                    case 1 when parameters[0].ParameterType == typeof(ILCursor):
                        driver.SourceHooks.IlHook(target.ActualMethod,
                            il => info.Invoke(new object[] { new ILCursor(il) })); break;
                    case 2 when parameters[0].ParameterType == typeof(ILContext) &&
                                parameters[1].ParameterType == typeof(ILCursor):
                        driver.SourceHooks.IlHook(target.ActualMethod,
                            il => info.Invoke(new object[] { il, new ILCursor(il) })); break;
                    default: throw new ArgumentException("Invalid signature for IL-hook: " + info.ActualMethod);
                }
            }
        }

        private bool HasTargetMethod(Type actualType, string name)
        {
            var reflection = ((ISourceEverestReflectionEnvironment)driver.Process).Reflection;
            var members = reflection.SourceMembers;
            for (var current = actualType; current != null && current != typeof(object); current = current.BaseType)
            {
                if (!members.ContainsSourceType(current))
                {
                    if (current.GetMethods(SourceTasModReflection.StaticInstanceAnyVisibility |
                                           BindingFlags.DeclaredOnly)
                        .Any(method => method.Name == name)) return true;
                    continue;
                }

                for (var type = members.GetOriginalTypeDefinition(current);
                     type != null && (members.ContainsSourceType(actualType) || type.Identity != "System.Object");
                     type = members.GetOriginalBaseTypeDefinition(type))
                    if (reflection.SourceMethods.EnumerateDeclaredDefinitions(type.AssemblyIdentity, type.Identity)
                        .Any(method => method.Name == name))
                        return true;
                return false;
            }

            return false;
        }

        private static (string Mod, string Type, string Method) Read(SourceMethodDefinition definition,
            string attribute)
        {
            var hex = definition.RequireDeclaredAttributeBlob(attribute);
            if ((hex.Length & 1) != 0) throw new FormatException("Invalid ModHook custom attribute length.");
            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = byte.Parse(hex.Substring(i * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            using var stream = new MemoryStream(bytes, false);
            using var reader = new BinaryReader(stream, Encoding.UTF8);
            if (reader.ReadUInt16() != 1) throw new FormatException("Invalid ModHook attribute prolog.");
            var result = (ReadString(reader), ReadString(reader), ReadString(reader));
            if (reader.ReadUInt16() != 0 || stream.Position != stream.Length)
                throw new FormatException("Unexpected ModHook named arguments.");
            return result;
        }

        private static string ReadString(BinaryReader reader)
        {
            byte leading = reader.ReadByte();
            if (leading == 0xff) return null;
            int length;
            if ((leading & 0x80) == 0) length = leading;
            else if ((leading & 0xc0) == 0x80) length = ((leading & 0x3f) << 8) | reader.ReadByte();
            else if ((leading & 0xe0) == 0xc0)
                length = ((leading & 0x1f) << 24) | (reader.ReadByte() << 16) | (reader.ReadByte() << 8) |
                         reader.ReadByte();
            else throw new FormatException("Invalid serialized ModHook string length.");
            var bytes = reader.ReadBytes(length);
            if (bytes.Length != length) throw new EndOfStreamException();
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
