using System;
using CodeAsCommandLine.Model;
using Xunit;

namespace CodeAsCommandLine.Tests
{
    public class HelpTextGeneratorTests
    {
        [Fact]
        public void HelpTextGeneratorShouldGenerateTextForMultipleCommands()
        {
            var commands = new Command[]
            {
                new Command{ CommandName = "SomeMethod", CommandParameters = { new CommandParameter { Name = "number", Short="n", HelpText = "", Type = typeof(int)} } },
                new Command{ CommandName = "SomeOtherMethod", CommandParameters = { new CommandParameter { Name = "text", Short="n", HelpText = "", Type = typeof(string)} } },
            };

            var helptext = new HelpTextsGenerator().HelpTextForCommands(commands);
            throw new NotImplementedException();
        }
    }
}