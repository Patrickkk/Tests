using FluentAssertions;
using FunctionalSharp.OptionTypes;
using TypescriptGeneration.Model;
using Xunit;

namespace TypescriptGeneration.Test
{
    public class InterfaceWritingTest
    {
        [Fact]
        public void WriteSimpleInterface()
        {
            var expectedResult =
@"interface TestInterface {
}
";
            var testInterface = new TypescriptInterface
            {
                Name = "TestInterface"
            };
            AssertThatWritingInterfaceGivesTheExpectedResult(expectedResult, testInterface);
        }

        [Fact]
        public void WriteInterfaceWithGenericsAndPropertiesAndFunctions()
        {
            var expectedResult =
@"interface TestInterface<T> {
    TestProperty: number;
    TestMethodGenericT<T>(): void;
    TestMethodNumber(): number;
}
";
            var tsInterface = new TypescriptInterface
            {
                Name = "TestInterface",
                GenricTypeParameters = new TypescriptGenericTypeParameters { new TypescriptGenericTypeParameter { Name = "T" } }
            };
            tsInterface.Content.Add(TestData.TestPropertyNumber);
            tsInterface.Content.Add(TestData.TestMethodWithGenericTParameter);
            tsInterface.Content.Add(TestData.TestMethodSignatureReturningNumber);

            AssertThatWritingInterfaceGivesTheExpectedResult(expectedResult, tsInterface);
        }

        [Fact]
        public void WriteInterfaceWithBaseInterface()
        {
            var expectedResult =
@"interface TestInterface extends BaseInterface {
}
";
            var testInterface = new TypescriptInterface
            {
                Name = "TestInterface",
                BaseType = new TypescriptInterfaceBaseTypes
                {
                    new TypescriptBaseInterface
                {
                    Name = "BaseInterface"
                }.ToTypescriptInterfaceBaseType()
                }
            };

            AssertThatWritingInterfaceGivesTheExpectedResult(expectedResult, testInterface);
        }

        [Fact]
        public void WriteInterfaceWithBaseClass()
        {
            var expectedResult =
@"interface TestInterface extends BaseClass {
}
";
            var testInterface = new TypescriptInterface
            {
                Name = "TestInterface",
                BaseType = new TypescriptInterfaceBaseTypes { new TypescriptBaseClass
                    {
                        Name = "BaseClass"
                    }
                }
            };

            AssertThatWritingInterfaceGivesTheExpectedResult(expectedResult, testInterface);
        }

        private static void AssertThatWritingInterfaceGivesTheExpectedResult(string expectedresult, TypescriptInterface tsInterface)
        {
            var writer = new TypescriptWriter();
            writer.WriteInterface(tsInterface);
            var result = writer.ToString();

            result.ShouldBeEquivalentTo(expectedresult);
        }
    }
}