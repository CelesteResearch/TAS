using Celeste.Core.Display;
using System;
using System.Numerics;
using Celeste.Core.Rendering;
using Celeste.Core.Mathematics;
using Celeste.Core.Mathematics.Extrema;
using Celeste.Core.Mathematics.Easing;
using Celeste.Everest.UI.Settings;
using Celeste.Simulation.UI.Menus.Text;
using Celeste.Game.Execution.Engine;
using Celeste.Graphics.Content.Atlas;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using global::Celeste.Game.Execution;

namespace Celeste.Mods.Tas.Module
{
    public class SourceTasEaseInSubMenu : EverestSubMenu
    {
        private readonly AtlasEntry icon;
        private float alpha;
        private float ease;
        private readonly TasPlaybackDriver driver;
        private float unEasedAlpha;

        public SourceTasEaseInSubMenu(TextMenuResources resources, TasPlaybackDriver driver, string label,
            bool enterOnSelect) : base(resources, label, enterOnSelect)
        {
            this.driver = driver;
            alpha = unEasedAlpha = driver.Settings.Enabled ? 1f : 0f;
            FadeVisible = Visible = driver.Settings.Enabled;
            icon = resources.Gui("downarrow");
        }

        public bool FadeVisible { get; set; }

        public override float Height() => ScalarMath.Lerp(-Container.ItemSpacing, base.Height(), alpha,
            ((ISourceScalarEnvironment)ResourceSet.EngineContext).ScalarArithmetic);

        public override void Update()
        {
            ease = ScalarMath.Approach(ease, Focused ? 1f : 0f, ResourceSet.EngineContext.RawDeltaTime * 4f,
                ((ISourceExtremaEnvironment)ResourceSet.EngineContext).ExtremaArithmetic);
            base.Update();

            float targetAlpha = FadeVisible ? 1 : 0;
            if (Math.Abs(unEasedAlpha - targetAlpha) > 0.001f)
            {
                unEasedAlpha = ScalarMath.Approach(unEasedAlpha, targetAlpha,
                    ResourceSet.EngineContext.RawDeltaTime * 3f,
                    ((ISourceExtremaEnvironment)ResourceSet.EngineContext).ExtremaArithmetic);
                alpha = FadeVisible
                    ? ((ISourceEasingEnvironment)ResourceSet.EngineContext).EasingArithmetic.SineOut(unEasedAlpha)
                    : ((ISourceEasingEnvironment)ResourceSet.EngineContext).EasingArithmetic.SineIn(unEasedAlpha);
            }

            Visible = alpha != 0;
        }

        public override void Render(ISourceRenderContext context, Vector2 position, bool highlighted)
        {
            Vector2 top = new(position.X, position.Y - (Height() / 2));

            float currentAlpha = Container.Alpha * alpha;
            PackedColor color = Disabled
                ? PackedColor.DarkSlateGray
                : ((highlighted ? Container.HighlightColor : PackedColor.White) * currentAlpha);
            PackedColor strokeColor = PackedColor.Black * (currentAlpha * currentAlpha * currentAlpha);

            bool unCentered = Container.InnerContent == TextMenuPresentation.InnerContentMode.TwoColumn &&
                              !AlwaysCenter;

            Vector2 titlePosition = top + (Vector2.UnitY * TitleHeight / 2) +
                                    (unCentered ? Vector2.Zero : new Vector2(Container.Width * 0.5f, 0f));
            Vector2 justify = unCentered ? new Vector2(0f, 0.5f) : new Vector2(0.5f, 0.5f);
            Vector2 iconJustify = unCentered
                ? new Vector2(ActiveFont.Measure(Label).X + icon.FrameWidth, 5f)
                : new Vector2(ActiveFont.Measure(Label).X / 2 + icon.FrameWidth, 5f);
            DrawIcon(context, titlePosition, iconJustify, true, Items.Count < 1 ? PackedColor.DarkSlateGray : color,
                alpha);
            ActiveFont.DrawOutline(context, Container, Label, titlePosition, justify, Vector2.One, color, 2f,
                strokeColor);

            if (Focused && ease > 0.9f)
            {
                Vector2 menuPosition = new(top.X + ItemIndent, top.Y + TitleHeight + ItemSpacing);
                RecalculateSize();
                foreach (TextMenuPresentation.Item item in Items)
                {
                    if (item.Visible)
                    {
                        float height = item.Height();
                        Vector2 itemPosition =
                            menuPosition + new Vector2(0f, height * 0.5f + item.SelectWiggler.Value * 8f);
                        if (itemPosition.Y + height * 0.5f > 0f &&
                            itemPosition.Y - height * 0.5f < EngineRuntime.Height)
                        {
                            item.Render(context, itemPosition, Focused && Current == item);
                        }

                        menuPosition.Y += height + ItemSpacing;
                    }
                }
            }
        }

        private void DrawIcon(ISourceRenderContext context, Vector2 position, Vector2 justify, bool outline,
            PackedColor color, float scale)
        {
            if (outline)
            {
                SourceAtlasDrawing.DrawOutline(context, Container, icon, position + justify, icon.Center, color, scale);
            }
            else
            {
                SourceAtlasDrawing.DrawCentered(context, Container, icon, position + justify, color, scale);
            }
        }
    }
}
