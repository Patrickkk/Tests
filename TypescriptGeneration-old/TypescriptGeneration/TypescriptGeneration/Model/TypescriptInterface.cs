namespace TypescriptGeneration.Model
{
    using System;
    using FunctionalSharp.OptionTypes;

    /// <summary>
    /// A representation of a typescript interface.
    /// </summary>
    [Serializable]
    public class TypescriptInterface : TypescriptComplexType
    {
        /// <summary>
        /// The base class of this TypescriptInterface
        /// </summary>
        /// <returns>A typescript interface basetype representing a TypescriptBaseClass or Typescript</returns>
        public TypescriptInterfaceBaseTypes BaseType { get; set; } = new TypescriptInterfaceBaseTypes();

        /// <summary>
        /// The content of the typescript interface costisting of TypescriptFunctionSignatures and TypescriptProperties.
        /// </summary>
        /// <returns>A Discriminated union list of TypescriptFunctionSignatures and TypescriptProperties</returns>
        public TypescriptInterfaceContentList Content { get; set; } = new TypescriptInterfaceContentList();
    }
}