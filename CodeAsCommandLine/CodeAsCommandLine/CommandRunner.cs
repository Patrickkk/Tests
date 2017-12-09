using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeAsCommandLine.Model;

namespace CodeAsCommandLine
{
    public class CommandRunner
    {
        private readonly IArgumentParser arumentParser;
        private readonly Func<Type, object> instanceProvider;
        private readonly List<CommandClassWithCommand> commands;

        public CommandRunner(List<CommandClassWithCommand> commands, IArgumentParser argumentParser, Func<Type, object> instanceProvider)
        {
            this.commands = commands;
            this.arumentParser = argumentParser;
            this.instanceProvider = instanceProvider;
        }

        public Task RunCommandAsync(string command)
        {
            var args = CommandParser.ParseCommand(command);
            return RunAsync(args);
        }

        public Task RunAsync(string[] args)
        {
            var command = args[0];

            var commandToRun = GetCommandToRun(command, args);

            return RunCommandAsync(commandToRun.Command, args);
        }

        private CommandClassWithCommand GetCommandToRun(string command, string[] args)
        {
            var matchingCommands = GetCommandsWithMathingName(command);
            if (matchingCommands.None())
            {
                throw new Exception($"No Commands found for command '{command}'");
            }

            // TODO match based on parameters
            return matchingCommands.Single();
        }

        private IEnumerable<CommandClassWithCommand> GetCommandsWithMathingName(string command)
        {
            if (command.Contains('.'))
            {
                var classPrefix = command.Split('.')[0];
                var commandName = command.Split('.')[1];
                return this.commands.Where(x => (x.CommandClass.ClassName == classPrefix || x.CommandClass.ClassNameShort == classPrefix) &&
                (x.Command.CommandName == commandName || x.Command.Short == commandName));
            }
            else
            {
                return this.commands.Where(x => x.Command.CommandName == command || x.Command.Short == command);
            }
        }

        private async Task RunCommandAsync(Command commandToRun, string[] args)
        {
            var argumentValues = this.arumentParser.Parse(args, commandToRun);
            var instance = GetInstanceOrDefault(commandToRun);
            if (typeof(Task).IsAssignableFrom(commandToRun.Method.ReturnType))
            {
                commandToRun.Method.Invoke(instance, argumentValues);
            }
            else
            {
                await (Task)commandToRun.Method.Invoke(instance, argumentValues);
            }
        }

        private object GetInstanceOrDefault(Command commandToRun)
        {
            if (commandToRun.Method.IsStatic)
            {
                return null;
            }
            return instanceProvider(commandToRun.Method.DeclaringType);
        }
    }
}