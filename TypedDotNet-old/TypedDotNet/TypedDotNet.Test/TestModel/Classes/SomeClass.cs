using System;
using System.Collections.Generic;
using TypedDotNet.Test.TestModel.Enums;
using TypedDotNet.Test.TestModel.Interfaces;

namespace TypedDotNet.Test.TestModel.Classes
{
    public class SomeClass : ISomeInterface
    {
        public string MyPropertyString { get; set; }

        public int MyPropertyInt { get; set; }

        public SimpleEnum EnumProperty { get; set; }

        public DateTime? NullableDateTime { get; set; }

        public Guid GuidProperty { get; set; }

        public List<string> StringsList { get; set; }
    }
}