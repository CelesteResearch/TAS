using Celeste.Core.Display;
using System.Numerics;
using Celeste.Core.Rendering;
using Celeste.Graphics.Display;
using Celeste.Physics.Collision.Shapes;
using Celeste.Scene.Entities;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Reflection;

namespace Celeste.Mods.Tas.Hitboxes
{
    internal sealed partial class SourceTasHitboxFixer
    {
        private delegate void OriginalEntityDebugRender(SceneEntityState self, ISourceRenderContext context,
            object camera);

        private delegate void OriginalColliderRender(ColliderGeometry self, object camera, PackedColor color,
            ISourceRenderContext context);

        [SourceTasModOnHook("FrostHelper", "FrostHelper.Colliders.ShapeHitbox", "Render")]
        private static void AccurateShapeHitbox(OriginalColliderRender orig, ColliderGeometry self, object camera,
            PackedColor color, ISourceRenderContext context)
        {
            var driver = CelesteTasModule.Instance.Driver;
            var fixer = driver.SourceHitboxFixer;
            if (!driver.Settings.ShowHitboxes || !fixer.DrawingHitboxes)
            {
                orig(self, camera, color, context);
                return;
            }

            var points = (Vector2[])driver.SourceModReflection.GetFieldInfo(self.GetType(), "Points").Read(self);
            var pos = self.Entity?.Position ?? default;
            for (int i = 0; i < points.Length - 1; i++)
                fixer.DrawExactLine(pos + points[i], pos + points[i + 1], color);
            if ((bool)driver.SourceModReflection.GetFieldInfo(self.GetType(), "Fill").Read(self))
                fixer.DrawExactLine(pos + points[0], pos + points[^1], color);
        }

        [SourceTasModOnHook("CrystallineHelper", "vitmod.ForceField", "DebugRender")]
        private static void AccurateForceFieldHitbox(OriginalEntityDebugRender orig, SceneEntityState self,
            ISourceRenderContext context, object camera)
        {
            var driver = CelesteTasModule.Instance.Driver;
            var fixer = driver.SourceHitboxFixer;
            if (!driver.Settings.ShowHitboxes || !fixer.DrawingHitboxes)
            {
                orig(self, context, camera);
                return;
            }

            var nodes = (Vector2[])driver.SourceModReflection.GetFieldInfo(self.GetType(), "nodes").Read(self);
            for (int i = 0; i < nodes.Length; i++)
                fixer.DrawExactLine(i > 0 ? nodes[i - 1] : self.Position, nodes[i], PackedColor.Red);
        }
    }
}
