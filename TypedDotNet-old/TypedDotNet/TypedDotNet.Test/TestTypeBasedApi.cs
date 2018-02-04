using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypedDotNet.Test.TestModel.Interfaces;

namespace TypedDotNet.Test
{
    [TestClass]
    public class TestTypeBasedApi
    {
        [TestMethod]
        public void TestMethod1()
        {
            Type x = typeof(ISomeInterface);
            x.TypescriptImplementedInterfaces();
            var types = X();
            
        }

        private IEnumerable<TypeInfo> X()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.DefinedTypes);
        }
    }
}
