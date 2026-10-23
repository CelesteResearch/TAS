using System;
using System.Collections.Generic;
using System.Reflection;
using StudioCommunication.Util;
using Celeste.Game.Execution.Areas;
using Celeste.Scene.Tracking;
using Session = Celeste.Simulation.Runtime.Session.ChapterCompletionSession;
using TargetQuery = Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess;

namespace Celeste.Mods.Tas.Query.Handlers
{
    internal class SourceSessionQueryHandler : TargetQuery.Handler
    {
        public SourceSessionQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
        }

        public override bool CanResolveInstances(Type type) => type == typeof(Session);

        public override (HashSet<Type> Types, string[] MemberArgs)? ResolveBaseTypes(string[] queryArgs)
        {
            // The original query names are an input protocol, not the internal session's CLR identity.
            if (queryArgs.Length == 0) return null;
            if (queryArgs[0] is "Session" or "Session@Celeste")
                return ([typeof(Session)], queryArgs[1..]);
            if (queryArgs.Length > 1 && queryArgs[0] == "Celeste" &&
                queryArgs[1] is "Session" or "Session@Celeste")
                return ([typeof(Session)], queryArgs[2..]);
            return null;
        }

        public override object[] ResolveInstances(Type type)
        {
            var scene = Query.Engine.Frame.Scenes.CurrentScene;
            if (scene == null) return [];
            return SourceTasSceneSessionQuery.GetSession(scene) is { } session ? [session] : [];
        }

        public override Result<bool, TargetQuery.MemberAccessError> ResolveMember(object? instance, out object? value,
            Type type, int memberIdx, string[] memberArgs)
        {
            if (instance is Session session && memberArgs[memberIdx] == "Area")
            {
                value = ((IOriginalAreaKeyEnvironment)Query.Engine).AreaKeys.GetSessionArea(session);
                return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
            }

            if (instance is OriginalAreaKey area && memberArgs[memberIdx] == nameof(OriginalAreaKey.SID))
            {
                value = ((IOriginalAreaKeyEnvironment)Query.Engine).AreaKeys.GetSID(area);
                return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
            }

            value = null;
            return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
        }

        public override Result<bool, TargetQuery.MemberAccessError> ResolveTargetTypes(out Type[] targetTypes,
            Type type,
            ref int memberIdx, string[] memberArgs)
        {
            if (type == typeof(Session) && memberArgs[memberIdx] == "Area")
            {
                targetTypes = [typeof(OriginalAreaKey)];
                return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
            }

            if (type == typeof(OriginalAreaKey) && memberArgs[memberIdx] == nameof(OriginalAreaKey.SID))
            {
                targetTypes = [typeof(string)];
                return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
            }

            targetTypes = [];
            return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
        }
    }
}
