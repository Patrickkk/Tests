using FluentAssertions;
using Xunit;

namespace CodeAsCommandLine.Tests
{
    public class CommandPArserTests
    {
        [Fact]
        public void WhenParsingAsStringWithTwoValuesSeperatedBySpaces2ResultsAreExpected()
        {
            var command = @"A B";
            var result = CommandParser.ParseCommand(command);
            result.Should().Equal("A", "B");
        }

        [Fact]
        public void WhenPArsingQuotedValuesTheReultsShouldNotHaveQuotes()
        {
            var command = @"""A A"" ""B B""";
            var result = CommandParser.ParseCommand(command);
            result.Should().Equal("A A", "B B");
        }

        [Fact]
        public void WhenParsingQuotedValuesWithEscapedQuotesTheEscapedQuoteShouldEndUpAsAValue()
        {
            var command = @"""A\""A"" ""B\""B""";
            var result = CommandParser.ParseCommand(command);
            result.Should().Equal(@"A""A", @"B""B");
        }

        [Fact]
        public void WhenParsingQuotedValuesWithEscapedBackSlashesTheyShouldEndUpAsAValue()
        {
            var command = @"""A\\A"" ""B\\\B""";
            var result = CommandParser.ParseCommand(command);
            result.Should().Equal(@"A\A", @"B\B");
        }
    }
}