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

        public static IEnumerable<EtlStepSignature> SelectEtlStepSignature(this IEnumerable<Type> types)
        {
            return types.Select(ToEtlStepSignature);
        }

        public static EtlStepSignature ToEtlStepSignature(this Type etlStep)
        {
            return new EtlStepSignature { Type = etlStep };
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