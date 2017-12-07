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

        public static IEnumerable<Command> CommandsForType(Type type, List<Command> currentCommands)
        {
            return type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                               .Aggregate(currentCommands, GetCommandForMethod).ToList();
        }

        private static List<Command> GetCommandForMethod(List<Command> currentComands, MethodInfo method)
        {
            var command = new Command
            {
                ClassPrefix = method.ReflectedType.Name,
                ClassShortPrefix = ShortNameCreator.GetShortNameFor(method.ReflectedType.Name, currentComands.Select(x => x.ClassShortPrefix)),
                CommandName = method.Name,
                CommandParameters = GetCommandParametersFor(method.GetParameters()),
                Method = method
            };
            currentComands.Add(command);
            return currentComands;
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
    }
}