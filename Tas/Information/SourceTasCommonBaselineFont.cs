using Celeste.Core.Display;
using System;
using System.Numerics;
using System.Xml;
using MonoMod.Utils;
using Celeste.Core.Rendering;
using Celeste.Core.Serialization.Xml;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Celeste.Graphics.Text.BitmapFonts;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Information
{
    public static class SourceTasCommonBaselineFont
    {
        private const string AttrBaseline = "CelesteTAS_baseline";

        [SourceTasLoad]
        private static void Load() => TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process)
            .InstallSourceTasFontBaselineHook();

        [SourceTasUnload]
        private static void Unload() => TasRuntimeEnvironment.Get(CelesteTasModule.Instance.Process)
            .UninstallSourceTasFontBaselineHook();

        internal static void AfterSourceFontSizeAdded(SourcePixelFontSize size, XmlElement data)
            => DynamicData.For(size).Set(AttrBaseline, data["common"].AttrInt("base"));

        /// Draws the specified string while keeping a common baseline at different scales
        public static void DrawCommonBaseline(this SourcePixelFont pixelFont, ISourceRenderContext context,
            object actualOwner, string text, float baseSize, Vector2 position, Vector2 justify, Vector2 scale,
            PackedColor color, float edgeDepth, PackedColor edgeColor, float stroke, PackedColor strokeColor)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            var pixelFontSize = pixelFont.Get((int)Math.Round(baseSize * Math.Max(scale.X, scale.Y)));
            var pixelFontSizeData = DynamicData.For(pixelFontSize);

            // Rescale based on font pixel size
            scale *= baseSize / pixelFontSize.Size;

            float lineWidth = justify.X != 0.0f ? pixelFontSize.WidthToNextLine(text, 0) : 0.0f;
            int baseline = pixelFontSizeData.Get<int>(AttrBaseline);

            var offset = Vector2.Zero;
            var justified = new Vector2(lineWidth * justify.X, pixelFontSize.HeightOf(text) * justify.Y);

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\n')
                {
                    offset.X = 0.0f;
                    offset.Y += pixelFontSize.LineHeight;
                    if (justify.X != 0.0f)
                    {
                        justified.X = pixelFontSize.WidthToNextLine(text, i + 1) * justify.X;
                    }

                    continue;
                }

                if (!pixelFontSize.Characters.TryGetValue(text[i], out var c))
                {
                    continue;
                }

                // Properly align every scale with a common baseline
                var pos = position
                          + (offset + justified) * scale
                          + new Vector2(c.XOffset * scale.X,
                              c.YOffset - baseline + (baseline - c.YOffset) * (1.0f - scale.Y));

                if (stroke > 0.0f && !pixelFontSize.Outline)
                {
                    if (edgeDepth > 0.0f)
                    {
                        SourceAtlasDrawing.Draw(context, actualOwner, c.Texture, pos + new Vector2(0.0f, 0.0f - stroke),
                            Vector2.Zero, strokeColor, scale);
                        for (float j = 0f - stroke; j < edgeDepth + stroke; j += stroke)
                        {
                            SourceAtlasDrawing.Draw(context, actualOwner, c.Texture,
                                pos + new Vector2(0.0f - stroke, j),
                                Vector2.Zero, strokeColor, scale);
                            SourceAtlasDrawing.Draw(context, actualOwner, c.Texture, pos + new Vector2(stroke, j),
                                Vector2.Zero, strokeColor, scale);
                        }

                        SourceAtlasDrawing.Draw(context, actualOwner, c.Texture,
                            pos + new Vector2(0.0f - stroke, edgeDepth + stroke), Vector2.Zero, strokeColor, scale);
                        SourceAtlasDrawing.Draw(context, actualOwner, c.Texture,
                            pos + new Vector2(0.0f, edgeDepth + stroke), Vector2.Zero, strokeColor, scale);
                        SourceAtlasDrawing.Draw(context, actualOwner, c.Texture,
                            pos + new Vector2(stroke, edgeDepth + stroke), Vector2.Zero, strokeColor, scale);
                    }
                    else
                    {
                        SourceAtlasDrawing.Draw(context, actualOwner, c.Texture,
                            pos + new Vector2(-1.0f, -1.0f) * stroke,
                            Vector2.Zero, strokeColor, scale);
                        SourceAtlasDrawing.Draw(context, actualOwner, c.Texture,
                            pos + new Vector2(0.0f, -1.0f) * stroke,
                            Vector2.Zero, strokeColor, scale);
                        SourceAtlasDrawing.Draw(context, actualOwner, c.Texture,
                            pos + new Vector2(1.0f, -1.0f) * stroke,
                            Vector2.Zero, strokeColor, scale);
                        SourceAtlasDrawing.Draw(context, actualOwner, c.Texture,
                            pos + new Vector2(-1.0f, 0.0f) * stroke,
                            Vector2.Zero, strokeColor, scale);
                        SourceAtlasDrawing.Draw(context, actualOwner, c.Texture, pos + new Vector2(1.0f, 0.0f) * stroke,
                            Vector2.Zero, strokeColor, scale);
                        SourceAtlasDrawing.Draw(context, actualOwner, c.Texture,
                            pos + new Vector2(-1.0f, 1.0f) * stroke,
                            Vector2.Zero, strokeColor, scale);
                        SourceAtlasDrawing.Draw(context, actualOwner, c.Texture, pos + new Vector2(0.0f, 1.0f) * stroke,
                            Vector2.Zero, strokeColor, scale);
                        SourceAtlasDrawing.Draw(context, actualOwner, c.Texture, pos + new Vector2(1.0f, 1.0f) * stroke,
                            Vector2.Zero, strokeColor, scale);
                    }
                }

                if (edgeDepth > 0f)
                {
                    SourceAtlasDrawing.Draw(context, actualOwner, c.Texture, pos + Vector2.UnitY * edgeDepth,
                        Vector2.Zero,
                        edgeColor, scale);
                }

                SourceAtlasDrawing.Draw(context, actualOwner, c.Texture, pos, Vector2.Zero, color, scale);

                offset.X += c.XAdvance;
                if (i < text.Length - 1 && c.Kerning.TryGetValue(text[i + 1], out int kerning))
                {
                    offset.X += kerning;
                }
            }
        }
    }
}
