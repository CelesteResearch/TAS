using Celeste.Everest;
#nullable enable annotations
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MonoMod.RuntimeDetour;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Types;
using Celeste.Core.Rendering;
using Celeste.Core.Rendering.Diagnostics;
using Celeste.Scene.Entities;
using Celeste.Scene.Scenes;
using Celeste.Game.Execution.Levels;
using Celeste.Everest.Reflection;
using Celeste.Everest.Modules.Runtime;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Hitboxes
{
    public sealed partial class SourceTasCycleHitboxColorProcess
    {
        private Hook? entityDebugRenderHook;
        private Hook? sceneBeforeUpdateHook;
        private Hook? levelBeginHook;

        private SourceMemberCatalog Members =>
            ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMembers;

        [SourceTasInitialize]
        private static void SourceInitialize() => CelesteTasModule.Instance.Driver.SourceCycleHitboxColor.Initialize();

        [SourceTasLoad]
        private static void SourceLoad() => CelesteTasModule.Instance.Driver.SourceCycleHitboxColor.Load();

        [SourceTasUnload]
        private static void SourceUnload() => CelesteTasModule.Instance.Driver.SourceCycleHitboxColor.Unload();

        private void Load()
        {
            entityDebugRenderHook = new Hook(
                RequireActualMethod(typeof(SceneEntityState), "DebugRender", typeof(ISourceRenderContext),
                    typeof(object)),
                (Action<Action<SceneEntityState, ISourceRenderContext, object>, SceneEntityState, ISourceRenderContext,
                    object>)EntityOnDebugRender);
            sceneBeforeUpdateHook = new Hook(RequireActualMethod(typeof(SourceSceneState), "BeforeUpdate"),
                (Action<Action<SourceSceneState>, SourceSceneState>)SceneOnBeforeUpdate);
            levelBeginHook = new Hook(RequireActualMethod(typeof(OriginalLevelSceneRoot), "Begin"),
                (Action<Action<OriginalLevelSceneRoot>, OriginalLevelSceneRoot>)LevelOnBegin);
        }

        private void Unload()
        {
            entityDebugRenderHook!.Dispose();
            sceneBeforeUpdateHook!.Dispose();
            levelBeginHook!.Dispose();
        }

        private MethodInfo RequireActualMethod(Type type, string name, params Type[] arguments)
        {
            var methods = ((ISourceEverestReflectionEnvironment)driver.Process).Reflection.SourceMethods;
            var sourceMethod = methods.GetMethodInfo(Members, type, name, null,
                                   BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                               ?? throw new MissingMethodException(Members.GetOriginalTypeFullName(type), name);
            var method = sourceMethod.ActualMethod;
            var parameters = method.GetParameters();
            if (method.IsStatic || method.DeclaringType != type || parameters.Length != arguments.Length)
                throw new InvalidOperationException(
                    "The genuine hook target does not match its shared source method ABI.");
            for (int index = 0; index < arguments.Length; index++)
                if (parameters[index].ParameterType != arguments[index])
                    throw new InvalidOperationException(
                        "The genuine hook target has an unqualified shared source parameter ABI.");
            return method;
        }

        private Type? GetModType(string name, string originalName)
            => driver.SourceModReflection.TryGetType(name, originalName);

        private Func<object, TResult>? GetFieldGetter<TResult>(Type type, string name)
        {
            if (!Members.ContainsSourceType(type))
                return SourceCompiledFieldGetter.CreateReflected<TResult>(
                    driver.SourceModReflection.GetFieldInfo(type, name));
            var binding = Members.GetMember(type, name, SourceMemberKind.Field,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, true);
            if (binding == null)
                driver.LogSourceTas(EverestLogLevel.Error,
                    $"Failed to find field '{name}' on type '{Members.GetOriginalTypeFullName(type)}'");
            return SourceCompiledFieldGetter.Create<TResult>(binding);
        }

        private Func<object, object>? GetSourceEntityFieldGetter(Type type, string name)
        {
            if (!Members.ContainsSourceType(type))
            {
                var member = driver.SourceModReflection.GetFieldInfo(type, name);
                if (member == null) return null;
                if (!typeof(SceneEntityState).IsAssignableFrom(member.ValueType))
                    throw new InvalidCastException(member.ValueType + " cannot be assigned to " +
                                                   typeof(SceneEntityState));
                return SourceCompiledFieldGetter.CreateReflected<object>(member);
            }

            var binding = Members.GetMember(type, name, SourceMemberKind.Field,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, true);
            if (binding == null)
            {
                driver.LogSourceTas(EverestLogLevel.Error,
                    $"Failed to find field '{name}' on type '{Members.GetOriginalTypeFullName(type)}'");
                return null;
            }

            var reference = binding.Definition.ValueTypeReference;
            if (reference == null || reference.Kind != SourceTypeReferenceKind.Named)
                throw new InvalidOperationException(
                    "The original Entity getter requires qualified named reference-type facts.");
            var target = Members.GetOriginalTypeDefinition(typeof(SceneEntityState));
            var current = Members.GetOriginalTypeDefinition(reference.AssemblyIdentity, reference.TypeIdentity);
            var visited = new HashSet<string>();
            while (current != null)
            {
                if (!visited.Add(current.AssemblyIdentity + ":" + current.Identity))
                    throw new InvalidOperationException("The original Entity field has a cyclic source base chain.");
                if (current.AssemblyIdentity == target.AssemblyIdentity && current.Identity == target.Identity)
                    return SourceCompiledFieldGetter.Create<object>(binding);
                if (((TypeAttributes)current.Attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.Interface
                    || current.GenericParameters == null || current.GenericParameters.Count != 0)
                    throw new InvalidOperationException(
                        "The original Entity getter requires complete non-generic class base facts.");
                if (current.BaseTypeIdentity == null) break;
                current = Members.GetOriginalTypeDefinition(current.BaseAssemblyIdentity, current.BaseTypeIdentity);
            }

            throw new InvalidCastException(
                $"{binding.Definition.ValueTypeIdentity} cannot be assigned to the original {target.Identity}.");
        }

        private void Initialize()
        {
            Dictionary<Type, string> types = new();
            Type? frostSpinnerType = GetModType("FrostHelper", "FrostHelper.CustomSpinner");
            Func<object, bool>? noCycles = null;
            if (frostSpinnerType != null &&
                !typeof(ISourceOptionalCycleHitboxOffset).IsAssignableFrom(frostSpinnerType))
            {
                types.Add(frostSpinnerType, "offset");
                if (GetSourceEntityFieldGetter(frostSpinnerType, "controller") is { } getController
                    && GetModType("FrostHelper", "FrostHelper.CustomSpinnerController") is { } spinnerControllerType
                    && GetFieldGetter<bool>(spinnerControllerType, "NoCycles") is { } getNoCycles)
                {
                    noCycles = entity => getNoCycles(getController(entity));
                }
            }

            if (GetModType("VivHelper", "VivHelper.Entities.CustomSpinner") is { } vivSpinnerType)
                types.Add(vivSpinnerType, "offset");
            if (GetModType("VivHelper", "VivHelper.Entities.AnimatedSpinner") is { } animatedSpinnerType)
                types.Add(animatedSpinnerType, "offset");
            if (GetModType("VivHelper", "VivHelper.Entities.MovingSpinner") is { } movingSpinnerType)
                types.Add(movingSpinnerType, "offset");
            if (GetModType("ChronoHelper", "Celeste.Mod.ChronoHelper.Entities.ShatterSpinner") is { } chronoSpinnerType)
                types.Add(chronoSpinnerType, "offset");
            if (GetModType("ChronoHelper", "Celeste.Mod.ChronoHelper.Entities.DarkLightning") is { } darkLightningType)
                types.Add(darkLightningType, "toggleOffset");
            foreach (Type type in types.Keys)
            {
                if (GetFieldGetter<float>(type, types[type]) is { } offsetGetter)
                {
                    if (type == frostSpinnerType)
                    {
                        OffsetGetters[type] = entity =>
                            noCycles == null || !noCycles(entity) ? offsetGetter(entity) : null;
                    }
                    else
                    {
                        OffsetGetters[type] = entity => offsetGetter(entity);
                    }
                }
            }
        }
    }
}
