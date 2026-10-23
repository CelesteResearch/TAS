using Celeste.Localization.Lookup;
using Celeste.Core.Scheduling;

namespace Celeste.Mods.Tas
{
    public static class SourceTasDialogText
    {
        public static string ToDialogText(ISourceEngineContext engine, string input)
            => ((ISourceDialogEnvironment)engine).Dialog.Clean("TAS_" + input.Replace(" ", "_"));
    }
}
