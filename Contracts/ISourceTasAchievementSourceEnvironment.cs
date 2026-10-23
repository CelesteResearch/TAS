using System;

namespace Celeste.Core.Diagnostics.Tas
{
    public interface ISourceTasAchievementSourceEnvironment
    {
        int SourceStatsIncrementInstructionCount { get; }
        int SourceAchievementsRegisterInstructionCount { get; }
        void InvokeSourceStatsIncrement(Action original);
        void InvokeSourceAchievementRegister(Action original);
    }
}
