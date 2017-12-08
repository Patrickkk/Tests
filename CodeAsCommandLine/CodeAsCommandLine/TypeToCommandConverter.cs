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

        public static CommandClass CommandsForType(Type type, List<CommandClass> currentCommandClasses)
        {
            return new CommandClass
            {
                ClassName = type.Name,
                ClassNameShort = ShortNameCreator.GetShortNameFor(type.Name, currentCommandClasses.Select(x => x.ClassNameShort)),
                Commands = GetCommandsForType(type)
            };
        }

        private static List<Command> GetCommandsForType(Type type)
        {
            return type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Aggregate(Enumerable.Empty<Command>(), GetCommandForMethod).ToList();
        }

        private static IEnumerable<Command> GetCommandForMethod(IEnumerable<Command> exsistingValues, MethodInfo method)
        {
            return exsistingValues.Append(new Command
            {
                CommandName = method.Name,
                CommandParameters = GetCommandParametersFor(method.GetParameters()),
                Short = ShortNameCreator.GetShortNameFor(method.Name, exsistingValues.Select(x => x.Short)),
                Method = method
            });
        }

        private static List<CommandParameter> GetCommandParametersFor(ParameterInfo[] parameterInfo)
        {
            return parameterInfo.Aggregate(Enumerable.Empty<CommandParameter>(), CreateCommandParameter).ToList();
        }

        private static IEnumerable<CommandParameter> CreateCommandParameter(IEnumerable<CommandParameter> exsistingValues, ParameterInfo parameter)
        {
            return exsistingValues.Append(new CommandParameter
            {
                Name = parameter.Name,
                Type = parameter.ParameterType,
                Position = parameter.Position,
                Short = ShortNameCreator.GetShortNameFor(parameter.Name, exsistingValues.Select(x => x.Short)),
            });
        }
    }
}