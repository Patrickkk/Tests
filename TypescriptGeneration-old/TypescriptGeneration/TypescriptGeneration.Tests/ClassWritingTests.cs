using FluentAssertions;
using FunctionalSharp.DiscriminatedUnions;
using FunctionalSharp.OptionTypes;
using TypescriptGeneration.Model;
using Xunit;

namespace TypescriptGeneration.Test
{
    public class ClassWritingTests
    {
        [Fact]
        public void TestEmptyClass()
        {
            var a = new DiscriminatedUnionList<int, string>();
            var b = a.Clone();
            var expectedResult =
@"class TestClass {
}
";
            AssertThatWritingClassGivesTheExpectedResult(expectedResult, TestData.simpleClass);
        }

        [Fact]
        public void TestEmptyclassWithBaseClass()
        {
            var expectedResult =
@"class TestClass extends TestClassBase {
}
";
            var tsClass = TestData.classWithBaseClass;
            AssertThatWritingClassGivesTheExpectedResult(expectedResult, tsClass);
        }

        [Fact]
        public void TestEmptyclassWithBaseClassAndGenericArgument()
        {
            var expectedResult =
@"class TestClass<Type1> extends TestClassBase {
}
";
            var tsClass = new TypescriptClass
            {
                Name = "TestClass",
                GenricTypeParameters = new TypescriptGenericTypeParameters { new TypescriptGenericTypeParameter { Name = "Type1" } },
                BaseClass = new TypescriptBaseClass
                {
                    Name = "TestClassBase"
                }.ToOption()
            };

            AssertThatWritingClassGivesTheExpectedResult(expectedResult, tsClass);
        }

        [Fact]
        public void TestEmptyclassWithBaseClassAndGenericArguments()
        {
            var expectedResult =
@"class TestClass<Type1, Type2> extends TestClassBase {
}
";
            AssertThatWritingClassGivesTheExpectedResult(expectedResult, TestData.classWithBaseAndGenerics);
        }

        [Fact]
        public void TestEmptyclassWithBaseClassAndGenericArgumentsAndBaseClassWithParameter()
        {
            var expectedResult =
@"class TestClass<Type1, Type2> extends TestClassBase<Type1> {
}
";
            var tsClass = new TypescriptClass
            {
                Name = "TestClass",
                BaseClass = new TypescriptBaseClass
                {
                    Name = "TestClassBase",
                }.ToOption()
            };
            tsClass.GenricTypeParameters.Add(new TypescriptGenericTypeParameter { Name = "Type1" });
            tsClass.GenricTypeParameters.Add(new TypescriptGenericTypeParameter { Name = "Type2" });
            tsClass.BaseClass.IfNotNullDo(baseClass => baseClass.GenericArguments.Add(tsClass.GenricTypeParameters[0]));

            AssertThatWritingClassGivesTheExpectedResult(expectedResult, tsClass);
        }

        [Fact]
        public void TestEmptyclassWithBaseClassAndGenericArgumentsAndBaseClassWithParameters()
        {
            var expectedResult =
@"class TestClass<Type1, Type2> extends TestClassBase<Type1, Type2> {
}
";
            AssertThatWritingClassGivesTheExpectedResult(expectedResult, TestData.classWithGenericBaseAndGenerics);
        }

        [Fact]
        public void TestClassWithProperty()
        {
            var expectedResult =
@"class TestClass {
    private TestProperty: number = 1;
}
";
            var tsClass = TestData.simpleClass.Clone();
            tsClass.Content.Add(new TypescriptProperty
            {
                Name = "TestProperty",
                Accesability = TypescriptAccesModifier.@private,
                DefaultValue = "1".ToOption(),
                Type = new TypescriptType(TypescriptPrimitiveType.number)
            });
            AssertThatWritingClassGivesTheExpectedResult(expectedResult, tsClass);
        }

        [Fact]
        public void TestClassImplementingInterfaces()
        {
            var expectedResult =
@"class TestClass implements TestInterface, TestInterface2 {
}
";
            var tsClass = TestData.simpleClass.Clone();
            tsClass.InterfaceImplementations.Add(new TypescriptInterface { Name = "TestInterface" });
            tsClass.InterfaceImplementations.Add(new TypescriptInterface { Name = "TestInterface2" });
            AssertThatWritingClassGivesTheExpectedResult(expectedResult, tsClass);
        }

        private static void AssertThatWritingClassGivesTheExpectedResult(string expectedresult, TypescriptClass tsClass)
        {
            var writer = new TypescriptWriter();
            writer.WriteClass(tsClass);
            var result = writer.ToString();

            result.ShouldBeEquivalentTo(expectedresult);
        }
    }
}