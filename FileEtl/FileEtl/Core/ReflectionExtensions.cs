using System;
using System.Collections.Generic;
using System.Linq;

namespace FileEtl.Core
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<Type> GetTypesImplementingOpenGenericInterface(this IEnumerable<Type> types, Type interfaceType)
        {
            return types.Where(x => x.ImplementsOpenGenericInterface(interfaceType));
        }

        public static bool ImplementsOpenGenericInterface(this Type type, Type interfaceType)
        {
            return type.GetInterfaces()
                .Where(@interface => @interface.IsGenericType)
                .Select(implementedInterface => implementedInterface.GetGenericTypeDefinition())
                .Contains(interfaceType);
        }

        public static IEnumerable<Type> GetGenericInterfaceTypeArguments(this Type type, Type interfaceType)
        {
            return type.GetInterfaces()
                .Where(@interface => @interface.IsGenericType)
                .Single(x => x.InterfaceIsImplementationOfGenericInterface(interfaceType))
                .GetGenericArguments();
        }

        public static Type GetLastGenericInterfaceTypeArgument(this Type type, Type interfaceType)
        {
            return type.GetGenericInterfaceTypeArguments(interfaceType).Last();
        }

        private static bool InterfaceIsImplementationOfGenericInterface(this Type type, Type interfaceType)
        {
            if (!type.IsInterface)
            {
                throw new ArgumentOutOfRangeException($"The type {type.FullName} is not an interface");
            }
            return type.GetGenericTypeDefinition() == interfaceType;
        }

        public static IEnumerable<T> ConcatSingle<T>(this IEnumerable<T> sequence, T addition)
        {
            return sequence.Concat(new T[] { addition });
        }

        public static IEnumerable<T> ExceptSingle<T>(this IEnumerable<T> sequence, T addition)
        {
            return sequence.Except(new T[] { addition });
        }

        public static bool None<T>(this IEnumerable<T> values)
        {
            return !values.Any();
        }
    }
}