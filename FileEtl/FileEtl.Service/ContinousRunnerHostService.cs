using System;
using System.Threading;
using System.Threading.Tasks;
using FileEtl.TwoWayNamedPipes;

namespace FileEtl.Service
{
    public class ContinousRunnerHostService : ShadowCopyServiceHost
    {
        private CancellationTokenSource cancellation = new CancellationTokenSource();
        private readonly string Name;

        public ContinousRunnerHostService(string Name)
        {
            this.Name = Name;
        }

        public void Start()
        {
            Task.Run(() =>
            {
                var process = StartRunner("Runner", new string[] { @"D:\Temp\AppHost\" }, new string[0]);
                using (var connection = new TwoWayNamedPipeConnection<string>(Name, null, null))
                {
                    connection.Connect();
                    connection.StartRecieving(x => Console.WriteLine(x));
                    while (!cancellation.Token.IsCancellationRequested)
                    {
                        Task.Delay(500);
                    }
                    connection.Dispose();
                }
            });
        }

        public void Stop()
        {
            cancellation.Cancel();
        }
    }
}