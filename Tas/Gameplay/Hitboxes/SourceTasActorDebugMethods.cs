using System;
using System.Linq;
using System.Reflection;
using System.Numerics;
using MonoMod.RuntimeDetour;
using Celeste.Core.Rendering;
using Celeste.Core.Mathematics;
using Celeste.Core.Reflection.Members;
using Celeste.Scene.Entities;
using Celeste.Scene.Tracking;
using Celeste.Simulation;
using Celeste.Physics.Collision.Shapes;
using Celeste.Game.Entities.Birds;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Gameplay.Enemies.BadelineBoss.Projectiles;
using Celeste.Graphics.Rendering.Geometry;
using Celeste.Mods.Tas.Gameplay.Hitboxes;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;

namespace Celeste.Mods.Tas.Gameplay.Hitboxes
{
    internal static class SourceTasActorDebugMethods
    {
        internal const string GameAssembly = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        internal static MethodInfo Require(GameApplication process, string sourceType, string sourceMethod)
        {
            var definitions = process.Reflection.SourceMethods.EnumerateDeclaredDefinitions(GameAssembly, sourceType);
            var definition = definitions.Single(d => d.Name == sourceMethod);
            return process.Reflection.SourceMethods.RequireActualMethod(GameAssembly, definition.MethodDefinitionToken);
        }

        internal static Type RequireType(GameApplication process, string sourceType)
            => process.SourceMembers.GetActualSourceType(GameAssembly, sourceType)
               ?? throw new TypeLoadException(sourceType);
    }
}
