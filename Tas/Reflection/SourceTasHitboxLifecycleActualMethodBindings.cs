using Celeste.Core.Display;
using System;
using System.Reflection;
using System.Linq;
using Celeste.Core.Reflection.Methods;
using Celeste.Core.Rendering;
using Celeste.Core.Scheduling;
using Celeste.Game.Presentation.Rendering;
using Celeste.Game.Execution.Levels;
using Celeste.Physics.Collision.Shapes;
using Celeste.Scene.Following;
using Celeste.Scene.Scenes;
using Celeste.Scene.Tracking;
using Celeste.Simulation;
using Celeste.Graphics.Display;

namespace Celeste.Mods.Tas.Reflection
{
    public static class SourceTasHitboxLifecycleActualMethodBindings
    {
        private const string GameAssembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        private const BindingFlags DeclaredInstance = BindingFlags.Public | BindingFlags.NonPublic |
                                                      BindingFlags.Instance | BindingFlags.DeclaredOnly;

        public static void Register(GameApplication process)
        {
            var methods = process.Reflection.SourceMethods;
            Bind(methods, 100676325, "Celeste.Player", "Update", typeof(RoomPlayerSession), Type.EmptyTypes);
            Bind(methods, 100665914, "Monocle.EntityList", "DebugRender", typeof(SceneEntityRegistry),
                new[] { typeof(ISourceRenderContext), typeof(object), typeof(object) });
            Bind(methods, 100665909, "Monocle.EntityList", "Update", typeof(SceneEntityRegistry),
                new[] { typeof(ISourceEngineContext) });
            var colliderRender = new[] { typeof(object), typeof(PackedColor), typeof(ISourceRenderContext) };
            Bind(methods, 100664664, "Monocle.Circle", "Render", typeof(CircleColliderGeometry), colliderRender);
            Bind(methods, 100664791, "Monocle.ColliderList", "Render", typeof(ColliderListGeometry), colliderRender);
            Bind(methods, 100675978, "Celeste.Level", "End", typeof(OriginalLevelSceneRoot), Type.EmptyTypes);
            Bind(methods, 100664833, "Monocle.Grid", "Render", typeof(GridColliderGeometry), colliderRender);
            Bind(methods, 100675133, "Celeste.Follower", "Update", typeof(FollowerState), Type.EmptyTypes);
            Bind(methods, 100666002, "Monocle.Scene", "BeforeUpdate", typeof(SourceSceneState), Type.EmptyTypes);
            Bind(methods, 100675977, "Celeste.Level", "Begin", typeof(OriginalLevelSceneRoot), Type.EmptyTypes);
            Bind(methods, 100674701, "Celeste.GameplayRenderer", "Render", typeof(GameplayRendererState),
                new[] { typeof(object) });
        }

        public static void RegisterModCompatibility(GameApplication process)
        {
            var methods = process.Reflection.SourceMethods;
            BindModCompatibility(methods, 100665074, "AccurateShapeHitbox", "OriginalColliderRender",
                new[]
                {
                    "TAS.EverestInterop.Hitboxes.HitboxFixer+orig_Collider_Render", "Monocle.Collider",
                    "Monocle.Camera", "Microsoft.Xna.Framework.Color"
                },
                new[] { typeof(ColliderGeometry), typeof(object), typeof(PackedColor), typeof(ISourceRenderContext) });
            BindModCompatibility(methods, 100665075, "AccurateForceFieldHitbox", "OriginalEntityDebugRender",
                new[]
                {
                    "TAS.EverestInterop.Hitboxes.HitboxFixer+orig_Entity_DebugRender", "Monocle.Entity",
                    "Monocle.Camera"
                },
                new[]
                {
                    typeof(Celeste.Scene.Entities.SceneEntityState), typeof(ISourceRenderContext), typeof(object)
                });
        }

        private static void BindModCompatibility(SourceAssemblyMethodCatalog methods, int token, string name,
            string delegateName, string[] sourceParameters, Type[] payloadParameters)
        {
            const string assembly = "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            var source = methods.RequireDefinition(assembly, token);
            if (source.DeclaringType != "TAS.EverestInterop.Hitboxes.HitboxFixer" ||
                source.TypeDefinitionToken != 33554619 ||
                source.Name != name || source.ReturnTypeIdentity != "System.Void" ||
                source.GenericParameterCount != 0 ||
                source.Attributes != 145 || !source.ParameterTypeIdentities.SequenceEqual(sourceParameters) ||
                !source.HasDeclaredAttribute("TAS.Utils.ModOnHook"))
                throw new InvalidOperationException("The genuine TAS hitbox compatibility MethodDef differs: " + token);
            var actualType =
                typeof(TasRuntimeEnvironment).Assembly.GetType("Celeste.Mods.Tas.Hitboxes.SourceTasHitboxFixer",
                    true);
            var originalDelegate = actualType.GetNestedType(delegateName, BindingFlags.NonPublic)
                                   ?? throw new MissingMemberException(actualType.FullName, delegateName);
            var parameters = new[] { originalDelegate }.Concat(payloadParameters).ToArray();
            var actual = actualType.GetMethod(name,
                BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly,
                null, parameters, null) ?? throw new MissingMethodException(actualType.FullName, name);
            if (actual.ReturnType != typeof(void) || actual.ContainsGenericParameters || actual.GetMethodBody() == null)
                throw new InvalidOperationException("The actual TAS hitbox compatibility body differs: " + token);
            methods.BindActualMethod(assembly, token, actual, null);
        }

        private static void Bind(SourceAssemblyMethodCatalog methods, int sourceToken,
            string sourceType, string sourceName, Type actualType, Type[] actualParameters)
        {
            var definition = methods.RequireDefinition(GameAssembly, sourceToken);
            if (definition.DeclaringType != sourceType || definition.Name != sourceName ||
                (definition.Attributes & (int)MethodAttributes.Static) != 0)
                throw new InvalidOperationException("The genuine hitbox lifecycle method identity does not match: " +
                                                    sourceToken);
            var actualMethod = actualType.GetMethod(sourceName, DeclaredInstance, null, actualParameters, null)
                               ?? throw new MissingMethodException(actualType.FullName, sourceName);
            if (actualMethod.IsStatic || actualMethod.ReturnType != typeof(void))
                throw new InvalidOperationException(
                    "The genuine hitbox lifecycle target is not an instance void body: " +
                    sourceToken);
            methods.BindActualMethod(GameAssembly, sourceToken, actualMethod, null);
        }
    }
}
