using System;
using System.Linq;
using System.Collections.Generic;

namespace TypescriptGeneration
{
    static class StringExtensions
    {
        public static string JoinStrings(this IEnumerable<String> strings)
        {
            return String.Join("", strings.ToArray());
        }
    }
}
