using System;
using System.Collections.Generic;
using System.Linq;
using FunctionalSharp.OptionTypes;
using TypescriptGeneration.Model;
using System.Reflection;

namespace TypedDotNet.Typescriptcreators
{
    public class TypescriptClassCreator : TypescriptTypeCreatorBase, ITypescriptTypeCreator
    {
        private ITypescriptTypeCreator typeCreator { get; set; }

        public void Initialize(ITypescriptTypeCreator typeCreator)
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
                var newClass = new TypescriptClass
                {
                    Name = NameWithoutGeneric(type)
                };
                model.knownTypes.Add(type, newClass.ToTypescriptType());
                newClass.BaseClass = GetBaseClassFor(type.BaseType, model);
                newClass.Content = new TypescriptClassContentList(GetClassContent(type, model));
                newClass.GenricTypeParameters = TypescriptTypeCreatorBase.GetGenericTypeParametersFor(type);

                return newClass.ToTypescriptType();
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
                GenericArguments =  TypescriptTypeCreatorBase.GetGenericTypeParametersAsArguments(baseType) 
            }.ToOption();
        }

        protected virtual IEnumerable<TypescriptClassContent> GetClassContent(Type type, TypescriptModel model)
        {
            return GetTypescriptProperties(type, model).Select(property => property.ToTypescriptClassContent());
        }

        protected virtual IEnumerable<TypescriptProperty> GetTypescriptProperties(Type type, TypescriptModel model)
        {
            return type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Select(property => new TypescriptProperty
            {
                Name = property.Name,
                Accesability = TypescriptAccesModifier.@public,
                DefaultValue = new None<String>(),
                Type = typeCreator.GetTypeFor(property.PropertyType, model)
            });
        }
    }
}