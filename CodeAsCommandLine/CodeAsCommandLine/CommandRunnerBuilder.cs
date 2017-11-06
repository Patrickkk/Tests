using System;
using System.Collections.Generic;
using System.Linq;
using CodeAsCommandLine.Model;

namespace CodeAsCommandLine
{
    public class CommandRunnerBuilder
    {
        private List<Command> Commands { get; set; } = new List<Command>();

        public ConsoleApplication CreateConsoleApplication()
        {
            return new ConsoleApplication(Commands, this.CreateRunner(), new HelpTextsGenerator());
        }

        public CommandRunner CreateRunner()
        {
            return new CommandRunner(this.Commands, new PositionalArgumentParser());
        }

        public CommandRunnerBuilder ForInstance(object instance)
        {
            throw new NotImplementedException();
        }

        public CommandRunnerBuilder ForType<Type>()
        {
            return this.ForType(typeof(Type));
        }

        public CommandRunnerBuilder ForType(Type type)
        {
            this.Commands.AddRange(TypeToCommandConverter.CommandsForType(type));
            return this;
        }
    }
}