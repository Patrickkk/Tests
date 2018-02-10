namespace TypescriptGeneration.Model
{
    using System;

    /// <summary>
    /// A representation of a typescript module.
    /// </summary>
    [Serializable]
    public class TypescriptModule : TypescriptNamedType
    {
        /// <summary>
        /// The content of the module consisting of TypescriptModule,TypescriptClass, TypescriptInterface, TypescriptFunction, TypescriptEnumerable and TypescriptCode.
        /// </summary>
        /// <returns>TypescriptModuleContent</returns>
        public TypescriptModuleContent Content { get; set; } = new TypescriptModuleContent();
    }
}
