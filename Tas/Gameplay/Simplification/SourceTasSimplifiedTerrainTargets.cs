using System;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Collections;
using Celeste.Core.Geometry;
using Celeste.Core.Reflection.Methods;
using Celeste.Everest.Content.Tiles.Autotiling;
using Celeste.Game.Execution.LevelLoader.Terrain;
using Celeste.Simulation.Gameplay.Platforms.Foundation;
using Celeste.Simulation.Presentation.Backdrops.Rendering;
using Celeste.Graphics.Content.Tiles.Autotiling;

namespace Celeste.Mods.Tas.Gameplay.Simplification
{
    public static class SourceTasSimplifiedTerrainTargets
    {
        public static void Register(GameApplication process)
        {
            var methods = process.Reflection.SourceMethods;
            string assembly = process.OriginalGameAssemblyIdentity;
            var constructor = typeof(OriginalSolidTiles).GetConstructor(
                                  [typeof(Vector2), typeof(VirtualMap<char>), typeof(ISourceSolidWorld)])
                              ?? throw new MissingMethodException(typeof(OriginalSolidTiles).FullName, ".ctor");
            Require(methods, assembly, 100676704, "Celeste.SolidTiles", ".ctor", "System.Void",
                ["Microsoft.Xna.Framework.Vector2", "Monocle.VirtualMap`1<System.Char>"]);
            methods.BindActualConstructor(assembly, 100676704, constructor);

            var getTile = typeof(EverestAutotiler).GetMethod("GetTile",
                              BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly, null,
                              [
                                  typeof(VirtualMap<char>), typeof(int), typeof(int), typeof(IntRect), typeof(char),
                                  typeof(AutotileBehavior)
                              ], null)
                          ?? throw new MissingMethodException(typeof(EverestAutotiler).FullName, "GetTile");
            Require(methods, assembly, 100673617, "Celeste.Autotiler", "GetTile", "System.Char",
            [
                "Monocle.VirtualMap`1<System.Char>", "System.Int32", "System.Int32",
                "Microsoft.Xna.Framework.Rectangle",
                "System.Char", "Celeste.Autotiler+Behaviour"
            ]);
            methods.BindActualMethod(assembly, 100673617, getTile, null);

            var render = typeof(BackdropRendererPresentation).GetMethod(nameof(BackdropRendererPresentation.Render),
                             BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly, null,
                             [typeof(object)], null)
                         ?? throw new MissingMethodException(typeof(BackdropRendererPresentation).FullName, "Render");
            Require(methods, assembly, 100675820, "Celeste.BackdropRenderer", "Render", "System.Void",
                ["Monocle.Scene"]);
            methods.BindActualMethod(assembly, 100675820, render, null);
        }

        private static void Require(SourceAssemblyMethodCatalog methods, string assembly, int token,
            string type, string name, string result, string[] parameters)
        {
            var definition = methods.RequireDefinition(assembly, token);
            if (definition.DeclaringType != type || definition.Name != name ||
                definition.ReturnTypeIdentity != result ||
                !definition.ParameterTypeIdentities.SequenceEqual(parameters))
                throw new InvalidOperationException(
                    "The actual simplified terrain target does not match its qualified source MethodDef: " + type +
                    "." +
                    name);
        }
    }
}
