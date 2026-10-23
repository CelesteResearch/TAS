using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using StudioCommunication.Util;
using Celeste.Mods.Tas.Input;
using Celeste.Mods.Tas.Module;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;

namespace Celeste.Mods.Tas.Playback
{
    internal readonly record struct SourceTasSavestate(
        SourceTasSavestateManager Owner,
        SourceTasInputController Controller,
        int Checksum,
        bool SavedByBreakpoint)
    {
        /// SpeedrunTool slot, which is used for this save-state
        public readonly string Slot = $"{Interop.SourceTasSpeedrunToolInterop.DefaultSlot}_{Checksum}";

        public int Frame => Controller.CurrentFrameInTas;

        public int StudioLine =>
            (SavedByBreakpoint
                ? (Owner.Driver.Controller.FastForwards.GetValueOrDefault(Frame)?.StudioLine ??
                   Owner.Driver.Controller.Comments.GetValueOrDefault(Frame)?.Where(comment =>
                   {
                       var span = comment.Text.AsSpan().TrimStart();
                       return span.StartsWith("***") &&
                              span["***".Length..].Contains("s", StringComparison.OrdinalIgnoreCase);
                   }).FirstOrNull()?.StudioLine)
                : Owner.Driver.Controller.Inputs.GetValueOrDefault(Frame)?.StudioLine) ?? -1;

        /// Checks if the breakpoint is currently just commented out
        public bool BreakpointCommented =>
            SavedByBreakpoint
            && (Owner.Driver.Controller.Comments.GetValueOrDefault(Frame)?.Any(comment =>
            {
                var span = comment.Text.AsSpan().TrimStart();
                return span.StartsWith("***") && span["***".Length..].Contains("s", StringComparison.OrdinalIgnoreCase);
            }) ?? false);

        /// Check if the breakpoint has been deleted and not just commented out
        public bool BreakpointDeleted =>
            SavedByBreakpoint
            && Owner.Driver.Controller.FastForwards.GetValueOrDefault(Frame)?.SaveState != true
            && !BreakpointCommented;

        public bool Load() => Owner.Load(this);
        public void Clear() => Owner.Clear(this);
    }
}
