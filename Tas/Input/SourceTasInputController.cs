using Celeste.Core.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Core.Diagnostics.Tas;
using Celeste.Core.Mathematics;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Everest.Modules.Runtime;
using Celeste.Mods.Tas.Commands;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Input
{
    /// Manages inputs, commands, etc. for the current TAS file
    public class SourceTasInputController
    {
        private readonly TasPlaybackDriver driver;
        internal SourceTasInputController(TasPlaybackDriver driver) => this.driver = driver;

        [SourceTasInitialize]
        private static void Initialize()
        {
            CelesteTasModule.Instance.Attributes.CollectAllMethods<SourceTasClearInputsAttribute>();
            CelesteTasModule.Instance.Attributes.CollectAllMethods<SourceTasParseFileEndAttribute>();
            CelesteTasModule.Instance.Attributes.CollectAllMethods<SourceTasFileChangedAttribute>();
        }

        private readonly Dictionary<string, FileSystemWatcher> watchers = new();

        public readonly List<SourceTasInputFrame> Inputs = [];
        public readonly SortedDictionary<int, List<SourceTasCommand>> Commands = new();
        internal bool HasRealTimeDisplayCommand { get; private set; }
        internal bool HasGameTimeDisplayCommand { get; private set; }

        private void IncludeTimeDisplayCommand(SourceTasCommand command)
        {
            HasRealTimeDisplayCommand |= command.Is("RealTime") || command.Is("MidwayRealTime");
            HasGameTimeDisplayCommand |= command.Is("FileTime") || command.Is("ChapterTime")
                                                                || command.Is("MidwayFileTime") ||
                                                                command.Is("MidwayChapterTime");
        }

        private void RebuildTimeDisplayCommandFlags()
        {
            HasRealTimeDisplayCommand = false;
            HasGameTimeDisplayCommand = false;
            foreach (var commands in Commands.Values)
            foreach (var command in commands)
                IncludeTimeDisplayCommand(command);
        }

        public readonly SortedDictionary<int, List<SourceTasComment>> Comments = new();
        internal readonly SortedDictionary<int, SourceTasFastForward> FastForwards = new();
        internal readonly SortedDictionary<int, SourceTasFastForward> FastForwardLabels = new();

        public SourceTasInputFrame? Previous => Inputs.GetValueOrDefault(CurrentFrameInTas - 1);
        public SourceTasInputFrame? Current => Inputs.GetValueOrDefault(CurrentFrameInTas);
        public SourceTasInputFrame? Next => Inputs.GetValueOrDefault(CurrentFrameInTas + 1);

        public int CurrentFrameInTas { get; set; } = 0;
        public int CurrentFrameInInput { get; set; } = 0;
        public int CurrentParsingFrame => Inputs.Count;

        public List<SourceTasCommand> CurrentCommands => Commands.GetValueOrDefault(CurrentFrameInTas) ?? [];
        public List<SourceTasComment> CurrentComments => Comments.GetValueOrDefault(CurrentFrameInTas) ?? [];

        internal SourceTasFastForward? CurrentFastForward
        {
            get
            {
                if (FastForwards.Count == 0) return NextLabelFastForward;
                SourceTasFastForward? firstFuture = null, last = null;
                bool foundFuture = false;
                foreach (var entry in FastForwards)
                {
                    last = entry.Value;
                    if (entry.Key <= CurrentFrameInTas) continue;
                    if (entry.Value.ForceStop) return entry.Value;
                    if (!foundFuture)
                    {
                        firstFuture = entry.Value;
                        foundFuture = true;
                    }
                }

                return NextLabelFastForward ?? firstFuture ?? last;
            }
        }

        public bool HasFastForward => CurrentFastForward is { } forward && forward.Frame > CurrentFrameInTas;

        internal SourceTasFastForward? NextLabelFastForward;

        /// Indicates whether the current TAS file needs to be reparsed before running
        public bool NeedsReload = true;

        private bool pendingStartupPreload;

        internal bool StartupPreloadReady => driver.Process.AreaTable.Areas != null &&
                                             driver.Process.Frame.Scenes.CurrentScene is not
                                                 Celeste.Game.Menus.Loading.GameLoaderScene { SourceLoaded: false };

        /// All files involved in the current TAS
        public readonly HashSet<string> UsedFiles = [];

        private const int InvalidChecksum = -1;
        private int checksum = InvalidChecksum;

        /// Current checksum of the TAS, used to increment RecordCount
        public int Checksum => checksum == InvalidChecksum ? checksum = CalcChecksum(Inputs.Count - 1) : checksum;

        /// Whether the controller can be advanced to a next frame
        public bool CanPlayback => CurrentFrameInTas < Inputs.Count;

        internal void SetSourceParseEndBreakpoint(int frame) =>
            FastForwards[frame] = new SourceTasFastForward(frame, StudioLine: 0, FilePath: string.Empty, FileLine: 0);

        /// Whether the TAS should be paused on this frame
        public bool Break
        {
            get
            {
                if (CurrentFastForward?.Frame == CurrentFrameInTas) return true;
                foreach (var entry in FastForwards)
                    if (entry.Key == CurrentFrameInTas && entry.Value.ForceStop)
                        return true;
                return false;
            }
        }

        // TODO: Convert into parameter while parsing
        internal bool EnableBreakpointParsing = true;

        private string DefaultFilePath =>
            Path.Combine(((IEverestModuleRuntimeEnvironment)driver.Process).PathEverest, "Celeste.tas");

        private string filePath = string.Empty;

        public string FilePath
        {
            get
            {
                var path = !string.IsNullOrEmpty(filePath) ? filePath : DefaultFilePath;

                // Ensure path exists
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, string.Empty);
                }

                return path;
            }
            set
            {
                if (filePath == value)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(value))
                {
                    CancelPendingStartupPreload();
                    filePath = string.Empty;
                    return;
                }

                filePath = Path.GetFullPath(value);
                if (!File.Exists(filePath))
                {
                    filePath = DefaultFilePath;
                }

                if (driver.Running)
                {
                    driver.DisableRunLater();
                }

                // Preload the TAS file
                Stop();
                Clear();
                RefreshInputs();
            }
        }

        private void CancelPendingStartupPreload()
        {
            pendingStartupPreload = false;
            if (!driver.Running && !StartupPreloadReady) driver.DisableRunLater();
        }

        internal void RefreshPendingStartupPreload()
        {
            if (pendingStartupPreload) RefreshInputs();
        }

        /// Re-parses the TAS file if necessary
        public void RefreshInputs(bool forceRefresh = false)
        {
            if (!NeedsReload && !forceRefresh)
            {
                return; // Already up-to-date
            }

            if (!StartupPreloadReady)
            {
                NeedsReload = true;
                pendingStartupPreload = true;
                return;
            }

            pendingStartupPreload = false;
            driver.LogSourceTas(global::Celeste.Everest.EverestLogLevel.Debug, "Refreshing inputs...");

            long disableRunRequestRevision = driver.DisableRunRequestRevision;
            int lastChecksum = Checksum;
            bool firstRun = UsedFiles.IsEmpty();

            Clear();
            if (ReadFile(FilePath))
            {
                if (driver.DisableRunRequestRevision != disableRunRequestRevision)
                {
                    // The TAS contains something invalid
                    Clear();
                    driver.DisableRunLater();
                }
                else
                {
                    CelesteTasModule.Instance.Attributes.Invoke<SourceTasParseFileEndAttribute>();
                    RebuildTimeDisplayCommandFlags();
                    if (driver.DisableRunRequestRevision != disableRunRequestRevision)
                    {
                        // The TAS contains something invalid
                        Clear();
                        driver.DisableRunLater();
                    }
                    else
                    {
                        NeedsReload = false;
                        StartWatchers();

                        if (!firstRun && lastChecksum != Checksum)
                        {
                            driver.SourceMetadataCommands.UpdateRecordCount(this);
                        }
                    }
                }
            }
            else
            {
                // Something failed while trying to parse
                Clear();
            }

            CurrentFrameInTas = Math.Min(Inputs.Count, CurrentFrameInTas);
        }

        /// Moves the controller 1 frame forward, updating inputs and triggering commands
        public void AdvanceFrame(out bool couldPlayback)
        {
            RefreshInputs();

            couldPlayback = CanPlayback;

            foreach (var command in CurrentCommands)
            {
                if ((command.Attribute.ExecuteTiming & SourceTasExecuteTiming.Runtime) != 0
                    && (!driver.EnforceLegal.EnabledWhenRunning || command.Attribute.LegalInFullGame ||
                        SourceTasSetProcess.IsPresentationCommand(command))
                   )
                {
                    var previousSource = driver.ActiveCommandSource;
                    driver.ActiveCommandSource =
                        new SourceTasSourceLocation(command.FilePath, command.FileLine, command.StudioLine);
                    try
                    {
                        command.Invoke();
                    }
                    finally
                    {
                        driver.ActiveCommandSource = previousSource;
                    }
                }

                // These commands insert new inputs dynamically
                // Since the generated inputs might've changed, the current position in the TAS need to be updated appropriately
                if (command.Attribute.Name is "SaveAndQuitReenter" or "SelectCampaign")
                {
                    var newCommand = Commands.Values
                        .SelectMany(cmds => cmds)
                        .FirstOrDefault(cmd => cmd.FileLine == command.FileLine && cmd.FilePath == command.FilePath);

                    CurrentFrameInTas = newCommand.Frame;
                }
            }

            // Validate that room labels are correct, to catch desyncs and ensure they're not accidentally messed up
            // Check comments of previous frame, since during the first frame of a transition, the room name won't be updated yet
            // However semantically, it is perfectly valid to do so, from a TAS perspective
            var logLevel = driver.SourceSyncChecker.ValidateRoomLabels
                ? global::Celeste.Everest.EverestLogLevel.Error
                : global::Celeste.Everest.EverestLogLevel.Warn;

            foreach (var comment in Comments.GetValueOrDefault(CurrentFrameInTas - 1) ?? [])
            {
                if (CommentLine.RoomLabelRegex.Match($"#{comment.Text}") is not { Success: true } match)
                {
                    continue;
                }

                if (driver.GetSourceCurrentSceneSession() is { } session)
                {
                    var labelSpan = comment.Text.AsSpan().Slice(match.Groups[1].Index - 1, match.Groups[1].Length)
                        .Trim();
                    var levelSpan = session.Level.AsSpan().Trim();
                    if (labelSpan.SequenceEqual(levelSpan))
                    {
                        continue;
                    }

                    ReportMessage(comment.Source,
                        $"Room label 'lvl_{labelSpan.ToString()}' does not match actual name 'lvl_{levelSpan.ToString()}'",
                        level: logLevel);
                }
                else
                {
                    ReportMessage(comment.Source, $"Found room label '#{comment.Text}' outside of level",
                        level: logLevel);
                }

                if (driver.SourceSyncChecker.ValidateRoomLabels)
                {
                    driver.DisableRunLater();
                }
            }

            if (!CanPlayback)
            {
                return;
            }

            driver.SourceExportGameInfo.ExportInfo();
            driver.SourceStunPauseCommand.UpdateSimulateSkipInput();
            driver.FeedSourceInput(Current!);

            // Increment if it's still the same input
            if (CurrentFrameInInput == 0 || Current!.StudioLine == Previous!.StudioLine &&
                Current.RepeatIndex == Previous.RepeatIndex && Current.FrameOffset == Previous.FrameOffset)
            {
                CurrentFrameInInput++;
            }
            else
            {
                CurrentFrameInInput = 1;
            }

            CurrentFrameInTas++;
        }

        /// Parses the file and adds the inputs / commands to the TAS
        public bool ReadFile(string path, int startLine = 0, int endLine = int.MaxValue, int studioLine = 0,
            int repeatIndex = 0, int repeatCount = 0)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return false;
                }

                UsedFiles.Add(path);
                ReadLines(File.ReadLines(path).Take(endLine), path, startLine, studioLine, repeatIndex, repeatCount);

                return true;
            }
            catch (Exception e)
            {
                driver.LogSourceTas(global::Celeste.Everest.EverestLogLevel.Error, e.ToString());
                return false;
            }
        }

        /// Parses the lines and adds the inputs / commands to the TAS
        public void ReadLines(IEnumerable<string> lines, string path, int startLine, int studioLine, int repeatIndex,
            int repeatCount, bool lockStudioLine = false)
        {
            int fileLine = 0;
            foreach (string readLine in lines)
            {
                fileLine++;
                if (fileLine < startLine)
                {
                    continue;
                }

                if (!ReadLine(readLine, path, fileLine, studioLine, repeatIndex, repeatCount))
                {
                    return;
                }

                if (path == FilePath && !lockStudioLine)
                {
                    studioLine++;
                }
            }

            // Add a hidden label at the of the text block
            if (path == FilePath)
            {
                FastForwardLabels[CurrentParsingFrame] =
                    new SourceTasFastForward(CurrentParsingFrame, studioLine, path, fileLine);
            }
        }

        /// Parses the line and adds the inputs / commands to the TAS
        public bool ReadLine(string line, string path, int fileLine, int studioLine, int repeatIndex = 0,
            int repeatCount = 0)
        {
            string lineText = line.Trim();

            // Commands might insert inputs, which would offset CurrentParsingFrame to after the command, instead of before
            int commandParsingFrame = CurrentParsingFrame;
            if (driver.CommandRegistry.TryParse(path, fileLine, lineText, commandParsingFrame, studioLine,
                    out SourceTasCommand command))
            {
                if (!Commands.TryGetValue(commandParsingFrame, out var commands))
                {
                    Commands[commandParsingFrame] = commands = new List<SourceTasCommand>();
                }

                commands.Add(command);
                IncludeTimeDisplayCommand(command);
                var previousSource = driver.ActiveCommandSource;
                driver.ActiveCommandSource =
                    new SourceTasSourceLocation(command.FilePath, command.FileLine, command.StudioLine);
                try
                {
                    command.Setup(driver.CommandRegistry);
                }
                finally
                {
                    driver.ActiveCommandSource = previousSource;
                }

                if (command.Is("Play"))
                {
                    // Workaround for the 'Play' command:
                    // It needs to stop reading the current file when it's done to prevent recursion
                    return false;
                }
            }
            else if (FastForwardLine.TryParse(lineText, out var fastForwardLine))
            {
                if (!EnableBreakpointParsing)
                {
                    return true;
                }

                var fastForward =
                    new SourceTasFastForward(CurrentParsingFrame, studioLine, path, fileLine, fastForwardLine);
                if (FastForwards.TryGetValue(CurrentParsingFrame, out var oldSourceTasFastForward) &&
                    oldSourceTasFastForward.SaveState && !fastForward.SaveState)
                {
                    // ignore
                }
                else
                {
                    FastForwards[CurrentParsingFrame] = fastForward;
                }
            }
            else if (lineText.StartsWith("#"))
            {
                if (CommentLine.IsLabel(lineText))
                {
                    FastForwardLabels[CurrentParsingFrame] =
                        new SourceTasFastForward(CurrentParsingFrame, studioLine, path, fileLine);
                }

                if (!Comments.TryGetValue(CurrentParsingFrame, out var comments))
                {
                    Comments[CurrentParsingFrame] = comments = [];
                }

                comments.Add(new SourceTasComment(CurrentParsingFrame, path, fileLine, studioLine, lineText));
            }
            else if (!driver.SourceAutoInputCommand.TryInsert(path, fileLine, lineText, studioLine, repeatIndex,
                         repeatCount))
            {
                AddFrames(lineText, path, fileLine, studioLine, repeatIndex, repeatCount);
            }

            return true;
        }

        /// Parses the input line and adds it to the TAS
        public void AddFrames(string line, string path, int fileLine, int studioLine, int repeatIndex = 0,
            int repeatCount = 0, int frameOffset = 0, SourceTasCommand? parentCommand = null)
        {
            if (SourceTasInputFrame.TryParse(driver.SourceAnalog, line, path, fileLine, studioLine,
                    Inputs.LastOrDefault(),
                    out var inputFrame, repeatIndex, repeatCount, frameOffset, parentCommand))
            {
                AddFrames(inputFrame);
            }
        }

        /// Adds the inputs to the TAS
        public void AddFrames(SourceTasInputFrame inputFrame)
        {
            for (int i = 0; i < inputFrame.Frames; i++)
            {
                Inputs.Add(inputFrame);
            }

            driver.SourceLibTasHelper.WriteLibTasFrame(inputFrame);
        }

        /// Fast-forwards to the next label / breakpoint
        public void FastForwardToNextLabel()
        {
            NextLabelFastForward = null;
            RefreshInputs();

            var next = FastForwardLabels.FirstOrDefault(pair => pair.Key > CurrentFrameInTas).Value;
            if (next != null && HasFastForward && CurrentFastForward is { } last && next.Frame > last.Frame)
            {
                // Forward to another breakpoint in-between instead
                NextLabelFastForward = last;
            }
            else
            {
                NextLabelFastForward = next;
            }

            driver.NextState = SourceTasManagerState.Running;
        }

        /// Stops execution of the current TAS and resets state
        public void Stop()
        {
            CancelPendingStartupPreload();
            CurrentFrameInTas = 0;
            CurrentFrameInInput = 0;
            NextLabelFastForward = null;
        }

        /// Clears all parsed data for the current TAS
        public void Clear()
        {
            CancelPendingStartupPreload();
            Inputs.Clear();
            Commands.Clear();
            HasRealTimeDisplayCommand = false;
            HasGameTimeDisplayCommand = false;
            Comments.Clear();
            FastForwards.Clear();
            FastForwardLabels.Clear();

            foreach (var watcher in watchers.Values)
            {
                watcher.Dispose();
            }

            watchers.Clear();
            UsedFiles.Clear();

            checksum = InvalidChecksum;
            NeedsReload = true;

            CelesteTasModule.Instance.Attributes.Invoke<SourceTasClearInputsAttribute>();
        }

        internal void ReportError(SourceTasSourceLocation src, string message, bool log = true,
            float duration = SourceTasPopupToast.DefaultDuration)
        {
            ReportMessage(src, message, level: global::Celeste.Everest.EverestLogLevel.Error, duration: duration);
            driver.DisableRunLater();
        }

        internal void ReportWarning(SourceTasSourceLocation src, string message, bool log = true,
            float duration = SourceTasPopupToast.DefaultDuration)
        {
            ReportMessage(src, message, level: global::Celeste.Everest.EverestLogLevel.Warn);
        }

        internal void ReportMessage(SourceTasSourceLocation src, string message,
            global::Celeste.Everest.EverestLogLevel level, bool log = false,
            float duration = SourceTasPopupToast.DefaultDuration)
        {
#if DEBUG
        // Always log in debug builds
        log = true;
#endif

            if (log || level == global::Celeste.Everest.EverestLogLevel.Error)
            {
                string logText = $"'{src.FilePath}' line {src.FileLine}: {message}";
                foreach (var line in logText.AsSpan().EnumerateLines())
                {
                    driver.LogSourceTas(level, line.ToString());
                }
            }

            driver.ShowSourcePopupToast($"""
                                         '{Path.GetFileName(src.FilePath)}' line {src.FileLine}:
                                         {message}
                                         """, duration);
        }

        /// Create file-system-watchers for all TAS-files used, to detect changes
        public void StartWatchers()
        {
            if (driver.SourceSyncChecker.Active)
            {
                return; // Avoid reloading TASes during sync-check
            }

            foreach (string path in UsedFiles)
            {
                string fullPath = Path.GetFullPath(path);

                // Watch TAS file
                CreateWatcher(fullPath);
            }

            void CreateWatcher(string path)
            {
                if (watchers.ContainsKey(path))
                {
                    return;
                }

                var watcher = new FileSystemWatcher
                {
                    Path = Path.GetDirectoryName(path)!,
                    Filter = Path.GetFileName(path),
                    NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite,
                };

                watcher.Changed += OnTasFileChanged;
                watcher.Created += OnTasFileChanged;
                watcher.Deleted += OnTasFileChanged;
                watcher.Renamed += OnTasFileChanged;

                try
                {
                    watcher.EnableRaisingEvents = true;
                    driver.LogSourceTas(global::Celeste.Everest.EverestLogLevel.Verbose,
                        $"Started watching '{path}' for changes...");
                }
                catch (Exception e)
                {
                    driver.LogSourceTas(global::Celeste.Everest.EverestLogLevel.Error,
                        $"Failed watching folder: {watcher.Path}, filter: {watcher.Filter}\n{e}");
                    watcher.Dispose();
                    return;
                }

                watchers[path] = watcher;
            }

            void OnTasFileChanged(object sender, FileSystemEventArgs e)
            {
                driver.LogSourceTas(global::Celeste.Everest.EverestLogLevel.Verbose,
                    $"TAS file changed: {e.FullPath} - {e.ChangeType}");
                NeedsReload = true;

                CelesteTasModule.Instance.Attributes.Invoke<SourceTasFileChangedAttribute>();
            }
        }

        /// Calculate a checksum until the specified frame
        public int CalcChecksum(int upToFrame)
        {
            var hash = new HashCode();
            hash.Add(filePath);

            upToFrame = Celeste.Core.Mathematics.Framework.SourceFrameworkMath.Clamp(upToFrame, 0, Inputs.Count);
            for (int i = 0; i < upToFrame; i++)
            {
                hash.Add(Inputs[i]);

                if (Commands.GetValueOrDefault(i) is { } commands)
                {
                    foreach (var command in commands.Where(command => command.Attribute.CalcChecksum))
                    {
                        hash.Add(command.LineText);
                    }
                }
            }

            return hash.ToHashCode();
        }

        public SourceTasInputController Clone()
        {
            var clone = new SourceTasInputController(driver)
            {
                filePath = filePath,
                checksum = checksum,

                NeedsReload = NeedsReload,
                CurrentFrameInTas = CurrentFrameInTas,
                CurrentFrameInInput = CurrentFrameInInput
            };

            clone.Inputs.AddRange(Inputs);

            foreach (int frame in Commands.Keys)
            {
                clone.Commands[frame] = [.. Commands[frame]];
            }

            clone.RebuildTimeDisplayCommandFlags();

            foreach (int frame in Comments.Keys)
            {
                clone.Comments[frame] = [.. Comments[frame]];
            }

            foreach ((int line, var fastForward) in FastForwards)
            {
                clone.FastForwards.Add(line, fastForward);
            }

            foreach ((int line, var fastForward) in FastForwardLabels)
            {
                clone.FastForwardLabels.Add(line, fastForward);
            }

            foreach (var file in UsedFiles)
            {
                clone.UsedFiles.Add(file);
            }

            return clone;
        }

        public void CopyProgressFrom(SourceTasInputController other)
        {
            CurrentFrameInTas = other.CurrentFrameInTas;
            CurrentFrameInInput = other.CurrentFrameInInput;
        }
    }
}
