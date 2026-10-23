using Celeste.Core.Reflection.Members;
using Celeste;
using System.Numerics;
using StudioCommunication;
using StudioCommunication.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TargetQuery = Celeste.Mods.Tas.Query.SourceTasTargetQueryProcess;

namespace Celeste.Mods.Tas.Query.Handlers
{
    internal class SourceModInteropQueryHandler : TargetQuery.Handler
    {
        public SourceModInteropQueryHandler(SourceTasTargetQueryProcess query) : base(query)
        {
            AcidLightningType = new(() =>
                query.Driver.SourceModReflection.TryGetType("Glyph", "Celeste.Mod.AcidHelper.Entities.AcidLightning"));
        }

        private readonly Lazy<Type?> AcidLightningType;

        public override Result<bool, TargetQuery.MemberAccessError> ResolveMember(object? instance, out object? value,
            Type type, int memberIdx, string[] memberArgs)
        {
            if (instance != null && instance.GetType() == AcidLightningType.Value &&
                memberArgs[memberIdx] == "toggleOffset")
            {
                // AcidLightning defines a new "toggleOffset" field, but doesn't use it
                // However while resolving members it would be used instead of the one from the base class
                var lightningType = Query.Members.GetActualSourceType(
                    "Celeste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Celeste.Lightning");
                value = Query.GetOriginalField(lightningType, "toggleOffset", QueryReflection.InstanceAnyVisibility)
                    .Read(instance);
                return Result<bool, TargetQuery.MemberAccessError>.Ok(true);
            }

            value = null;
            return Result<bool, TargetQuery.MemberAccessError>.Ok(false);
        }
    }
}
