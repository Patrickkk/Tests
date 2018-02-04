namespace TypescriptGeneration.Model
{
    using System;

    /// <summary>
    /// A representation of a function parameter in typescript.
    /// </summary>
    [Serializable]
    public class TypescriptParameter : TypescriptNamedType
    {
        /// <summary>
        /// The type of parameter.
        /// </summary>
        /// <returns>A typescript type representing TypescriptPrimitiveType, TypescriptClass or TypescriptInterface.</returns>
        public TypescriptType TypescriptType { get; set; }
    }
}