using System;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Celeste.Core.Mathematics;
using Celeste.Core.Mathematics.LinearAlgebra;
using Celeste.Everest.Modules.Runtime;
using Celeste.Game.Execution.Levels;

namespace Celeste.Mods.Tas.Interop
{
    internal sealed class SourceTasExCameraDynamicsInterop
    {
        private readonly GameApplication process;
        private readonly Lazy<bool> installed;
        private readonly Lazy<Type> hooks;
        private readonly Lazy<Action> hook;
        private readonly Lazy<Action> unhook;
        private readonly Lazy<Action<float>> resizeVanillaBuffers;

        internal SourceTasExCameraDynamicsInterop(GameApplication process)
        {
            this.process = process;
            installed = new(() =>
                ((ISourceEverestModulesEnvironment)process).Modules.Any(m =>
                    m.Metadata?.Name == "ExtendedCameraDynamics"));
            hooks = new(() => ((ISourceEverestModulesEnvironment)process).Modules
                .First(m => m.Metadata?.Name == "ExtendedCameraDynamics")
                .GetType().Assembly.GetType("Celeste.Mod.ExCameraDynamics.Code.Hooks.CameraZoomHooks", true));
            hook = new(() => Bind<Action>("Hook", Type.EmptyTypes));
            unhook = new(() => Bind<Action>("Unhook", Type.EmptyTypes));
            resizeVanillaBuffers = new(() => Bind<Action<float>>("ResizeVanillaBuffers", new[] { typeof(float) }));
        }

        private T Bind<T>(string name, Type[] args) where T : Delegate
            => (T)Delegate.CreateDelegate(typeof(T),
                hooks.Value.GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null,
                    args,
                    null));

        private object Read(string name)
        {
            const BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            // Actual addon metadata: these two are properties, the other two are fields.
            if (name == "AutomaticZooming" || name == "HooksEnabled")
                return hooks.Value.GetProperty(name, flags).GetValue(null);
            return hooks.Value.GetField(name, flags).GetValue(null);
        }

        private void Write(string name, object value)
        {
            const BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            if (name == "AutomaticZooming") hooks.Value.GetProperty(name, flags).SetValue(null, value);
            else hooks.Value.GetField(name, flags).SetValue(null, value);
        }

        public bool Installed => installed.Value;
        public bool Enabled => Installed && hooksEnabled();

        public void EnableHooks(float currentZoom = 1f)
        {
            if (Installed) enableHooks(currentZoom);
        }

        public void DisableHooks()
        {
            if (Installed) disableHooks();
        }

        public void SetCamera(OriginalLevelSceneRoot level, Vector2 center, float zoom)
        {
            if (Installed) setCamera(level, center, zoom);
        }

        public bool AutomaticZooming
        {
            get => Installed && (bool)Read("AutomaticZooming");
            set
            {
                if (Installed) Write("AutomaticZooming", value);
            }
        }

        public float TriggerZoomOverride
        {
            get => Installed ? (float)Read("TriggerZoomOverride") : -1f;
            set
            {
                if (Installed) Write("TriggerZoomOverride", value);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool hooksEnabled() => (bool)Read("HooksEnabled");

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void enableHooks(float currentZoom)
        {
            hook.Value();
            resizeVanillaBuffers.Value(currentZoom);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void disableHooks() => unhook.Value();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void setCamera(OriginalLevelSceneRoot level, Vector2 center, float zoom)
        {
            var arithmetic = ((ISourceVectorEnvironment)process).VectorArithmetic;
            // Actual addon CameraFocus.FromCenter followed by ForceCameraTo, in source order.
            Vector2 focusPosition = SourceVector2Math.Subtract(center, arithmetic.Divide(new Vector2(160f, 90f), zoom));
            Write("AutomaticZooming", false);
            level.Zoom = level.ZoomTarget = zoom;
            level.ZoomFocusPoint = arithmetic.Divide(new Vector2(160f, 90f), zoom);
            level.Camera.Viewport.Width = (int)(320f / zoom);
            level.Camera.Viewport.Height = (int)(180f / zoom);
            level.Camera.Position = focusPosition;
            if (level.Zoom < (float)Read("current_buffer_zoom")) resizeVanillaBuffers.Value(level.Zoom);
            Write("AutomaticZooming", true);
            Write("TriggerZoomOverride", zoom);
        }
    }
}
