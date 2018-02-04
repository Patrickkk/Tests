using System;
using FluentAssertions;
using TypescriptGeneration.Model;
using Xunit;

namespace TypescriptGeneration.Test
{
    public class ModuleWritingTest
    {
        [Fact]
        public void WriteEmptyModule()
        {
            var expectedResult =
@"module TestModule {
}
";
            var module = new TypescriptModule
            {
                Name = "TestModule",
            };

            AssertThatWritingModuleGivesTheExpectedResult(expectedResult, module);
        }

        [Fact]
        public void WriteModuleWithClass()
        {
            var expectedResult =
@"module TestModule {
    class TestClass {
    }
}
";
            var module = new TypescriptModule
            {
                Name = "TestModule",
            };
            module.Content.Add(TestData.simpleClass);
            AssertThatWritingModuleGivesTheExpectedResult(expectedResult, module);
        }

        [Fact]
        public void WriteModuleWithCode()
        {
            var expectedResult =
@"module TestModule {
    var a = 1;
    var b = 2;
}
";
            var module = new TypescriptModule
            {
                Name = "TestModule",
            };
            module.Content.Add(new TypescriptCode { "var a = 1;", "var b = 2;" });
            AssertThatWritingModuleGivesTheExpectedResult(expectedResult, module);
        }

        [Fact]
        public void WriteModuleWithModule()
        {
            var expectedResult =
@"module TestModule {
    module TestModule2 {
    }
}
";
            var module = new TypescriptModule
            {
                Name = "TestModule",
            };
            module.Content.Add(new TypescriptModule
            {
                Name = "TestModule2",
            });
            AssertThatWritingModuleGivesTheExpectedResult(expectedResult, module);
        }

        public void WriteModuleWithInterface()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void WriteComplexModule()
        {
            var expectedResult =
@"module TestModule {
    class TestClass<Type1, Type2> extends TestClassBase<Type1, Type2> {
    }

    interface TestInterface<Type1, Type2> {
    }

    class TestClass {
    }

    function TestFunction(): void{
        return 0;
    }
}
";
            var module = new TypescriptModule
            {
                Name = "TestModule",
            };
            module.Content.Add(TestData.classWithGenericBaseAndGenerics);
            module.Content.Add(TestData.interfaceWithGenerics);
            module.Content.Add(TestData.simpleClass);
            module.Content.Add(TestData.simpleFunction);

            AssertThatWritingModuleGivesTheExpectedResult(expectedResult, module);
        }

        private static void AssertThatWritingModuleGivesTheExpectedResult(string expectedresult, TypescriptModule tsModule)
        {
            var writer = new TypescriptWriter();
            writer.WriteModule(tsModule);
            var result = writer.ToString();

            result.ShouldBeEquivalentTo(expectedresult);
        }
    }
}