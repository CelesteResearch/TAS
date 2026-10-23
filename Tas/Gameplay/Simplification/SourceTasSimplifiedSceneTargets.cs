using System;
using System.Reflection;
using Celeste.Core.Rendering;
using Celeste.Game.Execution.Levels;
using Celeste.Scene.Entities;
using Celeste.Simulation.Presentation.Decals.Entities;

namespace Celeste.Mods.Tas.Gameplay.Simplification
{
    public static class SourceTasSimplifiedSceneTargets
    {
        public static void Register(GameApplication process)
        {
            Bind(process, 100675998, "Celeste.Level", typeof(OriginalLevelSceneRoot), "Update", Type.EmptyTypes);
            Bind(process, 100676000, "Celeste.Level", typeof(OriginalLevelSceneRoot), "Render", Type.EmptyTypes);
            Bind(process, 100665285, "Monocle.Entity", typeof(SceneEntityState), "Render",
                [typeof(ISourceRenderContext)]);
            Bind(process, 100673667, "Celeste.Decal", typeof(SourceDecalEntityState), "Render",
                [typeof(ISourceRenderContext)]);
        }

        private static void Bind(GameApplication process, int token, string sourceType,
            Type actualType, string name, Type[] parameters)
        {
            var methods = process.Reflection.SourceMethods;
            string assembly = process.OriginalGameAssemblyIdentity;
            var definition = methods.RequireDefinition(assembly, token);
            if (definition.DeclaringType != sourceType || definition.Name != name ||
                definition.ReturnTypeIdentity != "System.Void" || definition.ParameterTypeIdentities.Count != 0 ||
                ((MethodAttributes)definition.Attributes & MethodAttributes.Static) != 0)
                throw new InvalidOperationException(
                    "The simplified scene hook target differs from its original MethodDef: " + sourceType + "." + name);
            var method = actualType.GetMethod(name,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly,
                null, parameters, null) ?? throw new MissingMethodException(actualType.FullName, name);
            methods.BindActualMethod(assembly, token, method, null);
        }
    }
}
