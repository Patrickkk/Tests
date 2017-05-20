using System;
using System.Collections.Generic;
using System.Linq;

namespace FileEtl.ShadowCopy
{
    public static class LinqExtensions
    {
        public static IEnumerable<TResult> OuterJoin<T, TKey, TResult>(IEnumerable<T> source1Items, IEnumerable<T> source2Items, Func<T, TKey> keySelector, Func<T, T, TResult> resultSelector)
            where T : class
        {
            var keys = source1Items
                .Concat(source2Items)
                .Select(x => keySelector(x))
                .Distinct();

            var source1Lookup = source1Items.ToLookup(x => keySelector(x));
            var source2Lookup = source2Items.ToLookup(x => keySelector(x));

            return (from key in keys
                    from source1 in source1Lookup[key].DefaultIfEmpty(null)
                    from source2 in source2Lookup[key].DefaultIfEmpty(null)
                    select resultSelector(source1, source2)).ToList();
        }
    }
}