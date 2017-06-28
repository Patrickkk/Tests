using System;

namespace FileEtl.Core
{
    public static class ConfigurableInterfaceExtensions
    {
        public static Type GetIConfigurableConfigurationType(this Type type)
        {
            return type.GetLastGenericInterfaceTypeArgument(typeof(IConfigurableEtlStep<>));
        }

        public static object GetNewIConfigurableConfigurationObject(this Type type)
        {
            var configType = type.GetLastGenericInterfaceTypeArgument(typeof(IConfigurableEtlStep<>));
            if(configType == null)
            {
                return new object();
            }
            return Activator.CreateInstance(configType);
        }

        public static bool ImplementsIConfigurableEtlStep(this Type type)
        {
            return type.ImplementsOpenGenericInterface(typeof(IConfigurableEtlStep<>));
        }
    }
}