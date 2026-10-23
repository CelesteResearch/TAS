using Celeste.Core.Scheduling;
using Celeste.Core.Audio;
using System;
using Celeste.Core.Diagnostics.Tas;
using Celeste.Audio.Fmod.Events;
using Celeste.Audio.Fmod.Interop;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : ISourceAudioInterceptionPolicy, ISourceGameUpdateObserver,
        ISourceCassetteTimingPolicy
    {
        private bool sourceTasAutoMuteHooks;
        bool ISourceAudioInterceptionPolicy.IsEventInstanceInterceptionEnabled => sourceTasAutoMuteHooks;
        internal void InstallSourceTasAutoMuteHooks() => sourceTasAutoMuteHooks = true;
        internal void UninstallSourceTasAutoMuteHooks() => sourceTasAutoMuteHooks = false;

        bool ISourceAudioInterceptionPolicy.InvokeSetMusic(Func<bool> original)
            => sourceTasAutoMuteHooks ? sourceTasModule.Driver.SourceAutoMute.SetMusic(original) : original();

        void ISourceAudioInterceptionPolicy.InvokeSetAltMusic(Action original)
        {
            if (sourceTasAutoMuteHooks) sourceTasModule.Driver.SourceAutoMute.SetAltMusic(original);
            else original();
        }

        FmodResult ISourceAudioInterceptionPolicy.InvokeCreateEventInstance(string path,
            SourceFmodCreateEventInstance original, out FmodEventReference instance)
            => sourceTasAutoMuteHooks
                ? sourceTasModule.Driver.SourceAutoMute.CreateInstance(path, original, out instance)
                : original(out instance);

        int ISourceCassetteTimingPolicy.TransformLeadBeats(int leadBeats)
            => sourceTasAutoMuteHooks ? sourceTasModule.Driver.SourceAutoMute.MuteBeats(leadBeats) : leadBeats;

        void ISourceGameUpdateObserver.AfterSceneUpdate()
        {
            if (sourceTasAutoMuteHooks) sourceTasModule.Driver.SourceAutoMute.AfterSceneUpdate();
        }

        void ISourceGameUpdateObserver.AfterGameUpdate()
        {
            if (sourceTasAutoMuteHooks) sourceTasModule.Driver.SourceAutoMute.AfterCelesteUpdate();
        }
    }
}
