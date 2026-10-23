using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Celeste.Core.Components;
using Celeste.Everest.Modules.Runtime;
using Celeste.Game.Execution.LevelLoader;
using Celeste.Game.Execution.Levels;
using Celeste.Scene.Scenes;
using Celeste.Simulation;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed partial class SourceTasConsoleProcess
    {
        internal static readonly Regex LoadCommandRegex =
            new(@"^(load|hard|rmx2)(\d*)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private List<Action<object>> bugFixers;

        private bool IsModInstalled(string name)
            => ((ISourceEverestModulesProcessEnvironment)process).ModulesProcess.Modules.Any(module =>
                module.Metadata?.Name == name);

        private void EnterLevel(OriginalLevelLoaderScene levelLoader)
        {
            if (bugFixers == null)
            {
                bugFixers = new List<Action<object>>();
                if (IsModInstalled("PandorasBox"))
                    bugFixers.Add(entity =>
                    {
                        if (process.RuntimeTypeDiscovery.RuntimeTypeNameMatches(entity.GetType(),
                                "Celeste.Mod.PandorasBox.TileGlitcher"))
                            ((ISourceMutableEntityState)entity).Active = false;
                    });
                if (IsModInstalled("StrawberryJam2021"))
                    bugFixers.Add(entity =>
                    {
                        if (process.RuntimeTypeDiscovery.RuntimeTypeNameMatches(entity.GetType(),
                                "Celeste.Mod.StrawberryJam2021.Entities.DashBoostField"))
                            ((ISourceMutableEntityState)entity).Active = false;
                    });
            }

            if (process.Frame.Scenes.CurrentScene is OriginalLevelSceneRoot level && bugFixers.Count != 0)
                foreach (object entity in level.SourceScene.Entities)
                foreach (Action<object> bugFixer in bugFixers)
                    bugFixer(entity);
            process.Frame.Scenes.NextScene = levelLoader;
        }

        internal RoomPlayerSession SetPlayerRemainder(RoomPlayerSession player)
        {
            if (initRemainder != System.Numerics.Vector2.Zero)
            {
                player.Body.Remainder = initRemainder;
                initRemainder = System.Numerics.Vector2.Zero;
            }

            return player;
        }

        internal void AfterPlayerIntroRespawnEnd(RoomPlayerSession player)
        {
            if (initSpeed != System.Numerics.Vector2.Zero && player.Scene != null)
                ((SourceSceneState)player.Scene).OnEndOfFrame += () =>
                {
                    player.Data.Motion.Normal.Speed = initSpeed;
                    initSpeed = System.Numerics.Vector2.Zero;
                };
        }

        internal void AfterLevelEnd()
        {
            if (driver.Enabled && driver.SourceModReflection
                    .GetType("CrystallineHelper", "vitmod.ForceDashCrystal") is { } forceDashCrystal)
                driver.SourceModReflection.GetFieldInfo(forceDashCrystal, "dirToUse")
                    .Write(null, null);
        }
    }
}
