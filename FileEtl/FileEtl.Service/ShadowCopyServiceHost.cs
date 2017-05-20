using System.Diagnostics;
using System.IO;
using FileEtl.ShadowCopy;

namespace FileEtl.Service
{
    public class ShadowCopyServiceHost
    {
        protected Process StartRunner(string shadowDirectory, string[] sourceDirecties, string[] args)
        {
            ShadowFileCopier.Copy(sourceDirecties, shadowDirectory, true, filesunchangedWaitingTimeMilliseconds: 500);
            var runnerPath = Path.Combine(shadowDirectory, @"FileEtl.Runner.exe");

            var start = new ProcessStartInfo
            {
                FileName = Path.Combine(shadowDirectory, @"FileEtl.Runner.exe"),
                UseShellExecute = true,

                //WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
#if DEBUG
                Arguments = string.Join(" ", args, "--debug")
#else

                Arguments = string.Join(" ", args)
#endif
            };

            var process = new Process();
            process.StartInfo = start;
            process.Start();
            return process;
        }
    }
}