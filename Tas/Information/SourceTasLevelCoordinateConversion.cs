using System;
using System.Numerics;
using Celeste.Core.Mathematics;
using Celeste.Game.Execution.Engine;
using Celeste.Game.Execution.Levels;
using Celeste.Simulation.Save.Runtime;
using global::Celeste.Game.Execution;

namespace Celeste.Mods.Tas.Information
{
    internal static class SourceTasLevelCoordinateConversion
    {
        public static Vector2 ScreenToWorld(OriginalLevelSceneRoot level, TasPlaybackDriver driver, Vector2 position)
        {
            var arithmetic = ((ISourceVectorEnvironment)level.EngineContext).VectorArithmetic;
            Vector2 size = new Vector2(320f, 180f);
            Vector2 scaledSize = arithmetic.Divide(size, level.ZoomTarget);
            Vector2 offset = level.ZoomTarget != 1f
                ? Vector2ComponentDivision.Divide(level.ZoomFocusPoint - arithmetic.Divide(scaledSize, 2f),
                    size - scaledSize) * size
                : Vector2.Zero;
            float scale = level.Zoom * ((320f - level.ScreenPadding * 2f) / 320f);
            Vector2 paddingOffset = new Vector2(level.ScreenPadding, level.ScreenPadding * 9f / 16f);
            if (((ISourceCurrentSaveEnvironment)level.EngineContext).CurrentSave?.MirrorMode ?? false)
                position.X = 1920f - position.X;
            if (driver.SourceExtendedVariants.UpsideDown)
                position.Y = 1080f - position.Y;
            position = arithmetic.Divide(position, 1920f / 320f);
            position -= paddingOffset;
            position = arithmetic.Divide(position - offset, scale) + offset;
            position = level.Camera.ScreenToCamera(position);
            return position;
        }

        public static Vector2 WorldToScreen(OriginalLevelSceneRoot level, TasPlaybackDriver driver, Vector2 position)
        {
            var arithmetic = ((ISourceVectorEnvironment)level.EngineContext).VectorArithmetic;
            Vector2 size = new Vector2(320f, 180f);
            Vector2 scaledSize = arithmetic.Divide(size, level.ZoomTarget);
            Vector2 offset = level.ZoomTarget != 1f
                ? Vector2ComponentDivision.Divide(level.ZoomFocusPoint - arithmetic.Divide(scaledSize, 2f),
                    size - scaledSize) * size
                : Vector2.Zero;
            float scale = level.Zoom * ((320f - level.ScreenPadding * 2f) / 320f);
            Vector2 paddingOffset = new Vector2(level.ScreenPadding, level.ScreenPadding * 9f / 16f);
            position = level.Camera.CameraToScreen(position);
            position = (position - offset) * scale + offset;
            position += paddingOffset;
            position *= 1920f / 320f;
            if (((ISourceCurrentSaveEnvironment)level.EngineContext).CurrentSave?.MirrorMode ?? false)
                position.X = 1920f - position.X;
            if (driver.SourceExtendedVariants.UpsideDown)
                position.Y = 1080f - position.Y;
            return position;
        }

        public static Vector2 MouseToWorld(OriginalLevelSceneRoot level, TasPlaybackDriver driver,
            Vector2 mousePosition)
        {
            float viewScale = (float)EngineRuntime.ViewWidth / EngineRuntime.Width;
            var position = ScreenToWorld(level, driver,
                ((ISourceVectorEnvironment)level.EngineContext).VectorArithmetic.Divide(mousePosition, viewScale));
            return new Vector2((float)Math.Floor(position.X), (float)Math.Floor(position.Y));
        }
    }
}
