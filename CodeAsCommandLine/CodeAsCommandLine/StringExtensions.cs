using System.Collections.Generic;
using System.Linq;

namespace CodeAsCommandLine
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, params T[] values)
        {
            foreach (var item in source)
            {
                yield return item;
            }
            foreach (var item in values)
            {
                yield return item;
            }
        }

        public static bool None<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }
    }

    public static class StringExtensions
    {
        public static string SafeSubstring(this string value, int startIndex, int length)
        {
            if (value.Length > length + startIndex)
            {
                return value.Substring(startIndex, length);
            }
            return value.Substring(startIndex);
        }
    }
}