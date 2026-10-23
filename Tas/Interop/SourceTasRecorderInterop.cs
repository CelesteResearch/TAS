using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Celeste.Everest.Modules.Runtime;
using Celeste.Everest.Reflection;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;

namespace Celeste.Mods.Tas.Interop
{
    internal sealed class SourceTasRecorderInterop
    {
        private readonly SourceMemberCatalog sourceMembers;
        private readonly SourceAssemblyMethodCatalog sourceMethods;
        private readonly Lazy<bool> installed;
        private readonly Lazy<Type> apiType;
        private readonly Lazy<Action<string>> sourceStartRecording;
        private readonly Lazy<Action> sourceStopRecording;
        private readonly Lazy<Action<int>> sourceSetDurationEstimate;
        private readonly Lazy<Func<bool>> sourceIsRecording;
        private readonly Lazy<Func<bool>> sourceIsFFmpegInstalled;
        private readonly Lazy<int> sourceNoEstimate;

        internal SourceTasRecorderInterop(GameApplication process, TasPlaybackDriver driver)
        {
            var reflection = ((ISourceEverestReflectionEnvironment)process).Reflection;
            sourceMembers = reflection.SourceMembers;
            sourceMethods = reflection.SourceMethods;
            installed = new(() => ((ISourceEverestModulesProcessEnvironment)process).ModulesProcess.Modules
                .Any(module => module.Metadata?.Name == "TASRecorder"));
            apiType = new(() =>
                driver.SourceModReflection.TryGetType("TASRecorder", "Celeste.Mod.TASRecorder.TASRecorderAPI")
                ?? throw new TypeLoadException(
                    "Installed TASRecorder has no qualified original TASRecorderAPI type."));
            sourceStartRecording = new(() => Bind<Action<string>>("StartRecording", typeof(string)));
            sourceStopRecording = new(() => Bind<Action>("StopRecording"));
            sourceSetDurationEstimate = new(() => Bind<Action<int>>("SetDurationEstimate", typeof(int)));
            sourceIsRecording = new(() => Bind<Func<bool>>("IsRecording"));
            sourceIsFFmpegInstalled = new(() => Bind<Func<bool>>("IsFFmpegInstalled"));
            sourceNoEstimate = new(() =>
                (int)(sourceMembers.GetFieldInfo(apiType.Value, "NoEstimate", BindingFlags.Public | BindingFlags.Static)
                      ?? throw new MissingFieldException("Celeste.Mod.TASRecorder.TASRecorderAPI", "NoEstimate"))
                .Read(null));
        }

        private T Bind<T>(string name, params Type[] parameters) where T : Delegate
            => (T)Delegate.CreateDelegate(typeof(T), (sourceMethods.GetMethodInfo(sourceMembers, apiType.Value, name,
                                                          parameters,
                                                          BindingFlags.Public | BindingFlags.Static)
                                                      ?? throw new MissingMethodException(
                                                          "Celeste.Mod.TASRecorder.TASRecorderAPI",
                                                          name)).ActualMethod);

        public bool Installed => installed.Value;

        public void StartRecording(string fileName = null)
        {
            if (Installed) startRecording(fileName);
        }

        public void StopRecording()
        {
            if (Installed) stopRecording();
        }

        public void SetDurationEstimate(int? frames)
        {
            if (Installed) setDurationEstimate(frames ?? sourceNoEstimate.Value);
        }

        public bool IsRecording => Installed && isRecording();
        public bool IsFFmpegInstalled => Installed && isFFmpegInstalled();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void startRecording(string fileName = null) => sourceStartRecording.Value(fileName);

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void stopRecording() => sourceStopRecording.Value();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void setDurationEstimate(int frames) => sourceSetDurationEstimate.Value(frames);

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool isRecording() => sourceIsRecording.Value();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool isFFmpegInstalled() => sourceIsFFmpegInstalled.Value();
    }
}
