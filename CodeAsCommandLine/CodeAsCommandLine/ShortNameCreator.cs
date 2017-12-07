using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeAsCommandLine
{
    internal class ShortNameCreator
    {
        public static string AddShortNames(string name, IEnumerable<string> allNames)
        {
            var length = StringLengthUntillNoMoreDuplicates(allNames);
            return CreateShortName(name, length);
        }

        private static string CreateShortName(string parameterName, int length)
        {
            return parameterName.Substring(0, length);
        }

        private static int StringLengthUntillNoMoreDuplicates(IEnumerable<string> values)
        {
            var length = 0;
            while (true)
            {
                length += 1;
                var duplicates = values
                    .Select(value => value.SafeSubstring(0, length))
                    .GroupBy(value => value)
                    .Any(value => value.Count() > 1);
                if (!duplicates)
                {
                    return length;
                }
                if (length > values.Max(x => x.Length))
                {
                    throw new Exception("values aready contains duplicates at the maximum length");
                }
            }
        }
    }
}