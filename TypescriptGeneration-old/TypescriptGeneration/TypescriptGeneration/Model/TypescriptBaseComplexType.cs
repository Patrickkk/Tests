namespace TypescriptGeneration.Model
{
    using System;

    /// <summary>
    /// The base type for typescript types that are basetypes.
    /// </summary>
    [Serializable]
    public abstract class TypescriptBaseComplexType : TypescriptNamedType
    {
        /// <summary>
        /// The generic type arguments for this type. 
        /// </summary>
        /// <returns>A List of generic type arguments.</returns>
        public TypescriptGenericTypeArguments GenericArguments { get; set; } = new TypescriptGenericTypeArguments();
    }
}
