using Celeste.Mods.Tas.Input;
#nullable enable
using System;
using System.Linq;
using System.Numerics;
using StudioCommunication.Util;
using Celeste.Scene.Scenes;
using Level = Celeste.Game.Execution.Levels.OriginalLevelSceneRoot;

namespace Celeste.Mods.Tas.Information
{
    internal sealed class SourceTasInfoMouse
    {
        private readonly GameApplication process;
        private readonly TasPlaybackDriver driver;

        internal SourceTasInfoMouse(GameApplication process, TasPlaybackDriver driver)
        {
            this.process = process;
            this.driver = driver;
        }

        internal SourceTasSelectedAreaEntity? SelectedArea
        {
            get
            {
                var scene = (SourceSceneState)process.Frame.Scenes.CurrentScene;
                var type = process.SceneCatalog.Find(typeof(SourceTasSelectedAreaEntity))
                           ?? throw new InvalidOperationException(
                               "The original selected area actor has no actual type binding.");
                return scene.SourceScene.Entities.GetTrackedEntity<SourceTasSelectedAreaEntity>(type.Name);
            }
        }

        internal void Load() => process.LevelSourceHooks.AfterLoadLevel += LevelOnLoadLevel;
        internal void Unload() => process.LevelSourceHooks.AfterLoadLevel -= LevelOnLoadLevel;

        private void LevelOnLoadLevel(Level self)
        {
            var type = process.SceneCatalog.Find(typeof(SourceTasSelectedAreaEntity))
                       ?? throw new InvalidOperationException(
                           "The selected area actual source type has no catalog binding.");
            var tracked = self.SourceScene.Entities.IsTracked(type.Name);
            if (tracked && self.SourceScene.Entities.GetTrackedEntities(type.Name).IsEmpty())
            {
                self.AddEntity(new SourceTasSelectedAreaEntity(process, driver));
            }
            else if (self.SourceScene.Entities.OfType<SourceTasSelectedAreaEntity>().FirstOrDefault() == null)
            {
                self.AddEntity(new SourceTasSelectedAreaEntity(process, driver));
            }
        }

        public string MouseInfo
        {
            get
            {
                if (mouseWorldPosition == null)
                {
                    return string.Empty;
                }

                string result = $"Cursor: {mouseWorldPosition.Value.ToSimpleString(0)}";

                if (SourceTasSelectedAreaEntity.GetRect(driver) is { } selectedAreaRect &&
                    selectedAreaRect.IsNotEmpty())
                {
                    result += $"\nSelected Area: {selectedAreaRect}";
                    result += $"\nSelected Width: {SourceTasSelectedAreaEntity.GetWidth(driver)}";
                    result += $"\nSelected Height: {SourceTasSelectedAreaEntity.GetHeight(driver)}";
                    result += $"\nSelected Diagonal: {SourceTasSelectedAreaEntity.GetDiagonal(driver)}";
                }

                return result;
            }
        }

        private Vector2? mouseWorldPosition;
        private Vector2? startDragPosition;

        public void DragAndDropHud()
        {
            if (!driver.Settings.Enabled || !process.MInput.Engine.IsActive)
            {
                return;
            }

            if (!driver.SourceHotkeys.InfoHud.Check)
            {
                mouseWorldPosition = null;
                return;
            }

            if (!driver.Settings.InfoHud && !driver.SourceStudioConnected)
            {
                return;
            }

            if (process.Frame.Scenes.CurrentScene is Level level)
            {
                mouseWorldPosition =
                    SourceTasLevelCoordinateConversion.MouseToWorld(level, driver, driver.MouseInput.Position);
            }
            else
            {
                mouseWorldPosition = null;
            }

            driver.GameInfo.WatchEntities.CheckMouseButtons();

            DrawCursor(driver.MouseInput.Position);
            MoveInfoHud();
        }

        private void MoveInfoHud()
        {
            if (driver.MouseInput.Left.Pressed)
            {
                startDragPosition = driver.MouseInput.Position;
            }

            if (startDragPosition != null && !driver.MouseInput.Left.Check)
            {
                if (Math.Abs((int)(driver.MouseInput.Position.X - startDragPosition.Value.X)) > 0.1f ||
                    Math.Abs((int)(driver.MouseInput.Position.Y - startDragPosition.Value.Y)) > 0.1f)
                {
                    driver.SourceModule.SaveSettings();
                }

                startDragPosition = null;
            }

            if (startDragPosition != null && driver.MouseInput.Left.Check)
            {
                driver.Settings.InfoPosition += driver.MouseInput.PositionDelta;
            }
        }

        public void DrawCursor(Vector2 position) => driver.DrawSourceCameraCursor(position);
    }
}
