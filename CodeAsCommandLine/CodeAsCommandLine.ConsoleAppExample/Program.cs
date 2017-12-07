using System.Threading.Tasks;
using CodeAsCommandLine.Tests.TestInput;

namespace CodeAsCommandLine.ConsoleAppExample
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var app = CodeConvert.ForType<StaticMethods>()
                                 .ForType<AsyncStaticMethods>()
                                 .CreateConsoleApplication();
            await app.RunAsync(args);
        }
    }
}