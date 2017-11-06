using CodeAsCommandLine.Tests.TestInput;

namespace CodeAsCommandLine.ConsoleAppExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var app = CodeConvert.For<StaticMethods>().CreateConsoleApplication();
            app.Run();
        }
    }
}