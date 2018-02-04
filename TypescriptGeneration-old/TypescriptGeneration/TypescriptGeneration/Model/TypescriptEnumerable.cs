namespace TypescriptGeneration.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A representation of a typescript enumerable.
    /// </summary>
    [Serializable]
    public class TypescriptEnumerable : TypescriptNamedType
    {
        /// <summary>
        /// All options in the enumerable.
        /// </summary>
        /// <returns>A List of strings with the enumerable names.</returns>
        public List<string> Options { get; set; }
    }
}