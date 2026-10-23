using System;
using Celeste.Core.Diagnostics.Tas;
using Celeste.Everest.Reflection;
using Celeste.Game.Profiles;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : ISourceTasAchievementSourceEnvironment
    {
        private bool sourceTasStatsIncrementHook, sourceTasAchievementRegisterHook;

        private int RequireOriginalAchievementBodyCount(int originalToken, int installedToken, string sourceMethod)
        {
            const string assemblyIdentity = "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            int token = process.SourceProfile == GameProfile.Original ? originalToken : installedToken;
            var definition =
                ((ISourceEverestReflectionEnvironment)process).Reflection.SourceMethods.RequireDefinition(
                    assemblyIdentity,
                    token);
            return definition.BodyInstructionCount ??
                   throw new InvalidOperationException("Original PE method body is not qualified: " + sourceMethod);
        }

        int ISourceTasAchievementSourceEnvironment.SourceStatsIncrementInstructionCount
            => RequireOriginalAchievementBodyCount(100670833, 100676735, "Celeste.Stats.Increment");

        int ISourceTasAchievementSourceEnvironment.SourceAchievementsRegisterInstructionCount
            => RequireOriginalAchievementBodyCount(100668036, 100670956, "Celeste.Achievements.Register");

        internal void InstallSourceTasStatsIncrementHook() => sourceTasStatsIncrementHook = true;
        internal void InstallSourceTasAchievementRegisterHook() => sourceTasAchievementRegisterHook = true;
        internal void UninstallSourceTasStatsIncrementHook() => sourceTasStatsIncrementHook = false;
        internal void UninstallSourceTasAchievementRegisterHook() => sourceTasAchievementRegisterHook = false;

        void ISourceTasAchievementSourceEnvironment.InvokeSourceStatsIncrement(Action original)
        {
            if (sourceTasStatsIncrementHook && sourceTasModule.Driver.Running) return;
            original();
        }

        void ISourceTasAchievementSourceEnvironment.InvokeSourceAchievementRegister(Action original)
        {
            if (sourceTasAchievementRegisterHook && sourceTasModule.Driver.Running) return;
            original();
        }
    }
}
