using Celeste.Core.Display;
using System;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Geometry;
using Celeste.Core.Rendering;
using Celeste.Everest.Content.Extensions.Decals;
using Celeste.Audio.Fmod.Events;
using Celeste.Game.Audio.Runtime;
using Celeste.Game.Entities.Decoration.Decals;
using Celeste.Game.Presentation.Effects.Dust;
using Celeste.Game.Execution.LevelLoader.Terrain;
using Celeste.Game.Execution.Levels;
using Celeste.Scene.Entities;
using Celeste.Simulation.Runtime.Session;
using Celeste.Simulation.Gameplay.Hazards.Lightning;
using Celeste.Simulation.Gameplay.Hazards.Spikes;
using Celeste.Simulation.Gameplay.Hazards.Spinners.Dust;
using Celeste.Graphics.Display;
using Celeste.Graphics.Rendering;
using Celeste.Graphics.Rendering.Drawing;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace Celeste.Mods.Tas.EverestInterop
{
    public sealed partial class SourceTasSimplifiedGraphicsFeature
    {
        private void DecalRender(
            Action<global::Celeste.Simulation.Presentation.Decals.Entities.SourceDecalEntityState, ISourceRenderContext>
                original,
            global::Celeste.Simulation.Presentation.Decals.Entities.SourceDecalEntityState self,
            ISourceRenderContext context)
        {
            if (IsSimplifiedDecal())
            {
                string decalName = self.Name.ToLower().Replace("decals/", "");
                if (!SolidDecals.Contains(decalName))
                {
                    var registry = ((IEverestDecalRegistryEnvironment)driver.Process).DecalRegistry;
                    if (!registry.RegisteredDecals.TryGetValue(decalName, out var decalInfo)) return;
                    if (decalInfo.CustomProperties.All(pair => pair.Key != "solid")) return;
                }
            }

            original(self, context);
        }

        private void BackgroundTilesRender(Action<SceneEntityState, ISourceRenderContext> original,
            SceneEntityState self, ISourceRenderContext context)
        {
            if (self is OriginalBackgroundTiles && driver.Settings.SimplifiedGraphics &&
                driver.Settings.SimplifiedBackgroundTiles) return;
            original(self, context);
        }

        private DustStylePresentation DustStylesGet(
            Func<OriginalDustStyleProvider, int, DustStylePresentation> original,
            OriginalDustStyleProvider self, int areaId)
        {
            if (driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedDustSpriteEdge)
            {
                PackedColor color = PackedColor.Transparent;
                return new DustStylePresentation
                {
                    EdgeColors = [color.ToVector3(), color.ToVector3(), color.ToVector3()],
                    EyeColor = color,
                    EyeTextures = "danger/dustcreature/eyes"
                };
            }

            return original(self, areaId);
        }

        private bool LightningRenderBlock(Func<LightningRendererState, LightningState, bool> original,
            LightningRendererState self, LightningState block) => SimplifyLightning(original(self, block), block);

        private bool LightningRenderEdges(Func<LightningRendererState, bool> original, LightningRendererState self)
        {
            bool drawEdges = original(self);
            return (!driver.Settings.SimplifiedGraphics || !driver.Settings.SimplifiedWavedEdge) && drawEdges;
        }

        private bool SimplifyLightning(bool visible, LightningState lightning)
        {
            if (driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedWavedEdge)
            {
                IntRect rectangle = new((int)lightning.X + 1, (int)lightning.Y + 1, (int)lightning.Width,
                    (int)lightning.Height);
                var context = ((ISourceRenderEnvironment)driver.Process).CurrentContext;
                ((ISourceDrawScope)context).Draw.SpriteBatch.Draw(
                    ((global::Celeste.Simulation.Rendering.ISourceLightningBufferScene)lightning.Scene).LightningBuffer
                    .Target,
                    lightning.Position + Vector2.One, rectangle, PackedColor.Yellow.PackedValue);
                if (visible)
                    SourcePrimitiveDrawing.HollowRect(context, lightning, rectangle, PackedColor.LightGoldenrodYellow);
                return false;
            }

            return visible;
        }

        private void BoltRender(ILContext il)
        {
            var cursor = new ILCursor(il);
            var start = cursor.MarkLabel();
            cursor.MoveBeforeLabels();
            cursor.EmitDelegate(
                (Func<bool>)(() => driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedWavedEdge));
            cursor.EmitBrfalse(start);
            cursor.EmitRet();
        }

        private void LevelRender(ILContext il)
        {
            var cursor = new ILCursor(il);
            var wipe = typeof(OriginalLevelSceneRoot).GetProperty(nameof(OriginalLevelSceneRoot.Wipe))?.GetMethod ??
                       throw new MissingMethodException(typeof(OriginalLevelSceneRoot).FullName, "get_Wipe");
            if (!cursor.TryGotoNext(instruction => instruction.MatchLdarg(0),
                    instruction => MatchActualMethod(instruction, wipe),
                    instruction => instruction.OpCode == OpCodes.Brfalse_S || instruction.OpCode == OpCodes.Brfalse))
                throw new InvalidOperationException("The actual Level Render no longer tests its Wipe value.");
            {
                cursor.Index += 2;
                cursor.EmitDelegate(
                    (Func<Celeste.Simulation.Presentation.Scene.Transitions.ScreenWipePresentation,
                        Celeste.Simulation.Presentation.Scene.Transitions.ScreenWipePresentation>)(value =>
                        driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedScreenWipe ? null : value));
            }
        }

        private FmodEventReference AudioPlay(Func<OriginalGameAudioSession, string, FmodEventReference> original,
            OriginalGameAudioSession self, string path)
        {
            FmodEventReference result = original(self, path);
            if (driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedLightningStrike &&
                path == "event:/new_content/game/10_farewell/lightning_strike") result?.SetVolume(0);
            return result;
        }

        private void SpikesAdded(Action<SpikesState, object> original, SpikesState self, object scene)
        {
            if (driver.Settings.SimplifiedGraphics && driver.Settings.SimplifiedSpikes)
            {
                string spikeType = ((ISourceSpikeScene)scene).SpikeType;
                string overrideType = Read<string>(self, "overrideType");
                if (!string.IsNullOrEmpty(overrideType) && !overrideType.Equals("default")) spikeType = overrideType;
                string sourceFullName = global::Celeste.Core.Reflection.Types.TypeReflectionIdentity
                    .Resolve(Members, self.GetType()).TypeIdentity;
                bool animatedSpikes = sourceFullName == "VivHelper.Entities.AnimatedSpikes" ||
                                      driver.Process.RuntimeTypeDiscovery.RuntimeTypeNameMatches(self.GetType(),
                                          "VivHelper.Entities.AnimatedSpikes");
                bool dashThroughSpikes = sourceFullName == "Celeste.Mod.NerdHelper.Entities.DashThroughSpikes" ||
                                         driver.Process.RuntimeTypeDiscovery.RuntimeTypeNameMatches(self.GetType(),
                                             "Celeste.Mod.NerdHelper.Entities.DashThroughSpikes");
                if (spikeType != "tentacles" && !animatedSpikes)
                    Write(self, "overrideType", "outline");
                if (dashThroughSpikes)
                    Write(self, "overrideType", "Kalobi/NerdHelper/dashthroughspike");
            }

            original(self, scene);
        }
    }
}
