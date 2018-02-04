using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TypedDotNet.Test.TestModel.Classes;
using TypescriptGeneration;
using TypescriptGeneration.Model;

namespace TypedDotNet.Test
{
    [TestClass]
    public class ClassesConvertedToInterfacesTests
    {
        [TestMethod]
        public void ConvertSingleClassToInterface()
        {
            var propertyNames = new string[] { "MyPropertyString", "MyPropertyInt", "EnumProperty", "NullableDateTime", "GuidProperty" };
            var model = new TypescriptModel();
            var result = new TypescriptClassesAsInterfaceCreator().GetTypeFor(typeof(SomeClass), model);
            result.Match(primitive => Assert.Fail(),
                         tsclass => Assert.Fail(),
                         tsInerface =>
                         {
                             Assert.AreEqual(tsInerface.Name, typeof(SomeClass).Name);
                             tsInerface.Content.Match(functionSignature => Assert.Fail(), property => Assert.IsTrue(propertyNames.Contains(property.Name)));
                         },
                         tsenum => Assert.Fail(),
                         tsTypeParam => Assert.Fail());
        }

        [TestMethod]
        public void ConvertClassInherritingFromOtherClassToInterface()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestConvertingModelToInterfacesAndWriteAsString()
        {
            var typescriptTypeCreator = new TypescriptClassesAsInterfaceCreator();
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