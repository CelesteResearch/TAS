using Celeste.Input.Devices.Keyboard;
using System;
using System.Collections.Generic;
using Keys = Celeste.Input.Devices.Keyboard.SourceKeys;
using StudioCommunication;
using System.Linq;
using System.Threading.Tasks;
using Celeste.Mods.Tas.Input;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed class SourceTasPressProcess
    {
        internal static System.Type SourcePressMetaProviderType => typeof(PressMeta);

        private readonly TasPlaybackDriver driver;
        internal SourceTasPressProcess(TasPlaybackDriver driver) => this.driver = driver;

        private class PressMeta : ISourceTasCommandMeta
        {
            public string Insert => $"Press{CommandInfo.Separator}[0;Key1{CommandInfo.Separator}Key2...]";
            public bool HasArguments => true;

            public IEnumerator<CommandAutoCompleteEntry> GetAutoCompleteEntries(string[] args, string filePath,
                int fileLine)
            {
                if (args.Length != 1)
                {
                    yield break;
                }

                foreach (var key in (Keys[])Enum.GetValues(typeof(Keys)))
                {
                    yield return new CommandAutoCompleteEntry { Name = key.ToString(), Extra = "Keys", IsDone = true };
                }
            }
        }

        public readonly HashSet<Keys> PressKeys = new();

        // "Press, Key1, Key2...",
        [SourceTasCommand("Press", MetaDataProvider = typeof(PressMeta))]
        internal void Press(CommandLine commandLine, int studioLine, string filePath, int fileLine)
        {
            string[] args = commandLine.Arguments;
            if (args.IsEmpty())
            {
                return;
            }

            foreach (string key in args)
            {
                if (!Enum.TryParse(key, true, out Keys keys))
                {
                    driver.AbortSourceTas($"{key} is not a valid key");
                    return;
                }

                PressKeys.Add(keys);
            }
        }

        [SourceTasDisableRun]
        private static void SourceDisableRun() => CelesteTasModule.Instance.Driver.SourcePressCommand.DisableRun();

        internal void DisableRun()
        {
            PressKeys.Clear();
        }

        public HashSet<Keys> GetKeys()
        {
            HashSet<Keys> result = new(PressKeys);

            if (driver.Controller.Current != driver.Controller.Next)
            {
                PressKeys.Clear();
            }

            return result;
        }
    }
}
