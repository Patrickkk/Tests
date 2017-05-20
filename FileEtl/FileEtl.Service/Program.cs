using Topshelf;

namespace FileEtl.Service
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ContinousRunnerHostService>(s =>
                {
                    s.ConstructUsing(name => new ContinousRunnerHostService(name));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
            });

            //RI service non continious process
            // if files that should be processed are available start new runner.
            // run for all plugins.
            // wait untill process stops. => frees all memory. disables the possibility to cache....
            // watch for new files.

            //RI service continious process
            // on startup start new runner.
            // run for all plugins.
            // process keeps running. can be stopped by signaling from host process or if host process dies. (named pipes closes)
            // watch for new files.
        }
    }
}