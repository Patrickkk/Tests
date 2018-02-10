
namespace TypescriptGeneration.Model
{
    using System;
    using FunctionalSharp.DiscriminatedUnions;

    /// <summary>
    /// Represents the content of a module consisting of TypescriptModule,TypescriptClass, TypescriptInterface, TypescriptFunction, TypescriptEnumerable and TypescriptCode.
    /// </summary>
    [Serializable]
    public class TypescriptModuleContent : DiscriminatedUnionList<TypescriptModule,TypescriptClass, TypescriptInterface, TypescriptFunction, TypescriptEnumerable, TypescriptCode>
    {
    }
}
