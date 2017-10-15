using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeAsCommandLine
{
    public class CommandParser
    {
        public static string[] ParseCommand(string command)
        {
            var result = command.ToCharArray()
                .Aggregate(new ParseResult(), ParseCharacter);
            return ParseResult.WithCurrentResultAsArgument(result).Arguments.ToArray();
        }

        private static ParseResult ParseCharacter(ParseResult previousResult, char currentChar)
        {
            if (!previousResult.PreviouseIsEscape && currentChar == '\\')
            {
                return new ParseResult(previousResult) { PreviouseIsEscape = true };
            }

            if (!previousResult.PreviouseIsEscape &&
                currentChar == '"')
            {
                return new ParseResult(previousResult) { InQuotes = !previousResult.InQuotes };
            }

            if (currentChar == ' ' && !previousResult.InQuotes)
            {
                return ParseResult.WithCurrentResultAsArgument(previousResult);
            }

            return ParseResult.WithAdditionalCharacter(previousResult, currentChar);
        }

        private class ParseResult
        {
            public ParseResult()
            {
            }

            internal static ParseResult WithCurrentResultAsArgument(ParseResult previous)
            {
                var value = new ParseResult(previous);
                value.Arguments.Add(value.CurrentValue);
                value.CurrentValue = "";
                return value;
            }

            internal static ParseResult WithAdditionalCharacter(ParseResult previous, char currentChar)
            {
                var value = new ParseResult(previous);
                value.CurrentValue += currentChar;
                value.PreviouseIsEscape = false;
                return value;
            }

            public ParseResult(ParseResult previous)
            {
                this.InQuotes = previous.InQuotes;
                this.PreviouseIsEscape = previous.PreviouseIsEscape;
                this.CurrentValue = previous.CurrentValue;
                this.Arguments.AddRange(previous.Arguments);
            }

            public bool InQuotes { get; set; } = false;

            public bool PreviouseIsEscape { get; set; } = false;

            public string CurrentValue { get; set; } = "";

            public new List<string> Arguments { get; set; } = new List<string>();
        }
    }
}