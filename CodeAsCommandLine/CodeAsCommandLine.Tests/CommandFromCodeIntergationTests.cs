using System;
using System.Threading.Tasks;
using CodeAsCommandLine.Tests.TestInput;
using Xunit;

namespace CodeAsCommandLine.Tests
{
    public class CommandFromCodeIntergationTests
    {
        [Fact]
        public async Task RunningNonExsistingCommandShouldShowHelp()
        {
            await Assert.ThrowsAsync<Exception>(async () => { await RunForStaticMethodsCommand("NonExsisting"); });
        }

        [Fact]
        public async Task RunningCommandWithIncorrectParametersShouldShowErrorAndHelpTextForCommand()
        {
            await Assert.ThrowsAsync<Exception>(async () => { await RunForStaticMethodsCommand($"{nameof(StaticMethods.Parameters)} -text stringValue -numberOfTimes textWhereNumberIsExpected"); });
        }

        [Fact]
        public async Task TestSimpleStaticMethodWithoutParameters()
        {
            await RunForStaticMethodsCommand(nameof(StaticMethods.WithoutParameters));
        }

        [Fact]
        public async Task TestSimpleStaticAsyncMethodWithoutParameters()
        {
            await RunForStaticMethodsCommand(nameof(AsyncStaticMethods.AsyncMethod));
        }

        [Fact]
        public async Task SimpleIntAndStringValueTest()
        {
            await RunForStaticMethodsCommand($"{nameof(StaticMethods.Parameters)} -text stringValue -numberOfTimes 10");
        }

        [Fact]
        public async Task SimpleIntAndStringValueTestWithShorts()
        {
            await RunForStaticMethodsCommand($"{nameof(StaticMethods.Parameters)} -t stringValue -n 10");
        }

        [Fact]
        public async Task TypeWithStringBasedConstructorShouldBeParsed()
        {
            await RunForStaticMethodsCommand($"{nameof(StaticMethods.UriParameter)} -uri http://localhost:8000 -numberOfTimes 10");
        }

        [Fact]
        public async Task GenericMethod()
        {
            throw new NotImplementedException();
            await RunForStaticMethodsCommand($"{nameof(StaticMethods.Generic)} -T system.string -uri http://localhost:8000 -numberOfTimes 10");
        }

        private static Task RunForStaticMethodsCommand(string command)
        {
            var commandRunner = CodeConvert.ForType<StaticMethods>()
                                           .ForType<AsyncStaticMethods>()
                                           .CreateRunner();
            return commandRunner.RunCommandAsync(command);
        }
    }
}