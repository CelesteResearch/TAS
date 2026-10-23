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
    public enum SpinnerColor
    {
        Default,
        White,
        Blue,
        Red,
        Purple
    }
}
