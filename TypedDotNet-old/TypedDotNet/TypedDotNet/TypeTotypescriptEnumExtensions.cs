using System;
using System.Collections.Generic;
using System.Linq;
using TypescriptGeneration.Model;

namespace TypedDotNet
{
    public static class TypeToTypescriptEnumExtensions
    {
        public static TypescriptType EnumTypeToTypescriptEnum(this Type type)
        {
            if (type.IsEnum == false)
            {
                throw new ArgumentOutOfRangeException($"Type {type.Name} must be of type enum");
            }
            var values = ToEnumerable(Enum.GetValues(type));

            return new TypescriptEnumerable
            {
                Name = type.Name,
                Options = new List<string>(values.Select(x => x.ToString()))
            }.ToTypescriptType();
        }

        private static IEnumerable<Object> ToEnumerable(Array values)
        {
            foreach (var value in values)
            {
                yield return value;
            }
        }
    }
}