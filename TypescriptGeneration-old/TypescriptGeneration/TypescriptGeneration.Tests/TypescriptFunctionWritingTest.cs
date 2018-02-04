using System.Collections.Generic;
using FluentAssertions;
using TypescriptGeneration.Model;
using Xunit;

namespace TypescriptGeneration.Test
{
    public class TypescriptFunctionWritingTest
    {
        [Fact]
        public void WriteFunctionSignature()
        {
            var expectedResult =
@"function TestFunction(): void";
            var tsFunctionSignature = new TypescriptFunctionSignature
            {
                Name = "TestFunction",
            };

            AssertThatWritingFunctionSignatureGivesTheExpectedResult(expectedResult, tsFunctionSignature);
        }

        [Fact]
        public void WriteFunctionSignatureWithComplexParameters()
        {
            var expectedResult =
@"function TestFunction<Type1, Type2>(testParameter: TestClass<Type1, Type2>): void";
            var tsFunctionsignature = new TypescriptFunctionSignature
            {
                Name = "TestFunction",
                GenricTypeParameters = new TypescriptGenericTypeParameters { new TypescriptGenericTypeParameter { Name = "Type1"},
                new TypescriptGenericTypeParameter { Name = "Type2"}},
                Parameters = new List<TypescriptParameter>
                {
                    new TypescriptParameter {
                        Name = "testParameter",
                        TypescriptType = new TypescriptType(TestData.classWithBaseAndGenerics)
                    }
                }
            };

            AssertThatWritingFunctionSignatureGivesTheExpectedResult(expectedResult, tsFunctionsignature);
        }

        [Fact]
        public void WriteFunctionWithMethodBody()
        {
            var expectedResult =
@"function TestFunction(): void{
    var a = 1;
}
";
            var function = new TypescriptFunction
            {
                Name = "TestFunction",
                MethodBody = new TypescriptCode
                {
                    "var a = 1;"
                }
            };
            AssertThatWritingFunctionGivesTheExpectedResult(expectedResult, function);
        }

        private static void AssertThatWritingFunctionGivesTheExpectedResult(string expectedResult, TypescriptFunction tsFunction)
        {
            var writer = new TypescriptWriter();
            writer.WriteFunction(tsFunction, true);
            var result = writer.ToString();

            result.ShouldBeEquivalentTo(expectedResult);
        }

        private void AssertThatWritingFunctionSignatureGivesTheExpectedResult(string expectedResult, TypescriptFunctionSignature tsFunctionSignature)
        {
            var writer = new TypescriptWriter();
            writer.WriteTypescriptFunctionSignature(tsFunctionSignature, true);
            var result = writer.ToString();

            result.ShouldBeEquivalentTo(expectedResult);
        }
    }
}