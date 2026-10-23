using Celeste.Core.Display;
using StudioCommunication;

namespace Celeste.Mods.Tas.Module
{
    public partial class CelesteTasSettings
    {
        public bool ShowCycleHitboxColors { get; set; } = false;

        public global::Celeste.Core.Display.PackedColor CycleHitboxColor1 { get; set; } =
            global::Celeste.Mods.Tas.Hitboxes.SourceTasCycleHitboxColorProcess.DefaultColor1;

        public global::Celeste.Core.Display.PackedColor CycleHitboxColor2 { get; set; } =
            global::Celeste.Mods.Tas.Hitboxes.SourceTasCycleHitboxColorProcess.DefaultColor2;

        public global::Celeste.Core.Display.PackedColor CycleHitboxColor3 { get; set; } =
            global::Celeste.Mods.Tas.Hitboxes.SourceTasCycleHitboxColorProcess.DefaultColor3;

        public global::Celeste.Core.Display.PackedColor OtherCyclesHitboxColor { get; set; } =
            global::Celeste.Mods.Tas.Hitboxes.SourceTasCycleHitboxColorProcess.DefaultOthersColor;

        public bool SimplifiedHitboxes
        {
            get => StudioShared.SimplifiedHitboxes;
            set
            {
                StudioShared.SimplifiedHitboxes = value;
                SyncSettings();
                if (value && CelesteTasModule.Instance.Process.Frame.Scenes.CurrentScene != null)
                    CelesteTasModule.Instance.Driver.SourceTriggerHitbox.RecacheTriggers(
                        CelesteTasModule.Instance.Process.Frame.Scenes.CurrentScene);
            }
        }

        public ActualCollideHitboxType ShowActualCollideHitboxes
        {
            get => StudioShared.ActualCollideHitboxes;
            set
            {
                StudioShared.ActualCollideHitboxes = value;
                SyncSettings();
            }
        }
    }
}
