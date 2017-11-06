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
            return type.GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Select(GetCommandForMethod);
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
            var length = 0;
            while (true)
            {
                length += 1;
                var duplicates = parameters
                    .Select(x => x.Name)
                    .Select(x => x.Substring(0, length))
                    .GroupBy(x => x)
                    .Any(x => x.Count() > 1);

                if (!duplicates)
                {
                    parameters.ToList().ForEach(x => x.Short = CreateShortParam(x.Name, length));
                    return parameters.ToList();
                }
            }
        }

        private static string CreateShortParam(string parameterName, int length)
        {
            return parameterName.Substring(0, length);
        }
    }
}