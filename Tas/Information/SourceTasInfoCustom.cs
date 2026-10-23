using Celeste.Mods.Tas.Query.Handlers;
#nullable enable
using System.Numerics;
using Entity = Celeste.Core.Components.ISourceEntitySourceData;
using Scene = Celeste.Scene.Scenes.SourceSceneState;
using Component = Celeste.Core.Components.SourceComponentState;
using Collider = Celeste.Physics.Collision.Shapes.ColliderGeometry;
using Hitbox = Celeste.Physics.Collision.Shapes.HitboxColliderGeometry;
using Circle = Celeste.Physics.Collision.Shapes.CircleColliderGeometry;
using ColliderList = Celeste.Physics.Collision.Shapes.ColliderListGeometry;
using Celeste.Mods.Tas.Query;
using Celeste.Mods.Tas.Commands;
using StudioCommunication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using StudioCommunication.Util;


namespace Celeste.Mods.Tas.Information
{
    /// Handles parsing of custom Info HUD templates
    internal sealed class SourceTasInfoCustom
    {
        private static readonly Regex TargetQueryRegex = new(@"\{(.*?)\}", RegexOptions.Compiled);
        private static readonly Regex TableRegex = new(@"\|\|(.*?)\|\|", RegexOptions.Compiled);
        private static readonly Regex LuaRegex = new(@"\[\[(.+?)\]\]", RegexOptions.Compiled);

        /// Should return true if the value was successfully formatted, otherwise false
        private delegate bool ValueFormatter(object? value, int decimals, out string formattedValue);

        // Applies custom formatting to the result of a target-query
        private readonly TasPlaybackDriver driver;
        private SourceTasTargetQueryProcess TargetQuery => driver.TargetQuery;
        internal ISourceTasCommandMeta Meta { get; }

        internal SourceTasInfoCustom(TasPlaybackDriver driver)
        {
            this.driver = driver;
            Meta = new SourceTasInfoTemplateMeta(driver);
            CustomFormatters = new Dictionary<string, ValueFormatter>
            {
                { ".toFrame()", Formatter_toFrame },
                { ".toPixelPerFrame()", Formatter_toPixelPerFrame },
            };
        }

        private readonly Dictionary<string, ValueFormatter> CustomFormatters;

        /// Returns the parsed info for the current template
        public string GetInfo(int? decimals = null, bool forceAllowCodeExecution = false)
        {
            return string.Join('\n',
                ParseTemplate(driver.Settings.InfoCustomTemplate.SplitLines(),
                    decimals ?? driver.Settings.CustomInfoDecimals, forceAllowCodeExecution));
        }

        // Fake-command to provide auto-complete support for info-templates, using the same system

        #region Parsing

        /// Parses lines of a custom Info HUD template into actual values for the current frame
        public IEnumerable<string> ParseTemplate(IEnumerable<string> template, int decimals,
            bool forceAllowCodeExecution = false)
        {
            return template.SelectMany(line => ParseTemplateLine(line, decimals, forceAllowCodeExecution));
        }

        /// Parses a single line of a custom Info HUD template into actual values for the current frame
        public IEnumerable<string> ParseTemplateLine(string templateLine, int decimals,
            bool forceAllowCodeExecution = false)
        {
            /* Replace single results inline and can format an aligned list for multiple results
             * Example:
             *
             *     Template:
             * JumpThruPos: ||{JumpThru.Position=} | {JumpThru.X=} : {Player.X} # {Level.Wind=} ; {JumpThru.Y=}|| ABC={Session.SID}
             * Glider Data: X={Glider.X} Y={Glider.Y}
             *
             *     Single Entity:
             * JumpThruPos: JumpThru.Position=3608.00, -2040.00 | JumpThru.X=3608 : 3872.00 # Level-Wind=400 ; JumpThru.Y=-2040.00
             * Glider Data: X=3872.00 Y=2080.00
             *
             *     Multiple Entities:
             * JumpThruPos: # Level.Wind=400
             *     [9b:0] JumpThru.Position=3608.00, -2040.00  | JumpThru.X=3608.00  ; JumpThru.Y=-2040.00
             *     [9b:0] JumpThru.Position=36082.00, -2040.00 | JumpThru.X=36082.00 ; JumpThru.Y=-2040.00
             *     [1:0]  3872.00
             * Glider Data: X={[9c:3] 3872.00, [9c:5] 8273.00} Y={[9c:3] 2080.00, [9c:5] 802.00}
             */

            // A table is a mapping of result types -> it's query results (including prefix)
            List<Dictionary<string, List<string>>> tables = [];

            // Find tables (and remove them to avoid duplicate results)
            templateLine = TableRegex.Replace(templateLine, tableMatch =>
            {
                string tableQuery = tableMatch.Groups[1].Value;
                Dictionary<string, List<string>> tableResults = [];

                Type? firstResultType = null;
                bool hideTypes = true; // Don't show types if all result are of the same type

                Match? lastMatch = null;
                foreach (Match match in TargetQueryRegex.Matches(tableQuery))
                {
                    string prefixText; // Text before target-queries
                    if (lastMatch == null)
                    {
                        prefixText = tableQuery[..match.Index];
                    }
                    else
                    {
                        prefixText = tableQuery[(lastMatch.Index + lastMatch.Length)..match.Index];
                    }

                    lastMatch = match;

                    string query = match.Groups[1].Value;

                    // Find query prefix
                    string queryPrefix;
                    if (query[^1] == ':')
                    {
                        queryPrefix = $"{query} "; // query: value
                        query = query[..^1];
                    }
                    else if (query[^1] == '=')
                    {
                        queryPrefix = query; // query=value
                        query = query[..^1];
                    }
                    else
                    {
                        queryPrefix = "";
                    }

                    // Find custom formatter
                    ValueFormatter? formatter = null;
                    foreach ((string name, var customFormatter) in CustomFormatters)
                    {
                        if (query.EndsWith(name))
                        {
                            query = query[..^name.Length];
                            formatter = customFormatter;
                        }
                    }

                    var result = TargetQuery.GetMemberValues(query, forceAllowCodeExecution);
                    if (result.Failure)
                    {
                        tableResults.AddToKey("Error", $"{prefixText}{queryPrefix}<{result.Error}>");
                        continue;
                    }

                    foreach ((object baseInstance, object? value) in result.Value)
                    {
                        var currResultType = baseInstance is Type type ? type : baseInstance.GetType();
                        firstResultType ??= currResultType;

                        if (firstResultType != currResultType)
                        {
                            hideTypes = false;
                        }

                        if (formatter == null || !formatter(value, decimals, out string valueStr))
                        {
                            valueStr = DefaultFormatter(value, decimals);
                        }

                        string key = TargetQuery.GetOriginalTypeName(currResultType);
                        string queryResult = $"{queryPrefix}{valueStr}";

                        if (baseInstance is Entity entity && !string.IsNullOrEmpty(entity.SourceId.Level))
                        {
                            key += $"[{entity.SourceId.Level}:{entity.SourceId.ID}]";
                        }

                        if (tableResults.TryGetValue(key, out var results))
                        {
                            results.Add(prefixText + queryResult);
                        }
                        else
                        {
                            tableResults[key] = [prefixText.TrimStart() + queryResult];
                        }
                    }
                }

                if (hideTypes && firstResultType != null)
                {
                    tableResults = tableResults.ToDictionary(
                        entry => entry.Key[TargetQuery.GetOriginalTypeName(firstResultType).Length..],
                        entry => entry.Value);
                }

                tables.Add(tableResults);

                return string.Empty;
            });

            // Find main queries
            string mainResult = TargetQueryRegex.Replace(templateLine, match =>
            {
                string query = match.Groups[1].Value;

                // Find query prefix
                string queryPrefix;
                if (query[^1] == ':')
                {
                    queryPrefix = $"{query} "; // query: value
                    query = query[..^1];
                }
                else if (query[^1] == '=')
                {
                    queryPrefix = query; // query=value
                    query = query[..^1];
                }
                else
                {
                    queryPrefix = "";
                }

                // Find custom formatter
                ValueFormatter? formatter = null;
                foreach ((string name, var customFormatter) in CustomFormatters)
                {
                    if (query.EndsWith(name))
                    {
                        query = query[..^name.Length];
                        formatter = customFormatter;
                    }
                }

                var result = TargetQuery.GetMemberValues(query, forceAllowCodeExecution);
                if (result.Failure)
                {
                    return $"{queryPrefix}<{result.Error}>";
                }

                if (result.Value.Count == 0)
                {
                    return "<Not found>";
                }

                if (result.Value.Count == 1)
                {
                    if (formatter == null || !formatter(result.Value[0].Value, decimals, out string valueStr))
                    {
                        valueStr = DefaultFormatter(result.Value[0].Value, decimals);
                    }

                    return $"{queryPrefix}{valueStr}";
                }

                var resultCollection = new StringBuilder("{ ");
                bool firstValue = true;
                foreach ((object baseInstance, object? value) in result.Value)
                {
                    if (!firstValue)
                    {
                        resultCollection.Append(", ");
                    }

                    firstValue = false;

                    if (formatter == null || !formatter(value, decimals, out string valueStr))
                    {
                        valueStr = DefaultFormatter(value, decimals);
                    }

                    if (baseInstance is Entity entity && !string.IsNullOrEmpty(entity.SourceId.Level))
                    {
                        resultCollection.Append(
                            $"[{entity.SourceId.Level}:{entity.SourceId.ID}] {queryPrefix}{valueStr}");
                    }
                    else
                    {
                        resultCollection.Append($"{queryPrefix}{valueStr}");
                    }
                }

                resultCollection.Append(" }");

                return resultCollection.ToString();
            });

            // Evaluate Lua code for main line
            yield return LuaRegex.Replace(mainResult, match =>
            {
                if (TargetQuery.PreventCodeExecution && !forceAllowCodeExecution)
                {
                    return "<Cannot safely evaluate Lua code during EnforceLegal>";
                }

                string code = match.Groups[1].Value;
                object?[]? objects = driver.LuaCommands.ExecuteLua(code);
                return objects == null
                    ? "null"
                    : string.Join(", ", objects.Select(o => o == null ? "null" : TargetQuery.FormatOriginalObject(o)));
            });

            // Format tables
            foreach (var table in tables)
            {
                var lines = table.ToDictionary(
                    entry => entry.Key,
                    entry => new StringBuilder($"  {entry.Key} "));

                if (lines.Count == 0)
                {
                    continue;
                }

                int resultIdx = 0;
                bool allDone = false;

                while (!allDone)
                {
                    // Align all lines
                    int maxLength = lines
                        .Select(entry => entry.Value.Length)
                        .Aggregate(Math.Max);
                    foreach (var (_, line) in lines)
                    {
                        line.Append(' ', maxLength - line.Length);
                    }

                    // Append next parameter
                    allDone = true;
                    foreach ((string key, var results) in table)
                    {
                        if (resultIdx >= results.Count)
                        {
                            continue;
                        }

                        lines[key].Append(results[resultIdx]);
                        allDone = false;
                    }

                    resultIdx++;
                }

                foreach (var (_, line) in lines)
                {
                    yield return line.ToString();
                }
            }
        }

        #endregion

        #region Formatting

        /// Formats a value in seconds into frames
        private bool Formatter_toFrame(object? value, int _, out string formattedValue)
        {
            if (value is float floatValue)
            {
                formattedValue = driver.GameInfo.FormatFrames(driver.GameInfo.ToCeilingFrames(floatValue));
                return true;
            }

            formattedValue = "";
            return false;
        }

        /// Formats a value in px/s into px/f
        private bool Formatter_toPixelPerFrame(object? value, int decimals, out string formattedValue)
        {
            if (value is float floatValue)
            {
                formattedValue = driver.GameInfo.ConvertSpeedUnit(floatValue, SpeedUnit.PixelPerFrame)
                    .ToFormattedString(decimals);
                return true;
            }

            if (value is Vector2 vectorValue)
            {
                formattedValue = driver.GameInfo.ConvertSpeedUnit(vectorValue, SpeedUnit.PixelPerFrame)
                    .ToSimpleString(decimals);
                return true;
            }

            formattedValue = "";
            return false;
        }

        /// Fallback for when no specific formatter is applicable
        private string DefaultFormatter(object? obj, int decimals)
        {
            switch (obj)
            {
                case string stringValue:
                    return stringValue;
                case Vector2 vectorValue:
                    return vectorValue.ToSimpleString(decimals);
                case SourceEntityQueryHandler.SubpixelComponent subpixelComponentValue:
                    return subpixelComponentValue.ToFormattedString(decimals);
                case SourceEntityQueryHandler.SubpixelPosition subpixelPositionValue:
                    return subpixelPositionValue.ToFormattedString(decimals);
                case Vector2Double vectorValue:
                    return vectorValue.ToSimpleString(decimals);
                case float floatValue:
                    return floatValue.ToFormattedString(decimals);
                case global::Celeste.Scene.Scenes.SourceSceneState sceneValue:
                    return TargetQuery.FormatOriginalObject(sceneValue);
                case Entity entity:
                    string id = !string.IsNullOrEmpty(entity.SourceId.Level)
                        ? $"[{entity.SourceId.Level}:{entity.SourceId.ID}]"
                        : "";
                    return $"{TargetQuery.FormatOriginalObject(entity)}{id}";
                case Collider collider:
                    return ColliderToString(collider);
                case IEnumerable enumerable:
                    bool compressed = TargetQuery.IsOriginalEnumerableOf(enumerable, "Monocle.Component") ||
                                      TargetQuery.IsOriginalEnumerableOf(enumerable, "Monocle.Entity");
                    return IEnumerableToString(enumerable, ", ", compressed);

                default:
                    return obj == null ? "null" : TargetQuery.FormatOriginalObject(obj);
            }
        }

        /// Formats items of the IEnumerable, optionally compressing same values
        private string IEnumerableToString(IEnumerable enumerable, string separator, bool compressed)
        {
            var builder = new StringBuilder();

            if (!compressed)
            {
                foreach (object value in enumerable)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(separator);
                    }

                    builder.Append(value == null ? null : TargetQuery.FormatOriginalObject(value));
                }

                return builder.ToString();
            }

            var valueOccurrences = new Dictionary<string, int>();
            foreach (object value in enumerable)
            {
                string str = TargetQuery.FormatOriginalObject(value);
                if (!valueOccurrences.TryAdd(str, 1))
                {
                    valueOccurrences[str]++;
                }
            }

            foreach ((string key, int occurrences) in valueOccurrences)
            {
                if (builder.Length > 0)
                {
                    builder.Append(separator);
                }

                if (occurrences == 1)
                {
                    builder.Append(key);
                }
                else
                {
                    builder.Append($"{key} * {occurrences}");
                }
            }

            return builder.ToString();
        }

        /// Formats a collider with its important values
        private string ColliderToString(Collider collider, int iterationHeight = 1)
        {
            return collider switch
            {
                Hitbox hitbox => $"Hitbox=[{hitbox.Left},{hitbox.Right}]×[{hitbox.Top},{hitbox.Bottom}]",
                Circle circle => circle.Position == Vector2.Zero
                    ? $"Circle=[Radius={circle.Radius}]"
                    : $"Circle=[Radius={circle.Radius},Offset={circle.Position}]",
                ColliderList list => iterationHeight > 0
                    ? "ColliderList: { " +
                      string.Join("; ", list.colliders.Select(s => ColliderToString(s, iterationHeight - 1))) + " }"
                    : "ColliderList: { ... }",

                _ => TargetQuery.FormatOriginalObject(collider)
            };
        }

        #endregion
    }
}
