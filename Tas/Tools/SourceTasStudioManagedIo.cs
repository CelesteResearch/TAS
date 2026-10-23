using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Celeste.Mods.Tas.Tools
{
    internal static class SourceTasStudioManagedIo
    {
        internal static async Task WaitForExitAsync(ISourceTasStudioInstallationIo io, Process process,
            TimeSpan timeout)
        {
            Task exit = io.WaitForExitAsync(process);
            if (exit.IsCompleted || timeout == Timeout.InfiniteTimeSpan)
            {
                await exit.ConfigureAwait(false);
                return;
            }

            using var timeoutCancellation = new CancellationTokenSource();
            Task delay = Task.Delay(timeout, timeoutCancellation.Token);
            if (await Task.WhenAny(exit, delay).ConfigureAwait(false) == delay)
                throw new TimeoutException();
            timeoutCancellation.Cancel();
            await exit.ConfigureAwait(false);
        }

        internal static async Task<byte[]> ComputeHashAsync(HashAlgorithm algorithm, Stream source)
        {
            var buffer = new byte[4096];
            int read;
            while ((read = await source.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) != 0)
                algorithm.TransformBlock(buffer, 0, read, buffer, 0);
            algorithm.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
            return algorithm.Hash;
        }
    }
}
