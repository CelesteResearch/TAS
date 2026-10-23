using Celeste.Core.Display;
using System;
using System.Numerics;
using Celeste.Core.Geometry;
using Celeste.Core.Mathematics;
using Celeste.Core.Scheduling;
using Celeste.Graphics.Content;
using Celeste.Graphics.Content.Atlas;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Celeste.Graphics.Rendering.Drawing;
using Celeste.Everest.Content.Extensions.Decals;

namespace Celeste.Mods.Tas.Gameplay.Hitboxes.Rooms
{
    public sealed partial class TasUnloadedRoomHitboxProcess
    {
        private readonly ISourceEngineContext engine;
        private readonly ITasUnloadedRoomHitboxSettingsEnvironment settingsEnvironment;
        private ITasUnloadedRoomHitboxSettings settings => settingsEnvironment.UnloadedRoomHitboxSettings;
        private readonly SourceTasEntityTypeHelper types;
        private readonly EverestDecalRegistry decals;
        private AtlasData gameAtlas => ((ISourceGameGraphicsEnvironment)engine).GameGraphics.GameAtlas;
        private ISourceDrawScope drawScope => (ISourceDrawScope)engine;

        private PackedColor EntityColorInverselyLessAlpha
        {
            get
            {
                var c = settings.EntityHitboxColor;
                return new PackedColor(255 - c.R, 255 - c.G, 255 - c.B, c.A) * .6f;
            }
        }

        public TasUnloadedRoomHitboxProcess(ISourceEngineContext engine,
            ITasUnloadedRoomHitboxSettingsEnvironment settingsEnvironment, SourceTasEntityTypeHelper types,
            EverestDecalRegistry decals)
        {
            this.engine = engine;
            this.settingsEnvironment = settingsEnvironment;
            this.types = types;
            this.decals = decals;
            IgnoreTypes = CreateIgnoreTypes();
        }

        private static bool IsSameOrSubclassOf(Type type, params Type[] types)
        {
            foreach (var other in types)
                if (type == other || type.IsSubclassOf(other))
                    return true;
            return false;
        }

        private void HollowRect(IntRect rect, PackedColor color)
            => SourcePrimitiveDrawing.HollowRect(drawScope, rect.X, rect.Y, rect.Width, rect.Height, color);

        private void HollowRect(Vector2 position, float width, float height, PackedColor color)
            => SourcePrimitiveDrawing.HollowRect(drawScope, position.X, position.Y, width, height, color);

        private void FilledRect(IntRect rect, PackedColor color)
            => SourcePrimitiveDrawing.Rect(drawScope, rect, color);

        private void Circle(Vector2 position, float radius, PackedColor color, int resolution)
            => SourcePrimitiveDrawing.Circle(drawScope, position, radius, color, resolution,
                ((ISourceVectorEnvironment)engine).VectorArithmetic);

        private void DrawCentered(AtlasEntry texture, Vector2 position)
            => SourceAtlasDrawing.DrawCentered(drawScope, texture, position, PackedColor.White);

        private void DrawCentered(AtlasEntry texture, Vector2 position, PackedColor color,
            Vector2 scale, float rotation = 0f)
            => SourceAtlasDrawing.DrawCentered(drawScope, texture, position, color, scale, rotation);

        private void DrawOutlineCentered(AtlasEntry texture, Vector2 position, PackedColor color,
            Vector2 scale, float rotation = 0f)
            => texture.DrawOutline(drawScope, position, texture.Center, color, scale, rotation);
    }
}
