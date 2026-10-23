using System;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Geometry;
using Celeste.Core.Rendering;
using Celeste.Core.Mathematics;
using Celeste.Game.Presentation.PostProcessing.ColorGrading;
using Celeste.Game.Presentation.Effects.Bloom;
using Celeste.Game.Execution.LevelLoader.Terrain;
using Celeste.Graphics.Content.Atlas;
using Celeste.Graphics.Content.Tiles;
using Celeste.Graphics.Content.Tiles.Autotiling;
using Celeste.Graphics.Rendering.Primitives;
using Celeste.Graphics.Rendering.Effects;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace Celeste.Mods.Tas.EverestInterop
{
    public sealed partial class SourceTasSimplifiedGraphicsFeature
    {
        private static bool MatchActualField(Instruction instruction, FieldInfo field) =>
            instruction.OpCode == OpCodes.Ldfld && instruction.Operand is FieldReference reference &&
            reference.Name == field.Name &&
            reference.DeclaringType.FullName == field.DeclaringType.FullName.Replace('+', '/');

        private static bool MatchActualMethod(Instruction instruction, MethodInfo method) =>
            (instruction.OpCode == OpCodes.Call || instruction.OpCode == OpCodes.Callvirt) &&
            instruction.Operand is MethodReference reference && reference.Name == method.Name &&
            reference.DeclaringType.FullName == method.DeclaringType.FullName.Replace('+', '/') &&
            reference.Parameters.Count == method.GetParameters().Length;

        private void LightingRendererRender(ILContext il)
        {
            var cursor = new ILCursor(il);
            var clamp = typeof(ScalarMath).GetMethod("Clamp", new[] { typeof(float), typeof(float), typeof(float) }) ??
                        throw new MissingMethodException(typeof(ScalarMath).FullName, "Clamp");
            if (!cursor.TryGotoNext(MoveType.After, instruction => MatchActualMethod(instruction, clamp)))
                throw new InvalidOperationException("The actual LightingRenderer Render no longer clamps its Alpha.");
            cursor.EmitDelegate((Func<float, float>)(alpha =>
                driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedLighting != null
                    ? (10 - driver.Settings.SimplifiedLighting.Value) / 10f
                    : alpha));
        }

        private void ColorGradeSet(Action<OriginalColorGradeProcess, AtlasEntry, AtlasEntry, float> original,
            OriginalColorGradeProcess self, AtlasEntry fromTex, AtlasEntry toTex, float p)
        {
            bool? origEnabled = null;
            if (driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedColorGrade)
            {
                origEnabled = self.Enabled;
                self.Enabled = false;
            }

            original(self, fromTex, toTex, p);
            if (origEnabled.HasValue) self.Enabled = origEnabled.Value;
        }

        private void BloomRendererApply(ILContext il)
        {
            var cursor = new ILCursor(il);
            var baseField = typeof(BloomRendererPresentation).GetField(nameof(BloomRendererPresentation.Base)) ??
                            throw new MissingFieldException(typeof(BloomRendererPresentation).FullName, "Base");
            var strengthField =
                typeof(BloomRendererPresentation).GetField(nameof(BloomRendererPresentation.Strength)) ??
                throw new MissingFieldException(typeof(BloomRendererPresentation).FullName, "Strength");
            int baseReads = 0;
            while (cursor.TryGotoNext(MoveType.After, instruction => instruction.MatchLdarg0(),
                       instruction => MatchActualField(instruction, baseField)))
            {
                cursor.EmitDelegate((Func<float, float>)(value =>
                    driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedBloomBase.HasValue
                        ? driver.Settings.SimplifiedBloomBase.Value / 10f
                        : value));
                baseReads++;
            }

            if (baseReads == 0) throw new InvalidOperationException("The actual Bloom Apply no longer reads Base.");
            cursor.Index = 0;
            int strengthReads = 0;
            while (cursor.TryGotoNext(MoveType.After, instruction => instruction.MatchLdarg0(),
                       instruction => MatchActualField(instruction, strengthField)))
            {
                cursor.EmitDelegate((Func<float, float>)(value =>
                    driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedBloomStrength.HasValue
                        ? driver.Settings.SimplifiedBloomStrength.Value / 10f
                        : value));
                strengthReads++;
            }

            if (strengthReads == 0)
                throw new InvalidOperationException("The actual Bloom Apply no longer reads Strength.");
        }

        private void SolidTilesCtor(Action<OriginalSolidTiles, Vector2, Celeste.Core.Collections.VirtualMap<char>,
                Celeste.Simulation.Gameplay.Platforms.Foundation.ISourceSolidWorld> original,
            OriginalSolidTiles self, Vector2 position, Celeste.Core.Collections.VirtualMap<char> data,
            Celeste.Simulation.Gameplay.Platforms.Foundation.ISourceSolidWorld world)
        {
            SolidTilesConstructing();
            original(self, position, data, world);
            SolidTilesConstructed();
        }

        private void AutotilerGetTile(ILContext il)
        {
            var cursor = new ILCursor(il);
            int returns = 0;
            while (cursor.TryGotoNext(MoveType.Before, instruction => instruction.MatchRet()))
            {
                cursor.EmitDelegate((Func<char, char>)(tile =>
                {
                    if (creatingSolidTiles && driver.Settings.SimplifiedGraphics &&
                        driver.Settings.SimplifiedSolidTilesStyle != default &&
                        !default(char).Equals(tile) &&
                        tile != '0') return driver.Settings.SimplifiedSolidTilesStyle.Value;
                    return tile;
                }));
                cursor.Index++;
                returns++;
            }

            if (returns == 0)
                throw new InvalidOperationException("The actual Autotiler GetTile no longer has a char return.");
        }

        private void BackdropRendererRender(ILContext il)
        {
            var cursor = new ILCursor(il);
            var start = cursor.MarkLabel();
            cursor.MoveBeforeLabels();
            cursor.EmitDelegate((Func<bool>)(() =>
                !driver.Settings.SimplifiedGraphics || !driver.Settings.SimplifiedBackdrop));
            cursor.EmitBrtrue(start);
            cursor.EmitRet();
        }

        private void DistortRender(ILContext il)
        {
            var cursor = new ILCursor(il);
            var effectField =
                typeof(Celeste.Game.Execution.Graphics.OriginalGameGraphicsResources).GetField(
                    nameof(Celeste.Game.Execution.Graphics.OriginalGameGraphicsResources.FxDistort)) ??
                throw new MissingFieldException(
                    typeof(Celeste.Game.Execution.Graphics.OriginalGameGraphicsResources).FullName, "FxDistort");
            if (!cursor.TryGotoNext(MoveType.After, instruction => MatchActualField(instruction, effectField)))
                throw new InvalidOperationException("The actual Distort Render no longer reads FxDistort.");
            cursor.EmitDelegate((Func<SourceEffect, SourceEffect>)(effect =>
                driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedDistort ? null : effect));
        }

        private void ModTileGlitcher(ILCursor cursor, ILContext il)
        {
            if (!cursor.TryGotoNext(instruction => instruction.OpCode == OpCodes.Callvirt &&
                                                   instruction.Operand is MethodReference method &&
                                                   method.Name == "set_Item" &&
                                                   method.DeclaringType.FullName == typeof(AtlasEntry).FullName))
                return;
            if (!cursor.TryFindPrev(out var cursors,
                    instruction => instruction.MatchLdarg0(), instruction => instruction.OpCode == OpCodes.Ldfld,
                    instruction => instruction.MatchLdarg0(), instruction => instruction.OpCode == OpCodes.Ldfld,
                    instruction => instruction.MatchLdarg0(),
                    instruction => instruction.OpCode == OpCodes.Ldfld)) return;
            for (int i = 0; i < 6; i++)
            {
                cursor.Emit(cursors[0].Next.OpCode, cursors[0].Next.Operand);
                cursors[0].Index++;
            }

            cursor.EmitDelegate(
                (Func<AtlasEntry, Celeste.Core.Collections.VirtualMap<AtlasEntry>, int, int, AtlasEntry>)((newTexture,
                    fgTexes, x, y) =>
                {
                    if (driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedSolidTilesStyle != default)
                    {
                        if (fgTexes[x, y] is { } texture && newTexture != null) return texture;
                    }

                    return newTexture;
                }));
        }
    }
}
