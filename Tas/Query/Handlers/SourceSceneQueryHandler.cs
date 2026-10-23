using System;
using Celeste.Scene.Tracking;
using Celeste.Game.Execution.Levels;
using Celeste.Game.Execution.LevelLoader;
using StudioCommunication;
using TargetQuery = Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess;

namespace Celeste.Mods.Tas.Query.Handlers
{
    internal class SourceSceneQueryHandler : TargetQuery.Handler
    {
        public SourceSceneQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
        }

        private SceneEntityCatalog Catalog => ((ISourceSceneCatalogEnvironment)Query.Engine).SceneCatalog;

        public override bool CanResolveInstances(Type type) =>
            typeof(global::Celeste.Scene.Scenes.SourceSceneState).IsAssignableFrom(type);

        public override object[] ResolveInstances(Type type)
        {
            var scene = Query.Engine.Frame.Scenes.CurrentScene;
            if (scene != null && type.IsInstanceOfType(scene)) return [scene];
            bool mappedTarget = Query.Members.ContainsSourceType(type);
            if (mappedTarget && scene != null && Query.IsSameOrSubclassOfOriginal(scene.GetType(), type))
                return [scene];
            if (Query.IsSameOrSubclassOfOriginal(type, "Celeste.Level"))
            {
                // Original Extensions.GetLevel: Level itself, or the same LevelLoader.Level.
                OriginalLevelSceneRoot level = scene switch
                {
                    OriginalLevelSceneRoot actualLevel => actualLevel,
                    OriginalLevelLoaderScene loader => loader.Level,
                    _ => null
                };
                if (level != null && (type.IsInstanceOfType(level)
                                      || mappedTarget && Query.IsSameOrSubclassOfOriginal(level.GetType(), type)))
                    return [level];
            }

            return [];
        }

        public override bool IsMemberSuggested(SourceQueryMemberIdentity member, TargetQuery.Variant variant)
            => variant == TargetQuery.Variant.Set && member.DeclaringType != null
                                                  && typeof(global::Celeste.Scene.Scenes.SourceSceneState)
                                                      .IsAssignableFrom(
                                                          member.DeclaringType)
                                                  && member.Name == "TimeActive";
    }
}
