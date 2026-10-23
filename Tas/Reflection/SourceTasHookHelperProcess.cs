using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;
using System.Linq;
using Celeste.Mods.Tas.Module;
using Celeste.Everest.Diagnostics.Logging;
using MonoMod.Core.Platforms;

namespace Celeste.Mods.Tas.Reflection
{
    /// Helper class for registering and automatically unregistering (IL)-hooks
    internal sealed partial class SourceTasHookHelperProcess
    {
        private readonly GameApplication process;

        internal SourceTasHookHelperProcess(GameApplication process)
        {
            this.process = process;
        }

        private readonly HashSet<MethodBase> inliningDisabledMethods = new();

        private bool TryDisableInlining(MethodBase method)
        {
            if (inliningDisabledMethods.Contains(method)) return true;
            if (PlatformTriple.Current.TryDisableInlining(method))
            {
                inliningDisabledMethods.Add(method);
                return true;
            }

            return false;
        }

        private void Log(global::Celeste.Everest.EverestLogLevel level, string message) =>
            ((IEverestLoggerProcessEnvironment)process).Logger.Log(level, "CelesteTAS", message);

        internal void RegisterOnHook(Hook hook) => onHooks.Add(hook);
        internal void RegisterIlHook(ILHook hook) => ilHooks.Add(hook);
        private readonly List<Hook> onHooks = [];
        private readonly List<ILHook> ilHooks = [];

        internal void Unload()
        {
            foreach (var hook in onHooks)
            {
                hook.Dispose();
            }

            foreach (var hook in ilHooks)
            {
                hook.Dispose();
            }

            onHooks.Clear();
            ilHooks.Clear();
        }

        /// Creates an On-hook to the specified method, which will automatically be unregistered
        public void OnHook(MethodBase from, MethodInfo to)
        {
            if (!TryDisableInlining(from))
            {
                Log(global::Celeste.Everest.EverestLogLevel.Warn,
                    $"Failed to disable inlining for On-hooked method: {SourceTasMethodNameFormatting.GetFullName(process.Reflection.SourceMethods, process.SourceMembers, from)}");
            }
            else
            {
                Log(global::Celeste.Everest.EverestLogLevel.Debug,
                    $"Successfully disabled inlining for On-hooked method: {SourceTasMethodNameFormatting.GetFullName(process.Reflection.SourceMethods, process.SourceMembers, from)}");
            }

            onHooks.Add(new Hook(from, to));
        }

        /// Creates an On-hook to the specified method, which will automatically be unregistered
        public void OnHook(MethodBase from, Delegate to)
        {
            if (!TryDisableInlining(from))
            {
                Log(global::Celeste.Everest.EverestLogLevel.Warn,
                    $"Failed to disable inlining for On-hooked method: {SourceTasMethodNameFormatting.GetFullName(process.Reflection.SourceMethods, process.SourceMembers, from)}");
            }
            else
            {
                Log(global::Celeste.Everest.EverestLogLevel.Debug,
                    $"Successfully disabled inlining for On-hooked method: {SourceTasMethodNameFormatting.GetFullName(process.Reflection.SourceMethods, process.SourceMembers, from)}");
            }

            onHooks.Add(new Hook(from, to));
        }

        /// Creates an IL-hook to the specified method, which will automatically be unregistered
        public void IlHook(MethodBase from, ILContext.Manipulator manipulator)
        {
            if (!TryDisableInlining(from))
            {
                Log(global::Celeste.Everest.EverestLogLevel.Warn,
                    $"Failed to disable inlining for IL-hooked method: {SourceTasMethodNameFormatting.GetFullName(process.Reflection.SourceMethods, process.SourceMembers, from)}");
            }
            else
            {
                Log(global::Celeste.Everest.EverestLogLevel.Debug,
                    $"Successfully disabled inlining for IL-hooked method: {SourceTasMethodNameFormatting.GetFullName(process.Reflection.SourceMethods, process.SourceMembers, from)}");
            }

            ilHooks.Add(new ILHook(from, il =>
            {
                manipulator(il);

                try
                {
                    il.Method.FixShortLongOps();
                }
                catch
                {
                    // Some methods have slightly weird IL, causing that method to crash
                }
            }));
        }

        /// Creates an IL-hook to the specified method, which will automatically be unregistered
        public void IlHook(MethodBase from, Action<ILCursor, ILContext> manipulator) =>
            IlHook(from, il => manipulator(new ILCursor(il), il));

        /// Creates a callback before the original method is called
        public void HookBefore(MethodBase methodInfo, Action action)
        {
            IlHook(methodInfo, (cursor, _) => { EmitStaticDelegate(cursor, "HookBefore", action); });
        }

        /// Creates a callback before the original method is called
        public void HookBefore<T>(MethodBase methodInfo, Action<T> action)
        {
#if DEBUG
            if (methodInfo.IsStatic)
            {
                var parameters = methodInfo.GetParameters();
                Debug.Assert(parameters.Length >= 1 && typeof(T).IsAssignableFrom(parameters[0].ParameterType));
            }
            else
            {
                Debug.Assert((methodInfo.DeclaringType != null && typeof(T).IsAssignableFrom(methodInfo.DeclaringType)));
            }
#endif
            IlHook(methodInfo, (cursor, _) =>
            {
                cursor.EmitLdarg0();
                EmitStaticDelegate(cursor, "HookBefore", action);
            });
        }

        /// Creates a callback after the original method was called
        public void HookAfter(MethodBase methodInfo, Action action)
        {
            IlHook(methodInfo, (cursor, il) =>
            {
                while (cursor.TryGotoNext(MoveType.AfterLabel, ins => ins.MatchRet()))
                {
                    EmitStaticDelegate(cursor, "HookAfter", action);

                    // Fix exception handler blocks
                    foreach (var handler in il.Body.ExceptionHandlers)
                    {
                        if (handler.HandlerEnd == cursor.Next)
                        {
                            handler.HandlerEnd = cursor.Prev;
                        }
                    }

                    cursor.Index++;
                }
            });
        }

        /// Creates a callback after the original method was called
        public void HookAfter<T>(MethodBase methodInfo, Action<T> action)
        {
#if DEBUG
            if (methodInfo.IsStatic)
            {
                var parameters = methodInfo.GetParameters();
                Debug.Assert(parameters.Length >= 1 && typeof(T).IsAssignableFrom(parameters[0].ParameterType));
            }
            else
            {
                Debug.Assert((methodInfo.DeclaringType != null && typeof(T).IsAssignableFrom(methodInfo.DeclaringType)));
            }
#endif
            IlHook(methodInfo, (cursor, il) =>
            {
                while (cursor.TryGotoNext(MoveType.AfterLabel, ins => ins.MatchRet()))
                {
                    cursor.EmitLdarg0();

                    // Fix exception handler blocks
                    foreach (var handler in il.Body.ExceptionHandlers)
                    {
                        if (handler.HandlerEnd == cursor.Next)
                        {
                            handler.HandlerEnd = cursor.Prev;
                        }
                    }

                    EmitStaticDelegate(cursor, "HookAfter", action);
                    cursor.Index++;
                }
            });
        }

        /// Creates a callback to conditionally call the original method
        public void SkipMethod(MethodInfo method, Func<bool> condition)
        {
#if DEBUG
            Debug.Assert(method.ReturnType == typeof(void));
#endif
            IlHook(method, (cursor, _) =>
            {
                var start = cursor.MarkLabel();
                cursor.MoveBeforeLabels();

                EmitStaticDelegate(cursor, "SkipMethod", condition);
                cursor.EmitBrfalse(start);
                cursor.EmitRet();
            });
        }

        /// Creates a callback to conditionally call the original method
        public void SkipMethod<T>(MethodInfo method, Func<T, bool> condition)
        {
#if DEBUG
            if (method.IsStatic)
            {
                var parameters = method.GetParameters();
                Debug.Assert(parameters.Length >= 1 && typeof(T).IsAssignableFrom(parameters[0].ParameterType));
            }
            else
            {
                Debug.Assert((method.DeclaringType != null && typeof(T).IsAssignableFrom(method.DeclaringType)));
            }

            Debug.Assert(method.ReturnType == typeof(void));
#endif
            IlHook(method, (cursor, _) =>
            {
                var start = cursor.MarkLabel();
                cursor.MoveBeforeLabels();

                cursor.EmitLdarg0();
                EmitStaticDelegate(cursor, "SkipMethod", condition);
                cursor.EmitBrfalse(start);
                cursor.EmitRet();
            });
        }

        /// Creates a callback to conditionally call the original methods
        public void SkipMethods(Func<bool> condition, params MethodInfo?[] methods)
        {
            foreach (var method in methods)
            {
                if (method != null) SkipMethod(method, condition);
            }
        }

        /// Creates a callback to conditionally override the return value of the original method without ever even calling it
        public void OverrideReturn<T>(MethodInfo method, Func<bool>? condition, T value)
        {
#if DEBUG
            Debug.Assert(method.ReturnType.IsAssignableFrom(typeof(T)),
                $"Expected type of override {typeof(T).FullName} to be assignable to {method.ReturnType.FullName}");
#endif
            IlHook(method, (cursor, _) =>
            {
                var start = cursor.MarkLabel();
                cursor.MoveBeforeLabels();

                if (condition is not null)
                {
                    EmitStaticDelegate(cursor, "OverrideReturn", condition);
                    cursor.EmitBrfalse(start);
                }

                // Put the return value onto the stack
                switch (value)
                {
                    case int v:
                        cursor.EmitLdcI4(v);
                        break;
                    case long v:
                        cursor.EmitLdcI8(v);
                        break;
                    case float v:
                        cursor.EmitLdcR4(v);
                        break;
                    case double v:
                        cursor.EmitLdcR8(v);
                        break;

                    default:
                        // The type doesn't have a specific IL-instruction, so the reference-store
                        cursor.EmitReference(value);
                        break;
                }

                cursor.EmitRet();
            });
        }

        /// Creates a callback to conditionally override the return value of the original method without ever even calling it
        public void OverrideReturn<T>(MethodInfo method, Func<bool>? condition, Func<T> valueProvider)
        {
#if DEBUG
            Debug.Assert(method.ReturnType.IsAssignableFrom(typeof(T)),
                $"Expected type of override {typeof(T).FullName} to be assignable to {method.ReturnType.FullName}");
#endif
            IlHook(method, (cursor, _) =>
            {
                var start = cursor.MarkLabel();
                cursor.MoveBeforeLabels();

                if (condition is not null)
                {
                    EmitStaticDelegate(cursor, "OverrideReturn", condition);
                    cursor.EmitBrfalse(start);
                }

                // Put the return value onto the stack
                EmitStaticDelegate(cursor, "OverrideReturn", valueProvider);
                cursor.EmitRet();
            });
        }

        /// Creates a callback to conditionally override the return value of the original method without ever even calling it
        public void OverrideReturn<TParam, TReturn>(MethodInfo method, Func<TParam, bool>? condition,
            Func<TParam, TReturn> valueProvider)
        {
#if DEBUG
            if (method.IsStatic)
            {
                var parameters = method.GetParameters();
                Debug.Assert(parameters.Length >= 1 && typeof(TParam).IsAssignableFrom(parameters[0].ParameterType));
            }
            else
            {
                Debug.Assert((method.DeclaringType != null && typeof(TParam).IsAssignableFrom(method.DeclaringType)));
            }

            Debug.Assert(method.ReturnType.IsAssignableFrom(typeof(TReturn)),
                $"Expected type of override {typeof(TReturn).FullName} to be assignable to {method.ReturnType.FullName}");
#endif
            IlHook(method, (cursor, _) =>
            {
                var start = cursor.MarkLabel();
                cursor.MoveBeforeLabels();

                if (condition is not null)
                {
                    cursor.EmitLdarg0();
                    EmitStaticDelegate(cursor, "OverrideReturn", condition);
                    cursor.EmitBrfalse(start);
                }

                // Put the return value onto the stack
                cursor.EmitLdarg0();
                EmitStaticDelegate(cursor, "OverrideReturn", valueProvider);
                cursor.EmitRet();
            });
        }

        /// Creates a callback to conditionally override the return value of the original methods without ever even calling them
        public void OverrideReturns<T>(Func<bool> condition, T value, params MethodInfo?[] methods)
        {
            foreach (var method in methods)
            {
                if (method != null) OverrideReturn(method, condition, value);
            }
        }

        /// Creates a callback to conditionally override the return value of the original methods without ever even calling them
        public void OverrideReturns<T>(Func<bool> condition, Func<T> valueProvider, params MethodInfo?[] methods)
        {
            foreach (var method in methods)
            {
                if (method != null) OverrideReturn(method, condition, valueProvider);
            }
        }

        /// Emits a callback, retaining its actual process owner when required.
        /// Static and noncapturing callbacks retain the direct-call optimization.
        public void EmitStaticDelegate<T>(ILCursor cursor, T cb) where T : Delegate
            => EmitStaticDelegate(cursor, "Delegate", cb);

        /// Emits a callback, retaining its actual process owner when required.
        /// Static and noncapturing callbacks retain the direct-call optimization.
        public void EmitStaticDelegate<T>(ILCursor cursor, string methodName, T cb) where T : Delegate
        {
            // Preserve the direct-call path for an ordinary static method group.
            if (cb.GetInvocationList().Length == 1 && cb.Target == null && cb.Method.IsStatic)
            {
                cursor.EmitCall(cb.Method);
                return;
            }

            // Shared callbacks retain their process owner instead of using global static state.
            // Only a compiler-generated, noncapturing instance lambda can lose its receiver.
            if (cb.GetInvocationList().Length != 1 || cb.Target == null || cb.Method.IsStatic ||
                !cb.Method.Name.StartsWith("<", StringComparison.Ordinal) ||
                cb.Method.DeclaringType == null ||
                !cb.Method.DeclaringType.IsDefined(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute),
                    false))
            {
                cursor.EmitDelegate(cb);
                return;
            }

            var methodDef = ResolveDefinition(cb.Method);
            if (methodDef.Body.Instructions.Any(instruction =>
                    (instruction.MatchLdarg(out int argument) && argument == 0) ||
                    ((instruction.OpCode == OpCodes.Ldarga || instruction.OpCode == OpCodes.Ldarga_S) &&
                     instruction.Operand == methodDef.Body.ThisParameter)))
            {
                cursor.EmitDelegate(cb);
                return;
            }

            // Static delegates do not work well with hot-reloads
            // Since they're only intended for better performance, they are simply disabled in debug builds
#if DEBUG
            // Still validate callback method
            foreach (var instr in methodDef.Body.Instructions)
            {
                if (!instr.MatchLdarg(out int index))
                {
                    continue;
                }

                if (index == 0)
                {
                    throw new Exception("Using captured variables inside a static delegate is not allowed");
                }
            }

            cursor.EmitDelegate(cb);
#else
            // Extract hook name from delegate
            string hookName = cb.Method.Name.Split('>')[0][1..];
            string name = $"{hookName}_{methodName}";

            var parameters = cb.Method.GetParameters();

            var dynamicMethod = new DynamicMethodDefinition(name,
                cb.Method.ReturnType,
                parameters
                    .Select(p => p.ParameterType)
                    .ToArray());
            dynamicMethod.Definition.Body = methodDef.Body.Clone(dynamicMethod.Definition);
            for (int i = 0; i < dynamicMethod.Definition.Parameters.Count; i++)
            {
                dynamicMethod.Definition.Parameters[i].Name = parameters[i].Name;
            }

            // Shift over arguments, since "this" was removed
            var processor = dynamicMethod.GetILProcessor();
            foreach (var instr in processor.Body.Instructions)
            {
                if (!instr.MatchLdarg(out int index))
                {
                    continue;
                }

                switch (index)
                {
                    case 0:
                        throw new Exception("Using captured variables inside a static delegate is not allowed");

                    case 1:
                        instr.OpCode = OpCodes.Ldarg_0;
                        break;
                    case 2:
                        instr.OpCode = OpCodes.Ldarg_1;
                        break;
                    case 3:
                        instr.OpCode = OpCodes.Ldarg_2;
                        break;
                    case 4:
                        instr.OpCode = OpCodes.Ldarg_3;
                        break;

                    default:
                        instr.OpCode = OpCodes.Ldarg;
                        instr.Operand = index - 1;
                        break;
                }
            }

            var targetMethod = dynamicMethod.Generate();
            var targetReference = cursor.Context.Import(targetMethod);
            targetReference.Name = name;
            targetReference.DeclaringType =
                (cb.Method.DeclaringType?.DeclaringType == null
                    ? null
                    : ResolveDefinition(cb.Method.DeclaringType.DeclaringType));
            targetReference.ReturnType = dynamicMethod.Definition.ReturnType;
            targetReference.Parameters.AddRange(dynamicMethod.Definition.Parameters);

            cursor.EmitCall(targetReference);
#endif
        }

        /// Resolves the actual physical declaration, retaining its reader for the process lifetime.
        public TypeDefinition ResolveDefinition(Type type) =>
            ((ISourceTasHookModuleMetadataEnvironment)TasRuntimeEnvironment.Get(process)).HookModuleMetadata
            .ResolveDefinition(type);

        /// Resolves the actual physical method by CLR module identity and metadata token.
        public MethodDefinition ResolveDefinition(MethodBase method) =>
            ((ISourceTasHookModuleMetadataEnvironment)TasRuntimeEnvironment.Get(process)).HookModuleMetadata
            .ResolveDefinition(method);
    }
}
