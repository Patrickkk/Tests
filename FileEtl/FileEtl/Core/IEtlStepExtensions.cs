using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FileEtl.Core
{
    public static class IEtlStepExtensions
    {
        public static IEnumerable<Type> EtlSteps(this IEnumerable<Type> types)
        {
            return types.Where(type => type != typeof(IEtlStep))
                        .Where(type => type.GetInterfaces().Contains(typeof(IEtlStep)));
        }

        public static Type[] GetEtlRunMethodInputTypes(this Type etlStepType)
        {
            // TODO validate
            return etlStepType
                .GetEtlStepRunMethod()
                .GetParameters()
                .Select(x => x.ParameterType)
                .ToArray();
        }

        public static MethodInfo GetEtlStepRunMethod(this Type etlStep)
        {
            var methods = etlStep.GetMethods()
                .Where(x => x.GetCustomAttribute<EtlStepRunMethodAttribute>() != null);

            if (methods.Count() > 1)
            {
                throw new Exception("TODO");
            }
            if (!methods.Any())
            {
                throw new Exception($"no method found in {etlStep.Name} with the appropriate 'EtlStepMethodAttribute'");
            }
            return methods.Single();
        }
    }
}