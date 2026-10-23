using Celeste.Input.Devices.Keyboard;
using Celeste.Input.Devices.GamePads;
using System;
using Celeste.Core.Reflection.Members;
using Celeste.Everest.Reflection;

namespace Celeste.Mods.Tas
{
// The original Hotkeys snapshots are physical SDK state, not MInput TAS state.
    public sealed partial class SourceTasHotkeys
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private readonly SourceTasModReflection reflection;
        private readonly Lazy<ReflectedMember> celesteNetInstance;
        private readonly Lazy<ReflectedMember> celesteNetContext;
        private readonly Lazy<ReflectedMember> celesteNetChat;
        private readonly Lazy<ReflectedMember> celesteNetActive;
        internal SourceKeyboardState KeyboardState;
        internal SourceGamePadState GamePadState;

        public SourceTasHotkeys(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process ?? throw new ArgumentNullException(nameof(process));
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            reflection = driver.SourceModReflection;
            celesteNetInstance = new(() =>
                GetOptionalField("Celeste.Mod.CelesteNet.Client.CelesteNetClientModule", "Instance"));
            celesteNetContext = new(() =>
                GetOptionalField("Celeste.Mod.CelesteNet.Client.CelesteNetClientModule", "Context"));
            celesteNetChat =
                new(() => GetOptionalField("Celeste.Mod.CelesteNet.Client.CelesteNetClientContext", "Chat"));
            celesteNetActive = new(() =>
            {
                var type = reflection.GetType("CelesteNet.Client",
                    "Celeste.Mod.CelesteNet.Client.Components.CelesteNetChatComponent");
                return type == null ? null : reflection.GetPropertyInfo(type, "Active");
            });
        }

        private SourceGamePadState GetGamePadState()
        {
            for (int i = 0; i < 4; i++)
            {
                var state = process.FnaGamePads.GetState((SourcePlayerIndex)i);
                if (state.IsConnected) return state;
            }

            return default;
        }

        // Called at the original Hotkeys.UpdateMeta readpoint, after input-use gates.
        private void ReadPhysicalStates()
        {
            KeyboardState = process.KeyboardState.GetState();
            GamePadState = GetGamePadState();
        }
    }
}
