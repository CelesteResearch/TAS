using System;
using System.Collections.Generic;
using Celeste.Core.Components;
using Celeste.Scene.Tracking;

namespace Celeste.Mods.Tas.Information
{
    internal static class SourceTasTrackerExtensions
    {
        public static T GetEntityTrackIfNeeded<T>(this SceneEntityRegistry tracker) where T : class
        {
            var entities = tracker.GetEntitiesTrackIfNeeded<T>();
            return entities.Count == 0 ? null : entities[0] as T;
        }

        public static List<T> GetCastComponents<T>(this ISourceTrackedComponents tracker, string sourceType)
            where T : SourceComponentState
        {
            var source = tracker.GetTrackedComponents(sourceType);
            if (source == null) throw new ArgumentNullException(nameof(source));
            var result = new List<T>();
            foreach (var component in source)
                if (component is T matched)
                    result.Add(matched);
            return result;
        }
    }
}
