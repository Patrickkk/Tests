namespace TypescriptGeneration.Model
{
    using FunctionalSharp.OptionTypes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A representation of a typescript class.
    /// </summary>
    [Serializable]
    public class TypescriptClass : TypescriptComplexType
    {
        /// <summary>
        /// The baseclass for this class.
        /// </summary>
        /// <returns>A TypescriptBaseClass</returns>
        public IOption<TypescriptBaseClass> BaseClass { get; set; } = new None<TypescriptBaseClass>();

        /// <summary>
        /// The interfaces that this class implements.
        /// </summary>
        /// <returns>A List of TypescriptInterfaces</returns>
        public List<TypescriptInterface> InterfaceImplementations { get; set; } = new List<TypescriptInterface>();

        /// <summary>
        /// The content of this typescript class cosisting of TypescriptFunction, TypescriptProperty or TypescriptCode.
        /// </summary>
        /// <returns></returns>
        public TypescriptClassContentList Content { get; set; } = new TypescriptClassContentList();

        /// <summary>
        /// The typescriptclass representation of an array.
        /// </summary>
        /// <returns></returns>
        public static TypescriptClass TypescriptArray
        {
            get
            {
                var array = new TypescriptClass
                {
                    Name = "Array"
                };
                array.GenricTypeParameters.Add(new TypescriptGenericTypeParameter { Name = "T" });
                return array;
            }
        }
    }
}
