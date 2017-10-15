using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeAsCommandLine.Model;

namespace CodeAsCommandLine
{
    public class CommandRunnerBuilder
    {
        private List<Command> Commands { get; set; } = new List<Command>();

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

    public class CommandRunner
    {
        private readonly IArgumentParser arumentParser;
        private readonly List<Command> commands;

        public CommandRunner(List<Command> commands, IArgumentParser argumentParser)
        {
            this.commands = commands;
            this.arumentParser = argumentParser;
        }

        public void RunCommand(string command)
        {
            var args = CommandParser.ParseCommand(command);
            Run(args);
        }

        public void Run(string[] args)
        {
            var command = args[0];

            // TODO singleordefault etc.
            var commandToRun = this.commands.Single(x => x.CommandName == command || x.Short == command);

            RunCommand(commandToRun, args);
        }

        private void RunCommand(Command commandToRun, string[] args)
        {
            var argumentValues = this.arumentParser.Parse(args, commandToRun);

            //static only
            commandToRun.Method.Invoke(null, argumentValues);
        }
    }
}