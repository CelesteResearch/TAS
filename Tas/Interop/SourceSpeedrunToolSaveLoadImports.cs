using System;
using System.Collections.Generic;
using Celeste.Game.Execution.Levels;

namespace Celeste.Mods.Tas.Interop
{
// Assigned by the actual source ModInterop process.
#pragma warning disable CS0649
    internal sealed class SourceSpeedrunToolSaveLoadImports
    {
        public delegate object RegisterSaveLoadActionDelegate(
            Action<Dictionary<Type, Dictionary<string, object?>>, OriginalLevelSceneRoot> saveState,
            Action<Dictionary<Type, Dictionary<string, object?>>, OriginalLevelSceneRoot> loadState,
            Action clearState,
            Action<OriginalLevelSceneRoot>? beforeSaveState,
            Action<OriginalLevelSceneRoot>? beforeLoadState,
            Action? preCloneEntities);

        public delegate object RegisterStaticTypesDelegate(Type type, string[] memberNames);

        public delegate object DeepCloneDelegate(object from);

        /// Registers a new save-load action
        /// Provides an opaque handle to unregister later
        public RegisterSaveLoadActionDelegate? RegisterSaveLoadAction;

        /// Specifies which static members should be cloned
        /// Provides an opaque handle to unregister later
        public RegisterStaticTypesDelegate? RegisterStaticTypes;

        /// Unregisters a previously registered object with the returned handle
        public Action<object>? Unregister;

        /// Creates a deep recursive clone of the object and returns it
        public DeepCloneDelegate? DeepClone;
    }


#pragma warning restore CS0649
}
