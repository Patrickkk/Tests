using System;
using System.Collections.Generic;
using System.Linq;
using CodeAsCommandLine.Model;

namespace CodeAsCommandLine
{
    public class CommandRunnerBuilder
    {
        private Func<Type, object> instanceProvider;

        private List<CommandClass> CommandClasses { get; set; } = new List<CommandClass>();

        public ConsoleApplication CreateConsoleApplication()
        {
            return new ConsoleApplication(GetFlattenedClasses(), this.CreateRunner(), new HelpTextsGenerator(), instanceProvider);
        }

        private List<CommandClassWithCommand> GetFlattenedClasses()
        {
            return this.CommandClasses.SelectMany(x => x.Commands, (command, method) => new CommandClassWithCommand { Command = method, CommandClass = command }).ToList();
        }

        public CommandRunner CreateRunner()
        {
            return new CommandRunner(GetFlattenedClasses(), new PositionalArgumentParser(), instanceProvider);
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
            this.CommandClasses.Add(TypeToCommandConverter.CommandsForType(type, this.CommandClasses));
            return this;
        }

        public CommandRunnerBuilder WithInstanceCreator(Func<Type, object> instanceProvider)
        {
            this.instanceProvider = instanceProvider;
            return this;
        }
    }

    public class CommandClassWithCommand
    {
        public CommandClass CommandClass { get; set; }

        public Command Command { get; set; }
    }
}