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

        public string HelpTextForCommand(CommandClassWithCommand command)
        {
            return WithNewLines(
                $"{command.CommandClass.ClassName}[{command.CommandClass.ClassNameShort}] {command.Command.CommandName},[{command.Command.Short}] {command.Command.HelpText}" +
                HelpForParameters(command.Command.CommandParameters));
        }

        private string HelpForParameters(IEnumerable<CommandParameter> commandParameters)
        {
            return commandParameters
                .Select(parameter => $"{parameter.Name}[{parameter.Short}] type: {parameter.Type} {parameter.HelpText}")
                .Where(x => !string.IsNullOrEmpty(x))
                .StringJoin(Environment.NewLine);
        }

        public string HelpTextForCommands(IEnumerable<CommandClassWithCommand> commands)
        {
            return commands.Select(HelpTextForCommand).StringJoin(Environment.NewLine + Environment.NewLine);
        }

        private string WithNewLines(params string[] values)
        {
            return values.Where(x => !string.IsNullOrEmpty(x)).StringJoin(Environment.NewLine);
        }
    }
}