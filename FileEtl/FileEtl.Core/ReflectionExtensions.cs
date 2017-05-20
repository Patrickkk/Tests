using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FileEtl.Core
{
    public static class ReflectionExtensions
    {
        public static Type[] RunMethodGetInputTypes(this Type etlStepType)
        {
            // TODO validate
            return etlStepType
                .EtlStepMethod()
                .GetParameters()
                .Select(x => x.ParameterType)
                .ToArray();
        }

        public static MethodInfo EtlStepMethod(this Type etlStep)
        {
            var methods = etlStep.GetMethods()
                .Where(x => x.GetCustomAttribute<EtlStepMethodAttribute>() != null);

            if (methods.Count() > 1)
            {
                throw new Exception("TODO");
            }
            if (!methods.Any())
            {
                throw new Exception($"no method found in {etlStep.Name}");
            }
            return methods.Single();
        }

        public static IEnumerable<Type> EtlSteps(this IEnumerable<Type> types)
        {
            return types.Where(type => type != typeof(IEtlStep))
                        .Where(type => type.GetInterfaces().Contains(typeof(IEtlStep)));
        }

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