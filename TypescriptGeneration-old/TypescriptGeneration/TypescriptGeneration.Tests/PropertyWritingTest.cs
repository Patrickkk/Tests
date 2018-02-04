using FluentAssertions;
using TypescriptGeneration.Model;
using Xunit;

namespace TypescriptGeneration.Test
{
    public class PropertyWritingTest
    {
        [Fact]
        public void TestSimpleProperty()
        {
            var expected =
@"public TestProperty: TestClass;
";
            var property = new TypescriptProperty
            {
                Name = "TestProperty",
                Type = new TypescriptType(TestData.simpleClass)
            };

            AssertThatWritingPropertyGivesTheExpectedResult(expected, property);
        }

        private static void AssertThatWritingPropertyGivesTheExpectedResult(string expectedresult, TypescriptProperty tsProperty)
        {
            var writer = new TypescriptWriter();
            writer.WriteProperty(tsProperty);
            var result = writer.ToString();

            result.ShouldBeEquivalentTo(expectedresult);
        }
    }
}