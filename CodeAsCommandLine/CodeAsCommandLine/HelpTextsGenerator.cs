using System;
using System.Collections.Generic;
using System.Linq;
using CodeAsCommandLine.Model;

namespace CodeAsCommandLine
{
    public class HelpTextsGenerator
    {
        public HelpTextsGenerator()
        {
        }

        public string HelpTextForCommand(Command command)
        {
            return command.CommandName + "," + command.Short + command.HelpText +
                Environment.NewLine +
                HelpForParameters(command.CommandParameters);
        }

        private string HelpForParameters(IEnumerable<CommandParameter> commandParameters)
        {
            return commandParameters
                .Select(parameter => $"{parameter.Name}, {parameter.Short} type: {parameter.Type} {parameter.HelpText}")
                .StringJoin(Environment.NewLine);
        }

        public string HelpTextForCommands(IEnumerable<Command> commands)
        {
            return commands.Select(HelpTextForCommand).StringJoin(Environment.NewLine + Environment.NewLine);
        }
    }
}