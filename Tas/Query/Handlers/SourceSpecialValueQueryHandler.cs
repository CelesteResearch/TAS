using Celeste.Input.Runtime;
using Celeste.Input.Devices.Keyboard;
using Celeste.Input.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using StudioCommunication;
using Keys = Celeste.Input.Devices.Keyboard.SourceKeys;
using MouseButtons = Celeste.Everest.Input.SourceEverestMouseData.MouseButtons;
using ButtonBinding = Celeste.Everest.Input.Bindings.EverestButtonBinding;
using VirtualButton = Celeste.Input.Bindings.SourceVirtualButton;
using NodeButton = Celeste.Everest.Input.SourceEverestVirtualButton;
using TargetQuery = Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess;

namespace Celeste.Mods.Tas.Query.Handlers
{
    internal class SourceSpecialValueQueryHandler : TargetQuery.Handler
    {
        public SourceSpecialValueQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
        }

        /// Data-class to hold parsed ButtonBinding/VirtualButton data, before it being set
        private class ButtonBindingData
        {
            public readonly HashSet<Keys> KeyboardKeys = [];
            public readonly HashSet<MouseButtons> MouseButtons = [];
        }

        public override bool CanResolveValue(Type type) => type == typeof(Vector2) || type == typeof(Vector3) ||
                                                           type == typeof(Vector4) ||
                                                           type ==
                                                           typeof(global::Celeste.Core.Randomness.ISourceRandom) ||
                                                           type == typeof(ButtonBinding) ||
                                                           type == typeof(VirtualButton);

        public override bool CanEnumerateTypeEntries(Type type) =>
            type == typeof(ButtonBinding) || type == typeof(VirtualButton);

        public override Result<bool, TargetQuery.MemberAccessError> SetMember(object? instance, object? value,
            Type type,
            int memberIdx, string[] memberArgs, bool forceAllowCodeExecution)
        {
            if (Query.Driver.EnforceLegal.EnabledWhenRunning && !forceAllowCodeExecution)
            {
                return Result<bool, TargetQuery.MemberAccessError>.Fail(
                    new TargetQuery.MemberAccessError.CodeExecutionNotAllowed(type, memberArgs.Length - 1, memberArgs));
            }

            var bindingFlags = memberArgs.Length == 1
                ? instance is Type
                    ? QueryReflection.StaticAnyVisibility
                    : QueryReflection.StaticInstanceAnyVisibility
                : QueryReflection.InstanceAnyVisibility;


            VirtualButton button;
            if (Query.GetOriginalField(type, memberArgs[memberIdx], bindingFlags, logFailure: false) is { } field)
            {
                if (field.ValueType == typeof(ButtonBinding))
                {
                    var binding = (ButtonBinding)field.Read(instance)!;
                    button = binding.Button;
                }
                else if (field.ValueType == typeof(VirtualButton))
                {
                    button = (VirtualButton)field.Read(instance)!;
                }
                else
                {
                    return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
                }
            }
            else if (Query.GetOriginalProperty(type, memberArgs[memberIdx], bindingFlags, logFailure: false) is
                     { } property)
            {
                if (property.ValueType == typeof(ButtonBinding))
                {
                    var binding = (ButtonBinding)property.Read(instance)!;
                    button = binding.Button;
                }
                else if (property.ValueType == typeof(VirtualButton))
                {
                    button = (VirtualButton)property.Read(instance)!;
                }
                else
                {
                    return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
                }
            }
            else
            {
                return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
            }

            var nodes = ((NodeButton)button).Nodes;
            var mouseButtons = (button.Binding as Celeste.Everest.Input.InstalledSourceInputBinding)?.Mouse;
            var mInput = ((Celeste.Input.Runtime.ISourceMInputEnvironment)Query.Engine).MInput;
            var data = (ButtonBindingData)value!;

            if (data.KeyboardKeys.Count != 0)
            {
                nodes.RemoveAll(node => node is NodeButton.KeyboardKey);
                nodes.AddRange(data.KeyboardKeys.Select(key => new NodeButton.KeyboardKey(mInput, key)));
            }

            if (data.MouseButtons.Count != 0)
            {
                nodes.RemoveAll(node =>
                    node is NodeButton.MouseLeftButton or NodeButton.MouseMiddleButton or NodeButton.MouseRightButton);

                if (mouseButtons != null)
                {
                    mouseButtons.Clear();
                    mouseButtons.AddRange(data.MouseButtons);
                }
                else
                {
                    foreach (var mouseButton in data.MouseButtons)
                    {
                        switch (mouseButton)
                        {
                            case MouseButtons.Left:
                                nodes.AddRange(data.KeyboardKeys.Select(_ => new NodeButton.MouseLeftButton(mInput)));
                                break;
                            case MouseButtons.Right:
                                nodes.AddRange(data.KeyboardKeys.Select(_ => new NodeButton.MouseRightButton(mInput)));
                                break;
                            case MouseButtons.Middle:
                                nodes.AddRange(data.KeyboardKeys.Select(_ => new NodeButton.MouseMiddleButton(mInput)));
                                break;
                            case MouseButtons.XButton1 or MouseButtons.XButton2:
                                return Result<bool, TargetQuery.MemberAccessError>.Fail(
                                    new TargetQuery.MemberAccessError.Custom(type, memberIdx,
                                        "X1 and X2 are not supported before Everest adding mouse support"));
                        }
                    }
                }
            }

            return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
        }

        public override Result<bool, TargetQuery.QueryError> ResolveValue(Type targetType, ref int argIdx,
            string[] valueArgs, out object? value)
        {
            if (targetType == typeof(Vector2))
            {
                float x = float.Parse(valueArgs[argIdx]);
                argIdx++;
                float y = float.Parse(valueArgs[argIdx]);

                value = new Vector2(x, y);
                return Result<bool, TargetQuery.QueryError>.Ok(true);
            }

            if (targetType == typeof(Vector3))
            {
                float x = float.Parse(valueArgs[argIdx]);
                argIdx++;
                float y = float.Parse(valueArgs[argIdx]);
                argIdx++;
                float z = float.Parse(valueArgs[argIdx]);

                value = new Vector3(x, y, z);
                return Result<bool, TargetQuery.QueryError>.Ok(true);
            }

            if (targetType == typeof(Vector4))
            {
                float x = float.Parse(valueArgs[argIdx]);
                argIdx++;
                float y = float.Parse(valueArgs[argIdx]);
                argIdx++;
                float z = float.Parse(valueArgs[argIdx]);
                argIdx++;
                float w = float.Parse(valueArgs[argIdx]);

                value = new Vector4(x, y, z, w);
                return Result<bool, TargetQuery.QueryError>.Ok(true);
            }

            if (targetType == typeof(global::Celeste.Core.Randomness.ISourceRandom))
            {
                value = ((global::Celeste.Core.Randomness.ISourceRandomProcessEnvironment)Query.Driver.Process)
                    .RandomProcess.Factory.CreateSeeded(int.Parse(valueArgs[argIdx]));
                return Result<bool, TargetQuery.QueryError>.Ok(true);
            }

            if (targetType == typeof(ButtonBinding) || targetType == typeof(VirtualButton))
            {
                var data = new ButtonBindingData();

                // Parse all possible keys
                int j = argIdx;
                for (; j < valueArgs.Length; j++)
                {
                    // Parse mouse first, so Mouse.Left is not parsed as Keys.Left
                    if (Enum.TryParse<MouseButtons>(valueArgs[j], ignoreCase: true, out var button))
                    {
                        data.MouseButtons.Add(button);
                    }
                    else if (Enum.TryParse<Keys>(valueArgs[j], ignoreCase: true, out var key))
                    {
                        data.KeyboardKeys.Add(key);
                    }
                    else
                    {
                        if (j == argIdx)
                        {
                            value = null;
                            return Result<bool, TargetQuery.QueryError>.Fail(
                                new TargetQuery.QueryError.Custom(
                                    $"'{valueArgs[j]}' is not a valid keyboard key or mouse button"));
                        }

                        break;
                    }
                }

                argIdx = j - 1;

                value = data;
                return Result<bool, TargetQuery.QueryError>.Ok(true);
            }


            value = null;
            return Result<bool, TargetQuery.QueryError>.Ok(false);
        }

        public override IEnumerator<CommandAutoCompleteEntry> EnumerateTypeEntries(Type type)
        {
            if (type == typeof(ButtonBinding) || type == typeof(VirtualButton))
            {
                foreach (var key in (Keys[])Enum.GetValues(typeof(Keys)))
                {
                    if (key is < Keys.A or > Keys.Z)
                    {
                        // Only ASCII alphabet is allowed
                        continue;
                    }

                    yield return new CommandAutoCompleteEntry
                    {
                        Name = key.ToString(),
                        Extra = "Key",
                        IsDone = true,
                        StorageKey = $"{nameof(TargetQuery.Variant.Get)}_{typeof(Keys).FullName}",
                    };
                }

                foreach (var button in (MouseButtons[])Enum.GetValues(typeof(MouseButtons)))
                {
                    yield return new CommandAutoCompleteEntry
                    {
                        Name = button.ToString(),
                        Extra = "Mouse",
                        IsDone = true,
                        StorageKey =
                            $"{nameof(TargetQuery.Variant.Get)}_{typeof(MouseButtons).FullName}",
                    };
                }
            }
        }
    }
}
