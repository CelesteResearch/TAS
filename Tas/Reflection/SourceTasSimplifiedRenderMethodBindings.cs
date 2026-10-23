using System;
using System.Reflection;
using Celeste.Core.Rendering;
using Celeste.Core.Scheduling;
using Celeste.Game.Presentation.Effects.Bloom;
using Celeste.Game.Presentation.Effects.Distortion;
using Celeste.Game.Presentation.Effects.Glitch;
using Celeste.Game.Presentation.Effects.Mirrors;
using Celeste.Game.Presentation.Lighting;
using Celeste.Simulation.Gameplay.Hazards.Spinners.Dust;
using Celeste.Simulation.Presentation.Particles;
using Celeste.Graphics.Content.VirtualAssets;
using Celeste.Graphics.Rendering.Textures;

namespace Celeste.Mods.Tas.Reflection
{
    public static class SourceTasSimplifiedRenderMethodBindings
    {
        public static void Register(GameApplication process)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));
            Bind(process, 100670515, "Celeste.MirrorSurfaces", typeof(MirrorSurfacesState), "Render",
                typeof(ISourceRenderContext));
            Bind(process, 100670822, "Celeste.LightingRenderer", typeof(LightingRendererPresentation), "Render",
                typeof(object));
            Bind(process, 100675799, "Celeste.BloomRenderer", typeof(BloomRendererPresentation), "Apply",
                typeof(VirtualRenderTarget), typeof(object));
            Bind(process, 100665947, "Monocle.ParticleSystem", typeof(ParticleSystemPresentation), "Render",
                typeof(ISourceRenderContext));
            Bind(process, 100665948, "Monocle.ParticleSystem", typeof(ParticleSystemPresentation), "Render",
                typeof(ISourceRenderContext), typeof(float));
            Bind(process, 100674705, "Celeste.Glitch", typeof(OriginalGlitchRender), "Apply",
                typeof(ISourceEngineContext),
                typeof(VirtualRenderTarget), typeof(float), typeof(float), typeof(float));
            Bind(process, 100675761, "Celeste.Distort", typeof(OriginalDistortionRender), "Render",
                typeof(ISourceEngineContext), typeof(SourceTexture2DResource), typeof(SourceTexture2DResource),
                typeof(bool));
            Bind(process, 100672993, "Celeste.DustGraphic+Eyeballs", typeof(DustEyeballsState), "Render",
                typeof(ISourceRenderContext));
        }

        private static void Bind(GameApplication process, int token, string sourceType,
            Type actualType, string name, params Type[] parameters)
        {
            var catalog = process.Reflection.SourceMethods;
            string assembly = process.OriginalGameAssemblyIdentity;
            var definition = catalog.RequireDefinition(assembly, token);
            if (definition.DeclaringType != sourceType || definition.Name != name)
                throw new InvalidOperationException(
                    "The actual render target differs from its original MethodDef: " + token);
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                                       BindingFlags.Instance | BindingFlags.DeclaredOnly;
            var method = actualType.GetMethod(name, flags, null, parameters, null)
                         ?? throw new MissingMethodException(actualType.FullName, name);
            catalog.BindActualMethod(assembly, token, method, null);
        }
    }
}
