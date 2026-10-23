using YamlDotNet.Serialization;
using System.Collections.Generic;
using System.Linq;
using Celeste.Core.Mathematics;
using Celeste.Core.Mathematics.Framework;
using Celeste.Everest.Modules.Settings;
using StudioCommunication;

namespace Celeste.Mods.Tas.Module
{
    public partial class CelesteTasSettings : EverestModuleSettings
    {
        public static CelesteTasSettings Instance { get; private set; }

        public CelesteTasSettings()
        {
            Instance = this;
        }

        public bool Enabled { get; set; } = true;
        internal GameSettings StudioShared = new();

        private readonly float[] slowForwardSpeeds =
        {
            0.01f, 0.02f, 0.03f, 0.04f, 0.05f, 0.06f, 0.07f, 0.08f, 0.09f,
            0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f
        };

        public List<float> SlowForwardSpeeds
        {
            get
            {
                List<float> forwardSpeeds = slowForwardSpeeds.ToList();
                if (slowForwardSpeeds.Contains(StudioShared.SlowForwardSpeed)) return forwardSpeeds;
                forwardSpeeds.Add(StudioShared.SlowForwardSpeed);
                forwardSpeeds.Sort();
                return forwardSpeeds;
            }
        }

        public int FastForwardSpeed
        {
            get => StudioShared.FastForwardSpeed;
            set
            {
                StudioShared.FastForwardSpeed = SourceFrameworkMath.Clamp(value, 2, 30);
                SyncSettings();
            }
        }

        public float SlowForwardSpeed
        {
            get => StudioShared.SlowForwardSpeed;
            set
            {
                StudioShared.SlowForwardSpeed = ScalarMath.Clamp(value, 0.01f, 0.9f);
                SyncSettings();
            }
        }

        public bool RestoreSettings { get; set; } = true;
        public bool AutoPauseDraft { get; set; } = true;
        public bool LaunchStudioAtBoot { get; set; } = false;
        public bool ShowStudioUpdateBanner { get; set; } = true;

        [YamlMember(Alias = "AttemptConnectStudio")]
        public bool _AttemptConnectStudio { get; set; } = true;

        [YamlIgnore]
        public bool AttemptConnectStudio
        {
            get => Enabled && _AttemptConnectStudio;
            set => _AttemptConnectStudio = value;
        }

        [YamlMember(Alias = "BetterInvincible")]
        public bool _BetterInvincible = true;

        [YamlIgnore]
        public bool BetterInvincible
        {
            get => Enabled && _BetterInvincible;
            set => _BetterInvincible = value;
        }

        public StudioEnableCondition ForceAllowAccessibilityTools = StudioEnableCondition.WhileStudioConnected;
        public GameplayEnableCondition PreventSkinModGameplayChanges = GameplayEnableCondition.DuringTAS;
        public bool HideFreezeFrames { get; set; } = false;
        public bool IgnoreGcCollect { get; set; } = true;

        private void SyncSettings() => CelesteTasModule.Instance.Driver.SendSourceSettings(StudioShared);
    }
}
