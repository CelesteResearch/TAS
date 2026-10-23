using Celeste.Everest;
using System;
using System.Collections.Generic;
using System.Linq;
using Celeste.Core.Reflection.Methods;
using Celeste.Everest.Reflection;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Mods.Tas.Reflection;

namespace Celeste.Mods.Tas.Module
{
    public sealed class SourceTasAttributeProcess
    {
        private readonly GameApplication process;
        private readonly Dictionary<Type, SourceOrderedMethod[]> attributeMethods = new();

        public SourceTasAttributeProcess(GameApplication process, Type actualModuleType)
        {
            this.process = process;
        }

        public void CollectOwnMethods<T>(params Type[] parameterTypes) where T : Attribute
            => Collect<T>(
                new[] { global::Celeste.Mods.Tas.Reflection.Metadata.SourceTasMethodMetadata.TasAssemblyIdentity },
                parameterTypes);

        public void CollectAllMethods<T>(params Type[] parameterTypes) where T : Attribute
            => Collect<T>(((ISourceEverestReflectionEnvironment)process).Reflection.GetOriginalAggregateAssemblyOrder(),
                parameterTypes);

        private void Collect<T>(IEnumerable<string> assemblies, Type[] parameterTypes) where T : Attribute
        {
            var methods = ((ISourceEverestReflectionEnvironment)process).Reflection.SourceMethods;
            string identity = OriginalAttributeIdentity(typeof(T));
            attributeMethods[typeof(T)] = methods.EnumerateDeclaredAttributedMethods(assemblies, identity)
                .Where(source =>
                {
                    var info = source.ActualMethod;
                    if (info.GetParameters().Select(param => param.ParameterType).SequenceEqual(parameterTypes))
                        return true;
                    ((IEverestLoggerProcessEnvironment)process).Logger.Log(EverestLogLevel.Error, "CelesteTAS",
                        $"Method '{info}' on type '{info.DeclaringType}' has attribute '{typeof(T)}' without matching parameter signature '({string.Join<Type>(", ", parameterTypes)})'");
                    return false;
                })
                .OrderBy(source => SourceTasOriginalAttributeData.ReadEventPriority(
                    methods.RequireDefinition(source.SourceAssemblyIdentity, source.SourceMethodDefinitionToken),
                    identity))
                .ToArray();
        }

        public void Invoke<T>(params object[] parameters) where T : Attribute
        {
            if (!attributeMethods.TryGetValue(typeof(T), out var methods)) return;
            foreach (var method in methods) method.Invoke(parameters);
        }

        private static string OriginalAttributeIdentity(Type type)
        {
            if (type == typeof(SourceTasLoadAttribute)) return "TAS.Module.LoadAttribute";
            if (type == typeof(SourceTasLoadContentAttribute)) return "TAS.Module.LoadContentAttribute";
            if (type == typeof(SourceTasUnloadAttribute)) return "TAS.Module.UnloadAttribute";
            if (type == typeof(SourceTasInitializeAttribute)) return "TAS.Module.InitializeAttribute";
            if (type == typeof(SourceTasEnableRunAttribute)) return "TAS.EnableRunAttribute";
            if (type == typeof(SourceTasDisableRunAttribute)) return "TAS.DisableRunAttribute";
            if (type == typeof(SourceTasUpdateMetaAttribute)) return "TAS.UpdateMetaAttribute";
            if (type == typeof(SourceTasClearInputsAttribute)) return "TAS.Input.ClearInputsAttribute";
            if (type == typeof(SourceTasParseFileEndAttribute)) return "TAS.Input.ParseFileEndAttribute";
            if (type == typeof(SourceTasFileChangedAttribute)) return "TAS.Input.TasFileChangedAttribute";
            if (type == typeof(SourceTasPreEngineUpdateAttribute)) return "TAS.Gameplay.Events+PreEngineUpdate";
            if (type == typeof(SourceTasPostEngineUpdateAttribute)) return "TAS.Gameplay.Events+PostEngineUpdate";
            if (type == typeof(global::Celeste.Mods.Tas.Interop.SourceTasSaveStateAttribute))
                return "TAS.ModInterop.SaveStateAttribute";
            if (type == typeof(global::Celeste.Mods.Tas.Interop.SourceTasLoadStateAttribute))
                return "TAS.ModInterop.LoadStateAttribute";
            if (type == typeof(global::Celeste.Mods.Tas.Interop.SourceTasClearStateAttribute))
                return "TAS.ModInterop.ClearStateAttribute";
            throw new InvalidOperationException("Original TAS attribute identity is not qualified: " + type.FullName);
        }
    }
}
