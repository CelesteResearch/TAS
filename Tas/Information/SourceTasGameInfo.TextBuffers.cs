using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Celeste.Mods.Tas.Information
{
    internal sealed partial class SourceTasGameInfo
    {
        private StringBuilder statusTextBuilder;
        private List<string> statusParts;

        private StringBuilder BorrowStatusTextBuilder()
        {
            StringBuilder builder = Interlocked.Exchange(ref statusTextBuilder, null);
            return builder ?? new StringBuilder();
        }

        private void ReturnStatusTextBuilder(StringBuilder builder)
        {
            builder.Clear();
            Interlocked.Exchange(ref statusTextBuilder, builder);
        }

        private List<string> BorrowStatusParts()
        {
            List<string> parts = Interlocked.Exchange(ref statusParts, null);
            return parts ?? new List<string>();
        }

        private void ReturnStatusParts(List<string> parts)
        {
            parts.Clear();
            Interlocked.Exchange(ref statusParts, parts);
        }
    }
}
