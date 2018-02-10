namespace TypescriptGeneration.Model
{
    using System;

    /// <summary>
    /// The base class for typescript type representations with generic type parameters and names.
    /// </summary>
    [Serializable]
    public abstract class TypescriptComplexType : TypescriptNamedType
    {
        /// <summary>
        /// The generic type parameters of this type.
        /// </summary>
        /// <returns>A List of generic type parameters</returns>
        public TypescriptGenericTypeParameters GenricTypeParameters { get; set; } = new TypescriptGenericTypeParameters();
    }
}
