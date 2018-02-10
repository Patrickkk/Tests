using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypedDotNet.Typescriptcreators;
using TypescriptGeneration;

namespace TypedDotNet.Test
{
    [TestClass]
    public class ClassesConvertedToTypescriptTypes
    {
        [TestMethod]
        public void ConvertModelToTypescriptModel()
        {
            var typescriptTypeCreator = new TypescriptTypeCreator(new TypescriptClassCreator(), new TypescriptInterfaceCreator(), null);
            var typesToConvert = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.DefinedTypes)
                .Where(x => x.Namespace != null && x.Namespace.StartsWith("TypedDotNet.Test.TestModel"));

            var model = typescriptTypeCreator.CreateTypescriptModelFor(typesToConvert);

            var writer = new TypescriptWriter();
            foreach (var typescriptType in model.knownTypes)
            {
                typescriptType.Value.Match(
                    x => { },
                    x => writer.WriteClass(x),
                    x => writer.WriteInterface(x),
                    x => writer.WriteEnum(x),
                    x => { }
                    );
            }
            var result = writer.ToString();
        }
    }
}