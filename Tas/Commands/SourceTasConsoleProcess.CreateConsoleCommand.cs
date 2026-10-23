using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using StudioCommunication;
using Celeste.Game.Content.Maps;
using Celeste.Game.Minigames.CelesteClassic.Emulation;
using Celeste.Game.Execution.Levels;
using Celeste.Simulation;

namespace Celeste.Mods.Tas.Commands
{
    internal sealed partial class SourceTasConsoleProcess
    {
        internal string CreateConsoleCommand(bool simple)
        {
            if (process.Frame.Scenes.CurrentScene is ClassicConsoleRuntime { Game.room: var room })
            {
                return $"console pico {room.X} {room.Y}";
            }

            if (process.Frame.Scenes.CurrentScene is not OriginalLevelSceneRoot level)
            {
                return string.Empty;
            }

            var area = process.AreaKeys.GetSessionArea(level.Session);
            string mode = area.Mode switch
            {
                0 => "load",
                1 => "hard",
                2 => "rmx2",
                _ => throw new ArgumentOutOfRangeException()
            };

            // Only use IDs for vanilla levels
            string id = area.ID <= 10 ? area.ID.ToString() : process.AreaKeys.GetSID(area);
            List<string> values = ["console", mode, id];

            if (!simple)
            {
                if (level.Scheduler.Entities.GetTrackedEntity<RoomPlayerSession>(
                        typeof(global::Celeste.Simulation.RoomPlayerSession).FullName) is { } player)

                {
                    if (player.Body.Remainder == Vector2.Zero && player.Data.Motion.Normal.Speed == Vector2.Zero)
                    {
                        if (player.Body.Position == level.DefaultSpawnPoint)
                        {
                            if (level.Session.Level ==
                                ((OriginalChapterMapData)level.Session.MapData).StartLevel().Name)
                            {
                                // Simplify to load from beginning
                            }
                            else
                            {
                                // Simplify to default room load
                                if (level.Session.Level.StartsWith("lvl_"))
                                {
                                    values.Add(level.Session.Level);
                                }
                                else
                                {
                                    values.Add("lvl_" + level.Session.Level);
                                }
                            }
                        }
                        else if (level.Session.LevelData.Spawns.IndexOf(player.Body.Position) is var spawnIndex &&
                                 spawnIndex > 0)
                        {
                            // Simplify to indexed room load
                            if (level.Session.Level.StartsWith("lvl_"))
                            {
                                values.Add(level.Session.Level);
                            }
                            else
                            {
                                values.Add("lvl_" + level.Session.Level);
                            }

                            values.Add(spawnIndex.ToString());
                        }
                        else
                        {
                            values.Add(player.Body.Position.X.ToString("F0"));
                            values.Add(player.Body.Position.Y.ToString("F0"));
                        }
                    }
                    else
                    {
                        // Load exact position
                        string format = "0.".PadRight(GameSettings.MaxDecimals + 2, '#');
                        values.Add(((double)player.Body.Position.X + player.Body.Remainder.X).ToString(format,
                            CultureInfo.InvariantCulture));
                        values.Add(((double)player.Body.Position.Y + player.Body.Remainder.Y).ToString(format,
                            CultureInfo.InvariantCulture));

                        if (player.Data.Motion.Normal.Speed != Vector2.Zero)
                        {
                            values.Add(player.Data.Motion.Normal.Speed.X.ToString(CultureInfo.InvariantCulture));
                            values.Add(player.Data.Motion.Normal.Speed.Y.ToString(CultureInfo.InvariantCulture));
                        }
                    }
                }
                else
                {
                    if (level.Session.Level.StartsWith("lvl_"))
                    {
                        values.Add(level.Session.Level);
                    }
                    else
                    {
                        values.Add("lvl_" + level.Session.Level);
                    }
                }
            }

            return string.Join(' ', values.Select(val => val.Contains(' ') ? $"\"{val}\"" : val));
        }
    }
}
