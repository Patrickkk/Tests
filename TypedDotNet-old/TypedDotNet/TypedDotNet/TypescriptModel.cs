using System;
using System.Collections.Generic;
using System.Linq;
using TypescriptGeneration.Model;

namespace TypedDotNet
{
    public class TypescriptModel
    {
        public Dictionary<Type, TypescriptType> knownTypes { get; set; } = new Dictionary<Type, TypescriptType>();

        public TypescriptModelSplitByType ToSplitModel()
        {
            var classes = knownTypes
                .Where(x => x.Value
                    .Match(primitive => false, tsClass => false, tInterface => false, tsEnum => false, genericParam => false)
                ).ToDictionary(x => x.Key, x => x.Value
                    .Match<TypescriptClass>(primitive => null, tsClass => null, tInterface => null, tsEnum => null, genericParam => null)
                );


            return new TypescriptModelSplitByType
            {
                classes = knownTypes
                .Where(x => x.Value
                    .Match(primitive => false, tsClass => true, tInterface => false, tsEnum => false, genericParam => false)
                ).ToDictionary(x => x.Key, x => x.Value
                    .Match<TypescriptClass>(primitive => null, tsClass => tsClass, tInterface => null, tsEnum => null, genericParam => null)
                ),
                enumerables = knownTypes
                .Where(x => x.Value
                    .Match(primitive => false, tsClass => false, tInterface => false, tsEnum => true, genericParam => false)
                ).ToDictionary(x => x.Key, x => x.Value
                    .Match<TypescriptEnumerable>(primitive => null, tsClass => null, tInterface => null, tsEnum => tsEnum, genericParam => null)
                ),
                interfaces = knownTypes
                .Where(x => x.Value
                    .Match(primitive => false, tsClass => false, tInterface => true, tsEnum => false, genericParam => false)
                ).ToDictionary(x => x.Key, x => x.Value
                    .Match<TypescriptInterface>(primitive => null, tsClass => null, tInterface => tInterface, tsEnum => null, genericParam => null)
                ),
            };
        }
    }
}