
namespace TypescriptGeneration.Model
{
	using System;
	using System.Linq;
	using FunctionalSharp.DiscriminatedUnions;
	using System.Collections.Generic;
	

    /// <summary>
    /// Represents a discriminated union of the following types:TypescriptBaseClass,TypescriptBaseInterface. 
    /// </summary>
	[Serializable]
	public class TypescriptInterfaceBaseType : DiscriminatedUnion<TypescriptBaseClass,TypescriptBaseInterface>
	{
		/// <summary>
		/// Creates a new TypescriptInterfaceBaseType representing a TypescriptBaseClass.
		/// </summary>
		/// <param name="typescriptBaseClass"></param>
		public TypescriptInterfaceBaseType(TypescriptBaseClass typescriptBaseClass) : base(typescriptBaseClass) { }
		/// <summary>
		/// Creates a new TypescriptInterfaceBaseType representing a TypescriptBaseInterface.
		/// </summary>
		/// <param name="typescriptBaseInterface"></param>
		public TypescriptInterfaceBaseType(TypescriptBaseInterface typescriptBaseInterface) : base(typescriptBaseInterface) { }
	}


    /// <summary>
    /// Represents a list of TypescriptInterfaceBaseType. 
    /// </summary>
    [Serializable]
    public class TypescriptInterfaceBaseTypes : List<TypescriptInterfaceBaseType>
    {
		/// <summary>
		/// creates a new TypescriptInterfaceBaseTypes without contents.
		/// </summary>
		public TypescriptInterfaceBaseTypes() : base() { }

		/// <summary>
		/// creates a new TypescriptInterfaceBaseTypes with the starting values as its contents.
		/// <param name="values">startingValues</param>
		/// </summary>
		public TypescriptInterfaceBaseTypes(IEnumerable<TypescriptInterfaceBaseType> values) : base(values) { }

		/// <summary>
		/// creates a new TypescriptInterfaceBaseTypes with the starting values as its contents.
		/// <param name="typescriptBaseClass">startingValues</param>
		/// </summary>
		public TypescriptInterfaceBaseTypes(IEnumerable<TypescriptBaseClass> typescriptBaseClass) : base(typescriptBaseClass.Select(x => x.ToTypescriptInterfaceBaseType())) { }

        /// <summary>
        /// Creates and adds a new TypescriptInterfaceBaseType using the given TypescriptBaseClass.
        /// </summary>
        /// <param name="typescriptBaseClass">The typescriptBaseClass to add.</param>
        public void Add(TypescriptBaseClass typescriptBaseClass)
        {
            Add(new TypescriptInterfaceBaseType(typescriptBaseClass));
        }

		/// <summary>
		/// creates a new TypescriptInterfaceBaseTypes with the starting values as its contents.
		/// <param name="typescriptBaseInterface">startingValues</param>
		/// </summary>
		public TypescriptInterfaceBaseTypes(IEnumerable<TypescriptBaseInterface> typescriptBaseInterface) : base(typescriptBaseInterface.Select(x => x.ToTypescriptInterfaceBaseType())) { }

        /// <summary>
        /// Creates and adds a new TypescriptInterfaceBaseType using the given TypescriptBaseInterface.
        /// </summary>
        /// <param name="typescriptBaseInterface">The typescriptBaseInterface to add.</param>
        public void Add(TypescriptBaseInterface typescriptBaseInterface)
        {
            Add(new TypescriptInterfaceBaseType(typescriptBaseInterface));
        }

    }

    /// <summary>
    /// provides extensionmethods for TypescriptInterfaceBaseType. 
    /// </summary>
	public static class TypescriptInterfaceBaseTypeExtensions
	{
		/// <summary>
		/// Turns the TypescriptBaseClass into a TypescriptInterfaceBaseType.
		/// </summary>
		/// <param name="typescriptBaseClass"></param>
		public static TypescriptInterfaceBaseType ToTypescriptInterfaceBaseType(this TypescriptBaseClass typescriptBaseClass)
		{
			return new TypescriptInterfaceBaseType(typescriptBaseClass);
		}
		/// <summary>
		/// Turns the TypescriptBaseInterface into a TypescriptInterfaceBaseType.
		/// </summary>
		/// <param name="typescriptBaseInterface"></param>
		public static TypescriptInterfaceBaseType ToTypescriptInterfaceBaseType(this TypescriptBaseInterface typescriptBaseInterface)
		{
			return new TypescriptInterfaceBaseType(typescriptBaseInterface);
		}
        public static void Match(this IEnumerable<TypescriptInterfaceBaseType> values, Action<TypescriptBaseClass> actionForTypescriptBaseClass,Action<TypescriptBaseInterface> actionForTypescriptBaseInterface)
        {
            values.Match<TypescriptInterfaceBaseType, TypescriptBaseClass,TypescriptBaseInterface>(actionForTypescriptBaseClass,actionForTypescriptBaseInterface);
        }

        public static IEnumerable<TreturnType> Match<TreturnType>(this IEnumerable<TypescriptInterfaceBaseType> values, Func<TypescriptBaseClass, TreturnType> functionForTypescriptBaseClass,Func<TypescriptBaseInterface, TreturnType> functionForTypescriptBaseInterface)
        {
            return values.Match<TypescriptInterfaceBaseType, TypescriptBaseClass,TypescriptBaseInterface, TreturnType>(functionForTypescriptBaseClass,functionForTypescriptBaseInterface);
        }
	}
}

