using Celeste.Core.Diagnostics.Collision;
using Celeste.Core.Diagnostics.Tas;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment : ISourcePlayerCollisionObserver
    {
        void ISourcePlayerCollisionObserver.RecordCollider(object actualEntity)
        {
            if (sourceTasDriver != null)
                sourceTasDriver.SourceActualCollideHitbox.RecordSourcePhase(actualEntity);
        }

        void ISourcePlayerCollisionObserver.RecordOtherPlayerColliders(object actualPlayer)
        {
            if (sourceTasDriver != null)
                sourceTasDriver.SourceActualCollideHitbox.RecordOtherSourcePhase(actualPlayer);
        }
    }
}
