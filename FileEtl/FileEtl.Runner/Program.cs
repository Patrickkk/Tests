using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using FileEtl.TwoWayNamedPipes;

namespace FileEtl.Runner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Any(x => x.ToLower() == "--debug"))
            {
                Debugger.Launch();
                Debugger.Break();
            }
            var id = args[0];
            var folder = args[1];

            Console.WriteLine(id);
            Console.WriteLine(folder);

            using (var connection = new TwoWayNamedPipeConnection<string>("", null, null))
            {
                connection.Connect();
                while (true)
                {
                    Console.WriteLine($"im alive {DateTime.UtcNow} 2");
                    Thread.Sleep(1000);
                    connection.Send(DateTime.UtcNow.ToString());
                }
            }
        }
    }
}