using System;
using Celeste.Core.Reflection.Members;
using Celeste.Everest.Reflection;
using System.Linq;
using Celeste.Mods.Tas.Input;
using Celeste.Everest.Runtime.Input;
using Celeste.Everest.UI.Controls;
using Celeste.Scene.Tracking;
using Celeste.Framework.Diagnostics.Commands;

namespace Celeste.Mods.Tas
{
    public sealed partial class SourceTasHotkeys
    {
        private ReflectedMember GetOptionalField(string fullTypeName, string name)
        {
            var type = reflection.GetType("CelesteNet.Client", fullTypeName);
            return type == null ? null : reflection.GetFieldInfo(type, name);
        }

        private bool CelesteNetChatting
        {
            get
            {
                if (celesteNetInstance.Value?.Read(null) is not { } instance) return false;
                if (celesteNetContext.Value?.Read(instance) is not { } context) return false;
                if (celesteNetChat.Value?.Read(context) is not { } chat) return false;
                return celesteNetActive.Value?.Read(chat) as bool? == true;
            }
        }

        internal void UpdateMeta()
        {
            bool updateKey = true, updateButton = true;
            if (((ISourceCommandsProcessEnvironment)process).Commands.Open)
            {
                updateKey = false;
            }
            else
            {
                var textInput = ((IEverestTextInputEnvironment)process).TextInput;
                if (textInput.Initialized && reflection.GetFieldInfo(typeof(EverestTextInputProcess), "_OnInput")
                        ?.Read(null) is Action<char> inputEvent)
                    updateKey &= inputEvent.GetInvocationList().All(d =>
                    {
                        if (d.Target == null) return false;
                        var type = d.Target.GetType();
                        var members = ((ISourceEverestReflectionEnvironment)process).Reflection.SourceMembers;
                        string name = members.ContainsSourceType(type)
                            ? members.GetOriginalTypeFullName(type)
                            : type.FullName;
                        return name == "Celeste.Mod.ImGuiHelper.ImGuiRenderer+<>c";
                    });
            }

            if (!driver.Running)
            {
                if (CelesteNetChatting) updateKey = false;
                if (process.Frame.Scenes.CurrentScene is ISourceScene scene)
                {
                    var tracker = scene.SourceScene.Entities;
                    if (!tracker.IsEntityTracked<ModuleSettingsKeyboardConfigUI>())
                        tracker.AddTypeToTracker(typeof(ModuleSettingsKeyboardConfigUI),
                            typeof(EverestKeyboardConfigPresentation));
                    if (!tracker.IsEntityTracked<ModuleSettingsButtonConfigUI>())
                        tracker.AddTypeToTracker(typeof(ModuleSettingsButtonConfigUI),
                            typeof(EverestButtonConfigPresentation));
                    if (tracker.GetTrackedEntity<EverestKeyboardConfigPresentation>("Celeste.KeyboardConfigUI") != null)
                        updateKey = false;
                    if (tracker.GetTrackedEntity<EverestButtonConfigPresentation>("Celeste.ButtonConfigUI") != null)
                        updateButton = false;
                }
            }

            ReadPhysicalStates();
            foreach (var hotkey in AllHotkeys.Values)
            {
                if (hotkey == InfoHud) hotkey.Update();
                else if (hotkey == OpenConsole) hotkey.Update(true, updateButton);
                else hotkey.Update(updateKey, updateButton);
            }

            AfterUpdate();
        }
    }
}
