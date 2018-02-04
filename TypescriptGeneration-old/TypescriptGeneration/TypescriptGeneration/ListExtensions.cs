using System.Collections.Generic;
using System.Linq;

namespace TypescriptGeneration
{
    static class ListExtensions
    {
        public static List<T> AllButLast<T>(this List<T> values)
        {
            return values.Take(values.Count - 1).ToList();
        }
    }
}
