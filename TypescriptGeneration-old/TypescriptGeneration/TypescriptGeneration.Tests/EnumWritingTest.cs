using System.Collections.Generic;
using FluentAssertions;
using TypescriptGeneration.Model;
using Xunit;

namespace TypescriptGeneration.Test
{
    public class EnumWritingTest
    {
        [Fact]
        public void TestWritingSimpleEnum()
        {
            var expectedResult =
@"enum Test {
    value1,
    value2
}
";
            var enumerable = new TypescriptEnumerable
            {
                Name = "Test",
                Options = new List<string> { "value1", "value2" }
            };
            AssertThatWritingEnumGivesTheExpectedResult(expectedResult, enumerable);
        }

        [Fact]
        public void WriteEnumProperty()
        {
            var expectedResult =
@"class Test {
    public PropertyName: EnumName;
}
";
            var enumerable = new TypescriptClass
            {
                Name = "Test",
                Content = new TypescriptClassContentList
                {
                    new TypescriptProperty { Name = "PropertyName", Type = new TypescriptEnumerable { Name = "EnumName" }.ToTypescriptType() }
                }
            };
            AssertThatWritingEnumGivesTheExpectedResult(expectedResult, enumerable);
        }

        private void AssertThatWritingEnumGivesTheExpectedResult(string expectedResult, TypescriptClass tsClass)
        {
            var writer = new TypescriptWriter();
            writer.WriteClass(tsClass);
            var result = writer.ToString();

            result.ShouldBeEquivalentTo(expectedResult);
        }

        private static void AssertThatWritingEnumGivesTheExpectedResult(string expectedresult, TypescriptEnumerable enumerable)
        {
            var writer = new TypescriptWriter();
            writer.WriteEnum(enumerable);
            var result = writer.ToString();

            result.ShouldBeEquivalentTo(expectedresult);
        }
    }
}