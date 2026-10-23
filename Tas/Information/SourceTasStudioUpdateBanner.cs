using Celeste.Core.Display;
using System;
using System.Threading.Tasks;
using System.Numerics;
using StudioCommunication.Util;
using Celeste.Core.Rendering;
using Celeste.Core.Rendering.States;
using Celeste.Core.Mathematics;
using Celeste.Core.Mathematics.Extrema;
using Celeste.Game.Menus.Loading;
using Celeste.Game.Execution.Startup;
using Celeste.Game.Execution.Graphics;
using Celeste.Game.Content.Dialogs;
using Celeste.Game.Presentation.Hud.Speedrun;
using Celeste.Graphics.Content;
using Celeste.Graphics.Content.Atlas;
using Celeste.Graphics.Display;
using Celeste.Graphics.Display.Colors;
using Celeste.Graphics.Rendering;
using Celeste.Graphics.Text;
using Celeste.Graphics.Text.BitmapFonts;
using Celeste.Mods.Tas.Module;
using Celeste.Mods.Tas.Tools;
using global::Celeste.Game.Execution;

namespace Celeste.Mods.Tas.Information
{
    internal sealed class SourceTasStudioUpdateBanner(TasPlaybackDriver driver)
    {
        private GameApplication process => driver.Process;

        private OriginalGameGraphicsResources Graphics =>
            (OriginalGameGraphicsResources)((ISourceGameGraphicsEnvironment)process).GameGraphics;

        private OriginalGameTextProcess Text => ((IOriginalGameTextEnvironment)process).Text;
        private ActiveBitmapFont ActiveFont => Text.ActiveFont;
        private ISourceRenderContext Context => ((ISourceRenderEnvironment)process).CurrentContext;

        public enum State
        {
            Inactive,
            Download,
            Install,
            Success,
            Failure,
            Launch
        }

        [SourceTasLoad]
        private static void Load()
        {
            var module = CelesteTasModule.Instance;
            TasRuntimeEnvironment.Get(module.Process)
                .InstallSourceTasStudioBannerHooks(module.Driver.StudioUpdateBanner);
            module.Driver.StudioUpdateBanner.LoadResources();
        }

        private void LoadResources()
        {
            Task.Run(async () =>
            {
                // Wait for font / dialog to be loaded
                // NOTE: 'loader.loaded' is checked instead of 'loader.dialogLoaded' since for the latter, there is a race condition with Fast-Texture-Loading not yet being done
                while ((process.Frame.Scenes.CurrentScene is GameLoaderScene loader && !loader.SourceLoaded) ||
                       !Graphics.Loaded || Text.Dialog.Languages == null ||
                       !Text.Dialog.Languages.ContainsKey("english") ||
                       Font == null)
                {
                    await Task.Delay(10).ConfigureAwait(false);
                }

                // This only gets called when the speedrun timer is constructed
                // However the banner could be displayed before that
                SpeedrunTimerState.CalculateBaseSizes(process);

                banner = Graphics.GuiAtlas["CelesteTAS/extendedStrawberryCountBG"];
                loaded = true;
            });
        }

        public State CurrentState = State.Inactive;

        public long DownloadedBytes;
        public long TotalBytes;
        public long BytesPerSecond;
        public float FadeoutTimer = -1.0f;

        private AtlasEntry banner = null!;
        private SourcePixelFont Font => Text.Fonts.Get(Text.Dialog.Languages["english"].FontFace);
        private float FontFaceSize => Text.Dialog.Languages["english"].FontFaceSize;

        private const int BannerY = 150; // Slightly under the speedrun timer
        private const int TextY = BannerY + 23;

        private const float PaddingVerySmall = 16.0f;
        private const float PaddingSmall = 32.0f;
        private const float PaddingLarge = 40.0f;

        private const float DotSpeed = 0.5f;
        private const float BannerSpeed = 2000.0f;

        private bool loaded = false;

        private float bannerWidth;

        private float dotTimer;
        private int dotCount;

        internal void Update()
        {
            if (!loaded || !driver.Settings.ShowStudioUpdateBanner || !Text.Dialog.Languages.ContainsKey("english") ||
                Font == null)
            {
                bannerWidth = ScalarMath.Approach(bannerWidth, 0.0f, BannerSpeed * process.RawDeltaTime,
                    ((ISourceExtremaEnvironment)process).ExtremaArithmetic);
                return;
            }

            dotTimer -= process.RawDeltaTime;
            if (dotTimer <= 0.0f)
            {
                dotCount = ((dotCount + 1) % 4 + 4) % 4;
                dotTimer = DotSpeed;
            }

            float targetWidth = 0.0f;
            DrawAndMeasureBannerText(ref targetWidth, draw: false);

            // Keep last state during fadeout, so don't switch directly to Inactive
            if (FadeoutTimer > 0.0f)
            {
                FadeoutTimer -= process.RawDeltaTime;
                if (FadeoutTimer <= 0.0f)
                {
                    FadeoutTimer = 0.0f;
                }
            }

            if (FadeoutTimer == 0.0f)
            {
                targetWidth = 0.0f;
            }

            bannerWidth = ScalarMath.Approach(bannerWidth, targetWidth, BannerSpeed * process.RawDeltaTime,
                ((ISourceExtremaEnvironment)process).ExtremaArithmetic);

            if (FadeoutTimer == 0.0f && bannerWidth == 0.0f)
            {
                CurrentState = State.Inactive;
                FadeoutTimer = -1.0f;
            }
        }

        internal void Render()
        {
            if (!loaded || bannerWidth <= 0.001f || !Text.Dialog.Languages.ContainsKey("english") || Font == null)
            {
                return;
            }

            Context.BeginBatch(new SourceRenderBatchState(SourceSpriteSortMode.Deferred, SourceBlendState.AlphaBlend,
                SourceSamplerState.LinearClamp, SourceDepthStencilState.None, SourceRasterizerState.CullNone, null,
                Context.Graphics.ScreenMatrix));

            float left = GameRuntime.TargetWidth - bannerWidth;

            SourceAtlasDrawing.DrawJustified(Context, this, banner, position: new Vector2(left, BannerY),
                justify: new Vector2(1.0f, 0.0f),
                color: PackedColor.White,
                scale: new Vector2(-1.0f, 1.0f));

            DrawAndMeasureBannerText(ref left, draw: true);

            Context.EndBatch();
        }

        private void DrawAndMeasureBannerText(ref float left, bool draw)
        {
            switch (CurrentState)
            {
                case State.Inactive:
                    break;

                case State.Download:
                    left += PaddingSmall;

                    DrawText(ref left,
                        $"Downloading Celeste TAS Studio v{SourceTasStudioHelper.CurrentStudioVersion}{new string('.', dotCount)}",
                        $"Downloading Celeste TAS Studio v{SourceTasStudioHelper.CurrentStudioVersion}...", 1.0f,
                        PackedColor.White, draw);

                    const float progressSize = 0.8f;

                    left += PaddingLarge;

                    (string downloadedAmount, string downloadedSuffix) =
                        DownloadedBytes.HumanReadableBytes(decimals: 2);
                    DrawDecimal(ref left, downloadedAmount, progressSize, draw);
                    left += PaddingVerySmall * progressSize;
                    DrawText(ref left, downloadedSuffix, downloadedSuffix, 0.75f * progressSize,
                        SourceHexColor.HexToColor("7a6f6d"), draw);

                    left += PaddingSmall * progressSize;
                    DrawText(ref left, "/", "/", progressSize, SourceHexColor.HexToColor("7a6f6d"), draw);
                    left += PaddingSmall * progressSize;

                    (string totalAmount, string totalSuffix) = TotalBytes.HumanReadableBytes(decimals: 2);
                    DrawDecimal(ref left, totalAmount, progressSize, draw);
                    left += PaddingVerySmall * progressSize;
                    DrawText(ref left, totalSuffix, totalSuffix, 0.75f * progressSize,
                        SourceHexColor.HexToColor("7a6f6d"),
                        draw);

                    left += PaddingLarge * progressSize;

                    (string speedAmount, string speedSuffix) = BytesPerSecond.HumanReadableBytes(decimals: 2);
                    DrawText(ref left, "(", "(", 0.75f * progressSize, SourceHexColor.HexToColor("7a6f6d"), draw);
                    DrawDecimal(ref left, speedAmount, progressSize, draw);
                    left += PaddingVerySmall * progressSize;
                    DrawText(ref left, speedSuffix + "/s)", speedSuffix + "/s)", 0.75f * progressSize,
                        SourceHexColor.HexToColor("7a6f6d"), draw);

                    left += PaddingLarge * progressSize;

                    string progress = ((float)DownloadedBytes / (float)TotalBytes * 100.0f).ToString("0.00");
                    DrawText(ref left, "[", "[", 0.75f * progressSize, SourceHexColor.HexToColor("7a6f6d"), draw);
                    DrawDecimal(ref left, progress, progressSize, draw);
                    left += PaddingVerySmall * progressSize;
                    DrawText(ref left, "%]", "%]", 0.75f * progressSize, SourceHexColor.HexToColor("7a6f6d"), draw);

                    left += PaddingSmall;
                    break;

                case State.Install:
                    left += PaddingSmall;
                    DrawText(ref left,
                        $"Installing Celeste TAS Studio v{SourceTasStudioHelper.CurrentStudioVersion}{new string('.', dotCount)}",
                        $"Installing Celeste TAS Studio v{SourceTasStudioHelper.CurrentStudioVersion}...", 1.0f,
                        PackedColor.White, draw);
                    left += PaddingSmall;
                    break;

                case State.Success:
                    left += PaddingSmall;
                    DrawText(ref left,
                        $"Celeste TAS Studio v{SourceTasStudioHelper.CurrentStudioVersion} successfully installed",
                        $"Celeste TAS Studio v{SourceTasStudioHelper.CurrentStudioVersion} successfully installed",
                        1.0f,
                        PackedColor.LightGreen, draw);
                    left += PaddingSmall;
                    break;

                case State.Failure:
                    left += PaddingSmall;
                    DrawText(ref left,
                        $"Celeste TAS Studio v{SourceTasStudioHelper.CurrentStudioVersion} install failed!",
                        $"Celeste TAS Studio v{SourceTasStudioHelper.CurrentStudioVersion} install failed!", 1.0f,
                        PackedColor.IndianRed, draw);
                    left += PaddingSmall;
                    break;

                case State.Launch:
                    left += PaddingSmall;
                    DrawText(ref left, $"Launching Celeste TAS Studio{new string('.', dotCount)}",
                        "Launching Celeste TAS Studio...", 1.0f, PackedColor.LightSkyBlue, draw);
                    left += PaddingSmall;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DrawText(ref float left, string text, string measureText, float scale, PackedColor color,
            bool draw = true)
        {
            if (draw)
            {
                ActiveFont.Font.DrawCommonBaseline(Context, this, text, ActiveFont.BaseSize,
                    position: new Vector2(left, TextY /* + 15.0f * scale*/),
                    justify: Vector2.Zero,
                    scale: new Vector2(scale),
                    color,
                    stroke: 2.0f * scale,
                    strokeColor: PackedColor.Black,
                    edgeDepth: 0.0f,
                    edgeColor: PackedColor.Transparent);
            }

            float fullWidth = ActiveFont.Measure(measureText).X * scale;
            left += fullWidth;
        }

        private void DrawDecimal(ref float left, string text, float scale, bool draw = true)
        {
            float s = scale;
            var baseColor = PackedColor.White;
            var smallColor = PackedColor.LightGray;

            foreach (char c in text)
            {
                if (c == '.')
                {
                    s = scale * 0.7f;
                    // y -= 5.0f * scale;
                }

                var color = (c == ':' || c == '.' || s < scale) ? smallColor : baseColor;
                float advance = (c is ':' or '.'
                    ? SpeedrunTimerState.GetMetrics(process).SpacerWidth
                    : SpeedrunTimerState.GetMetrics(process).NumberWidth) * s;

                if (draw)
                {
                    Font.DrawCommonBaseline(Context, this, c.ToString(), FontFaceSize,
                        position: new Vector2(left, TextY),
                        justify: new Vector2(0.0f, 0.0f),
                        scale: new Vector2(s),
                        color,
                        stroke: 2.0f,
                        strokeColor: PackedColor.Black,
                        edgeDepth: 0.0f,
                        edgeColor: PackedColor.Transparent);
                }

                left += advance;
            }
        }
    }
}
