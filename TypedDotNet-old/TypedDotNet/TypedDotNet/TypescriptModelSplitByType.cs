using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypescriptGeneration.Model;

namespace TypedDotNet
{
    public class TypescriptModelSplitByType
    {
        public Dictionary<Type, TypescriptClass> classes { get; set; } = new Dictionary<Type, TypescriptClass>();
        public Dictionary<Type, TypescriptInterface> interfaces { get; set; } = new Dictionary<Type, TypescriptInterface>();
        public Dictionary<Type, TypescriptModule> modules { get; set; } = new Dictionary<Type, TypescriptModule>();
        public Dictionary<Type, TypescriptEnumerable> enumerables { get; set; } = new Dictionary<Type, TypescriptEnumerable>();
    }
}
