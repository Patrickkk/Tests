namespace TypescriptGeneration.Model
{
    using System;

    /// <summary>
    /// A representation of the typescript primitive types.
    /// </summary>
    [Serializable]
    public enum TypescriptPrimitiveType
    {
        /// <summary>
        /// A Typesript number.
        /// </summary>
        number,
        /// <summary>
        /// A Typescript boolean.
        /// </summary>
        boolean,
        /// <summary>
        /// A stypescript string.
        /// </summary>
        @string,
        /// <summary>
        /// A void type.
        /// </summary>
        @void,
        /// <summary>
        /// A untyped type.
        /// </summary>
        any
    }
}