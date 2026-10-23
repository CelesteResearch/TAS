using Celeste.Core.Reflection.Methods;
using MonoMod.Utils;

namespace Celeste.Mods.Tas.Interop
{
    internal sealed class SourceTasBoundFastInvoker
    {
        private readonly SourceOrderedMethod method;
        private readonly FastReflectionHelper.FastInvoker invoker;

        internal SourceTasBoundFastInvoker(SourceOrderedMethod method)
        {
            this.method = method;
            invoker = method.ActualMethod.GetFastInvoker();
        }

        internal object Invoke(object actualSourceInstance, params object[] arguments)
            => invoker(method.ActualOwner ?? actualSourceInstance, arguments);
    }
}
