using System;
using System.Linq;
using System.Reflection;
using Celeste.Core.Rendering;
using Celeste.Everest.Presentation.Hud.CoreMessages;
using Celeste.Everest.Presentation.Hud.Height;
using Celeste.Game.Components.Talking;
using Celeste.Game.Entities.Memorials;
using Celeste.Game.Presentation.Hud.CoreMessages;
using Celeste.Game.Presentation.Hud.Height;
using Celeste.Game.UI.Dialogue.MiniTextboxes;
using Celeste.Simulation.Presentation.Hud.Tutorials.Birds;

namespace Celeste.Mods.Tas.EverestInterop
{
    public static class SourceTasSimplifiedHudActualMethodBindings
    {
        public static void Register(GameApplication process)
        {
            BindRender(process, "Celeste.MiniTextbox", typeof(MiniTextboxState));
            BindRender(process, "Celeste.HeightDisplay", typeof(OriginalHeightDisplayState));
            BindRender(process, "Celeste.Mod.Entities.CustomHeightDisplay", typeof(CustomHeightDisplayState));
            BindRender(process, "Celeste.TalkComponent+TalkComponentUI", typeof(OriginalTalkUiEntity));
            BindRender(process, "Celeste.BirdTutorialGui", typeof(BirdTutorialGuiState));
            BindRender(process, "Celeste.CoreMessage", typeof(CoreMessageState));
            BindRender(process, "Celeste.Mod.Entities.CustomCoreMessage", typeof(CustomCoreMessageState));
            BindRender(process, "Celeste.MemorialText", typeof(MemorialTextState));
        }

        private static void BindRender(GameApplication process, string sourceTypeIdentity, Type actualType)
        {
            var sourceType = process.SourceMembers.GetOriginalTypeDefinition(actualType);
            var methods = process.Reflection.SourceMethods;
            var sourceMethod = methods.EnumerateDeclaredDefinitions(sourceType.AssemblyIdentity, sourceTypeIdentity)
                .Single(method => method.Name == "Render" && method.ParameterTypeIdentities.Count == 0
                                                          && (method.Attributes & (int)MethodAttributes.Static) == 0);
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
            var actualMethod = actualType.GetMethod("Render", flags, null, [typeof(ISourceRenderContext)], null)
                               ?? throw new MissingMethodException(actualType.FullName, "Render");
            if (actualMethod.ReturnType != typeof(void))
                throw new InvalidOperationException("The qualified HUD render target must return void: " +
                                                    actualMethod);
            methods.BindActualMethod(sourceType.AssemblyIdentity, sourceMethod.MethodDefinitionToken, actualMethod,
                null);
        }
    }
}
