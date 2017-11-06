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
            return WithNewLines(
                command.CommandName + "," + command.Short + command.HelpText,
                HelpForParameters(command.CommandParameters));
        }

        private string HelpForParameters(IEnumerable<CommandParameter> commandParameters)
        {
            return commandParameters
                .Select(parameter => $"{parameter.Name}, {parameter.Short} type: {parameter.Type} {parameter.HelpText}")
                .Where(x => !string.IsNullOrEmpty(x))
                .StringJoin(Environment.NewLine);
        }

        public string HelpTextForCommands(IEnumerable<Command> commands)
        {
            return commands.Select(HelpTextForCommand).StringJoin(Environment.NewLine + Environment.NewLine);
        }

        private string WithNewLines(params string[] values)
        {
            return values.Where(x => !string.IsNullOrEmpty(x)).StringJoin(Environment.NewLine);
        }
    }
}