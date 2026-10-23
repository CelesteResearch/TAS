using Celeste.Core.Rendering;
using Celeste.Game.Chapters.Flow;
using Celeste.Simulation.Runtime.Session;
using Celeste.Mods.Tas.Information;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : IOriginalAreaCompleteInfoHooksEnvironment,
        ISourceSessionFlagInterceptionEnvironment
    {
        private SourceTasAreaCompleteInfo sourceTasAreaCompleteInfoHook;

        internal void InstallSourceTasAreaCompleteInfoHooks(SourceTasAreaCompleteInfo actualInfo)
            => sourceTasAreaCompleteInfoHook = actualInfo;

        internal void RemoveSourceTasAreaCompleteInfoHooks()
            => sourceTasAreaCompleteInfoHook = null;

        public bool AllowSourceSessionFlagChange(ChapterCompletionSession session, string flag, bool setTo)
            => sourceTasAreaCompleteInfoHook?.AllowSessionFlagChange(flag, setTo) ?? true;

        public void AfterSourceAreaCompleteInfoInitialized(bool pieScreen, ChapterCompletionSession session)
            => sourceTasAreaCompleteInfoHook?.InitText(session);

        public void AfterSourceCS08EndingRendered(ISourceRenderContext context, object owner, float versionAlpha)
            => sourceTasAreaCompleteInfoHook?.DrawText(context, owner, versionAlpha);

        public void AfterSourceAreaCompleteInfoRendered(ISourceRenderContext context, object owner, float ease)
            => sourceTasAreaCompleteInfoHook?.DrawText(context, owner, ease);
    }
}
