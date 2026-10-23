#nullable enable
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;
using StudioCommunication;
using Celeste.Mods.Tas.Module;
using Celeste.Everest.Runtime.Startup;
using Celeste.Everest.Runtime.Threading;

namespace Celeste.Mods.Tas.Interop
{
    internal sealed class SourceTasDebugRcPage
    {
        private readonly TasPlaybackDriver driver;
        private readonly EverestDebugRemoteControlProcess DebugRC;

        internal SourceTasDebugRcPage(TasPlaybackDriver driver)
        {
            this.driver = driver;
            DebugRC = ((IEverestDebugRemoteControlEnvironment)driver.Process).DebugRC;
            InfoEndPoint = new EverestDebugRemoteControlEndpoint
            {
                Path = "/tas/info",
                Name = "General Info (CelesteTAS)",
                InfoHTML = "List some TAS info.",
                Handle = c =>
                {
                    string html = ((ISourceEverestMainThreadEnvironment)driver.Process).MainThreadProcess
                        .Schedule(() =>
                        {
                            StringBuilder builder = new();
                            DebugRC.WriteHTMLStart(c, builder);

                            WriteLine(builder, $"Running: {driver.Running}");
                            WriteLine(builder, $"State: {driver.CurrState}");
                            WriteLine(builder,
                                $"SaveState Lines: {string.Join(", ", driver.SourceSavestateManager.AllSavestates.Select(state => state.StudioLine))}");
                            WriteLine(builder, $"CurrentFrame: {driver.Controller.CurrentFrameInTas}");
                            WriteLine(builder, $"TotalFrames: {driver.Controller.Inputs.Count}");
                            WriteLine(builder, $"RoomName: {driver.GameInfo.LevelName}");
                            WriteLine(builder, $"ChapterTime: {driver.GameInfo.ChapterTime}");
                            WriteLine(builder, "Game Info: ");

                            var args = DebugRC.ParseQueryString(c.Request.RawUrl);
                            builder.Append(
                                $"<pre>{HttpUtility.HtmlEncode(args["forceAllowCodeExecution"] == "true" ? driver.GameInfo.ExactStudioInfoAllowCodeExecution : driver.GameInfo.ExactStudioInfo)}</pre>");

                            DebugRC.WriteHTMLEnd(c, builder);
                            return builder.ToString();
                        }).AsTask().GetAwaiter().GetResult();
                    DebugRC.Write(c, html);
                }
            };

            HotkeyEndPoint = new EverestDebugRemoteControlEndpoint
            {
                Path = "/tas/sendhotkey",
                PathHelp =
                    "/tas/sendhotkey?id={HotkeyIds}&action={press(default)|release} (Example: ?id=Start&action=press)",
                PathExample = "/tas/sendhotkey?id=Start&action=press",
                Name = "Send Hotkey (CelesteTAS)",
                InfoHTML =
                    $"Press/Release the specified hotkey.<br />Except for hotkeys FastForward and SlowForward, other hotkeys are automatically released after being pressed.<br />Available id: {string.Join(", ", Enum.GetNames(typeof(HotkeyID)))}",
                Handle = c =>
                {
                    if (driver.SourceSyncChecker.Active)
                    {
                        DebugRC.Write(c, "Sync-Check is currently active. Preventing user intervention.");
                        c.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        return;
                    }

                    void WriteIdErrorPage(string message)
                    {
                        StringBuilder builder = new();
                        DebugRC.WriteHTMLStart(c, builder);
                        WriteLine(builder, $"<h2>ERROR: {message}</h2>");
                        WriteLine(builder,
                            "Example: <a href='/tas/sendhotkey?id=Start&action=press'>/tas/sendhotkey?id=Start&action=press</a>");
                        WriteLine(builder,
                            $"Available IDs: {string.Join(", ", Enum.GetNames(typeof(HotkeyID)).Select(id => $"<a href='/tas/sendhotkey?id={id}'>{id}</a>"))}");
                        WriteLine(builder, "Available action: press, release");
                        DebugRC.WriteHTMLEnd(c, builder);
                        DebugRC.Write(c, builder.ToString());
                    }

                    var args = DebugRC.ParseQueryString(c.Request.RawUrl);
                    string? idValue = args["id"];
                    string? pressValue = args["action"];

                    if (string.IsNullOrEmpty(idValue))
                    {
                        WriteIdErrorPage("No ID given.");
                        return;
                    }

                    if (!Enum.TryParse<HotkeyID>(idValue, ignoreCase: true, out var id) ||
                        !driver.SourceHotkeys.AllHotkeys.TryGetValue(id, out var hotkey))
                    {
                        WriteIdErrorPage("Invalid ID value.");
                        return;
                    }

                    bool press = !"release".Equals(pressValue, StringComparison.InvariantCultureIgnoreCase);
                    hotkey.OverrideCheck = press;
                    DebugRC.Write(c, "OK");
                }
            };

            CustomInfoPoint = new EverestDebugRemoteControlEndpoint
            {
                Path = "/tas/custominfo",
                PathHelp = "/tas/custominfo?template={content} | Example: ?template=" + defaultCustomInfoTemplate,
                PathExample = $"/tas/custominfo?template={defaultCustomInfoTemplate}",
                Name = "Custom Info Template (CelesteTAS)",
                InfoHTML = "Get/Set custom info template. Please use \\n for linebreaks.",
                Handle = c =>
                {
                    StringBuilder builder = new();
                    DebugRC.WriteHTMLStart(c, builder);

                    NameValueCollection args = DebugRC.ParseQueryString(c.Request.RawUrl);
                    string? template = args["template"];
                    if (template != null)
                    {
                        driver.Settings.InfoCustomTemplate = WebUtility.UrlDecode(template).Replace("\\n", "\n");
                    }

                    WriteLine(builder, "<h2>Custom Info Template</h2>");
                    builder.Append($@"<pre>{driver.Settings.InfoCustomTemplate}</pre>");

                    DebugRC.WriteHTMLEnd(c, builder);
                    DebugRC.Write(c, builder.ToString());
                }
            };

            PlayTasPoint = new EverestDebugRemoteControlEndpoint
            {
                Path = "/tas/playtas",
                PathHelp = "/tas/playtas?filePath={filePath} | Example: ?file=C:\\Celeste.tas",
                PathExample = "/tas/playtas?filePath=C:\\Celeste.tas",
                Name = "Play TAS (CelesteTAS)",
                InfoHTML = "Play the specified TAS file",
                Handle = c =>
                {
                    if (driver.SourceSyncChecker.Active)
                    {
                        DebugRC.Write(c, "Sync-Check is currently active. Preventing user intervention.");
                        c.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        return;
                    }

                    var builder = new StringBuilder();
                    DebugRC.WriteHTMLStart(c, builder);

                    var args = DebugRC.ParseQueryString(c.Request.RawUrl);
                    string? filePath = args["filePath"];
                    if (string.IsNullOrEmpty(filePath))
                    {
                        WriteLine(builder, $"<h2>ERROR: Invalid file path: {filePath ?? "NULL"} </h2>");
                    }
                    else
                    {
                        filePath = WebUtility.UrlDecode(filePath);

                        if (!File.Exists(filePath))
                        {
                            WriteLine(builder, $"<h2>ERROR: File does not exist: {filePath} </h2>");
                        }
                        else
                        {
                            WriteLine(builder, "OK");
                            driver.AddMainThreadAction(() =>
                            {
                                driver.DisableRun();
                                driver.Controller.FilePath = filePath;
                                driver.EnableRun();
                            });
                        }
                    }

                    DebugRC.WriteHTMLEnd(c, builder);
                    DebugRC.Write(c, builder.ToString());
                }
            };

            GameStatePoint = new EverestDebugRemoteControlEndpoint
            {
                Path = "/tas/game_state",
                Name = "Game State (CelesteTAS)",
                InfoHTML = "Returns information about the current game state",
                Handle = c => DebugRC.Write(c, JsonSerializer.Serialize(driver.GameData.GetGameState(),
                    new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        WriteIndented = true,
                    }))
            };
        }

        private const string defaultCustomInfoTemplate = @"
Wind: {Level.Wind}\n
AutoJump: {Player.AutoJump} {Player.AutoJumpTimer.toFrame()}\n
Theo: {TheoCrystal.ExactPosition}\n
TheoCantGrab: {TheoCrystal.Hold.cannotHoldTimer.toFrame()}
";


        private readonly EverestDebugRemoteControlEndpoint InfoEndPoint;
        private readonly EverestDebugRemoteControlEndpoint HotkeyEndPoint;
        private readonly EverestDebugRemoteControlEndpoint CustomInfoPoint;
        private readonly EverestDebugRemoteControlEndpoint PlayTasPoint;
        private readonly EverestDebugRemoteControlEndpoint GameStatePoint;

        internal void Load()
        {
            DebugRC.EndPoints.Add(InfoEndPoint);
            DebugRC.EndPoints.Add(HotkeyEndPoint);
            DebugRC.EndPoints.Add(CustomInfoPoint);
            DebugRC.EndPoints.Add(PlayTasPoint);
            DebugRC.EndPoints.Add(GameStatePoint);
        }

        internal void Unload()
        {
            DebugRC.EndPoints.Remove(InfoEndPoint);
            DebugRC.EndPoints.Remove(HotkeyEndPoint);
            DebugRC.EndPoints.Remove(CustomInfoPoint);
            DebugRC.EndPoints.Remove(PlayTasPoint);
            DebugRC.EndPoints.Remove(GameStatePoint);
        }

        internal void WriteLine(StringBuilder builder, string text)
        {
            builder.Append($@"{HttpUtility.HtmlEncode(text)}<br />");
        }
    }
}
