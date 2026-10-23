using Celeste.Core.Reflection.RuntimeShapes;

namespace Celeste.Mods.Tas.Query
{
    public sealed partial class SourceTasTargetQueryProcess
    {
        public bool IsOriginalEnumerableOf(object actualValue, string originalElementQName)
            => ((ISourceRuntimeShapeEnvironment)process).SourceRuntimeShapes
                .IsOriginalEnumerableOf(actualValue, originalElementQName);
    }
}
