using Celeste.Mods.Tas.Hitboxes.ModInterop;
using Celeste.Mods.Tas.Reflection;
using MonoMod.Cil;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasPlaybackDriver
    {
        [SourceTasModILHook("SpirialisHelper", "Celeste.Mod.Spirialis.TimeGameplayRenderer", "Render")]
        private void SourceFixStopwatchRendering(ILCursor cursor)
            => SourceTasSpirialisHelperHitbox.FixStopwatchRendering(cursor, process,
                () => SourceHitboxToggle.DrawHitboxes);
    }
}
