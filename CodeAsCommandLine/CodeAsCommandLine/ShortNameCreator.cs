using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeAsCommandLine
{
    internal class ShortNameCreator
    {
        public static string GetShortNameFor(string name, IEnumerable<string> exsistingNames)
        {
            return
                GetSingleLetterShortName(name, exsistingNames) ??
                GetCapitalLettersShortName(name, exsistingNames) ??
                GetMinimumUniqueLengthShortName(name, exsistingNames);
        }

        private static string GetMinimumUniqueLengthShortName(string name, IEnumerable<string> exsistingNames)
        {
            var length = StringLengthUntillNoMoreDuplicates(exsistingNames);
            return name.Substring(0, length);
        }

        private static string GetCapitalLettersShortName(string name, IEnumerable<string> exsistingNames)
        {
            var capitalLettersName = string.Concat(name.Where(x => char.IsUpper(x)));
            if (!exsistingNames.Contains(capitalLettersName, StringComparer.OrdinalIgnoreCase))
            {
                return capitalLettersName;
            }
            return null;
        }

        private static string GetSingleLetterShortName(string name, IEnumerable<string> exsistingNames)
        {
            if (!exsistingNames.Contains(name.Substring(0, 1), StringComparer.OrdinalIgnoreCase))
            {
                return name.Substring(0, 1);
            }
            return null;
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