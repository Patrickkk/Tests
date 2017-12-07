using System;
using System.Collections.Generic;
using System.Text;

namespace CodeAsCommandLine
{
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