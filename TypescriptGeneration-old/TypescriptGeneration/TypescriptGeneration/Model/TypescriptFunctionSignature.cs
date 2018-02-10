namespace TypescriptGeneration.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// A representation of a typescript function signature.
    /// </summary>
    public class TypescriptFunctionSignature : TypescriptComplexType
    {
        /// <summary>
        /// The return type of the function.
        /// </summary>
        /// <returns>A typescript type. By default this is void.</returns>
        public TypescriptType ReturnType { get; set; } = new TypescriptType(TypescriptPrimitiveType.@void);
        /// <summary>
        /// The parameters 
        /// </summary>
        /// <returns>A list of TypescriptTarameters.</returns>
        public IList<TypescriptParameter> Parameters { get; set; } = new List<TypescriptParameter>();
    }
}
