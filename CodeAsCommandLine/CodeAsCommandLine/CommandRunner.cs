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
            switch (methodType)
            {
                case MethodType.Static:
                    commandToRun.Method.Invoke(null, argumentValues);
                    break;

                case MethodType.StaticAsync:
                    commandToRun.Method.Invoke(null, argumentValues);
                    break;

                case MethodType.Instance:
                    break;

                case MethodType.InstanceAsync:
                    break;

                default:
                    break;
            }
            //static only
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