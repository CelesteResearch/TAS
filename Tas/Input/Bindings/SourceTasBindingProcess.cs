using Celeste.Input.Devices.Keyboard;
using Celeste.Input.Devices.GamePads;
using Celeste.Input.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Reflection;
using Celeste.Everest.Input.Bindings;
using Celeste.Game.Settings;
using Celeste.Input.Gameplay;
using Keys = Celeste.Input.Devices.Keyboard.SourceKeys;
using Buttons = Celeste.Input.Devices.GamePads.SourceGamePadButtonFlags;

namespace Celeste.Mods.Tas.Input.Bindings
{
    public sealed class SourceTasBindingProcess
    {
        private readonly ISourceTasBindingEnvironment process;
        private bool? origControllerHasFocus;
        private bool? origKbTextInput;
        private bool? origAttached;
        private CrouchDashMode? origCrouchDashMode;
        private GrabMode? origGrabMode;
        private Func<SettingsState, object> cloneSettings;

        private readonly ConcurrentDictionary<(Type Type, BindingFlags Flags), IEnumerable<FieldInfo>>
            cachedAllFieldInfos =
                new ConcurrentDictionary<(Type, BindingFlags), IEnumerable<FieldInfo>>();

        private readonly ConcurrentDictionary<(Type Type, BindingFlags Flags), IEnumerable<PropertyInfo>>
            cachedAllPropertyInfos = new ConcurrentDictionary<(Type, BindingFlags), IEnumerable<PropertyInfo>>();

        public SourceTasBindingProcess(ISourceTasBindingEnvironment process)
        {
            this.process = process;
        }

        private SettingsState ShallowClone(SettingsState settings)
        {
            if (cloneSettings == null)
            {
                MethodInfo method =
                    typeof(SettingsState).GetMethod("MemberwiseClone",
                        BindingFlags.Instance | BindingFlags.NonPublic);
                cloneSettings =
                    (Func<SettingsState, object>)method.CreateDelegate(typeof(Func<SettingsState, object>));
            }

            return (SettingsState)cloneSettings(settings);
        }

        private IEnumerable<FieldInfo> GetAllFieldInfos(Type type)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                 BindingFlags.DeclaredOnly;
            var key = (type, flags);
            if (cachedAllFieldInfos.TryGetValue(key, out var result)) return result;
            var all = new HashSet<FieldInfo>();
            Type current = type;
            while (current != null && current.IsSubclassOf(typeof(object)))
            {
                foreach (FieldInfo field in current.GetFields(flags)) all.Add(field);
                current = current.BaseType;
            }

            return cachedAllFieldInfos[key] = all;
        }

        private IEnumerable<PropertyInfo> GetAllPropertyInfos(Type type)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                 BindingFlags.DeclaredOnly;
            var key = (type, flags);
            if (cachedAllPropertyInfos.TryGetValue(key, out var result)) return result;
            var all = new HashSet<PropertyInfo>();
            Type current = type;
            while (current != null && current.IsSubclassOf(typeof(object)))
            {
                foreach (PropertyInfo property in current.GetProperties(flags)) all.Add(property);
                current = current.BaseType;
            }

            return cachedAllPropertyInfos[key] = all;
        }

        private void CopyAllFields(object to, object from, bool onlyDifferent = false)
        {
            if (to.GetType() != from.GetType()) throw new ArgumentException("object to and from must be the same type");
            foreach (FieldInfo field in GetAllFieldInfos(to.GetType()))
            {
                object value = field.GetValue(from);
                if (onlyDifferent && value == field.GetValue(to)) continue;
                field.SetValue(to, value);
            }
        }

        private void ClearModsBindings()
        {
            foreach (var module in process.Modules)
            {
                Type settingsType = module.SettingsType;
                var settings = module._Settings;
                if (settingsType != null && settings != null &&
                    !process.ActualTasSettingsType.IsInstanceOfType(settings))
                {
                    foreach (PropertyInfo property in GetAllPropertyInfos(settingsType))
                    {
                        if (property.GetGetMethod(true) != null && property.GetSetMethod(true) != null &&
                            property.PropertyType == typeof(EverestButtonBinding) &&
                            property.GetValue(settings) is EverestButtonBinding binding && binding.Button != null)
                            binding.Button.Binding = process.InputBindings.Create();
                    }
                }
            }
        }

        private void SetBinding(string fieldName, params Buttons[] buttons)
        {
            SourceInputBinding binding = process.InputBindings.Create();
            binding.Add(buttons);
            process.CurrentSettings.GetType()
                .GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .SetValue(process.CurrentSettings, binding);
        }

        private void SetBinding(string fieldName, Keys[] keys, params Buttons[] buttons)
        {
            SourceInputBinding binding = process.InputBindings.Create();
            binding.Add(keys);
            binding.Add(buttons);
            process.CurrentSettings.GetType()
                .GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .SetValue(process.CurrentSettings, binding);
        }

        public void SetTasBindings()
        {
            SettingsState settingsBackup = ShallowClone(process.CurrentSettings);
            SetBinding("Left", Buttons.LeftThumbstickLeft, Buttons.DPadLeft);
            SetBinding("Right", Buttons.LeftThumbstickRight, Buttons.DPadRight);
            SetBinding("Down", Buttons.LeftThumbstickDown, Buttons.DPadDown);
            SetBinding("Up", Buttons.LeftThumbstickUp, Buttons.DPadUp);
            SetBinding("MenuLeft", Buttons.LeftThumbstickLeft, Buttons.DPadLeft);
            SetBinding("MenuRight", Buttons.LeftThumbstickRight, Buttons.DPadRight);
            SetBinding("MenuDown", Buttons.LeftThumbstickDown, Buttons.DPadDown);
            SetBinding("MenuUp", Buttons.LeftThumbstickUp, Buttons.DPadUp);
            SetBinding("Grab", Buttons.LeftStick, Buttons.Back);
            SetBinding("Jump", Buttons.A, Buttons.Y);
            SetBinding("Dash", Buttons.B, Buttons.X);
            SetBinding("Talk", Buttons.B, Buttons.LeftTrigger);
            SetBinding("Pause", Buttons.Start);
            SetBinding("Confirm", new Keys[] { Keys.NumPad0 }, Buttons.A);
            SetBinding("Cancel", Buttons.B, Buttons.X);
            SetBinding("Journal", Buttons.LeftTrigger);
            SetBinding("QuickRestart", Buttons.LeftShoulder);
            SetBinding("DemoDash", Buttons.RightShoulder, Buttons.RightStick);
            SetBinding("LeftDashOnly", Buttons.RightThumbstickLeft);
            SetBinding("RightDashOnly", Buttons.RightThumbstickRight);
            SetBinding("UpDashOnly", Buttons.RightThumbstickUp);
            SetBinding("DownDashOnly", Buttons.RightThumbstickDown);
            SetBinding("LeftMoveOnly", new Keys[] { Keys.Left });
            SetBinding("RightMoveOnly", new Keys[] { Keys.Right });
            SetBinding("UpMoveOnly", new Keys[] { Keys.Up });
            SetBinding("DownMoveOnly", new Keys[] { Keys.Down });
            process.InputSource.Initialize();
            ClearModsBindings();
            origControllerHasFocus = process.MInput.ControllerHasFocus;
            process.MInput.ControllerHasFocus = true;
            process.CoreModule.OnInputDeregister();
            process.SpeedrunToolInputImports.TryInputDeregister();
            CopyAllFields(process.CurrentSettings, settingsBackup);
            process.MInput.Active = true;
            process.MInput.Disabled = false;
            origKbTextInput = process.CoreSettings.UseKeyboardForTextInput;
            process.CoreSettings.UseKeyboardForTextInput = false;
            origAttached = process.MInput.GamePads[process.InputSource.Gamepad].Attached;
            process.MInput.GamePads[process.InputSource.Gamepad].Attached = true;
            origCrouchDashMode = process.CurrentSettings.CrouchDashMode;
            process.CurrentSettings.CrouchDashMode = CrouchDashMode.Press;
            origGrabMode = process.CurrentSettings.GrabMode;
            process.CurrentSettings.GrabMode = GrabMode.Hold;
        }

        public void RestorePlayerBindings()
        {
            if (origKbTextInput.HasValue)
            {
                process.InputSource.Initialize();
                process.CoreSettings.UseKeyboardForTextInput = origKbTextInput.Value;
                process.MInput.GamePads[process.InputSource.Gamepad].Attached = origAttached.Value;
                origKbTextInput = null;
                origAttached = null;
            }

            if (origControllerHasFocus.HasValue) process.MInput.ControllerHasFocus = origControllerHasFocus.Value;
            origControllerHasFocus = null;
            if (origCrouchDashMode.HasValue) process.CurrentSettings.CrouchDashMode = origCrouchDashMode.Value;
            if (origGrabMode.HasValue) process.CurrentSettings.GrabMode = origGrabMode.Value;
            origCrouchDashMode = null;
            origGrabMode = null;
        }

        public void SaveState(Dictionary<string, object> data)
        {
            data[nameof(SettingsState.CrouchDashMode)] = process.CurrentSettings.CrouchDashMode;
            data[nameof(SettingsState.GrabMode)] = process.CurrentSettings.GrabMode;
        }

        public void LoadState(Dictionary<string, object> data)
        {
            process.CurrentSettings.CrouchDashMode = (CrouchDashMode)data[nameof(SettingsState.CrouchDashMode)];
            process.CurrentSettings.GrabMode = (GrabMode)data[nameof(SettingsState.GrabMode)];
        }
    }
}
