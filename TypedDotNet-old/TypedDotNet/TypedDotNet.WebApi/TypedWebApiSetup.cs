using System;
using System.Collections.Generic;
using System.Reflection;

namespace TypedDotNet.WebApi
{
    public class TypedWebApiSetup : ITypedNetSetup
    {
        public Func<string, string> TypeNameFormatter { get; set; } = (name) => name;
        public Func<string, string> PropertyNameFormatter { get; set; } = (name) => name;
        public Func<string, string> FunctionNameFormatter { get; set; } = (name) => name;
        public List<Func<Type, bool>> ConditionsToVisitTypes { get; set; } = new List<Func<Type, bool>>();
        public int MyProperty { get; set; }

        public TypedWebApiSetup()
        {
            ConditionsToVisitTypes.Add((type) => type.GetCustomAttribute<TypescriptApiAttribute>(true) != null);
        }
    }
}
