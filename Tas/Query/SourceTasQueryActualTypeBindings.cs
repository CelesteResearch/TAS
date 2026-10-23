using Celeste.Core.Reflection.Members;
using Celeste.Mods.Tas.Module;

namespace Celeste.Mods.Tas.Query
{
    internal static class SourceTasQueryActualTypeBindings
    {
        internal static void Register(SourceMemberCatalog members)
        {
            const string assembly = "CelesteTAS-EverestInterop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            members.GetOriginalTypeDefinition(assembly, "TAS.InfoHUD.TargetQuery");
            members.RegisterSourceType(typeof(SourceTasTargetQueryProcess), assembly, "TAS.InfoHUD.TargetQuery");
            SourceTasQueryInternalValueTypeBindings.Register(members);
        }
    }
}
