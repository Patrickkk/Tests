using FileEtl.Console.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileEtl.Console
{
    internal class Program
    {
        private static Type[] types;
        private static EtlProcessFactory factory;

        private static void Main(string[] args)
        {
            types = typeof(Program).Assembly.GetTypes();
            factory = new EtlProcessFactory();
            while (true)
            {
                WriteAvailableDataTypes();
                var command = System.Console.ReadLine();
                if (command == "q")
                {
                    break;
                }
                else
                {
                    ProcessCommand(command);
                }
            }
        }

        private static void ProcessCommand(string command)
        {
            var parts = command.Split(' ');
            switch (parts[0].ToLower())
            {
                case "add":
                    AddStep(command);
                    break;

                case "config":
                    ConfigureStep(command);
                    break;

                default:
                    break;
            }
        }

        private static void ConfigureStep(string command)
        {
            var trimmed = command.TrimStart("config".ToCharArray()).TrimStart(' ');
            var positionText = trimmed.Substring(0, trimmed.IndexOf(' '));
            var position = int.Parse(positionText);
        }

        private static void AddStep(string command)
        {
            var type = typeof(FixedSingleFileDataSource);
            var step = (IDataSource)Activator.CreateInstance(type);
            factory.EtlSteps.Add(step);
        }

        private static void WriteAvailableDataTypes()
        {
            var availableData = factory.AvailableDataTypesAtStep(factory.EtlSteps.Count);
            var availableSteps = GetStepsForData(availableData);
            System.Console.WriteLine("available data:" + string.Join(", ", availableData));
            System.Console.WriteLine("available steps:" + string.Join(", ", availableSteps));
        }

        private static IEnumerable<Type> GetStepsForData(IEnumerable<Type> availableData)
        {
            return types.Where(x => x.ImplementsOpenGenericInterface(typeof(IDataSource<>)))
                .Concat(types.Where(x => x.ImplementsOpenGenericInterface(typeof(ITransformer<,>)))
                             .Where(x => availableData.Contains(x.GetTransformerInputType())));
        }
    }
}