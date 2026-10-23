using System;
using System.Linq;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Celeste.Everest.Reflection;
using Celeste.Core.Reflection.Methods;
using Celeste.Mods.Tas.Reflection;

namespace Celeste.Mods.Tas.Gameplay.Optimization
{
    internal sealed class SourceTasOptimizationMethods
    {
        private const string GameAssembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        private readonly TasPlaybackDriver driver;

        internal SourceTasOptimizationMethods(TasPlaybackDriver driver)
        {
            this.driver = driver;
        }

        private SourceAssemblyMethodCatalog Methods =>
            ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMethods;

        internal SourceMethodDefinition[] Declared(string type) =>
            Methods.EnumerateDeclaredDefinitions(GameAssembly, type).ToArray();

        internal MethodInfo Actual(SourceMethodDefinition definition) =>
            Methods.RequireActualMethod(definition.AssemblyIdentity, definition.MethodDefinitionToken);

        internal MethodInfo Require(string type, string name, params string[] parameterTypes)
        {
            var definitions = Declared(type)
                .Where(d => d.Name == name && d.ParameterTypeIdentities.SequenceEqual(parameterTypes)).ToArray();
            if (definitions.Length != 1)
                throw new MissingMethodException("Expected one original source method: " + type + "." + name);
            return Actual(definitions[0]);
        }

        internal MethodInfo Optional(string type, string name)
        {
            var definitions = Declared(type).Where(d => d.Name == name).ToArray();
            if (definitions.Length == 0) return null;
            if (definitions.Length != 1) throw new AmbiguousMatchException(type + "." + name);
            return Actual(definitions[0]);
        }

        internal bool Calls(Instruction instruction, MethodBase actual)
        {
            if (instruction.OpCode.Code != Code.Call && instruction.OpCode.Code != Code.Callvirt) return false;
            if (instruction.Operand is not MethodReference reference) return false;
            if (actual == null) throw new ArgumentNullException(nameof(actual));
            var metadata = ((ISourceTasHookModuleMetadataEnvironment)TasRuntimeEnvironment.Get(driver.Process))
                .HookModuleMetadata;
            var actualDefinition = metadata.ResolveDefinition(actual);
            // Resolve the real IL operand through the authenticated physical module reader,
            // not the temporary DMD module's unconfigured default resolver.
            var definition = actualDefinition.Module.MetadataResolver.Resolve(reference)
                             ?? throw new MissingMethodException(
                                 "The physical IL method reference cannot be resolved: " +
                                 reference.FullName);
            return definition.MetadataToken.ToInt32() == actual.MetadataToken &&
                   definition.Module.Mvid == actual.Module.ModuleVersionId;
        }
    }
}
