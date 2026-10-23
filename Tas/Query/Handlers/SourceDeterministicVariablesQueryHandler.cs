using System;
using StudioCommunication;
using TargetQuery = Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess;

namespace Celeste.Mods.Tas.Query.Handlers
{
    internal class SourceDeterministicVariablesQueryHandler : TargetQuery.Handler
    {
        public SourceDeterministicVariablesQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
        }

        public override Result<bool, TargetQuery.MemberAccessError> ResolveMember(object? instance, out object? value,
            Type type, int memberIdx, string[] memberArgs)
        {
            if (memberArgs[memberIdx] == "Random" && Query.Members.ContainsSourceType(type) &&
                Query.GetOriginalTypeFullName(type) == "Monocle.Calc")
            {
                value = Query.Driver.SourceSeededRandomness.SharedUpdateHandler.SharedUpdateRandom;
                return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
            }

            if (memberArgs[memberIdx] == "FrameCounter" && Query.Members.ContainsSourceType(type) &&
                Query.GetOriginalTypeFullName(type) == "Monocle.Engine")
            {
                value = Query.Driver.SourceSeededRandomness.FrameCounterHandler.FrameCounter;
                return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
            }

            value = null;
            return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
        }

        public override Result<bool, TargetQuery.MemberAccessError> SetMember(object? instance, object? value,
            Type type,
            int memberIdx, string[] memberArgs, bool forceAllowCodeExecution)
        {
            if (memberArgs[memberIdx] == "Random" && Query.Members.ContainsSourceType(type) &&
                Query.GetOriginalTypeFullName(type) == "Monocle.Calc")
            {
                Query.Driver.SourceSeededRandomness.SharedUpdateHandler.SharedUpdateRandom =
                    (global::Celeste.Core.Randomness.ISourceRandom)value!;
                return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
            }

            if (memberArgs[memberIdx] == "FrameCounter" && Query.Members.ContainsSourceType(type) &&
                Query.GetOriginalTypeFullName(type) == "Monocle.Engine")
            {
                Query.Driver.SourceSeededRandomness.FrameCounterHandler.FrameCounter = (ulong)value!;
                return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
            }

            return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
        }
    }
}
