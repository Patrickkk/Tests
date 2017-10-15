using System;
using System.Collections.Generic;
using System.Linq;
using CodeAsCommandLine.Model;

namespace CodeAsCommandLine
{
    internal class PositionalArgumentParser : IArgumentParser
    {
        public PositionalArgumentParser()
        {
        }

        public object[] Parse(string[] args, Command commandToRun)
        {
            return commandToRun.CommandParameters
                .Select(x => ParseArgForParameter(args, x))
                .ToArray();
        }

        private static object ParseArgForParameter(string[] args, CommandParameter parameter)
        {
            var argumentTags = args.Where(arg => string.Equals(parameter.Name, arg.TrimStart('-'), StringComparison.OrdinalIgnoreCase));
            if (argumentTags.Count() > 1)
            {
                throw new Exception($"More than one value found for the parameter {parameter.Name}");// TODO improve
            }

            if (!argumentTags.Any())
            {
                throw new Exception();
            }

            var argumentTag = argumentTags.Single();
            var tagIndex = Array.IndexOf(args, argumentTag);
            if (args.Length < tagIndex + 2)
            {
                throw new Exception();
            }

            var stringValue = args[tagIndex + 1];
            return ParseStringValue(stringValue, parameter.Type);
        }

        private static object ParseStringValue(string stringValue, Type type)
        {
            var types = new Dictionary<Type, Func<string, object>>
            {
                { typeof(string), str => { return str; } },
                { typeof(int), str => { return int.Parse(str); } },
                { typeof(float), str => { return float.Parse(str); } },
                { typeof(decimal), str => { return decimal.Parse(str); } },
                { typeof(bool), str => { return bool.Parse(str); } },
            };
            if (types.ContainsKey(type))
            {
                return types[type].Invoke(stringValue);
            }
            else
            {
                return TryConstructingTypeWithStringConstructor(stringValue, type);
            }
        }

        private static object TryConstructingTypeWithStringConstructor(string stringValue, Type type)
        {
            var constructors = type.GetConstructors(System.Reflection.BindingFlags.CreateInstance);

            // todo make a lot more robust.
            var stringBasedconstructor = constructors.SingleOrDefault(constructor => constructor.GetParameters().Count() == 1 && constructor.GetParameters().Single().ParameterType == typeof(string));
            if (stringBasedconstructor != null)
            {
                return Activator.CreateInstance(type, stringValue);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}