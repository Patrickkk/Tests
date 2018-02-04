namespace TypescriptGeneration.Model
{
    using System;

    /// <summary>
    /// A representation of a typescript function.
    /// </summary>
    [Serializable]
    public class TypescriptFunction : TypescriptFunctionSignature
    {
        /// <summary>
        /// The method body of this typescript method.
        /// </summary>
        /// <returns>the methodbody as TypescriptCode</returns>
        public TypescriptCode MethodBody { get; set; } = new TypescriptCode();

        /// <summary>
        /// defines the accesability of this function.
        /// </summary>
        /// <returns>The accesability as TypescriptAccesModifier</returns>
        public TypescriptAccesModifier Accesability { get; set; } = TypescriptAccesModifier.@public;
    }
}
