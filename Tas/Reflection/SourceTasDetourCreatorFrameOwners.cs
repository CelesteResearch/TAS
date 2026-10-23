using System;
using System.Linq;
using System.Reflection;

namespace Celeste.Mods.Tas.Reflection
{
    internal static class SourceTasDetourCreatorFrameOwners
    {
        private const string AssemblyIdentity =
            "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        internal static void Register(GameApplication process)
        {
            Admit(process, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasActualCollideHitbox), "Load",
                Type.EmptyTypes,
                "TAS.Gameplay.Hitboxes.ActualCollideHitbox", 33554580, 100664632, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasCycleHitboxColorProcess), "Load",
                Type.EmptyTypes,
                "TAS.EverestInterop.Hitboxes.CycleHitboxColor", 33554616, 100665026, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasHitboxColor), "Load",
                Type.EmptyTypes,
                "TAS.EverestInterop.Hitboxes.HitboxColor", 33554617, 100665051, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasHitboxFixer), "Load",
                Type.EmptyTypes,
                "TAS.EverestInterop.Hitboxes.HitboxFixer", 33554619, 100665065, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasHitboxOptimized), "Initialize",
                Type.EmptyTypes,
                "TAS.EverestInterop.Hitboxes.HitboxOptimized", 33554622, 100665088, "Initialize", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasHitboxOptimized), "Load",
                Type.EmptyTypes,
                "TAS.EverestInterop.Hitboxes.HitboxOptimized", 33554622, 100665089, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasHitboxRoomBoundary), "Load",
                Type.EmptyTypes,
                "TAS.EverestInterop.Hitboxes.HitboxRoomBoundary", 33554623, 100665114, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasHitboxSimplifiedProcess), "Load",
                Type.EmptyTypes,
                "TAS.EverestInterop.Hitboxes.HitboxSimplified", 33554624, 100665119, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasHitboxToggleProcess), "Load",
                Type.EmptyTypes,
                "TAS.EverestInterop.Hitboxes.HitboxToggle", 33554625, 100665130, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasHitboxTriggerSpikes), "Initialize",
                Type.EmptyTypes,
                "TAS.EverestInterop.Hitboxes.HitboxTriggerSpikes", 33554626, 100665138, "Initialize", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasHitboxTriggerSpikes), "Load",
                Type.EmptyTypes,
                "TAS.EverestInterop.Hitboxes.HitboxTriggerSpikes", 33554626, 100665139, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Hitboxes.SourceTasTriggerHitbox), "Load",
                Type.EmptyTypes,
                "TAS.Gameplay.Hitboxes.TriggerHitbox", 33554584, 100664662, "Load", []);
            Admit(process,
                typeof(global::Celeste.Mods.Tas.Gameplay.Hitboxes.SourceTasConquerorBeamHitboxProcess),
                "Initialize", Type.EmptyTypes,
                "TAS.EverestInterop.Hitboxes.HitboxConquerorBeam", 33554618, 100665060, "Initialize", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Gameplay.Hitboxes.SourceTasFinalBossHitboxProcess),
                "Load", Type.EmptyTypes,
                "TAS.Gameplay.Hitboxes.FinalBossHitbox", 33554582, 100664648, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Gameplay.Hitboxes.SourceTasNpcHitboxProcess), "Load",
                Type.EmptyTypes,
                "TAS.EverestInterop.Hitboxes.HitboxNpc", 33554621, 100665080, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.TasPlaybackDriver), "LoadSourceCoreMethodHooks",
                Type.EmptyTypes,
                "TAS.Playback.Core", 33554529, 100664049, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.TasPlaybackDriver),
                "InstallSourceStunPauseBeforeUpdate",
                [typeof(global::Celeste.Mods.Tas.Commands.SourceTasStunPauseProcess)],
                "TAS.Input.Commands.StunPauseCommand", 33554528, 100664033, "Initialize", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Hud.SourceTasInfoHud), "Load", Type.EmptyTypes,
                "TAS.EverestInterop.InfoHUD.InfoHud", 33554611, 100664971, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Hud.SourceTasJetBrainsMonoFont), "Load",
                Type.EmptyTypes,
                "TAS.EverestInterop.InfoHUD.JetBrainsMonoFont", 33554615, 100665013, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Gameplay.SourceTasForceAllowAccessibilityTools),
                "Load",
                Type.EmptyTypes,
                "TAS.Gameplay.ForceAllowAccessibilityTools", 33554574, 100664594, "Load", []);
            Admit(process,
                typeof(global::Celeste.Mods.Tas.Gameplay.Optimization.SourceTasFastForwardOptimization),
                "Initialize", Type.EmptyTypes,
                "TAS.Gameplay.Optimization.FastForwardOptimization", 33554578, 100664617, "Initialize", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.EverestInterop.SourceTasFastForwardBoost), "Load",
                Type.EmptyTypes,
                "TAS.EverestInterop.FastForwardBoost", 33554597, 100664776, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.EverestInterop.SourceTasDesyncFixerProcess), "Load",
                Type.EmptyTypes,
                "TAS.EverestInterop.DesyncFixer", 33554594, 100664759, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.DesyncFix.SourceTasSkinModFixProcess), "Load",
                Type.EmptyTypes,
                "TAS.Gameplay.DesyncFix.SkinModFix", 33554588, 100664685, "Load", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.Reflection.SourceTasHookHelperProcess), "OnHook",
                [typeof(global::System.Reflection.MethodBase), typeof(global::System.Reflection.MethodInfo)],
                "TAS.Utils.HookHelper", 33554486, 100663657, "OnHook",
                ["System.Reflection.MethodBase", "System.Reflection.MethodInfo"]);
            Admit(process, typeof(global::Celeste.Mods.Tas.Reflection.SourceTasHookHelperProcess), "OnHook",
                [typeof(global::System.Reflection.MethodBase), typeof(global::System.Delegate)],
                "TAS.Utils.HookHelper", 33554486, 100663658, "OnHook",
                ["System.Reflection.MethodBase", "System.Delegate"]);
            Admit(process, typeof(global::Celeste.Mods.Tas.Reflection.SourceTasHookHelperProcess), "IlHook",
                [typeof(global::System.Reflection.MethodBase), typeof(global::MonoMod.Cil.ILContext.Manipulator)],
                "TAS.Utils.HookHelper", 33554486, 100663659, "IlHook",
                ["System.Reflection.MethodBase", "MonoMod.Cil.ILContext+Manipulator"]);
            Admit(process, typeof(global::Celeste.Mods.Tas.Reflection.SourceTasHookHelperProcess), "IlHook",
                [
                    typeof(global::System.Reflection.MethodBase),
                    typeof(global::System.Action<global::MonoMod.Cil.ILCursor, global::MonoMod.Cil.ILContext>)
                ],
                "TAS.Utils.HookHelper", 33554486, 100663660, "IlHook",
                ["System.Reflection.MethodBase", "System.Action`2<MonoMod.Cil.ILCursor,MonoMod.Cil.ILContext>"]);
            Admit(process, typeof(global::Celeste.Mods.Tas.EverestInterop.SourceTasSimplifiedGraphicsFeature),
                "OwnIl",
                [
                    typeof(global::System.String), typeof(global::System.Reflection.MethodBase),
                    typeof(global::MonoMod.Cil.ILContext.Manipulator)
                ],
                "TAS.EverestInterop.SimplifiedGraphicsFeature", 33554605, 100664877, "Initialize", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.EverestInterop.SourceTasSimplifiedGraphicsFeature),
                "OwnOn",
                [
                    typeof(global::System.String), typeof(global::System.Reflection.MethodBase),
                    typeof(global::System.Delegate)
                ],
                "TAS.EverestInterop.SimplifiedGraphicsFeature", 33554605, 100664877, "Initialize", []);
            Admit(process, typeof(global::Celeste.Mods.Tas.EverestInterop.SourceTasHideGameplay), "Load",
                Type.EmptyTypes,
                "TAS.EverestInterop.HideGameplay", 33554598, 100664780, "Load", []);
        }

        private static void Admit(GameApplication process, Type physicalType, string physicalName,
            Type[] physicalParameters, string sourceType, int sourceTypeToken, int token, string sourceName,
            string[] sourceParameters)
        {
            var methods = process.Reflection.SourceMethods;
            var source = methods.RequireDefinition(AssemblyIdentity, token);
            if (source.DeclaringType != sourceType || source.TypeDefinitionToken != sourceTypeToken ||
                source.Name != sourceName || source.ReturnTypeIdentity != "System.Void" ||
                source.GenericParameterCount != 0 ||
                (source.Attributes & (int)MethodAttributes.Static) == 0 ||
                !source.ParameterTypeIdentities.SequenceEqual(sourceParameters))
                throw new InvalidOperationException(
                    "The actual hook creator bridge differs from its qualified original operation: " + token);
            var actual = physicalType.GetMethod(physicalName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly,
                null, physicalParameters,
                null) ?? throw new MissingMethodException(physicalType.FullName, physicalName);
            if (actual.IsStatic || actual.ReturnType != typeof(void) || actual.ContainsGenericParameters ||
                !actual.GetParameters().Select(parameter => parameter.ParameterType).SequenceEqual(physicalParameters))
                throw new InvalidOperationException(
                    "The actual hook creator bridge ABI does not match its owned instance implementation.");
            // These exact extracted creator frames carry the genuine TAS operation's
            // assembly identity. Existing callable lifecycle bindings remain unchanged;
            // differing bridge parameters and per-process instances are not source ABI aliases.
            methods.RegisterActualHelperOperationFrameOwnership(process.SourceMembers, actual, source);
        }
    }
}
