using System;
using System.Collections.Generic;
using System.Text;
using CodeAsCommandLine.Tests.TestInput;
using Xunit;

namespace CodeAsCommandLine.Tests
{
    public class IntergationTests
    {
        [Fact]
        public void TestSimpleStaticMethodWithoutParameters()
        {
            RunForStaticMethodsCommand(nameof(StaticMethods.WithoutParameters));
        }

        [Fact]
        public void SimpleIntAndStringValueTest()
        {
            RunForStaticMethodsCommand($"{nameof(StaticMethods.Parameters)} -text stringValue -numberOfTimes 10");
        }

        [Fact]
        public void SimpleIntAndStringValueTestWithShorts()
        {
            RunForStaticMethodsCommand($"{nameof(StaticMethods.Parameters)} -t stringValue -n 10");
        }

        [Fact]
        public void TypeWithStringBasedConstructorShouldBeParsed()
        {
            RunForStaticMethodsCommand($"{nameof(StaticMethods.UriParameter)} -uri http://localhost:8000 -numberOfTimes 10");
        }

        [Fact]
        public void GenericMethod()
        {
            throw new NotImplementedException();
            RunForStaticMethodsCommand($"{nameof(StaticMethods.Generic)} -T system.string -uri http://localhost:8000 -numberOfTimes 10");
        }

        private static void RunForStaticMethodsCommand(string command)
        {
            var commandRunner = CodeConvert.For<StaticMethods>().CreateRunner();
            commandRunner.RunCommand(command);
        }
    }
}