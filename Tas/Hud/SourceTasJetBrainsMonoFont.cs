using Celeste.Core.Display;
using System;
using System.Numerics;
using Celeste.Core.Rendering;
using Celeste.Game.Content.Fonts;
using Celeste.Game.Content.Dialogs;
using Celeste.Graphics.Display;
using Celeste.Graphics.Text.BitmapFonts;

namespace Celeste.Mods.Tas.Hud
{
    public sealed partial class SourceTasJetBrainsMonoFont
    {
        private const string FontFace = "JetBrains Mono";
        private readonly GameApplication process;
        private OriginalFontRegistry fonts => ((IOriginalGameTextEnvironment)process).Text.Fonts;

        internal SourceTasJetBrainsMonoFont(GameApplication process) => this.process = process;

        public SourcePixelFont Font => fonts.Get(FontFace) ?? fonts.LoadOriginal(FontFace);
        public SourcePixelFontSize FontSize => Font.Get(BaseSize);
        public float BaseSize => 32;
        public float LineHeight => FontSize.LineHeight;

        internal void Unload() => fonts.Unload(FontFace);

        internal void BeforeLoggedProgressGoto()
        {
            if (fonts.Get(FontFace) != null)
                fonts.Unload(FontFace);
        }

        public Vector2 Measure(char text) => FontSize.Measure(text);
        public Vector2 Measure(string text) => FontSize.Measure(text);
        public float WidthToNextLine(string text, int start) => FontSize.WidthToNextLine(text, start);
        public float HeightOf(string text) => FontSize.HeightOf(text);

        public void Draw(ISourceRenderContext context, object actualScene, char character, Vector2 position,
            Vector2 justify, Vector2 scale, PackedColor color) =>
            Font.Draw(context, actualScene, BaseSize, character, position, justify, scale, color);

        private void Draw(ISourceRenderContext context, object actualScene, string text, Vector2 position,
            Vector2 justify, Vector2 scale, PackedColor color, float edgeDepth, PackedColor edgeColor,
            float stroke, PackedColor strokeColor) =>
            Font.Draw(context, actualScene, BaseSize, text, position, justify, scale, color,
                edgeDepth, edgeColor, stroke, strokeColor);

        public void Draw(ISourceRenderContext context, object actualScene, string text, Vector2 position,
            PackedColor color) =>
            Draw(context, actualScene, text, position, Vector2.Zero, Vector2.One, color,
                0f, PackedColor.Transparent, 0f, PackedColor.Transparent);

        public void Draw(ISourceRenderContext context, object actualScene, string text, Vector2 position,
            Vector2 justify, Vector2 scale, PackedColor color) =>
            Draw(context, actualScene, text, position, justify, scale, color,
                0f, PackedColor.Transparent, 0f, PackedColor.Transparent);

        public void DrawOutline(ISourceRenderContext context, object actualScene, string text, Vector2 position,
            Vector2 justify, Vector2 scale, PackedColor color, float stroke, PackedColor strokeColor) =>
            Draw(context, actualScene, text, position, justify, scale, color,
                0f, PackedColor.Transparent, stroke, strokeColor);

        public void DrawEdgeOutline(ISourceRenderContext context, object actualScene, string text, Vector2 position,
            Vector2 justify, Vector2 scale, PackedColor color, float edgeDepth, PackedColor edgeColor,
            float stroke = 0f, PackedColor strokeColor = default) =>
            Draw(context, actualScene, text, position, justify, scale, color,
                edgeDepth, edgeColor, stroke, strokeColor);
    }
}
