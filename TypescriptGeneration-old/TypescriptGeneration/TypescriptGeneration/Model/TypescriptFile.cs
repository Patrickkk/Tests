using FunctionalSharp.DiscriminatedUnions;

namespace TypescriptGeneration.Model
{
    /// <summary>
    /// Represenst a typescript file. Beiing  DiscriminatedUnionList of TypescriptModule, TypescriptClass, TypescriptInterface, TypescriptFunction, TypescriptEnumerable and TypescriptCode.
    /// </summary>
    public class TypescriptFileContent : DiscriminatedUnionList<TypescriptModule, TypescriptClass, TypescriptInterface, TypescriptFunction, TypescriptEnumerable, TypescriptCode>
    {
    }
}
