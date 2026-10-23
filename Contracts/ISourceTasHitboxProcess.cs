using System;
using Celeste.Core.Rendering;

namespace Celeste.Core.Rendering.Diagnostics
{
    public interface ISourceTasHitboxProcess
    {
        void ClearActualCollide();
        void StoreActualCollider(object actualEntity);
        void StorePlayerColliderOwner(object actualScene, object actualOwner);
        void CompletePlayerUpdate();
        bool ShouldDebugRender(bool commandsOpen);
        void AfterEntityDebugRender(object actualEntity, ISourceRenderContext context);
        void RenderPlayerDebug(object actualPlayer, ISourceRenderContext context, Action originalRender);
    }
}
