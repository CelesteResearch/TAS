using Celeste.Core.Reflection.Assemblies;

namespace Celeste.Mods.Tas
{
    public sealed partial class TasRuntimeEnvironment
    {
        private void PublishSourceTasExternalBaseClosure()
        {
            process.ExternalBaseClosureFacts.RegisterSupplementalRoot(new SourceExternalAssemblyClosureNode(
                "StudioCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                "CAADC80EA2805A9CEE804A5CEC9204B285E39E88626D94B43C1B29799F54D9A6",
                new string[]
                {
                    "System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    "System.Collections, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    "MemoryPack.Core, Version=1.21.1.0, Culture=neutral, PublicKeyToken=df4c250b14d82627",
                    "System.Memory, Version=8.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51",
                    "System.Text.RegularExpressions, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    "System.Threading, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    "System.Threading.Thread, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    "System.IO.MemoryMappedFiles, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    "System.Diagnostics.Process, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    "System.Linq, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
                    "System.Runtime.InteropServices, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
                }, new string[] { }, false));
        }
    }
}
