using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Celeste.Core.Components;
using Celeste.Core.Randomness;
using Celeste.Core.Reflection.Members;
using Celeste.Core.Reflection.Methods;
using Celeste.Everest.Reflection;
using Celeste.Game.Entities.Hazards.Spinners.Crystal;
using Celeste.Game.Execution.Levels;
using Celeste.Scene.Entities;
using Celeste.Scene.Tracking;
using MonoMod.RuntimeDetour;

namespace Celeste.Mods.Tas.EverestInterop
{
    internal sealed class SourceTasRemoveSelfComponent : SourceComponentState
    {
        internal SourceTasRemoveSelfComponent() : base(true, false)
        {
        }

        public override void Added(object actualEntity)
        {
            base.Added(actualEntity);
            var entity = (SceneEntityState)actualEntity;
            entity.Visible = false;
            entity.Collidable = false;
            entity.Collider = null;
        }

        public override void Update()
        {
            ((SceneEntityState)Entity)?.RemoveSelf();
            RemoveSelf();
        }
    }
}
