namespace TypescriptGeneration.Model
{
    using System;

    /// <summary>
    /// Represents a typescript type with a name.
    /// </summary>
    [Serializable]
    public abstract class TypescriptNamedType
    {
        /// <summary>
        /// The name of this type
        /// </summary>
        /// <returns>The name as a string.</returns>
        public String Name { get; set; }
    }
}
