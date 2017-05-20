using System;

namespace FileEtl.Core
{
    public static class ConfigurableInterfaceExtensions
    {
        public static Type GetIConfigurableConfigurationType(this Type type)
        {
            return type.GetLastGenericInterfaceTypeArgument(typeof(IConfigurableEtlStep<>));
        }

        public static bool ImplementsIConfigurableEtlStep(this Type type)
        {
            return type.ImplementsOpenGenericInterface(typeof(IConfigurableEtlStep<>));
        }
    }
}