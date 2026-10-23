#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using Celeste.Everest.Modules.Runtime;
using Celeste.Everest.Modules.Capabilities;
using Celeste.Game.Execution.Areas;
using Celeste.Game.Execution.Levels;

namespace Celeste.Mods.Tas.Interop
{
    internal sealed class SourceTasCollabLobbyInterop
    {
        private readonly GameApplication process;
        private readonly SourceTasModReflection reflection;
        private readonly Func<bool> installed;
        private readonly Lazy<Type> lobbyMapUI;
        private readonly Lazy<Type> collabModule;

        // Assigned through the qualified original ModInterop field binding.
#pragma warning disable CS0649
        public Func<string, bool>? IsCollabLevelSet;
#pragma warning restore CS0649
        // Assigned through the qualified original ModInterop field binding.
#pragma warning disable CS0649
        public Func<string, bool>? IsCollabLobby;
#pragma warning restore CS0649

        internal SourceTasCollabLobbyInterop(GameApplication process,
            SourceTasModReflection reflection, Func<bool> installed)
        {
            this.process = process;
            this.reflection = reflection;
            this.installed = installed;
            lobbyMapUI = new(() => reflection.GetType("CollabUtils2", "Celeste.Mod.CollabUtils2.UI.LobbyMapUI")
                                   ?? throw new InvalidOperationException(
                                       "The legacy CollabUtils2 lobby map UI is unavailable."));
            collabModule = new(() => reflection.GetType("CollabUtils2", "Celeste.Mod.CollabUtils2.CollabModule")
                                     ?? throw new InvalidOperationException(
                                         "The legacy CollabUtils2 module type is unavailable."));
        }

        public bool TryGetActiveWarps(OriginalLevelSceneRoot level, [NotNullWhen(true)] out string[]? activeWarps)
        {
            if (installed()) return GetActiveWarps(level, out activeWarps);
            activeWarps = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ClearLobbyMap(string sid)
        {
            if (((ISourceEverestModulesEnvironment)process).Modules.OfType<ISourceCollabLobbyMaps>().FirstOrDefault() is
                { } capability)
            {
                capability.ClearLobbyMap(sid);
                return;
            }

            if (process.Frame.Scenes.CurrentScene is OriginalLevelSceneRoot level)
            {
                var keys = ((IOriginalAreaKeyEnvironment)process).AreaKeys;
                if (keys.GetSID(keys.GetSessionArea(level.Session)) == sid &&
                    level.SourceScene.Entities
                        .GetTrackedEntities("Celeste.Mod.CollabUtils2.Entities.LobbyMapController")
                        .FirstOrDefault() is { } controller)
                {
                    if (ReadField(controller, "VisitManager") is { } manager)
                        Invoke(manager.GetType(), manager, "Reset", true);
                    if (ReadField(controller, "VisitManager") is { } saveManager)
                        Invoke(saveManager.GetType(), saveManager, "Save");
                }
            }

            var module = reflection.GetFieldInfo(collabModule.Value, "Instance").Read(null)!;
            var saveData = ReadProperty(module, "SaveData")!;
            var positions = (IDictionary)ReadField(saveData, "VisitedLobbyPositions")!;
            string[] keysToRemove = positions.Keys.Cast<string>().Where(key => key.StartsWith(sid)).ToArray();
            foreach (string key in keysToRemove)
                ((IDictionary)ReadField(
                    ReadProperty(reflection.GetFieldInfo(collabModule.Value, "Instance").Read(null)!, "SaveData")!,
                    "VisitedLobbyPositions")!).Remove(key);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool GetActiveWarps(OriginalLevelSceneRoot level, [NotNullWhen(true)] out string[]? activeWarps)
        {
            if (((ISourceEverestModulesEnvironment)process).Modules.OfType<ISourceCollabLobbyMaps>().FirstOrDefault() is
                { } capability)
                return capability.TryGetActiveWarps(level, out activeWarps);
            activeWarps = null;
            var warps = level.SourceScene.Entities.GetTrackedEntities("Celeste.Mod.CollabUtils2.Entities.LobbyMapWarp");
            if (level.SourceScene.Entities.GetTrackedEntities("Celeste.Mod.CollabUtils2.Entities.LobbyMapController")
                    .FirstOrDefault() is not { } controller)
                return false;
            if (Invoke(lobbyMapUI.Value, null, "generateVisitedTiles", ReadField(controller, "Info"),
                    ReadField(controller, "VisitManager")) is not { } visitedTiles)
                return false;

            activeWarps = warps.Select(warp => ReadField(warp, "info")!)
                .Where(marker => IsVisited(visitedTiles, ReadField(marker, "Position")!) &&
                                 Convert.ToInt32(ReadField(marker, "Type")) == 0 &&
                                 (!(bool)ReadField(marker, "WarpRequiresActivation")! ||
                                  ((IEnumerable)ReadProperty(ReadField(controller, "VisitManager")!, "ActivatedWarps")!)
                                  .Cast<string>()
                                  .Contains((string)ReadField(marker, "MarkerId")!)))
                .Select(marker => (string)ReadField(marker, "MarkerId")!).ToArray();
            return true;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool IsVisited(object visitedTiles, object position, byte threshold = 0x7F)
        {
            float x = (float)ReadField(position, "X")!;
            float y = (float)ReadField(position, "Y")!;
            object?[] arguments = [(int)(x / 8), (int)(y / 8), (byte)0];
            return (bool)InvokeArguments(visitedTiles.GetType(), visitedTiles, "TryGet", arguments)! &&
                   (byte)arguments[2]! > threshold;
        }

        private object? ReadField(object instance, string name)
            => reflection.GetFieldInfo(instance.GetType(), name).Read(instance);

        private object? ReadProperty(object instance, string name)
            => reflection.GetPropertyInfo(instance.GetType(), name).Read(instance);

        private object? Invoke(Type type, object? instance, string name, params object?[] arguments)
            => InvokeArguments(type, instance, name, arguments);

        private object? InvokeArguments(Type type, object? instance, string name, object?[] arguments)
        {
            var method = reflection.GetMethodInfo(type, name)
                         ?? throw new MissingMethodException(type.FullName, name);
            try
            {
                return method.Invoke(instance, arguments);
            }
            catch (TargetInvocationException exception) when (exception.InnerException != null)
            {
                ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
                throw;
            }
        }
    }
}
