using Celeste.Core.Display;
using System.Numerics;
using Celeste.Core.Rendering;
using Celeste.Core.Mathematics.Easing;
using Celeste.Game.Content.Dialogs;
using Celeste.Graphics.Display;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasAreaCompleteInfo
    {
        internal void DrawText(ISourceRenderContext context, object owner, float ease)
        {
            if (string.IsNullOrEmpty(text))
                return;
            ((IOriginalGameTextEnvironment)process).Text.ActiveFont.DrawOutline(context, owner, text,
                new Vector2(10f, 10f),
                new Vector2(1f - ((ISourceEasingEnvironment)process).EasingArithmetic.CubeOut(ease), 0f),
                Vector2.One * .5f, PackedColor.White, 2f, PackedColor.Black);
        }
    }
}
