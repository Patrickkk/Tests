using System.Collections.Generic;
using System.Linq;
using CodeAsCommandLine.Model;

namespace CodeAsCommandLine
{
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
            var methodType = GetMethodType();
            switch ()
            //static only
            commandToRun.Method.Invoke(null, argumentValues);
        }
    }
}