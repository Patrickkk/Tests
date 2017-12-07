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
            await Assert.ThrowsAsync<Exception>(async () => { await RunCommand("NonExsisting"); });
        }

        [Fact]
        public async Task RunningCommandWithIncorrectParametersShouldShowErrorAndHelpTextForCommand()
        {
            await Assert.ThrowsAsync<Exception>(async () => { await RunCommand($"{nameof(StaticMethods.Parameters)} -text stringValue -numberOfTimes textWhereNumberIsExpected"); });
        }

        [Fact]
        public async Task TestSimpleStaticMethodWithoutParameters()
        {
            await RunCommand(nameof(StaticMethods.WithoutParameters));
        }

        [Fact]
        public async Task TestSimpleStaticAsyncMethodWithoutParameters()
        {
            await RunCommand(nameof(AsyncStaticMethods.AsyncMethod));
        }

        [Fact]
        public async Task SimpleIntAndStringValueTest()
        {
            await RunCommand($"{nameof(StaticMethods.Parameters)} -text stringValue -numberOfTimes 10");
        }

        [Fact]
        public async Task SimpleIntAndStringValueTestWithShorts()
        {
            await RunCommand($"{nameof(StaticMethods.Parameters)} -t stringValue -n 10");
        }

        [Fact]
        public async Task TypeWithStringBasedConstructorShouldBeParsed()
        {
            await RunCommand($"{nameof(StaticMethods.UriParameter)} -uri http://localhost:8000 -numberOfTimes 10");
        }

        [Fact]
        public async Task GenericMethod()
        {
            throw new NotImplementedException();
            await RunCommand($"{nameof(StaticMethods.Generic)} -T system.string -uri http://localhost:8000 -numberOfTimes 10");
        }

        private static Task RunCommand(string command)
        {
            var commandRunner = CodeConvert.ForType<StaticMethods>()
                                           .ForType<AsyncStaticMethods>()
                                           .ForType<InstanceTestClass>()
                                           .WithInstanceCreator(InstanceProvider)
                                           .CreateRunner();
            return commandRunner.RunCommandAsync(command);
        }

        private static InstanceTestClass instanceTestClass = new InstanceTestClass("Tests");

        private static object InstanceProvider(Type type)
        {
            return instanceTestClass;
        }
    }
}