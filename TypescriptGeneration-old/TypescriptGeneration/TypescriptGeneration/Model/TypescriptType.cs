
namespace TypescriptGeneration.Model
{
	using System;
	using System.Linq;
	using FunctionalSharp.DiscriminatedUnions;
	using System.Collections.Generic;
	

    /// <summary>
    /// Represents a discriminated union of the following types:TypescriptPrimitiveType,TypescriptClass,TypescriptInterface,TypescriptEnumerable,TypescriptGenericTypeParameter. 
    /// </summary>
	[Serializable]
	public class TypescriptType : DiscriminatedUnion<TypescriptPrimitiveType,TypescriptClass,TypescriptInterface,TypescriptEnumerable,TypescriptGenericTypeParameter>
	{
		/// <summary>
		/// Creates a new TypescriptType representing a TypescriptPrimitiveType.
		/// </summary>
		/// <param name="typescriptPrimitiveType"></param>
		public TypescriptType(TypescriptPrimitiveType typescriptPrimitiveType) : base(typescriptPrimitiveType) { }
		/// <summary>
		/// Creates a new TypescriptType representing a TypescriptClass.
		/// </summary>
		/// <param name="typescriptClass"></param>
		public TypescriptType(TypescriptClass typescriptClass) : base(typescriptClass) { }
		/// <summary>
		/// Creates a new TypescriptType representing a TypescriptInterface.
		/// </summary>
		/// <param name="typescriptInterface"></param>
		public TypescriptType(TypescriptInterface typescriptInterface) : base(typescriptInterface) { }
		/// <summary>
		/// Creates a new TypescriptType representing a TypescriptEnumerable.
		/// </summary>
		/// <param name="typescriptEnumerable"></param>
		public TypescriptType(TypescriptEnumerable typescriptEnumerable) : base(typescriptEnumerable) { }
		/// <summary>
		/// Creates a new TypescriptType representing a TypescriptGenericTypeParameter.
		/// </summary>
		/// <param name="typescriptGenericTypeParameter"></param>
		public TypescriptType(TypescriptGenericTypeParameter typescriptGenericTypeParameter) : base(typescriptGenericTypeParameter) { }
	}


    /// <summary>
    /// Represents a list of TypescriptType. 
    /// </summary>
    [Serializable]
    public class TypescriptTypeList : List<TypescriptType>
    {
		/// <summary>
		/// creates a new TypescriptTypeList without contents.
		/// </summary>
		public TypescriptTypeList() : base() { }

		/// <summary>
		/// creates a new TypescriptTypeList with the starting values as its contents.
		/// <param name="values">startingValues</param>
		/// </summary>
		public TypescriptTypeList(IEnumerable<TypescriptType> values) : base(values) { }

		/// <summary>
		/// creates a new TypescriptTypeList with the starting values as its contents.
		/// <param name="typescriptPrimitiveType">startingValues</param>
		/// </summary>
		public TypescriptTypeList(IEnumerable<TypescriptPrimitiveType> typescriptPrimitiveType) : base(typescriptPrimitiveType.Select(x => x.ToTypescriptType())) { }

        /// <summary>
        /// Creates and adds a new TypescriptType using the given TypescriptPrimitiveType.
        /// </summary>
        /// <param name="typescriptPrimitiveType">The typescriptPrimitiveType to add.</param>
        public void Add(TypescriptPrimitiveType typescriptPrimitiveType)
        {
            Add(new TypescriptType(typescriptPrimitiveType));
        }

		/// <summary>
		/// creates a new TypescriptTypeList with the starting values as its contents.
		/// <param name="typescriptClass">startingValues</param>
		/// </summary>
		public TypescriptTypeList(IEnumerable<TypescriptClass> typescriptClass) : base(typescriptClass.Select(x => x.ToTypescriptType())) { }

        /// <summary>
        /// Creates and adds a new TypescriptType using the given TypescriptClass.
        /// </summary>
        /// <param name="typescriptClass">The typescriptClass to add.</param>
        public void Add(TypescriptClass typescriptClass)
        {
            Add(new TypescriptType(typescriptClass));
        }

		/// <summary>
		/// creates a new TypescriptTypeList with the starting values as its contents.
		/// <param name="typescriptInterface">startingValues</param>
		/// </summary>
		public TypescriptTypeList(IEnumerable<TypescriptInterface> typescriptInterface) : base(typescriptInterface.Select(x => x.ToTypescriptType())) { }

        /// <summary>
        /// Creates and adds a new TypescriptType using the given TypescriptInterface.
        /// </summary>
        /// <param name="typescriptInterface">The typescriptInterface to add.</param>
        public void Add(TypescriptInterface typescriptInterface)
        {
            Add(new TypescriptType(typescriptInterface));
        }

		/// <summary>
		/// creates a new TypescriptTypeList with the starting values as its contents.
		/// <param name="typescriptEnumerable">startingValues</param>
		/// </summary>
		public TypescriptTypeList(IEnumerable<TypescriptEnumerable> typescriptEnumerable) : base(typescriptEnumerable.Select(x => x.ToTypescriptType())) { }

        /// <summary>
        /// Creates and adds a new TypescriptType using the given TypescriptEnumerable.
        /// </summary>
        /// <param name="typescriptEnumerable">The typescriptEnumerable to add.</param>
        public void Add(TypescriptEnumerable typescriptEnumerable)
        {
            Add(new TypescriptType(typescriptEnumerable));
        }

		/// <summary>
		/// creates a new TypescriptTypeList with the starting values as its contents.
		/// <param name="typescriptGenericTypeParameter">startingValues</param>
		/// </summary>
		public TypescriptTypeList(IEnumerable<TypescriptGenericTypeParameter> typescriptGenericTypeParameter) : base(typescriptGenericTypeParameter.Select(x => x.ToTypescriptType())) { }

        /// <summary>
        /// Creates and adds a new TypescriptType using the given TypescriptGenericTypeParameter.
        /// </summary>
        /// <param name="typescriptGenericTypeParameter">The typescriptGenericTypeParameter to add.</param>
        public void Add(TypescriptGenericTypeParameter typescriptGenericTypeParameter)
        {
            Add(new TypescriptType(typescriptGenericTypeParameter));
        }

    }

    /// <summary>
    /// provides extensionmethods for TypescriptType. 
    /// </summary>
	public static class TypescriptTypeExtensions
	{
		/// <summary>
		/// Turns the TypescriptPrimitiveType into a TypescriptType.
		/// </summary>
		/// <param name="typescriptPrimitiveType"></param>
		public static TypescriptType ToTypescriptType(this TypescriptPrimitiveType typescriptPrimitiveType)
		{
			return new TypescriptType(typescriptPrimitiveType);
		}
		/// <summary>
		/// Turns the TypescriptClass into a TypescriptType.
		/// </summary>
		/// <param name="typescriptClass"></param>
		public static TypescriptType ToTypescriptType(this TypescriptClass typescriptClass)
		{
			return new TypescriptType(typescriptClass);
		}
		/// <summary>
		/// Turns the TypescriptInterface into a TypescriptType.
		/// </summary>
		/// <param name="typescriptInterface"></param>
		public static TypescriptType ToTypescriptType(this TypescriptInterface typescriptInterface)
		{
			return new TypescriptType(typescriptInterface);
		}
		/// <summary>
		/// Turns the TypescriptEnumerable into a TypescriptType.
		/// </summary>
		/// <param name="typescriptEnumerable"></param>
		public static TypescriptType ToTypescriptType(this TypescriptEnumerable typescriptEnumerable)
		{
			return new TypescriptType(typescriptEnumerable);
		}
		/// <summary>
		/// Turns the TypescriptGenericTypeParameter into a TypescriptType.
		/// </summary>
		/// <param name="typescriptGenericTypeParameter"></param>
		public static TypescriptType ToTypescriptType(this TypescriptGenericTypeParameter typescriptGenericTypeParameter)
		{
			return new TypescriptType(typescriptGenericTypeParameter);
		}
        public static void Match(this IEnumerable<TypescriptType> values, Action<TypescriptPrimitiveType> actionForTypescriptPrimitiveType,Action<TypescriptClass> actionForTypescriptClass,Action<TypescriptInterface> actionForTypescriptInterface,Action<TypescriptEnumerable> actionForTypescriptEnumerable,Action<TypescriptGenericTypeParameter> actionForTypescriptGenericTypeParameter)
        {
            values.Match<TypescriptType, TypescriptPrimitiveType,TypescriptClass,TypescriptInterface,TypescriptEnumerable,TypescriptGenericTypeParameter>(actionForTypescriptPrimitiveType,actionForTypescriptClass,actionForTypescriptInterface,actionForTypescriptEnumerable,actionForTypescriptGenericTypeParameter);
        }

        public static IEnumerable<TreturnType> Match<TreturnType>(this IEnumerable<TypescriptType> values, Func<TypescriptPrimitiveType, TreturnType> functionForTypescriptPrimitiveType,Func<TypescriptClass, TreturnType> functionForTypescriptClass,Func<TypescriptInterface, TreturnType> functionForTypescriptInterface,Func<TypescriptEnumerable, TreturnType> functionForTypescriptEnumerable,Func<TypescriptGenericTypeParameter, TreturnType> functionForTypescriptGenericTypeParameter)
        {
            return values.Match<TypescriptType, TypescriptPrimitiveType,TypescriptClass,TypescriptInterface,TypescriptEnumerable,TypescriptGenericTypeParameter, TreturnType>(functionForTypescriptPrimitiveType,functionForTypescriptClass,functionForTypescriptInterface,functionForTypescriptEnumerable,functionForTypescriptGenericTypeParameter);
        }
	}
}

