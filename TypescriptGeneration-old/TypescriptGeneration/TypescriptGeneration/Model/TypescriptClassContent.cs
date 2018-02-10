
namespace TypescriptGeneration.Model
{
	using System;
	using System.Linq;
	using FunctionalSharp.DiscriminatedUnions;
	using System.Collections.Generic;
	

    /// <summary>
    /// Represents a discriminated union of the following types:TypescriptFunction,TypescriptProperty,TypescriptCode. 
    /// </summary>
	[Serializable]
	public class TypescriptClassContent : DiscriminatedUnion<TypescriptFunction,TypescriptProperty,TypescriptCode>
	{
		/// <summary>
		/// Creates a new TypescriptClassContent representing a TypescriptFunction.
		/// </summary>
		/// <param name="typescriptFunction"></param>
		public TypescriptClassContent(TypescriptFunction typescriptFunction) : base(typescriptFunction) { }
		/// <summary>
		/// Creates a new TypescriptClassContent representing a TypescriptProperty.
		/// </summary>
		/// <param name="typescriptProperty"></param>
		public TypescriptClassContent(TypescriptProperty typescriptProperty) : base(typescriptProperty) { }
		/// <summary>
		/// Creates a new TypescriptClassContent representing a TypescriptCode.
		/// </summary>
		/// <param name="typescriptCode"></param>
		public TypescriptClassContent(TypescriptCode typescriptCode) : base(typescriptCode) { }
	}


    /// <summary>
    /// Represents a list of TypescriptClassContent. 
    /// </summary>
    [Serializable]
    public class TypescriptClassContentList : List<TypescriptClassContent>
    {
		/// <summary>
		/// creates a new TypescriptClassContentList without contents.
		/// </summary>
		public TypescriptClassContentList() : base() { }

		/// <summary>
		/// creates a new TypescriptClassContentList with the starting values as its contents.
		/// <param name="values">startingValues</param>
		/// </summary>
		public TypescriptClassContentList(IEnumerable<TypescriptClassContent> values) : base(values) { }

		/// <summary>
		/// creates a new TypescriptClassContentList with the starting values as its contents.
		/// <param name="typescriptFunction">startingValues</param>
		/// </summary>
		public TypescriptClassContentList(IEnumerable<TypescriptFunction> typescriptFunction) : base(typescriptFunction.Select(x => x.ToTypescriptClassContent())) { }

        /// <summary>
        /// Creates and adds a new TypescriptClassContent using the given TypescriptFunction.
        /// </summary>
        /// <param name="typescriptFunction">The typescriptFunction to add.</param>
        public void Add(TypescriptFunction typescriptFunction)
        {
            Add(new TypescriptClassContent(typescriptFunction));
        }

		/// <summary>
		/// creates a new TypescriptClassContentList with the starting values as its contents.
		/// <param name="typescriptProperty">startingValues</param>
		/// </summary>
		public TypescriptClassContentList(IEnumerable<TypescriptProperty> typescriptProperty) : base(typescriptProperty.Select(x => x.ToTypescriptClassContent())) { }

        /// <summary>
        /// Creates and adds a new TypescriptClassContent using the given TypescriptProperty.
        /// </summary>
        /// <param name="typescriptProperty">The typescriptProperty to add.</param>
        public void Add(TypescriptProperty typescriptProperty)
        {
            Add(new TypescriptClassContent(typescriptProperty));
        }

		/// <summary>
		/// creates a new TypescriptClassContentList with the starting values as its contents.
		/// <param name="typescriptCode">startingValues</param>
		/// </summary>
		public TypescriptClassContentList(IEnumerable<TypescriptCode> typescriptCode) : base(typescriptCode.Select(x => x.ToTypescriptClassContent())) { }

        /// <summary>
        /// Creates and adds a new TypescriptClassContent using the given TypescriptCode.
        /// </summary>
        /// <param name="typescriptCode">The typescriptCode to add.</param>
        public void Add(TypescriptCode typescriptCode)
        {
            Add(new TypescriptClassContent(typescriptCode));
        }

    }

    /// <summary>
    /// provides extensionmethods for TypescriptClassContent. 
    /// </summary>
	public static class TypescriptClassContentExtensions
	{
		/// <summary>
		/// Turns the TypescriptFunction into a TypescriptClassContent.
		/// </summary>
		/// <param name="typescriptFunction"></param>
		public static TypescriptClassContent ToTypescriptClassContent(this TypescriptFunction typescriptFunction)
		{
			return new TypescriptClassContent(typescriptFunction);
		}
		/// <summary>
		/// Turns the TypescriptProperty into a TypescriptClassContent.
		/// </summary>
		/// <param name="typescriptProperty"></param>
		public static TypescriptClassContent ToTypescriptClassContent(this TypescriptProperty typescriptProperty)
		{
			return new TypescriptClassContent(typescriptProperty);
		}
		/// <summary>
		/// Turns the TypescriptCode into a TypescriptClassContent.
		/// </summary>
		/// <param name="typescriptCode"></param>
		public static TypescriptClassContent ToTypescriptClassContent(this TypescriptCode typescriptCode)
		{
			return new TypescriptClassContent(typescriptCode);
		}
        public static void Match(this IEnumerable<TypescriptClassContent> values, Action<TypescriptFunction> actionForTypescriptFunction,Action<TypescriptProperty> actionForTypescriptProperty,Action<TypescriptCode> actionForTypescriptCode)
        {
            values.Match<TypescriptClassContent, TypescriptFunction,TypescriptProperty,TypescriptCode>(actionForTypescriptFunction,actionForTypescriptProperty,actionForTypescriptCode);
        }

        public static IEnumerable<TreturnType> Match<TreturnType>(this IEnumerable<TypescriptClassContent> values, Func<TypescriptFunction, TreturnType> functionForTypescriptFunction,Func<TypescriptProperty, TreturnType> functionForTypescriptProperty,Func<TypescriptCode, TreturnType> functionForTypescriptCode)
        {
            return values.Match<TypescriptClassContent, TypescriptFunction,TypescriptProperty,TypescriptCode, TreturnType>(functionForTypescriptFunction,functionForTypescriptProperty,functionForTypescriptCode);
        }
	}
}

