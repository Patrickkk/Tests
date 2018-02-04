namespace TypescriptGeneration.Model
{
    using FunctionalSharp.OptionTypes;
    using System;

    /// <summary>
    /// A representation of a typescript property.
    /// </summary>
    [Serializable]
    public class TypescriptProperty : TypescriptNamedType
    {
        /// <summary>
        /// The typescript type of this property that can cosist of TypescriptPrimitiveType, TypescriptClass or TypescriptInterface.
        /// </summary>
        /// <returns>The typescriptType</returns>
        public TypescriptType Type { get; set; }

        /// <summary>
        /// The Acces modifier for Typescript methods and properties.
        /// </summary>
        /// <returns>TypescriptAccesModifier</returns>
        public TypescriptAccesModifier Accesability { get; set; } = TypescriptAccesModifier.@public;

        /// <summary>
        /// The default value of this property.
        /// </summary>
        /// <returns>the default value as a string.</returns>
        public IOption<string> DefaultValue { get; set; } = new None<string>();
    }
}
