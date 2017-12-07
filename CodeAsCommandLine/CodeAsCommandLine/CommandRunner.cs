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
        private readonly List<Command> commands;

        public CommandRunner(List<Command> commands, IArgumentParser argumentParser)
        {
            this.commands = commands;
            this.arumentParser = argumentParser;
        }

        public Task RunCommandAsync(string command)
        {
            var args = CommandParser.ParseCommand(command);
            return RunAsync(args);
        }

        public Task RunAsync(string[] args, Func<Type, object> instanceLoader = null)
        {
            var command = args[0];

            // TODO singleordefault etc.
            var commandToRun = this.commands.Single(x => x.CommandName == command || x.Short == command);

            return RunCommandAsync(commandToRun, args, instanceLoader);
        }

        private async Task RunCommandAsync(Command commandToRun, string[] args, Func<Type, object> instanceLoader)
        {
            var argumentValues = this.arumentParser.Parse(args, commandToRun);
            var methodType = GetMethodType(commandToRun);
            switch (methodType)
            {
                case MethodType.Static:
                    commandToRun.Method.Invoke(null, argumentValues);
                    break;

                case MethodType.StaticAsync:
                    await (Task)commandToRun.Method.Invoke(null, argumentValues);
                    break;

                case MethodType.Instance:
                    var instance = instanceLoader(commandToRun.Method.DeclaringType);
                    commandToRun.Method.Invoke(instance, argumentValues);
                    break;

                case MethodType.InstanceAsync:
                    break;

                default:
                    break;
            }
        }

        private MethodType GetMethodType(Command commandToRun)
        {
            if (commandToRun.Method.IsStatic)
            {
                if (typeof(Task).IsAssignableFrom(commandToRun.Method.ReturnType))
                {
                    return MethodType.StaticAsync;
                }
                else
                {
                    return MethodType.Static;
                }
            }
            else
            {
                if (typeof(Task).IsAssignableFrom(commandToRun.Method.ReturnType))
                {
                    return MethodType.InstanceAsync;
                }
                else
                {
                    return MethodType.Instance;
                }
            }
        }
    }
}