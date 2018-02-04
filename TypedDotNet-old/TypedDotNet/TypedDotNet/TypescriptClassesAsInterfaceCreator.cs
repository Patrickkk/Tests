using FunctionalSharp.PatternMatching;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypescriptGeneration.Model;

namespace TypedDotNet
{
    public class TypescriptClassesAsInterfaceCreator : ITypescriptTypeCreator
    {
        public static Dictionary<Type, TypescriptType> StandardMappings = new Dictionary<Type, TypescriptType>
        {
            [typeof(Guid)] = new TypescriptClass { Name = "string" }.ToTypescriptType(),
            [typeof(DateTime)] = new TypescriptClass { Name = "Date" }.ToTypescriptType(),
        };

        public TypescriptClassesAsInterfaceCreator()
        {
        }

        public TypescriptType GetTypeFor(Type type, TypescriptModel model)
        {
            var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);

            return type.Match()
                .With(IsGenericNullable, typeMatch => GetTypeForInnerType(typeMatch, model))
                .With(IsEnumerable, CreateCollection)
                .With(typeMatch => StandardMappings.ContainsKey(typeMatch), typeMatch => StandardMappings[typeMatch])
                .With(typeMatch => typeMatch.IsGenericParameter, typeMatch => typeMatch.ToTypescriptGenericParameter())
                .With(typeMatch => typeMatch.IsTypescriptPrimitiveType(), typeMatch => typeMatch.ToTypescriptPrimitiveType())
                .With(typeMatch => typeMatch.IsClass, typeMatch => typeMatch.ClassTypeToTypescriptInterface(this, model))
                .With(typeMatch => typeMatch.IsEnum, typeMatch => typeMatch.EnumTypeToTypescriptEnum())
                .With(typeMatch => typeMatch.IsInterface, typeMatch => typeMatch.InterfaceTypeToTypescriptInterface(this, model))
                .Else(typeMatch => { throw new ArgumentOutOfRangeException($"unknown type {type?.Name}"); });
        }

        private TypescriptType CreateCollection(Type type)
        {
            var genricParameters = new TypescriptGenericTypeParameters();
            genricParameters.Add(new TypescriptGenericTypeParameter { Name = type.GetGenericArguments()[0].Name });
            return new TypescriptClass { Name = "Array", GenricTypeParameters = genricParameters }.ToTypescriptType();
        }

        private bool IsEnumerable(Type arg)
        {
            return typeof(IEnumerable).IsAssignableFrom(arg);
        }

        private TypescriptType GetTypeForInnerType(Type type, TypescriptModel model)
        {
            return GetTypeFor(type.GenericTypeArguments.Single(), model);
        }

        private static bool IsGenericNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public TypescriptModel CreateTypescriptModelFor(IEnumerable<Type> types, bool IncludeDependencies = true)
        {
            // TODO: maybe change the way this currently works and return the types throughout the chain without actually adding them to the model.
            if (IncludeDependencies == false)
            {
                throw new NotImplementedException();
            }
            var model = new TypescriptModel();
            types.ToList().ForEach(x => GetTypeFor(x, model));
            return model;
        }

        public void SetTypeCreatorRoot(ITypescriptTypeCreator typescriptTypeCreatorRoot)
        {
            throw new NotImplementedException();
        }
    }
}