using FunctionalSharp.OptionTypes;
using FunctionalSharp.PatternMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypescriptGeneration.Model;

namespace TypedDotNet
{
    public static class TypeToTypescriptInterfaceExtensions
    {
        public static TypescriptType InterfaceTypeToTypescriptInterface(this Type type, ITypescriptTypeCreator typeCreator, TypescriptModel model)
        {
            // TODO Check and refactor this and method below.
            if (model.knownTypes.ContainsKey(type))
            {
                return model.knownTypes[type];
            }
            else
            {
                var newInterface = new TypescriptInterface
                {
                    Name = type.NameWithoutGeneric()
                };
                model.knownTypes.Add(type, newInterface.ToTypescriptType());
                // TODO: implement inherrited interfaces. newInterface. = GetBaseClassFor(type.BaseType, model);
                newInterface.BaseType = type.TypescriptImplementedInterfaces();
                newInterface.Content = type.GetInterfaceContent(typeCreator, model);
                newInterface.GenricTypeParameters = TypescriptTypeCreatorBase.GetGenericTypeParametersFor(type);

                return newInterface.ToTypescriptType();
            }
        }

        private static TypescriptInterfaceBaseTypes ClassBaseClassAndInterfacesAsBaseInterfaces(this Type classType, ITypescriptTypeCreator typeCreator, TypescriptModel model)
        {
            var interfaceBaseInterfaces = classType.TypescriptImplementedInterfaces();
            var classBaseClassAsInterface = classType.BaseType.ClassBaseClassToTypescriptInterfaceBase(typeCreator, model);
            classBaseClassAsInterface.IfNotNullDo(x => interfaceBaseInterfaces.Add(x));
            return interfaceBaseInterfaces;
        }

        public static TypescriptInterfaceBaseTypes TypescriptImplementedInterfaces(this Type type)
        {
            var interfaces = type.GetInterfaces()
                                    .Except(type.BaseType == null ? new Type[0] : type.BaseType.GetInterfaces())
                                    .Select(@interface =>
                                    new TypescriptBaseInterface
                                    {
                                        Name = @interface.NameWithoutGeneric(),
                                        GenericArguments = @interface.GetGenericTypeParametersAsArguments()
                                    });
            return new TypescriptInterfaceBaseTypes(interfaces);
        }

        public static TypescriptType ClassTypeToTypescriptInterface(this Type type, ITypescriptTypeCreator typeCreator, TypescriptModel model)
        {

            // TODO validate parameters. the input hsould be a class type...
            if (model.knownTypes.ContainsKey(type))
            {
                return model.knownTypes[type];
            }
            else
            {
                var newInterface = new TypescriptInterface
                {
                    Name = type.NameWithoutGeneric()
                };
                model.knownTypes.Add(type, newInterface.ToTypescriptType());
                // TODO: implement inherrited interfaces. newInterface. = GetBaseClassFor(type.BaseType, model);
                newInterface.BaseType = type.ClassBaseClassAndInterfacesAsBaseInterfaces(typeCreator, model);
                newInterface.Content = type.GetInterfaceContent(typeCreator, model);
                newInterface.GenricTypeParameters = TypescriptTypeCreatorBase.GetGenericTypeParametersFor(type);

                return newInterface.ToTypescriptType();
            }
        }

        private static IOption<TypescriptInterfaceBaseType> ClassBaseClassToTypescriptInterfaceBase(this Type baseType, ITypescriptTypeCreator typeCreator, TypescriptModel model)
        {
            return baseType.Match()
                   .With<IOption<TypescriptInterfaceBaseType>>(typeof(Object), new None<TypescriptInterfaceBaseType>())
                   .Else(NewTypescriptInterfaceBase);
        }

        private static IOption<TypescriptInterfaceBaseType> NewTypescriptInterfaceBase(this Type baseType)
        {
            // TODO needs to change once interface inherritance is propperly implemented.
            return new TypescriptInterfaceBaseType(new TypescriptBaseInterface
            {
                Name = baseType.NameWithoutGeneric(),
                GenericArguments = baseType.GetGenericTypeParametersAsArguments()
            }).ToOption();
        }

        public static TypescriptInterfaceContentList GetInterfaceContent(this Type type, ITypescriptTypeCreator typeCreator, TypescriptModel model)
        {
            return new TypescriptInterfaceContentList(type.GetTypescriptProperties(typeCreator, model).Select(x => x.ToTypescriptInterfaceContent()));
        }
    }
}
