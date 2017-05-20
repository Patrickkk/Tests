using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileEtl.ShadowCopy;

namespace FileEtl.Service
{
    /// <summary>
    /// Load given executable if certian condition is met
    /// </summary>
    public class FileEtlService : ContinousRunnerHostService
    {
        private static bool processIsRunning = false;
        private const string SourceDirectory = @"D:\Temp\AppHost\";
        public static RunnerProcess hostedProcess = new RunnerProcess { Id = Guid.NewGuid(), Folder = "" };

        public FileEtlService(string Name) : base(Name)
        {
        }

        public void Start()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (processIsRunning == false && StartconditionIsMet())
                    {
                        StartProcessInShadowCopyAndWaitForExit();
                    }
                }
            });
        }

        private bool StartconditionIsMet()
        {
            return Directory.GetFiles(@"D:\Temp\AppHost\TestFolder", "*.*").Any();
        }

        public void Stop()
        {
        }

        private static void StartProcessInShadowCopyAndWaitForExit()
        {
            var id = Guid.NewGuid();
            var shadowdirectory = Shadowdirectory(hostedProcess.Id);
            if (!Directory.Exists(shadowdirectory))
            {
                Directory.CreateDirectory(shadowdirectory);
            }

            while (ShadowFileCopier.FilesChanged(new string[] { SourceDirectory }, shadowdirectory))
            {
                ShadowFileCopier.Copy(new string[] { SourceDirectory }, shadowdirectory);
                Thread.Sleep(5000);
            }

            var infolder = @"D:\temp\FileEtl-Runner";

            var start = new ProcessStartInfo
            {
                FileName = Path.Combine(shadowdirectory, @"FileEtl.Runner.exe"),

                // To run as command line with io
                UseShellExecute = true,

                //WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                Arguments = $@"{id} ""{infolder}"""
            };

            var process = new Process();
            process.StartInfo = start;
            process.Start();
            processIsRunning = true;
        }

        private static void ProcessExisted(object sender, EventArgs e)
        {
            processIsRunning = false;
        }

        private static string Shadowdirectory(Guid id)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), id.ToString());
        }
    }

    public class RunnerProcess
    {
        public Guid Id { get; set; }

        public string Folder { get; set; }

        public Process Process { get; set; }
    }
}