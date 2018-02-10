using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FunctionalSharp.PatternMatching;
using TypescriptGeneration.Model;

namespace TypedDotNet
{
    public class TypescriptTypeCreator : ITypescriptTypeCreator
    {
        private readonly ITypescriptTypeCreator classCreator;

        private readonly ITypescriptTypeCreator interfaceCreator;

        private readonly ITypescriptTypeCreator enumCreator;

        private Dictionary<Type, TypescriptPrimitiveType> primitiveTypes = new Dictionary<Type, TypescriptPrimitiveType>
        {
            {typeof(string), TypescriptPrimitiveType.@string },
            {typeof(bool), TypescriptPrimitiveType.boolean },
            {typeof(int), TypescriptPrimitiveType.number},
            {typeof(float), TypescriptPrimitiveType.number},
            {typeof(decimal), TypescriptPrimitiveType.number},
            {typeof(double), TypescriptPrimitiveType.number},
            {typeof(byte), TypescriptPrimitiveType.number},
            {typeof(short), TypescriptPrimitiveType.number},
            {typeof(ushort), TypescriptPrimitiveType.number},
            {typeof(uint), TypescriptPrimitiveType.number},
            {typeof(void), TypescriptPrimitiveType.@void},
            {typeof(Object), TypescriptPrimitiveType.any}
        };

        public TypescriptTypeCreator(ITypescriptTypeCreator classCreator, ITypescriptTypeCreator interfaceCreator, ITypescriptTypeCreator enumCreator)
        {
            this.classCreator = classCreator;
            this.interfaceCreator = interfaceCreator;
            this.enumCreator = enumCreator;

            this.SetTypeCreatorRoot(this);
        }

        public void SetTypeCreatorRoot(ITypescriptTypeCreator typescriptTypeCreatorRoot)
        {
            this.classCreator.SetTypeCreatorRoot(typescriptTypeCreatorRoot);
            this.interfaceCreator.SetTypeCreatorRoot(typescriptTypeCreatorRoot);
            this.enumCreator.SetTypeCreatorRoot(typescriptTypeCreatorRoot);
        }

        public TypescriptType GetTypeFor(Type type, TypescriptModel model)
        {
            var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);

            return type.Match()
                .With(typeMatch => typeMatch.IsGenericParameter, typeMatch => NewGenericParameter(typeMatch))
                .With(typeMatch => primitiveTypes.ContainsKey(typeMatch), typeMatch =>  NewPrimitiveType(typeMatch))
                .With(typeMatch => typeMatch.IsClass, typeMatch => classCreator.GetTypeFor(typeMatch, model))
                .With(typeMatch => typeMatch.IsEnum, typeMatch => enumCreator.GetTypeFor(typeMatch, model))
                .With(typeMatch => typeMatch.IsInterface, typeMatch => interfaceCreator.GetTypeFor(typeMatch, model))
                .Else(typeMatch => { throw new ArgumentOutOfRangeException($"unknown type {type.Name}"); });
        }

        private TypescriptType NewPrimitiveType(Type type)
        {
            return primitiveTypes[type].ToTypescriptType();
        }

        private static TypescriptType NewGenericParameter(Type type)
        {
            return new TypescriptGenericTypeParameter { Name = type.Name }.ToTypescriptType();
        }


        // TODO create interface for this?
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
    }
}