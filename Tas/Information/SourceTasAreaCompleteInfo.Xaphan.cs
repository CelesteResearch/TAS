using System;
using System.Linq;
using System.Reflection;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Celeste.Core.Rendering;
using Celeste.Everest.Reflection;
using Celeste.Simulation.Runtime.Session;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasAreaCompleteInfo
    {
        [SourceTasInitialize]
        private static void Initialize() => CelesteTasModule.Instance.Driver.AreaCompleteInfo.InitializeXaphan();

        private void InitializeXaphan()
        {
            if (driver.SourceModReflection.TryGetType("XaphanHelper",
                    "Celeste.Mod.XaphanHelper.UI_Elements.CustomEndScreen")
                is not { } customEndScreen)
                return;

            var reflection = ((ISourceEverestReflectionEnvironment)process).Reflection;
            var type = reflection.SourceMembers.GetOriginalTypeDefinition(customEndScreen);
            var definitions = reflection.SourceMethods
                .EnumerateDeclaredDefinitions(type.AssemblyIdentity, type.Identity)
                .ToArray();
            var infoDefinitions = definitions.Where(definition => definition.Name == "Info").ToArray();
            if (infoDefinitions.Length > 1)
                throw new AmbiguousMatchException("Ambiguous original CustomEndScreen.Info method.");
            if (infoDefinitions.Length == 1)
            {
                var info = reflection.SourceMethods.RequireActualMethod(type.AssemblyIdentity,
                    infoDefinitions[0].MethodDefinitionToken);
                driver.SourceHooks.OnHook(info,
                    (Action<Action<float, string, string, string, string>, float, string, string, string, string>)
                    CustomEndScreenInfo);
            }

            var constructorDefinition = definitions.First(definition => definition.Name == ".ctor"
                                                                        && ((MethodAttributes)definition.Attributes &
                                                                            MethodAttributes.MemberAccessMask) ==
                                                                        MethodAttributes.Public
                                                                        && ((MethodAttributes)definition.Attributes &
                                                                            MethodAttributes.Static) == 0);
            var constructor = (ConstructorInfo)reflection.SourceMethods.RequireActualMethodBase(type.AssemblyIdentity,
                constructorDefinition.MethodDefinitionToken);
            var parameters = constructor.GetParameters();
            if (parameters.Length == 0 || parameters[0].ParameterType != typeof(ChapterCompletionSession))
                throw new InvalidOperationException(
                    "The original Xaphan end screen session constructor argument has no qualified actual source adapter.");
            driver.SourceHooks.IlHook(constructor, CustomEndScreenCtor);
        }

        private void CustomEndScreenCtor(ILCursor cursor, ILContext context)
            => cursor.Emit(OpCodes.Ldarg_1).EmitDelegate<Action<ChapterCompletionSession>>(InitText);

        private void CustomEndScreenInfo(Action<float, string, string, string, string> original, float ease,
            string chapterTime, string fileTime, string chapterText, string version)
        {
            original(ease, chapterTime, fileTime, chapterText, version);
            DrawText(((ISourceRenderEnvironment)process).CurrentContext, this, ease);
        }
    }
}
