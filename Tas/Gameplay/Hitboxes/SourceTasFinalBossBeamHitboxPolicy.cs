using Celeste.Core.Display;
using Celeste.Core.Mathematics;
using Celeste.Core.Scheduling;
using Celeste.Graphics.Display;
using Celeste.Game.Gameplay.Enemies.BadelineBoss.Projectiles;

namespace Celeste.Mods.Tas.Gameplay.Hitboxes
{
    using Celeste.Graphics.Rendering.Geometry;

    public static class SourceTasFinalBossBeamHitboxPolicy
    {
        public static bool IsCollidable(FinalBossBeamState beam, ISourceEngineContext engine)
            => beam.ChargeTimer <= 0.0f && beam.ActiveTimer - engine.DeltaTime > 0.0f;

        public static void Draw(FinalBossBeamState beam, ISourceEngineContext engine,
            bool? actualCollidable, float unCollidableAlpha, IPixelRectangleOutput output)
        {
            var color = beam.FollowTimer > 0.0f
                ? new PackedColor(153, 50, 204, 255)
                : new PackedColor(218, 165, 32, 255);
            bool collidable = actualCollidable ?? IsCollidable(beam, engine);
            color = collidable ? color : color * unCollidableAlpha;
            var from = beam.Boss.BeamOrigin + Vector2Geometry.AngleToVector(beam.Angle, 12f);
            var to = beam.Boss.BeamOrigin + Vector2Geometry.AngleToVector(beam.Angle, 2000f);
            var arithmetic = ((ISourceVectorEnvironment)engine).VectorArithmetic;
            var perp = arithmetic.SafeNormalize(Vector2Geometry.Perpendicular(to - from), 2f);
            ExactPixelLine.DrawExactLine(from + perp, to + perp, color, output);
            ExactPixelLine.DrawExactLine(from - perp, to - perp, color, output);
            ExactPixelLine.DrawExactLine(from, to, color, output);
        }
    }
}
