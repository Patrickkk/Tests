using System;
using FunctionalSharp.DiscriminatedUnions;
using TypescriptGeneration.Model;
using FunctionalSharp.OptionTypes;
using System.Collections.Generic;
using System.Linq;

namespace TypedDotNet
{
    public class TypescriptInterfaceCreator : TypescriptTypeCreatorBase, ITypescriptTypeCreator
    {
        private ITypescriptTypeCreator typeCreator { get; set; }

        public void Initialize(ITypescriptTypeCreator typecreator)
        {
            this.typeCreator = typeCreator;
        }

        public void SetTypeCreatorRoot(ITypescriptTypeCreator typescriptTypeCreatorRoot)
        {
            this.typeCreator = typescriptTypeCreatorRoot;
        }

        public TypescriptType GetTypeFor(Type type, TypescriptModel model)
        {
            if (model.knownTypes.ContainsKey(type))
            {
                return model.knownTypes[type];
            }
            else
            {
                var newInterface = new TypescriptInterface
                {
                    Name = NameWithoutGeneric(type)
                };
                model.knownTypes.Add(type, newInterface.ToTypescriptType());
                // TODO: implement inherrited interfaces. newInterface. = GetBaseClassFor(type.BaseType, model);
                newInterface.Content = new TypescriptInterfaceContentList( GetInterfaceContent(type, model));
                newInterface.GenricTypeParameters = TypescriptTypeCreatorBase.GetGenericTypeParametersFor(type);

                return newInterface.ToTypescriptType();
            }
        }

        protected virtual IOption<TypescriptBaseClass> GetBaseClassFor(Type baseType, TypescriptModel model)
        {
            if (baseType == typeof(Object))
            {
                return new None<TypescriptBaseClass>();
            }
            return new TypescriptBaseClass
            {
                Name = NameWithoutGeneric(baseType),
                GenericArguments = TypescriptTypeCreatorBase.GetGenericTypeArgumentsFor(typeCreator, baseType, model)
            }.ToOption();
        }

        protected virtual IEnumerable<TypescriptInterfaceContent> GetInterfaceContent(Type type, TypescriptModel model)
        {
            return GetTypescriptProperties(type, model).Select(property => new TypescriptInterfaceContent(property));
        }

        protected virtual IEnumerable<TypescriptProperty> GetTypescriptProperties(Type type, TypescriptModel model)
        {
            return type.GetProperties().Select(property => new TypescriptProperty
            {
                Name = property.Name,
                Accesability = TypescriptAccesModifier.@public,
                DefaultValue = new None<String>(),
                Type = typeCreator.GetTypeFor(property.PropertyType, model)
            });
        }
    }
}