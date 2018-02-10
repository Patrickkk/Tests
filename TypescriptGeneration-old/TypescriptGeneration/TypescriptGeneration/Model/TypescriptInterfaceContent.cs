
namespace TypescriptGeneration.Model
{
    using System;
    using System.Linq;
    using FunctionalSharp.DiscriminatedUnions;
    using System.Collections.Generic;
    

    /// <summary>
    /// Represents a discriminated union of the following types:TypescriptFunctionSignature,TypescriptProperty. 
    /// </summary>
    [Serializable]
    public class TypescriptInterfaceContent : DiscriminatedUnion<TypescriptFunctionSignature,TypescriptProperty>
    {
        /// <summary>
        /// Creates a new TypescriptInterfaceContent representing a TypescriptFunctionSignature.
        /// </summary>
        /// <param name="typescriptFunctionSignature"></param>
        public TypescriptInterfaceContent(TypescriptFunctionSignature typescriptFunctionSignature) : base(typescriptFunctionSignature) { }
        /// <summary>
        /// Creates a new TypescriptInterfaceContent representing a TypescriptProperty.
        /// </summary>
        /// <param name="typescriptProperty"></param>
        public TypescriptInterfaceContent(TypescriptProperty typescriptProperty) : base(typescriptProperty) { }
    }


    /// <summary>
    /// Represents a list of TypescriptInterfaceContent. 
    /// </summary>
    [Serializable]
    public class TypescriptInterfaceContentList : List<TypescriptInterfaceContent>
    {
        /// <summary>
        /// creates a new TypescriptInterfaceContentList without contents.
        /// </summary>
        public TypescriptInterfaceContentList() : base() { }

        /// <summary>
        /// creates a new TypescriptInterfaceContentList with the starting values as its contents.
        /// <param name="values">startingValues</param>
        /// </summary>
        public TypescriptInterfaceContentList(IEnumerable<TypescriptInterfaceContent> values) : base(values) { }

		/// <summary>
        /// creates a new TypescriptInterfaceContentList with the starting values as its contents.
        /// <param name="typescriptFunctionSignature">startingValues</param>
        /// </summary>
        public TypescriptInterfaceContentList(IEnumerable<TypescriptFunctionSignature> typescriptFunctionSignature) : base(typescriptFunctionSignature.Select(x => x.ToTypescriptInterfaceContent())) { }

        /// <summary>
        /// Creates and adds a new TypescriptInterfaceContent using the given TypescriptFunctionSignature.
        /// </summary>
        /// <param name="typescriptFunctionSignature">The typescriptFunctionSignature to add.</param>
        public void Add(TypescriptFunctionSignature typescriptFunctionSignature)
        {
            Add(new TypescriptInterfaceContent(typescriptFunctionSignature));
        }

		/// <summary>
        /// creates a new TypescriptInterfaceContentList with the starting values as its contents.
        /// <param name="typescriptProperty">startingValues</param>
        /// </summary>
        public TypescriptInterfaceContentList(IEnumerable<TypescriptProperty> typescriptProperty) : base(typescriptProperty.Select(x => x.ToTypescriptInterfaceContent())) { }

        /// <summary>
        /// Creates and adds a new TypescriptInterfaceContent using the given TypescriptProperty.
        /// </summary>
        /// <param name="typescriptProperty">The typescriptProperty to add.</param>
        public void Add(TypescriptProperty typescriptProperty)
        {
            Add(new TypescriptInterfaceContent(typescriptProperty));
        }

    }

    /// <summary>
    /// provides extensionmethods for TypescriptInterfaceContent. 
    /// </summary>
    public static class TypescriptInterfaceContentExtensions
    {
        /// <summary>
        /// Turns the TypescriptFunctionSignature into a TypescriptInterfaceContent.
        /// </summary>
        /// <param name="typescriptFunctionSignature"></param>
        public static TypescriptInterfaceContent ToTypescriptInterfaceContent(this TypescriptFunctionSignature typescriptFunctionSignature)
        {
            return new TypescriptInterfaceContent(typescriptFunctionSignature);
        }
        /// <summary>
        /// Turns the TypescriptProperty into a TypescriptInterfaceContent.
        /// </summary>
        /// <param name="typescriptProperty"></param>
        public static TypescriptInterfaceContent ToTypescriptInterfaceContent(this TypescriptProperty typescriptProperty)
        {
            return new TypescriptInterfaceContent(typescriptProperty);
        }
        public static void Match(this IEnumerable<TypescriptInterfaceContent> values, Action<TypescriptFunctionSignature> actionForTypescriptFunctionSignature,Action<TypescriptProperty> actionForTypescriptProperty)
        {
            values.Match<TypescriptInterfaceContent, TypescriptFunctionSignature,TypescriptProperty>(actionForTypescriptFunctionSignature,actionForTypescriptProperty);
        }

        public static IEnumerable<TreturnType> Match<TreturnType>(this IEnumerable<TypescriptInterfaceContent> values, Func<TypescriptFunctionSignature, TreturnType> functionForTypescriptFunctionSignature,Func<TypescriptProperty, TreturnType> functionForTypescriptProperty)
        {
            return values.Match<TypescriptInterfaceContent, TypescriptFunctionSignature,TypescriptProperty, TreturnType>(functionForTypescriptFunctionSignature,functionForTypescriptProperty);
        }
    }
}

