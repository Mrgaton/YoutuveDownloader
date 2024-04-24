using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Youtube_downloader
{
    public static class ProcessExtensions
    {
        public static async Task WaitForExitAsync(this Process process, CancellationToken cancellationToken = default)
        {
            while (!process.HasExited)
            {
                await Task.Delay(250, cancellationToken);
            }
        }
    }
}