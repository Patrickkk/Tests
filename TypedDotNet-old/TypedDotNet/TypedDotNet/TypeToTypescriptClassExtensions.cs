using FunctionalSharp.OptionTypes;
using FunctionalSharp.PatternMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TypescriptGeneration.Model;

namespace TypedDotNet
{
    public static class TypeToTypescriptClassExtensions
    {
        public static TypescriptType ClassTypeToTypescriptClass(this Type type, ITypescriptTypeCreator typeCreator, TypescriptModel model)
        {
            // TODO Check if type is indeed a class..
            if (model.knownTypes.ContainsKey(type))
            {
                return model.knownTypes[type];
            }
            else
            {
                var newClass = new TypescriptClass
                {
                    Name = type.NameWithoutGeneric()
                };
                model.knownTypes.Add(type, newClass.ToTypescriptType());
                newClass.BaseClass = type.BaseType.ToTypescriptBaseClass(model);
                newClass.Content = type.GetTypescriptProperties(typeCreator, model).ToClassContent();
                newClass.GenricTypeParameters = TypescriptTypeCreatorBase.GetGenericTypeParametersFor(type);

                return newClass.ToTypescriptType();
            }
        }

        public static TypescriptClassContentList ToClassContent(this IEnumerable<TypescriptProperty> properties)
        {
            return new TypescriptClassContentList(properties.Select(property => property.ToTypescriptClassContent()));
        }

        public static IEnumerable<TypescriptProperty> GetTypescriptProperties(this Type type, ITypescriptTypeCreator typeCreator, TypescriptModel model)
        {
            return type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Select(property => new TypescriptProperty
            {
                Name = property.Name,
                Accesability = TypescriptAccesModifier.@public,
                DefaultValue = new None<String>(),
                Type = typeCreator.GetTypeFor(property.PropertyType, model)
            });
        }

        public static IOption<TypescriptBaseClass> ToTypescriptBaseClass(this Type baseType, TypescriptModel model)
        {
            return baseType.Match()
                .With<IOption<TypescriptBaseClass>>(typeof(Object), new None<TypescriptBaseClass>())
                .Else(NewTypescriptBaseClass);
        }

        private static IOption<TypescriptBaseClass> NewTypescriptBaseClass(Type baseType)
        {
            return new TypescriptBaseClass
            {
                Name = baseType.NameWithoutGeneric(),
                GenericArguments = baseType.GetGenericTypeParametersAsArguments()
            }.ToOption();
        }
    }
}
