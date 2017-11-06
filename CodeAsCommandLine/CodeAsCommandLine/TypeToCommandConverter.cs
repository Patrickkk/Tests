using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeAsCommandLine.Model;

namespace CodeAsCommandLine
{
    public class TypeToCommandConverter
    {
        public TypeToCommandConverter()
        {
        }

        public static IEnumerable<Command> CommandsForType(Type type)
        {
            var commands = type.GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Select(GetCommandForMethod).ToList();
            return AddShortNames(commands);
        }

        private static IEnumerable<Command> AddShortNames(List<Command> commands)
        {
            var length = StringLengthUntillNoMoreDuplicates(commands.Select(x => x.CommandName));
            commands.ToList().ForEach(x => x.Short = CreateShortName(x.CommandName, length));
            return commands.ToList();
        }

        private static Command GetCommandForMethod(MethodInfo method)
        {
            return new Command
            {
                ClassPrefix = method.ReflectedType.Name,
                CommandName = method.Name,
                CommandParameters = AddShortNames(GetCommandParametersFor(method.GetParameters())),
                Method = method
            };
        }

        private static List<CommandParameter> GetCommandParametersFor(ParameterInfo[] parameterInfo)
        {
            return parameterInfo.Select(parameter =>
            new CommandParameter
            {
                Name = parameter.Name,
                Type = parameter.ParameterType,
                Position = parameter.Position,
            }).ToList();
        }

        private static List<CommandParameter> AddShortNames(IEnumerable<CommandParameter> parameters)
        {
            var length = StringLengthUntillNoMoreDuplicates(parameters.Select(x => x.Name));
            parameters.ToList().ForEach(x => x.Short = CreateShortName(x.Name, length));
            return parameters.ToList();
        }

        private static string CreateShortName(string parameterName, int length)
        {
            return parameterName.Substring(0, length);
        }

        private static int StringLengthUntillNoMoreDuplicates(IEnumerable<string> values)
        {
            var length = 0;
            while (true)
            {
                length += 1;
                var duplicates = values
                    .Select(value => value.Substring(0, length))
                    .GroupBy(value => value)
                    .Any(value => value.Count() > 1);
                if (!duplicates)
                {
                    return length;
                }
                if (length > values.Max(x => x.Length))
                {
                    throw new Exception("values aready contains duplicates at the maximum length");
                }
            }
        }
    }
}