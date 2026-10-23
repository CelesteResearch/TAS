using Celeste.Core.Display;
using System;
using System.Reflection;
using Celeste.Core.Reflection.Methods;
using Celeste.Core.Rendering;
using Celeste.Game.Entities.Birds;
using Celeste.Physics.Collision.Shapes;
using Celeste.Scene.Entities;
using Celeste.Scene.Scheduling;
using Celeste.Simulation;
using Celeste.Graphics.Display;

namespace Celeste.Mods.Tas.Reflection
{
    public static class SourceTasHitboxActualMethodBindings
    {
        private const string GameAssembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        private const BindingFlags DeclaredInstance = BindingFlags.Public | BindingFlags.NonPublic |
                                                      BindingFlags.Instance | BindingFlags.DeclaredOnly;

        public static void Register(GameApplication process)
        {
            var methods = process.Reflection.SourceMethods;
            Bind(methods, 100665892, "Monocle.EntityList", "UpdateLists",
                typeof(SceneUpdateScheduler), nameof(SceneUpdateScheduler.CommitQueuedEntityLists), Type.EmptyTypes);
            Bind(methods, 100665286, "Monocle.Entity", "DebugRender",
                typeof(SceneEntityState), nameof(SceneEntityState.DebugRender),
                new[] { typeof(ISourceRenderContext), typeof(object) });
            Bind(methods, 100664858, "Monocle.Hitbox", "Render",
                typeof(HitboxColliderGeometry), nameof(HitboxColliderGeometry.Render),
                new[] { typeof(object), typeof(PackedColor), typeof(ISourceRenderContext) });
            Bind(methods, 100675330, "Celeste.BirdNPC", "DebugRender",
                typeof(BirdNpcState), nameof(BirdNpcState.DebugRender),
                new[] { typeof(ISourceRenderContext), typeof(object) });
            Bind(methods, 100676324, "Celeste.Player", "DebugRender",
                typeof(RoomPlayerSession), nameof(RoomPlayerSession.DebugRender),
                new[] { typeof(ISourceRenderContext), typeof(object) });
        }

        private static void Bind(SourceAssemblyMethodCatalog methods, int sourceToken,
            string sourceType, string sourceName, Type actualType, string actualName, Type[] actualParameters)
        {
            var definition = methods.RequireDefinition(GameAssembly, sourceToken);
            if (definition.DeclaringType != sourceType || definition.Name != sourceName ||
                (definition.Attributes & (int)MethodAttributes.Static) != 0)
                throw new InvalidOperationException("The genuine source hitbox method identity does not match: " +
                                                    sourceToken);
            var actualMethod = actualType.GetMethod(actualName, DeclaredInstance, null, actualParameters, null)
                               ?? throw new MissingMethodException(actualType.FullName, actualName);
            if (actualMethod.IsStatic || actualMethod.ReturnType != typeof(void))
                throw new InvalidOperationException(
                    "The genuine source hitbox method requires an instance void body: " +
                    sourceToken);
            methods.BindActualMethod(GameAssembly, sourceToken, actualMethod, null);
        }
    }
}
