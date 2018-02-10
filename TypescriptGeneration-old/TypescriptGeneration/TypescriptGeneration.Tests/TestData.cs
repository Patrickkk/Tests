using FunctionalSharp.OptionTypes;
using TypescriptGeneration.Model;

namespace TypescriptGeneration.Test
{
    public class TestData
    {
        internal static readonly TypescriptClass simpleClass = new TypescriptClass
        {
            Name = "TestClass",
        };

        internal static readonly TypescriptClass classWithGenerics = new TypescriptClass
        {
            Name = "TestClass",
            GenricTypeParameters = new TypescriptGenericTypeParameters
                {
                    new TypescriptGenericTypeParameter { Name = "Type1" },
                    new TypescriptGenericTypeParameter { Name = "Type2" }
                },
        };

        internal static readonly TypescriptClass classWithBaseClass = new TypescriptClass
        {
            Name = "TestClass",
            BaseClass = new TypescriptBaseClass
            {
                Name = "TestClassBase",
            }.ToOption()
        };

        internal static readonly TypescriptClass classWithBaseAndGenerics = new TypescriptClass
        {
            Name = "TestClass",
            GenricTypeParameters = new TypescriptGenericTypeParameters
                {
                    new TypescriptGenericTypeParameter { Name = "Type1" },
                    new TypescriptGenericTypeParameter { Name = "Type2" }
                },
            BaseClass = new TypescriptBaseClass
            {
                Name = "TestClassBase",
            }.ToOption()
        };

        internal static readonly TypescriptClass classWithGenericBaseAndGenerics = new TypescriptClass
        {
            Name = "TestClass",
            GenricTypeParameters = new TypescriptGenericTypeParameters
                {
                    new TypescriptGenericTypeParameter { Name = "Type1" },
                    new TypescriptGenericTypeParameter { Name = "Type2" }
                },
            BaseClass = new TypescriptBaseClass
            {
                Name = "TestClassBase",
                GenericArguments = new TypescriptGenericTypeArguments
                {
                    new TypescriptGenericTypeParameter { Name = "Type1" },
                    new TypescriptGenericTypeParameter { Name = "Type2" }
                },
            }.ToOption()
        };

        internal static readonly TypescriptInterface interfaceWithGenerics = new TypescriptInterface
        {
            Name = "TestInterface",
            GenricTypeParameters = new TypescriptGenericTypeParameters
                {
                    new TypescriptGenericTypeParameter { Name = "Type1" },
                    new TypescriptGenericTypeParameter { Name = "Type2" }
                },
        };
        internal static readonly TypescriptFunction simpleFunction = new TypescriptFunction
        {
            Name = "TestFunction",
            MethodBody = new TypescriptCode { "return 0;" }
        };
        internal static readonly TypescriptProperty TestPropertyNumber = new TypescriptProperty
        {
            Name = "TestProperty",
            Type = new TypescriptType(TypescriptPrimitiveType.number)
        };
        internal static readonly TypescriptFunctionSignature TestMethodSignatureReturningNumber = new TypescriptFunctionSignature
        {
            Name = "TestMethodNumber",
            ReturnType = new TypescriptType(TypescriptPrimitiveType.number)
        };
        internal static readonly TypescriptFunctionSignature TestMethodWithGenericTParameter = new TypescriptFunctionSignature
        {
            Name = "TestMethodGenericT",
            GenricTypeParameters = new TypescriptGenericTypeParameters
                {
                    new TypescriptGenericTypeParameter { Name = "T" },
                },
        };
    }
}
