using System;
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
                                 .ForType<InstanceTestClass>()
                                 .WithInstanceCreator(InstanceProvider)
                                 .CreateConsoleApplication();
            await app.RunAsync(args);
        }

        private static InstanceTestClass instanceTestClass = new InstanceTestClass("Console instance class");

        private static object InstanceProvider(Type type)
        {
            return instanceTestClass;
        }
    }
}