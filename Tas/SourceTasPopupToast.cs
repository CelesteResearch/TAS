using Celeste.Core.Display;
using System;
using System.Collections.Generic;
using System.Numerics;
using Celeste.Core.Text;
using Celeste.Core.Mathematics;
using Celeste.Core.Mathematics.Easing;
using Celeste.Core.Mathematics.Extrema;
using Celeste.Core.Rendering;
using Celeste.Core.Rendering.States;
using Celeste.Everest.Diagnostics.Logging;
using Celeste.Game.Content.Dialogs;
using Celeste.Game.Menus.Loading;
using Celeste.Game.Execution.Engine;
using Celeste.Game.Execution.Graphics;
using Celeste.Graphics.Content;
using Celeste.Graphics.Display;
using global::Celeste.Game.Execution;

namespace Celeste.Mods.Tas
{
    internal sealed class SourceTasPopupToast
    {
        private const int Padding = 25;
        public const float DefaultDuration = 3f;

        public sealed class Entry
        {
            private readonly SourceTasPopupToast owner;
            public string Text;
            public PackedColor Color;
            public float Timeout;
            public float Fade = 0f;
            public bool Active => owner.entries.Contains(this);

            internal Entry(SourceTasPopupToast owner, string text, float timeout, PackedColor color)
            {
                this.owner = owner;
                Text = text;
                Timeout = timeout;
                Color = color;
            }
        }

        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;
        private readonly List<Entry> entries = new();

        internal SourceTasPopupToast(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        public Entry Show(string message, float timeout = DefaultDuration)
        {
            var entry = new Entry(this, message, timeout, PackedColor.White);
            Show(entry);
            return entry;
        }

        public Entry ShowWithColor(string message, PackedColor color, float timeout = DefaultDuration)
        {
            var entry = new Entry(this, message, timeout, color);
            Show(entry);
            return entry;
        }

        public Entry ShowAndLog(string message, float timeout = DefaultDuration,
            global::Celeste.Everest.EverestLogLevel level = global::Celeste.Everest.EverestLogLevel.Warn)
        {
            foreach (var line in message.AsSpan().EnumerateLines())
                ((IEverestLoggerProcessEnvironment)process).Logger.Log(level, "CelesteTAS", line.ToString());
            return ShowWithColor(message, level switch
            {
                global::Celeste.Everest.EverestLogLevel.Verbose => PackedColor.Purple,
                global::Celeste.Everest.EverestLogLevel.Debug => PackedColor.Blue,
                global::Celeste.Everest.EverestLogLevel.Info => PackedColor.White,
                global::Celeste.Everest.EverestLogLevel.Warn => PackedColor.Yellow,
                global::Celeste.Everest.EverestLogLevel.Error => PackedColor.Red,
                _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
            }, timeout);
        }

        public void Show(Entry entry) => entries.Add(entry);
        internal void Load(SourceTasGameplayEvents events) => events.PostRender += Render;

        private void Render()
        {
            // Settings.EnglishLanguage is the original compile-time constant "english".
            if ((process.Frame.Scenes.CurrentScene is GameLoaderScene loader && !loader.SourceLoaded)
                || !((OriginalGameGraphicsResources)((ISourceGameGraphicsEnvironment)process).GameGraphics).Loaded
                || ((IOriginalGameTextEnvironment)process).Text.Dialog?.Languages == null
                || !((IOriginalGameTextEnvironment)process).Text.Dialog.Languages.ContainsKey("english")
                || ((IOriginalGameTextEnvironment)process).Text.ActiveFont.Font == null)
                return;
            if (driver.Recorder.IsRecording) return;
            int entryCount = entries.Count;
            for (int entryIndex = 0; entryIndex < entryCount; entryIndex++)
            {
                var entry = entries[entryIndex];
                float target = entry.Timeout > 0f ? 1f : 0f;
                entry.Fade = ScalarMath.Approach(entry.Fade, target, TasPlaybackDriver.PlaybackDeltaTime * 5f,
                    ((ISourceExtremaEnvironment)process).ExtremaArithmetic);
            }

            var context = ((ISourceRenderEnvironment)process).CurrentContext;
            context.BeginBatch(new SourceRenderBatchState(SourceSpriteSortMode.Deferred,
                SourceBlendState.AlphaBlend, SourceSamplerState.LinearClamp, SourceDepthStencilState.Default,
                SourceRasterizerState.CullNone, null, EngineRuntime.ScreenMatrix));
            float yOffset = EngineRuntime.Height;
            for (int entryIndex = 0; entryIndex < entryCount; entryIndex++)
            {
                var entry = entries[entryIndex];
                var size = ((IOriginalGameTextEnvironment)process).Text.ActiveFont.Measure(entry.Text);
                yOffset -= size.Y * Math.Min(1f, entry.Fade * 2f) + Padding / 2f;
                float alpha = entry.Timeout > 0f
                    ? ((ISourceEasingEnvironment)process).EasingArithmetic.SineIn(entry.Fade)
                    : ((ISourceEasingEnvironment)process).EasingArithmetic.SineOut(entry.Fade);
                ((IOriginalGameTextEnvironment)process).Text.ActiveFont.DrawOutline(context, this, entry.Text,
                    new Vector2(Padding, yOffset), Vector2.Zero, Vector2.One,
                    PackedColor.Multiply(entry.Color, alpha), 2f,
                    PackedColor.Multiply(PackedColor.Black, alpha * alpha * alpha));
                if (entry.Fade >= 1f) entry.Timeout -= TasPlaybackDriver.PlaybackDeltaTime;
            }

            context.EndBatch();
            entries.RemoveAll(entry => entry.Timeout <= 0f && entry.Fade <= 0f);
        }
    }
}
