using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;
using Celeste.Everest.Reflection;
using Celeste.Scene.Entities;
using Celeste.Scene.Tracking;
using Celeste.Game.Entities.Hazards.Spinners.Crystal;
using Celeste.Simulation.Gameplay.Hazards.Spinners.Crystal;
using Celeste.Graphics.Animation;
using Celeste.Graphics.Display;
using StudioCommunication.Util;
using Celeste.Mods.Tas.Collections;

namespace Celeste.Mods.Tas.Gameplay
{
    internal static class SourceTasSpinnerColorExt
    {
        public static CrystalSpinnerColor ToCrystalSpinnerColor(this SpinnerColor color) => color switch
        {
            SpinnerColor.Default => (CrystalSpinnerColor)(-1),
            SpinnerColor.White => CrystalSpinnerColor.Rainbow,
            SpinnerColor.Blue => CrystalSpinnerColor.Blue,
            SpinnerColor.Red => CrystalSpinnerColor.Red,
            SpinnerColor.Purple => CrystalSpinnerColor.Purple,
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };

        public static string ToPathName(this SpinnerColor color) => color switch
        {
            SpinnerColor.Default => "default",
            SpinnerColor.White => "white",
            SpinnerColor.Blue => "blue",
            SpinnerColor.Red => "red",
            SpinnerColor.Purple => "purple",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    }
}
