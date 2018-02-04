using System;
using TypescriptGeneration.Model;

namespace TypedDotNet
{
    public interface ITypescriptTypeCreator
    {
        TypescriptType GetTypeFor(Type type, TypescriptModel model);

        void SetTypeCreatorRoot(ITypescriptTypeCreator typescriptTypeCreatorRoot);
        // TODO: so we dont require reqursion of all the types TypescriptType GetDeclarationOnlyFor(Type type, TypescriptModel model);
    }
}