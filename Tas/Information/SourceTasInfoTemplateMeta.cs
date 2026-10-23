#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using StudioCommunication;
using StudioCommunication.Util;
using Celeste.Mods.Tas.Query;
using Celeste.Mods.Tas.Commands;

namespace Celeste.Mods.Tas.Information
{
    internal sealed class SourceTasInfoTemplateMeta : ISourceTasCommandMeta
    {
        private readonly TasPlaybackDriver driver;
        private SourceTasTargetQueryProcess TargetQuery => driver.TargetQuery;

        internal SourceTasInfoTemplateMeta(TasPlaybackDriver driver)
        {
            this.driver = driver;
        }

        public string Insert => string.Empty;
        public bool HasArguments => false;

        public int GetHash(string[] args, string filePath, int fileLine)
        {
            return TargetQuery.GetQueryArgs(args, 0)
                .Aggregate(new StableHashCode(), (argHash, arg) => argHash.Append(arg))
                .ToHashCode();
        }

        public IEnumerator<CommandAutoCompleteEntry> GetAutoCompleteEntries(string[] args, string filePath,
            int fileLine)
        {
            // Target
            string[] targetQueryArgs = args.Length > 0 ? args[0].Split('.') : [];
            using var enumerator =
                TargetQuery.ResolveAutoCompleteEntries(targetQueryArgs, SourceTasTargetQueryProcess.Variant.Get);
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }

            // Helper functions
            if (targetQueryArgs.Length == 0)
            {
                yield break;
            }

            var baseTypes = TargetQuery.ResolveBaseTypes(targetQueryArgs[..^1], out string[] memberArgs);
            var targetTypes = baseTypes
                .Select(type =>
                    TargetQuery.RecurseMemberType(type, memberArgs, SourceTasTargetQueryProcess.Variant.Get))
                .Where(type => type != null)
                .ToArray();

            var formatters = new HashSet<string>();
            if (targetTypes.Contains(typeof(float)))
            {
                formatters.Add("toFrame()");
                formatters.Add("toPixelPerFrame()");
            }

            if (targetTypes.Contains(typeof(Vector2)))
            {
                formatters.Add("toPixelPerFrame()");
            }

            foreach (string formatter in formatters)
            {
                yield return new CommandAutoCompleteEntry
                {
                    Name = formatter,
                    Extra = "Formatter",
                    Prefix = string.Join('.', targetQueryArgs[..^1]) + ".",
                    Suggestion = true,
                    IsDone = true,
                };
            }
        }
    }
}
