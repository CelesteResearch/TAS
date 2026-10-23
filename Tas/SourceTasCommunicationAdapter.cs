using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Input;
using Celeste.Game.Audio.Mixing;

namespace Celeste.Mods.Tas
{
    internal sealed class SourceTasCommunicationAdapter(TasPlaybackDriver driver)
        : CommunicationAdapterBase(Location.Celeste)
    {
        protected override void FullReset()
        {
            driver.StopSourceStudioCommunication();
            driver.StartSourceStudioCommunication();
        }

        protected override void OnConnectionChanged()
        {
            if (Connected)
            {
                // Stall until input initialized to avoid sending invalid hotkey data
                while (driver.SourceHotkeys.AllHotkeys == null)
                {
                    Thread.Sleep(UpdateRate);
                }

                driver.SendSourceCurrentBindings();
                driver.SendSourceSettings(driver.Settings.StudioShared);
                driver.SendSourceCommandList();
            }
        }

        protected override void HandleMessage(MessageID messageId, BinaryReader reader)
        {
            switch (messageId)
            {
                case MessageID.FilePath:
                    string path = reader.ReadString();
                    LogVerbose($"Received message FilePath: '{path}'");

                    driver.AddMainThreadAction(() => driver.SetSourceFilePath(path));
                    break;

                case MessageID.Hotkey:
                    var hotkey = (HotkeyID)reader.ReadByte();
                    bool released = reader.ReadBoolean();
                    LogVerbose($"Received message Hotkey: {hotkey} ({(released ? "released" : "pressed")})");

                    driver.SourceHotkeys.AllHotkeys[hotkey].OverrideCheck = !released;
                    break;

                case MessageID.SetCustomInfoTemplate:
                    string customInfoTemplate = reader.ReadString();
                    LogVerbose($"Received message SetCustomInfoTemplate: '{customInfoTemplate}'");

                    driver.Settings.InfoCustomTemplate = customInfoTemplate;
                    driver.SourceModule.SaveSettings();
                    driver.GameInfo.Update();
                    break;

                case MessageID.ClearWatchEntityInfo:
                    LogVerbose("Received message ClearWatchEntityInfo");

                    driver.GameInfo.WatchEntities.ClearWatchEntities();
                    driver.GameInfo.Update();
                    break;

                case MessageID.RecordTAS:
                    var fileName = reader.ReadString();
                    LogVerbose($"Received message RecordTAS: '{fileName}'");

                    ProcessRecordTAS(fileName);
                    break;

                case MessageID.RequestGameData:
                    var gameDataType = (GameDataType)reader.ReadByte();
                    object? arg = gameDataType switch
                    {
                        GameDataType.ConsoleCommand => reader.ReadBoolean(),
                        GameDataType.SettingValue => reader.ReadString(),
                        GameDataType.EvaluateInfoTemplate => reader.ReadObject<string[]>(),
                        GameDataType.CommandHash => reader.ReadObject<(string, string[], string, int)>(),
                        _ => null,
                    };
                    LogVerbose($"Received message RequestGameData: '{gameDataType}' ('{arg ?? "<null>"}')");

                    // Gathering data from the game can sometimes take a while (and cause a timeout)
                    Task.Run(() =>
                    {
                        try
                        {
                            object? gameData;
                            switch (gameDataType)
                            {
                                case GameDataType.ConsoleCommand:
                                    gameData = driver.GameData.GetConsoleCommand((bool)arg!);
                                    break;
                                case GameDataType.ModInfo:
                                    gameData = driver.GameData.GetModInfo();
                                    break;
                                case GameDataType.ExactGameInfo:
                                    gameData =
                                        ((Celeste.Everest.Runtime.Threading.ISourceEverestMainThreadEnvironment)driver
                                            .Process).MainThreadProcess
                                        .Schedule(() => driver.GameInfo.ExactStudioInfo).AsTask().GetAwaiter()
                                        .GetResult();
                                    break;
                                case GameDataType.SettingValue:
                                    gameData =
                                        ((Celeste.Everest.Runtime.Threading.ISourceEverestMainThreadEnvironment)driver
                                            .Process).MainThreadProcess
                                        .Schedule(() => driver.GameData.GetSettingValue((string)arg!)).AsTask()
                                        .GetAwaiter().GetResult();
                                    break;
                                case GameDataType.CompleteInfoCommand:
                                    gameData = driver.GameData.CreateCompleteInfoCommand();
                                    break;
                                case GameDataType.ModUrl:
                                    gameData = driver.GameData.GetModUrl();
                                    break;
                                case GameDataType.CustomInfoTemplate:
                                    gameData = !string.IsNullOrWhiteSpace(driver.Settings.InfoCustomTemplate)
                                        ? driver.Settings.InfoCustomTemplate
                                        : string.Empty;
                                    break;
                                case GameDataType.EvaluateInfoTemplate:
                                    gameData =
                                        ((Celeste.Everest.Runtime.Threading.ISourceEverestMainThreadEnvironment)driver
                                            .Process).MainThreadProcess
                                        .Schedule(() => driver.GameInfo.CustomInfoProcess.ParseTemplate((string[])arg!,
                                                driver.Settings.CustomInfoDecimals, forceAllowCodeExecution: true)
                                            .ToArray())
                                        .AsTask().GetAwaiter().GetResult();
                                    break;
                                case GameDataType.GameState:
                                    gameData =
                                        ((Celeste.Everest.Runtime.Threading.ISourceEverestMainThreadEnvironment)driver
                                            .Process).MainThreadProcess
                                        .Schedule(() => driver.GameData.GetGameState()).AsTask().GetAwaiter()
                                        .GetResult();
                                    break;
                                case GameDataType.CommandHash:
                                    (string commandName, string[] commandArgs, string filePath, int fileLine) =
                                        ((string, string[], string, int))arg!;

                                    var meta = commandName.Equals(CommandInfo.GetCommand,
                                        StringComparison.InvariantCultureIgnoreCase)
                                        ? driver.GameInfo.CustomInfoProcess.Meta
                                        : driver.GetSourceCommandMeta(commandName);
                                    if (meta == null)
                                    {
                                        // Fallback to the default implementation
                                        gameData = commandArgs[..^1].Aggregate(17,
                                            (current, commandArg) =>
                                                31 * current + 17 * commandArg.GetStableHashCode());
                                        break;
                                    }

                                    gameData = meta.GetHash(commandArgs, filePath, fileLine);
                                    break;
                                case GameDataType.LevelInfo:
                                    gameData = new LevelInfo
                                    {
                                        ModUrl = driver.GameData.GetModUrl(),
                                        IntroTime = driver.GameData.GetIntroTime(),
                                        StartingRoom = driver.GameData.GetStartingRoom(),
                                    };
                                    break;
                                case GameDataType.RequireDependency:
                                    gameData = driver.GameData.GetRequireDependency();
                                    break;

                                default:
                                    gameData = null;
                                    break;
                            }

                            QueueMessage(MessageID.GameDataResponse, writer =>
                            {
                                writer.Write((byte)gameDataType);

                                switch (gameDataType)
                                {
                                    case GameDataType.ConsoleCommand:
                                    case GameDataType.ModInfo:
                                    case GameDataType.ExactGameInfo:
                                    case GameDataType.SettingValue:
                                    case GameDataType.CompleteInfoCommand:
                                    case GameDataType.ModUrl:
                                    case GameDataType.RequireDependency:
                                    case GameDataType.CustomInfoTemplate:
                                        writer.Write((string?)gameData ?? string.Empty);
                                        break;

                                    case GameDataType.EvaluateInfoTemplate:
                                        writer.WriteObject((string[])gameData!);
                                        break;

                                    case GameDataType.GameState:
                                        writer.WriteObject((GameState?)gameData);
                                        break;

                                    case GameDataType.CommandHash:
                                        writer.Write((int)gameData!);
                                        break;

                                    case GameDataType.LevelInfo:
                                        writer.WriteObject((LevelInfo)gameData!);
                                        break;
                                }

                                LogVerbose($"Sent message GameDataResponse: {gameDataType} = '{gameData}'");
                            });
                        }
                        catch (Exception ex)
                        {
                            driver.LogSourceCommunicationException(ex, $"Failed to get game data for '{gameDataType}'");
                        }
                    });
                    break;

                case MessageID.RequestCommandAutoComplete:
                    int hash = reader.ReadInt32();
                    string commandName = reader.ReadString();
                    string[] commandArgs = reader.ReadObject<string[]>();
                    string filePath = reader.ReadString();
                    int fileLine = reader.ReadInt32();
                    LogVerbose(
                        $"Received message RequestCommandAutoComplete: '{commandName}' '{string.Join(' ', commandArgs)}' file '{filePath}' line {fileLine} ({hash})");

                    var meta = commandName.Equals(CommandInfo.GetCommand, StringComparison.InvariantCultureIgnoreCase)
                        ? driver.GameInfo.CustomInfoProcess.Meta
                        : driver.GetSourceCommandMeta(commandName);
                    if (meta == null)
                    {
                        QueueMessage(MessageID.CommandAutoComplete, writer =>
                        {
                            writer.Write(hash);
                            writer.WriteObject(Array.Empty<CommandAutoCompleteEntry>());
                            writer.Write( /*done*/true);
                        });
                        LogVerbose($"Sent message CommandAutoComplete: 0 [Command meta not found] ({hash})");
                        return;
                    }

                    List<CommandAutoCompleteEntry> entries = [];
                    bool done = false;

                    // Collect entries
                    Task.Run(() =>
                    {
                        using var enumerator = meta.GetAutoCompleteEntries(commandArgs, filePath, fileLine);
                        while (Connected)
                        {
                            try
                            {
                                if (!enumerator.MoveNext())
                                {
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                driver.LogSourceCommunicationException(ex, "Failed to collect auto-complete entries");
                                break;
                            }

                            lock (entries)
                            {
                                entries.Add(enumerator.Current);
                            }
                        }

                        done = true;
                    });
                    // Send entries
                    Task.Run(async () =>
                    {
                        CommandAutoCompleteEntry[] entriesToWrite;

                        var timeout = TimeSpan.FromSeconds(20.0f);
                        var lastWrite = DateTime.UtcNow;

                        while (Connected && !done)
                        {
                            // Individual entries shouldn't take too long to compute
                            var now = DateTime.UtcNow;
                            if (now - lastWrite > timeout)
                            {
                                QueueMessage(MessageID.CommandAutoComplete, writer =>
                                {
                                    writer.Write(hash);
                                    writer.WriteObject(Array.Empty<CommandAutoCompleteEntry>());
                                    writer.Write( /*done*/true);
                                });
                                LogVerbose($"Sent message CommandAutoComplete: 0 [timeout] ({hash})");
                                return;
                            }

                            lock (entries)
                            {
                                if (entries.Count == 0)
                                {
                                    continue;
                                }

                                entriesToWrite = entries.ToArray();
                                entries.Clear();
                                lastWrite = now;
                            }

                            QueueMessage(MessageID.CommandAutoComplete, writer =>
                            {
                                writer.Write(hash);
                                writer.WriteObject(entriesToWrite);
                                writer.Write( /*done*/false);
                            });
                            LogVerbose(
                                $"Sent message CommandAutoComplete: {entriesToWrite.Length} [incremental] ({hash})");

                            await Task.Delay(TimeSpan.FromSeconds(0.1f)).ConfigureAwait(false);
                        }

                        lock (entries)
                        {
                            entriesToWrite = entries.ToArray();
                            entries.Clear();
                        }

                        QueueMessage(MessageID.CommandAutoComplete, writer =>
                        {
                            writer.Write(hash);
                            writer.WriteObject(entriesToWrite);
                            writer.Write( /*done*/true);
                        });
                        LogVerbose($"Sent message CommandAutoComplete: {entriesToWrite.Length} [done] ({hash})");
                    });
                    break;

                case MessageID.GameSettings:
                    var settings = reader.ReadObject<GameSettings>();
                    LogVerbose("Received message GameSettings");

                    driver.Settings.StudioShared = settings;
                    driver.SourceModule.SaveSettings();
                    break;

                default:
                    LogError($"Received unknown message ID: {messageId}");
                    break;
            }
        }

        public void WriteState(StudioState state)
        {
            QueueMessage(MessageID.State, writer => writer.WriteObject(state));
            // LogVerbose("Sent message State");
        }

        public void WriteUpdateLines(Dictionary<int, string> updateLines)
        {
            QueueMessage(MessageID.UpdateLines, writer => writer.WriteObject(updateLines));
            LogVerbose($"Sent message UpdateLines: {updateLines.Count}");
        }

        public void WriteCurrentBindings(Dictionary<int, List<int>> nativeBindings)
        {
            QueueMessage(MessageID.CurrentBindings, writer => writer.WriteObject(nativeBindings));
            LogVerbose($"Sent message CurrentBindings: {nativeBindings.Count}");
        }

        public void WriteRecordingFailed(RecordingFailedReason reason)
        {
            QueueMessage(MessageID.RecordingFailed, writer => writer.Write((byte)reason));
            LogVerbose($"Sent message RecordingFailed: {reason}");
        }

        public void WriteSettings(GameSettings settings)
        {
            QueueMessage(MessageID.GameSettings, writer => writer.WriteObject(settings));
            LogVerbose("Sent message GameSettings");
        }

        public void WriteCommandList(CommandInfo[] commands)
        {
            QueueMessage(MessageID.CommandList, writer => writer.WriteObject(commands));
            LogVerbose("Sent message CommandList");
        }

        public void WriteThirdPartyPopup(string id, string title, string text)
        {
            QueueMessage(MessageID.ThirdPartyPopup, writer =>
            {
                writer.Write(id);
                ;
                writer.Write(title);
                writer.Write(text);
            });
            LogVerbose("Sent message ThirdPartyPopup");
        }

        private void ProcessRecordTAS(string fileName)
        {
            if (!driver.Recorder.Installed)
            {
                WriteRecordingFailed(RecordingFailedReason.TASRecorderNotInstalled);
                return;
            }

            if (!driver.Recorder.IsFFmpegInstalled)
            {
                WriteRecordingFailed(RecordingFailedReason.FFmpegNotInstalled);
                return;
            }

            driver.AddMainThreadAction(() =>
            {
                driver.DisableRun();
                driver.RefreshInputs();
                if (driver.RecordingTimes.IsNotEmpty())
                {
                    driver.AbortSourceTas("Can't use StartRecording/StopRecording with \"Record TAS\"");
                    return;
                }

                driver.EnableRun();

                int totalFrames = driver.Controller.Inputs.Count;
                if (totalFrames <= 0) return;

                driver.Recorder.StartRecording(fileName);
                driver.Recorder.SetDurationEstimate(totalFrames);

                if (!driver.Controller.Commands.TryGetValue(0, out var commands))
                {
                    return;
                }

                bool startsWithConsoleLoad = commands.Any(c =>
                    c.Attribute.Name.Equals("Console", StringComparison.OrdinalIgnoreCase) &&
                    c.Args.Length >= 1 &&
                    SourceTasConsoleProcess.LoadCommandRegex.Match(c.Args[0].ToLower()) is { Success: true });

                if (startsWithConsoleLoad)
                {
                    // Restart the music when we enter the level
                    driver.Process.AudioSession.SetMusic(null, startPlaying: false, allowFadeOut: false);
                    driver.Process.AudioSession.SetAmbience(null, startPlaying: false);
                    driver.Process.AudioSession.Runtime.StopGameplaySfx(immediate: true);
                }
            });
        }

        protected override void LogInfo(string message) =>
            driver.LogSourceCommunication(global::Celeste.Everest.EverestLogLevel.Info, message);

        protected override void LogVerbose(string message) =>
            driver.LogSourceCommunication(global::Celeste.Everest.EverestLogLevel.Verbose, message);

        protected override void LogError(string message) =>
            driver.LogSourceCommunication(global::Celeste.Everest.EverestLogLevel.Error, message);
    }
}
